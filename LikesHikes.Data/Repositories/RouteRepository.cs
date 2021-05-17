using LikesHikes.Domain.Entities;
using LikesHikes.Domain.Models;
using LikesHikes.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LikesHikes.Data.Repositories
{
    public class RouteRepository : GenericRepository<Route>, IRouteRepository
    {
        public RouteRepository(DataContext context) : base(context)
        {
            this.context = context;
        }

        public Task<IQueryable<Route>> GetRoutesUsingFilter(RouteFilterModel routeFilterModel)
        {
            IQueryable<Route> routes = context.Routes;
            if (routeFilterModel != null)
            {
                if (!string.IsNullOrEmpty(routeFilterModel.Name))
                {
                    routes = routes.Where(a => a.Name.Contains(routeFilterModel.Name));
                }
                if (routeFilterModel.Complexity != null)
                {
                    routes = routes.Where(a => a.Complexity == routeFilterModel.Complexity);
                }
                if (!string.IsNullOrEmpty(routeFilterModel.Region))
                {
                    routes = routes.Where(a => a.Region.Contains(routeFilterModel.Region));
                }
                if (!string.IsNullOrEmpty(routeFilterModel.KeyPoints))
                {
                    routes = routes.Where(a => a.KeyPoints.Contains(routeFilterModel.KeyPoints));
                }
                if (routeFilterModel.Rating != null)
                {
                    routes = routes.Where(a => a.Rating > routeFilterModel.Rating);
                }
            }
            return Task.FromResult(routes);
        }
    }
}
