namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    internal static class MainHelper
    {
        internal static decimal CalcFractalMinutes(int seconds) => seconds / 60m;

        internal static int CalcSeconds(string text)
        {
            var split = text.Split(':');

            var seconds = int.Parse(split[0]) * 3600;

            seconds += int.Parse(split[1]) * 60;

            seconds += int.Parse(split[2]);

            return seconds;
        }
    }
}