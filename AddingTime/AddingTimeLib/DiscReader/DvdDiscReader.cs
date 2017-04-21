using System;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    public sealed class DvdDiscReader : IDiscReader
    {
        private readonly IIOServices IOServices;

        public DvdDiscReader(IIOServices ioServices)
        {
            if (ioServices == null)
            {
                throw (new ArgumentNullException(nameof(ioServices)));
            }

            IOServices = ioServices;
        }

        #region Methods

        public IDiscInfo GetDiscInfo(IDriveInfo drive)
        {
            DiscInfoBase discInfo;

            String path = IOServices.Path.Combine(drive.RootDirectory, "VIDEO_TS");

            discInfo = new DvdDiscInfo(IOServices);

            if (IOServices.File.Exists(IOServices.Path.Combine(path, "VIDEO_TS.IFO")))
            {
                discInfo.Init(path);
            }

            return (discInfo);
        }

        #endregion
    }
}