using System;
using System.Collections.Generic;
using System.Text;

namespace SporKulubu.Model.viewModel
{
    public class AdminDashboardViewModel
    {
        public List<MemberCoachPaymentViewModel> MemberPayments { get; set; }
        public List<SportMemberCountViewModel> SportCounts { get; set; }
        public List<PaymentStatusViewModel> PaymentStatuses { get; set; }
    }
}
