namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime
{
    using System;
    using System.ComponentModel;
    using AbstractionLayer.IOServices;

    interface IDriveViewModel : INotifyPropertyChanged
    {
        String Label { get; }

        IDriveInfo Actual { get; }

        void Refresh();
    }
}