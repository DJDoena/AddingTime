using System;
using DoenaSoft.DVDProfiler.AddingTime.Implementations;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            IWindowFactory windowFactory = new FormFactory();

            windowFactory.OpenMainWindow();
        }
    }
}