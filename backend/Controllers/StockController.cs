using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.Stock;
using backend.Helpers;
using backend.Interfaces;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        // security purpose
        // It declares a private field to hold your database context, so your controller (or service) can use it across all its methods without recreating it each time
        private readonly ApplicationDBContext _context;
        private readonly IStockRepository _stockRepo;
        public StockController(ApplicationDBContext context, IStockRepository stockRepo)
        {   
            _stockRepo = stockRepo;
            _context = context;
        }

        // Get All Stocks
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // get all the stocks from database
            // defered execution, it will return a list object
            // Select is dotnet version of map, we use in javascript 
            // var stocks = await _context.Stock.ToListAsync();
            
            var stocks = await _stockRepo.GetAllAsync(query);
            var stockDto = stocks.Select(s => s.ToStockDto());
            
            return Ok(stocks);
        }

        // Get Stock by Id
        // will return one actual item
        // model binding will extract this id 
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // var stock = await _context.Stock.FindAsync(id);
            // with stockrepository function
            var stock = await _stockRepo.GetByIdAsync(id);

            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        // Create Stock
        // Data will be send in the from of json, we will take it from the body
        // We are going to create request portion of our Dto, we want certain type of data from the user, not all type of data so we are creating dtos
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var stockModel = stockDto.ToStockFromCreateDTO();
            // await _context.Stock.AddAsync(stockModel);
            // // Save changes are required to save all the changes
            // await _context.SaveChangesAsync();

            await _stockRepo.CreateAsync(stockModel);

            // CreateAtAction is going to do run GetById Function after all the above code executed, it will pass the new object into the id and after that it is going to return in the form of ToStockDto
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id}, stockModel.ToStockDto());
        } 

        // Update Stock by Id
        [HttpPut]
        [Route("{id:int}")]
        // Each type of request, each Dto is going to be different
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            var stockModel = await _stockRepo.UpdateAsync(id, updateDto);

            if(stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto());
        }

        // Delete Stock By Id
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            // var stockModel = await _context.Stock.FirstOrDefaultAsync(x => x.Id == id);
            var stockModel = await _stockRepo.DeleteAsync(id);

            if(stockModel == null)
            {
                return NotFound();
            }

            // _context.Stock.Remove(stockModel);
            // await _context.SaveChangesAsync();
            // Creates a NoContentResult object that produces an empty StatusCodes.Status204NoContent response.
            return NoContent();   
        }

    }
}