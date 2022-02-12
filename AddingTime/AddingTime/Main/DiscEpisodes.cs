using System;
using System.Collections.Generic;

namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    internal sealed class DiscEpisodes : Tuple<string, List<string>>
    {
        public string DiscRunningTime => this.Item1;

        public List<string> EpisodeRunningTimes => this.Item2;

        public DiscEpisodes(string discRunningTime, List<string> episodeRunningTimes) : base(discRunningTime, episodeRunningTimes)
        {
        }
    }
}