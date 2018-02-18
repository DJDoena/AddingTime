namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using AbstractionLayer.IOServices;

    public sealed class DvdDiscReader : IDiscReader
    {
        private readonly IIOServices _IOServices;

        public DvdDiscReader(IIOServices ioServices)
            => _IOServices = ioServices ?? throw new ArgumentNullException(nameof(ioServices));

        #region Methods

        public IDiscInfo GetDiscInfo(IDriveInfo drive)
        {
            String path = _IOServices.Path.Combine(drive.RootFolder, "VIDEO_TS");

            DiscInfoBase discInfo = new DvdDiscInfo(_IOServices);

            if (_IOServices.File.Exists(_IOServices.Path.Combine(path, "VIDEO_TS.IFO")))
            {
                discInfo.Init(path);
            }

            return (discInfo);
        }

        #endregion
    }
}