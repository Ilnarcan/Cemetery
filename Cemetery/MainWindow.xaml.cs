using System;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml;
using Application = System.Windows.Forms.Application;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;

namespace Cemetery
{
    ///<summary>
    ///   Логика взаимодействия для MainWindow.xaml
    ///</summary>
    public partial class MainWindow
    {
        private int _minus4;
        private int _minus23;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void Message(string message)
        {
            var mess = new Message(message);
            mess.Show();
        }

        public string Transform(string str)
        {
            var mem = new StringBuilder();

            for (int i = 0; i < str.Length; i++)
            {
                mem.Append(i == 0 ? char.ToUpper(str[i]) : char.ToLower(str[i]));
            }
            return mem.ToString();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string databaseName = BaseFunctions.Getinput();
            string databaseNamecrypt = BaseFunctions.Getbase();
            var dt = new DataTable("Clients");
            Int64 numberdead = 0;

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
            //var command = new SQLiteCommand("SELECT * FROM Clients", connection);
            var command = new SQLiteCommand("SELECT ID,secondName,name,thirdName,age,dateOfDeath,dateOfBurials,seriesOfDocument,numberOfDocument,issuedBy,telephone,sector,raw,bed,urn FROM Clients ORDER BY ID DESC LIMIT 100", connection);
            command.ExecuteNonQuery();

            var dt2 = new DataTable();

            var dataAdp = new SQLiteDataAdapter(command);

            dataAdp.Fill(dt);

            var command2 = new SQLiteCommand("SELECT Count(*) FROM Clients", connection);

            dt2.Load(command2.ExecuteReader());
            foreach (DataRow record in dt2.Rows)
            {
                numberdead = (Int64)record["Count(*)"];
            }


            connection.Close();

            CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
            File.Delete(databaseName);

            TextBlock1.Text = numberdead.ToString(CultureInfo.InvariantCulture);

            dt.Columns["ID"].ColumnName = "ID";
            dt.Columns["secondName"].ColumnName = "Фамилия";
            dt.Columns["name"].ColumnName = "Имя";
            dt.Columns["thirdName"].ColumnName = "Отчество";
            dt.Columns["age"].ColumnName = "Возраст";
            dt.Columns["dateOfDeath"].ColumnName = "Дата смерти";
            dt.Columns["dateOfBurials"].ColumnName = "Дата захоронения";
            dt.Columns["seriesOfDocument"].ColumnName = "Серия документа";
            dt.Columns["numberOfDocument"].ColumnName = "Номер документа";
            dt.Columns["issuedBy"].ColumnName = "Кем выдано";
            dt.Columns["telephone"].ColumnName = "Номер телефона";
            dt.Columns["sector"].ColumnName = "Участок";
            dt.Columns["raw"].ColumnName = "ряд";
            dt.Columns["bed"].ColumnName = "могила";
            dt.Columns["urn"].ColumnName = "Захоронение";

            DataGrid1.ItemsSource = dt.DefaultView;
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            _minus4 = 0;
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

            if (String.IsNullOrEmpty(TextBox4.Text))
            {
                TextBox4.BorderBrush = Brushes.Red;
                iserror = 1;
            }
            else
                TextBox4.BorderBrush = Brushes.LightGray;

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

            if (String.IsNullOrEmpty(TextBox9.Text) || TextBox9.Text.Length < 4)
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

            if (String.IsNullOrEmpty(TextBox12.Text) || TextBox12.Text.Length < 4)
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
                    "INSERT INTO Clients(secondName,name,thirdName,age,dateOfDeath,dateOfBurials,seriesOfDocument,numberOfDocument,issuedBy,telephone,sector,raw,bed,urn) VALUES ('" +
                    secondName + "','" + name + "','" + thirdName + "','" + age + "','" + dateOfDeath + "','" +
                    dateOfBurials + "','" + seriesOfDocument + "','" + numberOfDocument + "','" + issuedBy + "','" +
                    telephone + "','" + sector + "','" + raw + "','" + bed + "','" + urn + "')", connection);
                command.ExecuteNonQuery();
                connection.Close();
                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);

