namespace DoenaSoft.DVDProfiler.AddingTime.Main.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using AbstractionLayer.UIServices;
    using ToolBox.Extensions;

    internal sealed class MainDataModel : IMainDataModel
    {
        private readonly IUIServices _uiServices;

        private readonly List<int> _episodes;

        private readonly List<DiscRunningTime> _discs;

        private readonly List<SeasonRunningTime> _seasons;

        public MainDataModel(IUIServices uiServices)
        {
            _uiServices = uiServices ?? throw new ArgumentNullException(nameof(uiServices));

            _episodes = new List<int>(10);

            _discs = new List<DiscRunningTime>(6);

            _seasons = new List<SeasonRunningTime>(6);
        }

        #region IMainModel

        #region Episodes

        public IEnumerable<int> EpisodeRunningTimes => _episodes;

        public event EventHandler EpisodesChanged;

        public string EpisodesFullTime { get; private set; }

        public event EventHandler EpisodesFullTimeChanged;

        public string EpisodesMiddleTime { get; private set; }

        public event EventHandler EpisodesMiddleTimeChanged;

        public string EpisodesShortTime { get; private set; }

        public event EventHandler EpisodesShortTimeChanged;

        public bool AddEpisode(string input)
        {
            if (this.FormatInput(input, out string text))
            {
                var seconds = MainHelper.CalcSeconds(text);

                _episodes.Add(seconds);

                this.OnEpisodesChanged();

                return true;
            }

            return false;
        }

        public void ClearEpisodes()
        {
            _episodes.Clear();

            this.OnEpisodesChanged();
        }

        public void RemoveEpisode(int index)
        {
            if (index >= _episodes.Count)
            {
                throw new ArgumentException("Invalid index", nameof(index));
            }

            _episodes.RemoveAt(index);

            this.OnEpisodesChanged();
        }

        #endregion

        #region Discs

        public IEnumerable<int> DiscRunningTimes => _discs.Select(d => d.RunningTime);

        public IEnumerable<DiscRunningTime> Discs => _discs;

        public event EventHandler DiscsChanged;

        public string DiscsFullTime { get; private set; }

        public event EventHandler DiscsFullTimeChanged;

        public string DiscsMiddleTime { get; private set; }

        public event EventHandler DiscsMiddleTimeChanged;

        public string DiscsShortTime { get; private set; }

        public event EventHandler DiscsShortTimeChanged;

        public void AddDisc(IEnumerable<int> episodeInputs)
        {
            _discs.Add(new DiscRunningTime(episodeInputs));

            this.OnDiscsChanged();
        }

        public void RemoveDisc(int index)
        {
            if (index >= _discs.Count)
            {
                throw new ArgumentException("Invalid index", nameof(index));
            }

            _discs.RemoveAt(index);

            this.OnDiscsChanged();
        }

        public void ClearDiscs()
        {
            _discs.Clear();

            this.OnDiscsChanged();
        }

        #endregion

        #region Seasons

        public IEnumerable<int> SeasonRunningTimes => _seasons.Select(s => s.RunningTime);

        public IEnumerable<SeasonRunningTime> Seasons => _seasons;

        public event EventHandler SeasonsChanged;

        public string SeasonsFullTime { get; private set; }

        public event EventHandler SeasonsFullTimeChanged;

        public string SeasonsMiddleTime { get; private set; }

        public event EventHandler SeasonsMiddleTimeChanged;

        public string SeasonsShortTime { get; private set; }

        public event EventHandler SeasonsShortTimeChanged;

        public void AddSeason(IEnumerable<DiscRunningTime> discInputs)
        {
            _seasons.Add(new SeasonRunningTime(discInputs));

            this.OnSeasonsChanged();
        }

        public void RemoveSeason(int index)
        {
            if (index >= _seasons.Count)
            {
                throw new ArgumentException("Invalid index", nameof(index));
            }

            _seasons.RemoveAt(index);

            this.OnSeasonsChanged();
        }

        public void ClearSeasons()
        {
            _seasons.Clear();

            this.OnSeasonsChanged();
        }

        #endregion

        #endregion

        #region Episodes

        private bool FormatInput(string input, out string text)
        {
            text = (input ?? string.Empty).Replace(".", ":").Replace(",", ":");

            var split = text.Split(':');

            if ((split.Length != 2) && (split.Length != 3))
            {
                _uiServices.ShowMessageBox("Invalid Time Format!", "Error", Buttons.OK, Icon.Warning);

                return false;
            }

            text = string.Empty;

            foreach (var part in split)
            {
                if (!int.TryParse(part, out int temp))
                {
                    _uiServices.ShowMessageBox($"Not a Number: {part}", "Error", Buttons.OK, Icon.Warning);

                    return false;
                }

                if (temp < 0 || temp > 59)
                {
                    _uiServices.ShowMessageBox($"Invalid Time Part: {part}", "Error", Buttons.OK, Icon.Warning);

                    return false;
                }

                text += temp.ToString("00:");
            }

            text = text.Substring(0, text.Length - 1);

            if (split.Length == 2)
            {
                text = "00:" + text;
            }

            return true;
        }

        private void OnEpisodesChanged()
        {
            Calc(_episodes, this.SetEpisodesFullTime, this.SetEpisodesMiddleTime, this.SetEpisodesShortTime);

            EpisodesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void SetEpisodesFullTime(string text)
        {
            this.EpisodesFullTime = text;

            this.RaisePropertyChanged(EpisodesFullTimeChanged);
        }

        private void SetEpisodesMiddleTime(string text)
        {
            this.EpisodesMiddleTime = text;

            this.RaisePropertyChanged(EpisodesMiddleTimeChanged);
        }

        private void SetEpisodesShortTime(string text)
        {
            this.EpisodesShortTime = text;

            this.RaisePropertyChanged(EpisodesShortTimeChanged);
        }

        #endregion

        #region Discs

        private void OnDiscsChanged()
        {
            Calc(this.DiscRunningTimes, this.SetDiscsFullTime, this.SetDiscsMiddleTime, this.SetDiscsShortTime);

            DiscsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void SetDiscsFullTime(string text)
        {
            this.DiscsFullTime = text;

            this.RaisePropertyChanged(DiscsFullTimeChanged);
        }

        private void SetDiscsMiddleTime(string text)
        {
            this.DiscsMiddleTime = text;

            this.RaisePropertyChanged(DiscsMiddleTimeChanged);
        }

        private void SetDiscsShortTime(string text)
        {
            this.DiscsShortTime = text;

            this.RaisePropertyChanged(DiscsShortTimeChanged);
        }

        #endregion

        #region Seasons

        private void OnSeasonsChanged()
        {
            Calc(this.SeasonRunningTimes, this.SetSeasonsFullTime, this.SetSeasonsMiddleTime, this.SetSeasonsShortTime);

            SeasonsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void SetSeasonsFullTime(string text)
        {
            this.SeasonsFullTime = text;

            this.RaisePropertyChanged(SeasonsFullTimeChanged);
        }

        private void SetSeasonsMiddleTime(string text)
        {
            this.SeasonsMiddleTime = text;

            this.RaisePropertyChanged(SeasonsMiddleTimeChanged);
        }

        private void SetSeasonsShortTime(string text)
        {
            this.SeasonsShortTime = text;

            this.RaisePropertyChanged(SeasonsShortTimeChanged);
        }

        #endregion

        private static void Calc(IEnumerable<int> entries, Action<string> setFullTime, Action<string> setMiddleTime, Action<string> setshortTime)
        {
            if (!entries.Any())
            {
                setFullTime(null);

                setMiddleTime(null);

                setshortTime(null);

                return;
            }

            var seconds = entries.Sum();

            var text = MainHelper.FormatTime(seconds);

            setFullTime(text);

            var fractalMinutes = MainHelper.CalcFractalMinutes(seconds);

            text = Math.Round(fractalMinutes, 2, MidpointRounding.AwayFromZero).ToString(CultureInfo.CurrentCulture);

            setMiddleTime(text);

            text = Math.Round(fractalMinutes, 0, MidpointRounding.AwayFromZero).ToString();

            setshortTime(text);
        }

        private void RaisePropertyChanged(EventHandler handler) => handler?.Invoke(this, EventArgs.Empty);
    }
}