using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Atlas.Efes.Common.GIB
{
    public class AccountingCustomerPartyInfo : BasePropertyChanged
    {

        private PartyInfo party;

        public PartyInfo Party
        {
            get { return party; }
            set
            {
                party = value;
                RaisePropertyChanged("Party");
            }
        }

    }
}
