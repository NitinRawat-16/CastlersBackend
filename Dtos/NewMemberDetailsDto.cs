using castlers.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace castlers.Dtos
{
    public class NewMemberDetailsDto
    {
        public int societyId { get; set; }
        public string societyCode { get; set; }
        public IFormFile file { get; set; }
    }
}
