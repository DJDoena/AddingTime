#if FAKE

namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Collections.Generic;

    internal sealed class FakeDiscInfo : IDiscInfo
    {
        public String DiscLabel
            => "FakeDisc";

        public String DriveLetter
            => "F";

        public Boolean IsValid
            => true;

        public IEnumerable<ISubsetInfo> Subsets
        {
            get
            {
                yield return (new FakeSubsetInfo());
                yield return (new FakeSubsetInfo());
            }
        }
    }
}

#endif