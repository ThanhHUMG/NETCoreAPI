using System.Data;
using System.IO;
using OfficeOpenXml;

namespace MVC.Models.Process
{
    public class ExcelProcess
    {
        /// <summary>
        /// Chuyển file Excel (.xls hoặc .xlsx) thành DataTable
        /// </summary>
        /// <param name="filePath">Đường dẫn file Excel</param>
        /// <returns>DataTable chứa dữ liệu</returns>
        public DataTable ExcelToDataTable(string filePath)
        {
            var dt = new DataTable();

            FileInfo existingFile = new FileInfo(filePath);
            using (var package = new ExcelPackage(existingFile))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0]; // sheet đầu tiên
                if (worksheet == null)
                    throw new Exception("Không tìm thấy worksheet nào trong file Excel!");

                // Giả sử dòng đầu tiên là header
                bool hasHeader = true;

                // Tạo các cột
                foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    dt.Columns.Add(hasHeader ? firstRowCell.Text : $"Column {firstRowCell.Start.Column}");
                }

                // Lấy dữ liệu từ các dòng còn lại
                int startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                {
                    var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                    var row = dt.NewRow();
                    int i = 0;
                    foreach (var cell in wsRow)
                        row[i++] = cell.Text;
                    dt.Rows.Add(row);
                }
            }

            return dt;
        }
    }
}
