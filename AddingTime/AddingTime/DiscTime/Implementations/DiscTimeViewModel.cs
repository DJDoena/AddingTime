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
    using ToolBox.Commands;
    using ToolBox.Extensions;

    internal sealed class DiscTimeViewModel : IDiscTimeViewModel
    {
        private readonly IDiscTimeDataModel _DataModel;

        private readonly IUIServices _UIServices;

        private static IDriveViewModel _SelectedDrive;

        private static Int32 _MinimumLength;

        static Boolean _ShowAnyDVDWarning;

        static DiscTimeViewModel()
        {
            _SelectedDrive = null;

            _MinimumLength = 5;

            _ShowAnyDVDWarning = true;
        }

        internal DiscTimeViewModel(IDiscTimeDataModel dataModel
            , IIOServices ioServices
            , IUIServices uiServices)
        {
            if (ioServices == null)
            {
                throw (new ArgumentNullException(nameof(ioServices)));
            }

            _DataModel = dataModel ?? throw new ArgumentNullException(nameof(dataModel));

            _UIServices = uiServices ?? throw new ArgumentNullException(nameof(uiServices));

            _DataModel.MinimumTrackLength = MinimumLength;

            Drives = GetDrives(ioServices);

#if FAKE

            Drives.Add(new DriveViewModel(new FakeDrive()));

#endif

            SelectedDrive = Drives.FirstOrDefault();
        }

        #region  IDiscTimeViewModel

        public IDriveViewModel SelectedDrive
        {
            get => _SelectedDrive;
            set
            {
                if (value != _SelectedDrive)
                {
                    _SelectedDrive = value;

                    RaisePropertyChanged(nameof(SelectedDrive));
                }
            }
        }

        public Int32 MinimumLength
        {
            get => _MinimumLength;
            set
            {
                _DataModel.MinimumTrackLength = value;

                if (value != _MinimumLength)
                {
                    _MinimumLength = value;

                    RaisePropertyChanged(nameof(MinimumLength));
                }
            }
        }

        public IEnumerable<TimeSpan> RunningTimes
            => _DataModel.GetCheckedNodes().Select(node => node.RunningTime);

        public ObservableCollection<IDriveViewModel> Drives { get; }

        public ICommand ScanCommand
            => new RelayCommand(Scan, CanScan);

        public ICommand SetSitcomLengthCommand
            => new RelayCommand(SetSitcomLength);

        public ICommand SetDramaLengthCommand
            => new RelayCommand(SetDramaLength);

        public ICommand SetMovieLengthCommand
            => new RelayCommand(SetMovieLength);

        public ObservableCollection<ITreeNode> DiscTree
            => _DataModel.DiscTree;

        public ICommand CheckAllNodesCommand
            => new RelayCommand(CheckAllNodes);

        public void CheckForDecrypter()
        {
            IEnumerable<Process> anydvd = Process.GetProcessesByName("AnyDVDtray").Union(Process.GetProcessesByName("DVDFabPasskey"));

            if ((anydvd.HasItems() == false) && (_ShowAnyDVDWarning))
            {
                _UIServices.ShowMessageBox("For Blu-ray discs, make sure RedFox AnyDVD or DVDFab Passkey is enabled or else you will not get useful results."
                    + Environment.NewLine + "For DVDs it will not hurt either.", "AnyDVD / Passkey", Buttons.OK, Icon.Information);

                _ShowAnyDVDWarning = false;
            }
        }

        public event EventHandler<CloseEventArgs> Closing;

        public ICommand AcceptCommand
            => new RelayCommand(Accept);

        public ICommand CancelCommand
            => new RelayCommand(Cancel);

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void RaisePropertyChanged(String attribute)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(attribute));

        private void Scan()
        {
            RefreshDrives();

            _DataModel.Scan(SelectedDrive.Actual);

            RaisePropertyChanged(nameof(DiscTree));
        }

        private void RefreshDrives()
            => Drives.ForEach(drive => drive.Refresh());

        private Boolean CanScan()
            => SelectedDrive != null;

        private void SetSitcomLength()
            => MinimumLength = 15;

        private void SetDramaLength()
            => MinimumLength = 35;

        private void SetMovieLength()
            => MinimumLength = 60;

        private void CheckAllNodes()
            => _DataModel.CheckAllNodes();

        private void Accept()
            => Close(Result.OK);

        private void Cancel()
            => Close(Result.Cancel);

        private void Close(Result result)
            => Closing?.Invoke(this, new CloseEventArgs(result));

        private ObservableCollection<IDriveViewModel> GetDrives(IIOServices ioServices)
            => new ObservableCollection<IDriveViewModel>(ioServices.GetDriveInfos(System.IO.DriveType.CDRom).Select(drive => new DriveViewModel(drive)));
    }
}