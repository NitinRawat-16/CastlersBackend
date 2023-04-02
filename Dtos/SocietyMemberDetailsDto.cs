namespace castlers.Dtos
{
    public class SocietyMemberDetailsDto
    {
        public int societyMemberDetailsId { get; set; }
        public int registeredSocietyId { get; set; }
        public string memberName { get; set; }
        public string mobileNumber { get; set; }
        public string email { get; set; }
        public int societyMemberDesignationId { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }

    }
}
