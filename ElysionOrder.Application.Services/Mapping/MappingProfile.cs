using AutoMapper;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Domain.Entitys;

namespace ElysionOrder.Application.Services.Mapping
{

    public class MappingProfile : Profile
    {
        
        public MappingProfile()
        {


            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
            CreateMap<Right, RightDto>();
            CreateMap<RightDto, Right>();

           

            CreateMap<RoleRightDto, RoleRight>();            

            CreateMap<UserType, UserTypeDto>();
            CreateMap<UserTypeDto, UserType>();
            CreateMap<CustomerTypeDto, CustomerType>();
            CreateMap<CustomerType, CustomerTypeDto>();
            var m1 = CreateMap<Customer, CustomerDto>();
            m1.AfterMap(((s, t) =>

            {


                if (s.CustomerType != null)
                {

                    t.CustomerTypeDto = new CustomerTypeDto();
                    t.CustomerTypeDto.Id = s.CustomerTypeId;
                    t.CustomerTypeDto.Description = s.Description;
                    t.CustomerTypeDto.Name = s.CustomerType.Name;
                    t.CustomerTypeDto.Status = s.CustomerType.Status;



                }
            }));
            CreateMap<CustomerDto, Customer>();

            var m2 = CreateMap<User, UserDto>();
            m2.AfterMap(((s, t) =>

            {


                if (s.UserType != null)
                {

                    t.UserTypeDto = new UserTypeDto();
                    t.UserTypeDto.Id = s.UserTypeId;
                    t.UserTypeDto.Description = s.UserType.Description;
                    t.UserTypeDto.Name = s.UserType.Name;
                    t.UserTypeDto.Status = s.UserType.Status;



                }
            }));
            CreateMap<UserDto, User>();

            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, Brand>();
            CreateMap<ProductType, ProductTypeDto>();
            CreateMap<ProductTypeDto, ProductType>();

            var m3 = CreateMap<SubProductType, SubProductTypeDto>();
            m3.AfterMap(((s, t) =>

            {


                if (s.ProductType != null)
                {

                    t.ProductTypeDto = new ProductTypeDto();
                    t.ProductTypeId = s.ProductTypeId;
                    t.ProductTypeDto.Id = s.ProductTypeId;
                    t.ProductTypeDto.Name = s.ProductType.Name;
                    t.ProductTypeDto.Description = s.ProductType.Description;
                   



                }
            }));
            CreateMap<SubProductTypeDto, SubProductType>();


            var m4 = CreateMap<Sales, SalesDto>();
            m4.AfterMap(((s, t) =>

            {


                if (s.SalesStatus != null)
                {

                    t.SalesStatusDto = new SalesStatusDto();
                    t.SalesStatusId = s.SalesStatusId;
                    t.SalesStatusDto.Id = s.SalesStatusId;
                    t.SalesStatusDto.Name = s.SalesStatus.Name;
                    t.SalesStatusDto.Description = s.SalesStatus.Description;
                    t.SalesStatusDto.LineNumber = s.SalesStatus.LineNumber;




                }
                if (s.Customer!=null)
                {
                    t.CustomerDto = new CustomerDto();
                    t.CustomerId = s.CustomerId;
                    t.CustomerDto.Id = s.CustomerId;
                    t.CustomerDto.Name = s.Customer.Name ;
                    t.CustomerDto.Description = s.Customer.Description;
                    t.CustomerDto.TaxNumber = s.Customer.TaxNumber;
                    t.CustomerDto.TaxOffice = s.Customer.TaxOffice;
                    t.CustomerDto.BillingAddRess = s.Customer.BillingAddRess;
                    t.CustomerDto.Status = s.Customer.Status;

                    if (s.Customer.CustomerType != null)
                    {
                        t.CustomerDto.CustomerTypeDto = new CustomerTypeDto();
                        t.CustomerDto.CustomerTypeId = s.Customer.CustomerTypeId;
                        t.CustomerDto.CustomerTypeDto.Id = s.Customer.CustomerTypeId;
                        t.CustomerDto.CustomerTypeDto.Name = s.Customer.CustomerType.Name;
                        t.CustomerDto.CustomerTypeDto.Description = s.Customer.CustomerType.Description;
                        t.CustomerDto.CustomerTypeDto.Status = s.Customer.CustomerType.Status;

                    }

                }
            }));
            CreateMap<SalesDto, Sales>();
            CreateMap<SalesStatus, SalesStatusDto>();
            CreateMap<SalesStatusDto, SalesStatus>();
            CreateMap<Currency, CurrencyDto>();
            CreateMap<CurrencyDto, Currency>();

            var m5 = CreateMap<Product, ProductDto>();
            m5.AfterMap(((s, t) =>

            {


                if (s.SubProductType != null)
                {

                    t.SubProductTypeDto = new SubProductTypeDto();
                    t.SubProductTypeId = s.SubProductTypeId;
                    t.SubProductTypeDto.Id = s.SubProductTypeId;
                    t.SubProductTypeDto.Name = s.SubProductType.Name;
                    t.SubProductTypeDto.Description = s.SubProductType.Description;

                    if (s.SubProductType.ProductType!=null)
                    {
                        t.SubProductTypeDto.ProductTypeDto = new ProductTypeDto();
                        t.SubProductTypeDto.ProductTypeDto.Id = s.SubProductType.ProductTypeId;
                        t.SubProductTypeDto.ProductTypeDto.Id = s.SubProductType.ProductTypeId;
                        t.SubProductTypeDto.ProductTypeDto.Name = s.SubProductType.ProductType.Name;
                        t.SubProductTypeDto.ProductTypeDto.Description = s.SubProductType.ProductType.Description;
                    }


                }
                if (s.Brand!=null)
                {
                    t.BrandDto = new BrandDto();
                    t.BrandId = s.BrandId;
                    t.BrandDto.Name = s.Brand.Name;
                    t.BrandDto.Description = s.Brand.Description;
                    t.BrandDto.Status = s.Brand.Status;
                }
                if(s.Tax!=null)
                {
                    t.TaxDto = new TaxDto();
                    t.TaxId = s.TaxId;
                    t.TaxDto.Name = s.Tax.Name;
                    t.TaxDto.Description = s.Tax.Description;
                    t.TaxDto.Rate = s.Tax.Rate;
                    t.TaxDto.Status = s.Tax.Status;

                }

                if (s.Currency != null)
                {
                    t.CurrencyDto = new CurrencyDto();
                    t.CurrencyId = s.CurrencyId;
                    t.CurrencyDto.Id = s.CurrencyId;
                    t.CurrencyDto.Name = s.Currency.Name;
                    t.CurrencyDto.Description = s.Currency.Description;                   
                    t.CurrencyDto.Status = s.Currency.Status;

                }


            }));
            CreateMap<ProductDto, Product>();

            var m6 = CreateMap<Order, OrderDto>();
            m6.AfterMap(((s, t) =>

            {
                

                

                if (s.Sales != null)
                {
                    t.SalesDto = new SalesDto();
                    t.SalesId = s.SalesId;
                    t.SalesDto.Id = s.SalesId;
                   
                }
                if (s.Product!=null)
                {
                    t.ProductDto = new ProductDto();
                    t.ProductId = s.ProductId;
                    t.ProductDto.Id = s.ProductId;
                    t.ProductDto.Name = s.Product.Name;
                    t.ProductDto.Price = s.Product.Price;
                    t.ProductDto.Status = s.Product.Status;

                    if (s.Product.Tax!=null)
                    {
                        t.ProductDto.TaxDto= new TaxDto();
                        t.ProductDto.Id = s.Product.TaxId;
                        t.ProductDto.TaxDto.Id= s.Product.TaxId;
                        t.ProductDto.TaxDto.Name=s.Product.Tax.Name;
                        t.ProductDto.TaxDto.Rate = s.Product.Tax.Rate;
                        t.ProductDto.TaxDto.Status = s.Product.Tax.Status;

                    }
                    if (s.Product.Currency != null)
                    {
                        t.ProductDto.CurrencyDto = new CurrencyDto();
                        t.ProductDto.Id = s.Product.CurrencyId;
                        t.ProductDto.CurrencyDto.Id = s.Product.CurrencyId;
                        t.ProductDto.CurrencyDto.Name = s.Product.Currency.Name;                
                        t.ProductDto.CurrencyDto.Status = s.Product.Currency.Status;

                    }

                }
               
            }));
            CreateMap<OrderDto, Order>();

            CreateMap<Currency, CurrencyDto>();
            CreateMap<CurrencyDto, Currency>();
            CreateMap<Tax, TaxDto>();
            CreateMap<TaxDto, Tax>();

   

            var m8 = CreateMap<RoleRight, RoleRightDto>();
            m8.AfterMap(((s, t) =>

            {

                if (s.Role != null)
                {
                    t.RoleDto = new RoleDto();
                    t.RoleId = s.RoleId;
                    t.RoleDto.Id = s.RoleId;
                    t.RoleDto.Name = s.Role.Name;
                    t.RoleDto.Description = s.Role.Description;

                
                }
                if (s.Right != null)
                {
                    t.RightDto = new RightDto();
                    t.RightId = s.RightId;
                    t.RightDto.Id = s.RightId;
                    t.RightDto.Name = s.Right.Name;
                    t.RightDto.Description = s.Right.Description;


                }

                



            }));

            CreateMap<IbanDto, Iban>();
            var m9 = CreateMap<Iban, IbanDto>();
            m9.AfterMap(((s, t) =>

            {

                if (s.Currency != null)
                {
                    t.CurrencyDto = new CurrencyDto();
                    t.CurrencyId = s.CurrencyId;
                    t.CurrencyDto.Id = s.CurrencyId;
                    t.CurrencyDto.Name = s.Currency.Name;
                    t.CurrencyDto.Description = s.Currency.Description;
                   


                }
               



            }));


            CreateMap<ElysionOrder.Domain.Entitys.DayOfWeek, DayOfWeekDto>();
            CreateMap<DayOfWeekDto, ElysionOrder.Domain.Entitys.DayOfWeek>();

            CreateMap<RouteDto, Route>();
            var m10 = CreateMap<Route, RouteDto>();
            m10.AfterMap(((s, t) =>

            {

                if (s.DayOfWeek != null)
                {
                    t.DayOfWeekDto = new DayOfWeekDto();
                    t.DayOfWeekId = s.DayOfWeekId;
                    t.DayOfWeekDto.Id = s.DayOfWeekId;
                    t.DayOfWeekDto.Name = s.DayOfWeek.Name;
                    t.DayOfWeekDto.Description = s.DayOfWeek.Description;



                }




            }));

            CreateMap<UserRoleDto, UserRole>();
               var m11= CreateMap<UserRole, UserRoleDto>();
            m11.AfterMap(((s, t) =>

            {

                if (s.User != null)
                {
                    t.UserDto = new UserDto();
                    t.UserDto.Id = s.UserId;
                    t.UserDto.Name = s.User.Name;
                    t.UserDto.LastName = s.User.LastName;
                    t.UserDto.Phone = s.User.Phone;
                    t.UserDto.Email = s.User.Email;



                }
                if (s.Role != null)
                {
                    t.RoleDto = new RoleDto();
                    t.RoleDto.Id = s.UserId;
                    t.RoleDto.Name = s.Role.Name;
                    t.RoleDto.Description = s.Role.Description;
                    
                }



            }));

            CreateMap<RouteUserDto, RouteUser>();
            var m12 = CreateMap<RouteUser, RouteUserDto>();
            m12.AfterMap(((s, t) =>
            {
                if (s.User != null)
                {
                    t.UserDto = new UserDto();
                    t.UserDto.Id = s.UserId;
                    t.UserDto.Name = s.User.Name;
                    t.UserDto.LastName = s.User.LastName;
                    t.UserDto.Phone = s.User.Phone;
                    t.UserDto.Email = s.User.Email;



                }
                if (s.Route!=null)
                {
                    t.RouteDto = new RouteDto();
                    t.RouteDto.Id = s.RouteId;
                    t.RouteDto.Name = s.Route.Name;
                  

                }


            }));

            CreateMap<RouteCustomerDto, RouteCustomer>();
            var m13 = CreateMap<RouteCustomer, RouteCustomerDto>();
            m13.AfterMap(((s, t) =>
            {
                if (s.Customer != null)
                {
                    t.CustomerDto = new CustomerDto();
                    t.CustomerDto.Id = s.CustomerId;
                    t.CustomerDto.Name = s.Customer.Name;
                   



                }
                if (s.Route != null)
                {
                    t.RouteDto = new RouteDto();
                    t.RouteDto.Id = s.RouteId;
                    t.RouteDto.Name = s.Route.Name;


                }


            }));

            CreateMap<StoreType, StoreTypeDto>();
            CreateMap<StoreTypeDto, StoreType>();

            CreateMap<StoreDto, Store>();
            var m14 = CreateMap<Store, StoreDto>();
            m14.AfterMap(((s, t) =>
            {
                if (s.StoreType != null)
                {
                    t.StoreTypeDto = new StoreTypeDto();
                    t.StoreTypeDto.Id = s.StoreType.Id;
                    t.StoreTypeDto.Name = s.StoreType.Name;
                    t.StoreTypeDto.Description = s.StoreType.Description;
                    t.StoreTypeDto.Status = s.StoreType.Status;




                }
            


            }));


            CreateMap<StockDto, Stock>();
            var m15 = CreateMap<Stock, StockDto>();
            m15.AfterMap(((s, t) =>
            {
                if (s.Store != null)
                {
                    t.StoreDto = new StoreDto();
                    t.StoreDto.Id = s.Store.Id;
                    t.StoreDto.Name = s.Store.Name;
                    t.StoreDto.Description = s.Store.Description;
                    t.StoreDto.Status = s.Store.Status;

                    if (s.Store.StoreType!=null)
                    {
                        t.StoreDto.StoreTypeDto = new StoreTypeDto();
                        t.StoreDto.StoreTypeDto.Id = s.Store.StoreType.Id;
                        t.StoreDto.StoreTypeDto.Name = s.Store.StoreType.Name;
                        t.StoreDto.StoreTypeDto.Description = s.Store.StoreType.Description;
                        t.StoreDto.StoreTypeDto.Status = s.Store.StoreType.Status;
                    }




                }

                if (s.Product!=null)
                {
                    t.ProductDto = new ProductDto();
                    t.ProductDto.Id = s.Product.Id;
                    t.ProductDto.Name = s.Product.Name;
                    t.ProductDto.Description = s.Product.Description;
                    t.ProductDto.Status = s.Product.Status;

                    if (s.Product.SubProductType != null)
                    {
                        t.ProductDto.SubProductTypeDto = new SubProductTypeDto();
                        t.ProductDto.SubProductTypeDto.Id = s.Product.SubProductTypeId;
                        t.ProductDto.SubProductTypeDto.Name = s.Product.SubProductType.Name;
                        t.ProductDto.SubProductTypeDto.Description = s.Product.SubProductType.Description;
                        t.ProductDto.SubProductTypeDto.Status = s.Product.SubProductType.Status;

                        if (s.Product.SubProductType.ProductType!=null)
                        {
                            t.ProductDto.SubProductTypeDto.ProductTypeDto = new ProductTypeDto();
                            t.ProductDto.SubProductTypeDto.ProductTypeDto.Id = s.Product.SubProductType.ProductTypeId;
                            t.ProductDto.SubProductTypeDto.ProductTypeDto.Name = s.Product.SubProductType.ProductType.Name;
                            t.ProductDto.SubProductTypeDto.ProductTypeDto.Description = s.Product.SubProductType.ProductType.Description;
                            t.ProductDto.SubProductTypeDto.ProductTypeDto.Status = s.Product.SubProductType.ProductType.Status;
                        }
                    }

                    if (s.Product.Brand!=null)
                    {
                        t.ProductDto.BrandDto = new BrandDto();
                        t.ProductDto.BrandDto.Id = s.Product.Brand.Id;
                        t.ProductDto.BrandDto.Name = s.Product.Brand.Name;                        
                        t.ProductDto.BrandDto.Status = s.Product.Brand.Status;

                    }
                }

            }));

      
            CreateMap<BillType, BillTypeDto>();
            CreateMap<BillTypeDto, BillType>();
          

            CreateMap<BillDto, Bill>();
            var m16 = CreateMap<Bill, BillDto>();
            m16.AfterMap(((s, t) =>
            {

                if (s.BillType!=null)
                {
                    t.BillTypeDto = new BillTypeDto();
                    t.BillTypeDto.Id = s.BillType.Id;
                    t.BillTypeDto.Name = s.BillType.Name;
                    t.BillTypeDto.Description = s.BillType.Description;
                }
             

            }));

            CreateMap<Company, CompanyDto>();
            CreateMap<CompanyDto, Company>();
            CreateMap<PaymentType, PaymentTypeDto>();
            CreateMap<PaymentTypeDto, PaymentType>();

            CreateMap<PaymentDto, Payment>();
            var m17 = CreateMap<Payment, PaymentDto>();
            m17.AfterMap(((s, t) =>
            {
                if (s.PaymentType != null)
                {
                    t.PaymentTypeDto = new PaymentTypeDto();
                    t.PaymentTypeId = s.PaymentTypeId;
                    t.PaymentTypeDto.Id= s.PaymentTypeId;
                    t.PaymentTypeDto.Name = s.PaymentType.Name;
                    t.PaymentTypeDto.Description = s.PaymentType.Description;
                    t.PaymentTypeDto.Type = s.PaymentType.Type;

                }

                if (s.PaymentWay!=null)
                {
                    t.PaymentWayDto = new PaymentWayDto();
                    t.PaymentWayId = s.PaymentWayId;
                    t.PaymentWayDto.Id = s.PaymentWayId;
                    t.PaymentWayDto.Name = s.PaymentWay.Name;
                    t.PaymentWayDto.Description = s.PaymentWay.Description;
                   
                }



            }));


            CreateMap<BillSetting, BillSettingDto>();
            CreateMap< BillSettingDto, BillSetting >();
            CreateMap< EBillSetting, EBillSettingDto>();
            CreateMap<EBillSettingDto, EBillSetting>();

            CreateMap<BillItemDto, BillItem>();
            var m18 = CreateMap<BillItem, BillItemDto>();
            m18.AfterMap(((s, t) =>
            {
                if (s.Product != null)
                {
                    t.ProductDto = new ProductDto();
                    t.ProductDto.Id = s.ProductId;
                    t.ProductDto.Name = s.Product.Name;
                    t.ProductDto.Status = s.Product.Status;
               

                }
                if (s.Tax != null)
                {
                    t.TaxDto = new TaxDto();
                    t.TaxDto.Id = s.TaxId;
                    t.TaxDto.Name = s.Tax.Name;
                    t.TaxDto.Status = s.Tax.Status;


                }
                if (s.Store != null)
                {
                    t.StoreDto = new StoreDto();
                    t.StoreDto.Id = s.StoreId;
                    t.StoreDto.Name = s.Store.Name;
                    t.StoreDto.Status = s.Store.Status;


                }



            }));

            CreateMap<PaymentWay, PaymentWayDto>();
            CreateMap<PaymentWayDto, PaymentWay>();


            CreateMap<ExpenseType, ExpenseTypeDto>();
            CreateMap<ExpenseTypeDto, ExpenseType>();


            CreateMap<ExpenseDto, Expense>();
            var m19 = CreateMap<Expense, ExpenseDto>();
            m19.AfterMap(((s, t) =>
            {
                if (s.ExpenseType != null)
                {
                    t.ExpenseTypeDto = new ExpenseTypeDto();
                    t.ExpenseTypeDto.Id = s.ExpenseTypeId;
                    t.ExpenseTypeDto.Name = s.ExpenseType.Name;
                    t.ExpenseTypeDto.Status = s.ExpenseType.Status;


                }
            

            }));
        }
    }
}
