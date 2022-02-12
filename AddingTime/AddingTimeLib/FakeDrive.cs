#if FAKE

namespace DoenaSoft.DVDProfiler.AddingTime
{
    using AbstractionLayer.IOServices;

    public sealed class FakeDrive : IDriveInfo
    {
        #region IDriveInfo

        public bool IsReady => true;

        public string DriveLabel => $"{this.DriveLetter} {this.VolumeLabel}";

        public string VolumeLabel => "[Fake]";

        public string RootFolder => @"F:\";

        public string DriveLetter => this.RootFolder.Substring(0, 2);

        public ulong AvailableFreeSpace => 0;

        #endregion
    }
}

#endif