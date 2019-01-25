using System.Windows.Input;

namespace Cemetery
{
    /// <summary>
    /// Логика взаимодействия для EasterEgg.xaml
    /// </summary>
    public partial class EasterEgg
    {
        private int _key;
        public EasterEgg()
        {
            InitializeComponent();
        }

        private void image2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _key = -1000;
        }

        private void image3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _key = -1000;
        }

        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _key++;
            if (_key == 5)
            {
                var page = new Nimda();
                page.Show();
                Close();
            }
        }
    }
}
