using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using DoenaSoft.AbstractionLayer.UIServices;
using DoenaSoft.DVDProfiler.DVDProfilerHelper;
using DoenaSoft.ToolBox.Commands;
using DoenaSoft.ToolBox.Extensions;

namespace DoenaSoft.DVDProfiler.AddingTime.Main.Implementations
{
    internal sealed class MainViewModel : IMainViewModel
    {
        private readonly IMainDataModel DataModel;

        private readonly IMainOutputModel OutputModel;

        private readonly IClipboardServices ClipboardServices;

        private readonly IWindowFactory WindowFactory;

        private readonly IUIServices UIServices;

        private String m_Input;

        private Int32 m_SelectedEpisode;

        private Int32 m_SelectedDisc;

        private Int32 m_SelectedSeason;

        private event PropertyChangedEventHandler m_PropertyChanged;

        internal MainViewModel(IMainDataModel dataModel
            , IMainOutputModel outputModel
            , IClipboardServices clipboardServices
            , IWindowFactory windowFactory
            , IUIServices uiServices)
        {
            #region Checks

            if (dataModel == null)
            {
                throw (new ArgumentNullException(nameof(dataModel)));
            }

            if (outputModel == null)
            {
                throw (new ArgumentNullException(nameof(outputModel)));
            }

            if (clipboardServices == null)
            {
                throw (new ArgumentNullException(nameof(clipboardServices)));
            }

            if (windowFactory == null)
            {
                throw (new ArgumentNullException(nameof(windowFactory)));
            }

            #endregion

            DataModel = dataModel;
            OutputModel = outputModel;
            ClipboardServices = clipboardServices;
            WindowFactory = windowFactory;
            UIServices = uiServices;

            SelectedEpisode = -1;
            SelectedDisc = -1;
            SelectedSeason = -1;
        }

        #region IViewModel

        #region Episodes

        public String Input
        {
            get
            {
                return (m_Input ?? String.Empty);
            }
            set
            {
                m_Input = value;

                OnInputChanged();
            }
        }

        public ObservableCollection<String> Episodes
            => (new ObservableCollection<String>(DataModel.Episodes));

        public Int32 SelectedEpisode
        {
            get
            {
                return (m_SelectedEpisode);
            }
            set
            {
                m_SelectedEpisode = value;

                OnSelectedEpisodeChanged();
            }
        }

        public String EpisodesFullTime
            => (DataModel.EpisodesFullTime ?? String.Empty);

        public String EpisodesMiddleTime
            => (DataModel.EpisodesMiddleTime ?? String.Empty);

        public String EpisodesShortTime
            => (DataModel.EpisodesShortTime ?? String.Empty);

        public ICommand AddEpisodeCommand
            => (new RelayCommand(AddEpisode, CanAddEpisode));

        public ICommand AddFromClipboardCommand
            => (new RelayCommand(AddFromClipboard, CanAddFromClipboard));

        public ICommand RemoveEpisodeCommand
            => (new RelayCommand(RemoveEpisode, CanRemoveEpisode));

        public ICommand ClearEpisodesCommand
            => (new RelayCommand(ClearEpisodes, CanClearEpisodes));

        public ICommand MoveEpisodesCommand
            => (new RelayCommand(MoveEpisodes, HasEpisodeFullTime));

        public ICommand CopyEpisodesCommand
            => (new RelayCommand(CopyEpisodes, HasEpisodeFullTime));

        public ICommand CopyAllEpisodesCommand
            => (new RelayCommand(CopyAllEpisodes, HasEpisodeFullTime));

        #endregion

        #region Discs

        public ObservableCollection<String> Discs
            => (new ObservableCollection<String>(DataModel.Discs));

        public Int32 SelectedDisc
        {
            get
            {
                return (m_SelectedDisc);
            }
            set
            {
                m_SelectedDisc = value;

                OnSelectedDiscChanged();
            }
        }

        public String DiscsFullTime
          => (DataModel.DiscsFullTime ?? String.Empty);

        public String DiscsMiddleTime
            => (DataModel.DiscsMiddleTime ?? String.Empty);

