using FastPizza.DataAccess.Interfaces.Products;
using FastPizza.DataAccess.Utils;
using FastPizza.Domain.Entities.Products;
using FastPizza.Service.Commons.Helper;
using FastPizza.Service.Dtos.ProductDtos;
using FastPizza.Service.Interfaces.Common;
using FastPizza.Service.Interfaces.Products;

namespace FastPizza.Service.Services.Products
{
    public class ProductServise : IProductServise
    {
        private IProductRepository _repository;
        private IFileService _fileservice;

        public ProductServise(IProductRepository repository,
            IFileService fileService)
        {
            this._repository = repository;
            this._fileservice = fileService;
        }
        public async Task<long> CountAsync()
        {
            var result = await _repository.CountAsync();
            return result;
        }

        public async Task<bool> CreateAsync(ProductCreateDto dto)
        {
            var img = await _fileservice.UploadImageAsync(dto.Image);
            Product product = new Product()
            {
                Name = dto.Name,
                Description = dto.Description,
                Unitprice = dto.Unitprice,
                CategoryID = dto.CategoryID,
                CreatedAt = TimeHelper.GetDateTime(),
                UpdatedAt = TimeHelper.GetDateTime(),
                ImagePath = img
            };
            var result = await _repository.CreateAsync(product);
            return result > 0;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var resImg = await _repository.GetByIdAsync(id);
            var result = await _repository.DeleteAsync(id);
            if( result > 0)
            {
                var res = await _fileservice.DeleteImageAsync(resImg.ImagePath);
                if(res)
                    return true;
                return false;
            }
            return false;
        }

        public async Task<IList<Product>> GetAllAsync(PaginationParams PaginationParams)
        {
            var result = await _repository.GetAllAsync(PaginationParams);
            return result;
        }

        public async Task<Product?> GetByIdAsync(long id)
        {
            var result = await _repository.GetByIdAsync(id);
            return result;
        }

        public async Task<bool> UpdateAsync(long id, ProductCreateDto dto)
        {
            var resultGetbyId = await _repository.GetByIdAsync(id);
            if(resultGetbyId is not null)
            {
                resultGetbyId.Name = dto.Name;
                resultGetbyId.Description = dto.Description;
                resultGetbyId.Unitprice = dto.Unitprice;
                resultGetbyId.CategoryID = dto.CategoryID;
                if(resultGetbyId.ImagePath is not null)
                {
                    await _fileservice.DeleteImageAsync(resultGetbyId.ImagePath);
                    string newIMG = await _fileservice.UploadImageAsync(dto.Image);
                    resultGetbyId.ImagePath = newIMG;
                    resultGetbyId.UpdatedAt = TimeHelper.GetDateTime();
                }
            }
            var result = await _repository.UpdateAsync(id, resultGetbyId);
            return result > 0;
        }
    }
}
