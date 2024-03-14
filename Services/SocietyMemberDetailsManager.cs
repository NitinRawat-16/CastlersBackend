using AutoMapper;
using castlers.Dtos;
using castlers.Models;
using castlers.Common.SMS;
using castlers.Repository;
using castlers.Common.Email;
using castlers.ResponseDtos;
using castlers.Common.Converters;

namespace castlers.Services
{
    public class SocietyMemberDetailsManager : ISocietyMemberDetailsService
    {
        private readonly IMapper _mapper;
        private readonly ISMSSender _smsSender;
        private readonly IEmailSender _emailSender;
        private readonly IRegisteredSocietyService _regSocietyService;
        private readonly ISocietyMemberDetailsRepository _societyMemberDetailsRepository;

        public SocietyMemberDetailsManager(ISocietyMemberDetailsRepository societyMemberDetailsRepository, IMapper mapper, IRegisteredSocietyService regSocietyService, IEmailSender emailSender, ISMSSender sMSSender)
        {
            _mapper = mapper;
            _smsSender = sMSSender;
            _emailSender = emailSender;
            _regSocietyService = regSocietyService;
            _societyMemberDetailsRepository = societyMemberDetailsRepository;
        }
        public Task<int> AddRegisteredSocietyMemberListAsync(List<SocietyMemberDetails> societyMemberDetails)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddRegisteredSocietyNewMembersAsync(SocietyNewMemberDetailsDto memberDetailsDto)
        {
            var members = ExcelFileConverter.ConvertToList(memberDetailsDto.societyId, memberDetailsDto.societyNewMemberDetails);
            SocietyNewMemberDetails newMemberDetails = new SocietyNewMemberDetails
            {
                societyId = memberDetailsDto.societyId,
                societyNewMemberDetails = members
            };

            var result = await _societyMemberDetailsRepository.AddRegisteredSocietyNewMembersAsync(newMemberDetails);
            var societyDetails = await _regSocietyService.GetRegisteredSocietyByIdAsync(memberDetailsDto.societyId);

            newMemberDetails.societyNewMemberDetails = newMemberDetails.societyNewMemberDetails
                .Where(m => m.email != string.Empty || m.email != "" || m.email is null).ToList();

            List<SendTo> newRegisteredMembersEmail = new List<SendTo>();
            foreach (var member in newMemberDetails.societyNewMemberDetails)
            {
                newRegisteredMembersEmail.Add(new SendTo
                {
                    Name = member.memberName,
                    Email = string.IsNullOrEmpty(member.email) ? string.Empty : member.email,
                    EMailType = Common.Enums.EmailTypes.MemberRegistration,
                    RegisteredSocietyCode = societyDetails.societyRegisteredCode,
                    SocietyName = societyDetails.societyName
                });
            }

            MailResponseDto emailResponse = new MailResponseDto();
            // Send Mail to newly registered member of society
            if (result.Status == Common.Enums.Status.success)
            {
                emailResponse = await _emailSender.SendEmailAsync(newRegisteredMembersEmail);
            }

            //var newRegisteredMembersSMS = new List<SendSMS>();
            //foreach (var member in newMemberDetails.societyNewMemberDetails)
            //{
            //    newRegisteredMembersSMS.Add(new SendSMS
            //    {
            //        message = societyDetails.societyRegisteredCode,
            //        mobileNumber = string.IsNullOrEmpty(member.mobileNumber) ? string.Empty : member.mobileNumber,
            //        smsType = Common.Enums.SMSTypes.MemberRegistration
            //    });
            //}

            //SMSResponse smsResponse = new SMSResponse();
            //// Send SMS to newly registered member of society
            //if(emailResponse.Status == "success")
            //{
            //    smsResponse = await _smsSender.SendBlukSMS(newRegisteredMembersSMS);
            //}

            if (emailResponse.Status == "success")
            {
                return 1;
            }
            return 0;

        }

        public async Task<int> UpdateRegisteredSocietyMemberAsync(List<SocietyMemberDetailsDto> societyMemberDetails)
        {
            var newMembers = _mapper.Map<List<SocietyMemberDetails>>(societyMemberDetails);
            return await _societyMemberDetailsRepository.UpdateRegisteredSocietyMembersAsync(newMembers);
        }

        public async Task<List<SocietyMemberDetailsDto>> GetRegisteredSocietyMembersBySocietyIdAsync(int registeredSocietyId)
        {
            var societyMemberDetails = await _societyMemberDetailsRepository.GetRegisteredSocietyMembersBySocietyIdAsync(registeredSocietyId);
            return _mapper.Map<List<SocietyMemberDetailsDto>>(societyMemberDetails);
        }

        public Task<int> DeleteRegisteredSocietyMemberByIdAsync(DeleteSocietyMemberDto deleteSocietyMemberDto)
        {
            return _societyMemberDetailsRepository
                .DeleteRegisteredSocietyMemberByIdAsync(deleteSocietyMemberDto.societyMemberDetailsId, deleteSocietyMemberDto.registeredSocietyId);
        }

        public async Task<List<SocietyMemberDetailsDto>> GetRegisteredSocietyCommitteeMembersBySocietyIdAsync(int registeredSocietyId)
        {
            var societyMemberDetails = _societyMemberDetailsRepository.GetSocietyCommitteeMembersAsync(registeredSocietyId);
            return _mapper.Map<List<SocietyMemberDetailsDto>>(societyMemberDetails);
        }

        public async Task<List<SocietyMemberDetailsDto>> GetSocietyAllMembersAsync(int societyId)
        {
            try
            {
                return _mapper.Map<List<SocietyMemberDetailsDto>>(await _societyMemberDetailsRepository.GetSocietyAllMembersAsync(societyId));
            }
            catch (Exception) { throw; }
        }
    }
}
