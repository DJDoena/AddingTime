using System;
using System.Collections.Generic;
using System.Linq;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    public static class SubsetStructurer
    {
        #region Methods

        public static Dictionary<String, List<ISubsetInfo>> GetStructuredSubsets(IDiscInfo discInfo)
        {
            Dictionary<String, List<ISubsetInfo>> structuredSubsets = new Dictionary<String, List<ISubsetInfo>>();

            IEnumerable<ISubsetInfo> subsets = GetFilteredSubsets(discInfo);

            foreach (ISubsetInfo subset in subsets)
            {
                AddSubset(structuredSubsets, subset);
            }

            return (structuredSubsets);
        }

        private static IEnumerable<ISubsetInfo> GetFilteredSubsets(IDiscInfo discInfo)
        {
            IEnumerable<ISubsetInfo> subsets = GetSubsets(discInfo);

            subsets = subsets.Where(subset => subset.IsValid);

            subsets = subsets.ToList();

            return (subsets);
        }

        private static List<ISubsetInfo> GetSubsets(IDiscInfo discInfo)
        {
            IEnumerable<ISubsetInfo> subsets = discInfo.Subsets;

            List<ISubsetInfo> list = subsets.ToList();

            list.Sort();

            return (list);
        }

        private static void AddSubset(Dictionary<String, List<ISubsetInfo>> subsets
            , ISubsetInfo subset)
        {
            List<ISubsetInfo> list = GetSubsetList(subsets, subset);

            list.Add(subset);
        }

        private static List<ISubsetInfo> GetSubsetList(Dictionary<String, List<ISubsetInfo>> subsets
            , ISubsetInfo subset)
        {
            List<ISubsetInfo> list;

            String key = subset.Name;

            if (subsets.TryGetValue(key, out list) == false)
            {
                list = new List<ISubsetInfo>();

                subsets.Add(key, list);
            }

            return (list);
        }

        #endregion
    }
}