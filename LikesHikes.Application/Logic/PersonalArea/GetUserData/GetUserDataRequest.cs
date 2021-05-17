using LikesHikes.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.PersonalArea.GetUserData
{
    public class GetUserDataRequest : IRequest<GetUserDataResult>
    {
        public Guid? AppUserId { get; set; }

        public bool OnlyPassedRoutes { get; set; }
    }
}
