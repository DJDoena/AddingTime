using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.AbstractionLayer.UIServices;
using DoenaSoft.ToolBox.Commands;
using DoenaSoft.ToolBox.Extensions;

namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    internal sealed class DiscTimeViewModel : IDiscTimeViewModel
    {
        private readonly IDiscTimeDataModel DataModel;

        private readonly IUIServices UIServices;

        private static IDriveViewModel s_SelectedDrive;

        private static Int32 s_MinimumLength;

        private static Boolean ShowAnyDVDWarning { get; set; }

        static DiscTimeViewModel()
        {
            s_SelectedDrive = null;
            s_MinimumLength = 5;
            ShowAnyDVDWarning = true;
        }

        internal DiscTimeViewModel(IDiscTimeDataModel dataModel
            , IIOServices ioServices
            , IUIServices uiServices)
        {
            if (dataModel == null)
            {
                throw (new ArgumentNullException(nameof(dataModel)));
            }

            if (ioServices == null)
            {
                throw (new ArgumentNullException(nameof(ioServices)));
            }

            if (uiServices == null)
            {
                throw (new ArgumentNullException(nameof(uiServices)));
            }

            DataModel = dataModel;
            UIServices = uiServices;

            DataModel.MinimumTrackLength = MinimumLength;

            Drives = GetDrives(ioServices);

#if FAKE

            Drives.Add(new DriveViewModel(new FakeDrive()));

#endif

            SelectedDrive = (s_SelectedDrive == null) ? SelectDefaultDrive() : s_SelectedDrive;
        }

        #region  IDiscTimeViewModel

        public IDriveViewModel SelectedDrive
        {
            get
            {
                return (s_SelectedDrive);
            }

            set
            {
                if (value != s_SelectedDrive)
                {
                    s_SelectedDrive = value;

                    RaisePropertyChanged(nameof(SelectedDrive));
                }
            }
        }

        public Int32 MinimumLength
        {
            get
            {
                return (s_MinimumLength);
            }

            set
            {
                DataModel.MinimumTrackLength = value;

                if (value != s_MinimumLength)
                {
                    s_MinimumLength = value;

                    RaisePropertyChanged(nameof(MinimumLength));
                }
            }
        }

        public IEnumerable<TimeSpan> RunningTimes
            => (DataModel.GetCheckedNodes().Select(node => node.RunningTime));

        public ObservableCollection<IDriveViewModel> Drives { get; private set; }

        public ICommand ScanCommand
            => (new RelayCommand(Scan, CanScan));

        public ICommand SetSitcomLengthCommand
            => (new RelayCommand(SetSitcomLength));

        public ICommand SetDramaLengthCommand
            => (new RelayCommand(SetDramaLength));

        public ICommand SetMovieLengthCommand
            => (new RelayCommand(SetMovieLength));

        public ObservableCollection<ITreeNode> DiscTree
            => (DataModel.DiscTree);

        public ICommand CheckAllNodesCommand
            => (new RelayCommand(CheckAllNodes));

        public void CheckForDecrypter()
        {
            IEnumerable<Process> anydvd = Process.GetProcessesByName("AnyDVDtray").Union(Process.GetProcessesByName("DVDFabPasskey"));

            if ((anydvd.HasItems() == false) && (ShowAnyDVDWarning))
            {
                UIServices.ShowMessageBox("For Blu-ray discs, make sure RedFox AnyDVD or DVDFab Passkey is enabled or else you will not get useful results."
                    + Environment.NewLine + "For DVDs it will not hurt either.", "AnyDVD / Passkey", Buttons.OK, Icon.Information);

                ShowAnyDVDWarning = false;
            }
        }

        public event EventHandler<CloseEventArgs> Closing;

        public ICommand AcceptCommand
            => (new RelayCommand(Accept));

        public ICommand CancelCommand
            => (new RelayCommand(Cancel));

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void RaisePropertyChanged(String attribute)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(attribute));
        }

        private IDriveViewModel SelectDefaultDrive()
        {
            foreach (IDriveViewModel drive in Drives)
            {
                if (drive.Actual.IsReady)
                {
                    return (drive);
                }
            }

            return ((Drives.Count > 0) ? Drives[0] : null);
        }

        private void Scan()
        {
            RefreshDrives();

            DataModel.Scan(SelectedDrive.Actual);

            RaisePropertyChanged(nameof(DiscTree));
        }

        private void RefreshDrives()
        {
            foreach (IDriveViewModel drive in Drives)
            {
                drive.Refresh();
            }
        }

        private Boolean CanScan()
            => (SelectedDrive != null);

        private void SetSitcomLength()
        {
            MinimumLength = 15;
        }

        private void SetDramaLength()
        {
            MinimumLength = 35;
        }

        private void SetMovieLength()
        {
            MinimumLength = 60;
        }

        private void CheckAllNodes()
        {
            DataModel.CheckAllNodes();
        }

        private void Accept()
        {
            Close(Result.OK);
        }

        private void Cancel()
        {
            Close(Result.Cancel);
        }

        private void Close(Result result)
        {
            Closing?.Invoke(this, new CloseEventArgs(result));
        }

        private ObservableCollection<IDriveViewModel> GetDrives(IIOServices ioServices)
        {
            IEnumerable<IDriveInfo> drives = ioServices.GetDriveInfos(System.IO.DriveType.CDRom);

            IEnumerable<IDriveViewModel> viewModelDrives = drives.Select(drive => new DriveViewModel(drive));

            return (new ObservableCollection<IDriveViewModel>(viewModelDrives));
        }
    }
}