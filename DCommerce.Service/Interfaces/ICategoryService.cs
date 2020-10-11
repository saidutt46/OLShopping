using System;
using System.Threading.Tasks;
using DCommerce.Data.Domain;
using DCommerce.Dto.Requests.Category;
using DCommerce.Dto.Responses;
using DCommerce.Dto.Shared;
using DCommerce.Service.Shared;

namespace DCommerce.Service.Interfaces
{
    public interface ICategoryService
    {
        Task<BaseDtoListResponse<CategoryDto>> ListAsync();
        Task<BaseDtoResponse<CategoryDto>> GetById(Guid id);
        Task<BaseDtoResponse<CategoryDto>> Add(CategoryCreateRequest request);
        Task<BaseDtoResponse<CategoryDto>> Update(Guid id, CategoryUpdateRequest request);
        Task<BaseDtoResponse<CategoryDto>> Delete(Guid id);
    }
}
