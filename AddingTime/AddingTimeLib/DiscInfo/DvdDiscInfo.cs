namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AbstractionLayer.IOServices;
    using DvdNavigatorCrm;
    using ToolBox.Extensions;

    internal sealed class DvdDiscInfo : DiscInfoBase
    {
        #region Fields

        private IEnumerable<DvdTitleSet> _TitleSets;

        #endregion

        #region Constructor

        public DvdDiscInfo(IIOServices ioServices)
            : base(ioServices)
            => _TitleSets = Enumerable.Empty<DvdTitleSet>();

        #endregion

        #region IDiscInfo

        #region Properties

        public override Boolean IsValid => _TitleSets.HasItems();

        public override IEnumerable<ISubsetInfo> Subsets
            => _TitleSets.Select(titleSet => new DvdSubsetInfo(titleSet)).ToList();

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

                _TitleSets = _IOServices.Folder.GetFiles(path, "*.IFO").Select(TryGetTitleSetFromFile).SelectMany(item => item).ToList();
            }
            catch
            { }
        }

        #region GetSubsets

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