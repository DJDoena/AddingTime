namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    [DebuggerDisplay("{Name} {GetType()}")]
    internal abstract class SubsetInfoBase : ISubsetInfo
    {
        #region ISubsetInfo

        #region Properties

        public abstract string Name { get; }

        public abstract bool IsValid { get; }

        public IEnumerable<ITrackInfo> Tracks
        {
            get => this.IsValid ? this.GetTracks().ToList() : Enumerable.Empty<ITrackInfo>();
        }

        #endregion

        #endregion

        #region IComparable<ISubsetInfo>

        #region Methods

        public int CompareTo(ISubsetInfo other) => other != null ? this.Name.CompareTo(other.Name) : 1;

        #endregion

        #endregion

        #region Methods

        protected abstract IEnumerable<ITrackInfo> GetTracks();

        #endregion
    }
}