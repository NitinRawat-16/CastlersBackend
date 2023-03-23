using castlers.Dtos;
using castlers.Models;

namespace castlers.Services
{
    public interface ISocietyMemberDetailsService
    {
        public Task<int> AddRegisteredSocietyMemberListAsync(List<SocietyMemberDetails> societyMemberDetails);
        public Task<int> AddRegisteredSocietyMemberAsync(NewMemberDetailsDto memberDetails);
    }
}
