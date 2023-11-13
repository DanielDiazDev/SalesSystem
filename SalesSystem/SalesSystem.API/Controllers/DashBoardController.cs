using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Data.Repositories.Interfaces;
using SalesSystem.Shared.DTOs;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashBoardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IDashBoardRepository _dashboardRepository;
        public DashBoardController(IDashBoardRepository dashBoardRepository, IMapper mapper)
        {
            _mapper = mapper;
            _dashboardRepository = dashBoardRepository;
        }

        [HttpGet]
        [Route("Resume")]
        public async Task<IActionResult> Resume()
        {
            ResponseDTO<DashBoardDTO> response = new ResponseDTO<DashBoardDTO>();

            try
            {

                DashBoardDTO vmDashboard = new DashBoardDTO();

                vmDashboard.SalesTotal = await _dashboardRepository.TotalSalesLastWeek();
                vmDashboard.IncomesTotal = await _dashboardRepository.TotalIncomesLastWeek();
                vmDashboard.ProductsTotal = await _dashboardRepository.TotalProduct();

                List<SaleWeeklyDTO> listSaleWeekly = new List<SaleWeeklyDTO>();

                foreach (KeyValuePair<string, int> item in await _dashboardRepository.SalesLastWeek())
                {
                    listSaleWeekly.Add(new SaleWeeklyDTO()
                    {
                        Date = item.Key,
                        Total = item.Value
                    });
                }
                vmDashboard.SalesLastWeek = listSaleWeekly;

                response = new ResponseDTO<DashBoardDTO>() { status = true, msg = "Ok", value = vmDashboard };
                return StatusCode(StatusCodes.Status200OK, response);

            }
            catch (Exception ex)
            {
                response = new ResponseDTO<DashBoardDTO>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, response);
            }

        }
    }
}