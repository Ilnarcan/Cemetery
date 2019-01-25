using System.Windows;

namespace Cemetery
{
    /// <summary>
    ///     Логика взаимодействия для Message.xaml
    /// </summary>
    public partial class Message
    {
        public Message(string message)
        {
            InitializeComponent();
            TextBlock1.Text = message;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}