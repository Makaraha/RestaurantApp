using System.Data.SqlClient;
using System.Reflection;

namespace Infrastructure.Sql
{
    public class SqlDataMapper
    {
        public SqlDataMapper() { }

        public async Task<List<T>> MapToAsync<T>(SqlDataReader reader)
            where T : new()
        {
            var result = new List<T>();

            var fieldCount = reader.FieldCount;

            while (await reader.ReadAsync())
            {
                var instance = new T();
                for(int i = 0; i < fieldCount; i++)
                {
                    SetField(instance, reader, i);
                }

                result.Add(instance);
            }

            return result;
        }

        private void SetField(object instance, SqlDataReader reader, int i)
        {
            var type = instance.GetType();
            var property = type.GetProperty(reader.GetName(i));

            if (property == null)
                throw new Exception($"Failed to map property {reader.GetName(i)}");

            property.SetValue(instance, ParseProperty(property, reader, i));
        }

        private object ParseProperty(PropertyInfo property, SqlDataReader reader, int i)
        {
            if (property.PropertyType == typeof(int))
                return reader.GetInt32(i);
            else if(property.PropertyType == typeof(float))
                return (float)Math.Round(reader.GetFloat(i), 2);
            else if(property.PropertyType == typeof(decimal))
                return Math.Round(reader.GetDecimal(i), 2);
            else if (property.PropertyType == typeof(string))
                return reader.GetString(i);
            else if (property.PropertyType == typeof(DateTime))
                return reader.GetDateTime(i);
            else
                throw new Exception($"Unknown property type: {property.PropertyType.Name}");
        }
    }
}
