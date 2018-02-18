#if FAKE

namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Collections.Generic;

    internal sealed class FakeSubsetInfo : SubsetInfoBase
    {
        private static Int32 _Counter = 1;

        private Int32 m_Counter = _Counter++;

        public override Boolean IsValid
            => true;

        public override String Name
            => "Fake Subset " + m_Counter;

        protected override IEnumerable<ITrackInfo> GetTracks()
        {
            yield return (new FakeTrackInfo());
            yield return (new FakeTrackInfo());
        }
    }
}

#endif