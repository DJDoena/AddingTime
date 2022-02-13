namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    internal sealed class EpisodeRunningTime : RunningTimeBase
    {
        public override int RunningTime { get; }

        public EpisodeRunningTime(int runningTime)
        {
            this.RunningTime = runningTime;
        }
    }
}