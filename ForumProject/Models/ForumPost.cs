using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectForum.Models
{
    public class ForumPost
    {
        [Key]
        public string Uid { get; private set; }

        [Required]
        public string OwnerId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        [NotMapped]
        public virtual SiteUser Owner { get; set; }

        [NotMapped]
        public virtual IEnumerable<ForumPostComment> Comments { get; set; }

        public ForumPost()
        {
            Uid = Guid.NewGuid().ToString();
        }

    }
}
