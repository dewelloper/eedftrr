using Atlas.Efes.Manager.Base;
using Infragistics.Windows.Ribbon;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Atlas.Efes.Manager.Menu
{
    internal class MenuFactory : MenuComponentBase
    {
        private ObservableCollection<RibbonTabItem> ribbonTabsCollection;

        public ObservableCollection<RibbonTabItem> RibbonTabsCollection
        {
            get { return ribbonTabsCollection; }
            set
            {
                ribbonTabsCollection = value;
                RaisePropertyChanged("RibbonTabsCollection");
            }
        }

        public MenuFactory(ObservableCollection<RibbonTabItem> ribbonTabsCollection)
        {
            this.RibbonTabsCollection = ribbonTabsCollection;
        }

        public void SetSelectedTab(XamRibbon ribbonContainer, Guid viewKey, bool isSelected)
        {
            foreach (ContextualTabGroup item in ribbonContainer.ContextualTabGroups)
            {
                if (item.Key != null)
                {
                    if (Guid.Parse(item.Key.ToString()) == viewKey)
                    {
                        item.Tabs[0].IsSelected = true;
                    }
                }
            }
        }

        private static ButtonTool CreateButton(MenuButton button, RibbonToolSizingMode ribbonToolSizingMode = RibbonToolSizingMode.ImageAndTextLarge)
        {
            ButtonTool buttonTool = new ButtonTool()
            {
                Content = button.Text,
                Command = button.Command,
                ToolTip = button.ToolTip,
                Visibility = button.IsVisible,
                IsEnabled = button.IsEnabled,
                //Tag = button.CurrentTag,
                CommandParameter = button.CommandParameter,
                HorizontalContentAlignment = System.Windows.HorizontalAlignment.Center,
                Name = button.Name
            };

            if (button.Icon != null)
            {
                buttonTool.LargeImage = new BitmapImage(button.Icon);
            }

            buttonTool.SetValue(RibbonGroup.MaximumSizeProperty, ribbonToolSizingMode);
            return buttonTool;
        }

        private RibbonGroup CreateTabGroup(MenuTabGroup group)
        {
            RibbonGroup ribbonGroup = new RibbonGroup
            {
                Caption = group.Text,
            };
            return ribbonGroup;
        }

        private static MenuTool CreateGalleryTool(GalleryItemBox galleryItemBox)
        {
            MenuTool menuTool = new MenuTool()
            {
                Caption = galleryItemBox.Text,
            };

            if (galleryItemBox.Icon != null)
            {
                menuTool.LargeImage = new BitmapImage(galleryItemBox.Icon);
            }

            /*
            GalleryTool galleryTool = new GalleryTool()
            {
                Caption = galleryItemBox.Text,
            };

            if (galleryItemBox.Icon != null)
            {
                galleryTool.LargeImage = new BitmapImage(galleryItemBox.Icon);
            }

            galleryTool.SetValue(RibbonGroup.MaximumSizeProperty, RibbonToolSizingMode.ImageAndTextLarge);
            galleryTool.ItemClicked += galleryItemBox.EventHandler;

            foreach (KeyValuePair<object, string> item in galleryItemBox.Items)
            {
                galleryTool.Items.Add(new GalleryItem()
                {
                    Key = item.Key.ToString(),
                    Text = item.Value,
                });
            }
            */

            menuTool.Items.Add("Patient Notification Form");
            menuTool.Items.Add("Payment Confirmation Form");
            menuTool.Items.Add("Pre Approval Form");
            menuTool.Items.Add("Spesific Medical Form");

            

            return menuTool;
        }


        private ContextualTabGroup CreateTabGroup(MenuContextualTabGroup group)
        {
            ContextualTabGroup ribbonGroup = new ContextualTabGroup
            {
                Caption = group.Text,
            };
            return ribbonGroup;
        }

        private RibbonTabItem CreateTabItem(MenuTabItem tabItem)
        {
            RibbonTabItem ribbonTabItem = new RibbonTabItem()
            {
                Header = tabItem.Text,
                Tag = tabItem.ViewKey,
            };

            //TOD0....

            return ribbonTabItem;
        }

        private RibbonTabItem AddToTabItem(MenuTabItem item)
        {
            //key controlling...

            //if (ribbonTabsCollection != null)
            //{
            //    foreach (var tabCollectionItem in ribbonTabsCollection)
            //    {
            //        /*
            //        if (item.TabKey == tabCollectionItem.Tag.ToString())
            //        {
            //            throw new NotImplementedException("A key already exist");
            //        }

            //         */

            //        //TODOO LATER...
            //        //ribbonTabsCollection[0].IsSelected = true;
            //        break;
            //    }
            //}


            var tabItem = CreateTabItem(item);

            foreach (var groupItem in item.Groups)
            {
                var ribbonTabGroup = CreateTabGroup(groupItem);

                foreach (var buttonItem in groupItem.Buttons)
                {
                    var ribbonTabButton = CreateButton(buttonItem);
                    ribbonTabGroup.Items.Add(ribbonTabButton);
                }

                if (groupItem.GaleryItems != null && groupItem.GaleryItems.Count != 0)
                {
                    foreach (var galeryItem in groupItem.GaleryItems)
                    {
                        var menuTool = CreateGalleryTool(galeryItem);
                        ribbonTabGroup.Items.Add(menuTool);
                    }
                }
            
                tabItem.RibbonGroups.Add(ribbonTabGroup);
            }

            return tabItem;
        }

        public RibbonTabItem AddTabItem(MenuTabItem item)
        {
            return AddToTabItem(item);
        }

        public ContextualTabGroup AddToContextualTabGroup(MenuContextualTabGroup menuTabGroup)
        {
            ContextualTabGroup tabGroup = new ContextualTabGroup();
            tabGroup.Caption = menuTabGroup.Text;
            tabGroup.Key = menuTabGroup.TabKey.ToString();
            tabGroup.Tabs.Add(AddToTabItem(menuTabGroup.TabItems[0]));
            return tabGroup;
        }

        public void RemoveContextualTabGrop(Guid key, XamRibbon ribbonControl)
        {
            foreach (var item in ribbonControl.ContextualTabGroups)
            {
                if (key == Guid.Parse(item.Key.ToString()))
                {
                    ribbonControl.ContextualTabGroups.Remove(item);
                    return;
                }
            }

            throw new NotImplementedException("OperationTab could not found in the ribbon control");
        }

        public void SetButtonEnableProperty(XamRibbon sharedRibbon, Guid viewKey, string buttonName, bool isEnable)
        {
            foreach (var item in sharedRibbon.ContextualTabGroups)
            {
                if (viewKey == Guid.Parse(item.Key.ToString()))
                {
                    var tabItem = item.Tabs[0];
                    var ribbonGroup = tabItem.RibbonGroups[0];

                    foreach (var ribbonTabGroup in tabItem.RibbonGroups)
                    {
                        foreach (var innerButtonItem in ribbonTabGroup.Items)
                        {
                            if (((FrameworkElement)innerButtonItem).Name == buttonName)
                            {
                                ((UIElement)innerButtonItem).IsEnabled = isEnable;
                                return;
                            }
                        }
                    }
                }
            }
        }
    }
}
