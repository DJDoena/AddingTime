namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using AbstractionLayer.IOServices;

    internal interface IDiscTimeDataModel
    {
        ObservableCollection<ITreeNode> DiscTree { get; }

        int MinimumTrackLength { set; }

        void Scan(IDriveInfo drive);

        IEnumerable<ITreeNode> GetCheckedNodes();

        void CheckAllNodes();
    }
}