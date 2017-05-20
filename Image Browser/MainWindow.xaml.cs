using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Image_Browser.Annotations;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace Image_Browser
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int _thumbnailSize = 50;
        private ObservableCollection<string> _folderContents = new ObservableCollection<string>();

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

        public ObservableCollection<string> FolderContents
        {
            get { return _folderContents; }
            set
            {
                if (Equals(value, _folderContents)) return;
                _folderContents = value;
                OnPropertyChanged();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowOpenImageDialog(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg, *.jpeg, *.bmp, *.png)|*.jpg;*.jpeg;*.bmp;*.png";
            bool? result = dialog.ShowDialog();
            if (!result.HasValue || !result.Value)
                return;

            ShowImageWindow(dialog.FileName);
        }

        private void ShowOpenFolderDialog(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            DialogResult result = dialog.ShowDialog();
            if (result != System.Windows.Forms.DialogResult.OK)
                return;

            FolderContents.Clear();
            try
            {
                foreach (string filePath in Directory.EnumerateFiles(dialog.SelectedPath).Where(IsFilePathImage))
                    FolderContents.Add(filePath);
            }
            catch (Exception error)
            {
                MessageBox.Show(this, "Error: " + error.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool IsFilePathImage(string filePath)
        {
            Match m = Regex.Match(filePath, @"(\.jpe?g|\.png|\.gif)$");
            return m.Success;
        }

        private void ShowImageWindow(string filePath)
        {
            ImageWindow imageWindow = new ImageWindow(filePath);
            imageWindow.Show();
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
