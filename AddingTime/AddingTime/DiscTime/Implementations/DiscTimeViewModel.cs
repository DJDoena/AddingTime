namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;
    using AbstractionLayer.IOServices;
    using AbstractionLayer.UIServices;
    using DoenaSoft.AbstractionLayer.Commands;
    using ToolBox.Extensions;

    internal sealed class DiscTimeViewModel : IDiscTimeViewModel
    {
        private readonly IDiscTimeDataModel _dataModel;

        private readonly IUIServices _uiServices;

        private static IDriveViewModel _selectedDrive;

        private static int _minimumLength;

        private static bool _showAnyDVDWarning;

        static DiscTimeViewModel()
        {
            _selectedDrive = null;

            _minimumLength = 5;

            _showAnyDVDWarning = true;
        }

        internal DiscTimeViewModel(IDiscTimeDataModel dataModel, IIOServices ioServices, IUIServices uiServices)
        {
            if (ioServices == null)
            {
                throw (new ArgumentNullException(nameof(ioServices)));
            }

            _dataModel = dataModel ?? throw new ArgumentNullException(nameof(dataModel));

            _uiServices = uiServices ?? throw new ArgumentNullException(nameof(uiServices));

            _dataModel.MinimumTrackLength = this.MinimumLength;

            this.Drives = this.GetDrives(ioServices);

#if FAKE

            Drives.Add(new DriveViewModel(new FakeDrive()));

#endif

            this.SelectedDrive = this.Drives.FirstOrDefault(d => d.Actual.DriveLetter == _selectedDrive?.Actual.DriveLetter) ?? this.Drives.FirstOrDefault();
        }

        #region  IDiscTimeViewModel

        public IDriveViewModel SelectedDrive
        {
            get => _selectedDrive;
            set
            {
                if (value != _selectedDrive)
                {
                    _selectedDrive = value;

                    this.RaisePropertyChanged(nameof(this.SelectedDrive));
                }
            }
        }

        public int MinimumLength
        {
            get => _minimumLength;
            set
            {
                _dataModel.MinimumTrackLength = value;

                if (value != _minimumLength)
                {
                    _minimumLength = value;

                    this.RaisePropertyChanged(nameof(this.MinimumLength));
                }
            }
        }

        public IEnumerable<TimeSpan> RunningTimes => _dataModel.GetCheckedNodes().Select(node => node.RunningTime);

        public ObservableCollection<IDriveViewModel> Drives { get; }

        public ICommand ScanCommand => new RelayCommand(this.Scan, this.CanScan);

        public ICommand SetSitcomLengthCommand => new RelayCommand(this.SetSitcomLength);

        public ICommand SetDramaLengthCommand => new RelayCommand(this.SetDramaLength);

        public ICommand SetMovieLengthCommand => new RelayCommand(this.SetMovieLength);

        public ObservableCollection<ITreeNode> DiscTree => _dataModel.DiscTree;

        public ICommand CheckAllNodesCommand => new RelayCommand(this.CheckAllNodes);

        public void CheckForDecrypter()
        {
            var anydvd = Process.GetProcessesByName("AnyDVDtray").Union(Process.GetProcessesByName("DVDFabPasskey"));

            if (!anydvd.HasItems() && _showAnyDVDWarning)
            {
                _uiServices.ShowMessageBox("For Blu-ray discs, make sure RedFox AnyDVD or DVDFab Passkey is enabled or else you will not get useful results."
                    + Environment.NewLine + "For DVDs it will not hurt either.", "AnyDVD / Passkey", Buttons.OK, Icon.Information);

                _showAnyDVDWarning = false;
            }
        }

        public event EventHandler<CloseEventArgs> Closing;

        public ICommand AcceptCommand => new RelayCommand(this.Accept);

        public ICommand CancelCommand => new RelayCommand(this.Cancel);

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void RaisePropertyChanged(string attribute) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(attribute));

        private void Scan()
        {
            this.RefreshDrives();

            _dataModel.Scan(this.SelectedDrive.Actual);

            this.RaisePropertyChanged(nameof(this.DiscTree));
        }

        private void RefreshDrives() => this.Drives.ForEach(drive => drive.Refresh());

        private bool CanScan() => this.SelectedDrive != null;

        private void SetSitcomLength() => this.MinimumLength = 15;

        private void SetDramaLength() => this.MinimumLength = 35;

        private void SetMovieLength() => this.MinimumLength = 70;

        private void CheckAllNodes() => _dataModel.CheckAllNodes();

        private void Accept() => this.Close(Result.OK);

        private void Cancel() => this.Close(Result.Cancel);

        private void Close(Result result) => Closing?.Invoke(this, new CloseEventArgs(result));

        private ObservableCollection<IDriveViewModel> GetDrives(IIOServices ioServices) => new ObservableCollection<IDriveViewModel>(ioServices.GetDriveInfos(System.IO.DriveType.CDRom).Select(drive => new DriveViewModel(drive)));
    }
}