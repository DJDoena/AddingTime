using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime
{
    internal interface ITreeNode : INotifyPropertyChanged
    {
        String Text { get; }

        TimeSpan RunningTime { get; }

        Boolean IsChecked { get; set; }

        Boolean CanBeChecked { get; }

        ObservableCollection<ITreeNode> Nodes { get; }
    }
}
