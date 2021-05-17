using LikesHikes.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LikesHikes.Domain.Entities
{
    public class BlogPostComment : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Text { get; set; }

        public DateTime Time { get; set; }


        public Guid BlogPostId { get; set; }

        public virtual BlogPost BlogPost { get; set; }

        public Guid AppUserId { get; set; }

        public virtual AppUser AppUser { get; set; }
    }
}
