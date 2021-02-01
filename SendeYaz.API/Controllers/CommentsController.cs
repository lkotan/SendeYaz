using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendeYaz.API.Repositories;
using SendeYaz.Business.Abstract;
using SendeYaz.Models;

namespace SendeYaz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerRepository<ICommentService,CommentModel>
    {
        private readonly ICommentService _service;

        public CommentsController(ICommentService service,IMapper mapper):base(service,mapper)
        {
            _service = service;
        }

        [HttpGet("GetAllWithSubComments")]
        public async Task<IActionResult> GetAllWithSubComments([FromQuery] int blogId,[FromQuery] int? parentId)
        {
            return Ok(await _service.GetAllWithSubCommentsAsync(blogId, parentId));
        }
    }
}