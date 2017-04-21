using System.Windows;
using DoenaSoft.DVDProfiler.AddingTime.Implementations;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IWindowFactory windowFactory = new WindowFactory();

            windowFactory.OpenMainWindow();
        }
    }
}