using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Application = System.Windows.Application;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Cemetery
{
    /// <summary>
    ///     Логика взаимодействия для Nimda.xaml
    /// </summary>
    public partial class Nimda
    {
        public Nimda()
        {
            InitializeComponent();
        }

        public void Message(string message)
        {
            var mess = new Message(message);
            mess.Show();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            BaseFunctions.SaveDataBase();
            Application app = Application.Current;
            app.Shutdown();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int iserror = 0;
            if (String.IsNullOrEmpty(TextBox1.Text))
            {
                TextBox1.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox1.BorderBrush = Brushes.LightGray;

            if (String.IsNullOrEmpty(PasswordBox1.Password))
            {
                PasswordBox1.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                PasswordBox1.BorderBrush = Brushes.LightGray;

            if (iserror == 0)
            {
                string login = TextBox1.Text;

                string pass = CryptDec.CalculateSHA1Hash(PasswordBox1.Password);
                pass = CryptDec.CalculateMd5Hash(pass);
                pass = CryptDec.CalculateSHA512Hash(pass);

                string nimda = CheckBox1.IsChecked == true ? "1" : "0";

                string databaseName = BaseFunctions.Getinput();
                string databaseNamecrypt = BaseFunctions.Getbase();

                if (!(File.Exists(databaseNamecrypt)))
                {
                    Environment.Exit(0);
                }

                CryptDec.DecryptFile(databaseNamecrypt, databaseName, new DESCryptoServiceProvider());

                if (!(File.Exists(databaseName)))
                {
                    Environment.Exit(0);
                }

                var connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));

                connection.Open();
                var command =
                    new SQLiteCommand(
                        "INSERT INTO Users(login,pass,godMode) VALUES('" + login + "','" + pass + "','" + nimda + "')",
                        connection);
                command.ExecuteNonQuery();
                connection.Close();
                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);

                Message("Новый пользователь успешно создан");

                TextBox1.Text = "";
                PasswordBox1.Password = "";

                if (!(File.Exists(databaseNamecrypt)))
                {
                    Environment.Exit(0);
                }

                var dt = new DataTable("Users");

                CryptDec.DecryptFile(databaseNamecrypt, databaseName, new DESCryptoServiceProvider());

                if (!(File.Exists(databaseName)))
                {
                    Environment.Exit(0);
                }

                connection.Open();
                var com = new SQLiteCommand("SELECT ID,login,godMode FROM Users", connection);
                com.ExecuteNonQuery();

                var dataAdp = new SQLiteDataAdapter(com);

                dataAdp.Fill(dt);

                connection.Close();

                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);

                DataGrid1.ItemsSource = dt.DefaultView;
                //DataGrid1.Columns[0].Visibility = Visibility.Hidden;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var rowView = DataGrid1.SelectedValue as DataRowView;
            if (rowView == null)
            {
                Message("Пользователь не выбран");
            }
            else
            {
                int id = Convert.ToInt32(rowView[0].ToString());

                const string message = "Вы действительно ходите удалить данного пользователя?";
                const string caption = "Потверждение удаления";
                const MessageBoxButtons buttons = MessageBoxButtons.YesNo;

                // Displays the MessageBox.

                DialogResult result = MessageBox.Show(message, caption, buttons);

                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    string databaseName = BaseFunctions.Getinput();
                    string databaseNamecrypt = BaseFunctions.Getbase();

                    if (!(File.Exists(databaseNamecrypt)))
                    {
                        Environment.Exit(0);
                    }

                    CryptDec.DecryptFile(databaseNamecrypt, databaseName, new DESCryptoServiceProvider());

                    if (!(File.Exists(databaseName)))
                    {
                        Environment.Exit(0);
                    }


                    var connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));

                    connection.Open();
                    var command = new SQLiteCommand("DELETE FROM Users WHERE ID = '" + id + "'", connection);
                    command.ExecuteNonQuery();
                    connection.Close();
                    CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                    File.Delete(databaseName);

                    Message("Пользователь успешно удален");

                    if (!(File.Exists(databaseNamecrypt)))
                    {
                        Environment.Exit(0);
                    }

                    var dt = new DataTable("Users");

                    CryptDec.DecryptFile(databaseNamecrypt, databaseName, new DESCryptoServiceProvider());

                    if (!(File.Exists(databaseName)))
                    {
                        Environment.Exit(0);
                    }

                    connection.Open();
                    var com = new SQLiteCommand("SELECT ID,login,godMode FROM Users", connection);
                    com.ExecuteNonQuery();

                    var dataAdp = new SQLiteDataAdapter(com);

                    dataAdp.Fill(dt);

                    connection.Close();

                    CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                    File.Delete(databaseName);

                    DataGrid1.ItemsSource = dt.DefaultView;
                    //DataGrid1.Columns[0].Visibility = Visibility.Hidden;
                }
            }
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid1.Visibility = Visibility.Hidden;
            string databaseName = BaseFunctions.Getinput();
            string databaseNamecrypt = BaseFunctions.Getbase();
            var dt = new DataTable("Users");

            if (!(File.Exists(databaseNamecrypt)))
            {
                Environment.Exit(0);
            }

            CryptDec.DecryptFile(databaseNamecrypt, databaseName, new DESCryptoServiceProvider());

            if (!(File.Exists(databaseName)))
            {
                Environment.Exit(0);
            }

            var connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));

            connection.Open();
            var command = new SQLiteCommand("SELECT ID,login,godMode FROM Users", connection);
            command.ExecuteNonQuery();

            var dataAdp = new SQLiteDataAdapter(command);

            dataAdp.Fill(dt);

            connection.Close();

            CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
            File.Delete(databaseName);

            DataGrid1.ItemsSource = dt.DefaultView;
            ////DataGrid1.Columns[0].Visibility = Visibility.Hidden;

            connection.Dispose();
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            string databaseName = BaseFunctions.Getinput();
            string databaseNamecrypt = BaseFunctions.Getbase();
            CryptDec.DecryptFile(databaseNamecrypt, databaseName, new DESCryptoServiceProvider());
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            string databaseName = BaseFunctions.Getinput();
            string databaseNamecrypt = BaseFunctions.Getbase();

            CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
            File.Delete(databaseName);
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (ComboBox6.SelectedIndex != -1)
            {
                string year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
                string month = DateTime.Now.Month.ToString(CultureInfo.InvariantCulture);
                string day = DateTime.Now.Day.ToString(CultureInfo.InvariantCulture);
                const int mode = 0; // mode - Режим: сегодняшний день(0), либо декада или месяц(1), либо период(2)
                string period = "за " + day + "." + month + "." + year;

                if (ComboBox6.SelectedIndex == 0)
                {
                    var doc = new Rezerv(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 1)
                {
                    var doc = new Ozp(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 2)
                {
                    var doc = new ComeSend(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 3)
                {
                    var doc = new Pm(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 4)
                {
                    var doc = new Advance(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 5)
                {
                    var doc = new PaySheet(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 6)
                {
                    var doc = new EditAwardMasters(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 7)
                {
                    var doc = new ComeApp(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 8)
                {
                    var doc = new ComePr(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 9)
                {
                    var doc = new Spending(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 10)
                {
                    var doc = new Szp(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 11)
                {
                    var doc = new RezervIn(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 12)
                {
                    var doc = new CancelOfOffice(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 13)
                {
                    var doc = new CancelOfMaterials(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 14)
                {
                    var doc = new Burials(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 15)
                {
                    var doc = new ListOfBurials(year, month, day, mode, period);
                    doc.Show();
                }
            }
            else
            {
                Message("Выберите тип документа!");
            }
        }

        private void Image_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            if (ComboBox6.SelectedIndex != -1)
            {
                string year = DateTime.Now.Year.ToString(CultureInfo.InvariantCulture);
                string month = DateTime.Now.Month.ToString(CultureInfo.InvariantCulture);
                string day = DateTime.Now.Day.ToString(CultureInfo.InvariantCulture);
                const int mode = 0; // mode - Режим: сегодняшний день(0), либо декада или месяц(1), либо период(2)
                string period = "за " + day + "." + month + "." + year;

                if (ComboBox6.SelectedIndex == 0)
                {
                    var doc = new Rezerv(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 1)
                {
                    var doc = new Ozp(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 2)
                {
                    var doc = new ComeSend(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 3)
                {
                    var doc = new Pm(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 4)
                {
                    var doc = new Advance(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 5)
                {
                    var doc = new PaySheet(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 6)
                {
                    var doc = new EditAwardMasters(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 7)
                {
                    var doc = new ComeApp(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 8)
                {
                    var doc = new ComePr(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 9)
                {
                    var doc = new Spending(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 10)
                {
                    var doc = new Szp(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 11)
                {
                    var doc = new RezervIn(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 12)
                {
                    var doc = new CancelOfOffice(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 13)
                {
                    var doc = new CancelOfMaterials(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 14)
                {
                    var doc = new Burials(year, month, day, mode, period);
                    doc.Show();
                }
                if (ComboBox6.SelectedIndex == 15)
                {
                    var doc = new ListOfBurials(year, month, day, mode, period);
                    doc.Show();
                }
            }
            else
            {
                Message("Выберите тип документа!");
            }
        }

        private void Image_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            if (ComboBox6.SelectedIndex != -1)
            {
                int error = 0;
                string days = "";
                string month = "";
                string year = "";
                const int mode = 1; // mode - Режим: сегодняшний день(0), либо декада или месяц(1), либо период(2)
                string period = "за ";
                if (ComboBox1.SelectedIndex == -1)
                {
                    error = 1;
                    ComboBox1.BorderBrush = Brushes.Red;
                }
                else
                {
                    if (ComboBox1.SelectedIndex == 0)
                    {
                        days = "1,2,3,4,5,6,7,8,9,10";
                        period += "1 декаду ";
                    }
                    if (ComboBox1.SelectedIndex == 1)
                    {
                        days = "11,12,13,14,15,16,17,18,19,20";
                        period += "2 декаду ";
                    }
                    if (ComboBox1.SelectedIndex == 2)
                    {
                        days = "21,22,23,24,25,26,27,28,29,30,31";
                        period += "3 декаду ";
                    }
                }

                if (ComboBox2.SelectedIndex == -1)
                {
                    error = 1;
                    ComboBox2.BorderBrush = Brushes.Red;
                }
                else
                {
                    int memory = ComboBox2.SelectedIndex + 1;
                    month = memory.ToString(CultureInfo.InvariantCulture);
                    switch (month)
                    {
                        case "1":
                            period += " января";
                            break;
                        case "2":
                            period += " февраля";
                            break;
                        case "3":
                            period += " марта";
                            break;
                        case "4":
                            period += " апреля";
                            break;
                        case "5":
                            period += " мая";
                            break;
                        case "6":
                            period += " июня";
                            break;
                        case "7":
                            period += " июля";
                            break;
                        case "8":
                            period += " августа";
                            break;
                        case "9":
                            period += " сентября";
                            break;
                        case "10":
                            period += " октября";
                            break;
                        case "11":
                            period += " ноября";
                            break;
                        case "12":
                            period += " декабря";
                            break;
                    }
                }

                if (ComboBox3.SelectedIndex == -1)
                {
                    error = 1;
                    ComboBox3.BorderBrush = Brushes.Red;
                }
                else
                {
                    year = ComboBox3.SelectionBoxItem.ToString();
                    period += " " + year;
                }

                if (error == 1)
                    Message("Не все поля заполнены!");
                else
                {
                    if (ComboBox6.SelectedIndex == 0)
                    {
                        var doc = new Rezerv(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 1)
                    {
                        var doc = new Ozp(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 2)
                    {
                        var doc = new ComeSend(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 3)
                    {
                        var doc = new Pm(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 4)
                    {
                        var doc = new Advance(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 5)
                    {
                        var doc = new PaySheet(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 6)
                    {
                        var doc = new EditAwardMasters(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 7)
                    {
                        var doc = new ComeApp(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 8)
                    {
                        var doc = new ComePr(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 9)
                    {
                        var doc = new Spending(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 10)
                    {
                        var doc = new Szp(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 11)
                    {
                        var doc = new RezervIn(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 12)
                    {
                        var doc = new CancelOfOffice(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 13)
                    {
                        var doc = new CancelOfMaterials(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 14)
                    {
                        var doc = new Burials(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 15)
                    {
                        var doc = new ListOfBurials(year, month, days, mode, period);
                        doc.Show();
                    }
                }
            }
            else
            {
                Message("Выберите тип документа!");
            }
        }

        private void Image_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            if (ComboBox6.SelectedIndex != -1)
            {
                int error = 0;
                const string days =
                    "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31";
                string month = "";
                string year = "";
                const int mode = 2; // mode - Режим: сегодняшний день(0), либо декада или месяц(1), либо период(2)
                string period = "за";

                if (ComboBox4.SelectedIndex == -1)
                {
                    error = 1;
                    ComboBox4.BorderBrush = Brushes.Red;
                }
                else
                {
                    int memory = ComboBox4.SelectedIndex + 1;
                    month = memory.ToString(CultureInfo.InvariantCulture);
                    switch (month)
                    {
                        case "1":
                            period += " январь";
                            break;
                        case "2":
                            period += " февраль";
                            break;
                        case "3":
                            period += " март";
                            break;
                        case "4":
                            period += " апрель";
                            break;
                        case "5":
                            period += " май";
                            break;
                        case "6":
                            period += " июнь";
                            break;
                        case "7":
                            period += " июль";
                            break;
                        case "8":
                            period += " август";
                            break;
                        case "9":
                            period += " сентябрь";
                            break;
                        case "10":
                            period += " октябрь";
                            break;
                        case "11":
                            period += " ноябрь";
                            break;
                        case "12":
                            period += " декабрь";
                            break;
                    }
                }

                if (ComboBox5.SelectedIndex == -1)
                {
                    error = 1;
                    ComboBox5.BorderBrush = Brushes.Red;
                }
                else
                {
                    year = ComboBox5.SelectionBoxItem.ToString();
                    period += " " + year;
                }

                if (error == 1)
                    Message("Не все поля заполнены!");
                else
                {
                    if (ComboBox6.SelectedIndex == 0)
                    {
                        var doc = new Rezerv(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 1)
                    {
                        var doc = new Ozp(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 2)
                    {
                        var doc = new ComeSend(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 3)
                    {
                        var doc = new Pm(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 4)
                    {
                        var doc = new Advance(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 5)
                    {
                        var doc = new PaySheet(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 6)
                    {
                        var doc = new EditAwardMasters(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 7)
                    {
                        var doc = new ComeApp(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 8)
                    {
                        var doc = new ComePr(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 9)
                    {
                        var doc = new Spending(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 10)
                    {
                        var doc = new Szp(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 11)
                    {
                        var doc = new RezervIn(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 12)
                    {
                        var doc = new CancelOfOffice(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 13)
                    {
                        var doc = new CancelOfMaterials(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 14)
                    {
                        var doc = new Burials(year, month, days, mode, period);
                        doc.Show();
                    }
                    if (ComboBox6.SelectedIndex == 15)
                    {
                        var doc = new ListOfBurials(year, month, days, mode, period);
                        doc.Show();
                    }
                }
            }
            else
            {
                Message("Выберите тип документа!");
            }
        }

        private void Image_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            if (ComboBox6.SelectedIndex != -1)
            {
                string days = "";
                string months = "";
                string year = "";
                const int mode = 3; // mode - Режим: сегодняшний день(0), либо декада или месяц(1), либо период(2)
                string period = "за период с ";

                if (DatePicker1.Text == "" || DatePicker2.Text == "")
                    Message("Заполните даты!");
                else
                {
                    var date1 = Convert.ToDateTime(DatePicker1.ToString());
                    var date2 = Convert.ToDateTime(DatePicker2.ToString());
                    int error = DateTime.Compare(date2, date1);

                    if (error == -1) 


                        Message("Начальная дата больше конечной!");
                    else
                    {
                        string[] memory = DatePicker1.ToString().Split('.');
                        string[] memory2 = memory[2].Split(' ');
                        int dayStart = Convert.ToInt32(memory[0]);
                        int monthStart = Convert.ToInt32(memory[1]);
                        int yearStart = Convert.ToInt32(memory2[0]);

                        memory = DatePicker2.ToString().Split('.');
                        memory2 = memory[2].Split(' ');
                        int dayFinish = Convert.ToInt32(memory[0]);
                        int monthFinish = Convert.ToInt32(memory[1]);
                        int yearFinish = Convert.ToInt32(memory2[0]);

                        period += dayStart + "." + monthStart + "." + yearStart + " по " + dayFinish + "." + monthFinish +
                                  "." + yearFinish;

                        if (yearStart == yearFinish)
                            year = "" + yearStart;
                        else
                        {
                            for (int i = yearStart; i <= yearFinish; i++)
                            {
                                if (i == yearStart)
                                    year += yearStart + ",";
                                if (i != yearStart && i != yearFinish)
                                    year += i + ",";
                                if (i == yearFinish)
                                    year += yearFinish;
                            }
                        }

                        if (monthStart == monthFinish && yearStart == yearFinish)
                        {
                            for (int i = dayStart; i <= dayFinish; i++)
                            {
                                if (i == dayStart)
                                    days += dayStart + ",";
                                if (i != dayStart && i != dayFinish)
                                    days += i + ",";
                                if (i == dayFinish)
                                    days += dayFinish;
                            }
                        }
                        else
                        {
                            if (dayStart > dayFinish)
                            {
                                for (int i = dayStart; i <= 31; i++)
                                    days += i + ",";

                                for (int i = 0; i <= dayFinish; i++)
                                {
                                    if (i != dayFinish)
                                        days += i + ",";
                                    if (i == dayFinish)
                                        days += dayFinish;
                                }
                            }
                            else
                            {
                                days = "1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31";
                            }
                        }

                        if (yearStart == yearFinish)
                        {
                            for (int i = monthStart; i <= monthFinish; i++)
                            {
                                if (i == monthStart)
                                    months += monthStart + ",";
                                if (i != monthStart && i != monthFinish)
                                    months += i + ",";
                                if (i == monthFinish)
                                    months += monthFinish;
                            }
                        }
                        else
                        {
                            months = "1,2,3,4,5,6,7,8,9,10,11,12";
                        }

                        if (ComboBox6.SelectedIndex == 0)
                        {
                            var doc = new Rezerv(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 1)
                        {
                            var doc = new Ozp(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 2)
                        {
                            var doc = new ComeSend(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 3)
                        {
                            var doc = new Pm(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 4)
                        {
                            var doc = new Advance(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 5)
                        {
                            var doc = new PaySheet(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 6)
                        {
                            var doc = new EditAwardMasters(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 7)
                        {
                            var doc = new ComeApp(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 8)
                        {
                            var doc = new ComePr(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 9)
                        {
                            var doc = new Spending(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 10)
                        {
                            var doc = new Szp(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 11)
                        {
                            var doc = new RezervIn(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 12)
                        {
                            var doc = new CancelOfOffice(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 13)
                        {
                            var doc = new CancelOfMaterials(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 14)
                        {
                            var doc = new Burials(year, months, days, mode, period);
                            doc.Show();
                        }
                        if (ComboBox6.SelectedIndex == 15)
                        {
                            var doc = new ListOfBurials(year, months, days, mode, period);
                            doc.Show();
                        }
                    }
                }
            }
            else
            {
                Message("Выберите тип документа!");
            }
        }

        private void ComboBox6_DropDownClosed(object sender, EventArgs e)
        {
            TextBlock1.Text = ComboBox6.Text;
        }

        private void Image_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            Grid1.Visibility = Visibility.Visible;
        }
    }
}