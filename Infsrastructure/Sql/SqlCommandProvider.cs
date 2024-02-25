using System.Data;
using System.Data.SqlClient;

namespace Infrastructure.Sql
{
    public class SqlCommandProvider : IDisposable
    {
        private SqlConnection _connection;

        public SqlCommandProvider(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async Task<SqlCommand> GetSqlCommandAsync(string script)
        {
            if(_connection.State != ConnectionState.Open)
                await _connection.OpenAsync();

            var command = new SqlCommand(script, _connection);
            return command;
        }

        public async void Dispose()
        {
            await _connection.CloseAsync();
            _connection.Dispose();
        }
    }
}
