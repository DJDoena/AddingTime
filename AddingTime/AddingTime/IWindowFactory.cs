namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Collections.Generic;

    internal interface IWindowFactory
    {
        void OpenMainWindow();

        void OpenAboutWindow();

        void OpenHelpWindow();

        IEnumerable<TimeSpan> OpenReadFromDriveWindow();
    }
}