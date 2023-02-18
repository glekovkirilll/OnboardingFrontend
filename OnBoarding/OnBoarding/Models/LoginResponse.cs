using Newtonsoft.Json;

namespace OnBoarding.Models
{
    public class LoginResponse
    {
        [JsonProperty("JWT")]
        public String JWT { get; set; }        
    }
}
