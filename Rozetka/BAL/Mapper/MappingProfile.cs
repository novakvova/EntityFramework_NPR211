using AutoMapper;
using BAL.DTO.Models;
using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateDTOsToEntity();
            CreateEntityToDTOs();
        }

        private void CreateEntityToDTOs()
        {
            CreateMap<BasketEntity, BasketEntityDTO>()
                .ForMember(x => x.Product, opt => opt.MapFrom(x => x.Product))
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.User));

            CreateMap<ProductEntity, ProductEntityDTO>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category))
                .ForMember(x => x.Baskets, opt => opt.MapFrom(x => x.Baskets))
                .ForMember(x => x.Images, opt => opt.MapFrom(x => x.Images))
                .ForMember(x => x.OrderItems, opt => opt.MapFrom(x => x.OrderItems))
                .ForMember(x => x.Sales_Products, opt => opt.MapFrom(x => x.Sales_Products))
                .AfterMap((foo, dto) => { dto.Images = dto.Images.OrderBy(x => x.Priority).ToList(); });
                //.AfterMap((foo, dto) => { dto.Sales_Products = dto.Sales_Products.OrderBy(x => x.Sale.DecreasePercent).ToList(); });

            CreateMap<UserEntity, UserEntityDTO>()
                .ForMember(x => x.Baskets, opt => opt.MapFrom(x => x.Baskets))
                .ForMember(x => x.Orders, opt => opt.MapFrom(x => x.Orders))
                .ForMember(x => x.UserRoles, opt => opt.MapFrom(x => x.UserRoles));

            CreateMap<CategoryEntity, CategoryEntityDTO>()
                .ForMember(x => x.Products, opt => opt.MapFrom(x => x.Products));

            CreateMap<OrderEntity, OrderEntityDTO>()
                .ForMember(x => x.OrderStatus, opt => opt.MapFrom(x => x.OrderStatus))
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.User))
                .ForMember(x => x.OrderStatus, opt => opt.MapFrom(x => x.OrderStatus));

            CreateMap<OrderItemEntity, OrderItemEntityDTO>()
                .ForMember(x => x.Order, opt => opt.MapFrom(x => x.Order))
                .ForMember(x => x.Product, opt => opt.MapFrom(x => x.Product));

            CreateMap<OrderStatusEntity, OrderStatusEntityDTO>()
                .ForMember(x => x.Orders, opt => opt.MapFrom(x => x.Orders));

            CreateMap<UserRoleEntity, UserRoleEntityDTO>()
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.User))
                .ForMember(x => x.Role, opt => opt.MapFrom(x => x.Role));

            CreateMap<Sales_ProductEntity, Sales_ProductEntityDTO>()
                .ForMember(x => x.Product, opt => opt.MapFrom(x => x.Product))
                .ForMember(x => x.Sale, opt => opt.MapFrom(x => x.Sale));

            CreateMap<SaleEntity, SaleEntityDTO>()
                .ForMember(x => x.Sales_Products, opt => opt.MapFrom(x => x.Sales_Products));

            CreateMap<RoleEntity, RoleEntityDTO>()
                .ForMember(x => x.UserRoles, opt => opt.MapFrom(x => x.UserRoles));

            CreateMap<ProductImageEntity, ProductImageEntityDTO>()
                .ForMember(x => x.Product, opt => opt.MapFrom(x => x.Product));
        }

        private void CreateDTOsToEntity()
        {
            CreateMap<BasketEntityDTO, BasketEntity>()
                .ForMember(x => x.Product, opt => opt.MapFrom(x => x.Product))
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.User));

            CreateMap<ProductEntityDTO, ProductEntity>()
                .ForMember(x => x.Category, opt => opt.MapFrom(x => x.Category))
                .ForMember(x => x.Baskets, opt => opt.MapFrom(x => x.Baskets))
                .ForMember(x => x.Images, opt => opt.MapFrom(x => x.Images))
                .ForMember(x => x.OrderItems, opt => opt.MapFrom(x => x.OrderItems))
                .ForMember(x => x.Sales_Products, opt => opt.MapFrom(x => x.Sales_Products));

            CreateMap<UserEntityDTO, UserEntity>()
                .ForMember(x => x.Baskets, opt => opt.MapFrom(x => x.Baskets))
                .ForMember(x => x.Orders, opt => opt.MapFrom(x => x.Orders))
                .ForMember(x => x.UserRoles, opt => opt.MapFrom(x => x.UserRoles));

            CreateMap<CategoryEntityDTO, CategoryEntity>()
                .ForMember(x => x.Products, opt => opt.MapFrom(x => x.Products));

            CreateMap<OrderEntityDTO, OrderEntity>()
                .ForMember(x => x.OrderStatus, opt => opt.MapFrom(x => x.OrderStatus))
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.User))
                .ForMember(x => x.OrderStatus, opt => opt.MapFrom(x => x.OrderStatus));

            CreateMap<OrderItemEntityDTO, OrderItemEntity>()
                .ForMember(x => x.Order, opt => opt.MapFrom(x => x.Order))
                .ForMember(x => x.Product, opt => opt.MapFrom(x => x.Product));

            CreateMap<OrderStatusEntityDTO, OrderStatusEntity>()
                .ForMember(x => x.Orders, opt => opt.MapFrom(x => x.Orders));

            CreateMap<UserRoleEntityDTO, UserRoleEntity>()
                .ForMember(x => x.User, opt => opt.MapFrom(x => x.User))
                .ForMember(x => x.Role, opt => opt.MapFrom(x => x.Role));

            CreateMap<Sales_ProductEntityDTO, Sales_ProductEntity>()
                .ForMember(x => x.Product, opt => opt.MapFrom(x => x.Product))
                .ForMember(x => x.Sale, opt => opt.MapFrom(x => x.Sale));

            CreateMap<SaleEntityDTO, SaleEntity>()
                .ForMember(x => x.Sales_Products, opt => opt.MapFrom(x => x.Sales_Products));

            CreateMap<RoleEntityDTO, RoleEntity>()
                .ForMember(x => x.UserRoles, opt => opt.MapFrom(x => x.UserRoles));

            CreateMap<ProductImageEntityDTO, ProductImageEntity>()
                .ForMember(x => x.Product, opt => opt.MapFrom(x => x.Product));
        }
    }
}
