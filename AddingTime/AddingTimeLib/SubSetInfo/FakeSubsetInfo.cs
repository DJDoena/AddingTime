#if FAKE

namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System.Collections.Generic;

    internal sealed class FakeSubsetInfo : SubsetInfoBase
    {
        private static int _id = 1;

        private readonly int _subsetNumber = _id++;

        public override bool IsValid => true;

        public override string Name => "Fake Subset " + _subsetNumber;

        protected override IEnumerable<ITrackInfo> GetTracks()
        {
            yield return new FakeTrackInfo();
            yield return new FakeTrackInfo();
        }
    }
}

#endif