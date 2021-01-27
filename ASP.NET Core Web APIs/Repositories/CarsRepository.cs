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
    }
}