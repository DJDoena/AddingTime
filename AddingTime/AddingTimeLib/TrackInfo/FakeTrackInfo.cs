#if FAKE

namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;

    internal sealed class FakeTrackInfo : TrackInfoBase
    {
        private static int _id = 1;

        private readonly int _hours = _id++;

        public override TimeSpan RunningTime => new TimeSpan(_hours, 2, 3);
    }
}

#endif