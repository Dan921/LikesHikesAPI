using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace LikesHikes.Application.Logic.Account.GetUserData
{
    public class GetUserDataRequest : IRequest<GetUserDataResult>
    {
        public Guid? AppUserId { get; set; }
    }
}
