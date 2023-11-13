using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Data.Repositories.Interfaces;
using SalesSystem.Shared.DTOs;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        public RoleController(IRoleRepository roleRepository, IMapper mapper)
        {
            _mapper = mapper;
            _roleRepository = roleRepository;
        }

        [HttpGet]
        [Route("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            ResponseDTO<List<RoleDTO>> responseDTO = new ResponseDTO<List<RoleDTO>>();

            try
            {
                List<RoleDTO> listRoles = new List<RoleDTO>();
                listRoles = _mapper.Map<List<RoleDTO>>(await _roleRepository.GetRoles());

                if (listRoles.Count > 0)
                    responseDTO = new ResponseDTO<List<RoleDTO>>() { status = true, msg = "ok", value = listRoles };
                else
                    responseDTO = new ResponseDTO<List<RoleDTO>>() { status = false, msg = "sin resultados", value = null };


                return StatusCode(StatusCodes.Status200OK, responseDTO);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO<List<RoleDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, responseDTO);
            }
        }
    }
}
