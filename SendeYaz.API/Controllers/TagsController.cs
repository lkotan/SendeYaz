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
    public class TagsController : ControllerRepository<ITagService,TagModel>
    {
        private readonly ITagService _service;
        
        public TagsController(ITagService service,IMapper mapper):base(service,mapper)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

    }
}