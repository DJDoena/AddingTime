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

        private readonly List<string> _episodes;

        private readonly List<DiscEpisodes> _discs;

        private readonly List<SeasonDiscEpisodes> _seasons;

        public MainDataModel(IUIServices uiServices)
        {
            _uiServices = uiServices ?? throw new ArgumentNullException(nameof(uiServices));

            _episodes = new List<string>(10);

            _discs = new List<DiscEpisodes>(6);

            _seasons = new List<SeasonDiscEpisodes>(6);
        }

        #region IMainModel

        #region Episodes

        public IEnumerable<string> Episodes => _episodes.Select(e => e);

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
                _episodes.Add(text);

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

        public IEnumerable<string> Discs => _discs.Select(d => d.DiscRunningTime);

        public IEnumerable<DiscEpisodes> DiscEpisodes => _discs;

        public event EventHandler DiscsChanged;

        public string DiscsFullTime { get; private set; }

        public event EventHandler DiscsFullTimeChanged;

        public string DiscsMiddleTime { get; private set; }

        public event EventHandler DiscsMiddleTimeChanged;

        public string DiscsShortTime { get; private set; }

        public event EventHandler DiscsShortTimeChanged;

        public void AddDisc(string input)
        {
            if (this.FormatInput(input, out string text))
            {
                _discs.Add(new DiscEpisodes(text, new List<string>(0)));

                this.OnDiscsChanged();
            }
        }

        public void AddDisc(string discInput, IEnumerable<string> episodeInputs)
        {
            if (episodeInputs != null)
            {
                if (this.FormatInput(discInput, out var discText))
                {
                    _discs.Add(new DiscEpisodes(discText, episodeInputs.ToList()));

                    this.OnDiscsChanged();
                }
            }
            else
            {
                this.AddDisc(discInput);
            }
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

        public IEnumerable<string> Seasons => _seasons.Select(s => s.SeasonRunningTime);

        public IEnumerable<SeasonDiscEpisodes> SeasonDiscs => _seasons;

        public event EventHandler SeasonsChanged;

        public string SeasonsFullTime { get; private set; }

        public event EventHandler SeasonsFullTimeChanged;

        public string SeasonsMiddleTime { get; private set; }

        public event EventHandler SeasonsMiddleTimeChanged;

        public string SeasonsShortTime { get; private set; }

        public event EventHandler SeasonsShortTimeChanged;

        public void AddSeason(string discInput)
        {
            if (this.FormatInput(discInput, out string discText))
            {
                _seasons.Add(new SeasonDiscEpisodes(discText, new List<DiscEpisodes>(0)));

                this.OnSeasonsChanged();
            }
        }

        public void AddSeason(string seasonInput, IEnumerable<DiscEpisodes> discInputs)
        {
            if (discInputs != null)
            {
                if (this.FormatInput(seasonInput, out var seasonText))
                {
                    _seasons.Add(new SeasonDiscEpisodes(seasonText, discInputs));

                    this.OnSeasonsChanged();
                }
            }
            else
            {
                this.AddSeason(seasonInput);
            }
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
            Calc(this.Discs, this.SetDiscsFullTime, this.SetDiscsMiddleTime, this.SetDiscsShortTime);

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
            Calc(this.Seasons, this.SetSeasonsFullTime, this.SetSeasonsMiddleTime, this.SetSeasonsShortTime);

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

        private static void Calc(IEnumerable<string> entries, Action<string> setFullTime, Action<string> setMiddleTime, Action<string> setshortTime)
        {
            if (!entries.Any())
            {
                setFullTime(null);

                setMiddleTime(null);

                setshortTime(null);

                return;
            }

            var seconds = 0;

            entries.ForEach(entry => seconds += MainHelper.CalcSeconds(entry));

            var fractalMinutes = MainHelper.CalcFractalMinutes(seconds);

            var hours = seconds / 3600;

            seconds -= hours * 3600;

            var minutes = seconds / 60;

            seconds -= minutes * 60;

            var text = $"{hours:00}:{minutes:00}:{seconds:00}";

            setFullTime(text);

            text = Math.Round(fractalMinutes, 2, MidpointRounding.AwayFromZero).ToString(CultureInfo.CurrentCulture);

            setMiddleTime(text);

            text = Math.Round(fractalMinutes, 0, MidpointRounding.AwayFromZero).ToString();

            setshortTime(text);
        }

        private void RaisePropertyChanged(EventHandler handler) => handler?.Invoke(this, EventArgs.Empty);
    }
}