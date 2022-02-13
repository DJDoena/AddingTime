namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    internal abstract class RunningTimeBase
    {
        public abstract int RunningTime { get; }

        public string RunningTimeText => MainHelper.FormatTime(this.RunningTime);
    }
}