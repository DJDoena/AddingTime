#if FAKE

namespace DoenaSoft.DVDProfiler.AddingTime
{
    using AbstractionLayer.IOServices;

    internal sealed class FakeDiscReader : IDiscReader
    {
        public IDiscInfo GetDiscInfo(IDriveInfo drive) => new FakeDiscInfo();
    }
}

#endif