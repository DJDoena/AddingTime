namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Collections.Generic;

    public interface ISubsetInfo : IComparable<ISubsetInfo>
    {
        #region Properties

        String Name { get; }

        Boolean IsValid { get; }

        IEnumerable<ITrackInfo> Tracks { get; }

        #endregion
    }
}