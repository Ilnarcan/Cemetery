using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Input;

namespace Cemetery
{
    /// <summary>
    ///     Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth
    {
        private int _keyi;

        public Auth()
        {
            InitializeComponent();
        }

        public void Message(string message)
        {
            var mess = new Message(message);
            mess.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TextBox1.Focus();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string login = TextBox1.Text;

            if (!String.IsNullOrEmpty(TextBox1.Text) && !String.IsNullOrEmpty(PasswordBox1.Password))
            {
                string pass = CryptDec.CalculateSHA1Hash(PasswordBox1.Password);
                pass = CryptDec.CalculateMd5Hash(pass);
                pass = CryptDec.CalculateSHA512Hash(pass);

                var dt = new DataTable();

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
                var command = new SQLiteCommand("SELECT ID,pass,godMode FROM Users WHERE login = '" + login + "'",
                    connection);

                dt.Load(command.ExecuteReader());

                connection.Close();

                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);

                foreach (DataRow record in dt.Rows)
                {
                    var date = (string) record["pass"];
                    var godMode = (string) record["godMode"];
                    if (date == pass)
                    {
                        if (godMode == "1" && _keyi == 3)
                        {
                            var page = new EasterEgg();
                            page.Show();
                            Close();
                        }
                        if (godMode == "0" || (godMode == "1" && _keyi != 3))
                        {
                            var form = new MainWindow();
                            form.Show();
                            Close();
                        }
                    }
                    else
                    {
                        TextBox1.Text = "";
                        PasswordBox1.Password = "";
                    }
                }
                TextBox1.Text = "";
                PasswordBox1.Password = "";
            }
            else
            {
                TextBox1.Text = "";
                PasswordBox1.Password = "";
            }
        }


        private void passwordBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return")
            {
                string login = TextBox1.Text;

                if (!String.IsNullOrEmpty(TextBox1.Text) && !String.IsNullOrEmpty(PasswordBox1.Password))
                {
                    string pass = CryptDec.CalculateSHA1Hash(PasswordBox1.Password);
                    pass = CryptDec.CalculateMd5Hash(pass);
                    pass = CryptDec.CalculateSHA512Hash(pass);

                    var dt = new DataTable();

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
                    var command = new SQLiteCommand("SELECT ID,pass,godMode FROM Users WHERE login = '" + login + "'",
                        connection);

                    dt.Load(command.ExecuteReader());

                    connection.Close();

                    CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                    File.Delete(databaseName);

                    foreach (DataRow record in dt.Rows)
                    {
                        var date = (string) record["pass"];
                        var godMode = (string) record["godMode"];
                        if (date == pass)
                        {
                            if (godMode == "1" && _keyi == 3)
                            {
                                var page = new Nimda();
                                page.Show();
                                Close();
                            }
                            if (godMode == "0" || (godMode == "1" && _keyi != 3))
                            {
                                var form = new MainWindow();
                                form.Show();
                                Close();
                            }
                        }
                        else
                        {
                            TextBox1.Text = "";
                            PasswordBox1.Password = "";
                        }
                    }
                    TextBox1.Text = "";
                    PasswordBox1.Password = "";
                }
                else
                {
                    TextBox1.Text = "";
                    PasswordBox1.Password = "";
                }
            }
        }

        private void button2_Click_1(object sender, RoutedEventArgs e)
        {
            string databaseName = BaseFunctions.Getinput();
            string databaseNamecrypt = BaseFunctions.Getbase();

            CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
            File.Delete(databaseName);
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            string databaseName = BaseFunctions.Getinput();
            string databaseNamecrypt = BaseFunctions.Getbase();
            CryptDec.DecryptFile(databaseNamecrypt, databaseName, new DESCryptoServiceProvider());
        }

        private void label1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _keyi += 1;
        }
    }
}