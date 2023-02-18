namespace OnBoarding.Models
{
    public class Quest
    {
        public string Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }

        public List<Stage>? Stages { get; set; }
    }
}
