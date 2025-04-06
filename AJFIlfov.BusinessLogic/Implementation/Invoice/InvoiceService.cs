using AJFIlfov.BusinessLogic.Base;
using AJFIlfov.DataAccess;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AJFIlfov.BusinessLogic.Implementation.Invoice.Models;
using iText.Kernel.Pdf;
using iText.Layout;
using System.Xml.Linq;

using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using PdfDocument = PdfSharp.Pdf.PdfDocument;


namespace AJFIlfov.BusinessLogic.Implementation.Invoice
{
    public class InvoiceService : BaseService
    {
        public InvoiceService(ServiceDependencies dependencies) : base(dependencies)
        {
        }

        public byte[] CreateInvoice(InvoiceData invoiceData)
        {
            var pdfBytes = GenerateInvoicePdf(invoiceData);
            SaveInvoicePdf(Guid.NewGuid(), pdfBytes);
            return pdfBytes;
        }

        private byte[] GenerateInvoicePdf(InvoiceData invoiceData)
        {
            using var memoryStream = new MemoryStream();
            var document = new PdfDocument();
            var page = document.AddPage();
            var gfx = XGraphics.FromPdfPage(page);
            var font = new XFont("Verdana", 12, XFontStyleEx.Regular);
            var boldFont = new XFont("Verdana", 12, XFontStyleEx.Bold);

            // Header
            gfx.DrawString("Factura", new XFont("Verdana", 16, XFontStyleEx.Bold), XBrushes.Black, new XRect(0, 40, page.Width, page.Height), XStringFormats.TopCenter);

            // Invoice Number and Dates
            gfx.DrawString($"Număr Factură: {invoiceData.InvoiceNumber}", boldFont, XBrushes.Black, new XRect(40, 80, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Data Emitere: {invoiceData.InvoiceDate:yyyy-MM-dd}", boldFont, XBrushes.Black, new XRect(40, 100, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Data Scadență: {invoiceData.DueDate:yyyy-MM-dd}", boldFont, XBrushes.Black, new XRect(40, 120, page.Width, page.Height), XStringFormats.TopLeft);

            // Supplier Details
            gfx.DrawString("Furnizor:", boldFont, XBrushes.Black, new XRect(40, 140, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(invoiceData.SenderName, font, XBrushes.Black, new XRect(150, 140, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(invoiceData.SenderAddress, font, XBrushes.Black, new XRect(150, 160, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"CUI: {invoiceData.SupplierCUI}", font, XBrushes.Black, new XRect(150, 180, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Reg. Com.: {invoiceData.SupplierRegCom}", font, XBrushes.Black, new XRect(150, 200, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Capital Social: {invoiceData.SupplierCapital}", font, XBrushes.Black, new XRect(150, 220, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Banca: {invoiceData.SupplierBank}", font, XBrushes.Black, new XRect(150, 240, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"IBAN: {invoiceData.SupplierIBAN}", font, XBrushes.Black, new XRect(150, 260, page.Width, page.Height), XStringFormats.TopLeft);

            // Client Details
            gfx.DrawString("Client:", boldFont, XBrushes.Black, new XRect(40, 280, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(invoiceData.RecipientName, font, XBrushes.Black, new XRect(150, 280, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString(invoiceData.RecipientAddress, font, XBrushes.Black, new XRect(150, 300, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"CUI: {invoiceData.ClientCUI}", font, XBrushes.Black, new XRect(150, 320, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Reg. Com.: {invoiceData.ClientRegCom}", font, XBrushes.Black, new XRect(150, 340, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Banca: {invoiceData.ClientBank}", font, XBrushes.Black, new XRect(150, 360, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"IBAN: {invoiceData.ClientIBAN}", font, XBrushes.Black, new XRect(150, 380, page.Width, page.Height), XStringFormats.TopLeft);

            // Items Table
            int yPoint = 420;
            gfx.DrawString("Articol", boldFont, XBrushes.Black, new XRect(40, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("UM", boldFont, XBrushes.Black, new XRect(200, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Cantitate", boldFont, XBrushes.Black, new XRect(250, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Preț", boldFont, XBrushes.Black, new XRect(350, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Valoare", boldFont, XBrushes.Black, new XRect(450, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString("Total", boldFont, XBrushes.Black, new XRect(550, yPoint, page.Width, page.Height), XStringFormats.TopLeft);

            yPoint += 20;
            foreach (var item in invoiceData.Items)
            {
                gfx.DrawString(item.Description, font, XBrushes.Black, new XRect(40, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(item.UnitOfMeasure, font, XBrushes.Black, new XRect(200, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(item.Quantity.ToString(), font, XBrushes.Black, new XRect(250, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString(item.UnitPrice.ToString("F2"), font, XBrushes.Black, new XRect(350, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString((item.Quantity * item.UnitPrice).ToString("F2"), font, XBrushes.Black, new XRect(450, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawString((item.Quantity * item.UnitPrice).ToString("F2"), font, XBrushes.Black, new XRect(550, yPoint, page.Width, page.Height), XStringFormats.TopLeft);
                yPoint += 20;
            }

            // Total Amount
            gfx.DrawString($"Total fără TVA: {invoiceData.TotalAmount:F2} Lei", boldFont, XBrushes.Black, new XRect(40, yPoint + 20, page.Width, page.Height), XStringFormats.TopLeft);
            gfx.DrawString($"Total: {invoiceData.TotalAmount:F2} Lei", boldFont, XBrushes.Black, new XRect(40, yPoint + 40, page.Width, page.Height), XStringFormats.TopLeft);

            document.Save(memoryStream);
            return memoryStream.ToArray();
        }

        public void SaveInvoicePdf(Guid invoiceId, byte[] pdfBytes)
        {
            var invoice = UnitOfWork.Invoices.Find(invoiceId);
            if (invoice != null)
            {
                invoice.PdfData = pdfBytes;
                UnitOfWork.SaveChanges();
            }
        }




    }
}
