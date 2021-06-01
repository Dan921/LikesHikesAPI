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
                if (!string.IsNullOrEmpty(routeFilterModel.NameRoute))
                {
                    routes = routes.Where(a => a.Name.Contains(routeFilterModel.NameRoute));
                }
                if (routeFilterModel.Complexity != null)
                {
                    if(routeFilterModel.Complexity == "Легкий")
                    {
                        routes = routes.Where(a => a.Complexity == Complexity.Easy);
                    }
                    if (routeFilterModel.Complexity == "Средний")
                    {
                        routes = routes.Where(a => a.Complexity == Complexity.Medium);
                    }
                    if (routeFilterModel.Complexity == "Сложный")
                    {
                        routes = routes.Where(a => a.Complexity == Complexity.Difficult);
                    }
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
                    routes = routes.Where(a => a.Rating >= routeFilterModel.Rating);
                }
            }
            return Task.FromResult(routes);
        }

        public Task<IQueryable<Route>> GetUserRoutesUsingFilter(UserRouteFilterModel userRouteFilterModel)
        {
            IQueryable<Route> routes = context.Routes;
            if (userRouteFilterModel != null)
            {
                if (!string.IsNullOrEmpty(userRouteFilterModel.NameRoute))
                {
                    routes = routes.Where(a => a.Name.Contains(userRouteFilterModel.NameRoute));
                }
                if (userRouteFilterModel.Complexity != null)
                {
                    if (userRouteFilterModel.Complexity == "Легкий")
                    {
                        routes = routes.Where(a => a.Complexity == Complexity.Easy);
                    }
                    if (userRouteFilterModel.Complexity == "Средний")
                    {
                        routes = routes.Where(a => a.Complexity == Complexity.Medium);
                    }
                    if (userRouteFilterModel.Complexity == "Сложный")
                    {
                        routes = routes.Where(a => a.Complexity == Complexity.Difficult);
                    }
                }
                if (!string.IsNullOrEmpty(userRouteFilterModel.Region))
                {
                    routes = routes.Where(a => a.Region.Contains(userRouteFilterModel.Region));
                }
                if (!string.IsNullOrEmpty(userRouteFilterModel.KeyPoints))
                {
                    routes = routes.Where(a => a.KeyPoints.Contains(userRouteFilterModel.KeyPoints));
                }
                if (userRouteFilterModel.IsPublished != null)
                {
                    routes = routes.Where(a => a.IsPublished == userRouteFilterModel.IsPublished);
                }
                if (userRouteFilterModel.Rating != null)
                {
                    routes = routes.Where(a => a.Rating >= userRouteFilterModel.Rating);
                }
            }
            return Task.FromResult(routes);
        }
    }
}
