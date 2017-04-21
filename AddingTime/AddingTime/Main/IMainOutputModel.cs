namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    internal interface IMainOutputModel
    {
        #region Episodes

        void CopyEpisodes();

        void CopyAllEpisodes();

        #endregion

        #region Discs

        void CopyDiscs();

        void CopyAllDiscs();

        void CopyFullDiscs();

        #endregion

        #region Seasons

        void CopySeasons();

        void CopyAllSeasons();

        void CopyFullSeasons();

        #endregion
    }
}