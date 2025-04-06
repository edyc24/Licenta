using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AJFIlfov.Common;

namespace AJFIlfov.Entities.Entities
{
    public class Invoice : IEntity 
    {
        public Guid Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierCUI { get; set; }
        public string SupplierRegCom { get; set; }
        public string SupplierCapital { get; set; }
        public string SupplierBank { get; set; }
        public string SupplierIBAN { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientCUI { get; set; }
        public string ClientRegCom { get; set; }
        public string ClientBank { get; set; }
        public string ClientIBAN { get; set; }
        public decimal TotalAmount { get; set; }
        public byte[] PdfData { get; set; }
        public ICollection<InvoiceItem> Items { get; set; }
    }


}
