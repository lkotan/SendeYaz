using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SendeYaz.API.Repositories;
using SendeYaz.Business.Abstract;
using SendeYaz.Models;
using System.Threading.Tasks;

namespace SendeYaz.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController:ControllerRepository<ICategoryService,CategoryModel>
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service,IMapper mapper):base(service,mapper)
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
