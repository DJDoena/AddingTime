namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    using System;
    using System.ComponentModel;
    using AbstractionLayer.IOServices;

    internal sealed class DriveViewModel : IDriveViewModel
    {
        public DriveViewModel(IDriveInfo actual)
            => Actual = actual;

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IDriveViewModel
        public String Label
            => Actual.Label;

        public IDriveInfo Actual { get; private set; }

        public void Refresh()
            => RaisePropertyChanged(nameof(Label));

        #endregion

        private void RaisePropertyChanged(String attribute)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(attribute));
    }
}