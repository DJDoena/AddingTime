namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ToolBox.Extensions;

    public static class SubsetStructurer
    {
        #region Methods

        public static Dictionary<string, List<ISubsetInfo>> GetStructuredSubsets(IDiscInfo discInfo)
        {
            Dictionary<string, List<ISubsetInfo>> structuredSubsets = new Dictionary<string, List<ISubsetInfo>>();

            discInfo.Subsets.Where(subset => subset.IsValid).OrderBy(subset => subset).ForEach(subset => AddSubset(structuredSubsets, subset));

            return (structuredSubsets);
        }

        private static void AddSubset(Dictionary<string, List<ISubsetInfo>> subsets
            , ISubsetInfo subset)
            => GetSubsetList(subsets, subset).Add(subset);

        private static List<ISubsetInfo> GetSubsetList(Dictionary<string, List<ISubsetInfo>> subsets
            , ISubsetInfo subset)
        {
            string key = subset.Name;

            if (subsets.TryGetValue(key, out List<ISubsetInfo> list) == false)
            {
                list = new List<ISubsetInfo>();

                subsets.Add(key, list);
            }

            return (list);
        }

        #endregion
    }
}