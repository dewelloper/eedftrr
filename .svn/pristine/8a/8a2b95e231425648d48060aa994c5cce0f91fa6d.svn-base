using System.ComponentModel;

namespace Atlas.Efes.Manager.Base
{
    public abstract class MenuComponentBase : INotifyPropertyChanged
    {
        public string Text { get; set; }

        public string ToolTip { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
            {
                return;
            }

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
