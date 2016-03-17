using Newtonsoft.Json;

namespace AppointmentControl.Models
{
    public class PatientType
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; } 
    }
}