using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Dtos.Excel
{
    public class ExcelUploadResultDto
    {
        public int Id { get; set; }
        public string FileName { get; set; } = string.Empty;
        public DateTime UploadedOn { get; set; }
        public int TotalRows { get; set; }
        public List<Dictionary<string, string >> Data { get; set; } = new List<Dictionary<string, string>>();
    }
}