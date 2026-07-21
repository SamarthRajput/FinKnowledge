using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class ExcelUpload
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DateTime UploadedOn {get; set; } = DateTime.UtcNow;

        public List<ExcelRow> Rows { get; set; } = new List<ExcelRow>();
    }
}