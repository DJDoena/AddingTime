namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class DiscRunningTime : RunningTimeBase
    {
        public override int RunningTime => this.EpisodeRunningTimes.Sum(rt => rt.RunningTime);

        public IEnumerable<EpisodeRunningTime> EpisodeRunningTimes { get; }

        public DiscRunningTime(IEnumerable<EpisodeRunningTime> episodeRunningTimes)
        {
            this.EpisodeRunningTimes = new List<EpisodeRunningTime>(episodeRunningTimes);
        }
    }
}