using System;
using System.Collections.Generic;
using System.IO;
using DvdNavigatorCrm;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    internal sealed class DvdSubsetInfo : SubsetInfoBase
    {
        #region Constants

        private readonly DvdTitleSet TitleSet;

        #endregion

        #region Constructor

        public DvdSubsetInfo(DvdTitleSet titleSet)
        {
            TitleSet = titleSet;
        }

        #endregion

        #region ISubsetInfo

        #region Properties

        public override String Name
        {
            get
            {
                FileInfo fi = new FileInfo(TitleSet.FileName);

                String[] parts = fi.Name.Split('_');

                String name = parts[0] + "_" + parts[1];

                return (name);
            }
        }

        public override Boolean IsValid
            => (TitleSet.IsValidTitleSet);

        #endregion

        #endregion

        #region Methods

        protected override IEnumerable<ITrackInfo> GetTracks()
        {
            IEnumerable<ProgramGroupChain> chains = GetChains();

            foreach (ProgramGroupChain chain in chains)
            {
                ITrackInfo trackInfo = new DvdTrackInfo(chain);

                yield return (trackInfo);
            }
        }

        private IEnumerable<ProgramGroupChain> GetChains()
        {
            for (Int32 i = 1; i <= TitleSet.ChainCount; i++)
            {
                ProgramGroupChain chain = TitleSet.GetChain(i);

                yield return (chain);
            }
        }

        #endregion
    }
}