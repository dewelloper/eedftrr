using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;

namespace Atlas.Efes.Portal.Helper
{
    public class CacheFabricHelper
    {
        public static void Add(string cacheKey, object value)
        {
            HttpContext.Current.Cache.Add(cacheKey, value, null, DateTime.Now.AddDays(1), Cache.NoSlidingExpiration, CacheItemPriority.AboveNormal, null);
        }

        public static void Add(string cacheKey, object value, DateTime date)
        {
            HttpContext.Current.Cache.Add(cacheKey, value, null, date, Cache.NoSlidingExpiration, CacheItemPriority.AboveNormal, null);
        }


        public static T Get<T>(string cacheKey)
        {
            T instance = default(T);

            object value = HttpContext.Current.Cache[cacheKey];

            if (value != null)
            {
                instance = (T)value;
            }

            return instance;
        }
    }
}