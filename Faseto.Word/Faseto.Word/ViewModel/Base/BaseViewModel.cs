using PropertyChanged;
using System.ComponentModel;

namespace Faseto.Word
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
