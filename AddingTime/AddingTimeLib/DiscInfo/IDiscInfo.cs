using System;
using System.Collections.Generic;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    public interface IDiscInfo
    {
        #region Properties

        String DiscLabel { get; }

        Boolean IsValid { get; }

        IEnumerable<ISubsetInfo> Subsets { get; }

        #endregion
    }
}