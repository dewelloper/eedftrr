using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Atlas.Efes.Portal.Helper
{
    public class SessionHelper
    {
        public static void AddToSession(string key, object value, string pageSessionId = null)
        {
            if (pageSessionId != null)
            {
                key = string.Format("{0}_{1}", key, pageSessionId);
            }

            HttpContext.Current.Session[key] = value;
        }

        public static T GetFromSession<T>(string key, string pageSessionId = null)
        {
            if (pageSessionId != null)
            {
                key = string.Format("{0}_{1}", key, pageSessionId);
            }

            T result = default(T);
            var value = HttpContext.Current.Session[key];
            if (value != null)
            {
                result = (T)value;
            }

            return result;
        }

        public static void ClearSession()
        {
            HttpContext.Current.Session.Abandon();
        }

        public static void ClearSessionByKey(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }
    }
}