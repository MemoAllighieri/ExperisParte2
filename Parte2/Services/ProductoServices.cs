using Parte2.DTOs;
using Parte2.IServices;
using Parte2.Persistences;
using System.Threading.Tasks;
using Dapper;
using System.Data;
using Parte2.Models;
using System.Collections.Generic;
using System.Linq;

namespace Parte2.Services
{
    public class ProductoServices : IProductoServices
    {
        private static ContextDatabase _contextDatabase;
        public ProductoServices(ContextDatabase contextDatabase)
        {
            _contextDatabase = contextDatabase;
        }

        public async Task<ResponseDTO> Create(ProductoCommand productoCommand)
        {
            ResponseDTO responseDTO = new();
            int rpta = 0;
            using (var connection = _contextDatabase.CreateConnection())
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@NOMBRE", productoCommand.Nombre, DbType.String, ParameterDirection.Input);
                parameter.Add("@PRECIO", productoCommand.Precio, DbType.Decimal, ParameterDirection.Input);
                parameter.Add("@STOCK", productoCommand.Stock, DbType.Decimal, ParameterDirection.Input);
                var result = await connection.ExecuteAsync("USP_CREATEUPDATEPRODUCTO", parameter, commandType: CommandType.StoredProcedure);
                rpta = result;                
                connection.Dispose();
            }
            responseDTO.Status = 201;
            responseDTO.Message = "Se procedio a crear el producto.";
            responseDTO.Data = rpta;
            return responseDTO;
        }

        public async Task<ResponseDTO> Update(ProductoCommand productoCommand)
        {
            ResponseDTO responseDTO = new();
            int rpta = 0;
            using (var connection = _contextDatabase.CreateConnection())
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@ID", productoCommand.Id, DbType.Int32, ParameterDirection.Input);
                parameter.Add("@NOMBRE", productoCommand.Nombre, DbType.String, ParameterDirection.Input);
                parameter.Add("@PRECIO", productoCommand.Precio, DbType.Decimal, ParameterDirection.Input);
                parameter.Add("@STOCK", productoCommand.Stock, DbType.Decimal, ParameterDirection.Input);
                var result = await connection.ExecuteAsync("USP_CREATEUPDATEPRODUCTO", parameter, commandType: CommandType.StoredProcedure);
                rpta = result;
                connection.Dispose();
            }
            responseDTO.Status = rpta == 1 ? 200 : 204;
            responseDTO.Message = rpta == 1 ? "Se procedio a modificar el producto." : "El usuario no existe para su modificación.";
            responseDTO.Data = rpta;
            return responseDTO;
        }

        public async Task<ResponseDTO> Delete(int id)
        {
            ResponseDTO responseDTO = new();
            int rpta = 0;
            using (var connection = _contextDatabase.CreateConnection())
            {
                DynamicParameters parameter = new DynamicParameters();
                parameter.Add("@ID", id, DbType.Int32, ParameterDirection.Input);
                var result = await connection.ExecuteAsync("USP_DELETEPRODUCTO", parameter, commandType: CommandType.StoredProcedure);
                rpta = result;
                connection.Dispose();
            }
            responseDTO.Status = rpta == 1 ? 200 : 204;
            responseDTO.Message = rpta == 1 ? "Se procedio a eliminar el producto." : "El usuario no existe para su eliminación.";
            responseDTO.Data = rpta;
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            ResponseDTO responseDTO = new();
            List<Producto> productos = new();
            using (var connection = _contextDatabase.CreateConnection())
            {
                using (var result = await connection.QueryMultipleAsync("USP_SEARCHPRODUCTO", null, commandType: CommandType.StoredProcedure))
                {
                    productos = result.Read<Producto>().ToList();
                }
                connection.Dispose();
            }
            responseDTO.Status = productos.Count > 0 ? 200 : 204;
            responseDTO.Message = productos.Count > 0 ? "Se procedio a listar los productos." : "No hay productos que listar";
            responseDTO.Data = productos;
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(int id)
        {
            ResponseDTO responseDTO = new();
            List<Producto> productos = new();
            using (var connection = _contextDatabase.CreateConnection())
            {
                using (var result = await connection.QueryMultipleAsync("USP_SEARCHPRODUCTO", new { ID = id }, commandType: CommandType.StoredProcedure))
                {
                    productos = result.Read<Producto>().ToList();
                }
                connection.Dispose();
            }
            responseDTO.Status = productos.Count > 0 ? 200 : 204;
            responseDTO.Message = productos.Count > 0 ? "Se procedio a listar el producto." : "No hay producto con el id ingresado.";
            responseDTO.Data = productos;
            return responseDTO;
        }
    }
}
