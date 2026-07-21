using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using backend.Data;
using backend.Interfaces;
using backend.Models;

namespace backend.Repository
{
    public class ExcelRepository : IExcelRepository
    {
        private readonly ApplicationDBContext _context;
        public ExcelRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public Task<List<ExcelUpload>> GetAllUploadsAsync()
        {
            
        }

        public Task<ExcelUpload?> GetUploadByIdAsync(int id)
        {
            throw new NotImplementedException();
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

            using var workbook = new 
        }
    }
}