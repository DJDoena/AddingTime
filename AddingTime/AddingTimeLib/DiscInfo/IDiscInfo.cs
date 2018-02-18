namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Collections.Generic;

    public interface IDiscInfo
    {
        #region Properties

        String DiscLabel { get; }

        Boolean IsValid { get; }

        IEnumerable<ISubsetInfo> Subsets { get; }

        #endregion
    }
}