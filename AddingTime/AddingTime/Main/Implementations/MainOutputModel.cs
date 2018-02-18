namespace DoenaSoft.DVDProfiler.AddingTime.Main.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AbstractionLayer.UIServices;
    using ToolBox.Extensions;

    internal sealed class MainOutputModel : IMainOutputModel
    {
        private readonly IMainDataModel _DataModel;

        private readonly IClipboardServices _ClipboardServices;

        public MainOutputModel(IMainDataModel dataModel
            , IClipboardServices clipboardServices)
        {
            _DataModel = dataModel ?? throw new ArgumentNullException(nameof(dataModel));

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
            => CopyFull(_DataModel.DiscsFullTime, _DataModel.DiscsShortTime, _DataModel.Discs);

        #endregion


        #region Seasons

        public void CopySeasons()
            => _ClipboardServices.SetText(_DataModel.SeasonsShortTime);

        public void CopyAllSeasons()
            => _ClipboardServices.SetText(_DataModel.SeasonsFullTime + "\t" + _DataModel.SeasonsShortTime);

        public void CopyFullSeasons()
            => CopyFull(_DataModel.SeasonsFullTime, _DataModel.SeasonsShortTime, _DataModel.Seasons);

        #endregion

        #endregion

        private void CopyFull(String fullTime
            , String shortTime
            , IEnumerable<String> entries)
        {
            StringBuilder sb = new StringBuilder();

            entries.ForEach(entry => PrintEntry(entry, sb));

            Int32 discsFullTimeLength = fullTime?.Length ?? 0;

            Int32 discsShortTimeLength = shortTime?.Length ?? 0;

            Int32 length = discsFullTimeLength + 8 + discsShortTimeLength;

            DrawLine(sb, length);

            sb.Append(fullTime);
            sb.Append("\t");
            sb.AppendLine(shortTime);

            DrawLine(sb, length);

            _ClipboardServices.SetText(sb.ToString());
        }

        private static void PrintEntry(String entry
            , StringBuilder sb)
        {
            Int32 seconds = MainHelper.CalcSeconds(entry);

            Decimal fractalMinutes = MainHelper.CalcFractalMinutes(seconds);

            fractalMinutes = Math.Round(fractalMinutes, 0, MidpointRounding.AwayFromZero);

            sb.Append(entry);
            sb.Append("\t");
            sb.AppendLine(fractalMinutes.ToString());
        }

        private static void DrawLine(StringBuilder sb
            , Int32 length)
        {
            for (Int32 i = 0; i < length; i++)
            {
                sb.Append("-");
            }

            sb.AppendLine();
        }
    }
}