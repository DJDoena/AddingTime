using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.AbstractionLayer.UIServices;
using DoenaSoft.ToolBox.Extensions;

namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    internal sealed class DiscTimeDataModel : IDiscTimeDataModel
    {
        private readonly IIOServices IOServices;

        private readonly IUIServices UIServices;

        internal DiscTimeDataModel(IIOServices ioServices
            , IUIServices uiServices)
        {
            IOServices = ioServices;
            UIServices = uiServices;
            
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
                IDiscInfo discInfo = DiscInfoFactory.GetDiscInfo(drive, IOServices);

                if (discInfo != null)
                {
                    Scan(discInfo);

                    UIServices.ShowMessageBox("Done.", String.Empty, Buttons.OK, Icon.Information);
                }
                else
                {
                    UIServices.ShowMessageBox("Disc could not be read!", "Error", Buttons.OK, Icon.Warning);
                }
            }
            else
            {
                UIServices.ShowMessageBox("The drive is not ready!", "Error", Buttons.OK, Icon.Warning);
            }
        }

        public IEnumerable<ITreeNode> GetCheckedNodes()
            => (GetSubNodes(IsChecked));

        public void CheckAllNodes()
        {
            foreach (ITreeNode node in GetCheckableNodes())
            {
                node.IsChecked = true;
            }
        }

        #endregion

        private void Scan(IDiscInfo discInfo)
        {
            Dictionary<String, List<ISubsetInfo>> structuredSubsets = SubsetStructurer.GetStructuredSubsets(discInfo);

            foreach (KeyValuePair<String, List<ISubsetInfo>> kvp in structuredSubsets)
            {
                TryAddSubsetNode(kvp);
            }
        }

        #region AddSubsetNode

        private void TryAddSubsetNode(KeyValuePair<String, List<ISubsetInfo>> subset)
        {
            IEnumerable<ISubsetInfo> subsets = GetFilteredSubsets(subset.Value);

            if (subsets.HasItems())
            {
                AddSubsetNode(subset.Key, subsets);
            }
        }

        private IEnumerable<ISubsetInfo> GetFilteredSubsets(IEnumerable<ISubsetInfo> subsets)
        {
            subsets = subsets.Where(subset => subset.IsValid);

            subsets = subsets.Where(SubsetHasTrackWithMinimumLength);

            subsets = subsets.ToList();

            return (subsets);
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
        {
            IEnumerable<TreeNode> tracks = GetTracks(subsets);

            TreeNode[] array = tracks.ToArray();

            foreach (TreeNode item in array)
            {
                subsetNode.Nodes.Add(item);
            }
        }

        #region GetTracks

        private IEnumerable<TreeNode> GetTracks(IEnumerable<ISubsetInfo> subsets)
        {
            IEnumerable<IEnumerable<TreeNode>> nested = GetTracksFromSubsets(subsets);

            IEnumerable<TreeNode> flat = nested.SelectMany(item => item);

            return (flat);
        }

        private IEnumerable<IEnumerable<TreeNode>> GetTracksFromSubsets(IEnumerable<ISubsetInfo> subsets)
        {
            foreach (ISubsetInfo subset in subsets)
            {
                IEnumerable<ITrackInfo> tracks = GetFilteredTracks(subset);

                IEnumerable<TreeNode> trackNodes = GetTrackNodesFromTrack(tracks);

                yield return (trackNodes);
            }
        }

        private IEnumerable<ITrackInfo> GetFilteredTracks(ISubsetInfo subset)
        {
            IEnumerable<ITrackInfo> tracks = subset.Tracks;

            tracks = tracks.Where(TrackHasMinimumLength);

            return (tracks);
        }

        private IEnumerable<TreeNode> GetTrackNodesFromTrack(IEnumerable<ITrackInfo> tracks)
        {
            foreach (ITrackInfo track in tracks)
            {
                TreeNode trackNode = GetTrackNode(track);

                yield return (trackNode);
            }
        }

        private TreeNode GetTrackNode(ITrackInfo track)
        {
            TimeSpan runningTime = track.RunningTime;

            String text = runningTime.ToString();

            TreeNode trackNode = new TreeNode(text, runningTime);

            return (trackNode);
        }

        #endregion

        private Boolean SubsetHasTrackWithMinimumLength(ISubsetInfo subset)
        {
            Boolean subsetHasTrackWithMinimumLength = subset.Tracks.HasItemsWhere(TrackHasMinimumLength);

            return (subsetHasTrackWithMinimumLength);
        }

        private Boolean TrackHasMinimumLength(ITrackInfo track)
        {
            Decimal totalSeconds = Convert.ToDecimal(track.RunningTime.TotalSeconds);

            Decimal minimumSeconds = MinimumTrackLength * 60;

            Boolean trackHasMinimumLength = (totalSeconds >= minimumSeconds);

            return (trackHasMinimumLength);
        }

        #endregion

        #region GetSubNodes

        private IEnumerable<ITreeNode> GetSubNodes(Func<ITreeNode, Boolean> predicate)
        {
            IEnumerable<IEnumerable<ITreeNode>> nested = GetSubNodesFromNodes(predicate);

            IEnumerable<ITreeNode> flat = nested.SelectMany(item => item);

            return (flat);
        }

        private IEnumerable<IEnumerable<ITreeNode>> GetSubNodesFromNodes(Func<ITreeNode, Boolean> predicate)
        {
            foreach (ITreeNode node in DiscTree)
            {
                if (node.Nodes != null)
                {
                    IEnumerable<ITreeNode> subNodes = GetSubNodesFromNode(node, predicate);

                    yield return (subNodes);
                }
            }
        }

        private IEnumerable<ITreeNode> GetSubNodesFromNode(ITreeNode node
            , Func<ITreeNode, Boolean> predicate)
        {
            foreach (ITreeNode subNode in node.Nodes)
            {
                if (predicate(subNode))
                {
                    yield return (subNode);
                }
            }
        }

        private static Boolean CanBeChecked(ITreeNode node)
            => (node.CanBeChecked);

        private static Boolean IsChecked(ITreeNode node)
            => (node.IsChecked);

        #endregion

        private IEnumerable<ITreeNode> GetCheckableNodes()
            => (GetSubNodes(CanBeChecked));
    }
}
