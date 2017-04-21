using System.Collections.Generic;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    public static class DiscInfoFactory
    {
        public static IDiscInfo GetDiscInfo(IDriveInfo drive
            , IIOServices ioServices)
        {
            foreach (IDiscReader discReader in GetDiscReaders(ioServices))
            {
                IDiscInfo discInfo = discReader.GetDiscInfo(drive);

                if (discInfo.IsValid)
                {
                    return (discInfo);
                }
            }

            return (null);
        }

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