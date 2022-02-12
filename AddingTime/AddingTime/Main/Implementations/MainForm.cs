namespace DoenaSoft.DVDProfiler.AddingTime.Main.Implementations
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using System.Windows.Input;

    internal partial class MainForm : Form
    {
        private readonly IMainViewModel _viewModel;

        public MainForm(IMainViewModel viewModel)
        {
            _viewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            this.InitializeComponent();

            Load += this.OnFormLoad;

            _viewModel.PropertyChanged += this.OnViewModelChanged;

            this.RegisterControlEvents();

            FormClosed += this.OnFormClosed;

            this.Icon = Properties.Resource.djdsoft;
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            FormClosed -= this.OnFormClosed;

            _viewModel.PropertyChanged -= this.OnViewModelChanged;

            this.UnregisterControlEvents();

            Load -= this.OnFormLoad;
        }

        private void RegisterControlEvents()
        {
            ReadFromDriveToolStripMenuItem.Click += this.OnReadFromDriveToolStripMenuItemClick;
            ReadMeToolStripMenuItem.Click += this.OnReadMeToolStripMenuItemClick;
            CheckForUpdateToolStripMenuItem.Click += this.OnCheckForUpdateToolStripMenuItemClick;
            AboutToolStripMenuItem.Click += this.OnAboutToolStripMenuItemClick;

            InputTextBox.TextChanged += this.OnInputTextBoxTextChanged;

            EpisodesListBox.SelectedIndexChanged += this.OnEpisodeListBoxSelectedIndexChanged;

            AddButton.Click += this.OnAddButtonClick;
            AddFromClipboardButton.Click += this.OnAddFromClipboardButtonClick;

            RemoveEpisodeButton.Click += this.OnRemoveEpisodeButtonClick;
            ClearEpisodesButton.Click += this.OnClearButtonClick;

            MoveEpisodeButton.Click += this.OnMoveEpisodeButtonClick;

            CopyEpisodesButton.Click += this.OnCopyButtonClick;
            CopyAllEpisodesButton.Click += this.OnCopyAllButtonClick;

            DiscsListBox.SelectedIndexChanged += this.OnDiscsListBoxSelectedIndexChanged;

            ClearAllButton.Click += this.OnClearAllButtonClick;

            RemoveDiscButton.Click += this.OnRemoveDiscButtonClick;
            ClearDiscsButton.Click += this.OnClearDiscsButtonClick;

            CopyDiscsButton.Click += this.OnCopyDiscsButtonClick;
            CopyAllDiscsButton.Click += this.OnCopyAllDiscsButtonClick;
            CopyFullDiscsButton.Click += this.OnCopyFullDiscsButtonClick;
        }

        private void UnregisterControlEvents()
        {
            ReadFromDriveToolStripMenuItem.Click -= this.OnReadFromDriveToolStripMenuItemClick;
            ReadMeToolStripMenuItem.Click -= this.OnReadMeToolStripMenuItemClick;
            CheckForUpdateToolStripMenuItem.Click -= this.OnCheckForUpdateToolStripMenuItemClick;
            AboutToolStripMenuItem.Click -= this.OnAboutToolStripMenuItemClick;

            InputTextBox.TextChanged -= this.OnInputTextBoxTextChanged;

            EpisodesListBox.SelectedIndexChanged -= this.OnEpisodeListBoxSelectedIndexChanged;

            AddButton.Click -= this.OnAddButtonClick;
            AddFromClipboardButton.Click -= this.OnAddFromClipboardButtonClick;

            RemoveEpisodeButton.Click -= this.OnRemoveEpisodeButtonClick;
            ClearEpisodesButton.Click -= this.OnClearButtonClick;

            MoveEpisodeButton.Click -= this.OnMoveEpisodeButtonClick;

            CopyEpisodesButton.Click -= this.OnCopyButtonClick;
            CopyAllEpisodesButton.Click -= this.OnCopyAllButtonClick;

            DiscsListBox.SelectedIndexChanged -= this.OnDiscsListBoxSelectedIndexChanged;

            ClearAllButton.Click -= this.OnClearAllButtonClick;

            RemoveDiscButton.Click -= this.OnRemoveDiscButtonClick;
            ClearDiscsButton.Click -= this.OnClearDiscsButtonClick;

            CopyDiscsButton.Click -= this.OnCopyDiscsButtonClick;
            CopyAllDiscsButton.Click -= this.OnCopyAllDiscsButtonClick;
            CopyFullDiscsButton.Click -= this.OnCopyFullDiscsButtonClick;
        }

        private void OnViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_viewModel.Input):
                    {
                        this.OnViewModelInputChanged();

                        break;
                    }
                case nameof(_viewModel.Episodes):
                    {
                        this.OnViewModelEpisodesChanged();

                        break;
                    }
                case nameof(_viewModel.SelectedEpisode):
                    {
                        RemoveEpisodeButton.Enabled = CanExecute(_viewModel.RemoveEpisodeCommand);

                        break;
                    }
                case nameof(_viewModel.EpisodesFullTime):
                    {
                        this.OnViewModelEpisodesFullTimeChanged();

                        break;
                    }
                case nameof(_viewModel.EpisodesMiddleTime):
                    {
                        EpisodesMiddleTimeTextBox.Text = _viewModel.EpisodesMiddleTime;

                        break;
                    }
                case nameof(_viewModel.EpisodesShortTime):
                    {
                        EpisodesShortTimeTextBox.Text = _viewModel.EpisodesShortTime;

                        break;
                    }
                case nameof(_viewModel.Discs):
                    {
                        this.OnViewModelDiscsChanged();

                        break;
                    }
                case nameof(_viewModel.SelectedDisc):
                    {
                        RemoveDiscButton.Enabled = CanExecute(_viewModel.RemoveDiscCommand);

                        break;
                    }
                case nameof(_viewModel.DiscsFullTime):
                    {
                        this.OnViewModelDiscsFullTimeChanged();

                        break;
                    }
            }
        }

        private void OnViewModelDiscsFullTimeChanged()
        {
            DiscsFullTimeTextBox.Text = _viewModel.DiscsFullTime;

            CopyDiscsButton.Enabled = CanExecute(_viewModel.CopyDiscsCommand);

            CopyAllDiscsButton.Enabled = CanExecute(_viewModel.CopyAllDiscsCommand);

            CopyFullDiscsButton.Enabled = CanExecute(_viewModel.CopyFullDiscsCommand);
        }

        private void OnViewModelDiscsChanged()
        {
            DiscsListBox.Items.Clear();

            DiscsListBox.Items.AddRange(_viewModel.Discs.ToArray());

            ClearDiscsButton.Enabled = CanExecute(_viewModel.ClearDiscsCommand);

            ClearAllButton.Enabled = CanExecute(_viewModel.ClearAllCommand);
        }

        private void OnViewModelEpisodesFullTimeChanged()
        {
            EpisodesFullTimeTextBox.Text = _viewModel.EpisodesFullTime;

            MoveEpisodeButton.Enabled = CanExecute(_viewModel.MoveEpisodesCommand);

            CopyEpisodesButton.Enabled = CanExecute(_viewModel.CopyEpisodesCommand);

            CopyAllEpisodesButton.Enabled = CanExecute(_viewModel.CopyAllEpisodesCommand);
        }

        private void OnViewModelEpisodesChanged()
        {
            EpisodesListBox.Items.Clear();

            EpisodesListBox.Items.AddRange(_viewModel.Episodes.ToArray());

            ClearEpisodesButton.Enabled = CanExecute(_viewModel.ClearEpisodesCommand);

            ClearAllButton.Enabled = CanExecute(_viewModel.ClearAllCommand);
        }

        private void OnViewModelInputChanged()
        {
            InputTextBox.Text = _viewModel.Input;

            AddButton.Enabled = CanExecute(_viewModel.AddEpisodeCommand);
        }

        private static bool CanExecute(ICommand command) => command.CanExecute(null);

        private static void ExecuteCommand(ICommand command) => command.Execute(null);

        private void OnAddButtonClick(object sender, EventArgs e)
        {
            ExecuteCommand(_viewModel.AddEpisodeCommand);

            InputTextBox.Focus();
        }

        private void OnClearButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.ClearEpisodesCommand);

        private void OnCopyButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.CopyEpisodesCommand);

        private void OnCopyAllButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.CopyAllEpisodesCommand);

        private void OnAddFromClipboardButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.AddFromClipboardCommand);

        private void OnRemoveEpisodeButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.RemoveEpisodeCommand);

        private void OnRemoveDiscButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.RemoveDiscCommand);

        private void OnClearDiscsButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.ClearDiscsCommand);

        private void OnCopyDiscsButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.CopyDiscsCommand);

        private void OnCopyAllDiscsButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.CopyAllDiscsCommand);

        private void OnMoveEpisodeButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.MoveEpisodesCommand);

        private void OnClearAllButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.ClearAllCommand);

        private void OnCopyFullDiscsButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.CopyFullDiscsCommand);

        private void OnFormLoad(object sender, EventArgs e)
        {
            var tt = new ToolTip();
            tt.SetToolTip(InputTextBox, "Enter the time as hours:minutes:seconds or minutes:seconds - but you can use \":\", \".\" or \",\" as seperator");

            tt = new ToolTip();
            tt.SetToolTip(EpisodesListBox, "Shows the added times");

            tt = new ToolTip();
            tt.SetToolTip(EpisodesFullTimeTextBox, "Shows the summed up times as hours:minutes:seconds");

            tt = new ToolTip();
            tt.SetToolTip(EpisodesMiddleTimeTextBox, "Shows the summed up times as minutes with 2 decimals");

            tt = new ToolTip();
            tt.SetToolTip(EpisodesShortTimeTextBox, "Shows the summed up times as minutes with 0 decimals");

            tt = new ToolTip();
            tt.SetToolTip(AddButton, "Adds the time entered in the TextBox into the ListBox");

            tt = new ToolTip();
            tt.SetToolTip(AddFromClipboardButton, "Adds the time from the clipboard into the left list");

            tt = new ToolTip();
            tt.SetToolTip(RemoveEpisodeButton, "Removes the selected entry from the left list");

            tt = new ToolTip();
            tt.SetToolTip(ClearEpisodesButton, "Clears all contents from the left list");

            tt = new ToolTip();
            tt.SetToolTip(MoveEpisodeButton, "Moves the summed up time to the right list");

            tt = new ToolTip();
            tt.SetToolTip(CopyEpisodesButton, "Copies the summed up times as minutes with 0 decimals into the clipboard");

            tt = new ToolTip();
            tt.SetToolTip(CopyAllEpisodesButton, "Copies the summed up times as hours:minutes:seconds and as minutes with 0 decimals into the clipboard");

            tt = new ToolTip();
            tt.SetToolTip(DiscsListBox, "Shows the added times");

            tt = new ToolTip();
            tt.SetToolTip(DiscsFullTimeTextBox, "Shows the summed up times as hours:minutes:seconds");

            tt = new ToolTip();
            tt.SetToolTip(DiscsMiddleTimeTextBox, "Shows the summed up times as minutes with 2 decimals");

            tt = new ToolTip();
            tt.SetToolTip(DiscsShortTimeTextBox, "Shows the summed up times as minutes with 0 decimals");

            tt = new ToolTip();
            tt.SetToolTip(ClearAllButton, "Clears all contents");

            tt = new ToolTip();
            tt.SetToolTip(RemoveDiscButton, "Removes the selected entry from the right list");

            tt = new ToolTip();
            tt.SetToolTip(ClearDiscsButton, "Clears all contents from the right list");

            tt = new ToolTip();
            tt.SetToolTip(CopyDiscsButton, "Copies the summed up times as minutes with 0 decimals into the clipboard");

            tt = new ToolTip();
            tt.SetToolTip(CopyAllDiscsButton, "Copies the summed up times as hours:minutes:seconds and as minutes with 0 decimals into the clipboard");

            tt = new ToolTip();
            tt.SetToolTip(CopyFullDiscsButton, "Copies the summed up times fancy fromatted into the clipboard");
        }

        private void CheckForNewVersion() => ExecuteCommand(_viewModel.CheckForNewVersionCommand);

        private void OnReadMeToolStripMenuItemClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.OpenHelpWindowCommand);

        private void OnAboutToolStripMenuItemClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.OpenAboutWindowCommand);

        private void OnCheckForUpdateToolStripMenuItemClick(object sender, EventArgs e) => this.CheckForNewVersion();

        private void OnDiscsListBoxSelectedIndexChanged(object sender, EventArgs e) => _viewModel.SelectedDisc = DiscsListBox.SelectedIndex;

        private void OnInputTextBoxTextChanged(object sender, EventArgs e) => _viewModel.Input = InputTextBox.Text;

        private void OnEpisodeListBoxSelectedIndexChanged(object sender, EventArgs e) => _viewModel.SelectedEpisode = EpisodesListBox.SelectedIndex;

        private void OnReadFromDriveToolStripMenuItemClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.OpenReadFromDriveWindowCommand);
    }
}