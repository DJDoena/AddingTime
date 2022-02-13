namespace DoenaSoft.DVDProfiler.AddingTime.Main.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using AbstractionLayer.UIServices;
    using ToolBox.Extensions;

    internal sealed class MainOutputModel : IMainOutputModel
    {
        private const int Padding = 3;

        private readonly IMainDataModel _dataModel;

        private readonly IUIServices _uiIServices;

        private readonly IClipboardServices _clipboardServices;

        public MainOutputModel(IMainDataModel dataModel, IUIServices uiServices, IClipboardServices clipboardServices)
        {
            _dataModel = dataModel ?? throw new ArgumentNullException(nameof(dataModel));

            _uiIServices = uiServices ?? throw new ArgumentNullException(nameof(uiServices));

            _clipboardServices = clipboardServices ?? throw new ArgumentNullException(nameof(clipboardServices));
        }

        #region IMainOutputModel

        #region Episodes

        public void CopyEpisodes() => _clipboardServices.SetText(_dataModel.EpisodesShortTime);

        public void CopyAllEpisodes() => _clipboardServices.SetText(_dataModel.EpisodesFullTime + string.Empty.PadRight(Padding) + _dataModel.EpisodesShortTime);

        #endregion

        #region Discs

        public void CopyDiscs() => _clipboardServices.SetText(_dataModel.DiscsShortTime);

        public void CopyAllDiscs() => _clipboardServices.SetText(_dataModel.DiscsFullTime + string.Empty.PadRight(Padding) + _dataModel.DiscsShortTime);

        public void CopyFullDiscs()
        {
            if (_uiIServices.ShowMessageBox("Details per disc (Yes = per disc, No = per episode)?", "Details", Buttons.YesNo, Icon.Question) == Result.Yes)
            {
                this.CopyFull(_dataModel.DiscsFullTime, _dataModel.DiscsShortTime, _dataModel.Discs, "Season: ", '-');
            }
            else
            {
                var discRunningTimes = _dataModel.Discs;

                var episodeRunningTimes = discRunningTimes.Select(d => d.EpisodeRunningTimes);

                this.CopyFullDetails(_dataModel.DiscsFullTime, _dataModel.DiscsShortTime, episodeRunningTimes, discRunningTimes, "Season: ", "Disc:   ");
            }
        }

        #endregion


        #region Seasons

        public void CopySeasons() => _clipboardServices.SetText(_dataModel.SeasonsShortTime);

        public void CopyAllSeasons() => _clipboardServices.SetText(_dataModel.SeasonsFullTime + string.Empty.PadRight(Padding) + _dataModel.SeasonsShortTime);

        public void CopyFullSeasons()
        {
            if (_uiIServices.ShowMessageBox("Details per season (Yes = per season, No = per disc/per episode)?", "Details", Buttons.YesNo, Icon.Question) == Result.Yes)
            {
                this.CopyFull(_dataModel.SeasonsFullTime, _dataModel.SeasonsShortTime, _dataModel.Seasons, "Series: ", '=');
            }
            else if (_uiIServices.ShowMessageBox("Details per season (Yes = per disc, No = per episode)?", "Details", Buttons.YesNo, Icon.Question) == Result.Yes)
            {
                var seasonRunningTimes = _dataModel.Seasons.Select(s => s.DiscRunningTimes);

                this.CopyFullDetails(_dataModel.SeasonsFullTime, _dataModel.SeasonsShortTime, seasonRunningTimes, _dataModel.Seasons, "Series: ", "Season: ");
            }
            else
            {
                this.CopyExtendedDetails(_dataModel.SeasonsFullTime, _dataModel.SeasonsShortTime, _dataModel.Seasons);
            }
        }

        #endregion

        #endregion

        private void CopyFull(string fullTime, string shortTime, IEnumerable<RunningTimeBase> entries, string summaryPrefix, char lineSymbol)
        {
            var outputBuilder = new StringBuilder();

            var discsFullTimeLength = fullTime?.Length ?? 0;

            var discsShortTimeLength = shortTime?.Length ?? 0;

            entries.ForEach(entry => PrintEntry(entry, outputBuilder, summaryPrefix.Length, discsShortTimeLength));

            var length = discsFullTimeLength + Padding + discsShortTimeLength + summaryPrefix.Length;

            DrawLine(outputBuilder, length, lineSymbol);

            outputBuilder.Append(summaryPrefix);
            outputBuilder.Append(fullTime);
            outputBuilder.Append(string.Empty.PadRight(Padding));
            outputBuilder.AppendLine(shortTime);

            DrawLine(outputBuilder, length, lineSymbol);

            _clipboardServices.SetText(outputBuilder.ToString().TrimEnd());
        }

        private void CopyFullDetails(string fullTime, string shortTime, IEnumerable<IEnumerable<RunningTimeBase>> subEntries, IEnumerable<RunningTimeBase> mainEntries, string superPrefix, string mainPrefix)
        {
            var outputBuilder = new StringBuilder();

            var length = GetLineLength(fullTime, shortTime) + superPrefix.Length;

            PrintEntries(subEntries, mainEntries, outputBuilder, length, '-', mainPrefix, shortTime.Length);

            DrawLine(outputBuilder, length, '=');

            outputBuilder.Append(superPrefix);
            outputBuilder.Append(fullTime);
            outputBuilder.Append(string.Empty.PadRight(Padding));
            outputBuilder.AppendLine(shortTime);

            DrawLine(outputBuilder, length, '=');

            _clipboardServices.SetText(outputBuilder.ToString().TrimEnd());
        }

        private static int GetLineLength(string fullTime, string shortTime)
        {
            var fullTimeLength = fullTime?.Length ?? 0;

            var shortTimeLength = shortTime?.Length ?? 0;

            var length = fullTimeLength + Padding + shortTimeLength;

            return length;
        }

        private static void PrintEntries(IEnumerable<IEnumerable<RunningTimeBase>> subEntries, IEnumerable<RunningTimeBase> mainEntries, StringBuilder outputBuilder, int length, char lineSymbol, string mainPrefix, int maxMinutesLength)
        {
            var subEntryList = subEntries.ToList();

            var mainEntryList = mainEntries.ToList();

            for (var subEntryIndex = 0; subEntryIndex < subEntryList.Count; subEntryIndex++)
            {
                var subEntry = subEntryList[subEntryIndex];

                subEntry.ForEach(entry => PrintEntry(entry, outputBuilder, mainPrefix.Length, maxMinutesLength));

                DrawLine(outputBuilder, length, lineSymbol);

                if (mainEntryList != null)
                {
                    outputBuilder.Append(mainPrefix);

                    PrintEntry(mainEntryList[subEntryIndex], outputBuilder, 0, maxMinutesLength);

                    DrawLine(outputBuilder, length, lineSymbol);
                }
            }
        }

        private void CopyExtendedDetails(string fullTime, string shortTime, IEnumerable<SeasonRunningTime> seasonRunningTimes)
        {
            var outputBuilder = new StringBuilder();

            var superPrefix = "Series: ";

            var prefixLength = superPrefix.Length;

            var length = GetLineLength(fullTime, shortTime) + prefixLength;

            seasonRunningTimes.ForEach(s =>
            {
                var discRunningTimes = s.DiscRunningTimes;

                var episodeRunningTimes = discRunningTimes.Select(d => d.EpisodeRunningTimes);

                PrintEntries(episodeRunningTimes, discRunningTimes, outputBuilder, length, '~', "Disc:   ", shortTime.Length);

                DrawLine(outputBuilder, length, '-');

                var minutesText = GetMinutesText(s.RunningTime);

                outputBuilder.Append("Season: ");
                outputBuilder.Append(s.RunningTimeText);
                outputBuilder.Append(string.Empty.PadRight(Padding));
                outputBuilder.AppendLine(minutesText.PadLeft(shortTime.Length));

                DrawLine(outputBuilder, length, '-');
            });

            DrawLine(outputBuilder, length, '=');

            outputBuilder.Append(superPrefix);
            outputBuilder.Append(fullTime);
            outputBuilder.Append(string.Empty.PadRight(Padding));
            outputBuilder.AppendLine(shortTime);

            DrawLine(outputBuilder, length, '=');

            _clipboardServices.SetText(outputBuilder.ToString().TrimEnd());
        }

        private static void PrintEntry(RunningTimeBase entry, StringBuilder outputBuilder, int prefixLength, int maxMinuteLength)
        {
            var minutesText = GetMinutesText(entry.RunningTime);

            outputBuilder.Append(string.Empty.PadRight(prefixLength));
            outputBuilder.Append(entry.RunningTimeText);
            outputBuilder.Append(string.Empty.PadRight(Padding));
            outputBuilder.AppendLine(minutesText.PadLeft(maxMinuteLength));
        }

        private static string GetMinutesText(int entry)
        {
            var fractalMinutes = MainHelper.CalcFractalMinutes(entry);

            fractalMinutes = Math.Round(fractalMinutes, 0, MidpointRounding.AwayFromZero);

            var minutesText = fractalMinutes.ToString();

            return minutesText;
        }

        private static void DrawLine(StringBuilder outputBuilder, int length, char symbol)
        {
            outputBuilder.AppendLine(string.Empty.PadRight(length, symbol));
        }
    }
}