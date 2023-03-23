using AutoMapper;
using castlers.Common;
using castlers.DbContexts;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace castlers.Services
{
    public class SocietyMemberDetailsManager : ISocietyMemberDetailsService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ISocietyMemberDetailsRepository _societyMemberDetailsRepository;
        private readonly IMapper _mapper;

        public SocietyMemberDetailsManager(ApplicationDbContext dbContext, ISocietyMemberDetailsRepository societyMemberDetailsRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _societyMemberDetailsRepository = societyMemberDetailsRepository;
            _mapper = mapper;
        }
        public Task<int> AddRegisteredSocietyMemberListAsync(List<SocietyMemberDetails> societyMemberDetails)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddRegisteredSocietyMemberAsync(NewMemberDetailsDto memberDetails)
        {

            var members = ExcelFileConverter.ConvertToList(memberDetails.societyId, memberDetails.file);
            NewMemberDetails newMemberDetails = new NewMemberDetails
            {
                societyId = memberDetails.societyId,
                societyCode = memberDetails.societyCode,
                societyMemberDetails = members
            };
            //var newMembers = _mapper.Map<NewMemberDetailsDto, NewMemberDetails>(memberDetails);

            return _societyMemberDetailsRepository.AddRegisteredSocietyMemberAsync(newMemberDetails);

        }

        public Task<int> UpdateRegisteredSocietyMemberAsync(UpdateSocietyMemberDto updateSocietyMemberDto)
        {
            throw new NotImplementedException();
        }
    }
}
