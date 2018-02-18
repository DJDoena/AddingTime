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
        private readonly IIOServices _IOServices;

        private readonly IUIServices _UIServices;

        internal DiscTimeDataModel(IIOServices ioServices
            , IUIServices uiServices)
        {
            _IOServices = ioServices;

            _UIServices = uiServices;

            DiscTree = new ObservableCollection<ITreeNode>();
        }

        #region  IDiscTimeDataModel

        public ObservableCollection<ITreeNode> DiscTree { get; private set; }

        public Int32 MinimumTrackLength { private get; set; }

        public void Scan(IDriveInfo drive)
        {
            DiscTree.Clear();

            if (drive.IsReady)
            {
                IDiscInfo discInfo = DiscInfoFactory.GetDiscInfo(drive, _IOServices);

                if (discInfo != null)
                {
                    Scan(discInfo);

                    _UIServices.ShowMessageBox("Done.", String.Empty, Buttons.OK, Icon.Information);
                }
                else
                {
                    _UIServices.ShowMessageBox("Disc could not be read!", "Error", Buttons.OK, Icon.Warning);
                }
            }
            else
            {
                _UIServices.ShowMessageBox("The drive is not ready!", "Error", Buttons.OK, Icon.Warning);
            }
        }

        public IEnumerable<ITreeNode> GetCheckedNodes()
            => GetSubNodes(node => node.IsChecked);

        public void CheckAllNodes()
            => GetSubNodes(node => node.CanBeChecked).ForEach(node => node.IsChecked = true);

        #endregion

        private void Scan(IDiscInfo discInfo)
            => SubsetStructurer.GetStructuredSubsets(discInfo).ForEach(kvp => TryAddSubsetNode(kvp));

        #region AddSubsetNode

        private void TryAddSubsetNode(KeyValuePair<String, List<ISubsetInfo>> subset)
        {
            IEnumerable<ISubsetInfo> subsets = subset.Value.Where(item => item.IsValid).Where(SubsetHasTrackWithMinimumLength).ToList();

            if (subsets.HasItems())
            {
                AddSubsetNode(subset.Key, subsets);
            }
        }

        private void AddSubsetNode(String text
            , IEnumerable<ISubsetInfo> subsets)
        {
            TreeNode subsetNode = new TreeNode(text);

            AddTrackNodes(subsetNode, subsets);

            DiscTree.Add(subsetNode);
        }

        private void AddTrackNodes(TreeNode subsetNode
            , IEnumerable<ISubsetInfo> subsets)
            => subsets.Select(GetTracksFromSubset).SelectMany(track => track).ForEach(track => subsetNode.Nodes.Add(track));

        #region GetTracks

        private IEnumerable<TreeNode> GetTracksFromSubset(ISubsetInfo subset)
            => subset.Tracks.Where(TrackHasMinimumLength).Select(track => new TreeNode(track.RunningTime.ToString(), track.RunningTime));

        #endregion

        private Boolean SubsetHasTrackWithMinimumLength(ISubsetInfo subset)
            => subset.Tracks.HasItemsWhere(TrackHasMinimumLength);

        private Boolean TrackHasMinimumLength(ITrackInfo track)
            => Convert.ToDecimal(track.RunningTime.TotalSeconds) >= MinimumTrackLength * 60;

        #endregion

        #region GetSubNodes

        private IEnumerable<ITreeNode> GetSubNodes(Func<ITreeNode, Boolean> predicate)
            => DiscTree.Where(node => node.Nodes != null).Select(node => node.Nodes.Where(predicate)).SelectMany(subNode => subNode);

        #endregion
    }
}