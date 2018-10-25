using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.Efes.Common.GibModel.UserProcess
{
    public class UserAccount
    {
        public string UserID { get; set; }

        public PersonName PersonName { get; set; }

        public UserRole UserRole { get; set; }

        
        public AuthorizedWorkScope AuthorizedWorkScope { get; set; }

        public AccountConfiguration AccountConfiguration { get; set; }
    }
}
