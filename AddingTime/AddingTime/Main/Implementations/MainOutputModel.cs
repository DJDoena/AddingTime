using System;
using System.Collections.Generic;
using System.Text;
using DoenaSoft.AbstractionLayer.UIServices;

namespace DoenaSoft.DVDProfiler.AddingTime.Main.Implementations
{
    internal sealed class MainOutputModel : IMainOutputModel
    {
        private readonly IMainDataModel DataModel;

        private readonly IClipboardServices ClipboardServices;

        public MainOutputModel(IMainDataModel dataModel
            , IClipboardServices clipboardServices)
        {
            if (dataModel == null)
            {
                throw (new ArgumentNullException(nameof(dataModel)));
            }

            if (clipboardServices == null)
            {
                throw (new ArgumentNullException(nameof(clipboardServices)));
            }

            DataModel = dataModel;
            ClipboardServices = clipboardServices;
        }

        #region IMainOutputModel

        #region Episodes

        public void CopyEpisodes()
        {
            ClipboardServices.SetText(DataModel.EpisodesShortTime);
        }

        public void CopyAllEpisodes()
        {
            ClipboardServices.SetText(DataModel.EpisodesFullTime + "\t" + DataModel.EpisodesShortTime);
        }

        #endregion

        #region Discs

        public void CopyDiscs()
        {
            ClipboardServices.SetText(DataModel.DiscsShortTime);
        }

        public void CopyAllDiscs()
        {
            ClipboardServices.SetText(DataModel.DiscsFullTime + "\t" + DataModel.DiscsShortTime);
        }

        public void CopyFullDiscs()
        {
            CopyFull(DataModel.DiscsFullTime, DataModel.DiscsShortTime, DataModel.Discs);
        }

        #endregion


        #region Seasons

        public void CopySeasons()
        {
            ClipboardServices.SetText(DataModel.SeasonsShortTime);
        }

        public void CopyAllSeasons()
        {
            ClipboardServices.SetText(DataModel.SeasonsFullTime + "\t" + DataModel.SeasonsShortTime);
        }

        public void CopyFullSeasons()
        {
            CopyFull(DataModel.SeasonsFullTime, DataModel.SeasonsShortTime, DataModel.Seasons);
        }

        #endregion

        #endregion

        private void CopyFull(String fullTime
            , String shortTime
            , IEnumerable<String> entries)
        {
            StringBuilder sb = new StringBuilder();

            foreach (String entry in entries)
            {
                Int32 seconds = MainHelper.CalcSeconds(entry);

                Decimal fractalMinutes = MainHelper.CalcFractalMinutes(seconds);

                fractalMinutes = Math.Round(fractalMinutes, 0, MidpointRounding.AwayFromZero);

                sb.Append(entry);
                sb.Append("\t");
                sb.AppendLine(fractalMinutes.ToString());
            }

            Int32 discsFullTimeLength = fullTime?.Length ?? 0;

            Int32 discsShortTimeLength = shortTime?.Length ?? 0;

            Int32 length = discsFullTimeLength + 8 + discsShortTimeLength;

            DrawLine(sb, length);

            sb.Append(fullTime);
            sb.Append("\t");
            sb.AppendLine(shortTime);

            DrawLine(sb, length);

            ClipboardServices.SetText(sb.ToString());
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