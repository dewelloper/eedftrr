using Infragistics.Windows.Ribbon;
using System;
using System.Collections.ObjectModel;

namespace Atlas.Efes.Manager.Menu
{
    public class ScreenManager
    {
        public static void AddTabItemToRibbonMenu(XamRibbon sharedRibbon, ObservableCollection<MenuTabItem> menuTabCollection)
        {
            MenuFactory menuFactory = CreateMenuFactory(sharedRibbon);

            foreach (MenuTabItem tabItem in menuTabCollection)
            {
                sharedRibbon.Tabs.Add(menuFactory.AddTabItem(tabItem));
            }
        }

        public static void AddOperationItemToTab(XamRibbon sharedRibbon, MenuContextualTabGroup contextualTabGroup)
        {
            MenuFactory menuFactory = CreateMenuFactory(sharedRibbon);

            ContextualTabGroup ribbonContextualTab = menuFactory.AddToContextualTabGroup(contextualTabGroup);

            sharedRibbon.ContextualTabGroups.Add(ribbonContextualTab);
        }

        public static void RemoveOperationTab(Guid tabKey,XamRibbon sharedRibbon)
        {
            MenuFactory factory = CreateMenuFactory(sharedRibbon);

            factory.RemoveContextualTabGrop(tabKey, sharedRibbon);
        }

        private static MenuFactory CreateMenuFactory(XamRibbon hostedRibbon)
        {
            if (hostedRibbon == null)
            {
                throw new NotImplementedException("MasterWindow has not a ribbon control....");
            }

            MenuFactory menuFactory = new MenuFactory(hostedRibbon.Tabs);

            return menuFactory;
        }

        public static void SetButtonEnableProperty(XamRibbon sharedRibbon,Guid viewKey,string buttonName,bool isEnable)
        {
            MenuFactory factory = CreateMenuFactory(sharedRibbon);
            factory.SetButtonEnableProperty(sharedRibbon, viewKey, buttonName, isEnable);
        }

        public static void SetSelectedTab(XamRibbon ribbonContainer, Guid viewKey,bool isSelected)
        {
            MenuFactory factory = CreateMenuFactory(ribbonContainer);

            factory.SetSelectedTab(ribbonContainer, viewKey, isSelected);
        }
    }
}
