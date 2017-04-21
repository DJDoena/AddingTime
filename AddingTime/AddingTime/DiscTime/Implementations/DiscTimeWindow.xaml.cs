using System;
using System.Windows;
using DoenaSoft.AbstractionLayer.UIServices;

namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    /// <summary>
    /// Interaction logic for DiscTimeWindow.xaml
    /// </summary>
    public partial class DiscTimeWindow : Window
    {
        public DiscTimeWindow()
        {
            InitializeComponent();
        }

        private void OnLoaded(Object sender
            , RoutedEventArgs e)
        {
            IDiscTimeViewModel viewModel = (IDiscTimeViewModel)DataContext;

            viewModel.CheckForDecrypter();

            viewModel.Closing += OnViewModelClosing;
        }

        private void OnViewModelClosing(Object sender
            , CloseEventArgs e)
        {
            IDiscTimeViewModel viewModel = (IDiscTimeViewModel)DataContext;

            viewModel.Closing -= OnViewModelClosing;

            DialogResult = (e.Result == Result.OK) ? true : false;

            Close();
        }
    }
}