using AutoMapper;
using BAL.DTO.Models;
using BAL.Interfaces;
using BAL.Mapper;
using BAL.Utilities;
using DAL.Constants;
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
    public class SaleService
    {
        private readonly SaleRepository _saleRepository;
        private readonly IMapper _mapper;
        public SaleService()
        {
            EFAppContext context = new EFAppContext();
            _saleRepository = new SaleRepository(context);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            _mapper = configuration.CreateMapper();
        }

        public async Task CreateSale(SaleEntityDTO saleDTO)
        {
            var sale = _mapper.Map<SaleEntityDTO, SaleEntity>(saleDTO);
            sale.Sales_Products = null;
            await _saleRepository.Create(sale);
        }

        public async Task EditSale(SaleEntityDTO saleDTO)
        {
            var sale = _mapper.Map<SaleEntityDTO, SaleEntity>(saleDTO);
            sale.Sales_Products = null;
            await _saleRepository.Update(sale.Id, sale);
        }

        public async Task DeleteSale(SaleEntityDTO saleDTO)
        {
            await _saleRepository.Delete(saleDTO.Id);
        }

        public async Task AddSalesProduct(Sales_ProductEntityDTO entityDTO)
        {
            await _saleRepository.AddSalesProduct(new Sales_ProductEntity()
            {
                ProductId = entityDTO.ProductId,
                SaleId = entityDTO.SaleId
            });
        }

        public async Task DeleteSalesProduct(Sales_ProductEntityDTO entityDTO)
        {
            await _saleRepository.DeleteSalesProduct(new Sales_ProductEntity()
            {
                ProductId = entityDTO.ProductId,
                SaleId = entityDTO.SaleId
            });
        }

        public IEnumerable<SaleEntityDTO> GetAllSales()
        {
            return _mapper.Map<IEnumerable<SaleEntity>, IEnumerable<SaleEntityDTO>>(_saleRepository.GetAllSales());
        }
    }
}
