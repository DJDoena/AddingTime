namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System.Collections.Generic;
    using AbstractionLayer.IOServices;
    using System.Linq;

    public static class DiscInfoFactory
    {
        public static IDiscInfo GetDiscInfo(IDriveInfo drive
            , IIOServices ioServices)
            => GetDiscReaders(ioServices).Select(discReader => discReader.GetDiscInfo(drive)).FirstOrDefault(discInfo => discInfo.IsValid);

        private static IEnumerable<IDiscReader> GetDiscReaders(IIOServices ioServices)
        {
            yield return (new DvdDiscReader(ioServices));
            yield return (new BluRayDiscReader(ioServices));

#if FAKE

            yield return (new FakeDiscReader());

#endif
        }
    }
}