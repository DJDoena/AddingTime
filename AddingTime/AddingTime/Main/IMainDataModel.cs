namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    using System;
    using System.Collections.Generic;

    internal interface IMainDataModel
    {
        #region Episodes

        IEnumerable<string> Episodes { get; }

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

        IEnumerable<string> Discs { get; }

        IEnumerable<DiscEpisodes> DiscEpisodes { get; }

        event EventHandler DiscsChanged;

        string DiscsFullTime { get; }

        event EventHandler DiscsFullTimeChanged;

        string DiscsMiddleTime { get; }

        event EventHandler DiscsMiddleTimeChanged;

        string DiscsShortTime { get; }

        event EventHandler DiscsShortTimeChanged;

        void AddDisc(string input);

        void AddDisc(string discInput, IEnumerable<string> episodeInputs);

        void RemoveDisc(int index);

        void ClearDiscs();

        #endregion

        #region Seasons

        IEnumerable<string> Seasons { get; }

        IEnumerable<SeasonDiscEpisodes> SeasonDiscs { get; }

        event EventHandler SeasonsChanged;

        string SeasonsFullTime { get; }

        event EventHandler SeasonsFullTimeChanged;

        string SeasonsMiddleTime { get; }

        event EventHandler SeasonsMiddleTimeChanged;

        string SeasonsShortTime { get; }

        event EventHandler SeasonsShortTimeChanged;

        void AddSeason(string input);

        void AddSeason(string seasonInput, IEnumerable<DiscEpisodes> discInputs);

        void RemoveSeason(int index);

        void ClearSeasons();

        #endregion
    }
}