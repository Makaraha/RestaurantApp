using System.Data.SqlClient;

namespace Infrastructure.Sql
{
    public class SqlExecutor
    {
        private SqlDataMapper _dataMapper;
        private SqlCommandProvider _commandProvider;

        public SqlExecutor(SqlCommandProvider commandProvider, SqlDataMapper dataMapper)
        {
            _dataMapper = dataMapper;
            _commandProvider = commandProvider;
        }

        public async Task<int> ExecuteNonQueryAsync(string script)
        {
            using var command = await _commandProvider.GetSqlCommandAsync(script);
            return await command.ExecuteNonQueryAsync();
        }

        public async Task<List<T>> ExecuteQuery<T>(string script)
            where T : new()
        {
            using var reader = await ExecuteDataReaderAsync(script);
            return await _dataMapper.MapToAsync<T>(reader);
        }

        public async Task<SqlDataReader> ExecuteDataReaderAsync(string script)
        {
            using var command = await _commandProvider.GetSqlCommandAsync(script);
            return await command.ExecuteReaderAsync();
        }
    }
}
