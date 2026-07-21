using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;

namespace backend.Models
{
    public class ExcelRow
    {
        public int Id { get; set; }
        public int RowNumber { get; set; }
        public int ExcelUploadId { get; set; }
        // foreign key to UploadExcel class
        public ExcelUpload? ExcelUpload{ get; set; }
        public List<ExcelCells> Cells { get; set; } = new List<ExcelCells>();

    }
}