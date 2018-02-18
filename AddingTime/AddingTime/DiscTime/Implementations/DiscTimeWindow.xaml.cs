namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    using System;
    using System.Windows;
    using AbstractionLayer.UIServices;

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