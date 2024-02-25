using System.Reflection;
using Domain.Attributes;

namespace Infrastructure.Sql
{
    public class SqlScriptGenerator
    {
        public string SelectScript<T>()
        {
            var type = typeof(T);
            var tableName = ExtractTableName(type);
            var properties = type.GetProperties().Select(x => x.Name);

            return $"SELECT {string.Join(", ", properties)} FROM {tableName}";
        }

        public string InsertScript(object entity)
        {
            var type = entity.GetType();
            var tableName = ExtractTableName(type);
            var fields = ExtractFields(type)
                .Where(x => !x.IsPrimaryKey)
                .ToList(); 

            var names = string.Join(", ", fields.Select(x => x.Name));
            var values = string.Join(", ", fields.Select(x => $"'{x.Value?.ToString() ?? "NULL"}'"));

            return $"INSERT INTO {tableName} ({names}) VALUES ({values})";
        }

        private IEnumerable<SqlField> ExtractFields(object entity)
        {
            var type = entity.GetType();
            var properties = type.GetProperties();

            foreach(var property in properties)
            {
                yield return new SqlField()
                {
                    Name = property.Name,
                    Value = property.GetValue(entity),
                    IsPrimaryKey = property.GetCustomAttribute<PrimaryKeyAttribute>() != null
                };
            }
        }

        private string ExtractTableName(Type type)
        {
            var tableName = type.GetCustomAttribute<TableNameAttribute>()?.TableName;

            if (tableName == null)
                throw new Exception("Failed to extract tableName attribute");

            return tableName;
        }
    }
}
