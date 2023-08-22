﻿using AutoMapper;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;
using castlers.Common.Email;

namespace castlers.Services
{
    public class RegisteredSocietyManager : IRegisteredSocietyService
    {
        #region Variables & Construction
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly ITenderRepository _tenderRepo;
        private readonly ISocietyDocRepository _societyDocRepository;
        private readonly ILetterOfInterestRepository _letterOfInterestRepository;
        private readonly IRegisteredSocietyRepository _registeredSocietyRepository;
        private readonly ISocietyMemberDetailsRepository _societyMemberDetailsRepository;

        public RegisteredSocietyManager(IRegisteredSocietyRepository registeredSocietyRepository, IMapper mapper, IEmailSender emailSender,
            ISocietyMemberDetailsRepository societyMemberDetailsRepository, ISocietyDocRepository societyDocRepository, ITenderRepository tenderRepo, ILetterOfInterestRepository letterOfInterestRepository)
        {
            _mapper = mapper;
            _tenderRepo = tenderRepo;
            _emailSender = emailSender;
            _societyDocRepository = societyDocRepository;
            _letterOfInterestRepository = letterOfInterestRepository;
            _registeredSocietyRepository = registeredSocietyRepository;
            _societyMemberDetailsRepository = societyMemberDetailsRepository;
        }
        #endregion

        #region User Defined Method

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

                #region Send Mail
                //Society registered confirmation mail to society 
                SendTo sendTo = new SendTo
                {
                    Name = registeredSocietyDto.societyName,
                    Email = registeredSocietyDto.email,
                    EMailType = Common.Enums.EmailTypes.SocietyRegister,
                    Message = registeredSocietyDto.societyRegisteredCode
                };
                _emailSender.SendEmailAsync(sendTo);
                //Member registered confirmation mail to society and members.
                if (result > 0)
                {
                    var memberList = MemberListForEmail(societyMemberDetails, registeredSociety.societyRegisteredCode);
                    _emailSender.SendEmailAsync(memberList);
                }
                #endregion
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
            if (isSocietyUpdated > 0)
            {
                var societyMemberDetails = _mapper.Map<List<SocietyMemberDetails>>(registeredSocietyDto.societyMemberDetails);
                isMembersUpdated = Convert.ToInt32(await _societyMemberDetailsRepository.UpdateRegisteredSocietyMembersAsync(societyMemberDetails));
            }
            return isMembersUpdated;
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
        public async Task<int> UpdateTechnicalDetailsSocietyAsync(UpdateTechnicalDetailsRegisteredSocietyDto technicalDetailsRegisteredSocietyDto)
        {
            //var registeredSociety = _mapper.Map<UpdateTechnicalDetailsRegisteredSocietyDto, RegisteredSociety>(technicalDetailsRegisteredSocietyDto);
            try
            {
                return await _registeredSocietyRepository.UpdateTechnicalDetailsSocietyAsync(technicalDetailsRegisteredSocietyDto);
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<SocietyInfoViewDto> GetRegSocietyInfoWithDocDetailsAsync(int registeredSocietyId)
        {
            try
            {
                var societyDetails = _mapper.Map<RegSocietyWithTechDetailsDto>
                    (await _registeredSocietyRepository.GetRegisteredSocietyWithTechnicalDetails(registeredSocietyId));

                var documentList = await _societyDocRepository.GetSocietyDocs(registeredSocietyId);

                var societyCommitteeMemberList = _mapper.Map<List<SocietyMemberDetailsDto>>
                    (await _societyMemberDetailsRepository.GetSocietyCommitteeMembersAsync(registeredSocietyId));

                var societyTenderDetailsList = _mapper.Map<List<SocietyTenderDetailsDto>>(await _tenderRepo.GetTenderDetailsByIdAsync(registeredSocietyId));

                //var societyTenderDetailList = _mapper.Map<List<TenderDetailsDto>>(await _registeredSocietyRepository.GetRegSocietyTenderDetails(registeredSocietyId));

                var developerTenderList = await _registeredSocietyRepository.GetDeveloperTendersBySocietyId(registeredSocietyId);

                var preTenderCompliances = await _letterOfInterestRepository.GetSocietyPreTenderCompliances(registeredSocietyId);

                var viewLetterOfInterestReceived = await _letterOfInterestRepository.GetSocietyLetterOfInterestReceived(registeredSocietyId);

                SocietyInfoViewDto societyInfoViewDto = new SocietyInfoViewDto
                {
                    regSocietyWithTechDetailsDto = societyDetails,
                    allDocuments = documentList,
                    committeeMembers = societyCommitteeMemberList,
                    developerTendersList = developerTenderList,
                    societyTendersList = societyTenderDetailsList,
                    preTenderCompliances = preTenderCompliances,
                    letterOfInterestReceived = viewLetterOfInterestReceived,
                };

                return societyInfoViewDto;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<RegisteredSocietyTechnicalDetails> GetRegisteredSocietyTechnicalDetails(int registeredSocietyId)
        {
            try
            {
                return await _registeredSocietyRepository.GetRegisteredSocietyTechnicalDetails(registeredSocietyId);
            }
            catch (Exception) { throw; }
        }
        public Task<int> DeleteRegisteredSocietyAsync(int Id)
        {
            throw new NotImplementedException();
        }
        private List<SendTo> MemberListForEmail(List<SocietyMemberDetails> societyMemberDetails, string societyCode)
        {
            var memberlist = new List<SendTo>();
            foreach (var member in societyMemberDetails)
            {
                memberlist.Add(new SendTo
                {
                    Name = member.memberName,
                    Email = member.email,
                    Message = societyCode,
                    EMailType = Common.Enums.EmailTypes.SocietyMemberRegister
                });
            }
            return memberlist;
        }
        public async Task<RegisteredSocietyWithTechnicalDetails> GetRegisteredSocietyWithTechnicalDetails(int societyId)
        {
            try
            {
                return await _registeredSocietyRepository.GetRegisteredSocietyWithTechnicalDetails(societyId);
            }
            catch (Exception) { throw; }
        }
        public async Task<List<ViewLetterOfInterestReceivedDto>> GetSocietyLetterOfInterestReceived(int registeredSocietyId)
        {
            try
            {
                var viewLetterOfInterestReceived = await _letterOfInterestRepository.GetSocietyLetterOfInterestReceived(registeredSocietyId);
                return viewLetterOfInterestReceived;
            }
            catch (Exception) { throw; }
        }

        #endregion
    }
}
