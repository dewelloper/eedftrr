﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.Efes.Common.Model
{
    public class UserInfo
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public CustomerInfo CustomerInfo { get; set; }
        public string ApiKey { get; set; }
    }
}
