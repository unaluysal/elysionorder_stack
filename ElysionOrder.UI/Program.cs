using AutoMapper;
using ElysionOrder.Application.Services.CustomerServices;
using ElysionOrder.Application.Services.Mapping;
using ElysionOrder.Application.Services.ProductServices;
using ElysionOrder.Application.Services.ReportServices;
using ElysionOrder.Application.Services.RoleRightServices;
using ElysionOrder.Application.Services.RouteServices;
using ElysionOrder.Application.Services.SalesServices;
using ElysionOrder.Application.Services.UserServices;
using ElysionOrder.Infrastructure.Data.Context;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRoleRightService, RoleRightService>();
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<ISalesService, SalesService>();
builder.Services.AddScoped<IOrderService , OrderService>();
builder.Services.AddScoped<IRouteService , RouteService>();
builder.Services.AddScoped<IStoreService , StoreService>();
builder.Services.AddScoped<IStockService , StockService>();
builder.Services.AddScoped<IBillService , BillService>();
builder.Services.AddScoped<IPaymentService , PaymentService>();
builder.Services.AddScoped<IReportService , ReportService>();
builder.Services.AddScoped<IExpenseService , ExpenseService>();
builder.Services.AddMemoryCache();
builder.Services.AddSession();


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opts =>
    {
        opts.Cookie.Name = $".elysionorder.auth";
        opts.AccessDeniedPath = "/User/AccessDenied";
        opts.LoginPath = "/User/Login";
        opts.SlidingExpiration = true;
    });
       
builder.Services.AddAuthorization();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddSession(options =>
{
    options.Cookie.Name = $"elysionorder.session";
    options.IdleTimeout = TimeSpan.FromMinutes(480);
    options.Cookie.IsEssential = true;
});

builder.Services.AddDbContext<ElysionOrderDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("LocalConnectionString")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}/{id?}");

app.Run();