                Message("Карточка покойного успешно создана");

                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";
                TextBox4.Text = "";
                TextBox7.Text = "";
                TextBox8.Text = "";
                TextBox9.Text = "";
                TextBox10.Text = "";
                TextBox11.Text = "";
                TextBox12.Text = "";
                TextBox14.Text = "";
                TextBox15.Text = "";
                TextBox16.Text = "";
                TextBox13.Text = "";
                TextBox17.Text = "";
                TextBox18.Text = "";
                TextBox19.Text = "";
                CheckBox1.IsChecked = false;
            }
        }

        private void button3_Click_1(object sender, RoutedEventArgs e)
        {
            var rowView = DataGrid1.SelectedValue as DataRowView;
            if (rowView == null)
            {
                Message("Карточка покойного не выбрана");
            }
            else
            {
                var f2 = new Edit(Convert.ToInt32(rowView[0].ToString()));
                f2.Show();
            }
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            _minus23 = 0;
            string sqlquer = "SELECT ID,secondName,name, thirdName, age, dateOfDeath, dateOfBurials, seriesOfDocument, numberOfDocument, issuedBy, telephone, sector, raw, bed, urn FROM Clients WHERE";
            int inkey = 0;

            string secondName = TextBox20.Text;
            string name = TextBox21.Text;
            string thirdName = TextBox22.Text;
            string age = TextBox23.Text;
            string dateOfDeath = TextBox26.Text + "." + TextBox27.Text + "." + TextBox28.Text;
            string dateOfBurials = TextBox29.Text + "." + TextBox30.Text + "." + TextBox31.Text;
            string telephone = TextBox32.Text;
            string seriesOfDocument = TextBox33.Text;
            string numberOfDocument = TextBox34.Text;
            string issuedBy = TextBox35.Text;
            string sector = TextBox36.Text;
            string raw = TextBox37.Text;
            string bed = TextBox38.Text;
            string urn = "";

            if (CheckBox2.IsChecked == true)
                urn = "урна";

            if (secondName != "")
            {
                secondName = Transform(secondName);

                if (inkey == 0)
                {
                    sqlquer += " secondName = '" + secondName + "'";
                    inkey = 1;
                }
                else
                    sqlquer += " AND secondName = '" + secondName + "'";
            }

            if (name != "")
            {
                name = Transform(name);

                if (inkey == 0)
                {
                    sqlquer += " name = '" + name + "'";
                    inkey = 1;
                }
                else
                    sqlquer += " AND name = '" + name + "'";
            }

            if (thirdName != "")
            {
                thirdName = Transform(thirdName);

                if (inkey == 0)
                {
                    sqlquer += " thirdName = '" + thirdName + "'";
                    inkey = 1;
                }
                else
                    sqlquer += " AND thirdName = '" + thirdName + "'";
            }

            if (age != "")
            {
                if (inkey == 0)
                {
                    sqlquer += " age = '" + age + "'";
                    inkey = 1;
                }
                else
                    sqlquer += " AND age = '" + age + "'";
            }

            if (dateOfDeath != "" && dateOfDeath != "..")
            {
                if (inkey == 0)
                {
                    string quer = "";

                    if (TextBox26.Text.Length == 1)
                        quer += "0" + TextBox26.Text;
                    if (TextBox26.Text.Length == 2)
                        quer += TextBox26.Text;
                    if (TextBox26.Text.Length == 0)
                        quer += "%";

                    if (TextBox27.Text.Length == 1)
                        quer += ".0" + TextBox27.Text;
                    if (TextBox27.Text.Length == 2)
                        quer += "." + TextBox27.Text;
                    if (TextBox27.Text.Length == 0)
                        quer += ".%";

                    if (TextBox28.Text.Length == 0)
                        quer += ".%";
                    else
                        quer += "." + TextBox28.Text;

                    sqlquer += " dateOfDeath LIKE '" + quer + "'";
                    inkey = 1;
                }
                else
                    sqlquer += " AND dateOfDeath = '" + dateOfDeath + "'";
            }

            if (dateOfBurials != "" && dateOfBurials != "..")
            {
                if (inkey == 0)
                {
                    string quer = "";

                    if (TextBox29.Text.Length == 1)
                        quer += "0" + TextBox29.Text;
                    if (TextBox29.Text.Length == 2)
                        quer += TextBox29.Text;
                    if (TextBox29.Text.Length == 0)
                        quer += "%";

                    if (TextBox30.Text.Length == 1)
                        quer += ".0" + TextBox30.Text;
                    if (TextBox30.Text.Length == 2)
                        quer += "." + TextBox30.Text;
                    if (TextBox30.Text.Length == 0)
                        quer += ".%";

                    if (TextBox31.Text.Length == 0)
                        quer += ".%";
                    else
                        quer += "." + TextBox31.Text;

                    sqlquer += " dateOfBurials LIKE '" + quer + "'";
                    inkey = 1;
                }
                else
                    sqlquer += " AND dateOfBurials = '" + dateOfBurials + "'";
            }

            if (seriesOfDocument != "")
            {
                if (inkey == 0)
                {
                    sqlquer += " seriesOfDocument = '" + seriesOfDocument + "'";
                    inkey = 1;
                }
                else
                    sqlquer += " AND seriesOfDocument = '" + seriesOfDocument + "'";
            }

            if (numberOfDocument != "")
            {
                if (inkey == 0)
                {
                    sqlquer += " numberOfDocument = '" + numberOfDocument + "'";
                    inkey = 1;
                }
                else
                    sqlquer += " AND numberOfDocument = '" + numberOfDocument + "'";
            }

            if (issuedBy != "")
            {
                if (inkey == 0)
                {
                    sqlquer += " issuedBy = '" + issuedBy + "'";
                    inkey = 1;
                }
                else
                    sqlquer += " AND issuedBy = '" + issuedBy + "'";
            }

            if (telephone != "")
            {
                if (inkey == 0)
                {
                    sqlquer += " telephone = '" + telephone + "'";
                    inkey = 1;
                }
                else
                    sqlquer += " AND telephone = '" + telephone + "'";
            }

            if (sector != "")
            {
                if (inkey == 0)
                {
                    sqlquer += " sector = '" + sector + "'";
                    inkey = 1;
                }
                else
                    sqlquer += " AND sector = '" + sector + "'";
            }

            if (raw != "")
            {
                if (inkey == 0)
                {
                    sqlquer += " raw = '" + raw + "'";
                    inkey = 1;
                }
                else
                    sqlquer += " AND raw = '" + raw + "'";
            }

            if (bed != "")
            {
                if (inkey == 0)
                {
                    sqlquer += " bed = '" + bed + "'";
                    inkey = 1;
                }
                else
                    sqlquer += " AND bed = '" + bed + "'";
            }

            if (urn != "")
            {
                if (inkey == 0)
                {
                    sqlquer += " urn = '" + urn + "'";
                    inkey = 1;
                }
                else
                    sqlquer += " AND urn = '" + urn + "'";
            }

            if (inkey == 0)
            {
                sqlquer = "SELECT ID,secondName,name, thirdName, age, dateOfDeath, dateOfBurials, seriesOfDocument, numberOfDocument, issuedBy, telephone, sector, raw, bed, urn FROM Clients";
            }

            if (secondName == "" && name == "" && thirdName == "" && age == "" && dateOfDeath == ".." && dateOfBurials == ".." &&
                seriesOfDocument == "" && numberOfDocument == "" && issuedBy == "" && telephone == "" && sector == "" &&
                raw == "" && bed == "" && urn != "урна")
            {

            }
            else
            {

                var dt = new DataTable("Clients");

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
                var command = new SQLiteCommand(sqlquer, connection);
                command.ExecuteNonQuery();

                var dataAdp = new SQLiteDataAdapter(command);

                dataAdp.Fill(dt);

                connection.Close();

                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);

                dt.Columns["secondName"].ColumnName = "Фамилия";
                dt.Columns["name"].ColumnName = "Имя";
                dt.Columns["thirdName"].ColumnName = "Отчество";
                dt.Columns["age"].ColumnName = "Возраст";
                dt.Columns["dateOfDeath"].ColumnName = "Дата смерти";
                dt.Columns["dateOfBurials"].ColumnName = "Дата захоронения";
                dt.Columns["seriesOfDocument"].ColumnName = "Серия документа";
                dt.Columns["numberOfDocument"].ColumnName = "Номер документа";
                dt.Columns["issuedBy"].ColumnName = "Кем выдано";
                dt.Columns["telephone"].ColumnName = "Номер телефона";
                dt.Columns["sector"].ColumnName = "Участок";
                dt.Columns["raw"].ColumnName = "ряд";
                dt.Columns["bed"].ColumnName = "могила";
                dt.Columns["urn"].ColumnName = "Захоронение";

                DataGrid2.ItemsSource = dt.DefaultView;
            }
        }

        private void dataGrid1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var rowView = DataGrid1.SelectedValue as DataRowView;
                if (rowView == null)
                {
                    Message("Карточка покойного \n    не выбрана");
                }
                else
                {
                    var f2 = new Edit(Convert.ToInt32(rowView[0].ToString()));
                    f2.Show();
                }
            }
        }

        private void dataGrid2_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                var rowView = DataGrid2.SelectedValue as DataRowView;
                if (rowView == null)
                {
                    Message("Покойный не выбран");
                }
                else
                {
                    var f2 = new Edit(Convert.ToInt32(rowView[0].ToString()));
                    f2.Show();
                }
            }
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            string databaseNamecrypt = BaseFunctions.Getbase();

            if (!(File.Exists(databaseNamecrypt)))
            {
                Environment.Exit(0);
            }


            var openFileDialog1 = new OpenFileDialog();

            string dir = Application.StartupPath;

            string path2 = dir + "\\Archive\\";

            openFileDialog1.InitialDirectory = path2;
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            string pathnewfile1 = "";
            string pathnewfile2 = "";
            int infile = 0;

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    Stream myStream;
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            infile = 1;
                            string newname = dir + "\\Archive\\" + databaseNamecrypt;

                            pathnewfile1 = openFileDialog1.FileName;
                            pathnewfile2 = newname;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Message("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }

            if (infile == 1)
            {
                dir = dir + "\\" + databaseNamecrypt;

                File.Move(pathnewfile1, pathnewfile2);

                if (File.Exists(dir))
                    File.Delete(dir);

                File.Move(pathnewfile2, dir);

                Message("База успешно загружена");
            }
        }

        private void button6_Click(object sender, RoutedEventArgs e)
        {
            BaseFunctions.SaveDataBase();
            Message("База успешно сохранена");
        }

        private void button7_Click(object sender, RoutedEventArgs e)
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

            var reader = new XmlTextReader(@"base3.xml") { Namespaces = false };
            //var newreader = new XmlTextReader(@"base2.xml") {Namespaces = false};

            for (int i = 1; i < 5071; i++)
            {
                string urn = "";
                int j;
                reader.ReadToFollowing("card");
                //if (backup == 0)
                //{
                //    newreader.ReadToFollowing("card");
                //}

                string headerName = reader.GetAttribute(3);

                string[] mem = headerName.Split(' ');

                string secondName = mem[0];
                string name = mem[1];
                string thirdName = mem[2];

                reader.ReadToFollowing("utext");
                //if (backup == 0)
                //{
                //    newreader.ReadToFollowing("utext");
                //}
                string age = reader.ReadElementContentAsString();
                mem = age.Split(' ');
                if (mem.Length != 1)
                {
                    age = mem[1];

                    if (mem.Length > 3)
                        for (j = 3; j < mem.Length; j++)
                            if (mem[j] == "УРНА" || mem[j] == "УРНЫ" || mem[j] == "УРНА")
                                urn = "УРНА";
                }
                else
                    age = "";

                //newreader.ReadToFollowing("utext");
                reader.ReadToFollowing("utext");
                string dateOfDeath = reader.ReadElementContentAsString();
                mem = dateOfDeath.Split(' ');
                dateOfDeath = mem[2];

                if (mem.Length > 3)
                    for (j = 3; j < mem.Length; j++)
                        if (mem[j] == "УРНА" || mem[j] == "УРНЫ" || mem[j] == "урна")
                            urn = "урна";

                reader.ReadToFollowing("utext");
                //newreader.ReadToFollowing("utext");
                string dateOfBurials = reader.ReadElementContentAsString();
                mem = dateOfBurials.Split(' ');

                if (mem[2].IndexOf("(", StringComparison.Ordinal) > -1)
                {
                    mem = mem[2].Split('(');
                    if (mem[1].IndexOf(")", StringComparison.Ordinal) > -1)
                        mem = mem[1].Split(')');
                    dateOfBurials = mem[0];
                }
                else
                {
                    dateOfBurials = mem[2];
                    if (mem.Length > 3)
                        for (j = 3; j < mem.Length; j++)
                            if (mem[j] == "УРНА" || mem[j] == "УРНЫ" || mem[j] == "урна")
                                urn = "урна";
                }


                //newreader.ReadToFollowing("utext");
                reader.ReadToFollowing("utext");
                string headerDocument = reader.ReadElementContentAsString();
                string seriesOfDocument = "";
                string numberOfDocument = "";
                mem = headerDocument.Split(' ');
                if (mem.Length > 4)
                    seriesOfDocument = mem[4];
                if (mem.Length > 5)
                {
                    seriesOfDocument = mem[4];
                    numberOfDocument = mem[5];
                }

                reader.ReadToFollowing("utext");
                //newreader.ReadToFollowing("utext");
                string issuedBy = reader.ReadElementContentAsString();
                issuedBy = issuedBy.Trim();

                //newreader.ReadToFollowing("utext");
                reader.ReadToFollowing("utext");
                string sector = reader.ReadElementContentAsString();
                mem = sector.Split(' ');

                if (mem.Length >= 3)
                    if (sector.IndexOf("(", StringComparison.Ordinal) > -1)
                    {
                        mem = sector.Split('(');
                        if (mem[1].IndexOf(")", StringComparison.Ordinal) > -1)
                            mem = mem[1].Split(')');
                        sector = mem[0];
                    }
                    else
                    {
                        sector = mem[1] + " " + mem[2];
                        if (mem.Length > 3)
                            for (j = 3; j < mem.Length; j++)
                                if (mem[j] == "УРНА" || mem[j] == "УРНЫ" || mem[j] == "урна")
                                    urn = "урна";
                    }
                else if (mem.Length > 1)
                    sector = mem[1];
                else
                    sector = "";

                //newreader.ReadToFollowing("utext");
                reader.ReadToFollowing("utext");
                string raw = reader.ReadElementContentAsString();
                mem = raw.Split(' ');

                if (mem.Length >= 3)
                    if (raw.IndexOf("(", StringComparison.Ordinal) > -1)
                    {
                        mem = raw.Split('(');
                        if (mem[1].IndexOf(")", StringComparison.Ordinal) > -1)
                            mem = mem[1].Split(')');
                        if (mem[0].IndexOf(" ", StringComparison.Ordinal) > -1)
                            mem = mem[0].Split(' ');
                        raw = mem[0];
                    }
                    else
                    {
                        raw = mem[1];
                    }
                else
                {
                    if (mem.Length < 2)
                    {
                        raw = mem[0] != "ряд" ? mem[0] : "";
                    }
                    else
                    {
                        if (mem[0] != "ряд")
                            raw = mem[0];
                        if (mem[1] != "ряд")
                            raw = mem[1];
                    }
                }

                //newreader.ReadToFollowing("utext");
                reader.ReadToFollowing("utext");
                string bed = reader.ReadElementContentAsString();
                mem = bed.Split(' ');

                if (mem.Length >= 3)
                    if (bed.IndexOf("(", StringComparison.Ordinal) > -1)
                    {
                        mem = bed.Split('(');
                        if (mem[1].IndexOf(")", StringComparison.Ordinal) > -1)
                            mem = mem[1].Split(')');
                        if (mem[0].IndexOf(" ", StringComparison.Ordinal) > -1)
                            mem = mem[0].Split(' ');
                        bed = mem[0];
                    }
                    else
                    {
                        bed = mem[1];
                    }
                else
                {
                    if (mem.Length < 2)
                    {
                        bed = mem[0] != "могила" ? mem[0] : "";
                    }
                    else
                    {
                        if (mem[0] != "могила")
                            bed = mem[0];
                        if (mem[1] != "могила")
                            bed = mem[1];
                    }
                }
                reader.ReadToFollowing("utext");
                string telephone1 = reader.ReadElementContentAsString();
                string telephone = "";
                if ((telephone1.IndexOf("озраст", StringComparison.Ordinal) == -1))
                {
                    mem = telephone1.Split(':');
                    if (mem.Length == 2)
                    {
                        telephone += mem[1].Trim();
                    }

                    reader.ReadToFollowing("utext");
                    string telephone2 = reader.ReadElementContentAsString();
                    if ((telephone2.IndexOf("озраст", StringComparison.Ordinal) == -1))
                    {
                        //reader.ReadToFollowing("utext");        
                        if (telephone2 != "" && telephone != "")
                            telephone += ", " + telephone2;
                        else if (telephone == "")
                            telephone = telephone2;
                    }


                    telephone = telephone.Trim();
                }

                var command =
                new SQLiteCommand(
                  "INSERT INTO Clients(secondName,name,thirdName,age,dateOfDeath,dateOfBurials,seriesOfDocument,numberOfDocument,issuedBy,telephone,sector,raw,bed,urn) VALUES ('" +
                  secondName + "','" + name + "','" + thirdName + "','" + age + "','" + dateOfDeath + "','" +
                  dateOfBurials + "','" + seriesOfDocument + "','" + numberOfDocument + "','" + issuedBy + "','" +
                  telephone + "','" + sector + "','" + raw + "','" + bed + "','" + urn + "')", connection);
                command.ExecuteNonQuery();

            }

            connection.Close();
            CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
            File.Delete(databaseName);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            BaseFunctions.SaveDataBase();
            System.Windows.Application app = System.Windows.Application.Current;
            app.Shutdown();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var dt = new DataTable("Clients");
            Int64 numberdead = 0;
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
            //var command = new SQLiteCommand("SELECT * FROM Clients", connection);
            var command = new SQLiteCommand("SELECT ID,secondName,name,thirdName,age,dateOfDeath,dateOfBurials,seriesOfDocument,numberOfDocument,issuedBy,telephone,sector,raw,bed,urn FROM Clients ORDER BY ID DESC LIMIT 100", connection);
            command.ExecuteNonQuery();
            var dt2 = new DataTable();

            var dataAdp = new SQLiteDataAdapter(command);

            dataAdp.Fill(dt);

            var command2 = new SQLiteCommand("SELECT Count(*) FROM Clients", connection);

            dt2.Load(command2.ExecuteReader());
            foreach (DataRow record in dt2.Rows)
            {
                numberdead = (Int64)record["Count(*)"];
            }


            connection.Close();

            CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
            File.Delete(databaseName);

            TextBlock1.Text = numberdead.ToString(CultureInfo.InvariantCulture);

            dt.Columns["ID"].ColumnName = "ID";
            dt.Columns["secondName"].ColumnName = "Фамилия";
            dt.Columns["name"].ColumnName = "Имя";
            dt.Columns["thirdName"].ColumnName = "Отчество";
            dt.Columns["age"].ColumnName = "Возраст";
            dt.Columns["dateOfDeath"].ColumnName = "Дата смерти";
            dt.Columns["dateOfBurials"].ColumnName = "Дата захоронения";
            dt.Columns["seriesOfDocument"].ColumnName = "Серия документа";
            dt.Columns["numberOfDocument"].ColumnName = "Номер документа";
            dt.Columns["issuedBy"].ColumnName = "Кем выдано";
            dt.Columns["telephone"].ColumnName = "Номер телефона";
            dt.Columns["sector"].ColumnName = "Участок";
            dt.Columns["raw"].ColumnName = "ряд";
            dt.Columns["bed"].ColumnName = "могила";
            dt.Columns["urn"].ColumnName = "Захоронение";

            DataGrid1.ItemsSource = dt.DefaultView;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A || e.Key == Key.B || e.Key == Key.C || e.Key == Key.D || e.Key == Key.E || e.Key == Key.F ||
              e.Key == Key.G || e.Key == Key.H || e.Key == Key.J || e.Key == Key.I || e.Key == Key.K || e.Key == Key.L ||
              e.Key == Key.M || e.Key == Key.N || e.Key == Key.O || e.Key == Key.P || e.Key == Key.Q || e.Key == Key.R ||
              e.Key == Key.S || e.Key == Key.T || e.Key == Key.U || e.Key == Key.V || e.Key == Key.W || e.Key == Key.X ||
              e.Key == Key.Y || e.Key == Key.Z || e.Key == Key.Oem1 || e.Key == Key.Oem3 || e.Key == Key.Oem4 || e.Key == Key.Oem6 ||
              e.Key == Key.Oem7 || e.Key == Key.OemComma || e.Key == Key.OemPeriod || e.Key == Key.Tab)
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
              e.Key == Key.G || e.Key == Key.H || e.Key == Key.J || e.Key == Key.I || e.Key == Key.K || e.Key == Key.L ||
              e.Key == Key.M || e.Key == Key.N || e.Key == Key.O || e.Key == Key.P || e.Key == Key.Q || e.Key == Key.R ||
              e.Key == Key.S || e.Key == Key.T || e.Key == Key.U || e.Key == Key.V || e.Key == Key.W || e.Key == Key.X ||
              e.Key == Key.Y || e.Key == Key.Z || e.Key == Key.Oem1 || e.Key == Key.Oem3 || e.Key == Key.Oem4 || e.Key == Key.Oem6 ||
              e.Key == Key.Oem7 || e.Key == Key.OemComma || e.Key == Key.OemPeriod || e.Key == Key.Tab)
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
              e.Key == Key.G || e.Key == Key.H || e.Key == Key.J || e.Key == Key.I || e.Key == Key.K || e.Key == Key.L ||
              e.Key == Key.M || e.Key == Key.N || e.Key == Key.O || e.Key == Key.P || e.Key == Key.Q || e.Key == Key.R ||
              e.Key == Key.S || e.Key == Key.T || e.Key == Key.U || e.Key == Key.V || e.Key == Key.W || e.Key == Key.X ||
              e.Key == Key.Y || e.Key == Key.Z || e.Key == Key.Oem1 || e.Key == Key.Oem3 || e.Key == Key.Oem4 || e.Key == Key.Oem6 ||
              e.Key == Key.Oem7 || e.Key == Key.OemComma || e.Key == Key.OemPeriod || e.Key == Key.Tab)
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
            if (TextBox4.CaretIndex == 0)
                if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
              e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
              e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
              e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
              e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab)
                e.Handled = false;
                else
                e.Handled = true;
            if (TextBox4.CaretIndex == 1)
                if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                    e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                    e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                    e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                    e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab)
                    e.Handled = false;
                else
                    e.Handled = true;
            if (TextBox4.CaretIndex == 2)
            {
                if (e.Key == Key.OemMinus)
                    _minus4 = 1;
                if (TextBox4.Text == "10" || TextBox4.Text == "11" || TextBox4.Text == "12" || TextBox4.Text == "13" ||
                    TextBox4.Text == "14")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                        e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                        e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                        e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                        e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.OemMinus || e.Key == Key.Tab)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                else
                {
                    if (e.Key == Key.Tab || e.Key == Key.OemMinus)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
            if (TextBox4.CaretIndex == 3)
            {
                if (_minus4 != 1)
                {
                    if (e.Key == Key.Tab)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                else
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
            if (TextBox4.CaretIndex == 4)
            {
                if (_minus4 != 1)
                {
                    e.Handled = true;
                }
                else
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
                  e.Key == Key.D5 || e.Key == Key.D6 || e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab)
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
              e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab || e.Key == Key.OemComma || e.Key == Key.Oem2 || e.Key == Key.RightShift)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void textBox20_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox20.Text = TextBox20.Text.Trim();
            TextBox20.SelectionStart = TextBox20.Text.Length;
        }

        private void textBox21_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox21.Text = TextBox21.Text.Trim();
            TextBox21.SelectionStart = TextBox21.Text.Length;
        }

        private void textBox22_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox22.Text = TextBox22.Text.Trim();
            TextBox22.SelectionStart = TextBox22.Text.Length;
        }

        private void textBox23_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox23.Text = TextBox23.Text.Trim();
            TextBox23.SelectionStart = TextBox23.Text.Length;
        }

        private void textBox26_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox26.Text = TextBox26.Text.Trim();
            TextBox26.SelectionStart = TextBox26.Text.Length;
        }

        private void textBox27_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox27.Text = TextBox27.Text.Trim();
            TextBox27.SelectionStart = TextBox27.Text.Length;
        }

        private void textBox28_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox28.Text = TextBox28.Text.Trim();
            TextBox28.SelectionStart = TextBox28.Text.Length;
        }

        private void textBox29_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox29.Text = TextBox29.Text.Trim();
            TextBox29.SelectionStart = TextBox29.Text.Length;
        }

        private void textBox30_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox30.Text = TextBox30.Text.Trim();
            TextBox30.SelectionStart = TextBox30.Text.Length;
        }

        private void textBox31_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox31.Text = TextBox31.Text.Trim();
            TextBox31.SelectionStart = TextBox31.Text.Length;
        }

        private void textBox32_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox32.Text = TextBox32.Text.Trim();
            TextBox32.SelectionStart = TextBox32.Text.Length;
        }

        private void textBox33_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox33.Text = TextBox33.Text.Trim();
            TextBox33.SelectionStart = TextBox33.Text.Length;
        }

        private void textBox34_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox34.Text = TextBox34.Text.Trim();
            TextBox34.SelectionStart = TextBox34.Text.Length;
        }

        private void textBox35_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox35.Text = TextBox35.Text.Trim();
            TextBox35.SelectionStart = TextBox35.Text.Length;
        }

        private void textBox36_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox36.Text = TextBox36.Text.Trim();
            TextBox36.SelectionStart = TextBox36.Text.Length;
        }

        private void textBox37_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox37.Text = TextBox37.Text.Trim();
            TextBox37.SelectionStart = TextBox37.Text.Length;
        }

        private void textBox38_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox38.Text = TextBox38.Text.Trim();
            TextBox38.SelectionStart = TextBox38.Text.Length;
        }

        private void TextBox23_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox23.CaretIndex == 0)
                if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
              e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
              e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
              e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
              e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab)
                    e.Handled = false;
                else
                    e.Handled = true;
            if (TextBox23.CaretIndex == 1)
                if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                    e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                    e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                    e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                    e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab)
                    e.Handled = false;
                else
                    e.Handled = true;
            if (TextBox23.CaretIndex == 2)
            {
                if (e.Key == Key.OemMinus)
                    _minus23 = 1;
                if (TextBox23.Text == "10" || TextBox23.Text == "11" || TextBox23.Text == "12" || TextBox23.Text == "13" ||
                    TextBox23.Text == "14")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                        e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                        e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                        e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                        e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.OemMinus || e.Key == Key.Tab)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                else
                {
                    e.Handled = e.Key != Key.OemMinus;
                }
            }
            if (TextBox23.CaretIndex == 3)
            {
                if (_minus23 != 1)
                {
                    e.Handled = true;
                }
                else
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
            if (TextBox23.CaretIndex == 4)
            {
                if (_minus23 != 1)
                {
                    e.Handled = true;
                }
                else
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
        }

        private void TextBox31_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox31.CaretIndex == 0)
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
            if (TextBox31.CaretIndex == 1)
            {
                if (TextBox31.Text == "1")
                {
                    if (e.Key == Key.NumPad9 || e.Key == Key.D9)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                if (TextBox31.Text == "2")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.D0 ||
                      e.Key == Key.D1 || e.Key == Key.D2)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
            if (TextBox31.CaretIndex == 2)
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
            if (TextBox31.CaretIndex == 3)
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

        private void TextBox30_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox30.CaretIndex == 0)
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
            if (TextBox30.CaretIndex == 1)
            {
                if (TextBox30.Text != "1")
                {
                    e.Handled = e.Key != Key.Tab;
                }
                if (TextBox30.Text == "1")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.D0 ||
                      e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.Tab)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
        }

        private void TextBox29_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox29.CaretIndex == 0)
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
            if (TextBox29.CaretIndex == 1)
            {
                if (TextBox29.Text == "1" || TextBox29.Text == "2")
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
                if (TextBox29.Text == "3")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.D0 || e.Key == Key.D1 ||
                      e.Key == Key.Tab)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                if (TextBox29.Text == "4" || TextBox29.Text == "5" || TextBox29.Text == "6" || TextBox29.Text == "7" ||
                  TextBox29.Text == "8" || TextBox29.Text == "9")
                {
                    e.Handled = e.Key != Key.Tab;
                }
            }
        }

        private void TextBox28_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox28.CaretIndex == 0)
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
            if (TextBox28.CaretIndex == 1)
            {
                if (TextBox28.Text == "1")
                {
                    if (e.Key == Key.NumPad9 || e.Key == Key.D9)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                if (TextBox28.Text == "2")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.D0 ||
                      e.Key == Key.D1 || e.Key == Key.D2)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
            if (TextBox28.CaretIndex == 2)
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
            if (TextBox28.CaretIndex == 3)
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

        private void TextBox27_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox27.CaretIndex == 0)
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
            if (TextBox27.CaretIndex == 1)
            {
                if (TextBox8.Text != "1")
                {
                    e.Handled = e.Key != Key.Tab;
                }
                if (TextBox27.Text == "1")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.D0 ||
                      e.Key == Key.D1 || e.Key == Key.D2 || e.Key == Key.Tab)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
            }
        }

        private void TextBox26_KeyDown(object sender, KeyEventArgs e)
        {
            if (TextBox26.CaretIndex == 0)
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
            if (TextBox26.CaretIndex == 1)
            {
                if (TextBox26.Text == "1" || TextBox26.Text == "2")
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
                if (TextBox26.Text == "3")
                {
                    if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.D0 || e.Key == Key.D1 ||
                      e.Key == Key.Tab)
                        e.Handled = false;
                    else
                        e.Handled = true;
                }
                if (TextBox26.Text == "4" || TextBox26.Text == "5" || TextBox26.Text == "6" || TextBox26.Text == "7" ||
                  TextBox26.Text == "8" || TextBox26.Text == "9")
                {
                    e.Handled = e.Key != Key.Tab;
                }
            }
        }

        private void TextBox4_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox4.BorderBrush = Brushes.LightGray;
        }

        private void TextBox15_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox15.BorderBrush = Brushes.LightGray;
        }

        private void TextBox16_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox16.BorderBrush = Brushes.LightGray;
        }

        private void TextBox14_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox14.BorderBrush = Brushes.LightGray;
        }

        private void TextBox17_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox17.BorderBrush = Brushes.LightGray;
        }

        private void TextBox18_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox18.BorderBrush = Brushes.LightGray;
        }

        private void TextBox19_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox19.BorderBrush = Brushes.LightGray;
        }

        private void TextBox10_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox10.BorderBrush = Brushes.LightGray;
        }

        private void TextBox11_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox11.BorderBrush = Brushes.LightGray;
        }

        private void TextBox12_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox12.BorderBrush = Brushes.LightGray;
        }

        private void TextBox9_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox9.BorderBrush = Brushes.LightGray;
        }

        private void TextBox8_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox8.BorderBrush = Brushes.LightGray;
        }

        private void TextBox7_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox7.BorderBrush = Brushes.LightGray;
        }

        private void TextBox3_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox3.BorderBrush = Brushes.LightGray;
        }

        private void TextBox2_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox2.BorderBrush = Brushes.LightGray;
        }

        private void TextBox1_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox1.BorderBrush = Brushes.LightGray;
        }
    }
}