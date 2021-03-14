using System;
using System.IO;
using System.Linq;

namespace HqPlusReporting
{
    class Program
    {
        static void Main(string[] args)
        {
            var reporter = new HotelRatesInfoExcelReporter();

            var currentDirectory = Directory.GetCurrentDirectory();
            var inputDataDirectory = Path.Combine(Directory.GetParent(currentDirectory).Parent.Parent.FullName, "input-data");
            var filePath = Path.Combine(inputDataDirectory, "hotelrates.json");

            if (args.Any())
            {
                filePath = args[0];
            }

            if (!File.Exists(filePath))
            {
                Console.WriteLine("File not found :(");
                return;
            }

            static string outputFilePath(Hotel h) => $"{DateTime.Now:yyyy-MM-dd--hh-MM-ss}--{h.HotelId}--report";

            var result = reporter.ReportFromFile(filePath, outputFilePath).Result;

            Console.WriteLine("Export location:");
            Console.WriteLine(result);
        }
    }
}
