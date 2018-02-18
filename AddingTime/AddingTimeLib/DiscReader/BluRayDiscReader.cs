namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using AbstractionLayer.IOServices;

    public sealed class BluRayDiscReader : IDiscReader
    {
        private readonly IIOServices _IOServices;

        public BluRayDiscReader(IIOServices ioServices)
            => _IOServices = ioServices ?? throw new ArgumentNullException(nameof(ioServices));

        #region Methods

        public IDiscInfo GetDiscInfo(IDriveInfo drive)
        {
            String path = _IOServices.Path.Combine(drive.RootFolder, "BDMV");

            DiscInfoBase discInfo = new BluRayDiscInfo(_IOServices);

            if (_IOServices.File.Exists(_IOServices.Path.Combine(path, "INDEX.BDMV")))
            {
                discInfo.Init(path);
            }

            return (discInfo);
        }

        #endregion
    }
}