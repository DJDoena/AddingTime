namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System.Windows;
    using Implementations;

    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e) => (new WindowFactory()).OpenMainWindow();
    }
}