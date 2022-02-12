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

        private readonly IMainDataModel _DataModel;

        private readonly IUIServices _UIServices;

        private readonly IClipboardServices _ClipboardServices;

        public MainOutputModel(IMainDataModel dataModel
            , IUIServices uiServices
            , IClipboardServices clipboardServices)
        {
            _DataModel = dataModel ?? throw new ArgumentNullException(nameof(dataModel));

            _UIServices = uiServices ?? throw new ArgumentNullException(nameof(uiServices));

            _ClipboardServices = clipboardServices ?? throw new ArgumentNullException(nameof(clipboardServices));
        }

        #region IMainOutputModel

        #region Episodes

        public void CopyEpisodes()
            => _ClipboardServices.SetText(_DataModel.EpisodesShortTime);

        public void CopyAllEpisodes()
            => _ClipboardServices.SetText(_DataModel.EpisodesFullTime + string.Empty.PadRight(Padding) + _DataModel.EpisodesShortTime);

        #endregion

        #region Discs

        public void CopyDiscs()
            => _ClipboardServices.SetText(_DataModel.DiscsShortTime);

        public void CopyAllDiscs()
            => _ClipboardServices.SetText(_DataModel.DiscsFullTime + string.Empty.PadRight(Padding) + _DataModel.DiscsShortTime);

        public void CopyFullDiscs()
        {
            if (_UIServices.ShowMessageBox("Details per disc (Yes = per disc, No = per episode)?", "Details", Buttons.YesNo, Icon.Question) == Result.Yes)
            {
                CopyFull(_DataModel.DiscsFullTime, _DataModel.DiscsShortTime, _DataModel.Discs, "Season: ", '-');
            }
            else
            {
                var discEpisodes = _DataModel.DiscEpisodes.Select(de => de.EpisodeRunningTimes);

                var discs = _DataModel.DiscEpisodes.Select(de => de.DiscRunningTime);

                CopyFullDetails(_DataModel.DiscsFullTime, _DataModel.DiscsShortTime, discEpisodes, discs, "Season: ", "Disc:   ");
            }
        }

        #endregion


        #region Seasons

        public void CopySeasons()
            => _ClipboardServices.SetText(_DataModel.SeasonsShortTime);

        public void CopyAllSeasons()
            => _ClipboardServices.SetText(_DataModel.SeasonsFullTime + string.Empty.PadRight(Padding) + _DataModel.SeasonsShortTime);

        public void CopyFullSeasons()
        {
            if (_UIServices.ShowMessageBox("Details per season (Yes = per season, No = per disc/per episode)?", "Details", Buttons.YesNo, Icon.Question) == Result.Yes)
            {
                CopyFull(_DataModel.SeasonsFullTime, _DataModel.SeasonsShortTime, _DataModel.Seasons, "Series: ", '=');
            }
            else if (_UIServices.ShowMessageBox("Details per season (Yes = per disc, No = per episode)?", "Details", Buttons.YesNo, Icon.Question) == Result.Yes)
            {
                var seasonDiscs = _DataModel.SeasonDiscs.Select(sd => sd.DiscRunningTimes.Select(drt => drt.DiscRunningTime));

                CopyFullDetails(_DataModel.SeasonsFullTime, _DataModel.SeasonsShortTime, seasonDiscs, _DataModel.Seasons, "Series: ", "Season: ");
            }
            else
            {
                CopyExtendedDetails(_DataModel.SeasonsFullTime, _DataModel.SeasonsShortTime, _DataModel.SeasonDiscs);
            }
        }

        #endregion

        #endregion

        private void CopyFull(string fullTime, string shortTime, IEnumerable<string> entries, string summaryPrefix, char lineSymbol)
        {
            var discsFullTimeLength = fullTime?.Length ?? 0;

            var discsShortTimeLength = shortTime?.Length ?? 0;

            var sb = new StringBuilder();

            entries.ForEach(entry => PrintEntry(entry, sb, summaryPrefix.Length, discsShortTimeLength));

            var length = discsFullTimeLength + Padding + discsShortTimeLength + summaryPrefix.Length;

            DrawLine(sb, length, lineSymbol);

            sb.Append(summaryPrefix);
            sb.Append(fullTime);
            sb.Append(string.Empty.PadRight(Padding));
            sb.AppendLine(shortTime);

            DrawLine(sb, length, lineSymbol);

            _ClipboardServices.SetText(sb.ToString().TrimEnd());
        }

        private void CopyFullDetails(string fullTime, string shortTime, IEnumerable<IEnumerable<string>> subEntries, IEnumerable<string> mainEntries, string superPrefix, string mainPrefix)
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

            _ClipboardServices.SetText(outputBuilder.ToString().TrimEnd());
        }

        private static int GetLineLength(string fullTime, string shortTime)
        {
            var fullTimeLength = fullTime?.Length ?? 0;

            var shortTimeLength = shortTime?.Length ?? 0;

            var length = fullTimeLength + Padding + shortTimeLength;

            return length;
        }

        private static void PrintEntries(IEnumerable<IEnumerable<string>> subEntries, IEnumerable<string> mainEntries, StringBuilder outputBuilder, int length, char lineSymbol, string mainPrefix, int maxMinutesLength)
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

        private void CopyExtendedDetails(string fullTime, string shortTime, IEnumerable<SeasonDiscEpisodes> seasonDiscs)
        {
            var outputBuilder = new StringBuilder();

            var superPrefix = "Series: ";

            var prefixLength = superPrefix.Length;

            var length = GetLineLength(fullTime, shortTime) + prefixLength;

            seasonDiscs.ForEach(sd =>
            {
                var discEpisodes = sd.DiscRunningTimes.Select(d => d.EpisodeRunningTimes);

                var discRunningTimes = sd.DiscRunningTimes.Select(d => d.DiscRunningTime);

                PrintEntries(discEpisodes, discRunningTimes, outputBuilder, length, '~', "Disc:   ", shortTime.Length);

                DrawLine(outputBuilder, length, '-');

                var minutesText = GetMinutesText(sd.SeasonRunningTime);

                outputBuilder.Append("Season: ");
                outputBuilder.Append(sd.SeasonRunningTime);
                outputBuilder.Append(string.Empty.PadRight(Padding));
                outputBuilder.AppendLine(minutesText);

                DrawLine(outputBuilder, length, '-');
            });

            DrawLine(outputBuilder, length, '=');

            outputBuilder.Append(superPrefix);
            outputBuilder.Append(fullTime);
            outputBuilder.Append(string.Empty.PadRight(Padding));
            outputBuilder.AppendLine(shortTime);

            DrawLine(outputBuilder, length, '=');

            _ClipboardServices.SetText(outputBuilder.ToString().TrimEnd());
        }

        private static void PrintEntry(string entry, StringBuilder outputBuilder, int prefixLength, int maxMinuteLength)
        {
            var minutesText = GetMinutesText(entry);

            outputBuilder.Append(string.Empty.PadRight(prefixLength));
            outputBuilder.Append(entry);
            outputBuilder.Append(string.Empty.PadRight(Padding));
            outputBuilder.AppendLine(minutesText.PadLeft(maxMinuteLength));
        }

        private static string GetMinutesText(string entry)
        {
            var seconds = MainHelper.CalcSeconds(entry);

            var fractalMinutes = MainHelper.CalcFractalMinutes(seconds);

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