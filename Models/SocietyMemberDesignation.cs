namespace castlers.Models
{
    public class SocietyMemberDesignation
    {
        public int societyMemberDesignationId { get; set; }
        public string designationType { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set;}
        public Guid createdBy { get; set; }
        public Guid updatedBy { get; set; }
    }
}
