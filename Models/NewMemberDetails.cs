using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace castlers.Models
{
    public class NewMemberDetails
    {
        [Key]
        public int id { get; set; }
        public int societyId { get; set; }
        public string societyCode { get; set; }
        public List<SocietyMemberDetails> societyMemberDetails { get; set; }
    }
}
