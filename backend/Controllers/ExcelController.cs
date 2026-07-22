using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("/api/excel")]
    public class ExcelController : ControllerBase
    {
        private readonly IExcelRepository _excelRepository;

        public ExcelController(IExcelRepository excelRepository)
        {
            _excelRepository = excelRepository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if(file == null || file.Length == 0)
            {
                return BadRequest("No File Uploaded");
            }
            if (!file.FileName.EndsWith(".xlsx"))
            {
                return BadRequest("Only .xlsx fileare allowed to upload");
            }
            var savedUpload = await _excelRepository.ParseAndSaveExcelAsync(file);
            var resultDto = savedUpload.ToUploadResultDto();
            return Ok(resultDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var uploads = await _excelRepository.GetAllUploadsAsync();
            var result = uploads.Select(u => u.ToUploadResultDto()).ToList();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var uploads = await _excelRepository.GetUploadByIdAsync(id);
            if(uploads == null)
            {
                return NotFound();
            }
            return Ok(uploads.ToUploadResultDto());
        }
    }
}