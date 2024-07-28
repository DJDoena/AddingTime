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

        public string RootFolderName => @"F:\";

        public string DriveLetter => this.RootFolderName.Substring(0, 2);

        public ulong AvailableFreeSpace => 0;

        public IIOServices IOServices
            => throw new System.NotImplementedException();

        public ulong TotalSpace
            => throw new System.NotImplementedException();

        public IFolderInfo RootFolder
            => throw new System.NotImplementedException();

        #endregion
    }
}

#endif