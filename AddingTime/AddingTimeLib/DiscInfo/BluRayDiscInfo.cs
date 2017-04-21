using System;
using System.Collections.Generic;
using System.Linq;
using BDInfoLib;
using BDInfoLib.BDROM;
using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    internal sealed class BluRayDiscInfo : DiscInfoBase
    {
        #region Properties

        private BDROM BluRay { get; set; }

        #endregion

        #region Constructor

        public BluRayDiscInfo(IIOServices ioServices)
            : base(ioServices)
        {
            BluRay = null;
        }

        #endregion

        #region IDiscInfo

        #region Properties

        public override Boolean IsValid
            => (BluRay != null);

        public override IEnumerable<ISubsetInfo> Subsets
        {
            get
            {
                IEnumerable<ISubsetInfo> subsets = Enumerable.Empty<ISubsetInfo>();

                if (BluRay != null)
                {
                    subsets = GetSubsets();
                }

                subsets = subsets.ToList();

                return (subsets);
            }
        }

        #endregion

        #endregion

        #region Methods

        public override void Init(String path)
        {
            base.Init(path);

            BDInfoSettings.FilterShortPlaylists = false;

            ScanAsync(path);
        }

        protected override void Scan(Object parameter)
        {
            try
            {
                String path = (String)parameter;

                BDROM bluRay = new BDROM(path);

                bluRay.Scan();

                BluRay = bluRay;
            }
            catch (OutOfMemoryException)
            {

                GC.Collect();

                GC.WaitForPendingFinalizers();
            }
            catch
            { }
        }

        private IEnumerable<ISubsetInfo> GetSubsets()
        {
            foreach (TSPlaylistFile playlist in BluRay.PlaylistFiles.Values)
            {
                if (playlist.StreamClips.Count > 0)
                {
                    ISubsetInfo subset = new BluRaySubsetInfo(playlist);

                    yield return (subset);
                }
            }
        }

        #endregion
    }
}