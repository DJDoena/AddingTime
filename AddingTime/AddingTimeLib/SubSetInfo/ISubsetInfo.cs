namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Collections.Generic;

    public interface ISubsetInfo : IComparable<ISubsetInfo>
    {
        #region Properties

        string Name { get; }

        bool IsValid { get; }

        IEnumerable<ITrackInfo> Tracks { get; }

        #endregion
    }
}