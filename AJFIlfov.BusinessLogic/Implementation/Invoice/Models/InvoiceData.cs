using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AJFIlfov.Entities.Entities;

namespace AJFIlfov.BusinessLogic.Implementation.Invoice.Models
{
    public class InvoiceData
    {
        public string SenderName { get; set; }
        public string SenderAddress { get; set; }
        public string SupplierCUI { get; set; }
        public string SupplierRegCom { get; set; }
        public string SupplierCapital { get; set; }
        public string SupplierBank { get; set; }
        public string SupplierIBAN { get; set; }
        public string RecipientName { get; set; }
        public string RecipientAddress { get; set; }
        public string ClientCUI { get; set; }
        public string ClientRegCom { get; set; }
        public string ClientBank { get; set; }
        public string ClientIBAN { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }


}
