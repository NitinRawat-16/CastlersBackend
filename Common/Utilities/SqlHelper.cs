using Microsoft.Data.SqlClient;

namespace castlers.Common.Utilities
{
    public static class SqlHelper
    {

        public static SqlParameter AddNullableParameter(string name,string value)
        {
            if (string.IsNullOrEmpty(value))
                return new SqlParameter(name, DBNull.Value);
            return new SqlParameter(name,value);
        }
        public static SqlParameter AddNullableParameter(string name, DateTime value)
        {
            if (value == null)
                return new SqlParameter(name, DBNull.Value);
            return new SqlParameter(name, value);
        }
        public static SqlParameter AddNullableParameter(string name, bool? value)
        {
            if (value == null)
                return new SqlParameter(name, DBNull.Value);
            return new SqlParameter(name, value);
        }
        public static SqlParameter AddNullableParameter(string name, int? value)
        {
            if (value == null)
                return new SqlParameter(name, DBNull.Value);
            return new SqlParameter(name, value);
        }
        public static SqlParameter AddNullableParameter(string name, decimal? value)
        {
            if (value == null)
                return new SqlParameter(name, DBNull.Value);
            return new SqlParameter(name, value);
        }
        public static SqlParameter AddNullableParameter(string name, float? value)
        {
            if (value == null)
                return new SqlParameter(name, DBNull.Value);
            return new SqlParameter(name, value);
        }
        public static SqlParameter AddNullableParameter(string name, long? value)
        {
            if (value == null)
                return new SqlParameter(name, DBNull.Value);
            return new SqlParameter(name, value);
        }
        public static SqlParameter AddNullableParameter(string name, double? value)
        {
            if (value == null)
                return new SqlParameter(name, DBNull.Value);
            return new SqlParameter(name, value);
        }
        public static SqlParameter AddOutputParameter(string name)
        {
                return new SqlParameter(name, DBNull.Value) { 
                Direction = System.Data.ParameterDirection.Output,
                Size= int.MaxValue
                };
        }
       
    }
}
