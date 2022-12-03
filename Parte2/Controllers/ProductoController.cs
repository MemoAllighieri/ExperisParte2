using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Parte2.Common;
using Parte2.DTOs;
using Parte2.IServices;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Threading.Tasks;

namespace Parte2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        private readonly IProductoServices _productoServices;        
        public ProductoController(IProductoServices productoServices)
        {
            _productoServices = productoServices;
        }
        [HttpGet("GetAll")]
        [SwaggerOperation(Summary = "Obtiene todos los registros de la tabla Producto")]
        public async Task<IActionResult> GetAll()
        {
            ResponseDTO response;
            try
            {
                response = await _productoServices.GetAll();
                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                response = new ResponseDTO { Status = 500, Message = ex.StackTrace.ToString() };                
                return StatusCode(response.Status, response);
            }
        }
        [HttpGet("GetById")]
        [SwaggerOperation(Summary = "Obtiene el registro por id de la tabla Producto")]
        public async Task<IActionResult> GetById(int id)
        {
            ResponseDTO response;
            try
            {
                response = await _productoServices.GetById(id);
                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                response = new ResponseDTO { Status = 500, Message = ex.StackTrace.ToString() };
                return StatusCode(response.Status, response);
            }
        }
        [HttpPost("Create")]
        [SwaggerOperation(Summary = "Crea un registro en la tabla Producto")]
        public async Task<IActionResult> Create(ProductoCommand productoCommand)
        {
            ResponseDTO response;
            try
            {
                var validator = new ModelValidator();
                ValidationResult result = validator.Validate(productoCommand);
                if (result.IsValid)
                {
                    response = await _productoServices.Create(productoCommand);
                    return StatusCode(response.Status, response);
                }
                else
                {
                    response = new ResponseDTO { Status = 400 };
                    foreach (var error in result.Errors)
                    {
                        response.Message += error.ErrorMessage.ToString() + "\n";                        
                    }
                    return StatusCode(response.Status, response);
                }                
            }
            catch (Exception ex)
            {
                response = new ResponseDTO { Status = 500, Message = ex.StackTrace.ToString() };
                return StatusCode(response.Status, response);
            }
        }
        [HttpPut("Update")]
        [SwaggerOperation(Summary = "Actualiza un registro en la tabla Producto")]
        public async Task<IActionResult> Update(ProductoCommand productoCommand)
        {
            ResponseDTO response;
            try
            {
                var validator = new ModelValidator();
                ValidationResult result = validator.Validate(productoCommand);
                if (result.IsValid)
                {
                    response = await _productoServices.Update(productoCommand);
                    return StatusCode(response.Status, response);
                }
                else
                {
                    response = new ResponseDTO { Status = 400 };
                    foreach (var error in result.Errors)
                    {
                        response.Message += error.ErrorMessage.ToString() + "\n";
                    }
                    return StatusCode(response.Status, response);
                }
            }
            catch (Exception ex)
            {
                response = new ResponseDTO { Status = 500, Message = ex.StackTrace.ToString() };
                return StatusCode(response.Status, response);
            }
        }
        [SwaggerOperation(Summary = "Elimina un registro en la tabla Producto")]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            ResponseDTO response;
            try
            {
                response = await _productoServices.Delete(id);
                return StatusCode(response.Status, response);
            }
            catch (Exception ex)
            {
                response = new ResponseDTO { Status = 500, Message = ex.StackTrace.ToString() };
                return StatusCode(response.Status, response);
            }
        }
    }
}
