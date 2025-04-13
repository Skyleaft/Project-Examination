using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Web.Client.Services;

public static class ExcelService
{
    public static byte[] GenerateExcelWorkbook(IEnumerable<object> data)
    {
        var stream = new MemoryStream();

        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using (var package = new ExcelPackage(stream))
        {
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");

            // simple way
            //workSheet.Cells.LoadFromCollection(data.ToList(), true);

            ////// mutual
            workSheet.Row(1).Height = 20;
            workSheet.Row(1).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            workSheet.Row(1).Style.Font.Bold = true;

            var props = data.FirstOrDefault().GetType().GetProperties();
            for (var i = 0; i < props.Length; i++) workSheet.Cells[1, i + 1].Value = props[i].Name;

            for (var i = 0; i < data.Count(); i++)
            for (var j = 0; j < props.Length; j++)
                workSheet.Cells[i + 2, j + 1].Value = props[j].GetValue(data.ElementAt(i));

            workSheet.Columns.AutoFit();
            workSheet.Columns[1].Style.HorizontalAlignment = ExcelHorizontalAlignment.CenterContinuous;
            return package.GetAsByteArray();
        }
    }
}