namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    using System;
    using System.Collections.Generic;

    internal interface IMainDataModel
    {
        #region Episodes

        IEnumerable<int> EpisodeRunningTimes { get; }

        event EventHandler EpisodesChanged;

        string EpisodesFullTime { get; }

        event EventHandler EpisodesFullTimeChanged;

        string EpisodesMiddleTime { get; }

        event EventHandler EpisodesMiddleTimeChanged;

        string EpisodesShortTime { get; }

        event EventHandler EpisodesShortTimeChanged;

        bool AddEpisode(string input);

        void RemoveEpisode(int index);

        void ClearEpisodes();

        #endregion

        #region Discs

        IEnumerable<int> DiscRunningTimes { get; }

        IEnumerable<DiscRunningTime> Discs { get; }

        event EventHandler DiscsChanged;

        string DiscsFullTime { get; }

        event EventHandler DiscsFullTimeChanged;

        string DiscsMiddleTime { get; }

        event EventHandler DiscsMiddleTimeChanged;

        string DiscsShortTime { get; }

        event EventHandler DiscsShortTimeChanged;

        void AddDisc(IEnumerable<int> episodeInputs);

        void RemoveDisc(int index);

        void ClearDiscs();

        #endregion

        #region Seasons

        IEnumerable<int> SeasonRunningTimes { get; }

        IEnumerable<SeasonRunningTime> Seasons { get; }

        event EventHandler SeasonsChanged;

        string SeasonsFullTime { get; }

        event EventHandler SeasonsFullTimeChanged;

        string SeasonsMiddleTime { get; }

        event EventHandler SeasonsMiddleTimeChanged;

        string SeasonsShortTime { get; }

        event EventHandler SeasonsShortTimeChanged;

        void AddSeason(IEnumerable<DiscRunningTime> discInputs);

        void RemoveSeason(int index);

        void ClearSeasons();

        #endregion
    }
}