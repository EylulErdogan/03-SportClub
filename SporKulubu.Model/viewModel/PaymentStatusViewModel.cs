namespace SporKulubu.Model.viewModel
{
    public class PaymentStatusViewModel
    {
        public string MemberName { get; set; }

        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; }

        public bool IsPaid { get; set; }
    }
}
