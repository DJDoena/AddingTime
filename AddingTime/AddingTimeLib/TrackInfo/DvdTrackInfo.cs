using System;
using System.Diagnostics;
using DvdNavigatorCrm;

namespace DoenaSoft.DVDProfiler.AddingTime
{
   internal sealed class DvdTrackInfo : TrackInfoBase
    {
        #region Constants

        private readonly ProgramGroupChain Chain;

        #endregion

        #region Constructor

        public DvdTrackInfo(ProgramGroupChain chain)
        {
            Chain = chain;
        }

        #endregion

        #region ITrackInfo

        #region Properties

        public override TimeSpan RunningTime
        {
            get
            {
                Int32 seconds = (Int32)(Chain.PlaybackTime);

                TimeSpan time = new TimeSpan(0, 0, seconds);

                return (time);
            }
        }

        #endregion

        #endregion
    }
}