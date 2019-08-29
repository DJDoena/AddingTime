namespace DoenaSoft.DVDProfiler.AddingTime.Main.Implementations
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;
    using AbstractionLayer.UIServices;
    using DVDProfilerHelper;
    using ToolBox.Commands;
    using ToolBox.Extensions;

    internal sealed class MainViewModel : IMainViewModel
    {
        private readonly IMainDataModel _DataModel;

        private readonly IMainOutputModel _OutputModel;

        private readonly IClipboardServices _ClipboardServices;

        private readonly IWindowFactory _WindowFactory;

        private readonly IUIServices _UIServices;

        private String _Input;

        private Int32 _SelectedEpisode;

        private Int32 _SelectedDisc;

        private Int32 _SelectedSeason;

        private event PropertyChangedEventHandler _PropertyChanged;

        internal MainViewModel(IMainDataModel dataModel
            , IMainOutputModel outputModel
            , IClipboardServices clipboardServices
            , IWindowFactory windowFactory
            , IUIServices uiServices)
        {
            _DataModel = dataModel ?? throw new ArgumentNullException(nameof(dataModel));

            _OutputModel = outputModel ?? throw new ArgumentNullException(nameof(outputModel));

            _ClipboardServices = clipboardServices ?? throw new ArgumentNullException(nameof(clipboardServices));

            _WindowFactory = windowFactory ?? throw new ArgumentNullException(nameof(windowFactory));

            _UIServices = uiServices;

            SelectedEpisode = -1;

            SelectedDisc = -1;

            SelectedSeason = -1;
        }

        #region IViewModel

        #region Episodes

        public String Input
        {
            get => _Input ?? String.Empty;
            set
            {
                _Input = value;

                OnInputChanged();
            }
        }

        public ObservableCollection<String> Episodes
            => new ObservableCollection<String>(_DataModel.Episodes);

        public Int32 SelectedEpisode
        {
            get => _SelectedEpisode;
            set
            {
                _SelectedEpisode = value;

                OnSelectedEpisodeChanged();
            }
        }

        public String EpisodesFullTime
            => _DataModel.EpisodesFullTime ?? String.Empty;

        public String EpisodesMiddleTime
            => _DataModel.EpisodesMiddleTime ?? String.Empty;

        public String EpisodesShortTime
            => _DataModel.EpisodesShortTime ?? String.Empty;

        public ICommand AddEpisodeCommand
            => new RelayCommand(AddEpisode, CanAddEpisode);

        public ICommand AddFromClipboardCommand
            => new RelayCommand(AddFromClipboard, CanAddFromClipboard);

        public ICommand RemoveEpisodeCommand
            => new RelayCommand(RemoveEpisode, CanRemoveEpisode);

        public ICommand ClearEpisodesCommand
            => new RelayCommand(ClearEpisodes, CanClearEpisodes);

        public ICommand MoveEpisodesCommand
            => new RelayCommand(MoveEpisodes, HasEpisodeFullTime);

        public ICommand CopyEpisodesCommand
            => new RelayCommand(CopyEpisodes, HasEpisodeFullTime);

        public ICommand CopyAllEpisodesCommand
            => new RelayCommand(CopyAllEpisodes, HasEpisodeFullTime);

        #endregion

        #region Discs

        public ObservableCollection<String> Discs
            => new ObservableCollection<String>(_DataModel.Discs);

        public Int32 SelectedDisc
        {
            get => _SelectedDisc;
            set
            {
                _SelectedDisc = value;

                OnSelectedDiscChanged();
            }
        }

        public String DiscsFullTime
          => _DataModel.DiscsFullTime ?? String.Empty;

        public String DiscsMiddleTime
            => _DataModel.DiscsMiddleTime ?? String.Empty;

        public String DiscsShortTime
            => _DataModel.DiscsShortTime ?? String.Empty;

        public ICommand RemoveDiscCommand
            => new RelayCommand(RemoveDisc, CanRemoveDisc);

        public ICommand ClearDiscsCommand
            => new RelayCommand(ClearDiscs, CanClearDiscs);

        public ICommand MoveDiscsCommand
            => new RelayCommand(MoveDiscs, HasDiscFullTime);

        public ICommand CopyDiscsCommand
            => new RelayCommand(CopyDiscs, HasDiscFullTime);

        public ICommand CopyAllDiscsCommand
            => new RelayCommand(CopyAllDiscs, HasDiscFullTime);

        public ICommand CopyFullDiscsCommand
            => new RelayCommand(CopyFullDiscs, HasDiscFullTime);

        #endregion

        #region Seasons

        public ObservableCollection<String> Seasons
            => new ObservableCollection<String>(_DataModel.Seasons);

        public Int32 SelectedSeason
        {
            get => _SelectedSeason;
            set
            {
                _SelectedSeason = value;

                OnSelectedSeasonChanged();
            }
        }

        public String SeasonsFullTime
          => _DataModel.SeasonsFullTime ?? String.Empty;

        public String SeasonsMiddleTime
            => _DataModel.SeasonsMiddleTime ?? String.Empty;

        public String SeasonsShortTime
            => _DataModel.SeasonsShortTime ?? String.Empty;

        public ICommand RemoveSeasonCommand
            => new RelayCommand(RemoveSeason, CanRemoveSeason);

        public ICommand ClearSeasonsCommand
            => new RelayCommand(ClearSeasons, CanClearSeasons);

        public ICommand CopySeasonsCommand
            => new RelayCommand(CopySeasons, HasSeasonFullTime);

        public ICommand CopyAllSeasonsCommand
            => new RelayCommand(CopyAllSeasons, HasSeasonFullTime);

        public ICommand CopyFullSeasonsCommand
            => new RelayCommand(CopyFullSeasons, HasSeasonFullTime);

        #endregion

        public ICommand ClearAllCommand
            => new RelayCommand(ClearAll, CanClearAll);


        public ICommand CheckForNewVersionCommand
            => new RelayCommand(CheckForNewVersion);

        public ICommand OpenAboutWindowCommand
            => new RelayCommand(OpenAboutWindow);

        public ICommand OpenHelpWindowCommand
            => new RelayCommand(OpenHelpWindow);

        public ICommand OpenReadFromDriveWindowCommand
          => new RelayCommand(OpenReadFromDriveWindow);

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                if (_PropertyChanged == null)
                {
                    RegisterModelEvents();
                }

                _PropertyChanged += value;
            }
            remove
            {
                _PropertyChanged -= value;

                if (_PropertyChanged == null)
                {
                    UnregisterModelEvents();
                }
            }
        }

        #endregion

        private void RegisterModelEvents()
        {
            _DataModel.EpisodesChanged += OnEpisodesChanged;

            _DataModel.EpisodesFullTimeChanged += OnEpisodesFullTimeChanged;
            _DataModel.EpisodesMiddleTimeChanged += OnEpisodesMiddleTimeChanged;
            _DataModel.EpisodesShortTimeChanged += OnEpisodesShortTimeChanged;

            _DataModel.DiscsChanged += OnDiscsChanged;

            _DataModel.DiscsFullTimeChanged += OnDiscsFullTimeChanged;
            _DataModel.DiscsMiddleTimeChanged += OnDiscsMiddleTimeChanged;
            _DataModel.DiscsShortTimeChanged += OnDiscsShortTimeChanged;

            _DataModel.SeasonsFullTimeChanged += OnSeasonsFullTimeChanged;
            _DataModel.SeasonsMiddleTimeChanged += OnSeasonsMiddleTimeChanged;
            _DataModel.SeasonsShortTimeChanged += OnSeasonsShortTimeChanged;

            _DataModel.SeasonsChanged += OnSeasonsChanged;
        }

        private void UnregisterModelEvents()
        {
            _DataModel.EpisodesChanged -= OnEpisodesChanged;

            _DataModel.EpisodesFullTimeChanged -= OnEpisodesFullTimeChanged;
            _DataModel.EpisodesMiddleTimeChanged -= OnEpisodesMiddleTimeChanged;
            _DataModel.EpisodesShortTimeChanged -= OnEpisodesShortTimeChanged;

            _DataModel.DiscsChanged -= OnDiscsChanged;

            _DataModel.DiscsFullTimeChanged -= OnDiscsFullTimeChanged;
            _DataModel.DiscsMiddleTimeChanged -= OnDiscsMiddleTimeChanged;
            _DataModel.DiscsShortTimeChanged -= OnDiscsShortTimeChanged;

            _DataModel.SeasonsChanged -= OnSeasonsChanged;

            _DataModel.SeasonsFullTimeChanged -= OnSeasonsFullTimeChanged;
            _DataModel.SeasonsMiddleTimeChanged -= OnSeasonsMiddleTimeChanged;
            _DataModel.SeasonsShortTimeChanged -= OnSeasonsShortTimeChanged;
        }

        private void RaisePropertyChanged(String attribute)
            => _PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(attribute));

        #region Episodes

        private void OnSelectedEpisodeChanged()
            => RaisePropertyChanged(nameof(SelectedEpisode));

        private void OnInputChanged()
        {
            RaisePropertyChanged(nameof(Input));
            RaisePropertyChanged(nameof(AddEpisodeCommand));
        }

        private void OnEpisodesChanged(Object sender
            , EventArgs e)
            => RaisePropertyChanged(nameof(Episodes));

        private void OnEpisodesFullTimeChanged(Object sender
            , EventArgs e)
            => RaisePropertyChanged(nameof(EpisodesFullTime));

        private void OnEpisodesMiddleTimeChanged(Object sender
            , EventArgs e)
            => RaisePropertyChanged(nameof(EpisodesMiddleTime));

        private void OnEpisodesShortTimeChanged(Object sender
            , EventArgs e)
            => RaisePropertyChanged(nameof(EpisodesShortTime));

        private void AddEpisode()
        {
            if (_DataModel.AddEpisode(Input))
            {
                Input = null;
            }
        }

        private Boolean CanAddEpisode()
            => String.IsNullOrEmpty(Input) == false;

        private void AddFromClipboard()
        {
            Input = _ClipboardServices.GetText();

            AddEpisode();
        }

        private Boolean CanAddFromClipboard()
            => (_ClipboardServices.ContainsText);

        private void RemoveEpisode()
        {
            Int32 selectedIndex = SelectedEpisode;

            _DataModel.RemoveEpisode(selectedIndex);

            SelectedEpisode = (Episodes.Count > selectedIndex) ? selectedIndex : (selectedIndex - 1);
        }

        private Boolean CanRemoveEpisode()
            => SelectedEpisode != -1;

        private void ClearEpisodes()
            => _DataModel.ClearEpisodes();

        private Boolean CanClearEpisodes()
            => Episodes.HasItems();

        private void MoveEpisodes()
        {
            _DataModel.AddDisc(EpisodesFullTime);

            ClearEpisodes();
        }

        private Boolean HasEpisodeFullTime()
            => String.IsNullOrEmpty(EpisodesFullTime) == false;

        private void CopyEpisodes()
            => _OutputModel.CopyEpisodes();

        private void CopyAllEpisodes()
            => _OutputModel.CopyAllEpisodes();

        #endregion

        #region Discs 

        private void OnSelectedDiscChanged()
            => RaisePropertyChanged(nameof(SelectedDisc));

        private void OnDiscsChanged(Object sender
            , EventArgs e)
            => RaisePropertyChanged(nameof(Discs));

        private void OnDiscsFullTimeChanged(Object sender
            , EventArgs e)
            => RaisePropertyChanged(nameof(DiscsFullTime));

        private void OnDiscsMiddleTimeChanged(Object sender
            , EventArgs e)
            => RaisePropertyChanged(nameof(DiscsMiddleTime));

        private void OnDiscsShortTimeChanged(Object sender
            , EventArgs e)
            => RaisePropertyChanged(nameof(DiscsShortTime));

        private void RemoveDisc()
        {
            Int32 selectedIndex = SelectedDisc;

            _DataModel.RemoveDisc(selectedIndex);

            SelectedDisc = (Discs.Count > selectedIndex) ? selectedIndex : (selectedIndex - 1);
        }

        private Boolean CanRemoveDisc()
            => SelectedDisc != -1;

        private void ClearDiscs()
            => _DataModel.ClearDiscs();

        private Boolean CanClearDiscs()
            => Discs.HasItems();

        private void MoveDiscs()
        {
            _DataModel.AddSeason(DiscsFullTime);

            ClearDiscs();
        }

        private Boolean HasDiscFullTime()
            => String.IsNullOrEmpty(DiscsFullTime) == false;

        private void CopyDiscs()
            => _OutputModel.CopyDiscs();

        private void CopyAllDiscs()
            => _OutputModel.CopyAllDiscs();

        private void CopyFullDiscs()
            => _OutputModel.CopyFullDiscs();

        #endregion

        #region Seasons 

        private void OnSelectedSeasonChanged()
            => RaisePropertyChanged(nameof(SelectedSeason));

        private void OnSeasonsChanged(Object sender
            , EventArgs e)
            => RaisePropertyChanged(nameof(Seasons));

        private void OnSeasonsFullTimeChanged(Object sender
            , EventArgs e)
            => RaisePropertyChanged(nameof(SeasonsFullTime));

        private void OnSeasonsMiddleTimeChanged(Object sender
            , EventArgs e)
            => RaisePropertyChanged(nameof(SeasonsMiddleTime));

        private void OnSeasonsShortTimeChanged(Object sender
            , EventArgs e)
            => RaisePropertyChanged(nameof(SeasonsShortTime));

        private void RemoveSeason()
        {
            Int32 selectedIndex = SelectedSeason;

            _DataModel.RemoveSeason(selectedIndex);

            SelectedSeason = (Seasons.Count > selectedIndex) ? selectedIndex : (selectedIndex - 1);
        }

        private Boolean CanRemoveSeason()
            => SelectedSeason != -1;

        private void ClearSeasons()
            => _DataModel.ClearSeasons();

        private Boolean CanClearSeasons()
            => Seasons.HasItems();

        private Boolean HasSeasonFullTime()
            => String.IsNullOrEmpty(SeasonsFullTime) == false;

        private void CopySeasons()
            => _OutputModel.CopySeasons();

        private void CopyAllSeasons()
            => _OutputModel.CopyAllSeasons();

        private void CopyFullSeasons()
            => _OutputModel.CopyFullSeasons();

        #endregion

        private void ClearAll()
        {
            ClearEpisodes();

            ClearDiscs();

            ClearSeasons();
        }

        private Boolean CanClearAll()
            => CanClearEpisodes() || CanClearDiscs() || CanClearSeasons();

        private void CheckForNewVersion()
        {
            OnlineAccess.Init("Doena Soft.", "AddingTime");
            OnlineAccess.CheckForNewVersion("http://doena-soft.de/dvdprofiler/3.9.0/versions.xml", null, "AddingTime", GetType().Assembly);
        }

        private void OpenAboutWindow()
            => _WindowFactory.OpenAboutWindow();

        private void OpenHelpWindow()
            => _WindowFactory.OpenHelpWindow();

        private void OpenReadFromDriveWindow()
        {
            try
            {
                TryOpenReadFromDriveWindow();
            }
            catch (Exception ex)
            {
                _UIServices.ShowMessageBox(ex.Message, "Error", Buttons.OK, Icon.Error);
            }
        }

        private void TryOpenReadFromDriveWindow()
        {
            _WindowFactory.OpenReadFromDriveWindow().ForEach(AddEpisode);

            if (HasEpisodeFullTime())
            {
                MoveEpisodes();
            }
        }

        private void AddEpisode(TimeSpan runningTime)
        {
            Input = runningTime.ToString();

            AddEpisode();
        }
    }
}