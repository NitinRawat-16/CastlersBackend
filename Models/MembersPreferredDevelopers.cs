namespace castlers.Models
{
    public class MembersPreferredDevelopers
    {
        public int PreferredDeveloperId { get; set; }
        public int MemberId { get; set; }
        public int ElectionId { get; set; }
        public int DeveloperFirst { get; set; }
        public int DeveloperSecond { get; set; }
        public int DeveloperThird { get; set; }
        public bool IsVoted { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime UpdationDate { get; set; }
    }
}
