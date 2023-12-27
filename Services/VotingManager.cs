using castlers.Dtos;
using castlers.Models;
using System.Text.Json;
using castlers.Repository;
using castlers.Common.Encrypt;
using castlers.Common.Enums;
using castlers.Common.Email;
using System.Reflection;

namespace castlers.Services
{
    public class VotingManager : IVotingService
    {
        private readonly IConfiguration _config;
        private readonly IEmailSender _emailSender;
        private readonly IVotingRepository _votingRepo;
        private readonly ITenderService _tenderService;
        private readonly IDeveloperService _developerService;
        private readonly IAmenitiesService _amenitiesService;
        private readonly ISecureInformation _secureInformation;
        private readonly IRegisteredSocietyService _societyService;
        private readonly ISocietyMemberDetailsService _societyMemberService;
        public VotingManager(IVotingRepository votingRepository,
                            ISecureInformation secureInformation,
                            IDeveloperService developerService,
                            ITenderService tenderService,
                            ISocietyMemberDetailsService societyMemberService,
                            IAmenitiesService amenitiesService,
                            IConfiguration config,
                            IRegisteredSocietyService societyService,
                            IEmailSender emailSender)
        {
            _config = config;
            _emailSender = emailSender;
            _votingRepo = votingRepository;
            _tenderService = tenderService;
            _societyService = societyService;
            _amenitiesService = amenitiesService;
            _developerService = developerService;
            _secureInformation = secureInformation;
            _societyMemberService = societyMemberService;
        }

        public async Task<int> SaveElectionDetailsAsync()
        {
            try
            {
                var tenderPublications = await _tenderService.GetTenderPublicationsAsync();
                if (tenderPublications.Any())
                {
                    foreach (var tender in tenderPublications)
                    {
                        // Save election details
                        var tenderDetails = await _tenderService.GetSocietyTenderDetailsAsync(tender.TenderCode ?? "");
                        var memberDetails = await _societyMemberService.GetSocietyAllMembersAsync(tender.SocietyId ?? -1);

                        ElectionDetails electionDetails = new()
                        {
                            TenderId = tenderDetails.tenderId,
                            StartDate = tender.PublicationDate ?? DateTime.Now,
                            EndDate = tender.PresentationDate ?? DateTime.Now.AddDays(15),
                            Status = ElectionStatus.Active.ToString(),
                            TotalVoters = memberDetails.Count,
                            TotalVoted = 0,
                            CreationDate = DateTime.Now,
                            UpdationDate = DateTime.Now
                        };

                        electionDetails.ElectionId = await _votingRepo.SaveElectionDetails(electionDetails);

                        // Send mail regarding election and voting process

                        SendVotingNotification(tender.SocietyId, electionDetails, memberDetails);
                    }
                }
                return 1;
            }
            catch (Exception) { throw; }
        }

        private async void SendVotingNotification(int? societyId, ElectionDetails election, List<SocietyMemberDetailsDto> societyMembers)
        {
            try
            {
                var society = await _societyService.GetRegisteredSocietyByIdAsync(societyId ?? -1);
                if (societyMembers.Any())
                {
                    foreach (var member in societyMembers)
                    {
                        var votingUrl = _config.GetSection("Voting_Page_API").Value ?? string.Empty;

                        var code = _secureInformation.Encrypt(JsonSerializer.Serialize(new VotingObj
                        {
                            memberId = member.societyMemberDetailsId,
                            electionId = election.ElectionId,
                        }));

                        SendTo sendTo = new()
                        {
                            SocietyName = society.societyName,
                            Email = member.email,
                            EMailType = EmailTypes.VotingNotification,
                            Name = member.memberName,
                            SendTenderNoticePublicationDate = election.StartDate.ToString("dd-MMM-yyyy"),
                            SendTenderNoticePresentationDate = election.EndDate.ToString("dd-MMM-yyyy"),
                            Message = votingUrl + code
                        };

                        var response = await _emailSender.SendEmailAsync(sendTo);
                    }
                }
            }
            catch (Exception) { throw; }
        }

