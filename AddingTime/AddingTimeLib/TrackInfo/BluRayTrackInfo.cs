using System;
using BDInfoLib.BDROM;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    internal sealed class BluRayTrackInfo : TrackInfoBase
    {
        #region Constants

        private readonly TSStreamClip Clip;

        #endregion

        #region Constructor

        public BluRayTrackInfo(TSStreamClip clip)
        {
            Clip = clip;
        }

        #endregion

        #region ITrackInfo

        #region Properties

        public override TimeSpan RunningTime
        {
            get
            {
                Int64 ticks = (Int64)(Clip.Length * Math.Pow(10, 7));

                TimeSpan time = new TimeSpan(ticks);

                time = new TimeSpan(time.Hours, time.Minutes, time.Seconds);

                return (time);
            }
        }

        #endregion

        #endregion
    }
}