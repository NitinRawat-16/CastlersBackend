using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace castlers.Models
{
    public class SocietyNewMemberDetails
    {
        public int societyId { get; set; }
        public List<SocietyMemberDetails> societyNewMemberDetails { get; set; }
    }
}
