namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using AbstractionLayer.IOServices;

    public sealed class BluRayDiscReader : IDiscReader
    {
        private readonly IIOServices _ioServices;

        public BluRayDiscReader(IIOServices ioServices)
        {
            _ioServices = ioServices ?? throw new ArgumentNullException(nameof(ioServices));
        }

        #region Methods

        public IDiscInfo GetDiscInfo(IDriveInfo drive)
        {
            var path = _ioServices.Path.Combine(drive.RootFolderName, "BDMV");

            var discInfo = new BluRayDiscInfo(_ioServices);

            if (_ioServices.File.Exists(_ioServices.Path.Combine(path, "INDEX.BDMV")))
            {
                discInfo.Init(path);
            }

            return discInfo;
        }

        #endregion
    }
}