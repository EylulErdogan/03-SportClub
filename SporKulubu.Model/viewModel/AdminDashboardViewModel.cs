namespace SporKulubu.Model.viewModel
{
    public class AdminDashboardViewModel
    {
        public int MemberCount { get; set; }
        public int CoachCount { get; set; }
        public int SportCount { get; set; }

        public decimal TotalPaymentAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal UnpaidAmount { get; set; }

        public List<PaymentStatusViewModel> MemberPayments { get; set; }
        public List<SportMemberCountViewModel> SportCounts { get; set; }
        public List<MemberCoachPaymentViewModel> CoachReports { get; set; }
    }

}
