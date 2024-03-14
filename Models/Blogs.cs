using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace castlers.Models
{
    [Keyless]
    public class Blogs
    {
        public int? blogId { get; set; }
        public int? typeId { get; set; }
        public string? typeName { get; set; }
        public string? heading { get; set; }
        public string? description { get; set; }
        public string? path { get; set; }
        public string? createdBy { get; set; }

        [NotMapped]
        public IFormFile? file { get; set; }
    }
}
