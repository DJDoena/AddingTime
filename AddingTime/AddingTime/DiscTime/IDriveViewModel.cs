namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime
{
    using System.ComponentModel;
    using AbstractionLayer.IOServices;

    internal interface IDriveViewModel : INotifyPropertyChanged
    {
        string Label { get; }

        IDriveInfo Actual { get; }

        void Refresh();
    }
}