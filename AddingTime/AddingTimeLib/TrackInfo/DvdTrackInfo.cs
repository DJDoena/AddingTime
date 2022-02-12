namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using DvdNavigatorCrm;

    internal sealed class DvdTrackInfo : TrackInfoBase
    {
        #region Readonlies

        private readonly ProgramGroupChain _chain;

        #endregion

        #region Constructor

        public DvdTrackInfo(ProgramGroupChain chain)
        {
            _chain = chain;
        }

        #endregion

        #region ITrackInfo

        #region Properties

        public override TimeSpan RunningTime => new TimeSpan(0, 0, (int)(_chain.PlaybackTime));

        #endregion

        #endregion
    }
}