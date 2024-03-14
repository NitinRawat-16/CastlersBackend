namespace castlers.Dtos
{
    public class ElectionDetailsDto
    {
        public int ElectionId { get; set; }
        public int TenderId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Status { get; set; }
        public int TotalVoters { get; set; }
        public int TotalVoted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
    }
}
