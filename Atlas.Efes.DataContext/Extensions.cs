using Atlas.Efes.OracleClient.OracleServiceProxy;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Atlas.Efes.DataContext
{
    public static class Extensions
    {
        public static void AddOracleParameters(this List<Params> parameters, string key, object value)
        {
            if (parameters == null)
            {
                parameters = new List<Params>();
            }

            Params param = new Params();
            param.ParamName = key;
            param.ParamVal = value;

            parameters.Add(param);
        }


        public static T Get<T>(this DataRow dr, string columnName)
        {
            try
            {
                if (dr[columnName] == DBNull.Value)
                {
                    return default(T);
                }
                Type type = typeof(T);
                if (type == typeof(DateTime))
                {
                    throw new ArgumentException("Date time not supported.");
                }
                else if (type == typeof(Guid))
                {
                    return (T)(object)new Guid(dr[columnName].ToString());
                }
                else if (type.IsEnum)
                {
                    return (T)Enum.Parse(type, dr[columnName].ToString());
                }

                return (T)dr[columnName];
            }
            catch (InvalidCastException ex)
            {
                throw new InvalidCastException("Specified cast is not valid, field: " + columnName + ", Type: " + typeof(T).FullName, ex);
            }
        }

        public static T GetNullable<T>(this DataRow dr, string columnName)
        {
            object value = dr[columnName];
            if (value == DBNull.Value)
            {
                return default(T);
            }
            return Get<T>(dr, columnName);
        }
    }
}
