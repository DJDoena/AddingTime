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
    using DiscTime;
    using DiscTime.Implementations;
    using DVDProfilerHelper;
    using Main;
    using Main.Implementations;

    internal sealed class WindowFactory : IWindowFactory
    {
        private readonly IUIServices _UIServices;

        private readonly IClipboardServices _ClipboardServices;

        private readonly IIOServices _IOServices;

        public WindowFactory()
        {
            _IOServices = new IOServices();

            _UIServices = new WindowUIServices();

            _ClipboardServices = new WindowClipboardServices();

            //For the old forms
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        #region IWindowFactory

        public void OpenMainWindow()
        {
            IMainDataModel dataModel = new MainDataModel(_UIServices);

            IMainOutputModel outputModel = new MainOutputModel(dataModel, _UIServices, _ClipboardServices);

            IMainViewModel viewModel = new MainViewModel(dataModel, outputModel, _ClipboardServices, this, _UIServices);

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
            IDiscTimeDataModel dataModel = new DiscTimeDataModel(_IOServices, _UIServices);

            IDiscTimeViewModel viewModel = new DiscTimeViewModel(dataModel, _IOServices, _UIServices);

            DiscTimeWindow window = new DiscTimeWindow()
            {
                DataContext = viewModel
            };

            if (window.ShowDialog() == true)
            {
                return (viewModel.RunningTimes);
            }

            return (Enumerable.Empty<TimeSpan>());
        }

        #endregion
    }
}