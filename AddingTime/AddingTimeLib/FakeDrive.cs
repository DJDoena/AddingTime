#if FAKE

using System;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    public sealed class FakeDrive : IDriveInfo
    {
        #region IDriveInfo

        public Boolean IsReady
            => (true);

        public String Label
            => (DriveLetter + " [Fake]");

        public String RootDirectory
            => (@"F:\");

        public String DriveLetter
            => (RootDirectory.Substring(0, 2));

        public Int64 AvailableFreeSpace
            => (0);

        #endregion
    }
}

#endif