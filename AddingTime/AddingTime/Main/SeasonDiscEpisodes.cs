using System;
using System.Collections.Generic;

namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    internal sealed class SeasonDiscEpisodes : Tuple<string, List<DiscEpisodes>>
    {
        public string SeasonRunningTime => this.Item1;

        public List<DiscEpisodes> DiscRunningTimes => this.Item2;

        public SeasonDiscEpisodes(string seasonRunningTime, List<DiscEpisodes> discRunningTimes) : base(seasonRunningTime, discRunningTimes)
        {
        }
    }
}