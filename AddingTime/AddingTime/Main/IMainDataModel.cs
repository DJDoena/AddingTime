using System;
using System.Collections.Generic;

namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
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

        event EventHandler DiscsChanged;

        String DiscsFullTime { get; }

        event EventHandler DiscsFullTimeChanged;

        String DiscsMiddleTime { get; }

        event EventHandler DiscsMiddleTimeChanged;

        String DiscsShortTime { get; }

        event EventHandler DiscsShortTimeChanged;

        void AddDisc(String input);

        void RemoveDisc(Int32 index);

        void ClearDiscs();

        #endregion

        #region Seasons

        IEnumerable<String> Seasons { get; }

        event EventHandler SeasonsChanged;

        String SeasonsFullTime { get; }

        event EventHandler SeasonsFullTimeChanged;

        String SeasonsMiddleTime { get; }

        event EventHandler SeasonsMiddleTimeChanged;

        String SeasonsShortTime { get; }

        event EventHandler SeasonsShortTimeChanged;

        void AddSeason(String input);

        void RemoveSeason(Int32 index);

        void ClearSeasons();

        #endregion
    }
}