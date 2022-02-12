namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using AbstractionLayer.IOServices;

    public sealed class DvdDiscReader : IDiscReader
    {
        private readonly IIOServices _ioServices;

        public DvdDiscReader(IIOServices ioServices)
        {
            _ioServices = ioServices ?? throw new ArgumentNullException(nameof(ioServices));
        }

        #region Methods

        public IDiscInfo GetDiscInfo(IDriveInfo drive)
        {
            var path = _ioServices.Path.Combine(drive.RootFolder, "VIDEO_TS");

            var discInfo = new DvdDiscInfo(_ioServices);

            if (_ioServices.File.Exists(_ioServices.Path.Combine(path, "VIDEO_TS.IFO")))
            {
                discInfo.Init(path);
            }

            return discInfo;
        }

        #endregion
    }
}