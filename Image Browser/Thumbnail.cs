using System.ComponentModel;
using System.Runtime.CompilerServices;
using Image_Browser.Annotations;

namespace Image_Browser
{
    public class Thumbnail : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}