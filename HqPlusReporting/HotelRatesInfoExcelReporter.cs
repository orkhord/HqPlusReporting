using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HqPlusReporting
{
    public class HotelRatesInfoExcelReporter : IHotelRatesInfoReporter
    {
        public HotelRatesInfoExcelReporter()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        }

        public async Task<string> ReportFromFile(string path, Func<Hotel, string> exportFilePath = null)
        {
            using FileStream fs = File.OpenRead(path);
            return await Report(fs, exportFilePath);
        }

        public async Task<string> Report(FileStream fs, Func<Hotel, string> exportFilePath = null)
        {
            using var sr = new StreamReader(fs, Encoding.UTF8);
            var json = await sr.ReadToEndAsync();
            return await Report(json, exportFilePath);
        }

        public async Task<string> Report(string json, Func<Hotel, string> exportFilePath = null)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<HotelRatesInfo>(json, options);
            return await Report(data, exportFilePath);
        }

        public async Task<string> Report(HotelRatesInfo data, Func<Hotel, string> exportFilePath = null)
        {
            using var package = new ExcelPackage();

            var sheet = package.Workbook.Worksheets.Add("Hotel rates");

            sheet.Cells[1, 1].Value = "Arrival date";
            sheet.Cells[1, 2].Value = "Departure date";
            sheet.Cells[1, 3].Value = "Price";
            sheet.Cells[1, 4].Value = "Currency";
            sheet.Cells[1, 5].Value = "Rate name";
            sheet.Cells[1, 6].Value = "Adults";
            sheet.Cells[1, 7].Value = "Breakfast";
            sheet.Cells[1, 8].Value = "ID";
            sheet.Cells[1, 9].Value = "Description";

            sheet.Cells[2, 1].LoadFromCollection(data.HotelRates.Select(hr => new HotelRateRow(hr)));

            var path = $"{DateTime.Now:yyyy-MM-dd--hh-MM-ss}--{data.Hotel.HotelId}--report";

            if (exportFilePath != null)
            {
                path = exportFilePath(data.Hotel);
            }
            if (!path.EndsWith(".xlsx")) path += ".xlsx";

            var fileInfo = new FileInfo(path);

            await package.SaveAsAsync(fileInfo);
            return fileInfo.FullName;
        }
    }
}
