using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Image_Browser.Annotations;

namespace Image_Browser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int _thumbnailSize = 50;

        public int ThumbnailSize
        {
            get { return _thumbnailSize; }
            set
            {
                if (value == _thumbnailSize) return;
                _thumbnailSize = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OpenImage(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void OpenFolder(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void DisplayAbout(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Simple image browser", "About", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
