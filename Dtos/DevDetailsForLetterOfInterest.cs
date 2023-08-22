namespace castlers.Dtos
{
    public class DevDetailsForLetterOfInterest
    {
        public int DeveloperId { get; set; }
        public string? Name { get; set; } = default;
        public string? Email { get; set; } = default;
        public string? Phone { get; set; } = default;
        public string? SocietyName { get; set; } = default;
        public int SocietyId { get; set; }
        public int TenderId { get; set; }
    }
}
