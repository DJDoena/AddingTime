namespace DoenaSoft.DVDProfiler.AddingTime.Main.Implementations
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Windows.Forms;
    using System.Windows.Input;

    internal partial class MainForm : Form
    {
        private readonly IMainViewModel _ViewModel;

        public MainForm(IMainViewModel viewModel)
        {
            _ViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            InitializeComponent();

            Load += OnFormLoad;

            _ViewModel.PropertyChanged += OnViewModelChanged;

            RegisterControlEvents();

            FormClosed += OnFormClosed;
        }

        private void OnFormClosed(Object sender
            , FormClosedEventArgs e)
        {
            FormClosed -= OnFormClosed;

            _ViewModel.PropertyChanged -= OnViewModelChanged;

            UnregisterControlEvents();

            Load -= OnFormLoad;
        }

        private void RegisterControlEvents()
        {
            ReadFromDriveToolStripMenuItem.Click += OnReadFromDriveToolStripMenuItemClick;
            ReadMeToolStripMenuItem.Click += OnReadMeToolStripMenuItemClick;
            CheckForUpdateToolStripMenuItem.Click += OnCheckForUpdateToolStripMenuItemClick;
            AboutToolStripMenuItem.Click += OnAboutToolStripMenuItemClick;

            InputTextBox.TextChanged += OnInputTextBoxTextChanged;

            EpisodesListBox.SelectedIndexChanged += OnEpisodeListBoxSelectedIndexChanged;

            AddButton.Click += OnAddButtonClick;
            AddFromClipboardButton.Click += OnAddFromClipboardButtonClick;

            RemoveEpisodeButton.Click += OnRemoveEpisodeButtonClick;
            ClearEpisodesButton.Click += OnClearButtonClick;

            MoveEpisodeButton.Click += OnMoveEpisodeButtonClick;

            CopyEpisodesButton.Click += OnCopyButtonClick;
            CopyAllEpisodesButton.Click += OnCopyAllButtonClick;

            DiscsListBox.SelectedIndexChanged += OnDiscsListBoxSelectedIndexChanged;

            ClearAllButton.Click += OnClearAllButtonClick;

            RemoveDiscButton.Click += OnRemoveDiscButtonClick;
            ClearDiscsButton.Click += OnClearDiscsButtonClick;

            CopyDiscsButton.Click += OnCopyDiscsButtonClick;
            CopyAllDiscsButton.Click += OnCopyAllDiscsButtonClick;
            CopyFullDiscsButton.Click += OnCopyFullDiscsButtonClick;
        }

        private void UnregisterControlEvents()
        {
            ReadFromDriveToolStripMenuItem.Click -= OnReadFromDriveToolStripMenuItemClick;
            ReadMeToolStripMenuItem.Click -= OnReadMeToolStripMenuItemClick;
            CheckForUpdateToolStripMenuItem.Click -= OnCheckForUpdateToolStripMenuItemClick;
            AboutToolStripMenuItem.Click -= OnAboutToolStripMenuItemClick;

            InputTextBox.TextChanged -= OnInputTextBoxTextChanged;

            EpisodesListBox.SelectedIndexChanged -= OnEpisodeListBoxSelectedIndexChanged;

            AddButton.Click -= OnAddButtonClick;
            AddFromClipboardButton.Click -= OnAddFromClipboardButtonClick;

            RemoveEpisodeButton.Click -= OnRemoveEpisodeButtonClick;
            ClearEpisodesButton.Click -= OnClearButtonClick;

            MoveEpisodeButton.Click -= OnMoveEpisodeButtonClick;

            CopyEpisodesButton.Click -= OnCopyButtonClick;
            CopyAllEpisodesButton.Click -= OnCopyAllButtonClick;

            DiscsListBox.SelectedIndexChanged -= OnDiscsListBoxSelectedIndexChanged;

            ClearAllButton.Click -= OnClearAllButtonClick;

            RemoveDiscButton.Click -= OnRemoveDiscButtonClick;
            ClearDiscsButton.Click -= OnClearDiscsButtonClick;

            CopyDiscsButton.Click -= OnCopyDiscsButtonClick;
            CopyAllDiscsButton.Click -= OnCopyAllDiscsButtonClick;
            CopyFullDiscsButton.Click -= OnCopyFullDiscsButtonClick;
        }

        private void OnViewModelChanged(Object sender
            , PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case (nameof(_ViewModel.Input)):
                    {
                        OnViewModelInputChanged();

                        break;
                    }
                case (nameof(_ViewModel.Episodes)):
                    {
                        OnViewModelEpisodesChanged();

                        break;
                    }
                case (nameof(_ViewModel.SelectedEpisode)):
                    {
                        RemoveEpisodeButton.Enabled = CanExecute(_ViewModel.RemoveEpisodeCommand);

                        break;
                    }
                case (nameof(_ViewModel.EpisodesFullTime)):
                    {
                        OnViewModelEpisodesFullTimeChanged();

                        break;
                    }
                case (nameof(_ViewModel.EpisodesMiddleTime)):
                    {
                        EpisodesMiddleTimeTextBox.Text = _ViewModel.EpisodesMiddleTime;

                        break;
                    }
                case (nameof(_ViewModel.EpisodesShortTime)):
                    {
                        EpisodesShortTimeTextBox.Text = _ViewModel.EpisodesShortTime;

                        break;
                    }
                case (nameof(_ViewModel.Discs)):
                    {
                        OnViewModelDiscsChanged();

                        break;
                    }
                case (nameof(_ViewModel.SelectedDisc)):
                    {
                        RemoveDiscButton.Enabled = CanExecute(_ViewModel.RemoveDiscCommand);

                        break;
                    }
                case (nameof(_ViewModel.DiscsFullTime)):
                    {
                        OnViewModelDiscsFullTimeChanged();

                        break;
                    }
            }
        }

        private void OnViewModelDiscsFullTimeChanged()
        {
            DiscsFullTimeTextBox.Text = _ViewModel.DiscsFullTime;

            CopyDiscsButton.Enabled = CanExecute(_ViewModel.CopyDiscsCommand);

            CopyAllDiscsButton.Enabled = CanExecute(_ViewModel.CopyAllDiscsCommand);

            CopyFullDiscsButton.Enabled = CanExecute(_ViewModel.CopyFullDiscsCommand);
        }

        private void OnViewModelDiscsChanged()
        {
            DiscsListBox.Items.Clear();

            DiscsListBox.Items.AddRange(_ViewModel.Discs.ToArray());

            ClearDiscsButton.Enabled = CanExecute(_ViewModel.ClearDiscsCommand);

            ClearAllButton.Enabled = CanExecute(_ViewModel.ClearAllCommand);
        }

        private void OnViewModelEpisodesFullTimeChanged()
        {
            EpisodesFullTimeTextBox.Text = _ViewModel.EpisodesFullTime;

            MoveEpisodeButton.Enabled = CanExecute(_ViewModel.MoveEpisodesCommand);

            CopyEpisodesButton.Enabled = CanExecute(_ViewModel.CopyEpisodesCommand);

            CopyAllEpisodesButton.Enabled = CanExecute(_ViewModel.CopyAllEpisodesCommand);
        }

        private void OnViewModelEpisodesChanged()
        {
            EpisodesListBox.Items.Clear();

            EpisodesListBox.Items.AddRange(_ViewModel.Episodes.ToArray());

            ClearEpisodesButton.Enabled = CanExecute(_ViewModel.ClearEpisodesCommand);

            ClearAllButton.Enabled = CanExecute(_ViewModel.ClearAllCommand);
        }

        private void OnViewModelInputChanged()
        {
            InputTextBox.Text = _ViewModel.Input;

            AddButton.Enabled = CanExecute(_ViewModel.AddEpisodeCommand);
        }

        private static Boolean CanExecute(ICommand command)
            => command.CanExecute(null);

        private static void ExecuteCommand(ICommand command)
            => command.Execute(null);

        private void OnAddButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(_ViewModel.AddEpisodeCommand);

            InputTextBox.Focus();
        }

        private void OnClearButtonClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.ClearEpisodesCommand);

        private void OnCopyButtonClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.CopyEpisodesCommand);

        private void OnCopyAllButtonClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.CopyAllEpisodesCommand);

        private void OnAddFromClipboardButtonClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.AddFromClipboardCommand);

        private void OnRemoveEpisodeButtonClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.RemoveEpisodeCommand);

        private void OnRemoveDiscButtonClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.RemoveDiscCommand);

        private void OnClearDiscsButtonClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.ClearDiscsCommand);

        private void OnCopyDiscsButtonClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.CopyDiscsCommand);

        private void OnCopyAllDiscsButtonClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.CopyAllDiscsCommand);

        private void OnMoveEpisodeButtonClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.MoveEpisodesCommand);

        private void OnClearAllButtonClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.ClearAllCommand);

        private void OnCopyFullDiscsButtonClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.CopyFullDiscsCommand);

        private void OnFormLoad(Object sender
            , EventArgs e)
        {
            ToolTip tt = new ToolTip();
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

        private void CheckForNewVersion()
            => ExecuteCommand(_ViewModel.CheckForNewVersionCommand);

        private void OnReadMeToolStripMenuItemClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.OpenHelpWindowCommand);

        private void OnAboutToolStripMenuItemClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.OpenAboutWindowCommand);

        private void OnCheckForUpdateToolStripMenuItemClick(Object sender
            , EventArgs e)
            => CheckForNewVersion();

        private void OnDiscsListBoxSelectedIndexChanged(Object sender
            , EventArgs e)
            => _ViewModel.SelectedDisc = DiscsListBox.SelectedIndex;

        private void OnInputTextBoxTextChanged(Object sender
            , EventArgs e)
            => _ViewModel.Input = InputTextBox.Text;

        private void OnEpisodeListBoxSelectedIndexChanged(Object sender
            , EventArgs e)
            => _ViewModel.SelectedEpisode = EpisodesListBox.SelectedIndex;

        private void OnReadFromDriveToolStripMenuItemClick(Object sender
            , EventArgs e)
            => ExecuteCommand(_ViewModel.OpenReadFromDriveWindowCommand);
    }
}