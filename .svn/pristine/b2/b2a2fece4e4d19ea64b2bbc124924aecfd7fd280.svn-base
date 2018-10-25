using Atlas.Efes.Manager.Base;
using System.Collections.ObjectModel;

namespace Atlas.Efes.Manager.Menu
{
    public class MenuTabGroup : MenuComponentBase
    {
        private ObservableCollection<MenuButton> buttons = new ObservableCollection<MenuButton>();

        public ObservableCollection<MenuButton> Buttons
        {
            get { return buttons; }
            set
            {
                buttons = value;
                RaisePropertyChanged("Buttons");
            }
        }

        private ObservableCollection<GalleryItemBox> galeryItems = new ObservableCollection<GalleryItemBox>();

        public ObservableCollection<GalleryItemBox> GaleryItems
        {
            get { return galeryItems; }
            set
            {
                galeryItems = value;
                RaisePropertyChanged("GaleryItems");
            }
        }


    }
}
