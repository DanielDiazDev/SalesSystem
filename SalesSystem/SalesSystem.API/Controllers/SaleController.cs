using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalesSystem.Data.Repositories.Interfaces;
using SalesSystem.Model;
using SalesSystem.Shared.DTOs;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaleController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISaleRepository _saleRepository;

        public SaleController(ISaleRepository saleRepository, IMapper mapper)
        {
            _mapper = mapper;
            _saleRepository = saleRepository;
        }


        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] SaleDTO request)
        {
            ResponseDTO<SaleDTO> responseDTO = new ResponseDTO<SaleDTO>();
            try
            {

                Sale saleCreated = await _saleRepository.Register(_mapper.Map<Sale>(request));
                request = _mapper.Map<SaleDTO>(saleCreated);

                if (saleCreated.SaleId != 0)
                    responseDTO = new ResponseDTO<SaleDTO>() { status = true, msg = "ok", value = request };
                else
                    responseDTO = new ResponseDTO<SaleDTO>() { status = false, msg = "No se pudo registrar la venta" };

                return StatusCode(StatusCodes.Status200OK, responseDTO);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO<SaleDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, responseDTO);
            }
        }

        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string findBy, string? saleNumber, string? startDate, string? endDate)
        {
            ResponseDTO<List<SaleDTO>> responseDTO = new ResponseDTO<List<SaleDTO>>();

            saleNumber = saleNumber is null ? "" : saleNumber;
            startDate = startDate is null ? "" : startDate;
            endDate = startDate is null ? "" : endDate;

            try
            {

                List<SaleDTO> vmHistorialSale = _mapper.Map<List<SaleDTO>>(await _saleRepository.Historial(findBy, saleNumber, startDate, endDate));

                if (vmHistorialSale.Count > 0)
                    responseDTO = new ResponseDTO<List<SaleDTO>>() { status = true, msg = "ok", value = vmHistorialSale };
                else
                    responseDTO = new ResponseDTO<List<SaleDTO>>() { status = false, msg = "No se pudo registrar la venta" };

                return StatusCode(StatusCodes.Status200OK, responseDTO);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO<List<SaleDTO>>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, responseDTO);

            }

        }

        [HttpGet]
        [Route("Report")]
        public async Task<IActionResult> Report(string? startDate, string? endDate)
        {
            ResponseDTO<List<ReportDTO>> responseDTO = new ResponseDTO<List<ReportDTO>>();
            try
            {
                List<ReportDTO> listReport = _mapper.Map<List<ReportDTO>>(await _saleRepository.Report(startDate, endDate));

                if (listReport.Count > 0)
                    responseDTO = new ResponseDTO<List<ReportDTO>>() { status = true, msg = "ok", value = listReport };
                else
                    responseDTO = new ResponseDTO<List<ReportDTO>>() { status = false, msg = "No se pudo registrar la venta" };

                return StatusCode(StatusCodes.Status200OK, responseDTO);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO<List<ReportDTO>>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, responseDTO);

            }

        }
    }
}
