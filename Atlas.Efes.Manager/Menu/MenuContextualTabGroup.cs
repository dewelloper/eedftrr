using Atlas.Efes.Manager.Base;
using System;
using System.Collections.ObjectModel;

namespace Atlas.Efes.Manager.Menu
{
    public class MenuContextualTabGroup : MenuComponentBase
    {
        private ObservableCollection<MenuTabItem> tabItems = new ObservableCollection<MenuTabItem>();

        public ObservableCollection<MenuTabItem> TabItems
        {
            get { return tabItems; }
            set
            {
                tabItems = value;
                RaisePropertyChanged("TabItems");
            }
        }

        public Guid TabKey { get; set; }
    }
}
