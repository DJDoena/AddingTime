namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    internal interface ITreeNode : INotifyPropertyChanged
    {
        String Text { get; }

        TimeSpan RunningTime { get; }

        Boolean IsChecked { get; set; }

        Boolean CanBeChecked { get; }

        ObservableCollection<ITreeNode> Nodes { get; }
    }
}