using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SporKulubu.Model
{
    public class Payment
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }
        public string PaymentType { get; set; }

        public bool IsPaid { get; set; }
        public int MemberId { get; set; }
        public Member Member { get; set; }
    }
}
