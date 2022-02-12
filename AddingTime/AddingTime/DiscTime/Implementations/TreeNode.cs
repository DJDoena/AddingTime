namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    internal sealed class TreeNode : ITreeNode
    {
        private bool _isChecked;

        internal TreeNode(string text) : this(text, new TimeSpan())
        {
            this.CanBeChecked = false;
        }

        internal TreeNode(string text, TimeSpan runningTime)
        {
            this.Text = text;

            this.RunningTime = runningTime;

            this.CanBeChecked = true;

            this.Nodes = new ObservableCollection<ITreeNode>();
        }

        #region ITreeNode

        public TimeSpan RunningTime { get; }

        public string Text { get; }

        public ObservableCollection<ITreeNode> Nodes { get; }

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                if (!this.CanBeChecked)
                {
                    throw (new ArgumentException("This node cannot be checked"));
                }

                if (value != _isChecked)
                {
                    _isChecked = value;

                    this.RaisePropertyChanged(nameof(this.IsChecked));
                }
            }
        }

        public bool CanBeChecked { get; }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void RaisePropertyChanged(string attribute) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(attribute));
    }
}