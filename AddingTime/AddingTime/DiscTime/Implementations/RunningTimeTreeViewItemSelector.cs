namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime.Implementations
{
    using System.Windows;
    using System.Windows.Controls;

    internal sealed class RunningTimeTreeViewItemSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container) => ((ITreeNode)item).CanBeChecked ? this.RunningTime : this.Header;

        public DataTemplate Header { get; set; }

        public DataTemplate RunningTime { get; set; }
    }
}