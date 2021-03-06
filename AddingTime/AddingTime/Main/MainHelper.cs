﻿namespace DoenaSoft.DVDProfiler.AddingTime.Main
{
    using System;

    internal static class MainHelper
    {
        internal static Decimal CalcFractalMinutes(Int32 seconds)
            => seconds / 60m;

        internal static Int32 CalcSeconds(String text)
        {
            String[] split = text.Split(':');

            Int32 seconds = Int32.Parse(split[0]) * 3600;

            seconds += Int32.Parse(split[1]) * 60;

            seconds += Int32.Parse(split[2]);

            return (seconds);
        }
    }
}