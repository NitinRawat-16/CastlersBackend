using System.Text;
using System.Security.Cryptography;

namespace castlers.Common.Encrypt
{
    public class SendIntimationObj
    {
        public int developerId { get; set; }
        public int societyId { get; set; }
        public int tenderId { get; set; }
        public bool interested { get; set; }
        public int offerId { get; set; }
    }

    public class TenderNoticeObj
    {
        public int societyId { get; set; }
        public int developerId { get; set; }
        public int tenderNoticeId { get; set; }
        public string? tenderCode { get; set; }
    }

    public class VotingObj
    {
        public int memberId { get; set; }
        public int electionId { get; set; }
    }

    public class SecureInformation : ISecureInformation
    {
        private readonly byte[] _byteArray;
        private readonly IConfiguration _config;
        public SecureInformation(IConfiguration configuration)
        {
            _config = configuration;
            _byteArray = new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 };
        }
        public string Encrypt(string data)
        {
            string encryptData = string.Empty;
            try
            {
                string EncryptionKey = _config.GetSection("EncryptionKey").Value ?? string.Empty;
                byte[] clearBytes = Encoding.Unicode.GetBytes(data);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, _byteArray);
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }
                        encryptData = Convert.ToBase64String(ms.ToArray());
                    }
                }
                return encryptData;
            }
            catch (Exception) { throw; }
        }

        public string Decrypt(string data)
        {
            string decryptData = string.Empty;
            try
            {
                string EncryptionKey = _config.GetSection("EncryptionKey").Value ?? string.Empty;
                data = data.Replace(" ", "+");
                byte[] cipherBytes = Convert.FromBase64String(data);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, _byteArray);
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        decryptData = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
                return decryptData;
            }
            catch (Exception) { throw; }
        }
    }
}