        public String DiscsShortTime
            => (DataModel.DiscsShortTime ?? String.Empty);

        public ICommand RemoveDiscCommand
            => (new RelayCommand(RemoveDisc, CanRemoveDisc));

        public ICommand ClearDiscsCommand
            => (new RelayCommand(ClearDiscs, CanClearDiscs));

        public ICommand MoveDiscsCommand
            => (new RelayCommand(MoveDiscs, HasDiscFullTime));

        public ICommand CopyDiscsCommand
            => (new RelayCommand(CopyDiscs, HasDiscFullTime));

        public ICommand CopyAllDiscsCommand
            => (new RelayCommand(CopyAllDiscs, HasDiscFullTime));

        public ICommand CopyFullDiscsCommand
            => (new RelayCommand(CopyFullDiscs, HasDiscFullTime));

        #endregion

        #region Seasons

        public ObservableCollection<String> Seasons
            => (new ObservableCollection<String>(DataModel.Seasons));

        public Int32 SelectedSeason
        {
            get
            {
                return (m_SelectedSeason);
            }
            set
            {
                m_SelectedSeason = value;

                OnSelectedSeasonChanged();
            }
        }

        public String SeasonsFullTime
          => (DataModel.SeasonsFullTime ?? String.Empty);

        public String SeasonsMiddleTime
            => (DataModel.SeasonsMiddleTime ?? String.Empty);

        public String SeasonsShortTime
            => (DataModel.SeasonsShortTime ?? String.Empty);

        public ICommand RemoveSeasonCommand
            => (new RelayCommand(RemoveSeason, CanRemoveSeason));

        public ICommand ClearSeasonsCommand
            => (new RelayCommand(ClearSeasons, CanClearSeasons));

        public ICommand CopySeasonsCommand
            => (new RelayCommand(CopySeasons, HasSeasonFullTime));

        public ICommand CopyAllSeasonsCommand
            => (new RelayCommand(CopyAllSeasons, HasSeasonFullTime));

        public ICommand CopyFullSeasonsCommand
            => (new RelayCommand(CopyFullSeasons, HasSeasonFullTime));

        #endregion

        public ICommand ClearAllCommand
            => (new RelayCommand(ClearAll, CanClearAll));


        public ICommand CheckForNewVersionCommand
            => (new RelayCommand(CheckForNewVersion));

        public ICommand OpenAboutWindowCommand
            => (new RelayCommand(OpenAboutWindow));

        public ICommand OpenHelpWindowCommand
            => (new RelayCommand(OpenHelpWindow));

        public ICommand OpenReadFromDriveWindowCommand
          => (new RelayCommand(OpenReadFromDriveWindow));

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                if (m_PropertyChanged == null)
                {
                    RegisterModelEvents();
                }

