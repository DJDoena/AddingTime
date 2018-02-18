namespace DoenaSoft.DVDProfiler.AddingTime.DiscScanner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ToolBox.Extensions;

    internal static class ConsolePrinter
    {
        internal static void Print(IDiscInfo discInfo)
            => Console.WriteLine(discInfo.DiscLabel);

        internal static void Print(Dictionary<String, List<ISubsetInfo>> structuredSubsets)
            => structuredSubsets.ForEach(Print);

        private static void Print(KeyValuePair<String, List<ISubsetInfo>> kvp)
        {
            IEnumerable<ISubsetInfo> subsets = kvp.Value.Where(SubsetHasTrackWithMinimumLength).ToList();

            if (subsets.HasItems())
            {
                Print(kvp.Key, subsets);
            }
        }

        private static Boolean SubsetHasTrackWithMinimumLength(ISubsetInfo subset)
            => subset.Tracks.HasItemsWhere(TrackHasMinimumLength);

        private static Boolean TrackHasMinimumLength(ITrackInfo track)
            => track.RunningTime.TotalSeconds > 60;

        private static void Print(String key
            , IEnumerable<ISubsetInfo> subsets)
        {
            Console.WriteLine(key);

            subsets.ForEach(PrintSubset);

            Console.WriteLine();
        }

        private static void PrintSubset(ISubsetInfo subset)
            => subset.Tracks.Where(TrackHasMinimumLength).ForEach(PrintTrack);

        private static void PrintTrack(ITrackInfo track)
            => Console.WriteLine("  " + track.RunningTime);
    }
}