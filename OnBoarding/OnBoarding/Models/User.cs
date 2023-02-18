using Newtonsoft.Json;

namespace OnBoarding.Models
{
    public class User
    {
        
        public String id { get; set; }
        
        public String EMail { get; set; }
        
        public About about { get; set; }
    }
}
