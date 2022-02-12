namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using BDInfoLib.BDROM;

    internal sealed class BluRayTrackInfo : TrackInfoBase
    {
        #region Readonlies

        private readonly TSStreamClip _clip;

        #endregion

        #region Constructor

        public BluRayTrackInfo(TSStreamClip clip)
        {
            _clip = clip;
        }

        #endregion

        #region ITrackInfo

        #region Properties

        public override TimeSpan RunningTime
        {
            get
            {
                var ticks = (long)(_clip.Length * Math.Pow(10, 7));

                var time = new TimeSpan(ticks);

                time = new TimeSpan(time.Hours, time.Minutes, time.Seconds);

                return time;
            }
        }

        #endregion

        #endregion
    }
}