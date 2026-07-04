namespace SporKulubu.Model
{
    public class Sport
    {
        public int Id { get; set; }
        public string SportName { get; set; }

        public List<Member> Members { get; set; }
        public List<Coach> Coaches { get; set; }
    }
}
