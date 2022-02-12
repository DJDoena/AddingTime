#if FAKE

namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System.Collections.Generic;

    internal sealed class FakeDiscInfo : IDiscInfo
    {
        public string DiscLabel => "FakeDisc";

        public string DriveLetter => "F";

        public bool IsValid => true;

        public IEnumerable<ISubsetInfo> Subsets
        {
            get
            {
                yield return new FakeSubsetInfo();
                yield return new FakeSubsetInfo();
            }
        }
    }
}

#endif