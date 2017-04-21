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
    internal sealed class FormFactory : IWindowFactory
    {
        private readonly IUIServices UIServices;

        private readonly IClipboardServices ClipboardServices;

        private readonly IIOServices IOServices;

        public FormFactory()
        {
            IOServices = new IOServices();
            UIServices = new FormUIServices();
            ClipboardServices = new FormClipboardServices();            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
        }

        #region IWindowFactory

        public void OpenMainWindow()
        {
            IMainDataModel dataModel = new MainDataModel(UIServices);

            IMainOutputModel outputModel = new MainOutputModel(dataModel, ClipboardServices);

            IMainViewModel viewModel = new MainViewModel(dataModel, outputModel, ClipboardServices, this, UIServices);

            MainForm form = new MainForm(viewModel);

            Application.Run(form);
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

            using (DiscTimeForm form = new DiscTimeForm(viewModel, UIServices))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    return (viewModel.RunningTimes);
                }
            }

            return (Enumerable.Empty<TimeSpan>());
        }

        #endregion
    }
}