using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    internal interface IMainViewModel : INotifyPropertyChanged
    {
        #region Episodes

        String Input { get; set; }

        ObservableCollection<String> Episodes { get; }

        Int32 SelectedEpisode { get; set; }

        String EpisodesFullTime { get; }

        String EpisodesMiddleTime { get; }

        String EpisodesShortTime { get; }

        ICommand AddEpisodeCommand { get; }

        ICommand AddFromClipboardCommand { get; }

        ICommand RemoveEpisodeCommand { get; }

        ICommand ClearEpisodesCommand { get; }

        ICommand MoveEpisodesCommand { get; }

        ICommand CopyEpisodesCommand { get; }

        ICommand CopyAllEpisodesCommand { get; }

        #endregion

        #region Discs

        ObservableCollection<String> Discs { get; }

        Int32 SelectedDisc { get; set; }

        String DiscsFullTime { get; }

        String DiscsMiddleTime { get; }

        String DiscsShortTime { get; }

        ICommand RemoveDiscCommand { get; }

        ICommand ClearDiscsCommand { get; }

        ICommand MoveDiscsCommand { get; }

        ICommand CopyDiscsCommand { get; }

        ICommand CopyAllDiscsCommand { get; }

        ICommand CopyFullDiscsCommand { get; }

        #endregion

        #region Seasons

        ObservableCollection<String> Seasons { get; }

        Int32 SelectedSeason { get; set; }

        String SeasonsFullTime { get; }

        String SeasonsMiddleTime { get; }

        String SeasonsShortTime { get; }

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