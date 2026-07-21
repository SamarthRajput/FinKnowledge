using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class ExcelCells
    {
        public int Id { get; set; }
        public string ColumnName { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public int ExcelRowId { get; set; }
        public ExcelRow? ExcelRow { get; set; }
    }
}