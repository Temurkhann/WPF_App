using Newtonsoft.Json;

namespace ExamApp.Models
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public dynamic Image { get; set; }

        [JsonProperty("faculty")]
        public string LastMessage { get; set; }
    }
}
