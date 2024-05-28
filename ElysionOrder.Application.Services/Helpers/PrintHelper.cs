using ElysionOrder.Application.Services.Dtos;
using System.Drawing;
using System.Drawing.Printing;

namespace ElysionOrder.Application.Services.Helpers
{
    public class PrintHelper
    {

        static BillDto _bill = new BillDto();



        //public void PrintBill(BillDto billDto, BillSettingDto billSettingDto)
        //{

        //    _bill = billDto;


        //    // Yazıcıyı seçin
        //    var printerSettings = new PrinterSettings();
        //    printerSettings.PrinterName = billSettingDto.PrinterName;

        //    // Yazdırma işlemini yapılandırın
        //    var printDocument = new PrintDocument();
        //    printDocument.PrinterSettings = printerSettings;
        //    printDocument.DefaultPageSettings.PaperSize = new PaperSize("Custom", Convert.ToInt32(billSettingDto.PaperWidth / 100 * 2.54F), Convert.ToInt32(billSettingDto.PaperWeight / 100 * 2.54F));
        //    printDocument.PrintPage += PrintDocument_PrintPage;


        //    // Yazdırma işlemini başlatın
        //    printDocument.Print();


        //}

        //private static void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        //{

        //    // Faturayı yazdırın
        //    var graphics = e.Graphics;
        //    var font = new Font("Arial", 8);

        //    // Fatura bilgilerini alın


        //    // Başlık
        //    //var titleFont = new Font("Arial", 18, FontStyle.Bold);
        //    //var titlePosition = new PointF(50, 50);
        //    //graphics.DrawString("FATURA", titleFont, Brushes.Black, titlePosition);

        //    // Fatura bilgileri
        //    var yPos = 100;
        //    graphics.DrawString("Fatura Numarası: " + _bill.BillNumber, font, Brushes.Black, 50, yPos);
        //    yPos += 20;
        //    graphics.DrawString("Tarih: " + _bill.CreateTime.Date.ToString("dd.MM.yyyy"), font, Brushes.Black, 50, yPos);
        //    yPos += 40;
        //    graphics.DrawString("Müşteri: " + _bill.SalesDto.CustomerDto.Name, font, Brushes.Black, 50, yPos);
        //    yPos += 40;
        //    graphics.DrawString("Vergi No / TCKN: " + _bill.SalesDto.CustomerDto.TaxNumber, font, Brushes.Black, 50, yPos);
        //    yPos += 40;

        //    graphics.DrawString("Vergi Dairesi : " + _bill.SalesDto.CustomerDto.TaxOffice, font, Brushes.Black, 50, yPos);
        //    yPos += 40;

        //    // Fatura kalemleri

        //    int header = 150;
        //    graphics.DrawString("Ürün", font, Brushes.Black, 50, yPos);
        //    header += 100;
        //    graphics.DrawString("Liste Fiyatı", font, Brushes.Black, header, yPos);
        //    header += 75;
        //    graphics.DrawString("İskonto Oranı", font, Brushes.Black, header ,yPos);
        //    header += 75;
        //    graphics.DrawString("Özel Fiyat", font, Brushes.Black, header, yPos);
        //    header += 75;
        //    graphics.DrawString("Adet", font, Brushes.Black, header, yPos);
        //    header += 75;
        //    graphics.DrawString("Tutar", font, Brushes.Black, header, yPos);
        //    header += 75;
        //    graphics.DrawString("Vergi", font, Brushes.Black, header, yPos);
        //    header += 75;
        //    graphics.DrawString("Vergili Tutar", font, Brushes.Black, header, yPos);
        //    yPos += 20;

        //    foreach (var item in _bill.SalesDto.OrderDtos)
        //    {
        //        header = 150;
        //        graphics.DrawString(item.PriceListDto.ProductDto.Name, font, Brushes.Black, 50, yPos);
        //        header += 100;
        //        graphics.DrawString(item.PriceListDto.Price.ToString("c"), font, Brushes.Black, header, yPos);
        //        header += 75;
        //        graphics.DrawString(item.Discount.ToString(), font, Brushes.Black, header, yPos);
        //        header += 75;
        //        graphics.DrawString(item.SpecialPrice.ToString("c"), font, Brushes.Black, header, yPos);
        //        header += 75;
        //        graphics.DrawString(item.Piece.ToString(), font, Brushes.Black, header, yPos);
        //        header += 75;
        //        graphics.DrawString(item.OrderTotalPrice.ToString("c"), font, Brushes.Black, header, yPos);
        //        header += 75;
        //        graphics.DrawString(item.OrderTotalTax.ToString("c"), font, Brushes.Black, header, yPos);
        //        header += 75;
        //        graphics.DrawString(item.OrderTotalPriceWithTax.ToString("c"), font, Brushes.Black, header, yPos);
        //        yPos += 20;
        //    }


        //    yPos += 20;
        //    graphics.DrawString("Toplam Vergi: " + _bill.SalesDto.OrderTotalTax.ToString("c"), font, Brushes.Black, 600, yPos);

        //    yPos += 20;
        //    graphics.DrawString("Toplam Tutar: " + _bill.SalesDto.OrderTotalTaxFreePrice.ToString("c"), font, Brushes.Black, 600, yPos);

        //    yPos += 20;
        //    graphics.DrawString("Vergi Dahil Toplam Tutar: " + _bill.SalesDto.OrderTotalPrice.ToString("c"), font, Brushes.Black, 600, yPos);



        //}


    }
}
