using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace castlers.Dtos
{
    public class BlogsDto
    {
        public int? blogId { get; set; }
        public int? typeId { get; set; }
        public string? typeName { get; set; }
        public string? heading { get; set; }
        public string? createdBy { get; set; }
        public string? path { get; set; }

        [MaxLength(4000)]
        public string? description { get; set; } 
        public IFormFile? file { get; set; }
    }
}
