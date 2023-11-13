using AutoMapper;
using SalesSystem.Model;
using SalesSystem.Shared.DTOs;
using System.Globalization;

namespace SalesSystem.Util
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            #region Rol
            CreateMap<Role, RoleDTO>().ReverseMap();
            #endregion Rol

            #region Usuario
            CreateMap<User, UserDTO>()
                .ForMember(destination =>
                    destination.roleDescription,
                    opt => opt.MapFrom(origin => origin.RoleNavigationId.Description)
                );

            CreateMap<UserDTO, User>()
            .ForMember(destination =>
                destination.RoleNavigationId,
                opt => opt.Ignore()
            );

            CreateMap<UserDTO, User>()
                .ForMember(destination =>
                    destination.IsActive,
                    opt => opt.MapFrom(src => true)
                );
            #endregion Usuario

            #region Categoria
            CreateMap<Category, CategoryDTO>().ReverseMap();
            #endregion Categoria

            #region Producto


            CreateMap<Product, ProductDTO>();
            //.ForMember(destination =>
            //    destination.CategoryDescripction,
            //    opt => opt.MapFrom(origin => origin.CategoryNavigationId.Description)
            //);


            //.ForMember(destino =>
            //    destino.Precio,
            //    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
            //);

            CreateMap<ProductDTO, Product>()
            .ForMember(destination =>
                destination.CategoryNavigationId,
                opt => opt.Ignore()
            );
            //.ForMember(destiono =>
            //    destiono.Precio,
            //    opt => opt.MapFrom(origen => Convert.ToDecimal(origen.Precio, new CultureInfo("es-PE")))
            //);
            #endregion Producto

            #region Venta
            CreateMap<Sale, SaleDTO>();
            //CreateMap<Venta, VentaDTO>()
            //    .ForMember(destino =>
            //        destino.TotalTexto,
            //        opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
            //    ).ForMember(destino =>
            //        destino.FechaRegistro,
            //        opt => opt.MapFrom(origen => origen.FechaRegistro.Value.ToString("dd/MM/yyyy"))
            //    );
            CreateMap<SaleDTO, Sale>();
            //CreateMap<VentaDTO, Venta>()
            //    .ForMember(destino =>
            //        destino.Total,
            //        opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-PE")))
            //    );

            #endregion Venta

            #region DetalleVenta
            CreateMap<SaleDetail, SaleDetailDTO>()
                .ForMember(destination =>
                    destination.ProductDescripction,
                    opt => opt.MapFrom(origin => origin.ProductNavigationId.Name)
                );
            //.ForMember(destino =>
            //    destino.PrecioTexto,
            //    opt => opt.MapFrom(origen => Convert.ToString(origen.Precio.Value, new CultureInfo("es-PE")))
            //)
            //.ForMember(destino =>
            //    destino.TotalTexto,
            //    opt => opt.MapFrom(origen => Convert.ToString(origen.Total.Value, new CultureInfo("es-PE")))
            //);
            CreateMap<SaleDetailDTO, SaleDetail>();

            //CreateMap<DetalleVentaDTO, DetalleVenta>()
            //    .ForMember(destino =>
            //        destino.Precio,
            //        opt => opt.MapFrom(origen => Convert.ToDecimal(origen.PrecioTexto, new CultureInfo("es-PE")))
            //    )
            //    .ForMember(destino =>
            //        destino.Total,
            //        opt => opt.MapFrom(origen => Convert.ToDecimal(origen.TotalTexto, new CultureInfo("es-PE")))
            //    );
            #endregion

            #region Reporte
            CreateMap<SaleDetail, ReportDTO>()
                .ForMember(destination =>
                    destination.CreatedDate,
                    opt => opt.MapFrom(origin => origin.SaleNavigationId.CreatedDate.Value.ToString("dd/MM/yyyy"))
                )
                .ForMember(destination =>
                    destination.DocumentNumber,
                    opt => opt.MapFrom(origin => origin.SaleNavigationId.DocumentNumber)
                )
                .ForMember(destination =>
                    destination.PaymentType,
                    opt => opt.MapFrom(origin => origin.SaleNavigationId.PaymentType)
                )
                .ForMember(destination =>
                    destination.SaleTotal,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.SaleNavigationId.Total.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destination =>
                    destination.Product,
                    opt => opt.MapFrom(origin => origin.ProductNavigationId.Name)
                )
                .ForMember(destination =>
                    destination.Price,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Price.Value, new CultureInfo("es-PE")))
                )
                .ForMember(destination =>
                    destination.Total,
                    opt => opt.MapFrom(origin => Convert.ToString(origin.Total.Value, new CultureInfo("es-PE")))
                );
            #endregion Reporte
        }
    }
}