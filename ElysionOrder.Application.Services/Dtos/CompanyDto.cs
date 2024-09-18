namespace ElysionOrder.Application.Services.Dtos
{
    public class CompanyDto :BaseDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string WebSite { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }

        public string Address { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public string EBillRole { get; set; }
       
    }
}
