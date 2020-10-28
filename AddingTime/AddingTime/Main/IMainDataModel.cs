namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    using System;
    using System.Collections.Generic;

    internal interface IMainDataModel
    {
        #region Episodes

        IEnumerable<String> Episodes { get; }

        event EventHandler EpisodesChanged;

        String EpisodesFullTime { get; }

        event EventHandler EpisodesFullTimeChanged;

        String EpisodesMiddleTime { get; }

        event EventHandler EpisodesMiddleTimeChanged;

        String EpisodesShortTime { get; }

        event EventHandler EpisodesShortTimeChanged;

        Boolean AddEpisode(String input);

        void RemoveEpisode(Int32 index);

        void ClearEpisodes();

        #endregion

        #region Discs

        IEnumerable<String> Discs { get; }

        IEnumerable<IEnumerable<string>> DiscEpisodes { get; }

        event EventHandler DiscsChanged;

        String DiscsFullTime { get; }

        event EventHandler DiscsFullTimeChanged;

        String DiscsMiddleTime { get; }

        event EventHandler DiscsMiddleTimeChanged;

        String DiscsShortTime { get; }

        event EventHandler DiscsShortTimeChanged;

        void AddDisc(string input);

        void AddDisc(string discInput, IEnumerable<string> episodeInputs);

        void RemoveDisc(Int32 index);

        void ClearDiscs();

        #endregion

        #region Seasons

        IEnumerable<string> Seasons { get; }

        IEnumerable<IEnumerable<string>> SeasonDiscs { get; }

        event EventHandler SeasonsChanged;

        String SeasonsFullTime { get; }

        event EventHandler SeasonsFullTimeChanged;

        String SeasonsMiddleTime { get; }

        event EventHandler SeasonsMiddleTimeChanged;

        String SeasonsShortTime { get; }

        event EventHandler SeasonsShortTimeChanged;

        void AddSeason(string input);

        void AddSeason(string seasonInput, IEnumerable<string> discInputs);

        void RemoveSeason(Int32 index);

        void ClearSeasons();

        #endregion
    }
}