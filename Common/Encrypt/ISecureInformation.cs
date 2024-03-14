namespace castlers.Common.Encrypt
{
    public interface ISecureInformation
    {
        public string Encrypt(string data);
        public string Decrypt(string data);
    }
}
