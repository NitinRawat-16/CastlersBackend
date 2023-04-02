using AutoMapper;
using castlers.Dtos;
using castlers.Models;
using castlers.DbContexts;
using castlers.Repository;


namespace castlers.Services
{
    public class RegisteredSocietyManager : IRegisteredSocietyService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IRegisteredSocietyRepository _registeredSocietyRepository;
        private readonly ISocietyMemberDetailsRepository _societyMemberDetailsRepository;
        private readonly IMapper _mapper;

        public RegisteredSocietyManager(ApplicationDbContext dbContext, IRegisteredSocietyRepository registeredSocietyRepository,
                                        IMapper mapper, ISocietyMemberDetailsRepository societyMemberDetailsRepository)
        {
            _dbContext = dbContext;
            _registeredSocietyRepository = registeredSocietyRepository;
            _societyMemberDetailsRepository = societyMemberDetailsRepository;
            _mapper = mapper;
        }
        public async Task<int> AddRegisteredSocietyAsync(RegisteredSocietyDto registeredSocietyDto)
        {
            var registeredSociety = _mapper.Map<RegisteredSocietyDto, RegisteredSociety>(registeredSocietyDto);
            registeredSociety.createdBy = Guid.NewGuid();
            registeredSociety.createdDate = DateTime.Now;
            registeredSociety.updatedBy = Guid.NewGuid();
            registeredSociety.updatedDate = DateTime.Now;

            int registeredSocietyId = Convert.ToInt32(await _registeredSocietyRepository.AddRegisteredSocietyAsync(registeredSociety));
            if (registeredSocietyId > 0)
            {
                foreach (var memberDetails in registeredSocietyDto.societyMemberDetails)
                {
                    memberDetails.registeredSocietyId = registeredSocietyId;
                    //memberDetails.createdDate = DateTime.Now.Date;
                    //memberDetails.updatedDate = DateTime.Now.Date;
                    memberDetails.societyMemberDetailsId = 0;
                }
                var societyMemberDetails = _mapper.Map<List<SocietyMemberDetailsDto>, List<SocietyMemberDetails>>(registeredSocietyDto.societyMemberDetails);
              int result = Convert.ToInt32(await _societyMemberDetailsRepository.AddRegisteredSocietyMemberListAsync(societyMemberDetails));
            }
            return Convert.ToInt32(registeredSocietyId);
        }
        public async Task<int> UpdateRegisteredSocietyAsync(RegisteredSocietyDto registeredSocietyDto)
        {
            var registeredSociety = _mapper.Map<RegisteredSocietyDto, RegisteredSociety>(registeredSocietyDto);  
            registeredSociety.createdBy = Guid.NewGuid();
            registeredSociety.createdDate = DateTime.Now;
            registeredSociety.updatedBy = Guid.NewGuid();
            registeredSociety.updatedDate = DateTime.Now;

            var isSocietyUpdated = await _registeredSocietyRepository.UpdateRegisteredSocietyAsync(registeredSociety);
            int isMembersUpdated = 0;
            if(isSocietyUpdated > 0)
            {
                var societyMemberDetails = _mapper.Map<List<SocietyMemberDetails>>(registeredSocietyDto.societyMemberDetails);
               isMembersUpdated = Convert.ToInt32(await _societyMemberDetailsRepository.UpdateRegisteredSocietyMembersAsync(societyMemberDetails));
            }

            return isMembersUpdated;


            //return _registeredSocietyRepository.UpdateRegisteredSocietyAsync(registeredSociety);
         
        }
        public Task<int> DeleteRegisteredSocietyAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RegisteredSocietyDto>> GetRegisteredSocietyAsync()
        {
            var registerdSocietyList = await _registeredSocietyRepository.GetAllRegisteredSocietyAsync();
            return _mapper.Map<List<RegisteredSocietyDto>>(registerdSocietyList);
        }

        public async Task<RegisteredSocietyDto> GetRegisteredSocietyByIdAsync(int Id)
        {
            var registerdSociety = await _registeredSocietyRepository.GetRegisteredSocietyByIdAsync(Id);
            return _mapper.Map<RegisteredSocietyDto>(registerdSociety);
        }

        public Task<int> UpdateTechnicalDetailsSocietyAsync(UpdateTechnicalDetailsRegisteredSocietyDto technicalDetailsRegisteredSocietyDto)
        {
            var registeredSociety = _mapper.Map<UpdateTechnicalDetailsRegisteredSocietyDto, RegisteredSociety>(technicalDetailsRegisteredSocietyDto);
            return _registeredSocietyRepository.UpdateRegisteredSocietyAsync(registeredSociety);
        }

        public async Task<List<SocietyMemberDesignationDto>> GetSocietyMemberDesignationList()
        {
            var societyMemberDesignation = await _registeredSocietyRepository.GetSocietyMemberDesignationsAsync();
            return _mapper.Map<List<SocietyMemberDesignationDto>>(societyMemberDesignation);
        }

        public async Task<RegisteredSocietyDto> GetRegisteredSocietyInfoAsync(string registeredSocietyCode)
        {
            RegisteredSociety registerdSociety = await _registeredSocietyRepository.GetRegisteredSocietyInfoAsync(registeredSocietyCode);
            RegisteredSocietyDto registeredSocietyDto = _mapper.Map<RegisteredSociety, RegisteredSocietyDto>(registerdSociety);
            List<SocietyMemberDetails> societyMemberDetails = await _societyMemberDetailsRepository.GetSocietyCommitteeMembersAsync(registerdSociety.registeredSocietyId);
            registeredSocietyDto.societyMemberDetails = _mapper.Map<List<SocietyMemberDetails>, List<SocietyMemberDetailsDto>>(societyMemberDetails);
            return registeredSocietyDto;
        }

    }
}
