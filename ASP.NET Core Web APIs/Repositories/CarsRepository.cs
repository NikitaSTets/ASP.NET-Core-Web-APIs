using System;
using System.Linq.Expressions;
using ASP.NET_Core_Web_APIs.Models;
using ASP.NET_Core_Web_APIs.Repositories.Interfaces;

namespace ASP.NET_Core_Web_APIs.Repositories
{
    public class CarsRepository : IRepository<Car>
    {
        public Car GetById(int id)
        {
            if (id == 0 || id > 1000)
            {
                return null;
            }

            return new Car
            {
                Id = id,
                MakeName = "Chevrolet",
                ModelName = "Comaro"
            };
        }

        public void Update(Car entity)
        {
            return;
        }

        public Car GetFirst(Expression<Func<Car, bool>> predicate)
        {
            var random = new Random();
            var value = random.Next(1, 10);

            if (value > 3)
            {
                return null;
            }

            return new Car
           {
               Id = 10,
               MakeName = "Audi",
               ModelName = "A4"
           };
        }
    }
}