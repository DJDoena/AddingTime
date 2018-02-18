namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    internal sealed class TreeNode : ITreeNode
    {
        private Boolean _IsChecked;

        internal TreeNode(String text)
            : this(text, new TimeSpan())
            => CanBeChecked = false;

        internal TreeNode(String text,
            TimeSpan runningTime)
        {
            Text = text;

            RunningTime = runningTime;

            CanBeChecked = true;

            Nodes = new ObservableCollection<ITreeNode>();
        }

        #region ITreeNode

        public TimeSpan RunningTime { get; }

        public String Text { get; }

        public ObservableCollection<ITreeNode> Nodes { get; }

        public Boolean IsChecked
        {
            get => _IsChecked;
            set
            {
                if (CanBeChecked == false)
                {
                    throw (new ArgumentException("This node cannot be checked"));
                }

                if (value != _IsChecked)
                {
                    _IsChecked = value;

                    RaisePropertyChanged(nameof(IsChecked));
                }
            }
        }

        public Boolean CanBeChecked { get; }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void RaisePropertyChanged(String attribute)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(attribute));
    }
}