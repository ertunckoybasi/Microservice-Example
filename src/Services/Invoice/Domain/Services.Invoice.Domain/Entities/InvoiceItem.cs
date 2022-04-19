using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Services.Invoice.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Invoice.Domain.Entities
{
    public class InvoiceItem : BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public int Id { get; set; }
            public int InvoiceId { get; set; }
            public string Item { get; set; }
            public double Quantity { get; set; }
            public double Price { get; set; }
            public Invoice Invoice { get; set; }
        
    }
}
