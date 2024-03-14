namespace castlers.Models
{
    public class RefreshToken
    {
        public Guid refreshTokenId { get; set; }
        public string token { get; set; }
        public string jwtId { get; set; }
        public int userId { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime expiryDate { get; set; }
        public bool isUsed { get; set; }
    }
}
