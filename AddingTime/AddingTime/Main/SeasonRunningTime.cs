namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class SeasonRunningTime
    {
        public int RunningTime => this.DiscRunningTimes.Sum(rt => rt.RunningTime);

        public IEnumerable<DiscRunningTime> DiscRunningTimes { get; }

        public SeasonRunningTime(IEnumerable<DiscRunningTime> discRunningTimes)
        {
            this.DiscRunningTimes = new List<DiscRunningTime>(discRunningTimes);
        }
    }
}