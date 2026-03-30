namespace DoenaSoft.DVDProfiler.AddingTime.DiscTime
{
    using System;
    using AbstractionLayer.UIServices;

    internal sealed class CloseEventArgs : EventArgs
    {
        public MessageResult Result { get; }

        public CloseEventArgs(MessageResult result) => this.Result = result;
    }
}