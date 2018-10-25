using Atlas.Efes.Manager.Base;
using System;
using System.Collections.ObjectModel;

namespace Atlas.Efes.Manager.Menu
{
    public class MenuTabItem : MenuComponentBase
    {
        private ObservableCollection<MenuTabGroup> groups = new ObservableCollection<MenuTabGroup>();

        public ObservableCollection<MenuTabGroup> Groups
        {
            get { return groups; }
            set
            {
                groups = value;
                RaisePropertyChanged("Groups");
            }
        }

        public string TabKey { get; set; } //if we want remmove the tab we will use this

        public Guid ViewKey { get; set; }
    }
}
