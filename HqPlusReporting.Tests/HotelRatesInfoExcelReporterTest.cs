using NUnit.Framework;
using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;

namespace HqPlusReporting.Tests
{
    [TestFixture]
    public class HotelRatesInfoExcelReporterTest
    {
        private HotelRatesInfoExcelReporter _reporter;
        private ExcelWorksheet _worksheet;

        [SetUp]
        public void Setup()
        {
            _reporter = new HotelRatesInfoExcelReporter();
            var currentDirectory = Directory.GetCurrentDirectory();
            var inputDataDirectory = Path.Combine(Directory.GetParent(currentDirectory).Parent.Parent.FullName, "input-data");
            var filePath = Path.Combine(inputDataDirectory, "hotelrates.json");

            var path = _reporter.ReportFromFile(filePath, _ => Guid.NewGuid().ToString()).Result;

            var fileInfo = new FileInfo(path);

            var package = new ExcelPackage(fileInfo);
            _worksheet = package.Workbook.Worksheets.FirstOrDefault();
        }

        [Test]
        public void TestRowCount()
        {
            int rows = _worksheet.Dimension.Rows;

            var rowsExpected = 105;

            Assert.AreEqual(rowsExpected, rows, $"Number of rows should be '{rowsExpected}'");
        }

        [Test]
        public void TestColumnsCount()
        {
            int columns = _worksheet.Dimension.Columns;

            var colsExpected = 9;

            Assert.AreEqual(colsExpected, columns, $"Number of columns should be '{colsExpected}'");
        }

        [Test]
        public void TestFirstRow()
        {
            var arrivalDate1 = _worksheet.Cells[2, 1].Value.ToString();
            var arrivalDate1Expeted = "3/15/2016";

            Assert.AreEqual(arrivalDate1Expeted, arrivalDate1, $"Arrival date at first row should be '{arrivalDate1Expeted}'");

            var currency1 = _worksheet.Cells[2, 4].Value.ToString();
            var currency1Expected = "EUR";

            Assert.AreEqual(currency1Expected, currency1, $"Currency at first row should be '{currency1Expected}'");

            var breakfast1 = _worksheet.Cells[2, 7].Value.ToString();
            var breakfast1Expected = "Excluded";

            Assert.AreEqual(breakfast1Expected, breakfast1, $"Breakfast at first row should be '{breakfast1Expected}'");
        }

        [Test]
        public void TestRow30()
        {
            var departureDate = _worksheet.Cells[30, 2].Value.ToString();
            var departureDateExpeted = "3/17/2016";

            Assert.AreEqual(departureDateExpeted, departureDate, $"Departure date at 30th row should be '{departureDateExpeted}'");

            var rateName = _worksheet.Cells[30, 5].Value.ToString();
            var rateNameExpected = "Classic Room - Bed & Breakfast";

            Assert.AreEqual(rateNameExpected, rateName, $"Rate name at 30th row should be '{rateNameExpected}'");

            var id = _worksheet.Cells[30, 8].Value.ToString();
            var idExpected = "1965705881";

            Assert.AreEqual(idExpected, id, $"ID at 30th row should be '{idExpected}'");
        }

        [Test]
        public void TestRow100()
        {
            var price = _worksheet.Cells[100, 3].Value.ToString();
            var priceExpeted = "639";

            Assert.AreEqual(priceExpeted, price, $"Price at 100th row should be '{priceExpeted}'");

            var adults = _worksheet.Cells[100, 6].Value.ToString();
            var adultsExpected = "2";

            Assert.AreEqual(adultsExpected, adults, $"Adults at 100th row should be '{adultsExpected}'");
        }
    }
}