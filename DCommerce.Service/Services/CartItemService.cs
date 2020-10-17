using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DCommerce.Data.Domain;
using DCommerce.Dto.Requests.AddToCart;
using DCommerce.Dto.Responses;
using DCommerce.Dto.Shared;
using DCommerce.Repository.ApplicationSpecifications;
using DCommerce.Repository.Interfaces;
using DCommerce.Service.Interfaces;

namespace DCommerce.Service.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CartItemService(ICartItemRepository cartItemRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<BaseDtoListResponse<AddToCartDto>> ListAllByUserId(string identityId)
        {
            try
            {
                IList<CartItem> items = await _cartItemRepository.List(new ItemsInCartForUserSpecification(identityId));
                List<AddToCartDto> cartItems = new List<AddToCartDto>();
                if (items != null)
                {
                    foreach (CartItem item in items)
                    {
                        AddToCartDto c = _mapper.Map<CartItem, AddToCartDto>(item);
                        cartItems.Add(c);
                    }
                    return new BaseDtoListResponse<AddToCartDto>(cartItems);
                }
                else
                {
                    return new BaseDtoListResponse<AddToCartDto>("Cannot find any items in the cart for the user");
                }

            }
            catch (Exception ex)
            {
                return new BaseDtoListResponse<AddToCartDto>(ex.Message);
            }
        }

        public async Task<BaseDtoResponse<AddToCartDto>> GetById(Guid id)
        {
            try
            {
                CartItem item = await _cartItemRepository.GetById(id);

                if (item == null)
                    return new BaseDtoResponse<AddToCartDto>("Category Not Found");
                AddToCartDto result = _mapper.Map<CartItem, AddToCartDto>(item);
                return new BaseDtoResponse<AddToCartDto>(result);
            }
            catch (Exception ex)
            {
                return new BaseDtoResponse<AddToCartDto>(ex.Message);
            }
        }

        public async Task<BaseDtoResponse<AddToCartDto>> Add(AddToCartRequest request)
        {
            try
            {
                Product product = await  _productRepository.GetById(request.ProductId);
                if (product != null)
                {
                    CartItem item = _mapper.Map<AddToCartRequest, CartItem>(request);
                    item.Product = product;
                    CartItem carItem = await _cartItemRepository.Add(item);
                    if (carItem == null)
                        return new BaseDtoResponse<AddToCartDto>("Error occured while creating the CartItem, try again");
                    AddToCartDto response = _mapper.Map<CartItem, AddToCartDto>(carItem);
                    return new BaseDtoResponse<AddToCartDto>(response);
                }
                else
                {
                    return new BaseDtoResponse<AddToCartDto>("Unable to find product in the Catalog, please try again");
                }
                //IList<CartItem> items = await _cartItemRepository.List(new ItemsInCartForUserSpecification(request.IdentityId));
                //if (items != null)
                //{
                //    CartItem sameProduct = items.Cast<CartItem>().SingleOrDefault(e => e.ProductId == request.ProductId);

                //}
            }
            catch (Exception ex)
            {
                return new BaseDtoResponse<AddToCartDto>(ex.Message);
            }
        }


        public async Task<BaseDtoResponse<AddToCartDto>> Update(Guid id, UpdateCartRequest request)
        {
            try
            {
                CartItem existingItem = await _cartItemRepository.GetById(id);
                if (existingItem == null)
                    return new BaseDtoResponse<AddToCartDto>("Unable to update the item in the cart, try later!");
                existingItem.Quantity = request.Quantity;
                await _cartItemRepository.Update(existingItem);
                CartItem updatedItem = await _cartItemRepository.GetById(id);
                AddToCartDto response = _mapper.Map<CartItem, AddToCartDto>(updatedItem);
                return new BaseDtoResponse<AddToCartDto>(response);
            }
            catch (Exception ex)
            {
                return new BaseDtoResponse<AddToCartDto>(ex.Message);
            }
        }

        public async Task<BaseDtoResponse<AddToCartDto>> Delete(Guid id)
        {
            try
            {
                CartItem item = await _cartItemRepository.GetById(id);
                if (item != null)
                {
                    await _cartItemRepository.Delete(item);
                    AddToCartDto result = _mapper.Map<CartItem, AddToCartDto>(item);
                    return new BaseDtoResponse<AddToCartDto>(result);

                }
                else
                {
                    return new BaseDtoResponse<AddToCartDto>("Cart Item Not found");
                }
            }
            catch (Exception ex)
            {
                return new BaseDtoResponse<AddToCartDto>($"An error occurred when deleting the cart item: {ex.Message}");
            }
        }
    }
}