        public async Task<int> SaveMemberVote(SubmitVotingDto submitVoting)
        {
            try
            {
                if (submitVoting == null) return 0;

                var votingDetails = JsonSerializer.Deserialize<VotingObj>(_secureInformation.Decrypt(submitVoting.Code));

                int result = 0;
                if (votingDetails != null)
                {
                    MembersPreferredDevelopers preferredDevelopers = new();
                    preferredDevelopers.MemberId = Convert.ToInt32(votingDetails.memberId);
                    preferredDevelopers.ElectionId = Convert.ToInt32(votingDetails.electionId);
                    preferredDevelopers.DeveloperFirst = Convert.ToInt32(submitVoting.DeveloperIds[0]);
                    preferredDevelopers.DeveloperSecond = Convert.ToInt32(submitVoting.DeveloperIds[1]);
                    preferredDevelopers.DeveloperThird = Convert.ToInt32(submitVoting.DeveloperIds[2]);
                    preferredDevelopers.IsVoted = true;
                    preferredDevelopers.CreationDate = DateTime.Now;
                    preferredDevelopers.UpdationDate = DateTime.Now;

                    result = await _votingRepo.SaveMemberVoteAsync(preferredDevelopers);
                }
                return result;
            }
            catch (Exception) { throw; }
        }

        public async Task<bool> SendVotingNotification()
        {
            try
            {
                int societyId = -1;
                List<SocietyMemberDetailsDto> societyMembers = new();
                societyMembers = await _societyMemberService.GetRegisteredSocietyMembersBySocietyIdAsync(societyId);

                foreach (var member in societyMembers)
                {
                    VotingObj votingObj = new()
                    {
                        electionId = -1,
                        memberId = member.societyMemberDetailsId
                    };
                }
                return true;
            }
            catch (Exception) { throw; }
        }

        public async Task<List<VotingDevelopersDto>> VerifyVotingUrl(string code)
        {
            try
            {
                List<DeveloperTenderDetailsDto> developerTenders = new();
                var votingDetails = JsonSerializer.Deserialize<VotingObj>(_secureInformation.Decrypt(code));
                if (votingDetails != null)
                {
                    var electionDetails = await _votingRepo.GetElectionDetailsAsync(votingDetails.electionId);
                    if (electionDetails != null && electionDetails.TenderId > 0)
                    {
                        developerTenders = await _tenderService.GetInterestedDevelopersForTenderId(electionDetails.TenderId);
                    }
                }

                if (developerTenders.Any())
                    return await GetDevelopersForVoting(developerTenders);

                return new();
            }
            catch (Exception error)
            {
                throw new Exception("Invalid Request!", error.InnerException);
            }
        }

        public async Task<List<VotingDevelopersDto>> GetDevelopersForVoting(List<DeveloperTenderDetailsDto> developerTenders)
        {
            List<VotingDevelopersDto> developers = new();
            foreach (var developerTender in developerTenders)
            {
                VotingDevelopersDto votingDeveloper = new();
                int developerId = developerTender.developerId > 0 ? (int)developerTender.developerId : -1;

                var developerDetails = await _developerService.GetDeveloperByIdAsync(developerId);
                var developerTenderDetails = await _tenderService.GetDeveloperTenderAsync(developerId);
                var amenitiesDetails = await _amenitiesService.GetDeveloperAmenitiesAsync(developerId);
                var constSpecDetails = await _amenitiesService.GetDeveloperConstructionSpecAsync(developerId);

                votingDeveloper.DeveloperId = developerId;
                votingDeveloper.DeveloperName = developerDetails.name;
                votingDeveloper.LogoUrl = developerDetails.logoPath;
                votingDeveloper.Rating = developerDetails.reviewRatingScore ?? 0;
                votingDeveloper.TenderPdfUrl = developerTenderDetails.developerTenderPdfPath;
                votingDeveloper.AmenitiesPdfUrl = amenitiesDetails.AmenitiesPdfUrl;
                votingDeveloper.UserAmenitiesPdfUrl = amenitiesDetails.UserAmenitiesPdfUrl;
                votingDeveloper.ConstructionSpecPdfUrl = constSpecDetails.ConstructionSpecPdfUrl;
                votingDeveloper.UserConstructionSpecPdfUrl = constSpecDetails.UserConstructionSpecPdfUrl;

                developers.Add(votingDeveloper);
            }
            return developers.Any() ? developers : new();
        }
    }
}
