using System;
using System.Windows;
using System.Windows.Controls;

namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    internal sealed class RunningTimeTreeViewItemSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(Object item
            , DependencyObject container)
            => ((((ITreeNode)item).CanBeChecked) ? RunningTime : Header);

        public DataTemplate Header { get; set; }

        public DataTemplate RunningTime { get; set; }
    }
}