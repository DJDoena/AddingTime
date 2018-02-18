namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using DvdNavigatorCrm;

    internal sealed class DvdTrackInfo : TrackInfoBase
    {
        #region Readonlies

        private readonly ProgramGroupChain _Chain;

        #endregion

        #region Constructor

        public DvdTrackInfo(ProgramGroupChain chain)
            => _Chain = chain;

        #endregion

        #region ITrackInfo

        #region Properties

        public override TimeSpan RunningTime
            => new TimeSpan(0, 0, (Int32)(_Chain.PlaybackTime));

        #endregion

        #endregion
    }
}