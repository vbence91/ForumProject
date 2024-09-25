using ForumProject.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectForum.Models
{
    public class Comment
    {
        [Key]
        public string Uid { get; private set; }

        public string? PostId { get; set; }

        public string? ParentId { get; set; }

        public string? OwnerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Content { get; set; }

        public int Rating { get; set; }

        public DateTime DateCreated { get; set; }

        [NotMapped]
        public virtual SiteUser Owner { get; set; }

        [NotMapped]
        public virtual ForumPost ForumPost { get; set; }

        public Comment()
        {
            DateCreated = DateTime.Now;
            Uid = Guid.NewGuid().ToString();
            Rating = 0;
        }
    }
}
