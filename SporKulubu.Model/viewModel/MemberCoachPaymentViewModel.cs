using System;
using System.Collections.Generic;
using System.Text;

namespace SporKulubu.Model.viewModel
{
    public class MemberCoachPaymentViewModel
    {
        public string MemberName { get; set; }
        public int Age { get; set; }

        public string SportName { get; set; }

        public string CoachName { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public string PaymentType { get; set; }

        public bool IsPaid { get; set; }
    }
}
