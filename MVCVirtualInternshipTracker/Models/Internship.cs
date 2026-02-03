namespace MVCVirtualInternshipTracker.Models
{
    public class Internship
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public Student? Student { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
        public string Position { get; set; }
        public DateTime StartDate { get; set; } //if create button doesnt work but u dont want this add a question mark
        public DateTime EndDate { get; set; }
        public int HoursLogged { get; set; }
        public string SupervisorFeedback { get; set; }

    }
}
