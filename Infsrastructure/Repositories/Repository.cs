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

        public async Task<int> AddAsync(T entity)
        {
            var insertScript = _scriptGenerator.InsertScript(entity);
            await _executor.ExecuteNonQueryAsync(insertScript);
            return await _executor.ExecuteScalarAsync(_scriptGenerator.LastIdScript<T>());
        }

        public Task DeleteAsync(int id)
        {
            var deleteScript = _scriptGenerator.DeleteScript<T>(id);
            return _executor.ExecuteNonQueryAsync(deleteScript);
        }

        public Task<T> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> ListAsync()
        {
            var query = _scriptGenerator.SelectScript<T>();
            return _executor.ExecuteQuery<T>(query);
        }

        public Task UpdateAsync(T entity)
        {
            var query = _scriptGenerator.UpdateScript(entity);
            return _executor.ExecuteNonQueryAsync(query);
        }
    }
}
