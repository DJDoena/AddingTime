using System;
using System.Collections.Generic;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    internal interface IWindowFactory
    {
        void OpenMainWindow();

        void OpenAboutWindow();

        void OpenHelpWindow();

        IEnumerable<TimeSpan> OpenReadFromDriveWindow();
    }
}
