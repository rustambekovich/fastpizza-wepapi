using FastPizza.DataAccess.Interfaces.Categories;
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Categories;
using FastPizza.Domain.Exceptions.Categories;
using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Dtos.CategoryDtos;
using FastPizza.Service.Interfaces.Categories;
using FastPizza.Service.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IFileService _fileService;
        public CategoryService(ICategoryRepository categoryRepository,
            IFileService fileService)
        {
            this._repository = categoryRepository;
            this._fileService = fileService;
        }
        public async Task<long> CountAsync()
        {
            var result = await _repository.CountAsync();
           
            return result;
        }

        public async Task<bool> CreateAsync(CategoryCreateDto dto)
        {
            var imagePath = await _fileService.UploadImageAsync(dto.ImagePath);
            Category category = new Category()
            {
                Name = dto.Name,
                Description = dto.Description,
                ImagePath = imagePath,
                CreatedAt = TimeHelper.GetDateTime(),
                UpdatedAt= TimeHelper.GetDateTime(),
                
            };
            var result = await _repository.CreateAsync(category);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var res = await _repository.GetByIdAsync(id);
            if (res is null)
                throw new CategoryNotFoundException();
            var result = await _repository.DeleteAsync(id);
            if(result > 0)
            {
                await _fileService.DeleteImageAsync(res.ImagePath);
                return true;
            }
            return false;
        }

        public async Task<List<Category>> GetAllAsync(PaginationParams @params)
        {
           var result = await _repository.GetAllAsync(@params);
           if (result is  null)
               throw new CategoryNotFoundException();
            return result.ToList();
        }

        

        public async Task<Category> GetByIdAsync(long id)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result is  null)
                throw new CategoryNotFoundException();
            return result;
        }

        public async Task<bool> UpdateAsync(long id, CategoryCreateDto dto)
        {
            var result = await _repository.GetByIdAsync(id);
            if (result is not null)
                throw new CategoryNotFoundException();
            if (result != null)
            {
                result.Name = dto.Name;
                result.Description = dto.Description;
                if(dto.ImagePath != null)
                {
                    await _fileService.DeleteImageAsync(result.ImagePath);
                    string newPath = await _fileService.UploadImageAsync(dto.ImagePath);
                    result.ImagePath = newPath;
                    result.UpdatedAt = TimeHelper.GetDateTime();
                }
            }
            var res = await _repository.UpdateAsync(id, result);
            return res > 0;
        }
    }
}
