using Microsoft.EntityFrameworkCore;

namespace castlers.Models
{
    [Keyless]
    public class Admin
    {
        public int admindetailsId {  get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string mobileNumber { get; set; }
        public string adminCode { get; set; }
        public bool isActive { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }
        public Guid createdBy { get; set; }
        public Guid updatedBy { get; set; }
    }
}
