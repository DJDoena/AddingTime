#if FAKE

using System;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    internal sealed class FakeTrackInfo : TrackInfoBase
    {
        private static Int32 s_Counter = 1;

        private Int32 m_Counter = s_Counter++;

        public override TimeSpan RunningTime
            => (new TimeSpan(m_Counter, 2, 3));
    }
}

#endif