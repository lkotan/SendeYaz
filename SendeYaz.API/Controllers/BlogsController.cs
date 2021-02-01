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
    public class BlogsController : ControllerRepository<IBlogService,BlogModel>
    {
        private readonly IBlogService _service;

        public BlogsController(IBlogService service,IMapper mapper):base(service,mapper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? tagId)
        {
            return Ok(await _service.GetAllAsync(tagId));
        }

        [HttpGet("Detail")]
        public async Task<IActionResult> Detail([FromQuery] int id)
        {
            return Ok(await _service.DetailAsync(id));
        }

        [HttpGet("GetAllCategoryOrAccount")]
        public async Task<IActionResult> GetAllCategoryOrAccount([FromQuery] int? categoryId,[FromQuery] int? accountId)
        {
            return Ok(await _service.GetAllByCategoryIdOrAccountIdAsync(categoryId,accountId));
        }

        [HttpGet("SelectList")]
        public async Task<IActionResult> SelectList()
        {
            return Ok(await _service.SelectListAsync());
        }
    }
}