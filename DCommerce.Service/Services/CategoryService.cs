using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DCommerce.Data.Domain;
using DCommerce.Dto.Requests.Category;
using DCommerce.Dto.Responses;
using DCommerce.Dto.Shared;
using DCommerce.Repository.Interfaces;
using DCommerce.Service.Interfaces;

namespace DCommerce.Service.Services
{
    public class CategoryService : ICategoryService
    {
        protected ICategoryRepository _categoryRepository;
        protected IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;

        }

        public async Task<BaseDtoListResponse<CategoryDto>> ListAsync()
        {
            try
            {
                IList<Category> categories = await _categoryRepository.ListAll();
                if (categories != null)
                {
                    IList<CategoryDto> result = _mapper.Map<IList<Category>, IList<CategoryDto>>(categories);
                    BaseDtoListResponse<CategoryDto> response = new BaseDtoListResponse<CategoryDto>(result);
                    return response;
                }
                else
                {
                    return new BaseDtoListResponse<CategoryDto>("Something went wrong, please try again");
                }
            }
            catch (Exception ex)
            {
                return new BaseDtoListResponse<CategoryDto>(ex.Message);
            }
        }

        public async Task<BaseDtoResponse<CategoryDto>> GetById(Guid id)
        {
            Category category = await _categoryRepository.GetById(id);

            if (category == null)
                return new BaseDtoResponse<CategoryDto>("Category Not Found");
            CategoryDto result = _mapper.Map<Category, CategoryDto>(category);
            return new BaseDtoResponse<CategoryDto>(result);
        }

        public async Task<BaseDtoResponse<CategoryDto>> Add(CategoryCreateRequest request)
        {
            try
            {
                Category model = _mapper.Map<CategoryCreateRequest, Category>(request);
                Category category = await _categoryRepository.Add(model);
                if (category != null)
                {
                    CategoryDto result = _mapper.Map<Category, CategoryDto>(category);
                    return new BaseDtoResponse<CategoryDto>(result);
                }
                else
                {
                    return new BaseDtoResponse<CategoryDto>("Unable to create a new category, try again");
                }
            }
            catch (Exception ex)
            {
                return new BaseDtoResponse<CategoryDto>($"An error occurred when saving the category: {ex.Message}");
            }
        }

        public async Task<BaseDtoResponse<CategoryDto>> Update(Guid id, CategoryUpdateRequest request)
        {
            try
            {
                Category category = await _categoryRepository.GetById(id);
                if (category != null)
                {
                    category.Name = request.Name;
                    category.Description = request.Description;
                    await _categoryRepository.Update(category);
                    Category updatedResult = await _categoryRepository.GetById(id);
                    CategoryDto result = _mapper.Map<Category, CategoryDto>(updatedResult);
                    return new BaseDtoResponse<CategoryDto>(result);

                }
                else
                {
                    return new BaseDtoResponse<CategoryDto>("Category Not found");
                }
            }
            catch (Exception ex)
            {
                return new BaseDtoResponse<CategoryDto>($"An error occurred when updating the category: {ex.Message}");
            }
        }

        public async Task<BaseDtoResponse<CategoryDto>> Delete(Guid id)
        {
            try
            {
                Category currentCategory = await _categoryRepository.GetById(id);
                if (currentCategory != null)
                {
                    await _categoryRepository.Delete(currentCategory);
                    CategoryDto result = _mapper.Map<Category, CategoryDto>(currentCategory);
                    return new BaseDtoResponse<CategoryDto>(result);

                }
                else
                {
                    return new BaseDtoResponse<CategoryDto>("Category Not found");
                }
            }
            catch (Exception ex)
            {
                return new BaseDtoResponse<CategoryDto>($"An error occurred when deleting the category: {ex.Message}");
            }
        }
    }
}
