namespace castlers.Dtos
{
    public class SubmitVotingDto
    {
        public SubmitVotingDto()
        {
            Code = string.Empty;
            DeveloperIds = new();
        }
        public string Code { get; set; }
        public List<int> DeveloperIds { get; set; }
    }
}
