using SentezMicro.Services.Customer.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SentezMicro.Services.Customer.Domain.Entities
{
    public class ErpCustomer : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RecId { get; set; }
        public string CurrentAccountCode { get; set; }
        public string CurrentAccountName { get; set; }
        public string CurrentAccountEmail { get; set; }
        public string CurrentAccountPhone { get; set; }
        public string CurrentAccountAddress { get; set; }
        
        public bool InUse { get; set; }
        public DateTime InsertedAt { get; set; }
        public int InsertedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
        public int DeletedBy { get; set; }
    }
}