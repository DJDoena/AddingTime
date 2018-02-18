namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Diagnostics;

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