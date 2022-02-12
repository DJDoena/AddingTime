namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Input;
    using AbstractionLayer.UIServices;
    using ToolBox.Extensions;
    using Forms = System.Windows.Forms;

    internal partial class DiscTimeForm : Forms.Form
    {
        private readonly IDiscTimeViewModel _viewModel;

        public DiscTimeForm(IDiscTimeViewModel viewModel)
        {
            _viewModel = viewModel;

            this.InitializeComponent();

            this.PrepareForm();

            this.Icon = Properties.Resource.djdsoft;
        }

        private void PrepareForm()
        {
            Load += this.OnFormLoad;

            _viewModel.PropertyChanged += this.OnViewModelChanged;
            _viewModel.Closing += this.OnViewModelClosing;

            this.RegisterControlEvents();

            FormClosed += this.OnFormClosed;

            this.FillDrivesComboBox();

            this.OnViewModelMinimumLengthChanged();
        }

        private void FillDrivesComboBox()
        {
            var labels = _viewModel.Drives.Select(drive => drive.Label);

            DriveComboBox.Items.AddRange(labels.ToArray());

            DriveComboBox.SelectedItem = _viewModel.SelectedDrive;

            this.OnViewModelSelectedDriveChanged();
        }

        private void OnViewModelClosing(object sender, CloseEventArgs e)
        {
            this.DialogResult = (e.Result == Result.OK) ? Forms.DialogResult.OK : Forms.DialogResult.Cancel;

            this.Close();
        }

        private void OnFormClosed(object sender, Forms.FormClosedEventArgs e)
        {
            FormClosed -= this.OnFormClosed;

            _viewModel.PropertyChanged -= this.OnViewModelChanged;
            _viewModel.Closing -= this.OnViewModelClosing;

            this.UnregisterControlEvents();

            Load -= this.OnFormLoad;
        }

        private void RegisterControlEvents()
        {
            DriveComboBox.SelectedIndexChanged += this.OnDriveComboBoxSelectedIndexChanged;

            MinimumTrackLengthUpDown.ValueChanged += this.OnMinimumTrackLengthUpDownValueChanged;

            DiscTreeView.AfterCheck += this.OnDiscTreeViewAfterCheck;

            ScanButton.Click += this.OnScanButtonClick;

            MovieButton.Click += this.OnMovieButtonClick;
            SitcomButton.Click += this.OnSitcomButtonClick;
            DramaButton.Click += this.OnDramaButtonClick;

            SelectAllButton.Click += this.OnSelectAllButtonClick;

            OKButton.Click += this.OnOKButtonClick;
            AbortButton.Click += this.OnAbortButtonClick;
        }

        private void UnregisterControlEvents()
        {
            DriveComboBox.SelectedIndexChanged -= this.OnDriveComboBoxSelectedIndexChanged;

            MinimumTrackLengthUpDown.ValueChanged -= this.OnMinimumTrackLengthUpDownValueChanged;

            DiscTreeView.AfterCheck -= this.OnDiscTreeViewAfterCheck;

            ScanButton.Click -= this.OnScanButtonClick;

            MovieButton.Click -= this.OnMovieButtonClick;
            SitcomButton.Click -= this.OnSitcomButtonClick;
            DramaButton.Click -= this.OnDramaButtonClick;

            SelectAllButton.Click -= this.OnSelectAllButtonClick;

            OKButton.Click -= this.OnOKButtonClick;
            AbortButton.Click -= this.OnAbortButtonClick;
        }

        #region OnViewModelDiscTreeChanged

        private void OnViewModelDiscTreeChanged()
        {
            this.DestroyTreeView();

            this.BuildTreeView();

            DiscTreeView.ExpandAll();
        }

        #region DestroyTreeView

        private void DestroyTreeView()
        {
            this.DestroyTreeView(DiscTreeView.Nodes);

            DiscTreeView.Nodes.Clear();
        }

        private void DestroyTreeView(Forms.TreeNodeCollection nodes) => nodes.OfType<Forms.TreeNode>().ForEach(this.DestroyTreeNode);

        private void DestroyTreeNode(Forms.TreeNode node)
        {
            var vmNode = (ITreeNode)node.Tag;

            if (vmNode.CanBeChecked)
            {
                vmNode.PropertyChanged -= this.OnViewModelNodeIsCheckedChanged;
            }

            this.DestroyTreeView(node.Nodes);
        }

        #endregion

        #region BuildTreeView

        private void BuildTreeView() => this.BuildTreeView(DiscTreeView.Nodes, _viewModel.DiscTree);

        private void BuildTreeView(Forms.TreeNodeCollection nodes, ObservableCollection<ITreeNode> vmNodes) => vmNodes.ForEach(vmNode => this.BuildTreeNode(nodes, vmNode));

        private void BuildTreeNode(Forms.TreeNodeCollection nodes, ITreeNode vmNode)
        {
            var node = new Forms.TreeNode(vmNode.Text)
            {
                Tag = vmNode,
            };

            nodes.Add(node);

            if (vmNode.CanBeChecked)
            {
                vmNode.PropertyChanged += this.OnViewModelNodeIsCheckedChanged;
            }
            else
            {
                this.HideCheckBox(DiscTreeView, node);
            }

            this.BuildTreeView(node.Nodes, vmNode.Nodes);
        }

        #region HideCheckBox

        private const int TVIF_STATE = 0x8;

        private const int TVIS_STATEIMAGEMASK = 0xF000;

        private const int TV_FIRST = 0x1100;

        private const int TVM_SETITEM = TV_FIRST + 63;

        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
        private struct TVITEM
        {
            public int mask;

            public IntPtr hItem;

            public int state;

            public int stateMask;

            [MarshalAs(UnmanagedType.LPTStr)]
            public string lpszText;

            public int cchTextMax;

            public int iImage;

            public int iSelectedImage;

            public int cChildren;

            public IntPtr lParam;
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, ref TVITEM lParam);

        /// <summary>
        /// Hides the checkbox for the specified node on a TreeView control.
        /// </summary>
        private void HideCheckBox(Forms.TreeView treeView, Forms.TreeNode node)
        {
            var treeViewItem = new TVITEM
            {
                hItem = node.Handle,
                mask = TVIF_STATE,
                stateMask = TVIS_STATEIMAGEMASK,
                state = 0,
            };

            SendMessage(treeView.Handle, TVM_SETITEM, IntPtr.Zero, ref treeViewItem);
        }

        #endregion

        #endregion

        #endregion

        #region OnViewModelNodeIsCheckedChanged

        private void OnViewModelNodeIsCheckedChanged(object sender, PropertyChangedEventArgs e) => this.UpdateTreeView(DiscTreeView.Nodes, (ITreeNode)sender);

        private bool UpdateTreeView(Forms.TreeNodeCollection nodes, ITreeNode vmCompareNode) => nodes.OfType<Forms.TreeNode>().HasItemsWhere(node => this.UpdateTreeNode(node, vmCompareNode));

        private bool UpdateTreeNode(Forms.TreeNode node, ITreeNode vmCompareNode)
        {
            var vmCurrentNode = (ITreeNode)node.Tag;

            if (vmCurrentNode == vmCompareNode)
            {
                node.Checked = vmCompareNode.IsChecked;

                return true;
            }

            if (this.UpdateTreeView(node.Nodes, vmCompareNode))
            {
                return true;
            }

            return false;
        }

        #endregion

        private void OnOKButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.AcceptCommand);

        private void OnAbortButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.CancelCommand);

        private void OnViewModelDrivesChanges()
        {
            for (var driveIndex = 0; driveIndex < _viewModel.Drives.Count; driveIndex++)
            {
                DriveComboBox.Items[driveIndex] = _viewModel.Drives[driveIndex].Label;
            }
        }

        private void OnFormLoad(object sender, EventArgs e) => _viewModel.CheckForDecrypter();

        private void OnDiscTreeViewAfterCheck(object sender, Forms.TreeViewEventArgs e)
        {
            var node = e.Node;

            var vmNode = (ITreeNode)node.Tag;

            vmNode.IsChecked = node.Checked;
        }

        private void OnViewModelChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(_viewModel.Drives):
                    {
                        this.OnViewModelDrivesChanges();

                        break;
                    }
                case nameof(_viewModel.SelectedDrive):
                    {
                        this.OnViewModelSelectedDriveChanged();

                        break;
                    }
                case nameof(_viewModel.DiscTree):
                    {
                        this.OnViewModelDiscTreeChanged();

                        break;
                    }
                case nameof(_viewModel.MinimumLength):
                    {
                        this.OnViewModelMinimumLengthChanged();

                        break;
                    }
            }
        }

        private void OnViewModelMinimumLengthChanged() => MinimumTrackLengthUpDown.Value = _viewModel.MinimumLength;

        private void OnViewModelSelectedDriveChanged()
        {
            DriveComboBox.SelectedItem = _viewModel.SelectedDrive;

            ScanButton.Enabled = CanExecute(_viewModel.ScanCommand);
        }

        private void OnMinimumTrackLengthUpDownValueChanged(object sender, EventArgs e) => _viewModel.MinimumLength = Convert.ToInt32(MinimumTrackLengthUpDown.Value);

        private void OnDriveComboBoxSelectedIndexChanged(object sender, EventArgs e) => _viewModel.SelectedDrive = (IDriveViewModel)(DriveComboBox.SelectedItem);

        private void OnScanButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.ScanCommand);

        private void OnSitcomButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.SetSitcomLengthCommand);

        private void OnDramaButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.SetDramaLengthCommand);

        private void OnMovieButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.SetMovieLengthCommand);

        private void OnSelectAllButtonClick(object sender, EventArgs e) => ExecuteCommand(_viewModel.CheckAllNodesCommand);

        private static bool CanExecute(ICommand command) => command.CanExecute(null);

        private static void ExecuteCommand(ICommand command) => command.Execute(null);
    }
}