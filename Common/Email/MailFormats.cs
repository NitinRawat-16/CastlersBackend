using castlers.Models;
using castlers.Common.Enums;

namespace castlers.Common.Email;

public class MailFormats
{
    private const string SENDER = "Castlers";
    private const string FROM = "support@mail.castlers.co.in";
    private const string DOMAIN = "mail.castlers.co.in";
    public StringContent GetMailBody(SendTo sendTo)
    {
        StringContent content = new StringContent(string.Empty);
        switch (sendTo.EMailType)
        {
            case EmailTypes.LoginOTP:
                // We are creating the content of email filling details of who we want to send and other details.
                content = new StringContent
                (
                "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"name\":\"" + sendTo.Name + "\",\"otp\":\"12345\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Login_OTP_Mail + "\"}"
                );
                break;
            case EmailTypes.SocietyRegistration:
                content = new StringContent
                (
                "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"societyname\":\"" + sendTo.Name + "\",\"societycode\":\"" + sendTo.RegisteredSocietyCode + "\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Society_Registration + "\"}"
                );
                break;
            case EmailTypes.MemberRegistration:
                content = new StringContent
                (
                "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"membername\":\"" + sendTo.Name + "\",\"societycode\":\"" + sendTo.RegisteredSocietyCode + "\", \"societyname\":\"" + sendTo.SocietyName + "\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Member_Registration + "\"}"
                );
                break;
            case EmailTypes.DeveloperRegister:
                content = new StringContent
                (
                "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"developername\":\"" + sendTo.Name + "\",\"developercode\":\"" + sendTo.Message + "\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Developer_registration + "\"}"
                );
                break;
            case EmailTypes.LetterOfInterest:
                content = new StringContent
                (
                "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"developername\":\"" + sendTo.Name + "\",\"societyname\":\"" + sendTo.SocietyName + "\",\"aminterested\":\"" + sendTo.InterestedDevAPI + "\",\"amnotinterested\":\"" + sendTo.UninterestedDevAPI + "\", \"address\":\"" + sendTo.SocietyLetterOfInterestDetails.registeredAddress + "\"}}],\"from\":{\"name\":\" " + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Letter_Of_Interest + "\"}"
                );
                break;
            case EmailTypes.LetterOfInterestReceived:
                content = new StringContent
                (
                "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"contactcastlers\":\"" + DOMAIN + "\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Letter_Of_Interest_Received + "\"}"
                );
                break;
            case EmailTypes.SendTenderNotice:
                content = new StringContent
                (
                "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"enddate\":\"" + sendTo.SendTenderNoticeEndDate + "\",\"filltender\":\"" + sendTo.SendTenderNoticeETenderFormAPI + "\",\"openingdate\":\"" + sendTo.SendTenderNoticePublicationDate + "\",\"societyname\":\"" + sendTo.SocietyName + "\",\"startdate\":\"" + sendTo.SendTenderNoticeStartDate + "\",\"tendercode\":\"" + sendTo.TenderCode + "\" , \"viewdocuments\":\"" + sendTo.SendTenderNoticeViewDocAPI + "\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Send_Tender_Notice + "\"}"
                );
                break;
            case EmailTypes.AdminRegistration:
                content = new StringContent
                (
                "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"admincode\":\"" + sendTo.Message + "\", \"name\":\"" + sendTo.Name + "\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Admin_Registration + "\"}"
                );
                break;
            case EmailTypes.ChairmanApproveTender:
                content = new StringContent
                (
                "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"approvetenderdetails\":\"" + sendTo.Message + "\", \"societyname\":\"" + sendTo.SocietyName + "\"}}],\"from\":{\"name\":\"" + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Chairmen_Approve_Tender + "\"}"
                );
                break;
            case EmailTypes.VotingNotification:
                content = new StringContent
                (
                     "{\"recipients\":[{\"to\":[{\"name\":\"" + sendTo.Name + "\",\"email\":\"" + sendTo.Email + "\"}],\"variables\":{\"membername\":\"" + sendTo.Name + "\",\"societyname\":\"" + sendTo.SocietyName + "\",\"startdate\":\"" + sendTo.SendTenderNoticePublicationDate + "\",\"enddate\":\"" + sendTo.SendTenderNoticePresentationDate + "\", \"vote\":\"" + sendTo.Message + "\"}}]," +
                "\"from\":{\"name\":\" " + SENDER + "\",\"email\":\"" + FROM + "\"},\"domain\":\"" + DOMAIN + "\",\"template_id\":\"" + EmailTemplateNames.Voting_Notification + "\"}"
                   );
                break;
        }
        return content;
    }
}
