using AutoMapper;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace ElysionOrder.Application.Services.ReportServices
{
    public class ReportService : IReportService
    {
       private readonly IUnitOfWork _unitOfWrok;
        private readonly IMapper _mapper;
        public ReportService(IUnitOfWork unitOfWork, IMapper mapper) {

            _unitOfWrok=unitOfWork;
            _mapper=mapper;

        }
        public async Task<DashBoardDto> TodayDashBoardAsync()
        {
            DashBoardDto dashBoardDto = new DashBoardDto();
            var datetime = DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000");
            var sales = await _unitOfWrok.GetRepository<Sales>().GetAll().Include(x=>x.Customer).ThenInclude(x=>x.CustomerType).Include(x=>x.SalesStatus).Where(x => x.Status && x.SalesStatus.Status && x.CreateTime >= Convert.ToDateTime(datetime)).ToListAsync();
            dashBoardDto.DailySalesCount = sales.Count;
            dashBoardDto.DailySalesDoneCount = sales.Count(x =>  x.SalesStatus.Name == "Ürünler Teslim Edildi");
            var lastdatetime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd 00:00:00.000");

            var lastdate = await _unitOfWrok.GetRepository<Sales>().GetAll().Include(x => x.SalesStatus).Where(x => x.CreateTime >= Convert.ToDateTime(lastdatetime) && x.CreateTime < Convert.ToDateTime(datetime) && x.Status).ToListAsync() ;
            dashBoardDto.LastDayTotalSales = lastdate.Count;
            dashBoardDto.LastDayTotalSalesPercent = (Convert.ToDouble(dashBoardDto.DailySalesCount- dashBoardDto.LastDayTotalSales) / Convert.ToDouble(dashBoardDto.LastDayTotalSales)) * 100;
    

            foreach (var item in sales.Select(x=>x.Id))
            {
                //var tt =await _unitOfWrok.GetRepository<Order>().GetAll().Include(x => x.PriceList).Where(x => x.Status && x.SalesId == item).ToListAsync();

                //foreach (var tem in tt)
                //{
                //    dashBoardDto.DailySalesTotalPrice = dashBoardDto.DailySalesTotalPrice + (tem.Piece * tem.PriceList.Price);
                //}


            }

            foreach (var item in lastdate.Select(x => x.Id))
            {
                //var tt = await _unitOfWrok.GetRepository<Order>().GetAll().Include(x => x.PriceList).Where(x => x.Status && x.SalesId == item).ToListAsync();

                //foreach (var tem in tt)
                //{
                //    dashBoardDto.DailySalesLastDayTotalPrice = dashBoardDto.DailySalesLastDayTotalPrice + (tem.Piece * tem.PriceList.Price);
                //}


            }

            dashBoardDto.DailySalesLastDayTotalPricePercent =Math.Round( (Convert.ToDouble(dashBoardDto.DailySalesTotalPrice - dashBoardDto.DailySalesLastDayTotalPrice) / Convert.ToDouble(dashBoardDto.DailySalesLastDayTotalPrice)) * 100,2);

            DateTime now = DateTime.Now;
            CultureInfo turkishCulture = new CultureInfo("tr-TR");
            System.DayOfWeek dayOfWeek = turkishCulture.Calendar.GetDayOfWeek(now);
            string turkishDayOfWeek = turkishCulture.DateTimeFormat.GetDayName(dayOfWeek);
            var customerList = await _unitOfWrok.GetRepository<RouteCustomer>()
    .GetAll()
    .Include(x => x.Customer)
    .Include(x => x.Route)
    .Include(x => x.Route.DayOfWeek)
    .Where(x => x.Route.DayOfWeek.Name == turkishDayOfWeek && x.Status && x.Customer.Status && x.Route.Status)
    .Select(x => x.Customer)
    .Distinct()
    .ToListAsync();

            var l = customerList.Take(5);
            foreach (var item in l)
            {
                dashBoardDto.TodayFiveRouteCustomer.Add(item.Name);
            }


            //var order =await _unitOfWrok.GetRepository<Order>().GetAll().Include(x=>x.PriceList.Product.SubProductType.ProductType).Include(x=>x.Sales)
            //    .Where(x => x.Status && x.Sales.CreateTime >= Convert.ToDateTime(datetime)).ToListAsync() ;

            //var b = order.GroupBy(x => x.PriceList.Product).OrderBy(x => x.Count()).Take(5).Select(x=>x.Key).ToList();

            //dashBoardDto.TopSeller = _mapper.Map<List<ProductDto>>( b);

            //var aktis = sales.Where(x => x.SalesStatus.Name != "Iptal").ToList();

            //var oc = aktis.OrderBy(x => x.CustomerId).ToList();
            //List<SalesDto> calc = new List<SalesDto>();
            //foreach (var item in oc)
            //{
            //    var ss = aktis.Where(x=>x.CustomerId== item.CustomerId).ToList();
            //    SalesDto sc =new SalesDto();

            //    sc.CustomerId= item.CustomerId;
            //    sc.CustomerDto = new CustomerDto();
            //    sc.CustomerDto.Name = item.Customer.Name;
            //    sc.CustomerDto.CustomerTypeDto = new CustomerTypeDto();
            //    sc.CustomerDto.CustomerTypeDto.Name= item.Customer.CustomerType.Name;
            //    foreach (var tem in ss)
            //    {
            //        var orders =await _unitOfWrok.GetRepository<Order>().GetAll().Include(x=>x.PriceList).ThenInclude(x=>x.Tax).Where(x => x.Status && x.SalesId == tem.Id).ToListAsync();
                    
            //        foreach (var te in orders)
            //        {
            //            var totalprice = te.PriceList.Price * te.Piece;
            //            var totaltax = totalprice * te.PriceList.Tax.Rate / 100;
            //            var wt = totalprice + totaltax;

            //            sc.OrderTotalPrice = sc.OrderTotalPrice + wt;
            //        }

            //    }
            //    calc.Add(sc);
            //}

            //var top = calc.OrderBy(x => x.OrderTotalPrice).Take(5).ToList();
            //dashBoardDto.TopBuyer = top;

            return dashBoardDto;

        }
    }
}
