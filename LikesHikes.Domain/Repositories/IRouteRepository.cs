using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikesHikes.Domain.Repositories
{
    public interface IRouteRepository : IGenericRepository<Route>
    {
        Task<IQueryable<Route>> GetRoutesUsingFilter(RouteFilterModel routeFilterModel);
    }
}
