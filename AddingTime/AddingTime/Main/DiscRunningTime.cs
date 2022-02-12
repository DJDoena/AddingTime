namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class DiscRunningTime
    {
        public int RunningTime => this.EpisodeRunningTimes.Sum(rt => rt);

        public IEnumerable<int> EpisodeRunningTimes { get; }

        public DiscRunningTime(IEnumerable<int> episodeRunningTimes)
        {
            this.EpisodeRunningTimes = new List<int>(episodeRunningTimes);
        }
    }
}