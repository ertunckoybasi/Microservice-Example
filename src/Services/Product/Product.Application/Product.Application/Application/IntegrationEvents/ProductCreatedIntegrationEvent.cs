using EventBus.Base.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Application.IntegrationEvents
{
    public class ProductCreatedIntegrationEvent : IntegrationEvent
    {
        public string ProductName { get; }
        public int RecID { get; }
        public ProductCreatedIntegrationEvent(int recID, string productName)
        {
            ProductName = ProductName;
            RecID = recID;
        }
    }
}
