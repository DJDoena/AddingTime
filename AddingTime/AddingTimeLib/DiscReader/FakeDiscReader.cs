#if FAKE

using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    internal sealed class FakeDiscReader : IDiscReader
    {
        public IDiscInfo GetDiscInfo(IDriveInfo drive)
            => (new FakeDiscInfo());
    }
}

#endif