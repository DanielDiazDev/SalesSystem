using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Data.Repositories.Interfaces;
using SalesSystem.Model;
using SalesSystem.Shared.DTOs;
using SalesSystem.Util;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        [HttpGet]
        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            ResponseDTO<List<UserDTO>> responseDTO = new ResponseDTO<List<UserDTO>>();

            try
            {
                List<UserDTO> listUsers = new List<UserDTO>();
                IQueryable<User> query = await _userRepository.Find();
                query = query.Include(r => r.RoleNavigationId);

                listUsers = _mapper.Map<List<UserDTO>>(query.ToList());

                if (listUsers.Count > 0)
                    responseDTO = new ResponseDTO<List<UserDTO>>() { status = true, msg = "ok", value = listUsers };
                else
                    responseDTO = new ResponseDTO<List<UserDTO>>() { status = false, msg = "", value = null };

                return StatusCode(StatusCodes.Status200OK, responseDTO);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO<List<UserDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, responseDTO);
            }
        }

        [HttpGet]
        [Route("Login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            ResponseDTO<User> _ResponseDTO = new ResponseDTO<User>();
            try
            {
                string passwordEncrypted = EncryptPassword.GetSHA256(password);
                User user = await _userRepository.Get(u => u.Email == email && u.Password == passwordEncrypted);

                if (user != null)
                    _ResponseDTO = new ResponseDTO<User>() { status = true, msg = "ok", value = user };
                else
                    _ResponseDTO = new ResponseDTO<User>() { status = false, msg = "no encontrado", value = null };

                return StatusCode(StatusCodes.Status200OK, _ResponseDTO);
            }
            catch (Exception ex)
            {
                _ResponseDTO = new ResponseDTO<User>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, _ResponseDTO);
            }
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO request) 
        {
            ResponseDTO<UserDTO> responseDTO = new ResponseDTO<UserDTO>();
            try
            {
                User user = _mapper.Map<User>(request);
                user.Password = EncryptPassword.GetSHA256(request.Password);

                User userCreated = await _userRepository.Create(user);

                if (userCreated.UserId != 0)
                    responseDTO = new ResponseDTO<UserDTO>() { status = true, msg = "ok", value = _mapper.Map<UserDTO>(userCreated) };
                else
                    responseDTO = new ResponseDTO<UserDTO>() { status = false, msg = "No se pudo crear el usuario" };

                return StatusCode(StatusCodes.Status200OK, responseDTO);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO<UserDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, responseDTO);
            }
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] UserDTO request)
        {
            ResponseDTO<UserDTO> responseDTO = new ResponseDTO<UserDTO>();
            try
            {
                User user = _mapper.Map<User>(request);
                User userToEdit = await _userRepository.Get(u => u.UserId == user.UserId);

                if (userToEdit != null)
                {

                    userToEdit.FullName = user.FullName;
                    userToEdit.Email = user.Email;
                    userToEdit.RoleId = user.RoleId;
                    userToEdit.Password = user.Password;

                    bool response = await _userRepository.Edit(userToEdit);

                    if (response)
                        responseDTO = new ResponseDTO<UserDTO>() { status = true, msg = "ok", value = _mapper.Map<UserDTO>(userToEdit) };
                    else
                        responseDTO = new ResponseDTO<UserDTO>() { status = false, msg = "No se pudo editar el usuario" };
                }
                else
                {
                    responseDTO = new ResponseDTO<UserDTO>() { status = false, msg = "No se encontró el usuario" };
                }

                return StatusCode(StatusCodes.Status200OK, responseDTO);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO<UserDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, responseDTO);
            }
        }



        [HttpDelete]
        [Route("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseDTO<string> responseDTO = new ResponseDTO<string>();
            try
            {
                User userDeleted = await _userRepository.Get(u => u.UserId == id);

                if (userDeleted != null)
                {

                    bool response = await _userRepository.Delete(userDeleted);

                    if (response)
                        responseDTO = new ResponseDTO<string>() { status = true, msg = "ok", value = "" };
                    else
                        responseDTO = new ResponseDTO<string>() { status = false, msg = "No se pudo eliminar el usuario", value = "" };
                }

                return StatusCode(StatusCodes.Status200OK, responseDTO);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO<string>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, responseDTO);
            }
        }
    }
}
