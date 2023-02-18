namespace OnBoarding.Models
{
    public class Subdivision
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        List<String> Leaders { get; set; }
        List<String> Team { get; set; }

    }
}
    