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
            => _ClipboardServices.SetText(_DataModel.EpisodesFullTime + "\t" + _DataModel.EpisodesShortTime);

        #endregion

        #region Discs

        public void CopyDiscs()
            => _ClipboardServices.SetText(_DataModel.DiscsShortTime);

        public void CopyAllDiscs()
            => _ClipboardServices.SetText(_DataModel.DiscsFullTime + "\t" + _DataModel.DiscsShortTime);

        public void CopyFullDiscs()
        {
            if (_UIServices.ShowMessageBox("Details per disc (Yes = per disc, No = per episode)?", "Details", Buttons.YesNo, Icon.Question) == Result.Yes)
            {
                CopyFull(_DataModel.DiscsFullTime, _DataModel.DiscsShortTime, _DataModel.Discs);
            }
            else
            {
                CopyFullDetails(_DataModel.DiscsFullTime, _DataModel.DiscsShortTime, _DataModel.DiscEpisodes, null);
            }
        }

        #endregion


        #region Seasons

        public void CopySeasons()
            => _ClipboardServices.SetText(_DataModel.SeasonsShortTime);

        public void CopyAllSeasons()
            => _ClipboardServices.SetText(_DataModel.SeasonsFullTime + "\t" + _DataModel.SeasonsShortTime);

        public void CopyFullSeasons()
        {
            if (_UIServices.ShowMessageBox("Details per season (Yes = per season, No = per disc)?", "Details", Buttons.YesNo, Icon.Question) == Result.Yes)
            {
                CopyFull(_DataModel.SeasonsFullTime, _DataModel.SeasonsShortTime, _DataModel.Seasons);
            }
            else
            {
                CopyFullDetails(_DataModel.SeasonsFullTime, _DataModel.SeasonsShortTime, _DataModel.SeasonDiscs, _DataModel.Seasons);
            }
        }

        #endregion

        #endregion

        private void CopyFull(string fullTime, string shortTime, IEnumerable<string> entries)
        {
            var sb = new StringBuilder();

            entries.ForEach(entry => PrintEntry(entry, sb));

            var discsFullTimeLength = fullTime?.Length ?? 0;

            var discsShortTimeLength = shortTime?.Length ?? 0;

            var length = discsFullTimeLength + 8 + discsShortTimeLength;

            DrawLine(sb, length);

            sb.Append(fullTime);
            sb.Append("\t");
            sb.AppendLine(shortTime);

            DrawLine(sb, length);

            _ClipboardServices.SetText(sb.ToString().Trim());
        }

        private void CopyFullDetails(string fullTime, string shortTime, IEnumerable<IEnumerable<string>> entryGroups, IEnumerable<string> entries)
        {
            var sb = new StringBuilder();

            var fullTimeLength = fullTime?.Length ?? 0;

            var shortTimeLength = shortTime?.Length ?? 0;

            var length = fullTimeLength + 8 + shortTimeLength;

            var entryGroupList = entryGroups.ToList();

            var entryList = entries?.ToList();

            for (int index = 0; index < entryGroupList.Count; index++)
            {
                var entryGroup = entryGroupList[index];

                entryGroup.ForEach(entry => PrintEntry(entry, sb));

                DrawLine(sb, length);

                if (entryList != null)
                {
                    PrintEntry(entryList[index], sb);

                    DrawLine(sb, length);
                }
            }

            DrawLine(sb, length, '=');

            sb.Append(fullTime);
            sb.Append("\t");
            sb.AppendLine(shortTime);

            DrawLine(sb, length, '=');

            _ClipboardServices.SetText(sb.ToString().Trim());
        }

        private static void PrintEntry(string entry, StringBuilder sb)
        {
            var seconds = MainHelper.CalcSeconds(entry);

            var fractalMinutes = MainHelper.CalcFractalMinutes(seconds);

            fractalMinutes = Math.Round(fractalMinutes, 0, MidpointRounding.AwayFromZero);

            sb.Append(entry);
            sb.Append("\t");
            sb.AppendLine(fractalMinutes.ToString());
        }

        private static void DrawLine(StringBuilder sb, int length, char symbol = '-')
        {
            for (var index = 0; index < length; index++)
            {
                sb.Append(symbol);
            }

            sb.AppendLine();
        }
    }
}