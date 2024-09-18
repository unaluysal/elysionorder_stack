using AutoMapper;
using EFaturaEdm;
using ElysionOrder.Application.Services.Dtos;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text;

namespace ElysionOrder.Application.Services.SalesServices
{
    public class BillService : IBillService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ISalesService _salesService;

        private string EdmSessionId;
        private readonly EFaturaEDMPortClient _client;
        private Company _company;
        private EBillSetting _ebillSettings;

        public BillService(IUnitOfWork unitOfWork, IMapper mapper, ISalesService salesService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            _client = new EFaturaEdm.EFaturaEDMPortClient();
            _company = new Company();
            _ebillSettings = new EBillSetting();
            EdmSessionId = string.Empty;
            _salesService=salesService;



        }

     

        //public async Task AddBillAfterSalesAsync(Guid id)
        //{
        //  //  var sales  =await _unitOfWork.GetRepository<Sales>().GetByIdAsync ( id);

        //  //  if (sales!=null)
        //  //  {
        //  //      Bill bill = new Bill();
        //  //      bill.SalesId= sales.Id;
        //  //      bill.BillNumber = 1;

        //  //     await _unitOfWork.GetRepository<Bill>().AddAsync(bill);
        //  //  }
        //  //await  _unitOfWork.SaveChangesAsync();
        //}

        public async Task AddBillAsync(BillDto billDto)
        {
            var b = _mapper.Map<Bill>(billDto);
            await _unitOfWork.GetRepository<Bill>().AddAsync(b);
            await _unitOfWork.SaveChangesAsync();
        }

        public Task AddCompanyAsync(CompanyDto companyDto)
        {
            throw new NotImplementedException();
        }



        public async Task AddSupplierBillAsync(BillDto billDto)
        {

            var b = _mapper.Map<Bill>(billDto);
            var bt = await _unitOfWork.GetRepository<BillType>().GetFirstWhereAsync(x => x.Status && x.Name == "Gelen Fatura");
            b.BillTypeId = bt.Id;


        }

