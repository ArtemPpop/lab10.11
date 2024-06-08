using System.Reflection.Emit;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab10._11
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            mainFrame.Navigate(new Lab10());

        }
        public static class CustomCommands
        {
            public static readonly RoutedUICommand CopyFile = new RoutedUICommand(
                "CopyFile", "CopyFile", typeof(CustomCommands),
                new InputGestureCollection() { new KeyGesture(Key.C, ModifierKeys.Control) }
            );
        }

        private void btLab10_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Lab10());
        }

        private void btLab11_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(new Lab11());
        }
    }
}
