namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    using System.Windows;
    using AbstractionLayer.UIServices;

    public partial class DiscTimeWindow : Window
    {
        public DiscTimeWindow()
        {
            this.InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (IDiscTimeViewModel)this.DataContext;

            viewModel.CheckForDecrypter();

            viewModel.Closing += this.OnViewModelClosing;
        }

        private void OnViewModelClosing(object sender, CloseEventArgs e)
        {
            var viewModel = (IDiscTimeViewModel)this.DataContext;

            viewModel.Closing -= this.OnViewModelClosing;

            this.DialogResult = (e.Result == Result.OK);

            this.Close();
        }
    }
}