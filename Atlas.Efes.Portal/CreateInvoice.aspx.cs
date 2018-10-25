using Atlas.Efes.Common.Model;
using Atlas.Efes.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Atlas.Efes.Portal
{
    public partial class CreateInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ettn = Request.QueryString["ETTN"];
            
            if (ettn != null)
            {
                ETTN = ettn.ToString();
            }
            else
            {
                ETTN = Guid.NewGuid().ToString();
            }
        }


        private string ettn;
        public string ETTN
        {
            get { return ettn; }
            set { ettn = value; }
        }
        
        public string InvoiceNumber
        {
            get
            {
                return "GIB" + DateTime.Now.ToString("yyyy.mm.dd.hh").Replace(".", "");
            }
        }
    }
}