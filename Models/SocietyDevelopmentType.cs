namespace castlers.Models
{
    public class SocietyDevelopmentType
    {
        public int societyDevelopmentTypeId { get; set; }
        public string developmentTypeName { get; set; }
        public Guid createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public Guid updatedBy { get; set; }
        public DateTime updatedDate { get; set; }
    }
}
