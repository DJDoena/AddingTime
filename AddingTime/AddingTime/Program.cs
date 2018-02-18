namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using Implementations;

    public static class Program
    {
        [STAThread]
        public static void Main()
            => (new FormFactory()).OpenMainWindow();
    }
}