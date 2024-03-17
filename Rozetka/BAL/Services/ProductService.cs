using AutoMapper;
using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Mapper;
using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BAL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;
        public ProductService()
        {
            EFAppContext context = new EFAppContext();
            _productRepository = new ProductRepository(context);
            _productImageRepository = new ProductImageRepository(context);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }
        public async Task CreateProduct(ProductEntityDTO entity)
        {
            var product = _mapper.Map<ProductEntityDTO, ProductEntity>(entity);

            var images = product.Images;

            product.Images = null;
            product.Category = null;

            await _productRepository.Create(product);

            foreach (var image in images)
            {
                image.ProductId = product.Id;
                await _productImageRepository.Create(image);
            }

            entity.Id = product.Id;
        }

        public async Task DeleteProduct(ProductEntityDTO entity)
        {
            await _productRepository.Delete(entity.Id);
        }

        public async Task EditProduct(ProductEntityDTO entity)
        {
            var product = _mapper.Map<ProductEntityDTO, ProductEntity>(entity);

            var images = product.Images;

            var oldImages = _productImageRepository.GetAll().Where(x => x.ProductId == product.Id);

            product.Images = null;
            product.Category = null;
            product.Baskets = null;
            product.Sales_Products = null;
            product.OrderItems = null;

            await _productRepository.Update(product.Id, product);

            foreach (var oldImage in oldImages)
            {
                bool isDeleted = true;
                foreach (var image in images)
                {
                    if (oldImage.Id == image.Id)
                        isDeleted = false;
                }

                if (isDeleted)
                    using(EFAppContext context = new EFAppContext())
                    {
                        IProductImageRepository repository = new ProductImageRepository(context);
                        await repository.Delete(oldImage.Id);
                    }

            }

            foreach (var image in images)
            {
                if (image.ProductId == 0 && image.Id == 0)
                {
                    image.ProductId = product.Id;
                    await _productImageRepository.Create(image);
                }
                else
                {
                    await _productImageRepository.Update(image.Id, image);
                }
            }


        }

        public ICollection<ProductEntityDTO> GetAllProducts()
        {
            return _mapper.Map<ICollection<ProductEntity>, ICollection<ProductEntityDTO>>(_productRepository.GetProducts());
        }
    }
}
