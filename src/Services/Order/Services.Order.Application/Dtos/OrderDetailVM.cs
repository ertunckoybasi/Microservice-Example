using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Order.Application.Dtos
{
    public class OrderDetailVM
    {
        public string Ordernumber { get; init; }
        public DateTime Date { get; init; }
        public string Status { get; init; }
        public string Description { get; init; }
        public string Street { get; init; }
        public string City { get; init; }
        public string Zipcode { get; init; }
        public string Country { get; init; }
        public List<Orderitem> Orderitems { get; set; }
        public decimal Total { get; set; }
    }

    public class Orderitem
    {
        public string Productname { get; init; }
        public int Units { get; init; }
        public double Unitprice { get; init; }
        public string Pictureurl { get; init; }
    }
}
