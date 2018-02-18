#if FAKE

namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;

    internal sealed class FakeTrackInfo : TrackInfoBase
    {
        private static Int32 _Counter = 1;

        private readonly Int32 _Hours = _Counter++;

        public override TimeSpan RunningTime
            => new TimeSpan(_Hours, 2, 3);
    }
}

#endif