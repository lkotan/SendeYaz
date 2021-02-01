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
    public class AccountsController : ControllerRepository<IAccountService,AccountModel>
    {
        private readonly IAccountService _service;

        public AccountsController(IAccountService service,IMapper mapper):base(service,mapper)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("SelectList")]
        public async Task<IActionResult> SelectList()
        {
            return Ok(await _service.SelectListAsync());
        }

        [HttpPut("IsBlockedChange")]
        public async Task<IActionResult> IsBlockedChange([FromQuery] int id)
        {
            return Ok(await _service.IsBlockedChangeAsync(id));
        }

        [HttpPut("UpdateMe")]
        public async Task<IActionResult> UpdateMe([FromBody] AccountMeModel model)
        {
            return Ok(await _service.UpdateMeAsync(model));
        }
    }
}