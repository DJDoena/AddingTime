namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using DvdNavigatorCrm;

    internal sealed class DvdSubsetInfo : SubsetInfoBase
    {
        #region Readonlies

        private readonly DvdTitleSet _TitleSet;

        #endregion

        #region Constructor

        public DvdSubsetInfo(DvdTitleSet titleSet)
            => _TitleSet = titleSet;

        #endregion

        #region ISubsetInfo

        #region Properties

        public override String Name
        {
            get
            {
                FileInfo fi = new FileInfo(_TitleSet.FileName);

                String[] parts = fi.Name.Split('_');

                String name = parts[0] + "_" + parts[1];

                return (name);
            }
        }

        public override Boolean IsValid
            => _TitleSet.IsValidTitleSet;

        #endregion

        #endregion

        #region Methods

        protected override IEnumerable<ITrackInfo> GetTracks()
            => GetChains().Select(chain => new DvdTrackInfo(chain));

        private IEnumerable<ProgramGroupChain> GetChains()
        {
            for (Int32 i = 1; i <= _TitleSet.ChainCount; i++)
            {
                ProgramGroupChain chain = _TitleSet.GetChain(i);

                yield return (chain);
            }
        }

        #endregion
    }
}