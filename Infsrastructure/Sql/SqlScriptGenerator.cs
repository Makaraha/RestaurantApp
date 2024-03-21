using System.Globalization;
using System.Reflection;
using Domain.Attributes;

namespace Infrastructure.Sql
{
    public class SqlScriptGenerator
    {
        public string LastIdScript<T>()
        {
            var type = typeof(T);
            var tableName = ExtractTableName(type);
            return $"SELECT IDENT_CURRENT('{tableName}')";
        }

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
            var fields = ExtractFields(entity)
                .Where(x => !x.IsPrimaryKey)
                .ToList(); 

            var names = string.Join(", ", fields.Select(x => x.Name));
            var values = string.Join(", ", fields.Select(x => $"'{ValueToString(x.Value)}'"));

            return $"INSERT INTO {tableName} ({names}) VALUES ({values})";
        }

        public string DeleteScript<T>(int id)
        {
            var type = typeof(T);
            var tableName = ExtractTableName(type);

            return $"DELETE FROM {tableName} WHERE Id = {id}";
        }

        public string UpdateScript(object entity)
        {
            var type = entity.GetType();
            var tableName = ExtractTableName(type);
            var fields = ExtractFields(entity);
            var primaryKey = fields.First(x => x.IsPrimaryKey);
            var dataFields = fields.Where(x => !x.IsPrimaryKey);

            var setStatement = string.Join(',', dataFields.Select(x => $"{x.Name} = '{ValueToString(x.Value)}'"));
            return $"UPDATE {tableName} SET {setStatement} WHERE {primaryKey.Name} = '{primaryKey.Value}'";
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

        private string ValueToString(object? value)
        {
            if (value == null)
                return "NULL";

            switch (value)
            {
                case DateTime _dateTime:
                    return _dateTime.ToString("yyyy-MM-dd HH:mm:ss");

                case float _float:
                    return _float.ToString("#.##", CultureInfo.InvariantCulture);

                case decimal _decimal:
                    return _decimal.ToString("#.##", CultureInfo.InvariantCulture);

                default:
                    return value.ToString() ?? "NULL";
            }
        }
    }
}
