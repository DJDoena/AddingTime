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

        private IEnumerable<DvdTitleSet> _titleSets;

        #endregion

        #region Constructor

        public DvdDiscInfo(IIOServices ioServices) : base(ioServices)
        {
            _titleSets = Enumerable.Empty<DvdTitleSet>();
        }

        #endregion

        #region IDiscInfo

        #region Properties

        public override bool IsValid => _titleSets.HasItems();

        public override IEnumerable<ISubsetInfo> Subsets => _titleSets.Select(titleSet => new DvdSubsetInfo(titleSet)).ToList();

        #endregion

        #endregion

        #region Methods

        public override void Init(string path)
        {
            base.Init(path);

            this.ScanAsync(path);
        }

        protected override void Scan(object parameter)
        {
            try
            {
                var path = (string)parameter;

                _titleSets = _ioServices.Folder.GetFileNames(path, "*.IFO").Select(TryGetTitleSetFromFile).SelectMany(item => item).ToList();
            }
            catch
            { }
        }

        #region GetSubsets

        private static IEnumerable<DvdTitleSet> TryGetTitleSetFromFile(string file)
        {
            if (!file.EndsWith("_TS.IFO", StringComparison.InvariantCultureIgnoreCase))
            {
                var titleSet = GetTitleSetFromFile(file);

                if (titleSet.IsValidTitleSet)
                {
                    yield return titleSet;
                }
            }
        }

        private static DvdTitleSet GetTitleSetFromFile(string file)
        {
            var titleSet = new DvdTitleSet(file);

            titleSet.Parse();

            return titleSet;
        }

        #endregion

        #endregion
    }
}