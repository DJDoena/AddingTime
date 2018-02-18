#if FAKE

namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using AbstractionLayer.IOServices;

    public sealed class FakeDrive : IDriveInfo
    {
        #region IDriveInfo

        public Boolean IsReady
            => true;

        public String Label
            => DriveLetter + " [Fake]";

        public String RootFolder
            => @"F:\";

        public String DriveLetter
            => RootFolder.Substring(0, 2);

        public UInt64 AvailableFreeSpace
            => 0;

        #endregion
    }
}

#endif