namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System.Collections.Generic;

    public interface IDiscInfo
    {
        #region Properties

        string DiscLabel { get; }

        bool IsValid { get; }

        IEnumerable<ISubsetInfo> Subsets { get; }

        #endregion
    }
}