namespace ASP.NET_Core_Web_APIs.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(int id);

        void Update(T entity);
    }
}