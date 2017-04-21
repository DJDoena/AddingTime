using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    internal sealed class TreeNode : ITreeNode
    {
        private Boolean m_IsChecked;

        internal TreeNode(String text)
            : this(text, new TimeSpan())
        {
            CanBeChecked = false;
        }

        internal TreeNode(String text,
            TimeSpan runningTime)
        {
            Text = text;
            RunningTime = runningTime;

            CanBeChecked = true;

            Nodes = new ObservableCollection<ITreeNode>();
        }

        #region ITreeNode

        public TimeSpan RunningTime { get; private set; }

        public String Text { get; private set; }

        public ObservableCollection<ITreeNode> Nodes { get; private set; }

        public Boolean IsChecked
        {
            get
            {
                return (m_IsChecked);
            }
            set
            {
                if (CanBeChecked == false)
                {
                    throw (new ArgumentException("This node cannot be checked"));
                }

                if (value != m_IsChecked)
                {
                    m_IsChecked = value;

                    RaisePropertyChanged(nameof(IsChecked));
                }
            }
        }

        public Boolean CanBeChecked { get; private set; }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        private void RaisePropertyChanged(String attribute)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(attribute));
        }
    }
}