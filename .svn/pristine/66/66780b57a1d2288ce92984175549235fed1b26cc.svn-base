using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Atlas.Efes.Portal.Helper;
using Atlas.Efes.Common.Model;
using Atlas.Efes.Portal.Constants;
namespace Atlas.Efes.Portal
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {

        public string Username { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                UserInfo userInfo = SessionHelper.GetFromSession<UserInfo>(LifeTimeKeys.User_Information);
                Username = userInfo.Username;
            }
        }
    }
}