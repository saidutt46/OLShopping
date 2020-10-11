using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DCommerce.Data.Domain;
using DCommerce.Data.Helpers;
using DCommerce.Dto.Requests.Product;
using DCommerce.Dto.Responses;
using DCommerce.Dto.Shared;
using DCommerce.Repository.Interfaces;
using DCommerce.Service.Interfaces;
using DCommerce.Service.Shared;

namespace DCommerce.Service.Services
{
    public class ProductService : IProductService
    {

        protected IProductRepository _productRepository;
        protected IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<BaseDtoListResponse<ProductDto>> ListAsync()
        {
            try
            {
                IList<Product> products = await _productRepository.ListAll();
                if (products != null)
                {
                    IList<ProductDto> result = _mapper.Map<IList<Product>, IList<ProductDto>>(products);
                    BaseDtoListResponse<ProductDto> response = new BaseDtoListResponse<ProductDto>(result);
                    return response;
                }
                else
                {
                    return new BaseDtoListResponse<ProductDto>("Something went wrong, please try again");
                }
            }
            catch (Exception ex)
            {
                return new BaseDtoListResponse<ProductDto>(ex.Message);
            }
        }

        public async Task<BaseDtoResponse<ProductDto>> GetById(Guid id)
        {
            Product product = await _productRepository.GetById(id);

            if (product == null)
                return new BaseDtoResponse<ProductDto>("Product Not Found");
            ProductDto result = _mapper.Map<Product, ProductDto>(product);
            return new BaseDtoResponse<ProductDto>(result);
        }

        public async Task<BaseDtoResponse<ProductDto>> Add(ProductCreateRequest request)
        {
            try
            {
                Product model = _mapper.Map<ProductCreateRequest, Product>(request);
                Product product = await _productRepository.Add(model);
                if (product != null)
                {
                    ProductDto result = _mapper.Map<Product, ProductDto>(product);
                    return new BaseDtoResponse<ProductDto>(result);
                }
                else
                {
                    return new BaseDtoResponse<ProductDto>("Unable to create a new product, try again");
                }
            }
            catch (Exception ex)
            {
                return new BaseDtoResponse<ProductDto>($"An error occurred when saving the Product: {ex.Message}");
            }
        }

        public async Task<BaseDtoResponse<ProductDto>> Update(Guid id, ProductUpdateRequest request)
        {
            try
            {
                Product product = await _productRepository.GetById(id);
                if (product != null)
                {
                    if (request.UnitOfMeasurement != null)
                    {
                        product.UnitOfMeasurement = EnumUtilExtension.ParseEnum<EUnitOfMeasurement>(request.UnitOfMeasurement);
                    }
                    else
                    {
                        product.UnitOfMeasurement = EUnitOfMeasurement.Unknown;
                    }
                    product.Name = request.Name;
                    product.Description = request.Description;
                    product.CategoryId = request.CategoryId;
                    product.ImagePath = request.ImagePath;
                    product.UnitPrice = request.UnitPrice;
                    await _productRepository.Update(product);
                    Product updatedResult = await _productRepository.GetById(id);
                    ProductDto result = _mapper.Map<Product, ProductDto>(updatedResult);
                    return new BaseDtoResponse<ProductDto>(result);

                }
                else
                {
                    return new BaseDtoResponse<ProductDto>("Product Not found");
                }
            }
            catch (Exception ex)
            {
                return new BaseDtoResponse<ProductDto>($"An error occurred when updating the Product: {ex.Message}");
            }
        }

        public async Task<BaseDtoResponse<ProductDto>> Delete(Guid id)
        {
            try
            {
                Product currentProduct = await _productRepository.GetById(id);
                if (currentProduct != null)
                {
                    await _productRepository.Delete(currentProduct);
                    ProductDto result = _mapper.Map<Product, ProductDto>(currentProduct);
                    return new BaseDtoResponse<ProductDto>(result);

                }
                else
                {
                    return new BaseDtoResponse<ProductDto>("Product Not found");
                }
            }
            catch (Exception ex)
            {
                return new BaseDtoResponse<ProductDto>($"An error occurred when deleting the Product: {ex.Message}");
            }
        }

    }
}
