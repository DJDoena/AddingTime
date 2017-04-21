using System;
using System.Collections.Generic;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    public interface ISubsetInfo : IComparable<ISubsetInfo>
    {
        #region Properties

        String Name { get; }

        Boolean IsValid { get; }

        IEnumerable<ITrackInfo> Tracks { get; }

        #endregion
    }
}
