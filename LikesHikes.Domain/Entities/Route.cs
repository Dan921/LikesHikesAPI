using LikesHikes.Domain;
using LikesHikes.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LikesHikes.Domain.Entities
{
    public class Route : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public double Length { get; set; }

        public int Duration { get; set; }

        public Complexity Complexity { get; set; }

        public string Region { get; set; }

        public string Description { get; set; }

        public string KeyPoints { get; set; }

        public string Coordinates { get; set; }
         
        public bool IsPublished { get; set; }

        public float? Rating { get; set; }

        public int CountOfVoces { get; set; }

        public bool IsBanned { get; set; }


        public Guid CreatedById { get; set; }

        public virtual AppUser CreatedBy { get; set; }

        public virtual IEnumerable<RouteReview> RouteReviews { get; set; }
    }
}
