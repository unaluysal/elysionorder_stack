namespace ElysionOrder.Application.Services.Dtos
{
    public class DashBoardDto
    {

        public DashBoardDto() {


            TodayFiveRouteCustomer = new List<string>();
            TopSeller = new List<ProductDto>();
            TopBuyer = new List<SalesDto>();
          


        }
      
        public int DailySalesCount { get; set; }
        public int DailySalesDoneCount { get; set; }
        public int LastDayTotalSales { get; set; }
        public double LastDayTotalSalesPercent { get; set; }
        public double DailySalesTotalPrice { get; set; }
        public double DailySalesLastDayTotalPrice { get; set; }
        public double DailySalesLastDayTotalPricePercent { get; set; }

        public List<string> TodayFiveRouteCustomer { get; set; }
        public List<ProductDto> TopSeller { get; set; }
        public List<SalesDto> TopBuyer { get; set; }
        
    }
}
