using System;
using System.Collections.Generic;
using System.Text;

namespace SporKulubu.Model.viewModel
{
    public class PaymentStatusViewModel
    {
        public string MemberName { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentStatus { get; set; }
    }
}
