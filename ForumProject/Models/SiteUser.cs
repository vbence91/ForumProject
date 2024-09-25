using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectForum.Models
{
    public class SiteUser: IdentityUser
    {
        [MaxLength(50)]
        public string DisplayName { get; set; }

        // profile picture type
        public string ContentType { get; set; }

        // profile picture data
        public byte[] Data { get; set; }

        [NotMapped]
        public virtual IEnumerable<ForumPost> Posts { get; set; }

        [NotMapped]
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}
