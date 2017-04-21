using System;
using System.ComponentModel;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime
{
    interface IDriveViewModel : INotifyPropertyChanged
    {
        String Label { get; }

        IDriveInfo Actual { get; }

        void Refresh();
    }
}