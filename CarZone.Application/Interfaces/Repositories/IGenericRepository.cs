namespace CarZone.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {

        public Task<T> GetById(int id);
        public Task<IEnumerable<T>> GetAll();

        public Task<bool> Delete(int id);

        public Task<T> Create(T obj,int id=int.MinValue);

        public Task<bool> Update(int id, T obj);

    }
}