using System;
using DCommerce.Dto.Requests.Category;
using DCommerce.Dto.Responses;
using DCommerce.Dto.Shared;
using System.Threading.Tasks;
using DCommerce.Dto.Requests.Product;

namespace DCommerce.Service.Interfaces
{
    public interface IProductService
    {
        Task<BaseDtoListResponse<ProductDto>> ListAsync();
        Task<BaseDtoResponse<ProductDto>> GetById(Guid id);
        Task<BaseDtoResponse<ProductDto>> Add(ProductCreateRequest request);
        Task<BaseDtoResponse<ProductDto>> Update(Guid id, ProductUpdateRequest request);
        Task<BaseDtoResponse<ProductDto>> Delete(Guid id);
    }
}
