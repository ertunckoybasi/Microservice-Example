using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Customer.Api.Application.Responses
{
    public class CustomerResponse
    {
        public int RecId { get; set; }
        public string CurrentAccountCode { get; set; }
        public string CurrentAccountName { get; set; }
        public string CurrentAccountEmail { get; set; }
        public string CurrentAccountPhone { get; set; }
        public string CurrentAccountAddress { get; set; }
    }
}
