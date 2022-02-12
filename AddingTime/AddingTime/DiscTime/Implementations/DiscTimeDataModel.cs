namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using AbstractionLayer.IOServices;
    using AbstractionLayer.UIServices;
    using ToolBox.Extensions;

    internal sealed class DiscTimeDataModel : IDiscTimeDataModel
    {
        private readonly IIOServices _ioServices;

        private readonly IUIServices _uiServices;

        internal DiscTimeDataModel(IIOServices ioServices, IUIServices uiServices)
        {
            _ioServices = ioServices;

            _uiServices = uiServices;

            this.DiscTree = new ObservableCollection<ITreeNode>();
        }

        #region  IDiscTimeDataModel

        public ObservableCollection<ITreeNode> DiscTree { get; private set; }

        public int MinimumTrackLength { private get; set; }

        public void Scan(IDriveInfo drive)
        {
            this.DiscTree.Clear();

            if (drive.IsReady)
            {
                IDiscInfo discInfo = DiscInfoFactory.GetDiscInfo(drive, _ioServices);

                if (discInfo != null)
                {
                    this.Scan(discInfo);

                    //_UIServices.ShowMessageBox("Done.", string.Empty, Buttons.OK, Icon.Information);
                }
                else
                {
                    _uiServices.ShowMessageBox("Disc could not be read!", "Error", Buttons.OK, Icon.Warning);
                }
            }
            else
            {
                _uiServices.ShowMessageBox("The drive is not ready!", "Error", Buttons.OK, Icon.Warning);
            }
        }

        public IEnumerable<ITreeNode> GetCheckedNodes() => this.GetSubNodes(node => node.IsChecked);

        public void CheckAllNodes() => this.GetSubNodes(node => node.CanBeChecked).ForEach(node => node.IsChecked = true);

        #endregion

        private void Scan(IDiscInfo discInfo) => SubsetStructurer.GetStructuredSubsets(discInfo).ForEach(kvp => this.TryAddSubsetNode(kvp));

        #region AddSubsetNode

        private void TryAddSubsetNode(KeyValuePair<string, List<ISubsetInfo>> subset)
        {
            var subsets = subset.Value.Where(item => item.IsValid).Where(this.SubsetHasTrackWithMinimumLength).ToList();

            if (subsets.HasItems())
            {
                this.AddSubsetNode(subset.Key, subsets);
            }
        }

        private void AddSubsetNode(string text, IEnumerable<ISubsetInfo> subsets)
        {
            var subsetNode = new TreeNode(text);

            this.AddTrackNodes(subsetNode, subsets);

            this.DiscTree.Add(subsetNode);
        }

        private void AddTrackNodes(TreeNode subsetNode, IEnumerable<ISubsetInfo> subsets)
        {
            var tracks = subsets.Select(this.GetTracksFromSubset).SelectMany(track => track);

            var times = new HashSet<TimeSpan>();

            foreach (var track in tracks)
            {
                subsetNode.Nodes.Add(track);

                if (!times.Contains(track.RunningTime))
                {
                    track.IsChecked = true;
                }

                times.Add(track.RunningTime);
            }
        }

        #region GetTracks

        private IEnumerable<TreeNode> GetTracksFromSubset(ISubsetInfo subset) => subset.Tracks.Where(this.TrackHasMinimumLength).Select(track => new TreeNode(track.RunningTime.ToString(), track.RunningTime));

        #endregion

        private bool SubsetHasTrackWithMinimumLength(ISubsetInfo subset) => subset.Tracks.HasItemsWhere(this.TrackHasMinimumLength);

        private bool TrackHasMinimumLength(ITrackInfo track) => Convert.ToDecimal(track.RunningTime.TotalSeconds) >= this.MinimumTrackLength * 60;

        #endregion

        #region GetSubNodes

        private IEnumerable<ITreeNode> GetSubNodes(Func<ITreeNode, bool> predicate) => this.DiscTree.Where(node => node.Nodes != null).Select(node => node.Nodes.Where(predicate)).SelectMany(subNode => subNode);

        #endregion
    }
}