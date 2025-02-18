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

        private readonly object _threadLock;

        protected readonly IIOServices _ioServices;

        #endregion

        #region Fields

        private IDriveInfo _driveInfo;

        private Thread _discScanner;

        private Timers.Timer _timer;

        #endregion

        #region IDiscInfo

        #region Properties

        public string DiscLabel => _driveInfo.DriveLabel;

        public abstract bool IsValid { get; }

        public abstract IEnumerable<ISubsetInfo> Subsets { get; }

        #endregion

        #endregion

        #region Constructor

        protected DiscInfoBase(IIOServices ioServices)
        {
            _ioServices = ioServices;

            _threadLock = new object();
        }

        #endregion

        #region Methods

        public virtual void Init(string path)
        {
            if (path == null)
            {
                throw (new ArgumentNullException(nameof(path)));
            }
            else if (_ioServices.Folder.Exists(path) == false)
            {
                throw (new ArgumentException("Path does not exist.", nameof(path)));
            }

            _driveInfo = this.GetDriveInfo(path);
        }

        protected void ScanAsync(object parameter)
        {
            _discScanner = new Thread(new ParameterizedThreadStart(this.Scan));

            _discScanner.Start(parameter);

            this.StartTimer();

            while (_discScanner.ThreadState == System.Threading.ThreadState.Running)
            {
                Thread.Sleep(250);
            }

            this.StopTimer();
        }

        protected abstract void Scan(object path);

        #region Timer

        private void StartTimer()
        {
            _timer = new Timers.Timer()
            {
                Interval = 90000
            };

            _timer.Elapsed += this.OnTimerElapsed;

            _timer.Start();
        }

        private void StopTimer()
        {
            lock (_threadLock)
            {
                if (_timer != null)
                {
                    _timer.Stop();

                    _timer.Elapsed -= this.OnTimerElapsed;

                    _timer = null;
                }
            }
        }

        private void OnTimerElapsed(object sender
            , Timers.ElapsedEventArgs e)
        {
            this.StopTimer();

            this.TryAbortThread();
        }

        private void TryAbortThread()
        {
            if ((_discScanner != null) && (_discScanner.ThreadState == System.Threading.ThreadState.Running))
            {
                try
                {
                    _discScanner.Abort();
                }
                catch
                { }
            }
        }

        #endregion

        private IDriveInfo GetDriveInfo(string path)
            => _ioServices.GetDrive(_ioServices.GetFolder(path).Root.Name);

        #endregion
    }
}