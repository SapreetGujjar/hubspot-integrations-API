namespace AI_Testing.Models.DTO
{
    public class leadDTO
    {
        public LeadProperties Properties { get; set; }
    }

    public class LeadProperties
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string Website { get; set; }
        public string Jobtitle { get; set; }
        public string HsLeadStatus { get; set; }
        public string Lifecyclestage { get; set; }
    }
}
