using System;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    public sealed class BluRayDiscReader : IDiscReader
    {
        private readonly IIOServices IOServices;

        public BluRayDiscReader(IIOServices ioServices)
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
            String path = IOServices.Path.Combine(drive.RootDirectory, "BDMV");

            DiscInfoBase discInfo = new BluRayDiscInfo(IOServices);

            if (IOServices.File.Exists(IOServices.Path.Combine(path, "INDEX.BDMV")))
            {
                discInfo.Init(path);
            }

            return (discInfo);
        }

        #endregion
    }
}