using Atlas.Efes.Manager.Base;
using Microsoft.Practices.Prism.Commands;
using System;
using System.Windows;

namespace Atlas.Efes.Manager.Menu
{
    public class MenuButton : MenuComponentBase
    {
        public Uri Icon { get; set; }

        public string IconKey { get; set; }

        public Visibility IsVisible { get; set; }

        public string Name { get; set; }

        public object CommandParameter { get; set; }

        //public SimpleCommand<object, object> Command { get; set; }

        public DelegateCommand Command { get; set; }

        public bool IsEnabled = true;
    }
}
