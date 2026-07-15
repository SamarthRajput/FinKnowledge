using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Data;
using backend.Dtos.Stock;
using backend.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/stock")]
    [ApiController]
    public class StockController : ControllerBase
    {
        // security purpose
        // It declares a private field to hold your database context, so your controller (or service) can use it across all its methods without recreating it each time
        private readonly ApplicationDBContext _context;
        public StockController(ApplicationDBContext context)
        {   
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            // get all the stocks from database
            // defered execution, it will return a list object
            // Select is dotnet version of map, we use in javascript 
            var stocks = _context.Stock.ToList()
                .Select(s => s.ToStockDto());
            
            return Ok(stocks);
        }

        // will return one actual item
        // model binding will extract this id 
        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var stock = _context.Stock.Find(id);

            if(stock == null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());
        }

        // Data will be send in the from of json, we will take it from the body
        // We are going to create request portion of our Dto, we want certain type of data from the user, not all type of data so we are creating dtos
        [HttpPost]
        public IActionResult Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDTO();
            _context.Stock.Add(stockModel);
            // Save changes are required to save all the changes
            _context.SaveChanges();
            // CreateAtAction is going to do run GetById Function after all the above code executed, it will pass the new object into the id and after that it is going to return in the form of ToStockDto
            return CreatedAtAction(nameof(GetById), new { id = stockModel.Id}, stockModel.ToStockDto());

        }


    }
}