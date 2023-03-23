﻿using AutoMapper;
using castlers.DbContexts;
using castlers.Dtos;
using castlers.Models;
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
            registeredSociety.createdBy=Guid.NewGuid();
            registeredSociety.createdDate = DateTime.UtcNow;
            registeredSociety.updatedBy = Guid.NewGuid();
            registeredSociety.updatedDate = DateTime.UtcNow;
            int result = Convert.ToInt32( await _registeredSocietyRepository.AddRegisteredSocietyAsync(registeredSociety));
            if(result > 0 )
            {
                foreach(var memberDetails in registeredSocietyDto.societyMemberDetails)
                {
                    memberDetails.registeredSocietyId = result;
                 //  memberDetails.createdDate = DateTime.Now.Date;
                   // memberDetails.updatedDate = DateTime.Now.Date;
                    memberDetails.societyMemberDetailsId = 0; 

                }
                var societyMemberDetails =  _mapper.Map<List<SocietyMemberDetailsDto>, List<SocietyMemberDetails>>(registeredSocietyDto.societyMemberDetails);
                 result = Convert.ToInt32(await _societyMemberDetailsRepository.AddRegisteredSocietyMemberListAsync(societyMemberDetails));
            }

            return Convert.ToInt32(result);
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

        public Task<int> UpdateRegisteredSocietyAsync(RegisteredSocietyDto registeredSocietyDto)
        {
            var registeredSociety = _mapper.Map<RegisteredSocietyDto, RegisteredSociety>(registeredSocietyDto);
            return _registeredSocietyRepository.UpdateRegisteredSocietyAsync(registeredSociety);
        }

        public Task<int> UpdateTechnicalDetailsSocietyAsync(UpdateTechnicalDetailsRegisteredSocietyDto technicalDetailsRegisteredSocietyDto)
        {
            var registeredSociety = _mapper.Map<UpdateTechnicalDetailsRegisteredSocietyDto, RegisteredSociety>(technicalDetailsRegisteredSocietyDto);
            return _registeredSocietyRepository.UpdateRegisteredSocietyAsync(registeredSociety);
        }
    }
}
