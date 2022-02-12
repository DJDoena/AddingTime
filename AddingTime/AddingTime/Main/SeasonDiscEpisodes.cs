namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class SeasonDiscEpisodes
    {
        public string SeasonRunningTime { get; }

        public List<DiscEpisodes> DiscRunningTimes { get; }

        public SeasonDiscEpisodes(string seasonRunningTime, IEnumerable<DiscEpisodes> discRunningTimes)
        {
            this.SeasonRunningTime = seasonRunningTime;
            this.DiscRunningTimes = discRunningTimes.ToList();
        }
    }
}