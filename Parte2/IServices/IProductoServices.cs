using Parte2.DTOs;
using System.Threading.Tasks;

namespace Parte2.IServices
{
    public interface IProductoServices
    {
        Task<ResponseDTO> Create(ProductoCommand productoCommand);
        Task<ResponseDTO> Update(ProductoCommand productoCommand);
        Task<ResponseDTO> GetAll();
        Task<ResponseDTO> GetById(int id);
        Task<ResponseDTO> Delete(int id);
    }
}
