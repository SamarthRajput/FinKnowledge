using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using backend.Data;
using backend.Interfaces;
using backend.Models;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository
{
    public class ExcelRepository : IExcelRepository
    {
        private readonly ApplicationDBContext _context;
        public ExcelRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<ExcelUpload>> GetAllUploadsAsync()
        {
            return await _context.ExcelUploads
                    .Include(u => u.Rows)
                    .ThenInclude(r => r.Cells)
                    .ToListAsync();
        }

        public async Task<ExcelUpload?> GetUploadByIdAsync(int id)
        {
            return await _context.ExcelUploads
                    .Include(u => u.Rows)
                    .ThenInclude(r => r.Cells)
                    .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ExcelUpload> ParseAndSaveExcelAsync(IFormFile file)
        {
            var excelUpload = new ExcelUpload {
                FileName = file.FileName,
                UploadedOn = DateTime.UtcNow
            };

            using var stream = new MemoryStream();
            await file.CopyToAsync(stream);
            stream.Position = 0;

            using var workbook = new XLWorkbook(stream);
            var worksheet = workbook.Worksheets.First();
            var allRows = worksheet.RangeUsed()!.RowsUsed().ToList();

            var headerRow = allRows[0];
            var headers = headerRow.Cells().Select(c => c.Value.ToString()).ToList();
            
            int rowNumber = 1;
            foreach(var row in allRows.Skip(1))
            {
                var excelRow = new ExcelRow { RowNumber = rowNumber };

                for(int i = 0; i < headers.Count; i++)
                {
                    var cell = row.Cell(i + 1);
                    excelRow.Cells.Add(new ExcelCells
                    {
                        ColumnName = headers[i],
                        Value = cell.GetString()
                    });
                }

                excelUpload.Rows.Add(excelRow);
                rowNumber++;
            }

            await _context.ExcelUploads.AddAsync(excelUpload);
            await _context.SaveChangesAsync();

            return excelUpload;

        }
    }
}