using System;
using System.Collections.Generic;
using BDInfoLib.BDROM;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    internal sealed class BluRaySubsetInfo : SubsetInfoBase
    {
        #region Constants

        private readonly TSPlaylistFile PlayList;

        #endregion

        #region Constructor

        public BluRaySubsetInfo(TSPlaylistFile playList)
        {
            PlayList = playList;
        }

        #endregion

        #region ISubsetInfo

        #region Properties

        public override String Name
        {
            get
            {
                String name = PlayList.Name;

                foreach (TSStreamClip clip in PlayList.StreamClips)
                {
                    name = clip.Name;

                    break;
                }

                return (name);
            }
        }

        public override Boolean IsValid
            => (PlayList.IsValid);

        #endregion

        #endregion

        #region Methods

        protected override IEnumerable<ITrackInfo> GetTracks()
        {
            foreach (TSStreamClip clip in PlayList.StreamClips)
            {
                ITrackInfo trackInfo = new BluRayTrackInfo(clip);

                yield return (trackInfo);
            }
        }

        #endregion
    }
}