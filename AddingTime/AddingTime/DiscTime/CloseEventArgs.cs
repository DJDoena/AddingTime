using System;
using DoenaSoft.AbstractionLayer.UIServices;

namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime
{
    internal sealed class CloseEventArgs : EventArgs
    {
        public Result Result { get; private set; }

        public CloseEventArgs(Result result)
        {
            Result = result;
        }
    }
}