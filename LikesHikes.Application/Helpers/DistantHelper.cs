using LikesHikes.Domain.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LikesHikes.Application.Helpers
{
    public class DistantHelper
    {
        public double CalculateDistant(List<Coordinate> coordinates)
        {
            double dist = 0;
            for (int i = 0; i < coordinates.Count - 1; i++)
            {
                double lat1 = coordinates[i].lat * Math.PI / 180;
                double lat2 = coordinates[i + 1].lat * Math.PI / 180;
                double long1 = coordinates[i].lng * Math.PI / 180;
                double long2 = coordinates[i + 1].lng * Math.PI / 180;
                double cl1 = Math.Cos(lat1);
                double cl2 = Math.Cos(lat2);
                double sll = Math.Sin(lat1);
                double sl2 = Math.Sin(lat2);
                double delta = long2 - long1;
                double cdelta = Math.Cos(delta);
                double sdelta = Math.Sin(delta);
                double y = Math.Sqrt(Math.Pow(cl2 * sdelta, 2) + Math.Pow(cl1 * sl2 - sll * cl2 * cdelta, 2));
                double x = sll * sl2 + cl1 * cl2 * cdelta;
                double ad = Math.Atan2(y, x);
                dist += ad * 6372795;
            }
            return dist / 1000;
        }
    }
}
