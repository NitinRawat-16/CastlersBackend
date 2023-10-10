using AutoMapper.Execution;
using castlers.Models;
using ExcelDataReader;
using System.Text;

namespace castlers.Common.Converters
{
    public static class ExcelFileConverter
    {
        public static List<SocietyMemberDetails> ConvertToList(this int societyId, IFormFile newMembersDetails)
        {
            List<SocietyMemberDetails> members = new List<SocietyMemberDetails>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = new MemoryStream())
            {
                newMembersDetails.CopyTo(stream);
                stream.Position = 0;
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    if (reader.Read())
                    {
                        while (reader.Read()) //Each row of the societyNewMemberDetails
                        {
                            members.Add(new SocietyMemberDetails
                            {
                                registeredSocietyId = societyId,
                                memberName = reader.GetValue(0) is null ? string.Empty : reader.GetValue(0).ToString(),
                                email = reader.GetValue(1) is null ? string.Empty : reader.GetValue(1).ToString(),
                                mobileNumber = reader.GetValue(2) is null ? string.Empty : reader.GetValue(2).ToString(),
                                societyMemberDesignationId = 0,
                                createdBy = Guid.NewGuid(),
                                updatedBy = Guid.NewGuid()
                            }); ;
                        }
                    }
                }
            }
            return members;
        }
    }
}
