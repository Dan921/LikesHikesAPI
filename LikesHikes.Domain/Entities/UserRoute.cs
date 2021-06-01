using LikesHikes.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LikesHikes.Domain.Entities
{
    public class UserRoute : IEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool IsPassed { get; set; }


        public Guid AppUserId { get; set; }

        //public virtual AppUser AppUser { get; set; }

        public Guid RouteId { get; set; }

        public virtual Route Route { get; set; }

        public Guid? ReportId { get; set; }

        public virtual Report Report { get; set; }
    }
}