                m_PropertyChanged += value;
            }
            remove
            {
                m_PropertyChanged -= value;

                if (m_PropertyChanged == null)
                {
                    UnregisterModelEvents();
                }
            }
        }

        #endregion

        private void RegisterModelEvents()
        {
            DataModel.EpisodesChanged += OnEpisodesChanged;

            DataModel.EpisodesFullTimeChanged += OnEpisodesFullTimeChanged;
            DataModel.EpisodesMiddleTimeChanged += OnEpisodesMiddleTimeChanged;
            DataModel.EpisodesShortTimeChanged += OnEpisodesShortTimeChanged;

            DataModel.DiscsChanged += OnDiscsChanged;

            DataModel.DiscsFullTimeChanged += OnDiscsFullTimeChanged;
            DataModel.DiscsMiddleTimeChanged += OnDiscsMiddleTimeChanged;
            DataModel.DiscsShortTimeChanged += OnEpisodesShortTimeChanged;

            DataModel.SeasonsChanged += OnSeasonsChanged;
        }

        private void UnregisterModelEvents()
        {
            DataModel.EpisodesChanged -= OnEpisodesChanged;

            DataModel.EpisodesFullTimeChanged -= OnEpisodesFullTimeChanged;
            DataModel.EpisodesMiddleTimeChanged -= OnEpisodesMiddleTimeChanged;
            DataModel.EpisodesShortTimeChanged -= OnEpisodesShortTimeChanged;

            DataModel.DiscsChanged -= OnDiscsChanged;

            DataModel.DiscsFullTimeChanged -= OnDiscsFullTimeChanged;
            DataModel.DiscsMiddleTimeChanged -= OnDiscsMiddleTimeChanged;
            DataModel.DiscsShortTimeChanged -= OnDiscsShortTimeChanged;

            DataModel.SeasonsChanged -= OnSeasonsChanged;
        }

        private void RaisePropertyChanged(String attribute)
        {
            m_PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(attribute));
        }

        #region Episodes

        private void OnSelectedEpisodeChanged()
        {
            RaisePropertyChanged(nameof(SelectedEpisode));
        }

        private void OnInputChanged()
        {
            RaisePropertyChanged(nameof(Input));
            RaisePropertyChanged(nameof(AddEpisodeCommand));
        }

        private void OnEpisodesChanged(Object sender
            , EventArgs e)
        {
            RaisePropertyChanged(nameof(Episodes));
        }

        private void OnEpisodesFullTimeChanged(Object sender
            , EventArgs e)
        {
            RaisePropertyChanged(nameof(EpisodesFullTime));
        }

        private void OnEpisodesMiddleTimeChanged(Object sender
            , EventArgs e)
        {
            RaisePropertyChanged(nameof(EpisodesMiddleTime));
        }

        private void OnEpisodesShortTimeChanged(Object sender
            , EventArgs e)
        {
            RaisePropertyChanged(nameof(EpisodesShortTime));
        }

        private void AddEpisode()
        {
            if (DataModel.AddEpisode(Input))
            {
                Input = null;
            }
        }

        private Boolean CanAddEpisode()
            => (String.IsNullOrEmpty(Input) == false);

        private void AddFromClipboard()
        {
            Input = ClipboardServices.GetText();

            AddEpisode();
        }

        private Boolean CanAddFromClipboard()
            => (ClipboardServices.ContainsText);

        private void RemoveEpisode()
        {
            Int32 selectedIndex = SelectedEpisode;

            DataModel.RemoveEpisode(selectedIndex);

            SelectedEpisode = (Episodes.Count > selectedIndex) ? selectedIndex : (selectedIndex - 1);
        }

        private Boolean CanRemoveEpisode()
            => (SelectedEpisode != -1);

        private void ClearEpisodes()
        {
            DataModel.ClearEpisodes();
        }

        private Boolean CanClearEpisodes()
            => (Episodes.HasItems());

        private void MoveEpisodes()
        {
            DataModel.AddDisc(EpisodesFullTime);

            ClearEpisodes();
        }

        private Boolean HasEpisodeFullTime()
            => (String.IsNullOrEmpty(EpisodesFullTime) == false);

        private void CopyEpisodes()
        {
            OutputModel.CopyEpisodes();
        }

        private void CopyAllEpisodes()
        {
            OutputModel.CopyAllEpisodes();
        }

        #endregion

        #region Discs 

        private void OnSelectedDiscChanged()
        {
            RaisePropertyChanged(nameof(SelectedDisc));
        }

        private void OnDiscsChanged(Object sender
            , EventArgs e)
        {
            RaisePropertyChanged(nameof(Discs));
        }

        private void OnDiscsFullTimeChanged(Object sender
            , EventArgs e)
        {
            RaisePropertyChanged(nameof(DiscsFullTime));
        }

        private void OnDiscsMiddleTimeChanged(Object sender
            , EventArgs e)
        {
            RaisePropertyChanged(nameof(DiscsMiddleTime));
        }

        private void OnDiscsShortTimeChanged(Object sender
            , EventArgs e)
        {
            RaisePropertyChanged(nameof(DiscsShortTime));
        }

        private void RemoveDisc()
        {
            Int32 selectedIndex = SelectedDisc;

            DataModel.RemoveDisc(selectedIndex);

            SelectedDisc = (Discs.Count > selectedIndex) ? selectedIndex : (selectedIndex - 1);
        }

        private Boolean CanRemoveDisc()
            => (SelectedDisc != -1);

        private void ClearDiscs()
        {
            DataModel.ClearDiscs();
        }

        private Boolean CanClearDiscs()
            => (Discs.HasItems());

        private void MoveDiscs()
        {
            DataModel.AddSeason(DiscsFullTime);

            ClearDiscs();
        }

        private Boolean HasDiscFullTime()
            => (String.IsNullOrEmpty(DiscsFullTime) == false);

        private void CopyDiscs()
        {
            OutputModel.CopyDiscs();
        }

        private void CopyAllDiscs()
        {
            OutputModel.CopyAllDiscs();
        }

        private void CopyFullDiscs()
        {
            OutputModel.CopyFullDiscs();
        }

        #endregion

        #region Seasons 

        private void OnSelectedSeasonChanged()
        {
            RaisePropertyChanged(nameof(SelectedSeason));
        }

        private void OnSeasonsChanged(Object sender
            , EventArgs e)
        {
            RaisePropertyChanged(nameof(Seasons));
        }

        private void OnSeasonsFullTimeChanged(Object sender
            , EventArgs e)
        {
            RaisePropertyChanged(nameof(SeasonsFullTime));
        }

        private void OnSeasonsMiddleTimeChanged(Object sender
            , EventArgs e)
        {
            RaisePropertyChanged(nameof(SeasonsMiddleTime));
        }

        private void OnSeasonsShortTimeChanged(Object sender
            , EventArgs e)
        {
            RaisePropertyChanged(nameof(SeasonsShortTime));
        }

        private void RemoveSeason()
        {
            Int32 selectedIndex = SelectedSeason;

            DataModel.RemoveSeason(selectedIndex);

            SelectedSeason = (Seasons.Count > selectedIndex) ? selectedIndex : (selectedIndex - 1);
        }

        private Boolean CanRemoveSeason()
            => (SelectedSeason != -1);

        private void ClearSeasons()
        {
            DataModel.ClearSeasons();
        }

        private Boolean CanClearSeasons()
            => (Seasons.HasItems());

        private Boolean HasSeasonFullTime()
            => (String.IsNullOrEmpty(SeasonsFullTime) == false);

        private void CopySeasons()
        {
            OutputModel.CopySeasons();
        }

        private void CopyAllSeasons()
        {
            OutputModel.CopyAllSeasons();
        }

        private void CopyFullSeasons()
        {
            OutputModel.CopyFullSeasons();
        }

        #endregion

        private void ClearAll()
        {
            ClearEpisodes();

            ClearDiscs();

            ClearSeasons();
        }

        private Boolean CanClearAll()
            => ((CanClearEpisodes()) || (CanClearDiscs()) || (CanClearSeasons()));

        private void CheckForNewVersion()
        {
            OnlineAccess.Init("Doena Soft.", "AddingTime");
            OnlineAccess.CheckForNewVersion("http://doena-soft.de/dvdprofiler/3.9.0/versions.xml", null, "AddingTime", GetType().Assembly);
        }

        private void OpenAboutWindow()
        {
            WindowFactory.OpenAboutWindow();
        }

        private void OpenHelpWindow()
        {
            WindowFactory.OpenHelpWindow();
        }

        private void OpenReadFromDriveWindow()
        {
            try
            {
                TryOpenReadFromDriveWindow();
            }
            catch (Exception ex)
            {
                UIServices.ShowMessageBox(ex.Message, "Error", Buttons.OK, Icon.Error);
            }
        }

        private void TryOpenReadFromDriveWindow()
        {
            IEnumerable<TimeSpan> runningTimes = WindowFactory.OpenReadFromDriveWindow();

            foreach (TimeSpan runningTime in runningTimes)
            {
                Input = runningTime.ToString();

                AddEpisode();
            }

            if (HasEpisodeFullTime())
            {
                MoveEpisodes();
            }
        }
    }
}