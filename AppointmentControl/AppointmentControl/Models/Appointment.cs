using Newtonsoft.Json;

namespace AppointmentControl.Models
{

    public class Appointment
    {
        public const string REQUESTED = "Requested";
        public const string ACCEPTED = "Accepted";
        public const string DECLINED = "Declined";
        public const string REJECTED = "Rejected";
        public const string DONE = "DONE";
        
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "doctor_id")]
        public string DoctorId { get; set; }

        [JsonProperty(PropertyName = "patient_id")]
        public string PatientId { get; set; }

        [JsonProperty(PropertyName = "startdate")]
        public string StartDate { get; set; }

        [JsonProperty(PropertyName = "enddate")]
        public string EndDate { get; set; }

        [JsonProperty(PropertyName = "Reason")]
        public string Reason { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string status { get; set; }
    }
}