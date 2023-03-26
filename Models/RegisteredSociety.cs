using System;

namespace castlers.Models
{
    public class RegisteredSociety
    {
        public int registeredSocietyId { get; set; }
        public int societyDevelopmentTypeId { get; set; }
        public string societyDevelopmentSubType { get; set; }
        public string societyRegistrationNumber { get; set; }
        public string societyName { get; set; }
        public string registeredAddress { get; set; }
        public string email { get; set; }
        public int existingMemberCount { get; set; }
        public double age { get; set; }
        public string societyRegisteredCode { get; set; }
        public Guid createdBy { get; set; }
        public DateTime createdDate { get; set; }
        public Guid updatedBy { get; set; }
        public DateTime updatedDate { get; set; }

    }
}
