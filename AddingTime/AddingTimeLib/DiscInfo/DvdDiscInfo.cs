using System;
using System.Collections.Generic;
using System.Linq;
using DoenaSoft.AbstractionLayer.IOServices;
using DoenaSoft.ToolBox.Extensions;
using DvdNavigatorCrm;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    internal sealed class DvdDiscInfo : DiscInfoBase
    {
        #region Properties

        private IEnumerable<DvdTitleSet> TitleSets { get; set; }

        #endregion

        #region Constructor

        public DvdDiscInfo(IIOServices ioServices)
            : base(ioServices)
        {
            TitleSets = Enumerable.Empty<DvdTitleSet>();
        }

        #endregion

        #region IDiscInfo

        #region Properties

        public override Boolean IsValid
            => (TitleSets.HasItems());

        public override IEnumerable<ISubsetInfo> Subsets
            => (GetSubsets().ToList());

        #endregion

        #endregion

        #region Methods

        public override void Init(String path)
        {
            base.Init(path);

            ScanAsync(path);
        }

        protected override void Scan(Object parameter)
        {
            try
            {
                String path = (String)parameter;

                TitleSets = GetTitleSets(path);
            }
            catch
            { }
        }

        #region GetSubsets

        private IEnumerable<ISubsetInfo> GetSubsets()
        {
            foreach (DvdTitleSet titleSet in TitleSets)
            {
                ISubsetInfo subset = new DvdSubsetInfo(titleSet);

                yield return (subset);
            }
        }

        private IEnumerable<DvdTitleSet> GetTitleSets(String path)
        {
            IEnumerable<IEnumerable<DvdTitleSet>> nested = GetTitleSetsFromPath(path);

            IEnumerable<DvdTitleSet> flat = nested.SelectMany(item => item);

            flat = flat.ToList();

            return (flat);
        }

        private IEnumerable<IEnumerable<DvdTitleSet>> GetTitleSetsFromPath(String path)
        {
            String[] files = IOServices.Directory.GetFiles(path, "*.IFO");

            foreach (String file in files)
            {
                IEnumerable<DvdTitleSet> titleSets = TryGetTitleSetFromFile(file);

                yield return (titleSets);
            }
        }

        private static IEnumerable<DvdTitleSet> TryGetTitleSetFromFile(String file)
        {
            if (file.EndsWith("_TS.IFO", StringComparison.InvariantCultureIgnoreCase) == false)
            {
                DvdTitleSet titleSet = GetTitleSetFromFile(file);

                if (titleSet.IsValidTitleSet)
                {
                    yield return (titleSet);
                }
            }
        }

        private static DvdTitleSet GetTitleSetFromFile(String file)
        {
            DvdTitleSet titleSet = new DvdTitleSet(file);

            titleSet.Parse();

            return (titleSet);
        }

        #endregion

        #endregion
    }
}