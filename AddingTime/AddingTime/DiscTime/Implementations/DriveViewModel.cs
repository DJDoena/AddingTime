namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    using System.ComponentModel;
    using AbstractionLayer.IOServices;

    internal sealed class DriveViewModel : IDriveViewModel
    {
        public DriveViewModel(IDriveInfo actual)
        {
            this.Actual = actual;
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IDriveViewModel

        public string Label => this.Actual.DriveLabel;

        public IDriveInfo Actual { get; private set; }

        public void Refresh() => this.RaisePropertyChanged(nameof(this.Label));

        #endregion

        private void RaisePropertyChanged(string attribute) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(attribute));
    }
}