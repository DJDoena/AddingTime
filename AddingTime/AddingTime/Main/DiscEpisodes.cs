namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class DiscEpisodes
    {
        public string DiscRunningTime { get; }

        public List<string> EpisodeRunningTimes { get; }

        public DiscEpisodes(string discRunningTime, IEnumerable<string> episodeRunningTimes)
        {
            this.DiscRunningTime = discRunningTime;
            this.EpisodeRunningTimes = episodeRunningTimes.ToList();
        }
    }
}