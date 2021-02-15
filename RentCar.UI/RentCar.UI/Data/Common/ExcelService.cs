using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using RentCar.UI.Data.Cars.Brands.Models;

namespace RentCar.UI.Data.Common
{
    public static class ExcelService
    {
        public static byte[] GenerateExcelWorkbook<T>(List<T> list)
        {
            var stream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Sheet1");

                workSheet.Cells.LoadFromCollection(list, true);

                return package.GetAsByteArray();
            }
        }
    }
}
