#if FAKE

using System;
using System.Collections.Generic;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    internal sealed class FakeSubsetInfo : SubsetInfoBase
    {
        private static Int32 s_Counter = 1;

        private Int32 m_Counter = s_Counter++;

        public override Boolean IsValid
            => (true);

        public override String Name
            => ("Fake Subset " + m_Counter);

        protected override IEnumerable<ITrackInfo> GetTracks()
        {
            yield return (new FakeTrackInfo());
            yield return (new FakeTrackInfo());
        }
    }
}

#endif