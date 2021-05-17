using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Routs.RemoveRout
{
    public class RemoveRoutRequest : IRequest
    {
        public Guid Id { get; set; }
    }
}
