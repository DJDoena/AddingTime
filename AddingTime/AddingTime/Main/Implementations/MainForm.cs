using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using DoenaSoft.DVDProfiler.DVDProfilerHelper;

namespace DoenaSoft.DVDProfiler.AddingTime.Main.Implementations
{
    internal partial class MainForm : Form
    {
        private readonly IMainViewModel ViewModel;

        public MainForm(IMainViewModel viewModel)
        {
            if (viewModel == null)
            {
                throw (new ArgumentNullException(nameof(viewModel)));
            }

            ViewModel = viewModel;

            InitializeComponent();

            Load += OnFormLoad;

            ViewModel.PropertyChanged += OnViewModelChanged;

            RegisterControlEvents();

            FormClosed += OnFormClosed;
        }

        private void OnFormClosed(Object sender
            , FormClosedEventArgs e)
        {
            FormClosed -= OnFormClosed;

            ViewModel.PropertyChanged -= OnViewModelChanged;

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
                case (nameof(ViewModel.Input)):
                    {
                        OnViewModelInputChanged();

                        break;
                    }
                case (nameof(ViewModel.Episodes)):
                    {
                        OnViewModelEpisodesChanged();

                        break;
                    }
                case (nameof(ViewModel.SelectedEpisode)):
                    {
                        RemoveEpisodeButton.Enabled = CanExecute(ViewModel.RemoveEpisodeCommand);

                        break;
                    }
                case (nameof(ViewModel.EpisodesFullTime)):
                    {
                        OnViewModelEpisodesFullTimeChanged();

                        break;
                    }
                case (nameof(ViewModel.EpisodesMiddleTime)):
                    {
                        EpisodesMiddleTimeTextBox.Text = ViewModel.EpisodesMiddleTime;

                        break;
                    }
                case (nameof(ViewModel.EpisodesShortTime)):
                    {
                        EpisodesShortTimeTextBox.Text = ViewModel.EpisodesShortTime;

                        break;
                    }
                case (nameof(ViewModel.Discs)):
                    {
                        OnViewModelDiscsChanged();

                        break;
                    }
                case (nameof(ViewModel.SelectedDisc)):
                    {
                        RemoveDiscButton.Enabled = CanExecute(ViewModel.RemoveDiscCommand);

                        break;
                    }
                case (nameof(ViewModel.DiscsFullTime)):
                    {
                        OnViewModelDiscsFullTimeChanged();

                        break;
                    }
            }
        }

        private void OnViewModelDiscsFullTimeChanged()
        {
            DiscsFullTimeTextBox.Text = ViewModel.DiscsFullTime;

            CopyDiscsButton.Enabled = CanExecute(ViewModel.CopyDiscsCommand);

            CopyAllDiscsButton.Enabled = CanExecute(ViewModel.CopyAllDiscsCommand);

            CopyFullDiscsButton.Enabled = CanExecute(ViewModel.CopyFullDiscsCommand);
        }

        private void OnViewModelDiscsChanged()
        {
            DiscsListBox.Items.Clear();

            DiscsListBox.Items.AddRange(ViewModel.Discs.ToArray());

            ClearDiscsButton.Enabled = CanExecute(ViewModel.ClearDiscsCommand);

            ClearAllButton.Enabled = CanExecute(ViewModel.ClearAllCommand);
        }

        private void OnViewModelEpisodesFullTimeChanged()
        {
            EpisodesFullTimeTextBox.Text = ViewModel.EpisodesFullTime;

            MoveEpisodeButton.Enabled = CanExecute(ViewModel.MoveEpisodesCommand);

            CopyEpisodesButton.Enabled = CanExecute(ViewModel.CopyEpisodesCommand);

            CopyAllEpisodesButton.Enabled = CanExecute(ViewModel.CopyAllEpisodesCommand);
        }

        private void OnViewModelEpisodesChanged()
        {
            EpisodesListBox.Items.Clear();

            EpisodesListBox.Items.AddRange(ViewModel.Episodes.ToArray());

            ClearEpisodesButton.Enabled = CanExecute(ViewModel.ClearEpisodesCommand);

            ClearAllButton.Enabled = CanExecute(ViewModel.ClearAllCommand);
        }

        private void OnViewModelInputChanged()
        {
            InputTextBox.Text = ViewModel.Input;

            AddButton.Enabled = CanExecute(ViewModel.AddEpisodeCommand);
        }

        private static Boolean CanExecute(ICommand command)
            => (command.CanExecute(null));

        private static void ExecuteCommand(ICommand command)
        {
            command.Execute(null);
        }

        private void OnAddButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.AddEpisodeCommand);

            InputTextBox.Focus();
        }

        private void OnClearButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.ClearEpisodesCommand);
        }

        private void OnCopyButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.CopyEpisodesCommand);
        }

        private void OnCopyAllButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.CopyAllEpisodesCommand);
        }

        private void OnAddFromClipboardButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.AddFromClipboardCommand);
        }

        private void OnRemoveEpisodeButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.RemoveEpisodeCommand);
        }

        private void OnRemoveDiscButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.RemoveDiscCommand);
        }

        private void OnClearDiscsButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.ClearDiscsCommand);
        }

        private void OnCopyDiscsButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.CopyDiscsCommand);
        }

        private void OnCopyAllDiscsButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.CopyAllDiscsCommand);
        }

        private void OnMoveEpisodeButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.MoveEpisodesCommand);
        }

        private void OnClearAllButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.ClearAllCommand);
        }

        private void OnCopyFullDiscsButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.CopyFullDiscsCommand);
        }

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
        {
            ExecuteCommand(ViewModel.CheckForNewVersionCommand);
        }

        private void OnReadMeToolStripMenuItemClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.OpenHelpWindowCommand);
        }

        private void OnAboutToolStripMenuItemClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.OpenAboutWindowCommand);
        }

        private void OnCheckForUpdateToolStripMenuItemClick(Object sender
            , EventArgs e)
        {
            CheckForNewVersion();
        }

        private void OnDiscsListBoxSelectedIndexChanged(Object sender
            , EventArgs e)
        {
            ViewModel.SelectedDisc = DiscsListBox.SelectedIndex;
        }

        private void OnInputTextBoxTextChanged(Object sender
            , EventArgs e)
        {
            ViewModel.Input = InputTextBox.Text;
        }

        private void OnEpisodeListBoxSelectedIndexChanged(Object sender
            , EventArgs e)
        {
            ViewModel.SelectedEpisode = EpisodesListBox.SelectedIndex;
        }

        private void OnReadFromDriveToolStripMenuItemClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.OpenReadFromDriveWindowCommand);
        }
    }
}