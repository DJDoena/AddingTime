namespace DoenaSoft.DVDProfiler.AddingTime.DiscScanner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AbstractionLayer.IOServices;
    using AbstractionLayer.IOServices.Implementations;
    using ToolBox.Extensions;

    public static class Program
    {
        private static readonly IIOServices _IOServices;

        static Program()
            => _IOServices = new IOServices();

        public static void Main()
        {

#if FAKE

            IEnumerable<IDriveInfo> drives = _IOServices.GetDriveInfos(System.IO.DriveType.CDRom).Union(new[] { new FakeDrive() });

#else

            IEnumerable<IDriveInfo> drives = _IOServices.GetDriveInfos(System.IO.DriveType.CDRom);

#endif

            drives.Where(drive => drive.IsReady).ForEach(Process);

            Console.WriteLine("Press <Enter> to exit.");

            Console.ReadLine();
        }

        private static void Process(IDriveInfo drive)
        {
            try
            {
                TryProcess(drive);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                Console.WriteLine(ex.StackTrace);
            }
        }

        private static void TryProcess(IDriveInfo drive)
        {
            IDiscInfo discInfo = DiscInfoFactory.GetDiscInfo(drive, _IOServices);

            if (discInfo != null)
            {
                Process(discInfo);
            }
        }

        private static void Process(IDiscInfo discInfo)
        {
            Dictionary<String, List<ISubsetInfo>> structuredSubsets = SubsetStructurer.GetStructuredSubsets(discInfo);

            ConsolePrinter.Print(discInfo);

            Console.WriteLine();

            ConsolePrinter.Print(structuredSubsets);

            Console.WriteLine();

            Console.WriteLine("Press <Enter> to continue.");

            Console.ReadLine();
        }
    }
}