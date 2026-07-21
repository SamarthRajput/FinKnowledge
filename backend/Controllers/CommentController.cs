using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Dtos.Comment;
using backend.Interfaces;
using backend.Mappers;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("/api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentRepository _commentRepo;
        private readonly IStockRepository _stockRepo;

        public CommentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // We have to use ModelState so that C# knows that we are using data validations
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var comments = await _commentRepo.GetAllAsync();

            var CommentDto = comments.Select(s => s.ToCommentDto());

            return Ok(CommentDto);
        }

        // Data validation, url constraint
        [HttpGet("{id:int}")]
        public async Task<IActionResult>GetById([FromRoute]int id)
        {  
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           var comment = await _commentRepo.GetByIdAsync(id);

           if(comment == null)
            {
                return NotFound();
            } 

            return Ok(comment.ToCommentDto());
        }

        [HttpPost("{stockId:int}")]
        public async Task<IActionResult>Create([FromRoute] int stockId, CreateCommentDto commentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock doesnot exists");
            }

            // now we are going to map it with the commentDto using mapper
            var commentModel = commentDto.ToCommentFromCreate(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }   

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult>Delete ([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var commentModel = await _commentRepo.DeleteAsync(id);

            if(commentModel == null)
            {
                return NotFound("Comment does not exists");
            }
            return Ok(commentModel);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto UpdateDto)
        {
            // As in Database call we are using CommentModel, so we can pass our Dto, so we need to create a Mapper for them
            var comment = await _commentRepo. UpdateAsync(id, UpdateDto.ToCommentFromUpdate());
            if(comment == null)
            {
                return NotFound("Comment not found");
            }
            return Ok(comment.ToCommentDto());

        }
    }
}