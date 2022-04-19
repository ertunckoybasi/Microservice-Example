using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Services.Invoice.Domain.Common;
using System;
using System.Collections.Generic;

namespace Services.Invoice.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public Invoice()
        {
            this.InvoiceItems = new List<InvoiceItem>();
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public DateTime Date { get; set; }
        public string PaymentTerms { get; set; }
        public double TaxRate { get; set; }
        public double Tax { get; set; }
        public double Amount{ get; set; }

        public double TotalAmount { get; set; }
        public IList<InvoiceItem> InvoiceItems { get; set; }
    }
}
