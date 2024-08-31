using Microsoft.AspNetCore.Identity;

namespace ProjectForum.Models
{
    public class SiteUser: IdentityUser
    {
        public string Username { get; set; }

        // profile picture type
        public string ContentType { get; set; }

        // profile picture data
        public byte[] Data { get; set; }
    }
}
