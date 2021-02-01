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
    public class RolesController : ControllerRepository<IRoleService,RoleModel>
    {
        private readonly IRoleService _service;

        public RolesController(IRoleService service,IMapper mapper):base(service,mapper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpPut("IsBlockedChange")]
        public async Task<IActionResult> IsBlockedChange([FromQuery] int id)
        {
            return Ok(await _service.IsBlockedChangeAsync(id));
        }
    }
}