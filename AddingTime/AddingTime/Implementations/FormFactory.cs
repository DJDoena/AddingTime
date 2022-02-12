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

    internal sealed class FormFactory : IWindowFactory
    {
        private readonly IUIServices _uiServices;

        private readonly IClipboardServices _clipboardServices;

        private readonly IIOServices _ioServices;

        public FormFactory()
        {
            _ioServices = new IOServices();

            _uiServices = new FormUIServices();

            _clipboardServices = new FormClipboardServices();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        #region IWindowFactory

        public void OpenMainWindow()
        {
            var dataModel = new MainDataModel(_uiServices);

            var outputModel = new MainOutputModel(dataModel, _uiServices, _clipboardServices);

            var viewModel = new MainViewModel(dataModel, outputModel, _clipboardServices, this, _uiServices);

            var form = new MainForm(viewModel);

            Application.Run(form);
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

            using (var form = new DiscTimeForm(viewModel))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    return viewModel.RunningTimes;
                }
            }

            return Enumerable.Empty<TimeSpan>();
        }

        #endregion
    }
}