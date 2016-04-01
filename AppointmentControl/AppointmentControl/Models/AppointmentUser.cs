namespace AppointmentControl.Models
{
    public class AppointmentUser
    {
        public string Id { get; set; }
        public string DoctorId { get; set; }
        public string PatientId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Reason { get; set; }
        public string status { get; set; } 
        public string DoctorName { get; set; } 
        public string PatientName { get; set; } 
    }
}