using PropertyChanged;
using System.ComponentModel;

namespace WpfTreeView
{
    [ImplementPropertyChanged]
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => {};
        
        public void OnPropertyChanged(string _name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(_name));
        }
    }
}
