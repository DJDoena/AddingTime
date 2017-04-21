using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.AbstractionLayer.IOServices.Implementations;
using DoenaSoft.AbstractionLayer.UIServices;
using DoenaSoft.AbstractionLayer.UIServices.Implementations;
using DoenaSoft.DVDProfiler.AddingTime.DiscTime;
using DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations;
using DoenaSoft.DVDProfiler.AddingTime.Main;
using DoenaSoft.DVDProfiler.AddingTime.Main.Implementations;
using DoenaSoft.DVDProfiler.DVDProfilerHelper;

namespace DoenaSoft.DVDProfiler.AddingTime.Implementations
{
    internal sealed class WindowFactory : IWindowFactory
    {
        private readonly IUIServices UIServices;

        private readonly IClipboardServices ClipboardServices;

        private readonly IIOServices IOServices;

        public WindowFactory()
        {
            IOServices = new IOServices();
            UIServices = new WindowUIServices();
            ClipboardServices = new WindowClipboardServices();            

            //For the old forms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        #region IWindowFactory

        public void OpenMainWindow()
        {
            IMainDataModel dataModel = new MainDataModel(UIServices);

            IMainOutputModel outputModel = new MainOutputModel(dataModel, ClipboardServices);

            IMainViewModel viewModel = new MainViewModel(dataModel, outputModel, ClipboardServices, this, UIServices);

            MainWindow window = new MainWindow();

            window.DataContext = viewModel;

            window.Show();
        }

        public void OpenAboutWindow()
        {
            using (AboutBox form = new AboutBox(GetType().Assembly))
            {
                form.ShowDialog();
            }
        }

        public void OpenHelpWindow()
        {
            using (HelpForm form = new HelpForm())
            {
                form.ShowDialog();
            }
        }

        public IEnumerable<TimeSpan> OpenReadFromDriveWindow()
        {
            IDiscTimeDataModel dataModel = new DiscTimeDataModel(IOServices, UIServices);

            IDiscTimeViewModel viewModel = new DiscTimeViewModel(dataModel, IOServices, UIServices);

            DiscTimeWindow window = new DiscTimeWindow();

            window.DataContext = viewModel;

            if (window.ShowDialog() == true)
            {
                return (viewModel.RunningTimes);
            }

            return (Enumerable.Empty<TimeSpan>());
        }

        #endregion
    }
}