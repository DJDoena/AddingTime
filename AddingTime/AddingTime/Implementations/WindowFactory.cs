namespace DoenaSoft.DVDProfiler.AddingTime.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using AbstractionLayer.IOServices;
    using AbstractionLayer.IOServices.Implementations;
    using AbstractionLayer.UIServices;
    using AbstractionLayer.UIServices.Implementations;
    using DiscTime.Implementations;
    using DVDProfilerHelper;
    using Main.Implementations;

    internal sealed class WindowFactory : IWindowFactory
    {
        private readonly IUIServices _uiServices;

        private readonly IClipboardServices _clipboardServices;

        private readonly IIOServices _ioServices;

        public WindowFactory()
        {
            _ioServices = new IOServices();

            _uiServices = new WindowUIServices();

            _clipboardServices = new WindowClipboardServices();

            //For the old forms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        #region IWindowFactory

        public void OpenMainWindow()
        {
            var dataModel = new MainDataModel(_uiServices);

            var outputModel = new MainOutputModel(dataModel, _uiServices, _clipboardServices);

            var viewModel = new MainViewModel(dataModel, outputModel, _clipboardServices, this, _uiServices);

            var window = new MainWindow
            {
                DataContext = viewModel,
            };

            window.Show();
        }

        public void OpenAboutWindow()
        {
            using (var form = new AboutBox(this.GetType().Assembly))
            {
                form.ShowDialog();
            }
        }

        public void OpenHelpWindow()
        {
            using (var form = new HelpForm())
            {
                form.ShowDialog();
            }
        }

        public IEnumerable<TimeSpan> OpenReadFromDriveWindow()
        {
            var dataModel = new DiscTimeDataModel(_ioServices, _uiServices);

            var viewModel = new DiscTimeViewModel(dataModel, _ioServices, _uiServices);

            var window = new DiscTimeWindow()
            {
                DataContext = viewModel,
            };

            if (window.ShowDialog() == true)
            {
                return viewModel.RunningTimes;
            }

            return Enumerable.Empty<TimeSpan>();
        }

        #endregion
    }
}