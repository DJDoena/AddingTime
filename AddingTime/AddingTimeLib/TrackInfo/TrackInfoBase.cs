using System;
using System.Diagnostics;
using DvdNavigatorCrm;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    [DebuggerDisplay("{RunningTime} {GetType()}")]
    internal abstract class TrackInfoBase : ITrackInfo
    {
        #region ITrackInfo

        #region Properties

        public abstract TimeSpan RunningTime { get; }

        #endregion

        #endregion
    }
}