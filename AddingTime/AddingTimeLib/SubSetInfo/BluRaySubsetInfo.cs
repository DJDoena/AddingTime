namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BDInfoLib.BDROM;

    internal sealed class BluRaySubsetInfo : SubsetInfoBase
    {
        #region Readonlies

        private readonly TSPlaylistFile _PlayList;

        #endregion

        #region Constructor

        public BluRaySubsetInfo(TSPlaylistFile playList)
            => _PlayList = playList;

        #endregion

        #region ISubsetInfo

        #region Properties

        public override String Name
            => _PlayList.StreamClips.FirstOrDefault()?.Name ?? _PlayList.Name;

        public override Boolean IsValid
            => _PlayList.IsValid;

        #endregion

        #endregion

        #region Methods

        protected override IEnumerable<ITrackInfo> GetTracks()
            => _PlayList.StreamClips.Select(clip => new BluRayTrackInfo(clip));

        #endregion
    }
}