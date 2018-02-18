namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;

    public interface ITrackInfo
    {
        #region Properties

        TimeSpan RunningTime { get; }

        #endregion
    }
}