using AutoMapper;
using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Mapper;
using BAL.Utilities;
using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryService()
        {
            EFAppContext context = new EFAppContext();
            _categoryRepository = new CategoryRepository(context);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        public async Task CreateCategory(CategoryEntityDTO entity)
        {
            var category = _mapper.Map<CategoryEntityDTO, CategoryEntity>(entity);

            await _categoryRepository.Create(category);

            entity.Id = category.Id;
        }

        public async Task DeleteCategory(CategoryEntityDTO entity)
        {
            await _categoryRepository.Delete(entity.Id);
        }

        public async Task EditCategory(CategoryEntityDTO entity)
        {
            var category = _mapper.Map<CategoryEntityDTO, CategoryEntity>(entity);
            category.Products = null;
            await _categoryRepository.Update(category.Id, category);
        }

        public IEnumerable<CategoryEntityDTO> GetCategories()
        {
            return _mapper.Map<IEnumerable<CategoryEntity>, IEnumerable<CategoryEntityDTO>>(_categoryRepository.GetAllCategories());
        }
    }
}
