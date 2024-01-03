namespace castlers.Dtos
{
    public class GenSocietyDto
    {
        public int registeredSocietyId { get; set; }
        public int societyDevelopmentTypeId { get; set; }
        public string? societyName { get; set; }
        public string? registeredAddress { get; set; }
        public int existingMemberCount { get; set; }
        public float age { get; set; }
        public string? city { get; set; }
        public DateTime createdDate { get; set; }

      //public string societyRegisteredCode { get; set; }
      //public string? societyDevelopmentSubType { get; set; }
      //public string? societyRegistrationNumber { get; set; }
      //public string? email { get; set; }
      //public int? ActiveTenderId { get; set; }
        public List<SocietyMemberDetailsDto> societyMemberDetails { get; set; } = new();
    }
}
