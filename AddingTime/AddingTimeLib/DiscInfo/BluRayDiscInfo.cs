namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AbstractionLayer.IOServices;
    using BDInfoLib;
    using BDInfoLib.BDROM;

    internal sealed class BluRayDiscInfo : DiscInfoBase
    {
        #region Properties

        private BDROM _bluRay;

        #endregion

        #region Constructor

        public BluRayDiscInfo(IIOServices ioServices) : base(ioServices)
        {
            _bluRay = null;
        }

        #endregion

        #region IDiscInfo

        #region Properties

        public override bool IsValid => _bluRay != null;

        public override IEnumerable<ISubsetInfo> Subsets
        {
            get => _bluRay != null
                ? _bluRay.PlaylistFiles.Values.Where(playlist => playlist.StreamClips.Count > 0).Select(playlist => new BluRaySubsetInfo(playlist)).ToList()
                : Enumerable.Empty<ISubsetInfo>();
        }

        #endregion

        #endregion

        #region Methods

        public override void Init(string path)
        {
            base.Init(path);

            BDInfoSettings.FilterShortPlaylists = false;

            this.ScanAsync(path);
        }

        protected override void Scan(object parameter)
        {
            try
            {
                var path = (string)parameter;

                var bluRay = new BDROM(path);

                bluRay.Scan();

                _bluRay = bluRay;
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