using System.Windows;
using System.Windows.Controls;

namespace MsToDo.Controls
{
    /// <summary>
    /// Interaction logic for AddTaskInputField.xaml
    /// </summary>
    public partial class AddTaskTextBox : TextBox
    {
        public AddTaskTextBox()
        {
            GotFocus += GotFocus_RoutedEventHandler;
            LostFocus += LostFocus_RoutedEventHandler;
            Loaded += Loaded_RoutedEventHandler;

            InitializeComponent();
        }

        public string DeafultText { get; set; }

        public CornerRadius CornerRadius { get; set; }

        private void GotFocus_RoutedEventHandler(object sender, RoutedEventArgs e)
        {
            if (Text == DeafultText)
            {
                Text = string.Empty;
            }
        }

        private void LostFocus_RoutedEventHandler(object sender, RoutedEventArgs e)
        {
            if (Text == string.Empty)
            {
                Text = DeafultText;
            }
        }

        private void Loaded_RoutedEventHandler(object sender, RoutedEventArgs e)
        {
            Text = DeafultText;
        }
    }
}
