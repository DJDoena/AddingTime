namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AbstractionLayer.IOServices;
    using BDInfoLib;
    using BDInfoLib.BDROM;
    using ToolBox.Extensions;

    internal sealed class BluRayDiscInfo : DiscInfoBase
    {
        #region Properties

        private BDROM _BluRay;

        #endregion

        #region Constructor

        public BluRayDiscInfo(IIOServices ioServices)
            : base(ioServices)
            => _BluRay = null;

        #endregion

        #region IDiscInfo

        #region Properties

        public override Boolean IsValid
            => _BluRay != null;

        public override IEnumerable<ISubsetInfo> Subsets
        {
            get => _BluRay != null
                ? _BluRay.PlaylistFiles.Values.Where(playlist => playlist.StreamClips.Count > 0).Select(playlist => new BluRaySubsetInfo(playlist)).ToList()
                : Enumerable.Empty<ISubsetInfo>();
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

                _BluRay = bluRay;
            }
            catch (OutOfMemoryException)
            {
                GC.Collect();

                GC.WaitForPendingFinalizers();
            }
            catch
            { }
        }

        #endregion
    }
}