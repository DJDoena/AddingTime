namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System.Collections.Generic;
    using System.Linq;
    using BDInfoLib.BDROM;

    internal sealed class BluRaySubsetInfo : SubsetInfoBase
    {
        #region Readonlies

        private readonly TSPlaylistFile _playList;

        #endregion

        #region Constructor

        public BluRaySubsetInfo(TSPlaylistFile playList)
        {
            _playList = playList;
        }

        #endregion

        #region ISubsetInfo

        #region Properties

        public override string Name => _playList.StreamClips.FirstOrDefault()?.Name ?? _playList.Name;

        public override bool IsValid => _playList.IsValid;

        #endregion

        #endregion

        #region Methods

        protected override IEnumerable<ITrackInfo> GetTracks() => _playList.StreamClips.Select(clip => new BluRayTrackInfo(clip));

        #endregion
    }
}