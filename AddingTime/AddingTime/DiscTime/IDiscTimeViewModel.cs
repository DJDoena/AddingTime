using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime
{
    internal interface IDiscTimeViewModel : INotifyPropertyChanged
    {
        IDriveViewModel SelectedDrive { get; set; }

        Int32 MinimumLength { get; set; }

        IEnumerable<TimeSpan> RunningTimes { get; }

        ObservableCollection<IDriveViewModel> Drives { get; }

        ICommand ScanCommand { get; }

        ICommand SetSitcomLengthCommand { get; }

        ICommand SetDramaLengthCommand { get; }

        ICommand SetMovieLengthCommand { get; }

        ObservableCollection<ITreeNode> DiscTree { get; }

        ICommand CheckAllNodesCommand { get; }

        void CheckForDecrypter();

        event EventHandler<CloseEventArgs> Closing;

        ICommand AcceptCommand { get; }

        ICommand CancelCommand { get; }
    }
}