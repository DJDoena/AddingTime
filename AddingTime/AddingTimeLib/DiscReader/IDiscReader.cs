using DoenaSoft.AbstractionLayer.IOServices;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    public interface IDiscReader
    {
        #region Methods

        IDiscInfo GetDiscInfo(IDriveInfo drive);

        #endregion
    }
}