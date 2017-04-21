using System;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    public interface ITrackInfo
    {
        #region Properties

        TimeSpan RunningTime { get; }

        #endregion
    }
}