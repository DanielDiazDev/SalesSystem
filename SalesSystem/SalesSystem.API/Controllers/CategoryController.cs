using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Data.Repositories.Interfaces;
using SalesSystem.Shared.DTOs;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            ResponseDTO<List<CategoryDTO>> response = new ResponseDTO<List<CategoryDTO>>();

            try
            {
                List<CategoryDTO> listCategories = new List<CategoryDTO>();
                listCategories = _mapper.Map<List<CategoryDTO>>(await _categoryRepository.GetCategories());

                if (listCategories.Count > 0)
                    response = new ResponseDTO<List<CategoryDTO>>() { status = true, msg = "Ok", value = listCategories };
                else
                    response = new ResponseDTO<List<CategoryDTO>>() { status = false, msg = "Sin resultados", value = null };


                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                response = new ResponseDTO<List<CategoryDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }
        }
    }
}
