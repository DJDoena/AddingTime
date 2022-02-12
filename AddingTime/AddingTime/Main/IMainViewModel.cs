namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;

    internal interface IMainViewModel : INotifyPropertyChanged
    {
        #region Episodes

        string Input { get; set; }

        ObservableCollection<string> Episodes { get; }

        int SelectedEpisode { get; set; }

        string EpisodesFullTime { get; }

        string EpisodesMiddleTime { get; }

        string EpisodesShortTime { get; }

        ICommand AddEpisodeCommand { get; }

        ICommand AddFromClipboardCommand { get; }

        ICommand RemoveEpisodeCommand { get; }

        ICommand ClearEpisodesCommand { get; }

        ICommand MoveEpisodesCommand { get; }

        ICommand CopyEpisodesCommand { get; }

        ICommand CopyAllEpisodesCommand { get; }

        #endregion

        #region Discs

        ObservableCollection<string> Discs { get; }

        int SelectedDisc { get; set; }

        string DiscsFullTime { get; }

        string DiscsMiddleTime { get; }

        string DiscsShortTime { get; }

        ICommand RemoveDiscCommand { get; }

        ICommand ClearDiscsCommand { get; }

        ICommand MoveDiscsCommand { get; }

        ICommand CopyDiscsCommand { get; }

        ICommand CopyAllDiscsCommand { get; }

        ICommand CopyFullDiscsCommand { get; }

        #endregion

        #region Seasons

        ObservableCollection<string> Seasons { get; }

        int SelectedSeason { get; set; }

        string SeasonsFullTime { get; }

        string SeasonsMiddleTime { get; }

        string SeasonsShortTime { get; }

        ICommand RemoveSeasonCommand { get; }

        ICommand ClearSeasonsCommand { get; }

        ICommand CopySeasonsCommand { get; }

        ICommand CopyAllSeasonsCommand { get; }

        ICommand CopyFullSeasonsCommand { get; }

        #endregion

        ICommand ClearAllCommand { get; }

        ICommand CheckForNewVersionCommand { get; }

        ICommand OpenAboutWindowCommand { get; }

        ICommand OpenHelpWindowCommand { get; }

        ICommand OpenReadFromDriveWindowCommand { get; }
    }
}