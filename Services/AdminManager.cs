using AutoMapper;
using castlers.Dtos;
using castlers.Models;
using castlers.Repository;
using castlers.Common.Email;
using System.Security.Cryptography;

namespace castlers.Services
{
    public class AdminManager : IAdminService
    {
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IAdminRepository _adminRepo;
        public AdminManager(IAdminRepository adminRepo, IMapper mapper, IEmailSender emailSender)
        {
            _mapper = mapper;
            _adminRepo = adminRepo;
            _emailSender = emailSender;
        }
        public async Task<AdminDto> AddAdmin(AdminDto adminDto)
        {
            try
            {
                //Generate Admin Code
                var code = "AD" + adminDto.firstName.Substring(0, 2).ToUpper();
                code += adminDto.lastName.Substring(0, 1).ToUpper();
                code += RandomNumberGenerator.GetInt32(0, 10000).ToString("D5");
                //

                var adminDetails = _mapper.Map<Admin>(adminDto);
                adminDetails.adminCode = code;
                adminDetails.admindetailsId = 0;
                var id = await _adminRepo.AddAdmin(adminDetails);
                adminDto.adminDetailsId = id > 0 ? id : 0;

                // Send mail to new admin with admin code and password
                var sendTo = new SendTo()
                {
                    Name = adminDto.firstName + ' ' + adminDto.lastName,
                    Email = adminDto.email,
                    EMailType = Common.Enums.EmailTypes.AdminRegistration,
                    Message = code
                };
                if (id > 0)
                {
                    var mailResponse = _emailSender.SendEmailAsync(sendTo);
                }
                //

                return adminDto;
            }
            catch (Exception) { throw; }
        }
    }
}
