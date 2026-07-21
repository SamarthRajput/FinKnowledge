using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Excel;
using backend.Models;

namespace backend.Mappers
{
    public static class ExcelMappers
    {
        public static ExcelUploadResultDto ToUploadResultDto( this ExcelUpload upload)
        {
            return new ExcelUploadResultDto
            {
                Id = upload.Id,
                FileName = upload.FileName,
                UploadedOn = upload.UploadedOn,
                TotalRows = upload.Rows.Count,
                Data = upload.Rows
                    .OrderBy(r => r.RowNumber)
                    .Select(r => r.Cells.ToDictionary(c => c.ColumnName, c => c.Value))
                    .ToList()
            };
        }
    }
}