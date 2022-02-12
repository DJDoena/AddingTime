namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    internal interface ITreeNode : INotifyPropertyChanged
    {
        string Text { get; }

        TimeSpan RunningTime { get; }

        bool IsChecked { get; set; }

        bool CanBeChecked { get; }

        ObservableCollection<ITreeNode> Nodes { get; }
    }
}