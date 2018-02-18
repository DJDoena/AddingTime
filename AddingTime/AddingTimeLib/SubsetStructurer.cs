namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ToolBox.Extensions;

    public static class SubsetStructurer
    {
        #region Methods

        public static Dictionary<String, List<ISubsetInfo>> GetStructuredSubsets(IDiscInfo discInfo)
        {
            Dictionary<String, List<ISubsetInfo>> structuredSubsets = new Dictionary<String, List<ISubsetInfo>>();

            discInfo.Subsets.Where(subset => subset.IsValid).OrderBy(subset => subset).ForEach(subset => AddSubset(structuredSubsets, subset));

            return (structuredSubsets);
        }

        private static void AddSubset(Dictionary<String, List<ISubsetInfo>> subsets
            , ISubsetInfo subset)
            => GetSubsetList(subsets, subset).Add(subset);

        private static List<ISubsetInfo> GetSubsetList(Dictionary<String, List<ISubsetInfo>> subsets
            , ISubsetInfo subset)
        {
            String key = subset.Name;

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