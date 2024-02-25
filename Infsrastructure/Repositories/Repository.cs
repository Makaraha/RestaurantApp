using Domain.Services;
using Infrastructure.Sql;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T>
        where T : new()
    {
        private SqlScriptGenerator _scriptGenerator;
        private SqlExecutor _executor;

        public Repository(SqlScriptGenerator scriptGenerator, SqlExecutor executor) 
        {
            _scriptGenerator = scriptGenerator;
            _executor = executor;
        }

        public async Task AddAsync(T entity)
        {
            var insertScript = _scriptGenerator.InsertScript(entity);
            await _executor.ExecuteNonQueryAsync(insertScript);
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> ListAsync()
        {
            var query = _scriptGenerator.SelectScript<T>();
            return await _executor.ExecuteQuery<T>(query);
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
