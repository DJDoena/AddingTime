using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using DoenaSoft.AbstractionLayer.IOServices;
using Timers = System.Timers;

namespace DoenaSoft.DVDProfiler.AddingTime
{
    [DebuggerDisplay("{DriveLetter} [{DiscLabel}] {GetType()}")]
    internal abstract class DiscInfoBase : IDiscInfo
    {
        #region Readonlies

        private readonly Object ThreadLock;

        protected readonly IIOServices IOServices;

        #endregion

        #region Properties

        private IDriveInfo DriveInfo { get; set; }

        private Thread DiscScanner { get; set; }

        private Timers.Timer Timer { get; set; }

        #endregion

        #region IDiscInfo

        #region Properties

        public String DiscLabel
            => (DriveInfo.Label);

        public abstract Boolean IsValid { get; }

        public abstract IEnumerable<ISubsetInfo> Subsets { get; }

        #endregion

        #endregion

        #region Constructor

        protected DiscInfoBase(IIOServices ioServices)
        {
            IOServices = ioServices;

            ThreadLock = new Object();
        }

        #endregion

        #region Methods

        public virtual void Init(String path)
        {
            if (path == null)
            {
                throw (new ArgumentNullException(nameof(path)));
            }
            else if (IOServices.Directory.Exists(path) == false)
            {
                throw (new ArgumentException("Path does not exist.", nameof(path)));
            }

            DriveInfo = GetDriveInfo(path);
        }

        protected void ScanAsync(Object parameter)
        {
            DiscScanner = new Thread(new ParameterizedThreadStart(Scan));

            DiscScanner.Start(parameter);

            StartTimer();

            while (DiscScanner.ThreadState == System.Threading.ThreadState.Running)
            {
                Thread.Sleep(250);
            }

            StopTimer();
        }

        protected abstract void Scan(Object path);

        #region Timer

        private void StartTimer()
        {
            Timer = new Timers.Timer();

            Timer.Interval = 90000;

            Timer.Elapsed += OnTimerElapsed;

            Timer.Start();
        }

        private void StopTimer()
        {
            lock (ThreadLock)
            {
                if (Timer != null)
                {
                    Timer.Stop();

                    Timer.Elapsed -= OnTimerElapsed;

                    Timer = null;
                }
            }
        }

        private void OnTimerElapsed(Object sender
            , Timers.ElapsedEventArgs e)
        {
            StopTimer();

            TryAbortThread();
        }

        private void TryAbortThread()
        {
            if ((DiscScanner != null) && (DiscScanner.ThreadState == System.Threading.ThreadState.Running))
            {
                try
                {
                    DiscScanner.Abort();
                }
                catch
                { }
            }
        }

        #endregion

        private IDriveInfo GetDriveInfo(String path)
        {
            IDirectoryInfo dirInfo = IOServices.GetDirectoryInfo(path);

            dirInfo = dirInfo.Root;

            IDriveInfo drive = IOServices.GetDriveInfo(dirInfo.Name);

            return (drive);
        }

        #endregion
    }
}