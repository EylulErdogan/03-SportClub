namespace SporKlubuCodeFirstKatmanlıMimariProjectUI.Model
{
    public class Member
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public int SportId { get; set; }
        public Sport Sport { get; set; }
    }
}
