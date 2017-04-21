using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    [DebuggerDisplay("{Name} {GetType()}")]
    internal abstract class SubsetInfoBase : ISubsetInfo
    {
        #region ISubsetInfo

        #region Properties

        public abstract String Name { get; }

        public abstract Boolean IsValid { get; }
        
        public IEnumerable<ITrackInfo> Tracks
        {
            get
            {
                IEnumerable<ITrackInfo> tracks = Enumerable.Empty<ITrackInfo>();

                if (IsValid)
                {
                    tracks = GetTracks();
                }

                tracks = tracks.ToList();

                return (tracks);
            }
        }

        #endregion

        #endregion

        #region IComparable<ISubsetInfo>

        #region Methods

        public Int32 CompareTo(ISubsetInfo other)
            => ((other != null) ? (Name.CompareTo(other.Name)) : 1);

        #endregion

        #endregion

        #region Methods

        protected abstract IEnumerable<ITrackInfo> GetTracks();

        #endregion
    }
}