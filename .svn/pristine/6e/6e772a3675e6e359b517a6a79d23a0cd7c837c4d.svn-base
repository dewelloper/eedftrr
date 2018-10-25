using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atlas.Efes.Manager.Helper
{
    public class ResourceHelper
    {
        private static string GetUriString(string resourceNamespace, string value)
        {
            return string.Format("pack://application:,,,/{0};component/{1}", resourceNamespace, value);
        }

        public static Uri GetResource(string value)
        {
            return new Uri(GetUriString("Atlas.Efes.Manager", value));
        }

        public static Uri GetResource(string resourceNamespace, string value)
        {
            return new Uri(GetUriString(resourceNamespace, value));
        }
    }
}
