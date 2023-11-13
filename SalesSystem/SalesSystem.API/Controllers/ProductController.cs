using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesSystem.Data.Repositories.Interfaces;
using SalesSystem.Model;
using SalesSystem.Shared.DTOs;

namespace SalesSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository, IMapper mapper)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            ResponseDTO<List<ProductDTO>> responseDTO = new ResponseDTO<List<ProductDTO>>();

            try
            {
                List<ProductDTO> ListaProductos = new List<ProductDTO>();
                IQueryable<Product> query = await _productRepository.Find();
                query = query.Include(r => r.CategoryNavigationId);

                ListaProductos = _mapper.Map<List<ProductDTO>>(query.ToList());

                if (ListaProductos.Count > 0)
                    responseDTO = new ResponseDTO<List<ProductDTO>>() { status = true, msg = "ok", value = ListaProductos };
                else
                    responseDTO = new ResponseDTO<List<ProductDTO>>() { status = false, msg = "", value = null };

                return StatusCode(StatusCodes.Status200OK, responseDTO);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO<List<ProductDTO>>() { status = false, msg = ex.Message, value = null };
                return StatusCode(StatusCodes.Status500InternalServerError, responseDTO);
            }
        }


        [HttpPost]
        [Route("Save")]
        public async Task<IActionResult> Save([FromBody] ProductDTO request)
        {
            ResponseDTO<ProductDTO> responseDTO = new ResponseDTO<ProductDTO>();
            try
            {
                Product product = _mapper.Map<Product>(request);

                Product productCreated = await _productRepository.Create(product);

                if (productCreated.ProductId != 0)
                    responseDTO = new ResponseDTO<ProductDTO>() { status = true, msg = "ok", value = _mapper.Map<ProductDTO>(productCreated) };
                else
                    responseDTO = new ResponseDTO<ProductDTO>() { status = false, msg = "No se pudo crear el producto" };

                return StatusCode(StatusCodes.Status200OK, responseDTO);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO<ProductDTO>() { status = false, msg = ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, responseDTO);
            }
        }

        [HttpPut]
        [Route("Edit")]
        public async Task<IActionResult> Edit([FromBody] ProductDTO request)
        {
            ResponseDTO<bool> responseDTO = new ResponseDTO<bool>();
            try
            {
                Product product = _mapper.Map<Product>(request);
                Product productToEdit = await _productRepository.Get(u => u.ProductId == product.ProductId);

                if (productToEdit != null)
                {

                    productToEdit.Name = product.Name;
                    productToEdit.CategoryId = product.CategoryId;
                    productToEdit.Stock = product.Stock;
                    productToEdit.Price = product.Price;

                    bool respuesta = await _productRepository.Edit(productToEdit);

                    if (respuesta)
                        responseDTO = new ResponseDTO<bool>() { status = true, msg = "ok", value = true };
                    else
                        responseDTO = new ResponseDTO<bool>() { status = false, msg = "No se pudo editar el producto" };
                }
                else
                {
                    responseDTO = new ResponseDTO<bool>() { status = false, msg = "No se encontró el producto" };
                }

                return StatusCode(StatusCodes.Status200OK, responseDTO);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO<bool>() { status = false, msg = ex.Message };
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
                Product productToDelete = await _productRepository.Get(u => u.ProductId == id);

                if (productToDelete != null)
                {

                    bool response = await _productRepository.Delete(productToDelete);

                    if (response)
                        responseDTO = new ResponseDTO<string>() { status = true, msg = "ok", value = "" };
                    else
                        responseDTO = new ResponseDTO<string>() { status = false, msg = "No se pudo eliminar el producto", value = "" };
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
