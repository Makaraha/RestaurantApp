namespace Domain.Services
{
    public interface IRepository<T>
    {
        public Task<T> GetAsync(int id);

        public Task<List<T>> ListAsync();

        public Task AddAsync(T entity);

        public Task UpdateAsync(T entity);

        public Task DeleteAsync(int id);
    }
}
