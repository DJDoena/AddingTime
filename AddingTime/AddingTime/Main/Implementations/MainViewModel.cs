namespace DoenaSoft.DVDProfiler.AddingTime.Main.Implementations
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Input;
    using AbstractionLayer.UIServices;
    using DVDProfilerHelper;
    using ToolBox.Commands;
    using ToolBox.Extensions;

    internal sealed class MainViewModel : IMainViewModel
    {
        private readonly IMainDataModel _dataModel;

        private readonly IMainOutputModel _outputModel;

        private readonly IClipboardServices _clipboardServices;

        private readonly IWindowFactory _windowFactory;

        private readonly IUIServices _uiServices;

        private string _input;

        private int _selectedEpisode;

        private int _selectedDisc;

        private int _selectedSeason;

        private event PropertyChangedEventHandler _propertyChanged;

        internal MainViewModel(IMainDataModel dataModel, IMainOutputModel outputModel, IClipboardServices clipboardServices, IWindowFactory windowFactory, IUIServices uiServices)
        {
            _dataModel = dataModel ?? throw new ArgumentNullException(nameof(dataModel));

            _outputModel = outputModel ?? throw new ArgumentNullException(nameof(outputModel));

            _clipboardServices = clipboardServices ?? throw new ArgumentNullException(nameof(clipboardServices));

            _windowFactory = windowFactory ?? throw new ArgumentNullException(nameof(windowFactory));

            _uiServices = uiServices;

            this.SelectedEpisode = -1;

            this.SelectedDisc = -1;

            this.SelectedSeason = -1;
        }

        #region IViewModel

        #region Episodes

        public string Input
        {
            get => _input ?? string.Empty;
            set
            {
                _input = value;

                this.OnInputChanged();
            }
        }

        public ObservableCollection<string> Episodes => new ObservableCollection<string>(_dataModel.EpisodeRunningTimes.Select(e => MainHelper.FormatTime(e)));

        public int SelectedEpisode
        {
            get => _selectedEpisode;
            set
            {
                _selectedEpisode = value;

                this.OnSelectedEpisodeChanged();
            }
        }

        public string EpisodesFullTime => _dataModel.EpisodesFullTime ?? string.Empty;

        public string EpisodesMiddleTime => _dataModel.EpisodesMiddleTime ?? string.Empty;

        public string EpisodesShortTime => _dataModel.EpisodesShortTime ?? string.Empty;

        public ICommand AddEpisodeCommand => new RelayCommand(this.AddEpisode, this.CanAddEpisode);

        public ICommand AddFromClipboardCommand => new RelayCommand(this.AddFromClipboard, this.CanAddFromClipboard);

        public ICommand RemoveEpisodeCommand => new RelayCommand(this.RemoveEpisode, this.CanRemoveEpisode);

        public ICommand ClearEpisodesCommand => new RelayCommand(this.ClearEpisodes, this.CanClearEpisodes);

        public ICommand MoveEpisodesCommand => new RelayCommand(this.MoveEpisodes, this.HasEpisodeFullTime);

        public ICommand CopyEpisodesCommand => new RelayCommand(this.CopyEpisodes, this.HasEpisodeFullTime);

        public ICommand CopyAllEpisodesCommand => new RelayCommand(this.CopyAllEpisodes, this.HasEpisodeFullTime);

        #endregion

        #region Discs

        public ObservableCollection<string> Discs => new ObservableCollection<string>(_dataModel.DiscRunningTimes.Select(d => MainHelper.FormatTime(d)));

        public int SelectedDisc
        {
            get => _selectedDisc;
            set
            {
                _selectedDisc = value;

                this.OnSelectedDiscChanged();
            }
        }

        public string DiscsFullTime => _dataModel.DiscsFullTime ?? string.Empty;

        public string DiscsMiddleTime => _dataModel.DiscsMiddleTime ?? string.Empty;

        public string DiscsShortTime => _dataModel.DiscsShortTime ?? string.Empty;

        public ICommand RemoveDiscCommand => new RelayCommand(this.RemoveDisc, this.CanRemoveDisc);

        public ICommand ClearDiscsCommand => new RelayCommand(this.ClearDiscs, this.CanClearDiscs);

        public ICommand MoveDiscsCommand => new RelayCommand(this.MoveDiscs, this.HasDiscFullTime);

        public ICommand CopyDiscsCommand => new RelayCommand(this.CopyDiscs, this.HasDiscFullTime);

        public ICommand CopyAllDiscsCommand => new RelayCommand(this.CopyAllDiscs, this.HasDiscFullTime);

        public ICommand CopyFullDiscsCommand => new RelayCommand(this.CopyFullDiscs, this.HasDiscFullTime);

        #endregion

        #region Seasons

        public ObservableCollection<string> Seasons => new ObservableCollection<string>(_dataModel.SeasonRunningTimes.Select(s => MainHelper.FormatTime(s)));

        public int SelectedSeason
        {
            get => _selectedSeason;
            set
            {
                _selectedSeason = value;

                this.OnSelectedSeasonChanged();
            }
        }

        public string SeasonsFullTime => _dataModel.SeasonsFullTime ?? string.Empty;

        public string SeasonsMiddleTime => _dataModel.SeasonsMiddleTime ?? string.Empty;

        public string SeasonsShortTime => _dataModel.SeasonsShortTime ?? string.Empty;

        public ICommand RemoveSeasonCommand => new RelayCommand(this.RemoveSeason, this.CanRemoveSeason);

        public ICommand ClearSeasonsCommand => new RelayCommand(this.ClearSeasons, this.CanClearSeasons);

        public ICommand CopySeasonsCommand => new RelayCommand(this.CopySeasons, this.HasSeasonFullTime);

        public ICommand CopyAllSeasonsCommand => new RelayCommand(this.CopyAllSeasons, this.HasSeasonFullTime);

        public ICommand CopyFullSeasonsCommand => new RelayCommand(this.CopyFullSeasons, this.HasSeasonFullTime);

        #endregion

        public ICommand ClearAllCommand => new RelayCommand(this.ClearAll, this.CanClearAll);


        public ICommand CheckForNewVersionCommand => new RelayCommand(this.CheckForNewVersion);

        public ICommand OpenAboutWindowCommand => new RelayCommand(this.OpenAboutWindow);

        public ICommand OpenHelpWindowCommand => new RelayCommand(this.OpenHelpWindow);

        public ICommand OpenReadFromDriveWindowCommand => new RelayCommand(this.OpenReadFromDriveWindow);

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                if (_propertyChanged == null)
                {
                    this.RegisterModelEvents();
                }

                _propertyChanged += value;
            }
            remove
            {
                _propertyChanged -= value;

                if (_propertyChanged == null)
                {
                    this.UnregisterModelEvents();
                }
            }
        }

        #endregion

        private void RegisterModelEvents()
        {
            _dataModel.EpisodesChanged += this.OnEpisodesChanged;

            _dataModel.EpisodesFullTimeChanged += this.OnEpisodesFullTimeChanged;
            _dataModel.EpisodesMiddleTimeChanged += this.OnEpisodesMiddleTimeChanged;
            _dataModel.EpisodesShortTimeChanged += this.OnEpisodesShortTimeChanged;

            _dataModel.DiscsChanged += this.OnDiscsChanged;

            _dataModel.DiscsFullTimeChanged += this.OnDiscsFullTimeChanged;
            _dataModel.DiscsMiddleTimeChanged += this.OnDiscsMiddleTimeChanged;
            _dataModel.DiscsShortTimeChanged += this.OnDiscsShortTimeChanged;

            _dataModel.SeasonsFullTimeChanged += this.OnSeasonsFullTimeChanged;
            _dataModel.SeasonsMiddleTimeChanged += this.OnSeasonsMiddleTimeChanged;
            _dataModel.SeasonsShortTimeChanged += this.OnSeasonsShortTimeChanged;

            _dataModel.SeasonsChanged += this.OnSeasonsChanged;
        }

        private void UnregisterModelEvents()
        {
            _dataModel.EpisodesChanged -= this.OnEpisodesChanged;

            _dataModel.EpisodesFullTimeChanged -= this.OnEpisodesFullTimeChanged;
            _dataModel.EpisodesMiddleTimeChanged -= this.OnEpisodesMiddleTimeChanged;
            _dataModel.EpisodesShortTimeChanged -= this.OnEpisodesShortTimeChanged;

            _dataModel.DiscsChanged -= this.OnDiscsChanged;

            _dataModel.DiscsFullTimeChanged -= this.OnDiscsFullTimeChanged;
            _dataModel.DiscsMiddleTimeChanged -= this.OnDiscsMiddleTimeChanged;
            _dataModel.DiscsShortTimeChanged -= this.OnDiscsShortTimeChanged;

            _dataModel.SeasonsChanged -= this.OnSeasonsChanged;

            _dataModel.SeasonsFullTimeChanged -= this.OnSeasonsFullTimeChanged;
            _dataModel.SeasonsMiddleTimeChanged -= this.OnSeasonsMiddleTimeChanged;
            _dataModel.SeasonsShortTimeChanged -= this.OnSeasonsShortTimeChanged;
        }

        private void RaisePropertyChanged(string attribute) => _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(attribute));

        #region Episodes

        private void OnSelectedEpisodeChanged() => this.RaisePropertyChanged(nameof(this.SelectedEpisode));

        private void OnInputChanged()
        {
            this.RaisePropertyChanged(nameof(this.Input));
            this.RaisePropertyChanged(nameof(this.AddEpisodeCommand));
        }

        private void OnEpisodesChanged(object sender, EventArgs e) => this.RaisePropertyChanged(nameof(this.Episodes));

        private void OnEpisodesFullTimeChanged(object sender, EventArgs e) => this.RaisePropertyChanged(nameof(this.EpisodesFullTime));

        private void OnEpisodesMiddleTimeChanged(object sender, EventArgs e) => this.RaisePropertyChanged(nameof(this.EpisodesMiddleTime));

        private void OnEpisodesShortTimeChanged(object sender, EventArgs e) => this.RaisePropertyChanged(nameof(this.EpisodesShortTime));

        private void AddEpisode()
        {
            if (_dataModel.AddEpisode(this.Input))
            {
                this.Input = null;
            }
        }

        private bool CanAddEpisode() => !string.IsNullOrEmpty(this.Input);

        private void AddFromClipboard()
        {
            this.Input = _clipboardServices.GetText();

            this.AddEpisode();
        }

        private bool CanAddFromClipboard() => _clipboardServices.ContainsText;

        private void RemoveEpisode()
        {
            var selectedIndex = this.SelectedEpisode;

            _dataModel.RemoveEpisode(selectedIndex);

            this.SelectedEpisode = this.Episodes.Count > selectedIndex ? selectedIndex : selectedIndex - 1;
        }

        private bool CanRemoveEpisode() => this.SelectedEpisode != -1;

        private void ClearEpisodes() => _dataModel.ClearEpisodes();

        private bool CanClearEpisodes() => this.Episodes.HasItems();

        private void MoveEpisodes()
        {
            _dataModel.AddDisc(_dataModel.EpisodeRunningTimes);

            this.ClearEpisodes();
        }

        private bool HasEpisodeFullTime() => !string.IsNullOrEmpty(this.EpisodesFullTime);

        private void CopyEpisodes() => _outputModel.CopyEpisodes();

        private void CopyAllEpisodes() => _outputModel.CopyAllEpisodes();

        #endregion

        #region Discs 

        private void OnSelectedDiscChanged() => this.RaisePropertyChanged(nameof(this.SelectedDisc));

        private void OnDiscsChanged(object sender, EventArgs e) => this.RaisePropertyChanged(nameof(this.Discs));

        private void OnDiscsFullTimeChanged(object sender, EventArgs e) => this.RaisePropertyChanged(nameof(this.DiscsFullTime));

        private void OnDiscsMiddleTimeChanged(object sender, EventArgs e) => this.RaisePropertyChanged(nameof(this.DiscsMiddleTime));

        private void OnDiscsShortTimeChanged(object sender, EventArgs e) => this.RaisePropertyChanged(nameof(this.DiscsShortTime));

        private void RemoveDisc()
        {
            int selectedIndex = this.SelectedDisc;

            _dataModel.RemoveDisc(selectedIndex);

            this.SelectedDisc = this.Discs.Count > selectedIndex ? selectedIndex : selectedIndex - 1;
        }

        private bool CanRemoveDisc() => this.SelectedDisc != -1;

        private void ClearDiscs() => _dataModel.ClearDiscs();

        private bool CanClearDiscs() => this.Discs.HasItems();

        private void MoveDiscs()
        {
            _dataModel.AddSeason(_dataModel.Discs);

            this.ClearDiscs();
        }

        private bool HasDiscFullTime() => !string.IsNullOrEmpty(this.DiscsFullTime);

        private void CopyDiscs() => _outputModel.CopyDiscs();

        private void CopyAllDiscs() => _outputModel.CopyAllDiscs();

        private void CopyFullDiscs() => _outputModel.CopyFullDiscs();

        #endregion

        #region Seasons 

        private void OnSelectedSeasonChanged() => this.RaisePropertyChanged(nameof(this.SelectedSeason));

        private void OnSeasonsChanged(object sender, EventArgs e) => this.RaisePropertyChanged(nameof(this.Seasons));

        private void OnSeasonsFullTimeChanged(object sender, EventArgs e) => this.RaisePropertyChanged(nameof(this.SeasonsFullTime));

        private void OnSeasonsMiddleTimeChanged(object sender, EventArgs e) => this.RaisePropertyChanged(nameof(this.SeasonsMiddleTime));

        private void OnSeasonsShortTimeChanged(object sender, EventArgs e) => this.RaisePropertyChanged(nameof(this.SeasonsShortTime));

        private void RemoveSeason()
        {
            int selectedIndex = this.SelectedSeason;

            _dataModel.RemoveSeason(selectedIndex);

            this.SelectedSeason = this.Seasons.Count > selectedIndex ? selectedIndex : selectedIndex - 1;
        }

        private bool CanRemoveSeason() => this.SelectedSeason != -1;

        private void ClearSeasons() => _dataModel.ClearSeasons();

        private bool CanClearSeasons() => this.Seasons.HasItems();

        private bool HasSeasonFullTime() => !string.IsNullOrEmpty(this.SeasonsFullTime);

        private void CopySeasons() => _outputModel.CopySeasons();

        private void CopyAllSeasons() => _outputModel.CopyAllSeasons();

        private void CopyFullSeasons() => _outputModel.CopyFullSeasons();

        #endregion

        private void ClearAll()
        {
            this.ClearEpisodes();

            this.ClearDiscs();

            this.ClearSeasons();
        }

        private bool CanClearAll() => this.CanClearEpisodes() || this.CanClearDiscs() || this.CanClearSeasons();

        private void CheckForNewVersion()
        {
            OnlineAccess.Init("Doena Soft.", "AddingTime");
            OnlineAccess.CheckForNewVersion("http://doena-soft.de/dvdprofiler/3.9.0/versions.xml", null, "AddingTime", this.GetType().Assembly);
        }

        private void OpenAboutWindow() => _windowFactory.OpenAboutWindow();

        private void OpenHelpWindow() => _windowFactory.OpenHelpWindow();

        private void OpenReadFromDriveWindow()
        {
            try
            {
                this.TryOpenReadFromDriveWindow();
            }
            catch (Exception ex)
            {
                _uiServices.ShowMessageBox(ex.Message, "Error", Buttons.OK, Icon.Error);
            }
        }

        private void TryOpenReadFromDriveWindow()
        {
            _windowFactory.OpenReadFromDriveWindow().ForEach(this.AddEpisode);

            if (this.HasEpisodeFullTime())
            {
                this.MoveEpisodes();
            }
        }

        private void AddEpisode(TimeSpan runningTime)
        {
            this.Input = runningTime.ToString();

            this.AddEpisode();
        }
    }
}