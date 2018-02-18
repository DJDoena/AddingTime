namespace DoenaSoft.DVDProfiler.AddingTime
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;
    using AbstractionLayer.IOServices;
    using Timers = System.Timers;

    [DebuggerDisplay("{DriveLetter} [{DiscLabel}] {GetType()}")]
    internal abstract class DiscInfoBase : IDiscInfo
    {
        #region Readonlies

        private readonly Object _ThreadLock;

        protected readonly IIOServices _IOServices;

        #endregion

        #region Fields

        private IDriveInfo _DriveInfo;

        private Thread _DiscScanner;

        private Timers.Timer _Timer;

        #endregion

        #region IDiscInfo

        #region Properties

        public String DiscLabel
            => _DriveInfo.Label;

        public abstract Boolean IsValid { get; }

        public abstract IEnumerable<ISubsetInfo> Subsets { get; }

        #endregion

        #endregion

        #region Constructor

        protected DiscInfoBase(IIOServices ioServices)
        {
            _IOServices = ioServices;

            _ThreadLock = new Object();
        }

        #endregion

        #region Methods

        public virtual void Init(String path)
        {
            if (path == null)
            {
                throw (new ArgumentNullException(nameof(path)));
            }
            else if (_IOServices.Folder.Exists(path) == false)
            {
                throw (new ArgumentException("Path does not exist.", nameof(path)));
            }

            _DriveInfo = GetDriveInfo(path);
        }

        protected void ScanAsync(Object parameter)
        {
            _DiscScanner = new Thread(new ParameterizedThreadStart(Scan));

            _DiscScanner.Start(parameter);

            StartTimer();

            while (_DiscScanner.ThreadState == System.Threading.ThreadState.Running)
            {
                Thread.Sleep(250);
            }

            StopTimer();
        }

        protected abstract void Scan(Object path);

        #region Timer

        private void StartTimer()
        {
            _Timer = new Timers.Timer()
            {
                Interval = 90000
            };

            _Timer.Elapsed += OnTimerElapsed;

            _Timer.Start();
        }

        private void StopTimer()
        {
            lock (_ThreadLock)
            {
                if (_Timer != null)
                {
                    _Timer.Stop();

                    _Timer.Elapsed -= OnTimerElapsed;

                    _Timer = null;
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
            if ((_DiscScanner != null) && (_DiscScanner.ThreadState == System.Threading.ThreadState.Running))
            {
                try
                {
                    _DiscScanner.Abort();
                }
                catch
                { }
            }
        }

        #endregion

        private IDriveInfo GetDriveInfo(String path)
            => _IOServices.GetDriveInfo(_IOServices.GetFolderInfo(path).Root.Name);

        #endregion
    }
}