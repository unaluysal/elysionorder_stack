using EFaturaEDMTest;
using ElysionOrder.Domain.Entitys;
using ElysionOrder.Infrastructure.Data.UnitOfWork;
using System.Xml.Serialization;

namespace ElysionOrder.Application.Services.Helpers
{
    public class EBillHelper
    {
        readonly IUnitOfWork _unitOfWork;
        private Company _company;
        private EBillSetting _eBillSetting;
        public EBillHelper(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _company = new Company();
            _eBillSetting = new EBillSetting();
        }

        public async Task<string> EdmAuthenticationAsync()
        {

            _company = await _unitOfWork.GetRepository<Company>().GetFirstAsync();
            _eBillSetting = await _unitOfWork.GetRepository<EBillSetting>().GetFirstWhereAsync(x => x.Status && x.Role == _company.EBillRole);
            string res = "";
            if (_company.EBillRole == "TEST")
            {
                EFaturaEDMTest.EFaturaEDMPortClient client = new EFaturaEDMTest.EFaturaEDMPortClient();


                EFaturaEDMTest.LoginRequest loginRequest = new EFaturaEDMTest.LoginRequest();
                EFaturaEDMTest.REQUEST_HEADERType header = new EFaturaEDMTest.REQUEST_HEADERType();

               

                header.SESSION_ID = "0";
                header.ACTION_DATE = DateTime.Now;
                header.ACTION_DATESpecified = true;
                header.CLIENT_TXN_ID = System.Guid.NewGuid().ToString();
                header.REASON = _eBillSetting.Reason;
                header.APPLICATION_NAME = _eBillSetting.ApplicationName;
                header.HOSTNAME = _eBillSetting.HostName;
                header.CHANNEL_NAME = _eBillSetting.ChannelName;
                header.COMPRESSED = _eBillSetting.Compressed;


                loginRequest.USER_NAME = _eBillSetting.UserName;
                loginRequest.PASSWORD = _eBillSetting.Password;
                loginRequest.REQUEST_HEADER = header;
                var response = await client.LoginAsync(loginRequest);
                if (response.LoginResponse != null)
                {

                    res = response.LoginResponse.SESSION_ID.ToString();


                }
            }



            return res;
        }
        public async Task<string> EdmSendInvoiceAsync(Bill bill)
        {


            var token = await EdmAuthenticationAsync();
            if (!string.IsNullOrEmpty(token))
            {
                if (_company.EBillRole == "TEST")
                {
                    EFaturaEDMTest.EFaturaEDMPortClient client = new EFaturaEDMTest.EFaturaEDMPortClient();
                    //test olduğunda bu bilgiler edm olmalı, servis böyle çalışıyor





                    _company.Name = "2018 & EDM BILISIM SISTEMLERI VE DANISMANLIK HIZMETLERI ANONIM SIRKETI";
                    _company.TaxNumber = "3230512384";
                    EFaturaEDMTest.REQUEST_HEADERType header = new EFaturaEDMTest.REQUEST_HEADERType();
                    header.ACTION_DATE = DateTime.Now;
                    header.ACTION_DATESpecified = true;
                    header.CLIENT_TXN_ID = Guid.NewGuid().ToString();
                    header.REASON = _eBillSetting.Reason;
                    header.APPLICATION_NAME = _eBillSetting.ApplicationName;
                    header.HOSTNAME = _eBillSetting.HostName;
                    header.CHANNEL_NAME = _eBillSetting.ChannelName;
                    header.COMPRESSED = _eBillSetting.Compressed;
                    header.SESSION_ID = token;

                    EFaturaEDMTest.SendInvoiceRequest req = new EFaturaEDMTest.SendInvoiceRequest();
                    req.REQUEST_HEADER = header;



                    CheckUserRequest checkUserRequest = new CheckUserRequest();
                    checkUserRequest.USER = new GIBUSER();
                    checkUserRequest.USER.IDENTIFIER = _company.TaxNumber;
                    checkUserRequest.REQUEST_HEADER = header;

                    var gibusers = await client.CheckUserAsync(checkUserRequest);



                    EFaturaEDMTest.INVOICE inovice = new EFaturaEDMTest.INVOICE();
                 



                    Invoice invoice = new Invoice();

                    invoice.UBLVersionID = 2.1m;
                    invoice.CustomizationID = "TR1.2";
                    invoice.ProfileID = "TEMELFATURA";
                    invoice.UUID = Guid.NewGuid().ToString();
                    invoice.InvoiceTypeCode = "SATIS";
                    invoice.DocumentCurrencyCode = "TRY";
                    invoice.Note = "TEST1 FATURA";
                    invoice.IssueDate = DateTime.Now;
                    invoice.ID = new ID { Value = "SAM2023000000002" };
                    invoice.LineCountNumeric = 1;


                    // kötü yapmışlar bunu, mecbur burayı list olarak gönderiyorum
                    AccountingSupplierPartyPartyPartyIdentification[] pt = new AccountingSupplierPartyPartyPartyIdentification[1];
                    pt[0] = new AccountingSupplierPartyPartyPartyIdentification { ID = new ID { schemeID = "VKN", Value = _company.TaxNumber } };

                    // Kullanan firma tarafı
                    invoice.AccountingSupplierParty = new AccountingSupplierParty
                    {

                        Party = new AccountingSupplierPartyParty
                        {

                            PartyIdentification = pt,
                            PartyName = new AccountingSupplierPartyPartyPartyName { Name = _company.Name },
                            PostalAddress = new AccountingSupplierPartyPartyPostalAddress
                            {
                                BuildingName = _company.Address,
                                CityName = "ANKARA",
                                CitySubdivisionName = "ÇANKAYA",
                                Country = new AccountingSupplierPartyPartyPostalAddressCountry { Name = "TURKİYE", IdentificationCode = "TR" }
                            },
                            PartyTaxScheme = new AccountingSupplierPartyPartyPartyTaxScheme
                            {
                                TaxScheme = new AccountingSupplierPartyPartyPartyTaxSchemeTaxScheme
                                {
                                    Name = _company.TaxOffice

                                }
                            }

                        }
                    };

                    // Fatura alan tarafı
                    invoice.AccountingCustomerParty = new AccountingCustomerParty
                    {
                        Party = new AccountingCustomerPartyParty
                        {
                            PartyName = new AccountingCustomerPartyPartyPartyName { Name = "ABUZER" },
                            PartyIdentification = new AccountingCustomerPartyPartyPartyIdentification { ID = new ID { schemeID = "VKN", Value = _company.TaxNumber } },
                            PartyTaxScheme = new AccountingCustomerPartyPartyPartyTaxScheme
                            {
                                TaxScheme = new AccountingCustomerPartyPartyPartyTaxSchemeTaxScheme
                                {
                                    Name = "ÇANKAYA"

                                }

                            },
                            //PostalAddress = new AccountingCustomerPartyPartyPostalAddress
                            //{
                            //    BuildingName = bill.Sales.Customer.Address,
                            //    CityName =bill.Sales.Customer.BillingAddRess,
                            //    CitySubdivisionName = "ÇANKAYA",
                            //    Country = new AccountingCustomerPartyPartyPostalAddressCountry { IdentificationCode = "TR", Name = "TÜRKİYE" }
                            //}



                        }
                    };


                    //Kalem işlemleri

                    invoice.TaxTotal = new TaxTotal
                    {
                        TaxAmount = new TaxAmount { currencyID = "TRY", Value = 100 },
                        TaxSubtotal = new TaxTotalTaxSubtotal
                        {


                            TaxableAmount = new TaxableAmount { currencyID = "TRY", Value = 100 },
                            TaxAmount = new TaxAmount { currencyID = "TRY", Value = 10 },
                            CalculationSequenceNumeric = 1,
                            Percent = 10,
                            TaxCategory = new TaxTotalTaxSubtotalTaxCategory
                            {
                                TaxScheme = new TaxTotalTaxSubtotalTaxCategoryTaxScheme
                                {
                                    TaxTypeCode = "0015",
                                    Name = "KDV GERCEK"

                                }
                            }




                        }
                    };
                    invoice.LegalMonetaryTotal = new LegalMonetaryTotal
                    {
                        LineExtensionAmount = new LineExtensionAmount { currencyID = "TRY", Value = 220 },
                        TaxExclusiveAmount = new TaxExclusiveAmount { currencyID = "TRY", Value = 200 },
                        TaxInclusiveAmount = new TaxInclusiveAmount { currencyID = "TRY", Value = 220 },
                        AllowanceTotalAmount = new AllowanceTotalAmount { currencyID = "TRY", Value = 20 },
                        PayableAmount = new PayableAmount { currencyID = "TRY", Value = 220 }

                    };


                    invoice.InvoiceLine = new InvoiceLine
                    {
                        ID = new ID { schemeID = "ID", Value = "1" },
                        InvoicedQuantity = new InvoicedQuantity { unitCode = "C62", Value = 1 },
                        LineExtensionAmount = new LineExtensionAmount { currencyID = "TRY", Value = 50 },
                        TaxTotal = new InvoiceLineTaxTotal
                        {
                            TaxAmount = new TaxAmount { currencyID = "TRY", Value = 50 },
                            TaxSubtotal = new InvoiceLineTaxTotalTaxSubtotal
                            {
                                TaxableAmount = new TaxableAmount { currencyID = "TRY", Value = 50 },
                                TaxAmount = new TaxAmount { currencyID = "TRY", Value = 5 },
                                CalculationSequenceNumeric = 1,
                                Percent = 10,
                                TaxCategory = new InvoiceLineTaxTotalTaxSubtotalTaxCategory
                                {
                                    TaxScheme = new InvoiceLineTaxTotalTaxSubtotalTaxCategoryTaxScheme
                                    {

                                        TaxTypeCode = "0015",
                                        Name = "GERÇEK USULDE KATMA DEĞER VERGİSİ"
                                    }
                                }
                            }


                        },
                        Item = new InvoiceLineItem
                        {
                            Name = "URUN1"
                        },
                        Price = new InvoiceLinePrice { PriceAmount = new PriceAmount { currencyID = "TRY", Value = 10 } }



                    };
                    XmlSerializer serializer = new XmlSerializer(typeof(Invoice));


                    byte[] xmlBytes;
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        var names = new XmlSerializerNamespaces();
                        names.Add("cbc", "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2");
                        names.Add("cac", "urn:oasis:names:specification:ubl:schema:xsd:CommonAggregateComponents-2");
                        names.Add("ext", "urn:oasis:names:specification:ubl:schema:xsd:CommonExtensionComponents-2");


                        serializer.Serialize(memoryStream, invoice, names);


                        xmlBytes = memoryStream.ToArray();
                        string xmlContent = System.Text.Encoding.UTF8.GetString(xmlBytes);
                        using (FileStream fileStream = new FileStream("C:\\Users\\Unal\\Desktop\\invoice.xml", FileMode.Create))
                        {
                            memoryStream.Seek(0, SeekOrigin.Begin);
                            memoryStream.CopyTo(fileStream);
                        }


                    }


                    EFaturaEDMTest.base64Binary bd = new base64Binary();
                    bd.Value = xmlBytes;
                    inovice.CONTENT = bd;

                    inovice.HEADER = new INVOICEHEADER()
                    {
                        SENDER=_company.TaxNumber,
                        RECEIVER=_company.TaxNumber,
                        FROM = "urn:mail:defaultgb@edmbilisim.com.tr",
                        TO = "defaultgb@edmbilisim.com.tr",
                        INVOICESERIAL_REQUESTED="PDF"

                    };

                    INVOICE[] ls = new INVOICE[1];
                    ls[0] = inovice;
                    req.INVOICE = ls;

                    req.RECEIVER = new SendInvoiceRequestRECEIVER { vkn = _company.TaxNumber, alias = "defaultgb@edmbilisim.com.tr" };
                    try
                    {

                      
                        var re = await client.SendInvoiceAsync(req);
                    }
                    catch (Exception )
                    {

                      
                    }
                    

                   
                }

                

            }

            string res = "";

            return res;



        }
    }
}
