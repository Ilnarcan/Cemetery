using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.Forms.MessageBox;

namespace Cemetery
{
    /// <summary>
    ///     Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit
    {
        private readonly int _id;

        public Edit(int index)
        {
            _id = index;
            InitializeComponent();
        }

        public void Message(string message)
        {
            var mess = new Message(message);
            mess.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
            var command = new SQLiteCommand("SELECT secondName,name, thirdName, age, dateOfDeath, dateOfBurials, seriesOfDocument, numberOfDocument, issuedBy, telephone, sector, raw, bed, urn FROM Clients WHERE ID = '" + _id + "'", connection);

            dt.Load(command.ExecuteReader());

            connection.Close();

            CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
            File.Delete(databaseName);

            foreach (DataRow record in dt.Rows)
            {
                TextBox1.Text = record["secondName"].ToString();
                TextBox2.Text = record["name"].ToString();
                TextBox3.Text = record["thirdName"].ToString();
                TextBox4.Text = record["age"].ToString();

                string dateOfDeath = record["dateOfDeath"].ToString();
                string[] massdateOfDeath = dateOfDeath.Split('.');

                if (massdateOfDeath.Length == 3)
                {
                    TextBox7.Text = massdateOfDeath[0];
                    TextBox8.Text = massdateOfDeath[1];
                    TextBox9.Text = massdateOfDeath[2];
                }


                string dateOfBurials = record["dateOfBurials"].ToString();
                string[] massdateOfBurials = dateOfBurials.Split('.');

                if (massdateOfBurials.Length == 3)
                {
                    TextBox10.Text = massdateOfBurials[0];
                    TextBox11.Text = massdateOfBurials[1];
                    TextBox12.Text = massdateOfBurials[2];
                }

                TextBox13.Text = record["telephone"].ToString();
                TextBox14.Text = record["seriesOfDocument"].ToString();
                TextBox15.Text = record["numberOfDocument"].ToString();
                TextBox16.Text = record["issuedBy"].ToString();
                TextBox17.Text = record["sector"].ToString();
                TextBox18.Text = record["raw"].ToString();
                TextBox19.Text = record["bed"].ToString();

                CheckBox1.IsChecked = record["urn"].ToString() == "урна" || record["urn"].ToString() == "УРНА";
                //CheckBox1.IsChecked = record["urn"].ToString() == "УРНА";
            }
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

            if (String.IsNullOrEmpty(TextBox2.Text))
            {
                TextBox2.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox2.BorderBrush = Brushes.LightGray;

            if (String.IsNullOrEmpty(TextBox3.Text))
            {
                TextBox3.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox3.BorderBrush = Brushes.LightGray;

            if (String.IsNullOrEmpty(TextBox7.Text))
            {
                TextBox7.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox7.BorderBrush = Brushes.LightGray;

            if (String.IsNullOrEmpty(TextBox8.Text))
            {
                TextBox8.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox8.BorderBrush = Brushes.LightGray;

            if (String.IsNullOrEmpty(TextBox9.Text))
            {
                TextBox9.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox9.BorderBrush = Brushes.LightGray;

            if (String.IsNullOrEmpty(TextBox10.Text))
            {
                TextBox10.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox10.BorderBrush = Brushes.LightGray;

            if (String.IsNullOrEmpty(TextBox11.Text))
            {
                TextBox11.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox11.BorderBrush = Brushes.LightGray;

            if (String.IsNullOrEmpty(TextBox12.Text))
            {
                TextBox12.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox12.BorderBrush = Brushes.LightGray;

            if (String.IsNullOrEmpty(TextBox14.Text))
            {
                TextBox14.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox14.BorderBrush = Brushes.LightGray;

            if (String.IsNullOrEmpty(TextBox15.Text))
            {
                TextBox15.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox15.BorderBrush = Brushes.LightGray;

            if (String.IsNullOrEmpty(TextBox16.Text))
            {
                TextBox16.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox16.BorderBrush = Brushes.LightGray;

            if (String.IsNullOrEmpty(TextBox17.Text))
            {
                TextBox17.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox17.BorderBrush = Brushes.LightGray;

            if (String.IsNullOrEmpty(TextBox18.Text))
            {
                TextBox18.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox18.BorderBrush = Brushes.LightGray;

            if (String.IsNullOrEmpty(TextBox19.Text))
            {
                TextBox19.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox19.BorderBrush = Brushes.LightGray;

            if (iserror == 0)
            {
                string secondName = TextBox1.Text;
                string name = TextBox2.Text;
                string thirdName = TextBox3.Text;
                string age = TextBox4.Text;

                string dateOfDeath = "";
                if (TextBox7.Text.Length == 1)
                    dateOfDeath += "0" + TextBox7.Text;
                else
                    dateOfDeath += TextBox7.Text;
                if (TextBox8.Text.Length == 1)
                    dateOfDeath += ".0" + TextBox8.Text;
                else
                    dateOfDeath += "." + TextBox8.Text;

                dateOfDeath += "." + TextBox9.Text;

                string dateOfBurials = "";
                if (TextBox10.Text.Length == 1)
                    dateOfBurials += "0" + TextBox10.Text;
                else
                    dateOfBurials += TextBox10.Text;
                if (TextBox11.Text.Length == 1)
                    dateOfBurials += ".0" + TextBox11.Text;
                else
                    dateOfBurials += "." + TextBox11.Text;

                dateOfBurials += "." + TextBox12.Text;
                string seriesOfDocument = TextBox14.Text;
                string numberOfDocument = TextBox15.Text;
                string issuedBy = TextBox16.Text;
                string telephone = TextBox13.Text;
                string sector = TextBox17.Text;
                string raw = TextBox18.Text;
                string bed = TextBox19.Text;
                string urn = "";

                if (CheckBox1.IsChecked == true)
                    urn = "урна";

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
                        "UPDATE Clients SET secondName = '" + secondName + "',name = '" + name + "',thirdName = '" +
                        thirdName + "',age = '" + age + "',dateOfDeath = '" + dateOfDeath + "',dateOfBurials = '" +
                        dateOfBurials + "',seriesOfDocument = '" + seriesOfDocument + "',numberOfDocument = '" +
                        numberOfDocument + "',issuedBy = '" + issuedBy + "',telephone = '" + telephone + "',sector = '" +
                        sector + "',raw = '" + raw + "',bed = '" + bed + "',urn = '" + urn + "' WHERE ID = '" + _id +
                        "'", connection);

                command.ExecuteNonQuery();
                connection.Close();
                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);

                Message("Изменения успешно сохранены");
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            const string message = "Вы действительно ходите удалить карточку покойного из базы?";
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
                var command = new SQLiteCommand("DELETE FROM Clients WHERE ID = '" + _id + "'", connection);
                command.ExecuteNonQuery();
                connection.Close();
                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);

                Message("Карточка покойного успешно удалена");

                Close();
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A || e.Key == Key.B || e.Key == Key.C || e.Key == Key.D || e.Key == Key.E || e.Key == Key.F ||
                e.Key == Key.G || e.Key == Key.H || e.Key == Key.I || e.Key == Key.K || e.Key == Key.L || e.Key == Key.M ||
                e.Key == Key.N || e.Key == Key.O || e.Key == Key.P || e.Key == Key.Q || e.Key == Key.R || e.Key == Key.S ||
                e.Key == Key.T || e.Key == Key.V || e.Key == Key.X || e.Key == Key.Y || e.Key == Key.Z ||
                e.Key == Key.Tab)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox1.Text = TextBox1.Text.Trim();
            TextBox1.SelectionStart = TextBox1.Text.Length;
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A || e.Key == Key.B || e.Key == Key.C || e.Key == Key.D || e.Key == Key.E || e.Key == Key.F ||
                e.Key == Key.G || e.Key == Key.H || e.Key == Key.I || e.Key == Key.K || e.Key == Key.L || e.Key == Key.M ||
                e.Key == Key.N || e.Key == Key.O || e.Key == Key.P || e.Key == Key.Q || e.Key == Key.R || e.Key == Key.S ||
                e.Key == Key.T || e.Key == Key.V || e.Key == Key.X || e.Key == Key.Y || e.Key == Key.Z ||
                e.Key == Key.Tab)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox2_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox2.Text = TextBox2.Text.Trim();
            TextBox2.SelectionStart = TextBox2.Text.Length;
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A || e.Key == Key.B || e.Key == Key.C || e.Key == Key.D || e.Key == Key.E || e.Key == Key.F ||
                e.Key == Key.G || e.Key == Key.H || e.Key == Key.I || e.Key == Key.K || e.Key == Key.L || e.Key == Key.M ||
                e.Key == Key.N || e.Key == Key.O || e.Key == Key.P || e.Key == Key.Q || e.Key == Key.R || e.Key == Key.S ||
                e.Key == Key.T || e.Key == Key.V || e.Key == Key.X || e.Key == Key.Y || e.Key == Key.Z ||
                e.Key == Key.Tab)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox3_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox3.Text = TextBox3.Text.Trim();
            TextBox3.SelectionStart = TextBox3.Text.Length;
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox4.CaretIndex == 2)
            {
                if (Convert.ToInt32(TextBox4.Text) < 20)
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                        e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                        e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                        e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                        e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                else
                    e.Handled = true;
            }
            else
            {
                if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                    e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                    e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                    e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                    e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void textBox4_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox4.Text = TextBox4.Text.Trim();
            TextBox4.SelectionStart = TextBox4.Text.Length;
        }

        private void textBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox7.CaretIndex == 0)
            {
                if (e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 ||
                    e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 ||
                    e.Key == Key.NumPad9 || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 ||
                    e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            if (TextBox7.CaretIndex == 1)
            {
                if (TextBox7.Text == "1" || TextBox7.Text == "2")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                        e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                        e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                        e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                        e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                if (TextBox7.Text == "3")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.D0 || e.Key == Key.D1 ||
                        e.Key == Key.Tab)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                if (TextBox7.Text == "4" || TextBox7.Text == "5" || TextBox7.Text == "6" || TextBox7.Text == "7" ||
                    TextBox7.Text == "8" || TextBox7.Text == "9")
                {
                    e.Handled = e.Key != Key.Tab;
                }
            }
        }

        private void textBox7_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox7.Text = TextBox7.Text.Trim();
            TextBox7.SelectionStart = TextBox7.Text.Length;
        }

        private void textBox8_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox8.CaretIndex == 0)
            {
                if (e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 ||
                    e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 ||
                    e.Key == Key.NumPad9 || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 ||
                    e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            if (TextBox8.CaretIndex == 1)
            {
                if (TextBox8.Text != "1")
                {
                    e.Handled = e.Key != Key.Tab;
                }
                if (TextBox8.Text == "1")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.D0 ||
                        e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.Tab)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
        }

        private void textBox8_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox8.Text = TextBox8.Text.Trim();
            TextBox8.SelectionStart = TextBox8.Text.Length;
        }

        private void textBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox9.CaretIndex == 0)
            {
                if (e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.D1 || e.Key == Key.D2)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            if (TextBox9.CaretIndex == 1)
            {
                if (TextBox9.Text == "1")
                {
                    if (e.Key == Key.NumPad9 || e.Key == Key.D9)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                if (TextBox9.Text == "2")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.D0 ||
                        e.Key == Key.D1 || e.Key == Key.D2)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
            if (TextBox9.CaretIndex == 2)
            {
                if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                    e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                    e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                    e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                    e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            if (TextBox9.CaretIndex == 3)
            {
                if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                    e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                    e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                    e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                    e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
        }

        private void textBox9_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox9.Text = TextBox9.Text.Trim();
            TextBox9.SelectionStart = TextBox9.Text.Length;
        }

        private void textBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox10.CaretIndex == 0)
            {
                if (e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 ||
                    e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 ||
                    e.Key == Key.NumPad9 || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 ||
                    e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            if (TextBox10.CaretIndex == 1)
            {
                if (TextBox10.Text == "1" || TextBox10.Text == "2")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                        e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                        e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                        e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                        e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                if (TextBox10.Text == "3")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.D0 || e.Key == Key.D1 ||
                        e.Key == Key.Tab)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                if (TextBox10.Text == "4" || TextBox10.Text == "5" || TextBox10.Text == "6" || TextBox10.Text == "7" ||
                    TextBox10.Text == "8" || TextBox10.Text == "9")
                {
                    e.Handled = e.Key != Key.Tab;
                }
            }
        }

        private void textBox10_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox10.Text = TextBox10.Text.Trim();
            TextBox10.SelectionStart = TextBox10.Text.Length;
        }

        private void textBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox11.CaretIndex == 0)
            {
                if (e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 || e.Key == Key.NumPad4 ||
                    e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 || e.Key == Key.NumPad8 ||
                    e.Key == Key.NumPad9 || e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 ||
                    e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            if (TextBox11.CaretIndex == 1)
            {
                if (TextBox11.Text != "1")
                {
                    e.Handled = e.Key != Key.Tab;
                }
                if (TextBox11.Text == "1")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.D0 ||
                        e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.Tab)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
        }

        private void textBox11_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox11.Text = TextBox11.Text.Trim();
            TextBox11.SelectionStart = TextBox11.Text.Length;
        }

        private void textBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox12.CaretIndex == 0)
            {
                if (e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.D1 || e.Key == Key.D2)
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
            if (TextBox12.CaretIndex == 1)
            {
                if (TextBox12.Text == "1")
                {
                    if (e.Key == Key.NumPad9 || e.Key == Key.D9)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                if (TextBox12.Text == "2")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.D0 ||
                        e.Key == Key.D1 || e.Key == Key.D2)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
            if (TextBox12.CaretIndex == 2)
            {
                if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                    e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                    e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                    e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                    e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
            if (TextBox12.CaretIndex == 3)
            {
                if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                    e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                    e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                    e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                    e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab)
                    e.Handled = false;
                else
                    e.Handled = true;
            }
        }

        private void textBox12_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox12.Text = TextBox12.Text.Trim();
            TextBox12.SelectionStart = TextBox12.Text.Length;
        }

        private void textBox13_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 || e.Key == Key.D2 ||
                e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 ||
                e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BaseFunctions.SaveDataBase();
        }
    }
}