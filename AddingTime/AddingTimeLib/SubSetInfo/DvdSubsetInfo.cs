namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using DvdNavigatorCrm;

    internal sealed class DvdSubsetInfo : SubsetInfoBase
    {
        #region Readonlies

        private readonly DvdTitleSet _titleSet;

        #endregion

        #region Constructor

        public DvdSubsetInfo(DvdTitleSet titleSet)
        {
            _titleSet = titleSet;
        }

        #endregion

        #region ISubsetInfo

        #region Properties

        public override string Name
        {
            get
            {
                var fi = new FileInfo(_titleSet.FileName);

                var parts = fi.Name.Split('_');

                var name = parts[0] + "_" + parts[1];

                return name;
            }
        }

        public override bool IsValid => _titleSet.IsValidTitleSet;

        #endregion

        #endregion

        #region Methods

        protected override IEnumerable<ITrackInfo> GetTracks() => this.GetChains().Select(chain => new DvdTrackInfo(chain));

        private IEnumerable<ProgramGroupChain> GetChains()
        {
            for (var chainIndex = 1; chainIndex <= _titleSet.ChainCount; chainIndex++)
            {
                var chain = _titleSet.GetChain(chainIndex);

                yield return chain;
            }
        }

        #endregion
    }
}