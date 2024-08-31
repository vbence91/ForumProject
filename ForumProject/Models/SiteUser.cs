using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectForum.Models
{
    public class SiteUser: IdentityUser
    {
        public string Username { get; set; }

        // profile picture type
        public string ContentType { get; set; }

        // profile picture data
        public byte[] Data { get; set; }

        [NotMapped]
        public virtual IEnumerable<ForumPost> Posts { get; set; }

        [NotMapped]
        public virtual IEnumerable<ForumPostComment> Comments { get; set; }
    }
}
