namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using BDInfoLib.BDROM;

    internal sealed class BluRayTrackInfo : TrackInfoBase
    {
        #region Readonlies

        private readonly TSStreamClip _Clip;

        #endregion

        #region Constructor

        public BluRayTrackInfo(TSStreamClip clip)
            => _Clip = clip;

        #endregion

        #region ITrackInfo

        #region Properties

        public override TimeSpan RunningTime
        {
            get
            {
                Int64 ticks = (Int64)(_Clip.Length * Math.Pow(10, 7));

                TimeSpan time = new TimeSpan(ticks);

                time = new TimeSpan(time.Hours, time.Minutes, time.Seconds);

                return (time);
            }
        }

        #endregion

        #endregion
    }
}