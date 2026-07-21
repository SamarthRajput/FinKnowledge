using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;

namespace backend.Interfaces
{
    public interface IExcelRepository
    {
        Task<ExcelUpload> ParseAndSaveExcelAsync (IFormFile file);
        Task <List<ExcelUpload>> GetAllUploadsAsync();
        Task<ExcelUpload?> GetUploadByIdAsync(int id);
    }
}