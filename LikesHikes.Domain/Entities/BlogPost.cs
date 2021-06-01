using LikesHikes.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LikesHikes.Domain.Entities
{
    public class BlogPost : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Heading { get; set; }

        public string Text { get; set; }

        public DateTime PublishingDate { get; set; }


        public Guid? AppImageId { get; set; }

        public virtual AppImage AppImage { get; set; }

        public virtual IEnumerable<BlogPostComment> BlogPostComments { get; set; }
    }
}
