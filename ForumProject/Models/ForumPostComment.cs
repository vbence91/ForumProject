using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectForum.Models
{
    public class ForumPostComment
    {
        [Key]
        public string Uid { get; private set; }

        [Required]  
        public string PostId { get; set; }

        
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

        public ForumPostComment()
        {
            DateCreated = DateTime.Now;
            Uid = Guid.NewGuid().ToString();
            Rating = 0;
        }
    }
}
