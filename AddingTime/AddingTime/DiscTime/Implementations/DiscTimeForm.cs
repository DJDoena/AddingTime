using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Input;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.AbstractionLayer.UIServices;
using Forms = System.Windows.Forms;

namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    internal partial class DiscTimeForm : Forms.Form
    {
        private readonly IDiscTimeViewModel ViewModel;

        private readonly IUIServices UIServices;

        public DiscTimeForm(IDiscTimeViewModel viewModel
            , IUIServices uiServices)
        {
            ViewModel = viewModel;
            UIServices = uiServices;

            InitializeComponent();

            PrepareForm();
        }

        private void PrepareForm()
        {
            Load += OnFormLoad;

            ViewModel.PropertyChanged += OnViewModelChanged;
            ViewModel.Closing += OnViewModelClosing;

            RegisterControlEvents();

            FormClosed += OnFormClosed;

            FillDrivesComboBox();

            OnViewModelMinimumLengthChanged();
        }

        private void FillDrivesComboBox()
        {
            IEnumerable<String> labels = ViewModel.Drives.Select(drive => drive.Label);

            DriveComboBox.Items.AddRange(labels.ToArray());

            DriveComboBox.SelectedItem = ViewModel.SelectedDrive;

            OnViewModelSelectedDriveChanged();
        }

        private void OnViewModelClosing(Object sender, CloseEventArgs e)
        {
            DialogResult = (e.Result == Result.OK) ? Forms.DialogResult.OK : Forms.DialogResult.Cancel;

            Close();
        }

        private void OnFormClosed(Object sender
            , Forms.FormClosedEventArgs e)
        {
            FormClosed -= OnFormClosed;

            ViewModel.PropertyChanged -= OnViewModelChanged;
            ViewModel.Closing -= OnViewModelClosing;

            UnregisterControlEvents();

            Load -= OnFormLoad;
        }

        private void RegisterControlEvents()
        {
            DriveComboBox.SelectedIndexChanged += OnDriveComboBoxSelectedIndexChanged;

            MinimumTrackLengthUpDown.ValueChanged += OnMinimumTrackLengthUpDownValueChanged;

            DiscTreeView.AfterCheck += OnDiscTreeViewAfterCheck;

            ScanButton.Click += OnScanButtonClick;

            MovieButton.Click += OnMovieButtonClick;
            SitcomButton.Click += OnSitcomButtonClick;
            DramaButton.Click += OnDramaButtonClick;

            SelectAllButton.Click += OnSelectAllButtonClick;

            OKButton.Click += OnOKButtonClick;
            AbortButton.Click += OnAbortButtonClick;
        }

        private void UnregisterControlEvents()
        {
            DriveComboBox.SelectedIndexChanged -= OnDriveComboBoxSelectedIndexChanged;

            MinimumTrackLengthUpDown.ValueChanged -= OnMinimumTrackLengthUpDownValueChanged;

            DiscTreeView.AfterCheck -= OnDiscTreeViewAfterCheck;

            ScanButton.Click -= OnScanButtonClick;

            MovieButton.Click -= OnMovieButtonClick;
            SitcomButton.Click -= OnSitcomButtonClick;
            DramaButton.Click -= OnDramaButtonClick;

            SelectAllButton.Click -= OnSelectAllButtonClick;

            OKButton.Click -= OnOKButtonClick;
            AbortButton.Click -= OnAbortButtonClick;
        }

        #region OnViewModelDiscTreeChanged

        private void OnViewModelDiscTreeChanged()
        {
            DestroyTreeView();

            BuildTreeView();

            DiscTreeView.ExpandAll();
        }

        #region DestroyTreeView

        private void DestroyTreeView()
        {
            DestroyTreeView(DiscTreeView.Nodes);

            DiscTreeView.Nodes.Clear();
        }

        private void DestroyTreeView(Forms.TreeNodeCollection nodes)
        {
            foreach (Forms.TreeNode node in nodes)
            {
                ITreeNode vmNode = (ITreeNode)(node.Tag);

                if (vmNode.CanBeChecked)
                {
                    vmNode.PropertyChanged -= OnViewModelNodeIsCheckedChanged;
                }

                DestroyTreeView(node.Nodes);
            }
        }

        #endregion

        #region BuildTreeView

        private void BuildTreeView()
        {
            BuildTreeView(DiscTreeView.Nodes, ViewModel.DiscTree);
        }

        private void BuildTreeView(Forms.TreeNodeCollection nodes
            , ObservableCollection<ITreeNode> vmNodes)
        {
            foreach (ITreeNode vmNode in vmNodes)
            {
                Forms.TreeNode node = new Forms.TreeNode(vmNode.Text);

                node.Tag = vmNode;

                nodes.Add(node);

                if (vmNode.CanBeChecked)
                {
                    vmNode.PropertyChanged += OnViewModelNodeIsCheckedChanged;
                }
                else
                {
                    HideCheckBox(DiscTreeView, node);
                }

                BuildTreeView(node.Nodes, vmNode.Nodes);
            }
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
        private void HideCheckBox(Forms.TreeView treeView
            , Forms.TreeNode node)
        {
            TVITEM treeViewItem = new TVITEM();

            treeViewItem.hItem = node.Handle;

            treeViewItem.mask = TVIF_STATE;

            treeViewItem.stateMask = TVIS_STATEIMAGEMASK;

            treeViewItem.state = 0;

            SendMessage(treeView.Handle, TVM_SETITEM, IntPtr.Zero, ref treeViewItem);
        }

        #endregion

        #endregion

        #endregion

        #region OnViewModelNodeIsCheckedChanged

        private void OnViewModelNodeIsCheckedChanged(Object sender
            , PropertyChangedEventArgs e)
        {
            UpdateTreeView(DiscTreeView.Nodes, (ITreeNode)sender);
        }

        private Boolean UpdateTreeView(Forms.TreeNodeCollection nodes
            , ITreeNode vmCompareNode)
        {
            foreach (Forms.TreeNode node in nodes)
            {
                ITreeNode vmCurrentNode = (ITreeNode)(node.Tag);

                if (vmCurrentNode == vmCompareNode)
                {
                    node.Checked = vmCompareNode.IsChecked;

                    return (true);
                }

                if (UpdateTreeView(node.Nodes, vmCompareNode))
                {
                    return (true);
                }
            }

            return (false);
        }

        #endregion

        private void OnOKButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.AcceptCommand);
        }

        private void OnAbortButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.CancelCommand);
        }
        private void OnViewModelDrivesChanges()
        {
            for (Int32 i = 0; i < ViewModel.Drives.Count; i++)
            {
                DriveComboBox.Items[i] = ViewModel.Drives[i].Label;
            }
        }

        private void OnFormLoad(Object sender
            , EventArgs e)
        {
            ViewModel.CheckForDecrypter();
        }

        private void OnDiscTreeViewAfterCheck(Object sender
            , Forms.TreeViewEventArgs e)
        {
            Forms.TreeNode node = e.Node;

            ITreeNode vmNode = (ITreeNode)(node.Tag);

            vmNode.IsChecked = node.Checked;
        }

        private void OnViewModelChanged(Object sender
            , PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case (nameof(ViewModel.Drives)):
                    {
                        OnViewModelDrivesChanges();

                        break;
                    }
                case (nameof(ViewModel.SelectedDrive)):
                    {
                        OnViewModelSelectedDriveChanged();

                        break;
                    }
                case (nameof(ViewModel.DiscTree)):
                    {
                        OnViewModelDiscTreeChanged();

                        break;
                    }
                case (nameof(ViewModel.MinimumLength)):
                    {
                        OnViewModelMinimumLengthChanged();

                        break;
                    }
            }
        }

        private void OnViewModelMinimumLengthChanged()
        {
            MinimumTrackLengthUpDown.Value = ViewModel.MinimumLength;
        }

        private void OnViewModelSelectedDriveChanged()
        {
            DriveComboBox.SelectedItem = ViewModel.SelectedDrive;

            ScanButton.Enabled = CanExecute(ViewModel.ScanCommand);
        }

        private void OnMinimumTrackLengthUpDownValueChanged(Object sender
            , EventArgs e)
        {
            ViewModel.MinimumLength = Convert.ToInt32(MinimumTrackLengthUpDown.Value);
        }

        private void OnDriveComboBoxSelectedIndexChanged(Object sender
            , EventArgs e)
        {
            ViewModel.SelectedDrive = (IDriveViewModel)(DriveComboBox.SelectedItem);
        }

        private void OnScanButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.ScanCommand);
        }

        private void OnSitcomButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.SetSitcomLengthCommand);
        }

        private void OnDramaButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.SetDramaLengthCommand);
        }

        private void OnMovieButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.SetMovieLengthCommand);
        }

        private void OnSelectAllButtonClick(Object sender
            , EventArgs e)
        {
            ExecuteCommand(ViewModel.CheckAllNodesCommand);
        }

        private static Boolean CanExecute(ICommand command)
            => (command.CanExecute(null));

        private static void ExecuteCommand(ICommand command)
        {
            command.Execute(null);
        }
    }
}