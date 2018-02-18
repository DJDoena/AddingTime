namespace DoenaSoft.DVDProfiler.AddingTime.Main.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using AbstractionLayer.UIServices;

    internal sealed class MainDataModel : IMainDataModel
    {
        private readonly IUIServices _UIServices;

        private List<String> _Episodes;

        private List<String> _Discs;

        private List<String> _Seasons;

        public MainDataModel(IUIServices uiServices)
        {
            _UIServices = uiServices ?? throw new ArgumentNullException(nameof(uiServices));

            _Episodes = new List<String>(10);

            _Discs = new List<String>(6);

            _Seasons = new List<String>(5);
        }

        #region IMainModel

        #region Episodes

        public IEnumerable<String> Episodes
            => _Episodes;

        public event EventHandler EpisodesChanged;

        public String EpisodesFullTime { get; private set; }

        public event EventHandler EpisodesFullTimeChanged;

        public String EpisodesMiddleTime { get; private set; }

        public event EventHandler EpisodesMiddleTimeChanged;

        public String EpisodesShortTime { get; private set; }

        public event EventHandler EpisodesShortTimeChanged;

        public Boolean AddEpisode(String input)
        {
            if (FormatInput(input, out String text))
            {
                _Episodes.Add(text);

                OnEpisodesChanged();

                return (true);
            }

            return (false);
        }

        public void ClearEpisodes()
        {
            _Episodes.Clear();

            OnEpisodesChanged();
        }

        public void RemoveEpisode(Int32 index)
        {
            if (index >= _Episodes.Count)
            {
                throw (new ArgumentException("Invalid index", nameof(index)));
            }

            _Episodes.RemoveAt(index);

            OnEpisodesChanged();
        }

        #endregion

        #region Discs

        public IEnumerable<String> Discs
            => _Discs;

        public event EventHandler DiscsChanged;

        public String DiscsFullTime { get; private set; }

        public event EventHandler DiscsFullTimeChanged;

        public String DiscsMiddleTime { get; private set; }

        public event EventHandler DiscsMiddleTimeChanged;

        public String DiscsShortTime { get; private set; }

        public event EventHandler DiscsShortTimeChanged;

        public void AddDisc(String input)
        {
            if (FormatInput(input, out String text))
            {
                _Discs.Add(text);

                OnDiscsChanged();
            }
        }

        public void RemoveDisc(Int32 index)
        {
            if (index >= _Discs.Count)
            {
                throw (new ArgumentException("Invalid index", nameof(index)));
            }

            _Discs.RemoveAt(index);

            OnDiscsChanged();
        }

        public void ClearDiscs()
        {
            _Discs.Clear();

            OnDiscsChanged();
        }

        #endregion

        #region Seasons

        public IEnumerable<String> Seasons
            => _Seasons;

        public event EventHandler SeasonsChanged;

        public String SeasonsFullTime { get; private set; }

        public event EventHandler SeasonsFullTimeChanged;

        public String SeasonsMiddleTime { get; private set; }

        public event EventHandler SeasonsMiddleTimeChanged;

        public String SeasonsShortTime { get; private set; }

        public event EventHandler SeasonsShortTimeChanged;

        public void AddSeason(String input)
        {
            if (FormatInput(input, out String text))
            {
                _Seasons.Add(text);

                OnSeasonsChanged();
            }
        }

        public void RemoveSeason(Int32 index)
        {
            if (index >= _Seasons.Count)
            {
                throw (new ArgumentException("Invalid index", nameof(index)));
            }

            _Seasons.RemoveAt(index);

            OnSeasonsChanged();
        }

        public void ClearSeasons()
        {
            _Seasons.Clear();

            OnSeasonsChanged();
        }

        #endregion

        #endregion

        #region Episodes

        private Boolean FormatInput(String input
            , out String text)
        {
            text = input.Replace(".", ":").Replace(",", ":");

            String[] split = text.Split(':');

            if ((split.Length != 2) && (split.Length != 3))
            {
                _UIServices.ShowMessageBox("Invalid Time Format!", "Error", Buttons.OK, Icon.Warning);

                return (false);
            }

            text = String.Empty;

            foreach (String part in split)
            {
                if (Int32.TryParse(part, out Int32 temp) == false)
                {
                    _UIServices.ShowMessageBox($"Not a Number: {part}", "Error", Buttons.OK, Icon.Warning);

                    return (false);
                }

                if (temp > 59)
                {
                    _UIServices.ShowMessageBox($"Invalid Time Part: {part}", "Error", Buttons.OK, Icon.Warning);

                    return (false);
                }

                text += temp.ToString("00:");
            }

            text = text.Substring(0, text.Length - 1);

            if (split.Length == 2)
            {
                text = "00:" + text;
            }

            return (true);
        }

        private void OnEpisodesChanged()
        {
            Calc(_Episodes, SetEpisodesFullTime, SetEpisodesMiddleTime, SetEpisodesShortTime);

            EpisodesChanged?.Invoke(this, EventArgs.Empty);
        }

        private void SetEpisodesFullTime(String text)
        {
            EpisodesFullTime = text;

            RaisePropertyChanged(EpisodesFullTimeChanged);
        }

        private void SetEpisodesMiddleTime(String text)
        {
            EpisodesMiddleTime = text;

            RaisePropertyChanged(EpisodesMiddleTimeChanged);
        }

        private void SetEpisodesShortTime(String text)
        {
            EpisodesShortTime = text;

            RaisePropertyChanged(EpisodesShortTimeChanged);
        }

        #endregion

        #region Discs    

        private void OnDiscsChanged()
        {
            Calc(_Discs, SetDiscsFullTime, SetDiscsMiddleTime, SetDiscsShortTime);

            DiscsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void SetDiscsFullTime(String text)
        {
            DiscsFullTime = text;

            RaisePropertyChanged(DiscsFullTimeChanged);
        }

        private void SetDiscsMiddleTime(String text)
        {
            DiscsMiddleTime = text;

            RaisePropertyChanged(DiscsMiddleTimeChanged);
        }

        private void SetDiscsShortTime(String text)
        {
            DiscsShortTime = text;

            RaisePropertyChanged(DiscsShortTimeChanged);
        }

        #endregion

        #region Seasons    

        private void OnSeasonsChanged()
        {
            Calc(_Seasons, SetSeasonsFullTime, SetSeasonsMiddleTime, SetSeasonsShortTime);

            SeasonsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void SetSeasonsFullTime(String text)
        {
            SeasonsFullTime = text;

            RaisePropertyChanged(SeasonsFullTimeChanged);
        }

        private void SetSeasonsMiddleTime(String text)
        {
            SeasonsMiddleTime = text;

            RaisePropertyChanged(SeasonsMiddleTimeChanged);
        }

        private void SetSeasonsShortTime(String text)
        {
            SeasonsShortTime = text;

            RaisePropertyChanged(SeasonsShortTimeChanged);
        }

        #endregion

        private static void Calc(List<String> entries
            , Action<String> setFullTime
            , Action<String> setMiddleTime
            , Action<String> setshortTime)
        {
            if (entries.Count == 0)
            {
                setFullTime(null);

                setMiddleTime(null);

                setshortTime(null);

                return;
            }

            Int32 seconds = 0;

            entries.ForEach(entry => seconds += MainHelper.CalcSeconds(entry));

            Decimal fractalMinutes = MainHelper.CalcFractalMinutes(seconds);

            Int32 hours = seconds / 3600;

            seconds -= hours * 3600;

            Int32 minutes = seconds / 60;

            seconds -= minutes * 60;

            String text = $"{hours:00}:{minutes:00}:{seconds:00}";

            setFullTime(text);

            text = Math.Round(fractalMinutes, 2, MidpointRounding.AwayFromZero).ToString(CultureInfo.CurrentCulture);

            setMiddleTime(text);

            text = Math.Round(fractalMinutes, 0, MidpointRounding.AwayFromZero).ToString();

            setshortTime(text);
        }

        private void RaisePropertyChanged(EventHandler handler)
            => handler?.Invoke(this, EventArgs.Empty);
    }
}