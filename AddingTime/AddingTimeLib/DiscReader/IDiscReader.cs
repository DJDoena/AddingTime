namespace DoenaSoft.DVDProfiler.AddingTime
{
    using AbstractionLayer.IOServices;

    public interface IDiscReader
    {
        #region Methods

        IDiscInfo GetDiscInfo(IDriveInfo drive);

        #endregion
    }
}