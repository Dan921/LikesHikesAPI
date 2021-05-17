using LikesHikes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Models
{
    public class ReportModel
    {
        public ReportModel(Report report)
        {
            Id = report.Id;
            Name = report.Name;
            Text = report.Text;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }
    }
}
