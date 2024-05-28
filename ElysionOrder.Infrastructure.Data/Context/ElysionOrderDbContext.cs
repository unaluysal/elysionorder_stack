using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.Context.Configuration;
using Microsoft.EntityFrameworkCore;
using DayOfWeek = ElysionOrder.Domain.Entitys.DayOfWeek;

namespace ElysionOrder.Infrastructure.Data.Context
{
    public class ElysionOrderDbContext : DbContext
    {
        public ElysionOrderDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ElysionOrder");
            modelBuilder.ApplyConfiguration(new UserTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new RightConfiguration());
            modelBuilder.ApplyConfiguration(new RoleRightConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new BrandConfiguration());
            modelBuilder.ApplyConfiguration(new ProductTypeConfiguration());
            modelBuilder.ApplyConfiguration(new SubProductTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new SalesStatusConfiguration());
            modelBuilder.ApplyConfiguration(new SalesConfiguration());
            modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());           
            modelBuilder.ApplyConfiguration(new TaxConfiguration());
            modelBuilder.ApplyConfiguration(new IbanConfiguration());
            modelBuilder.ApplyConfiguration(new DayOfWeekConfiguration());
            modelBuilder.ApplyConfiguration(new RouteConfiguration());
            modelBuilder.ApplyConfiguration(new RouteCustomerConfiguration());
            modelBuilder.ApplyConfiguration(new RouteUserConfiguration());
            modelBuilder.ApplyConfiguration(new StoreTypeConfiguration());
            modelBuilder.ApplyConfiguration(new StoreConfiguration());
            modelBuilder.ApplyConfiguration(new StockConfiguration());
            modelBuilder.ApplyConfiguration(new BillConfiguration());
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new BillSettingConfiguration());
            modelBuilder.ApplyConfiguration(new EBillingSettingConfiguration());
            modelBuilder.ApplyConfiguration(new BillTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BillItemConfiguration());
            modelBuilder.ApplyConfiguration(new PaymentWayConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());



        }

        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Right> Rights { get; set; }
        public DbSet<RoleRight> RoleRights { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductType>ProductTypes   { get; set; }
        public DbSet<SubProductType> SubProductTypes   { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<SalesStatus> SalesStatuses { get; set; }
        public DbSet<Sales> Saleses { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<Iban> Ibans { get; set; }
        public DbSet<DayOfWeek> DayOfWeeks { get; set; }
        public DbSet<Route> Routes { get; set; }

        public DbSet<RouteUser> RoutesUsers { get; set; }
        public DbSet<RouteCustomer> RoutesCustomers { get; set; }
        public DbSet<StoreType> StoreTypes { get; set; }
        public DbSet<Store> Stores { get; set; }

        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<Company> Companys { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<BillSetting> BillSettings{ get; set; }
        public DbSet<BillType> BillTypes{ get; set; }
        public DbSet<BillItem> BillItems{ get; set; }
        public DbSet<PaymentWay> PaymentWays{ get; set; }
        public DbSet<ExpenseType> ExpenseTypes{ get; set; }
        public DbSet<Expense> Expenses{ get; set; }
             
             
             
        

    }
}
