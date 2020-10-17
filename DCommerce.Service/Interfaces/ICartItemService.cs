using System;
using System.Threading.Tasks;
using DCommerce.Dto.Requests.AddToCart;
using DCommerce.Dto.Responses;
using DCommerce.Dto.Shared;

namespace DCommerce.Service.Interfaces
{
    public interface ICartItemService
    {
        Task<BaseDtoListResponse<AddToCartDto>> ListAllByUserId(string IdentityId);
        Task<BaseDtoResponse<AddToCartDto>> GetById(Guid id);
        Task<BaseDtoResponse<AddToCartDto>> Add(AddToCartRequest request);
        Task<BaseDtoResponse<AddToCartDto>> Update(Guid id, UpdateCartRequest request);
        Task<BaseDtoResponse<AddToCartDto>> Delete(Guid id);
    }
}