        public async Task DeleteBillAsync(Guid id)
        {
            var b = await _unitOfWork.GetRepository<Bill>().GetByIdAsync(id);
            b.Status = false;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteCompanyAsync(Guid id)
        {
            var c = await _unitOfWork.GetRepository<Company>().GetByIdAsync(id);
            c.Status = false;
            _unitOfWork.GetRepository<Company>().Update(c);
            await _unitOfWork.SaveChangesAsync();
        }



        public async Task EditBillSettingAsync(BillSettingDto billSettingDto)
        {
            var b = _mapper.Map<BillSetting>(billSettingDto);
            b.Status = true;
            _unitOfWork.GetRepository<BillSetting>().Update(b);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EditCompanyAsync(CompanyDto companyDto)
        {
            var c = _mapper.Map<Company>(companyDto);
            c.Status = true;
            _unitOfWork.GetRepository<Company>().Update(c);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EditEBillSettingAsync(EBillSettingDto eBillSettingDto)
        {
            var e = _mapper.Map<EBillSetting>(eBillSettingDto);
            e.Status = true;
            _unitOfWork.GetRepository<EBillSetting>().Update(e);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<BillDto>> GetAllBillsAsync()
        {
            var bill = await _unitOfWork.GetRepository<Bill>().GetAll().Include(x => x.BillType)
                .Where(x => x.Status && x.BillType.Status).ToListAsync();
            var mb = _mapper.Map<List<BillDto>>(bill);
            foreach (var b in mb)
            {

                var items = await _unitOfWork.GetRepository<BillItem>().GetAll().Where(x => x.Status && x.BillId == b.Id).ToListAsync();
                b.BillItems = _mapper.Map<List<BillItemDto>>(items);
                var k = await _unitOfWork.GetRepository<Customer>().GetFirstWhereAsync(x => x.Id == b.InvoicerId);
                b.InvoicerDto = _mapper.Map<CustomerDto>(k);
                var kk = await _unitOfWork.GetRepository<Customer>().GetFirstWhereAsync(x => x.Id == b.CustomerId);
                b.RecipientDto = _mapper.Map<CustomerDto>(kk);

                b.Total = items.Sum(x => x.Total);

            }

            return mb;
        }

        public async Task<List<BillSettingDto>> GetAllBillSettingsAsync()
        {
            var list = await _unitOfWork.GetRepository<BillSetting>().GetWhere(x => x.Status).ToListAsync();
            var mlist = _mapper.Map<List<BillSettingDto>>(list);
            return mlist;
        }

        //public async Task<List<BillDto>> GetAllBillsWithCustomerIdAsync(Guid id)
        //{
        //    var bill = await _unitOfWork.GetRepository<Bill>().GetAll().Include(x => x.Sales).ThenInclude(x => x.Customer).Include(x => x.Sales.SalesStatus)
        //        .Where(x => x.Status && x.Sales.Status && x.Sales.CustomerId==id ).ToListAsync();
        //    var mb = _mapper.Map<List<BillDto>>(bill);
        //    return mb;
        //}

        public async Task<List<CompanyDto>> GetAllCompanysAsync()
        {
            var list = await _unitOfWork.GetRepository<Company>().GetWhere(x => x.Status).ToListAsync();
            var ml = _mapper.Map<List<CompanyDto>>(list);
            return ml;
        }



        public async Task<List<EBillSettingDto>> GetAllEbillSettingsAsync()
        {
            var bl = await _unitOfWork.GetRepository<EBillSetting>().GetWhere(x => x.Status).ToListAsync();
            var bld = _mapper.Map<List<EBillSettingDto>>(bl);
            return bld;
        }



        public async Task<BillSettingDto> GetBillSettingWithIdAsync(Guid id)
        {
            var bil = await _unitOfWork.GetRepository<BillSetting>().GetByIdAsync(id);
            var mb = _mapper.Map<BillSettingDto>(bil);
            return mb;
        }

       

        public async Task<CompanyDto> GetCompanyWithAsync(Guid id)
        {
            var c = await _unitOfWork.GetRepository<Company>().GetByIdAsync(id);
            return _mapper.Map<CompanyDto>(c);
        }

        public async Task<EBillSettingDto> GetEBillSettingWithIdAsync(Guid id)
        {
            var ebil = await _unitOfWork.GetRepository<EBillSetting>().GetFirstWhereAsync(x => x.Status && x.Id == id);
            var me = _mapper.Map<EBillSettingDto>(ebil);
            return me;
        }



        public async Task<string> LoginEdmAsync()
        {
            _company = await _unitOfWork.GetRepository<Company>().GetFirstWhereAsync(x => x.Status);
            _ebillSettings = await _unitOfWork.GetRepository<EBillSetting>().GetFirstWhereAsync(x => x.Status && x.ChannelName == _company.EBillRole);
            _client.Endpoint.Address = new System.ServiceModel.EndpointAddress(_ebillSettings.Url);

            EFaturaEdm.LoginRequest loginRequest = new EFaturaEdm.LoginRequest();

            loginRequest.USER_NAME = _ebillSettings.UserName;
            loginRequest.PASSWORD = _ebillSettings.Password;
            loginRequest.REQUEST_HEADER =  GetEdmRequestHeader();


            var res = await _client.LoginAsync(loginRequest);
            if (res.LoginResponse != null)
            {
                EdmSessionId = res.LoginResponse.SESSION_ID;
                return res.LoginResponse.SESSION_ID;
            }

            return string.Empty;
        }

        public async Task LogOutEdmAsync(string sessionId)
        {

            LogoutRequest logoutRequest = new LogoutRequest();
            logoutRequest.REQUEST_HEADER =  GetEdmRequestHeader();

            var res = await _client.LogoutAsync(logoutRequest);


        }



        private  REQUEST_HEADERType GetEdmRequestHeader()
        {
            var reqHeader = new REQUEST_HEADERType();

            


            reqHeader.SESSION_ID = EdmSessionId;
            reqHeader.ACTION_DATE = DateTime.Now;
            reqHeader.ACTION_DATESpecified = true;
            reqHeader.REASON = _ebillSettings.Reason;
            reqHeader.APPLICATION_NAME = _ebillSettings.ApplicationName;
            reqHeader.CHANNEL_NAME = _ebillSettings.ChannelName;
            reqHeader.HOSTNAME = _ebillSettings.HostName;
            reqHeader.COMPRESSED = _ebillSettings.Compressed;

            return reqHeader;



        }

        public async Task SendInvoiceAsync(Guid salesId)
        {

            // EFaturaEDMTest client oluşturulması
            SendInvoiceRequest sendInvoiceRequest = new SendInvoiceRequest();

            // Login işlemi
            var token = await LoginEdmAsync();

            sendInvoiceRequest.REQUEST_HEADER =  GetEdmRequestHeader();


            string ublXml = "";
            bool earsiv = false;


            var sal = await _salesService.GetSalesWithIdAsync(salesId);

            var trm = new GetTurmobRequest();
            trm.REQUEST_HEADER = sendInvoiceRequest.REQUEST_HEADER;
            trm.VKN = sal.CustomerDto.TaxNumber;

            var rep = await _client.GetTurmobAsync(trm);

            var customerGibResult = await this.GetCustomerBillTypeAsync(sal.CustomerId);

            sendInvoiceRequest.RECEIVER = new SendInvoiceRequestRECEIVER();

            if (customerGibResult.CheckUserResponse != null & customerGibResult.CheckUserResponse.Length > 0)
            {
                sendInvoiceRequest.RECEIVER.vkn = customerGibResult.CheckUserResponse[0].IDENTIFIER;
                sendInvoiceRequest.RECEIVER.alias = customerGibResult.CheckUserResponse[0].ALIAS;
                sal.CustomerDto.Description = customerGibResult.CheckUserResponse[0].ALIAS;
                ublXml =  EFaturaCreateUblXml(sal, rep);
                earsiv = false;

            }
            else
            {
                sendInvoiceRequest.RECEIVER.vkn = sal.CustomerDto.TaxNumber;

                earsiv = true;
                ublXml = EArsivCreateUblXml(sal,rep);

            }




            byte[] ublBytes = Encoding.UTF8.GetBytes(ublXml);
            string base64Ubl = Convert.ToBase64String(ublBytes);

            File.WriteAllBytes(@"C:\Users\Unal\Desktop\ubl\earsiv.xml", ublBytes);


            var lst = new List<INVOICE>
    {
        new INVOICE
        {
            HEADER = new INVOICEHEADER
            {
                SENDER = _company.TaxNumber,
                RECEIVER = rep.GetTurmobResponse.MUKELLEF.vkn,
                FROM = "urn:mail:"+_company.Mail,
                TO = sendInvoiceRequest.RECEIVER.alias,
                INTERNETSALES = false,
                EARCHIVE = earsiv,                             
                EARCHIVE_REPORT_SENDDATE = DateTime.Now,
                CANCEL_EARCHIVE_REPORT_SENDDATE = DateTime.Now
            },
            CONTENT = new base64Binary
            {
                contentType = "XML",
                Value = ublBytes
            }
        }
    };

            // SendInvoiceRequest'e INVOICE listesi ekleme
            sendInvoiceRequest.INVOICE = lst.ToArray();


            // Fatura gönderme işlemi


           var resp = await _client.SendInvoiceAsync(sendInvoiceRequest);

           await this.LogOutEdmAsync(EdmSessionId);


        }


        private async Task< EDM_GetTurmobResponseMessage> GetTurmobAsync(string VKN)
        {

            var trm = new GetTurmobRequest();
            trm.REQUEST_HEADER = GetEdmRequestHeader();
            trm.VKN = VKN;

            var rep = await _client.GetTurmobAsync(trm);
            return rep;
        }



        private string EFaturaCreateUblXml(SalesDto salesDto,EDM_GetTurmobResponseMessage trm)
        {


           
            var invoiceLines = new StringBuilder();

            for (int i = 0; i < salesDto.OrderDtos.Count; i++)
            {
                var order = salesDto.OrderDtos[i];
                invoiceLines.Append($@"
        <cac:InvoiceLine>
            <cbc:ID>{i + 1}</cbc:ID>
            <cbc:InvoicedQuantity unitCode=""C62"">{order.Piece}</cbc:InvoicedQuantity>
            <cbc:LineExtensionAmount currencyID=""TRY"">{(order.ProductDto.Price * order.Piece).ToString("F2", CultureInfo.InvariantCulture)}</cbc:LineExtensionAmount>
            <cac:TaxTotal>
                <cbc:TaxAmount currencyID=""TRY"">{(order.ProductDto.Price * order.Piece * order.ProductDto.TaxDto.Rate / 100).ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxAmount>
                <cac:TaxSubtotal>
                    <cbc:TaxableAmount currencyID=""TRY"">{(order.ProductDto.Price * order.Piece).ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxableAmount>
                    <cbc:TaxAmount currencyID=""TRY"">{(order.ProductDto.Price * order.Piece * order.ProductDto.TaxDto.Rate / 100).ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxAmount>
                    <cbc:CalculationSequenceNumeric>1</cbc:CalculationSequenceNumeric>
                    <cbc:Percent>{order.ProductDto.TaxDto.Rate}</cbc:Percent>
                    <cac:TaxCategory>
                        <cac:TaxScheme>
                            <cbc:Name>KDV GERÇEK</cbc:Name>
                            <cbc:TaxTypeCode>0015</cbc:TaxTypeCode>
                        </cac:TaxScheme>
                    </cac:TaxCategory>
                </cac:TaxSubtotal>
            </cac:TaxTotal>
            <cac:Item>
                <cbc:Name>{order.ProductDto.Name}</cbc:Name>
                <cbc:BrandName>{order.ProductDto.Name}</cbc:BrandName>
            </cac:Item>
            <cac:Price>
                <cbc:PriceAmount currencyID=""TRY"">{order.ProductDto.Price.ToString("F2", CultureInfo.InvariantCulture)}</cbc:PriceAmount>
            </cac:Price>
        </cac:InvoiceLine>");
            }

            return $@"<Invoice xmlns=""urn:oasis:names:specification:ubl:schema:xsd:Invoice-2""
         xmlns:cbc=""urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2""
         xmlns:cac=""urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2""
         xmlns:xades=""http://uri.etsi.org/01903/v1.3.2#""
         xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
         xmlns:ext=""urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2""
         xmlns:ds=""http://www.w3.org/2000/09/xmldsig#""
         xsi:schemaLocation=""urn:oasis:names:specification:ubl:schema:xsd:Invoice-2 UBL-Invoice-2.1.xsd"">

    <cbc:UBLVersionID>2.1</cbc:UBLVersionID>
    <cbc:CustomizationID>TR1.2</cbc:CustomizationID>
    <cbc:ProfileID>TEMELFATURA</cbc:ProfileID>
    <cbc:ID>ABC2009123456789</cbc:ID>
    <cbc:CopyIndicator>false</cbc:CopyIndicator>
    <cbc:UUID>{Guid.NewGuid()}</cbc:UUID>
    <cbc:IssueDate>{DateTime.Now:yyyy-MM-dd}</cbc:IssueDate>
    <cbc:IssueTime>{DateTime.Now:HH:mm:ss}</cbc:IssueTime>
    <cbc:InvoiceTypeCode>SATIS</cbc:InvoiceTypeCode>
    <cbc:Note>Yazı ile: #{salesDto.OrderTotalPrice.ToString("F2", CultureInfo.InvariantCulture)} TL #</cbc:Note>
    <cbc:DocumentCurrencyCode>TRY</cbc:DocumentCurrencyCode>
    <cbc:LineCountNumeric>{salesDto.OrderDtos.Count}</cbc:LineCountNumeric>

    <cac:AccountingSupplierParty>
		<cac:Party>
			<cbc:WebsiteURI>{_company.WebSite}</cbc:WebsiteURI>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""VKN"">{_company.TaxNumber}</cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""SUBENO""></cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""MERSISNO""></cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""HIZMETNO""></cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""TICARETSICILNO""></cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyName>
				<cbc:Name>{_company.Name}</cbc:Name>
			</cac:PartyName>
			<cac:PostalAddress>
				<cbc:BuildingName>{_company.Address}</cbc:BuildingName>
				<cbc:CitySubdivisionName>{_company.District}</cbc:CitySubdivisionName>
				<cbc:CityName>{_company.City}</cbc:CityName>
				<cac:Country>
					<cbc:IdentificationCode>TR</cbc:IdentificationCode>
					<cbc:Name>Türkiye</cbc:Name>
				</cac:Country>
			</cac:PostalAddress>
			<cac:PartyTaxScheme>
				<cac:TaxScheme>
					<cbc:Name>{_company.TaxOffice}</cbc:Name>
				</cac:TaxScheme>
			</cac:PartyTaxScheme>
			<cac:Contact>
				<cbc:ElectronicMail>{_company.Mail}</cbc:ElectronicMail>
			</cac:Contact>
		</cac:Party>
	</cac:AccountingSupplierParty>
	<cac:AccountingCustomerParty>
		<cac:Party>
			<cbc:WebsiteURI></cbc:WebsiteURI>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""VKN"">{salesDto.CustomerDto.TaxNumber}</cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""SUBENO""></cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""MUSTERINO""></cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyName>
				<cbc:Name>{salesDto.CustomerDto.Name}</cbc:Name>
			</cac:PartyName>
			<cac:PostalAddress>
				<cbc:BuildingName>{trm.GetTurmobResponse.MUKELLEF.adresBilgileri[0].mahalleSemt + " "
                + trm.GetTurmobResponse.MUKELLEF.adresBilgileri[0].caddeSokak + " "
                + trm.GetTurmobResponse.MUKELLEF.adresBilgileri[0].disKapiNo + " /" +
                trm.GetTurmobResponse.MUKELLEF.adresBilgileri[0].icKapiNo}</cbc:BuildingName>
				<cbc:CitySubdivisionName>{trm.GetTurmobResponse.MUKELLEF.adresBilgileri[0].ilceAdi}</cbc:CitySubdivisionName>
				<cbc:CityName>{trm.GetTurmobResponse.MUKELLEF.adresBilgileri[0].ilAdi}</cbc:CityName>
				<cac:Country>
					<cbc:IdentificationCode>TR</cbc:IdentificationCode>
					<cbc:Name>Türkiye</cbc:Name>
				</cac:Country>
			</cac:PostalAddress>
			<cac:PartyTaxScheme>
				<cac:TaxScheme>
					<cbc:Name></cbc:Name>
				</cac:TaxScheme>
			</cac:PartyTaxScheme>
			<cac:Contact>
				<cbc:Telephone></cbc:Telephone>
				<cbc:ElectronicMail>{salesDto.CustomerDto.Description}</cbc:ElectronicMail>
			</cac:Contact>
		</cac:Party>
	</cac:AccountingCustomerParty>

    <cac:PaymentTerms>
        <cbc:PaymentDueDate>{DateTime.Now:yyyy-MM-dd}</cbc:PaymentDueDate>
    </cac:PaymentTerms>

    <cac:TaxTotal>
        <cbc:TaxAmount currencyID=""TRY"">{salesDto.OrderTotalTax.ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxAmount>
        <cac:TaxSubtotal>
            <cbc:TaxableAmount currencyID=""TRY"">{salesDto.OrderTotalTaxFreePrice.ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxableAmount>
            <cbc:TaxAmount currencyID=""TRY"">{salesDto.OrderTotalTax.ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxAmount>
            <cbc:CalculationSequenceNumeric>1</cbc:CalculationSequenceNumeric>
            <cbc:Percent>18</cbc:Percent>
            <cac:TaxCategory>
                <cac:TaxScheme>
                    <cbc:Name>KDV</cbc:Name>
                    <cbc:TaxTypeCode>0015</cbc:TaxTypeCode>
                </cac:TaxScheme>
            </cac:TaxCategory>
        </cac:TaxSubtotal>
    </cac:TaxTotal>

    <cac:LegalMonetaryTotal>
        <cbc:LineExtensionAmount currencyID=""TRY"">{salesDto.OrderTotalTaxFreePrice.ToString("F2", CultureInfo.InvariantCulture)}</cbc:LineExtensionAmount>
        <cbc:TaxExclusiveAmount currencyID=""TRY"">{salesDto.OrderTotalTaxFreePrice.ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxExclusiveAmount>
        <cbc:TaxInclusiveAmount currencyID=""TRY"">{salesDto.OrderTotalPrice.ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxInclusiveAmount>
        <cbc:AllowanceTotalAmount currencyID=""TRY"">0.00</cbc:AllowanceTotalAmount>
        <cbc:PayableAmount currencyID=""TRY"">{salesDto.OrderTotalPrice.ToString("F2", CultureInfo.InvariantCulture)}</cbc:PayableAmount>
    </cac:LegalMonetaryTotal>

    {invoiceLines.ToString()}
</Invoice>";
        }

        private string EArsivCreateUblXml(SalesDto salesDto, EDM_GetTurmobResponseMessage trm)

        {

          
            var invoiceLines = new StringBuilder();

          
            for (int i = 0; i < salesDto.OrderDtos.Count; i++)
            {
                var order = salesDto.OrderDtos[i];
                invoiceLines.Append($@"
        <cac:InvoiceLine>
            <cbc:ID>{i + 1}</cbc:ID>
            <cbc:InvoicedQuantity unitCode=""C62"">{order.Piece}</cbc:InvoicedQuantity>
            <cbc:LineExtensionAmount currencyID=""TRY"">{(order.ProductDto.Price * order.Piece).ToString("F2", CultureInfo.InvariantCulture)}</cbc:LineExtensionAmount>
            <cac:TaxTotal>
                <cbc:TaxAmount currencyID=""TRY"">{(order.ProductDto.Price * order.Piece * order.ProductDto.TaxDto.Rate / 100).ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxAmount>
                <cac:TaxSubtotal>
                    <cbc:TaxableAmount currencyID=""TRY"">{(order.ProductDto.Price * order.Piece).ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxableAmount>
                    <cbc:TaxAmount currencyID=""TRY"">{(order.ProductDto.Price * order.Piece * order.ProductDto.TaxDto.Rate / 100).ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxAmount>
                    <cbc:CalculationSequenceNumeric>1</cbc:CalculationSequenceNumeric>
                    <cbc:Percent>{order.ProductDto.TaxDto.Rate}</cbc:Percent>
                    <cac:TaxCategory>
                        <cac:TaxScheme>
                            <cbc:Name>KDV GERÇEK</cbc:Name>
                            <cbc:TaxTypeCode>0015</cbc:TaxTypeCode>
                        </cac:TaxScheme>
                    </cac:TaxCategory>
                </cac:TaxSubtotal>
            </cac:TaxTotal>
            <cac:Item>
                <cbc:Name>{order.ProductDto.Name}</cbc:Name>
                <cbc:BrandName>{order.ProductDto.Name}</cbc:BrandName>
            </cac:Item>
            <cac:Price>
                <cbc:PriceAmount currencyID=""TRY"">{order.ProductDto.Price.ToString("F2", CultureInfo.InvariantCulture)}</cbc:PriceAmount>
            </cac:Price>
        </cac:InvoiceLine>");
            }

            return $@"<Invoice xmlns=""urn:oasis:names:specification:ubl:schema:xsd:Invoice-2""
         xmlns:cbc=""urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2""
         xmlns:cac=""urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2""
         xmlns:xades=""http://uri.etsi.org/01903/v1.3.2#"" 
         xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance""
         xmlns:ext=""urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2""
         xmlns:ds=""http://www.w3.org/2000/09/xmldsig#"" 
         xsi:schemaLocation=""urn:oasis:names:specification:ubl:schema:xsd:Invoice-2 UBL-Invoice-2.1.xsd"">

    <cbc:UBLVersionID>2.1</cbc:UBLVersionID>
    <cbc:CustomizationID>TR1.2</cbc:CustomizationID>
    <cbc:ProfileID>EARSIVFATURA</cbc:ProfileID> <!-- Profile ID is specific to e-archive invoices -->
    <cbc:ID>ABC2009123456789</cbc:ID>
    <cbc:CopyIndicator>false</cbc:CopyIndicator>
    <cbc:UUID>{Guid.NewGuid()}</cbc:UUID>
    <cbc:IssueDate>{DateTime.Now:yyyy-MM-dd}</cbc:IssueDate>
    <cbc:IssueTime>{DateTime.Now:HH:mm:ss}</cbc:IssueTime>
    <cbc:InvoiceTypeCode>SATIS</cbc:InvoiceTypeCode>
    <cbc:Note>Yazı ile: #{salesDto.OrderTotalPrice.ToString("F2", CultureInfo.InvariantCulture)} TL #</cbc:Note>
    <cbc:DocumentCurrencyCode>TRY</cbc:DocumentCurrencyCode>
    <cbc:LineCountNumeric>{salesDto.OrderDtos.Count}</cbc:LineCountNumeric>

    <cac:AccountingSupplierParty>
		<cac:Party>
			<cbc:WebsiteURI>{_company.WebSite}</cbc:WebsiteURI>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""VKN"">{_company.TaxNumber}</cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""SUBENO""></cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""MERSISNO""></cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""HIZMETNO""></cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""TICARETSICILNO""></cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyName>
				<cbc:Name>{_company.Name}</cbc:Name>
			</cac:PartyName>
			<cac:PostalAddress>
				<cbc:BuildingName>{_company.Address}</cbc:BuildingName>
				<cbc:CitySubdivisionName>{_company.District}</cbc:CitySubdivisionName>
				<cbc:CityName>{_company.City}</cbc:CityName>
				<cac:Country>
					<cbc:IdentificationCode>TR</cbc:IdentificationCode>
					<cbc:Name>Türkiye</cbc:Name>
				</cac:Country>
			</cac:PostalAddress>
			<cac:PartyTaxScheme>
				<cac:TaxScheme>
					<cbc:Name>{_company.TaxOffice}</cbc:Name>
				</cac:TaxScheme>
			</cac:PartyTaxScheme>
			<cac:Contact>
				<cbc:ElectronicMail>{_company.Mail}</cbc:ElectronicMail>
			</cac:Contact>
		</cac:Party>
	</cac:AccountingSupplierParty>

	<cac:AccountingCustomerParty>
		<cac:Party>
			<cbc:WebsiteURI></cbc:WebsiteURI>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""VKN"">{trm.GetTurmobResponse.MUKELLEF.vkn}</cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""SUBENO""></cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyIdentification>
				<cbc:ID schemeID=""MUSTERINO""></cbc:ID>
			</cac:PartyIdentification>
			<cac:PartyName>
				<cbc:Name>{salesDto.CustomerDto.Name}</cbc:Name>
			</cac:PartyName>
			<cac:PostalAddress>
				<cbc:BuildingName>{salesDto.CustomerDto.BillingAddRess}</cbc:BuildingName>
				<cbc:CitySubdivisionName>{salesDto.CustomerDto.Address}</cbc:CitySubdivisionName>
				<cbc:CityName>{salesDto.CustomerDto.Address}</cbc:CityName>
				<cac:Country>
					<cbc:IdentificationCode>TR</cbc:IdentificationCode>
					<cbc:Name>Türkiye</cbc:Name>
				</cac:Country>
			</cac:PostalAddress>
			<cac:PartyTaxScheme>
				<cac:TaxScheme>
					<cbc:Name>{trm.GetTurmobResponse.MUKELLEF.vergiDairesiAdi}</cbc:Name>
				</cac:TaxScheme>
			</cac:PartyTaxScheme>
			<cac:Contact>
				<cbc:Telephone></cbc:Telephone>
				<cbc:ElectronicMail></cbc:ElectronicMail>
			</cac:Contact>
		</cac:Party>
	</cac:AccountingCustomerParty>

    <cac:PaymentTerms>
        <cbc:PaymentDueDate>{DateTime.Now:yyyy-MM-dd}</cbc:PaymentDueDate>
    </cac:PaymentTerms>

    <cac:TaxTotal>
        <cbc:TaxAmount currencyID=""TRY"">{salesDto.OrderTotalTax.ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxAmount>
        <cac:TaxSubtotal>
            <cbc:TaxableAmount currencyID=""TRY"">{salesDto.OrderTotalTaxFreePrice.ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxableAmount>
            <cbc:TaxAmount currencyID=""TRY"">{salesDto.OrderTotalTax.ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxAmount>
            <cbc:CalculationSequenceNumeric>1</cbc:CalculationSequenceNumeric>
            <cbc:Percent>18</cbc:Percent>
            <cac:TaxCategory>
                <cac:TaxScheme>
                    <cbc:Name>KDV</cbc:Name>
                    <cbc:TaxTypeCode>0015</cbc:TaxTypeCode>
                </cac:TaxScheme>
            </cac:TaxCategory>
        </cac:TaxSubtotal>
    </cac:TaxTotal>

    <cac:LegalMonetaryTotal>
        <cbc:LineExtensionAmount currencyID=""TRY"">{salesDto.OrderTotalTaxFreePrice.ToString("F2", CultureInfo.InvariantCulture)}</cbc:LineExtensionAmount>
        <cbc:TaxExclusiveAmount currencyID=""TRY"">{salesDto.OrderTotalTaxFreePrice.ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxExclusiveAmount>
        <cbc:TaxInclusiveAmount currencyID=""TRY"">{salesDto.OrderTotalPrice.ToString("F2", CultureInfo.InvariantCulture)}</cbc:TaxInclusiveAmount>
        <cbc:AllowanceTotalAmount currencyID=""TRY"">0.00</cbc:AllowanceTotalAmount>
        <cbc:PayableAmount currencyID=""TRY"">{salesDto.OrderTotalPrice.ToString("F2", CultureInfo.InvariantCulture)}</cbc:PayableAmount>
    </cac:LegalMonetaryTotal>

    {invoiceLines.ToString()}
</Invoice>";
        }


        public async Task<EDM_CheckUserResponse> GetCustomerBillTypeAsync(Guid customerId)
        {
            var customer = await _unitOfWork.GetRepository<Customer>().GetFirstWhereAsync(x => x.Status && x.Id == customerId);

            var header =  GetEdmRequestHeader();

            CheckUserRequest checkUserRequest = new CheckUserRequest();
            checkUserRequest.REQUEST_HEADER = header;
            checkUserRequest.USER = new GIBUSER();
            checkUserRequest.USER.IDENTIFIER = customer.TaxNumber;





            var response = await _client.CheckUserAsync(checkUserRequest);
            return response;



        }
    }
}
