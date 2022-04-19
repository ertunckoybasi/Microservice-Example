using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentezMicro.Web.Core.ViewModel
{
  public  class CustomerVM
    {
        public CustomerVM()
        {
            RecId = 0;
            CurrentAccountEmail = "ertunc.koybasi@sentez.com";
            CurrentAccountAddress = "İstanbul/Türkiye";
        }
        public int RecId { get; set; }
        
        public string CurrentAccountCode { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string CurrentAccountName { get; set; }
        public string CurrentAccountEmail { get; set; }
        public string CurrentAccountPhone { get; set; }
        public string CurrentAccountAddress { get; set; }
    }
}
