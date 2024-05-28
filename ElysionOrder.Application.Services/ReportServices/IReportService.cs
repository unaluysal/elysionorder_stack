using ElysionOrder.Application.Services.Dtos;

namespace ElysionOrder.Application.Services.ReportServices
{
    public interface IReportService
    {
        public Task<DashBoardDto> TodayDashBoardAsync();
        
    }
}
