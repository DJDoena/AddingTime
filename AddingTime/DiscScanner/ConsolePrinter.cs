using System;
using System.Collections.Generic;
using System.Linq;
using DoenaSoft.ToolBox.Extensions;

namespace DoenaSoft.DVDProfiler.AddingTime.DiscScanner
{
    internal static class ConsolePrinter
    {
        internal static void Print(IDiscInfo discInfo)
        {
            Console.WriteLine(discInfo.DiscLabel);
        }

        internal static void Print(Dictionary<String, List<ISubsetInfo>> structuredSubsets)
        {
            foreach (KeyValuePair<String, List<ISubsetInfo>> kvp in structuredSubsets)
            {
                IEnumerable<ISubsetInfo> subsets = GetFilteredSubsets(kvp.Value);

                if (subsets.HasItems())
                {
                    Print(kvp.Key, subsets);
                }
            }
        }

        private static IEnumerable<ISubsetInfo> GetFilteredSubsets(IEnumerable<ISubsetInfo> subsets)
        {
            subsets = subsets.Where(SubsetHasTrackWithMinimumLength);

            subsets = subsets.ToList();

            return (subsets);
        }

        private static Boolean SubsetHasTrackWithMinimumLength(ISubsetInfo subset)
        {
            Boolean subsetHasTrackWithMinimumLength = subset.Tracks.HasItemsWhere(TrackHasMinimumLength);

            return (subsetHasTrackWithMinimumLength);
        }

        private static Boolean TrackHasMinimumLength(ITrackInfo track)
        {
            Boolean trackHasMinimumLength = (track.RunningTime.TotalSeconds > 60);

            return (trackHasMinimumLength);
        }

        private static void Print(String key
            , IEnumerable<ISubsetInfo> subsets)
        {
            Console.WriteLine(key);

            foreach (ISubsetInfo subset in subsets)
            {
                PrintSubset(subset);
            }

            Console.WriteLine();
        }

        private static void PrintSubset(ISubsetInfo subset)
        {
            foreach (ITrackInfo track in subset.Tracks)
            {
                if (TrackHasMinimumLength(track))
                {
                    PrintTrack(track);
                }
            }
        }

        private static void PrintTrack(ITrackInfo track)
        {
            Console.WriteLine("  " + track.RunningTime);
        }
    }
}