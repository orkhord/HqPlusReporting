using System;
using System.IO;
using System.Threading.Tasks;

namespace HqPlusReporting
{
    public interface IHotelRatesInfoReporter
    {
        Task<string> ReportFromFile(string path, Func<Hotel, string> exportFilePath = null);
        Task<string> Report(FileStream fs, Func<Hotel, string> exportFilePath = null);
        Task<string> Report(string json, Func<Hotel, string> exportFilePath = null);
        Task<string> Report(HotelRatesInfo data, Func<Hotel, string> exportFilePath = null);
    }
}
