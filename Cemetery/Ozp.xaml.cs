using System;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Printing;
using System.Security.Cryptography;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Cemetery
{
    /// <summary>
    /// Логика взаимодействия для OZP.xaml
    /// </summary>
    public partial class Ozp
    {
        private readonly string _day = "";
        private readonly int _mode;
        private readonly string _month = "";
        private readonly DataTable _myTable = new DataTable();
        private readonly string _year = "";
        private readonly string _period = "";

        public Ozp(string y, string m, string d, int md, string per)
        {
            _year = y;
            _month = m;
            _day = d;
            _mode = md;
            _period = per;
            InitializeComponent();
        }

        public void Message(string message)
        {
            var mess = new Message(message);
            mess.Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_mode != 0)
                Button4.Visibility = Visibility.Hidden;
            Label4.Content = "Отчет по захоронениям и памятникамё " + _period;
            if (!String.IsNullOrEmpty(_year) && !String.IsNullOrEmpty(_month) && !String.IsNullOrEmpty(_day))
            {
                string databaseName = BaseFunctions.Getinput();
                string databaseNamecrypt = BaseFunctions.Getbase();

                double totalpr = 0.0;
                double totalpp = 0.0;
                double totalptotal = 0.0;
                double totalrc = 0.0;
                double totalrk = 0.0;
                double totalrtotal = 0.0;
                double totalcoms = 0.0;
                
                string sqlquer = "SELECT ID,date,pr,pp,ptotal,rc,rk,rtotal,com,coms FROM OZP WHERE ";

                if (_mode == 0)
                    sqlquer += " year = '" + _year + "' AND month = '" + _month + "' AND day = '" + _day + "'";

                if (_mode == 1 || _mode == 2)
                {
                    sqlquer += " year = '" + _year + "' AND month = '" + _month + "' AND";
                    string[] mass = _day.Split(',');
                    for (int i = 0; i < mass.Length; i++)
                    {
                        if (i == 0)
                            sqlquer += " (day = '" + mass[i] + "'";
                        if (i != 0 && i != (mass.Length - 1))
                            sqlquer += " OR day = '" + mass[i] + "'";
                        if (i == (mass.Length - 1))
                            sqlquer += " OR day = '" + mass[i] + "')";
                    }
                }

                if (_mode == 3)
                {
                    string[] mass = _year.Split(',');
                    for (int i = 0; i < mass.Length; i++)
                    {
                        if (i == 0)
                            sqlquer += " (year = '" + mass[i] + "'";
                        if (i != 0 && i != (mass.Length - 1))
                            sqlquer += " OR year = '" + mass[i] + "'";
                        if (i == (mass.Length - 1))
                            sqlquer += " OR year = '" + mass[i] + "') AND";
                    }

                    mass = _month.Split(',');
                    for (int i = 0; i < mass.Length; i++)
                    {
                        if (i == 0)
                            sqlquer += " (month = '" + mass[i] + "'";
                        if (i != 0 && i != (mass.Length - 1))
                            sqlquer += " OR month = '" + mass[i] + "'";
                        if (i == (mass.Length - 1))
                            sqlquer += " OR month = '" + mass[i] + "') AND";
                    }

                    mass = _day.Split(',');
                    for (int i = 0; i < mass.Length; i++)
                    {
                        if (i == 0)
                            sqlquer += " (day = '" + mass[i] + "'";
                        if (i != 0 && i != (mass.Length - 1))
                            sqlquer += " OR day = '" + mass[i] + "'";
                        if (i == (mass.Length - 1))
                            sqlquer += " OR day = '" + mass[i] + "')";
                    }
                }

                var dt = new DataTable("Clients");

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
                //command.ExecuteNonQuery();

                var dtmem = new DataTable();
                dtmem.Load(command.ExecuteReader());

                _myTable.Columns.Add();
                _myTable.Columns.Add();
                _myTable.Columns.Add();
                _myTable.Columns.Add();
                _myTable.Columns.Add();
                _myTable.Columns.Add();
                _myTable.Columns.Add();
                _myTable.Columns.Add();
                _myTable.Columns.Add();
                _myTable.Columns.Add();

                dt.Columns.Add();
                dt.Columns.Add();
                dt.Columns.Add();
                dt.Columns.Add();
                dt.Columns.Add();
                dt.Columns.Add();
                dt.Columns.Add();
                dt.Columns.Add();
                dt.Columns.Add();
                dt.Columns.Add();

                foreach (DataRow record in dtmem.Rows)
                {
                    _myTable.Rows.Add(new object[]
                    {
                       (Int64) record["ID"], CryptDec.NewForm(record["date"].ToString()), 
                       CryptDec.NewForm(record["pr"].ToString()), CryptDec.NewForm(record["pp"].ToString()), 
                       CryptDec.NewForm(record["ptotal"].ToString()), CryptDec.NewForm(record["rc"].ToString()),
                       CryptDec.NewForm(record["rk"].ToString()), CryptDec.NewForm(record["rtotal"].ToString()),
                       CryptDec.NewForm(record["com"].ToString()), CryptDec.NewForm(record["coms"].ToString())
                    });
                    dt.Rows.Add(new object[]
                    {
                       (Int64) record["ID"], CryptDec.NewForm(record["date"].ToString()), 
                       CryptDec.NewForm(record["pr"].ToString()), CryptDec.NewForm(record["pp"].ToString()), 
                       CryptDec.NewForm(record["ptotal"].ToString()), CryptDec.NewForm(record["rc"].ToString()),
                       CryptDec.NewForm(record["rk"].ToString()), CryptDec.NewForm(record["rtotal"].ToString()),
                       CryptDec.NewForm(record["com"].ToString()), CryptDec.NewForm(record["coms"].ToString())
                    });
                }


                //var dataAdp = new SQLiteDataAdapter(command);

                //dataAdp.Fill(dt);
                //dataAdp.Fill(_myTable);

                connection.Close();

                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);

                dt.Columns[0].ColumnName = "ID";
                dt.Columns[1].ColumnName = "Дата";
                dt.Columns[2].ColumnName = "Приход рит услуги";
                dt.Columns[3].ColumnName = "Приход памятники";
                dt.Columns[4].ColumnName = "Приход всего";
                dt.Columns[5].ColumnName = "Расход кладбище";
                dt.Columns[6].ColumnName = "Расход КАМЕЯ";
                dt.Columns[7].ColumnName = "Всего расход";
                dt.Columns[8].ColumnName = "Примечание";
                dt.Columns[9].ColumnName = "руб";

                DataGrid1.ItemsSource = dt.DefaultView;
                ////DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                DatePicker1.Focus();

                var tempCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

                for (int i = 0; i < _myTable.Rows.Count; i++)
                {
                    if (_myTable.Rows[i].ItemArray[2].ToString() != "")
                    {
                        totalpr += Convert.ToDouble(_myTable.Rows[i].ItemArray[2].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                    {
                        totalpp += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[4].ToString() != "")
                    {
                        totalptotal += Convert.ToDouble(_myTable.Rows[i].ItemArray[4].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[5].ToString() != "")
                    {
                        totalrc += Convert.ToDouble(_myTable.Rows[i].ItemArray[5].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[6].ToString() != "")
                    {
                        totalrk += Convert.ToDouble(_myTable.Rows[i].ItemArray[6].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[7].ToString() != "")
                    {
                        totalrtotal += Convert.ToDouble(_myTable.Rows[i].ItemArray[7].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[9].ToString() != "")
                    {
                        totalcoms += Convert.ToDouble(_myTable.Rows[i].ItemArray[9].ToString());
                    }

                }

                Thread.CurrentThread.CurrentCulture = tempCulture;

                TextBox5.Text = totalpr.ToString(CultureInfo.InvariantCulture);
                TextBox9.Text = totalpp.ToString(CultureInfo.InvariantCulture);
                TextBox10.Text = totalptotal.ToString(CultureInfo.InvariantCulture);
                TextBox11.Text = totalrc.ToString(CultureInfo.InvariantCulture);
                TextBox12.Text = totalrk.ToString(CultureInfo.InvariantCulture);
                TextBox6.Text = totalrtotal.ToString(CultureInfo.InvariantCulture);
                TextBox13.Text = totalcoms.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var rowView = DataGrid1.SelectedValue as DataRowView;
            if (rowView != null)
            {
                DatePicker1.Text = rowView[1].ToString();
                TextBox3.Text = rowView[2].ToString();
                TextBox4.Text = rowView[3].ToString();
                TextBox7.Text = rowView[5].ToString();
                TextBox1.Text = rowView[6].ToString();
                TextBox8.Text = rowView[8].ToString();
                TextBox2.Text = rowView[9].ToString();
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            var rowView = DataGrid1.SelectedIndex;
            if (rowView == -1)
            {
                Message("Строка редактирования \n       не выбрана");
            }
            else
            {

                double totalpr = 0.0;
                double totalpp = 0.0;
                double totalptotal = 0.0;
                double totalrc = 0.0;
                double totalrk = 0.0;
                double totalrtotal = 0.0;
                double totalcoms = 0.0;

                var rowItem = DataGrid1.SelectedValue as DataRowView;
                string databaseName = BaseFunctions.Getinput();
                string databaseNamecrypt = BaseFunctions.Getbase();

                if (rowItem != null)
                {
                    string[] mem = (DatePicker1.ToString()).Split(' ');
                    var date = mem[0];
                    int strNum = Convert.ToInt32(rowItem[0].ToString());
                    var dateD = CryptDec.BackToForm(date);
                    var prD = CryptDec.BackToForm(TextBox3.Text);
                    var ppD = CryptDec.BackToForm(TextBox4.Text);
                    var size = Convert.ToDouble(TextBox3.Text) + Convert.ToDouble(TextBox4.Text);
                    var ptotalD = CryptDec.BackToForm(size.ToString(CultureInfo.InvariantCulture));
                    var rcD = CryptDec.BackToForm(TextBox7.Text);
                    var rkD = CryptDec.BackToForm(TextBox1.Text);
                    var size2 = Convert.ToDouble(TextBox7.Text) + Convert.ToDouble(TextBox1.Text);
                    var rtotalD = CryptDec.BackToForm(size2.ToString(CultureInfo.InvariantCulture));
                    var comD = CryptDec.BackToForm(TextBox8.Text);
                    var comsD = CryptDec.BackToForm(TextBox2.Text);

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
                            "UPDATE OZP SET date = '" + dateD + "'," +
                            "pr = '" + prD + "'," +
                            "pp = '" + ppD + "'," +
                            "ptotal = '" + ptotalD + "'," +
                            "rc = '" + rcD + "'," +
                            "rk = '" + rkD + "'," +
                            "rtotal = '" + rtotalD + "'," +
                            "com = '" + comD + "', coms = '" + comsD + "' WHERE ID = '" + strNum + "'", connection);
                    command.ExecuteNonQuery();

                    connection.Close();
                    CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                    File.Delete(databaseName);

                    _myTable.Rows[rowView][1] = date;
                    _myTable.Rows[rowView][2] = TextBox3.Text;
                    _myTable.Rows[rowView][3] = TextBox4.Text;
                    _myTable.Rows[rowView][4] = size;
                    _myTable.Rows[rowView][5] = TextBox7.Text;
                    _myTable.Rows[rowView][6] = TextBox1.Text;
                    _myTable.Rows[rowView][7] = size2;
                    _myTable.Rows[rowView][8] = TextBox8.Text;
                    _myTable.Rows[rowView][9] = TextBox2.Text;
                }

                _myTable.Columns[0].ColumnName = "ID";
                _myTable.Columns[1].ColumnName = "Дата";
                _myTable.Columns[2].ColumnName = "Приход рит услуги";
                _myTable.Columns[3].ColumnName = "Приход памятники";
                _myTable.Columns[4].ColumnName = "Приход всего";
                _myTable.Columns[5].ColumnName = "Расход кладбище";
                _myTable.Columns[6].ColumnName = "Расход КАМЕЯ";
                _myTable.Columns[7].ColumnName = "Всего расход";
                _myTable.Columns[8].ColumnName = "Примечание";
                _myTable.Columns[9].ColumnName = "руб";


                DataGrid1.ItemsSource = _myTable.DefaultView;
                //DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                var tempCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

                for (int i = 0; i < _myTable.Rows.Count; i++)
                {
                    if (_myTable.Rows[i].ItemArray[2].ToString() != "")
                    {
                        totalpr += Convert.ToDouble(_myTable.Rows[i].ItemArray[2].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                    {
                        totalpp += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[4].ToString() != "")
                    {
                        totalptotal += Convert.ToDouble(_myTable.Rows[i].ItemArray[4].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[5].ToString() != "")
                    {
                        totalrc += Convert.ToDouble(_myTable.Rows[i].ItemArray[5].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[6].ToString() != "")
                    {
                        totalrk += Convert.ToDouble(_myTable.Rows[i].ItemArray[6].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[7].ToString() != "")
                    {
                        totalrtotal += Convert.ToDouble(_myTable.Rows[i].ItemArray[7].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[9].ToString() != "")
                    {
                        totalcoms += Convert.ToDouble(_myTable.Rows[i].ItemArray[9].ToString());
                    }

                }

                Thread.CurrentThread.CurrentCulture = tempCulture;

                TextBox5.Text = totalpr.ToString(CultureInfo.InvariantCulture);
                TextBox9.Text = totalpp.ToString(CultureInfo.InvariantCulture);
                TextBox10.Text = totalptotal.ToString(CultureInfo.InvariantCulture);
                TextBox11.Text = totalrc.ToString(CultureInfo.InvariantCulture);
                TextBox12.Text = totalrk.ToString(CultureInfo.InvariantCulture);
                TextBox6.Text = totalrtotal.ToString(CultureInfo.InvariantCulture);
                TextBox13.Text = totalcoms.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            double totalpr = 0.0;
            double totalpp = 0.0;
            double totalptotal = 0.0;
            double totalrc = 0.0;
            double totalrk = 0.0;
            double totalrtotal = 0.0;
            double totalcoms = 0.0; 
            
            var rowView = DataGrid1.SelectedIndex;
            if (rowView == -1)
            {
                Message("Строка редактирования \n       не выбрана");
            }
            else
            {
                var rowItem = DataGrid1.SelectedValue as DataRowView;
                string databaseName = BaseFunctions.Getinput();
                string databaseNamecrypt = BaseFunctions.Getbase();
                if (rowItem != null)
                {
                    int strNum = Convert.ToInt32(rowItem[0].ToString());

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
                    var command = new SQLiteCommand("DELETE FROM OZP WHERE ID = '" + strNum + "'", connection);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);

                _myTable.Rows[rowView].Delete();

                _myTable.AcceptChanges();

                _myTable.Columns[0].ColumnName = "ID";
                _myTable.Columns[1].ColumnName = "Дата";
                _myTable.Columns[2].ColumnName = "Приход рит услуги";
                _myTable.Columns[3].ColumnName = "Приход памятники";
                _myTable.Columns[4].ColumnName = "Приход всего";
                _myTable.Columns[5].ColumnName = "Расход кладбище";
                _myTable.Columns[6].ColumnName = "Расход КАМЕЯ";
                _myTable.Columns[7].ColumnName = "Всего расход";
                _myTable.Columns[8].ColumnName = "Примечание";
                _myTable.Columns[9].ColumnName = "руб";

                DataGrid1.ItemsSource = _myTable.DefaultView;
                //DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                var tempCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

                for (int i = 0; i < _myTable.Rows.Count; i++)
                {
                    if (_myTable.Rows[i].ItemArray[2].ToString() != "")
                    {
                        totalpr += Convert.ToDouble(_myTable.Rows[i].ItemArray[2].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                    {
                        totalpp += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[4].ToString() != "")
                    {
                        totalptotal += Convert.ToDouble(_myTable.Rows[i].ItemArray[4].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[5].ToString() != "")
                    {
                        totalrc += Convert.ToDouble(_myTable.Rows[i].ItemArray[5].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[6].ToString() != "")
                    {
                        totalrk += Convert.ToDouble(_myTable.Rows[i].ItemArray[6].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[7].ToString() != "")
                    {
                        totalrtotal += Convert.ToDouble(_myTable.Rows[i].ItemArray[7].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[9].ToString() != "")
                    {
                        totalcoms += Convert.ToDouble(_myTable.Rows[i].ItemArray[9].ToString());
                    }

                }

                Thread.CurrentThread.CurrentCulture = tempCulture;

                TextBox5.Text = totalpr.ToString(CultureInfo.InvariantCulture);
                TextBox9.Text = totalpp.ToString(CultureInfo.InvariantCulture);
                TextBox10.Text = totalptotal.ToString(CultureInfo.InvariantCulture);
                TextBox11.Text = totalrc.ToString(CultureInfo.InvariantCulture);
                TextBox12.Text = totalrk.ToString(CultureInfo.InvariantCulture);
                TextBox6.Text = totalrtotal.ToString(CultureInfo.InvariantCulture);
                TextBox13.Text = totalcoms.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

            double totalpr = 0.0;
            double totalpp = 0.0;
            double totalptotal = 0.0;
            double totalrc = 0.0;
            double totalrk = 0.0;
            double totalrtotal = 0.0;
            double totalcoms = 0.0;

            int y = 0;

            var myFlowDocument = new FlowDocument();
            var table1 = new Table();

            const int numberOfColumns = 10;
            for (int x = 0; x < numberOfColumns; x++)
            {
                table1.Columns.Add(new TableColumn());

                // Set alternating background colors for the middle colums. 

                table1.Columns[x].Background = Brushes.White;

                if (x == 0)
                    table1.Columns[x].Width = new GridLength(30);
                if (x == 1)
                    table1.Columns[x].Width = new GridLength(100);
                if (x == 2)
                    table1.Columns[x].Width = new GridLength(100);
                if (x == 3)
                    table1.Columns[x].Width = new GridLength(100);
                if (x == 4 || x == 5)
                    table1.Columns[x].Width = new GridLength(100);
                if (x == 6)
                    table1.Columns[x].Width = new GridLength(100);
                if (x == 7)
                    table1.Columns[x].Width = new GridLength(100);
                if (x == 8)
                    table1.Columns[x].Width = new GridLength(150);
                if (x == 8)
                    table1.Columns[x].Width = new GridLength(100);

            }


            // Create and add an empty TableRowGroup to hold the table's Rows.
            table1.RowGroups.Add(new TableRowGroup());

            // Add the first (title) row.
            table1.RowGroups[0].Rows.Add(new TableRow());

            // Alias the current working row for easy reference.
            TableRow currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the title row.
            currentRow.Background = Brushes.White;
            currentRow.FontSize = 15;
            currentRow.FontWeight = FontWeights.Bold;

            // Add the header row with content, 
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("                                              '" + "КАМЕЯ-РИТУАЛ" + "'"))));
            currentRow.Cells[0].ColumnSpan = 10;

            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the row.
            currentRow.FontSize = 14;
            currentRow.FontWeight = FontWeights.Bold;

            // Add cells with content to the third row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("                                              Кузьмоловское кладбище"))));
            currentRow.Cells[0].ColumnSpan = 10;

            y = FreestringY(table1, y);

            // Add the third row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the row.
            currentRow.FontSize = 14;
            currentRow.FontWeight = FontWeights.Bold;

            string headerName = "Отчет по захоронениям и памятникам " + _period;

            // Add cells with content to the third row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(headerName.ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells[0].ColumnSpan = 10;

            y = FreestringY(table1, y);

            // Add the second (header) row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the header row.
            currentRow.FontSize = 18;
            currentRow.FontWeight = FontWeights.Bold;

            // Add cells with content to the second row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("№"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Дата"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Приход рит услуги"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Приход памятники"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Приход всего"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Расход кладбище"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Расход КАМЕЯ"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Всего расход"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Примечание"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("руб"))));

            currentRow.Cells[0].TextAlignment = TextAlignment.Center;
            currentRow.Cells[1].TextAlignment = TextAlignment.Center;
            currentRow.Cells[2].TextAlignment = TextAlignment.Center;
            currentRow.Cells[3].TextAlignment = TextAlignment.Center;
            currentRow.Cells[4].TextAlignment = TextAlignment.Center;
            currentRow.Cells[5].TextAlignment = TextAlignment.Center;
            currentRow.Cells[6].TextAlignment = TextAlignment.Center;
            currentRow.Cells[7].TextAlignment = TextAlignment.Center;
            currentRow.Cells[8].TextAlignment = TextAlignment.Center;
            currentRow.Cells[9].TextAlignment = TextAlignment.Center;

            y = FreestringY(table1, y);

            int count = 1;

            for (int i = 0; i < _myTable.Rows.Count; i++)
            {
                if (_myTable.Rows[i].ItemArray[2].ToString() != "")
                {
                    totalpr += Convert.ToDouble(_myTable.Rows[i].ItemArray[2].ToString());
                }
                if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                {
                    totalpp += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString());
                }
                if (_myTable.Rows[i].ItemArray[4].ToString() != "")
                {
                    totalptotal += Convert.ToDouble(_myTable.Rows[i].ItemArray[4].ToString());
                }
                if (_myTable.Rows[i].ItemArray[5].ToString() != "")
                {
                    totalrc += Convert.ToDouble(_myTable.Rows[i].ItemArray[5].ToString());
                }
                if (_myTable.Rows[i].ItemArray[6].ToString() != "")
                {
                    totalrk += Convert.ToDouble(_myTable.Rows[i].ItemArray[6].ToString());
                }
                if (_myTable.Rows[i].ItemArray[7].ToString() != "")
                {
                    totalrtotal += Convert.ToDouble(_myTable.Rows[i].ItemArray[7].ToString());
                }
                if (_myTable.Rows[i].ItemArray[9].ToString() != "")
                {
                    totalcoms += Convert.ToDouble(_myTable.Rows[i].ItemArray[9].ToString());
                }

                // Add the third row.
                table1.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table1.RowGroups[0].Rows[y];

                var mySolidColorBrush = new SolidColorBrush { Color = Color.FromArgb(255, 240, 240, 240) };

                // Describes the brush's color using RGB values. 
                // Each value has a range of 0-255.

                currentRow.Background = i % 2 == 0 ? mySolidColorBrush : Brushes.White;

                // Global formatting for the row.
                currentRow.FontSize = 14;
                currentRow.FontWeight = FontWeights.Normal;

                // Add cells with content to the third row.
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(count.ToString(CultureInfo.InvariantCulture)))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[1].ToString()))));
                if (_myTable.Rows[i].ItemArray[2].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[2]))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[2].ToString()))));
                }
                if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[3]))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[3].ToString()))));
                }
                if (_myTable.Rows[i].ItemArray[4].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[4]))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[4].ToString()))));
                }
                if (_myTable.Rows[i].ItemArray[5].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[5]))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[5].ToString()))));
                }
                if (_myTable.Rows[i].ItemArray[6].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[6]))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[6].ToString()))));
                }
                if (_myTable.Rows[i].ItemArray[7].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[7]))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[7].ToString()))));
                }
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[8].ToString()))));
                if (_myTable.Rows[i].ItemArray[9].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[9]))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[9].ToString()))));
                }

                currentRow.Cells[0].TextAlignment = TextAlignment.Center;
                currentRow.Cells[1].TextAlignment = TextAlignment.Center;
                currentRow.Cells[2].TextAlignment = TextAlignment.Center;
                currentRow.Cells[3].TextAlignment = TextAlignment.Center;
                currentRow.Cells[4].TextAlignment = TextAlignment.Center;
                currentRow.Cells[5].TextAlignment = TextAlignment.Center;
                currentRow.Cells[6].TextAlignment = TextAlignment.Center;
                currentRow.Cells[7].TextAlignment = TextAlignment.Center;
                currentRow.Cells[8].TextAlignment = TextAlignment.Center;
                currentRow.Cells[9].TextAlignment = TextAlignment.Center;

                y++;
                count++;
            }

            y = FreestringY(table1, y);


            // Bold the first cell.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the footer row.
            currentRow.FontSize = 14;
            currentRow.FontWeight = FontWeights.Bold;

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("ИТОГО"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", totalpr).ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", totalpp).ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", totalptotal).ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", totalrc).ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", totalrk).ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", totalrtotal).ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", totalcoms).ToString(CultureInfo.InvariantCulture)))));
        
            currentRow.Cells[2].TextAlignment = TextAlignment.Center;
            currentRow.Cells[3].TextAlignment = TextAlignment.Center;
            currentRow.Cells[4].TextAlignment = TextAlignment.Center;
            currentRow.Cells[5].TextAlignment = TextAlignment.Center;
            currentRow.Cells[6].TextAlignment = TextAlignment.Center;
            currentRow.Cells[7].TextAlignment = TextAlignment.Center;
            currentRow.Cells[8].TextAlignment = TextAlignment.Center;
            currentRow.Cells[9].TextAlignment = TextAlignment.Center;

            y = FreestringY(table1, y);
            y = FreestringY(table1, y);

            // Bold the first cell.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];

            // Global formatting for the footer row.
            currentRow.FontSize = 14;
            currentRow.FontWeight = FontWeights.Bold;

            // Add cells with content to the third row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("                       Директор                                                                     М.Р. Багаутдинов"))));
            currentRow.Cells[0].ColumnSpan = 10;

            myFlowDocument.Blocks.Add(table1);

            //Print the document
            var dialog = new PrintDialog();
            dialog.PrintTicket = dialog.PrintQueue.DefaultPrintTicket;
            dialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
            if (dialog.ShowDialog() == true)
            {
                myFlowDocument.PageHeight = dialog.PrintableAreaHeight;
                myFlowDocument.PageWidth = dialog.PrintableAreaWidth;
                myFlowDocument.PagePadding = new Thickness(25);

                myFlowDocument.ColumnGap = 1;

                myFlowDocument.ColumnWidth = (myFlowDocument.PageWidth -
                                              myFlowDocument.ColumnGap -
                                              myFlowDocument.PagePadding.Left -
                                              myFlowDocument.PagePadding.Right);
                const int margin = 5;
                var pageSize = new Size(dialog.PrintableAreaWidth - margin * 2, dialog.PrintableAreaHeight - margin * 2);
                var paginator = myFlowDocument as IDocumentPaginatorSource;
                paginator.DocumentPaginator.PageSize = pageSize;
                dialog.PrintDocument(paginator.DocumentPaginator, "Flow print");
            }
        }

        private static int FreestringY(Table table1, int y)
        {

            table1.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the row.
            currentRow.FontSize = 14;
            currentRow.FontWeight = FontWeights.Normal;

            // Add cells with content to the third row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            currentRow.Cells[0].ColumnSpan = 10;

            return y;
        }

        private void TextBox7_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem2 || e.Key == Key.Decimal)
            {
                e.Handled = true;
                TextBox7.Text += ",";
                TextBox7.SelectionStart = TextBox7.Text.Length;
            }
            if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            string date = DatePicker1.ToString();
            string com = TextBox8.Text;

            double totalpr = 0.0;
            double totalpp = 0.0;
            double totalptotal = 0.0;
            double totalrc = 0.0;
            double totalrk = 0.0;
            double totalrtotal = 0.0;
            double totalcoms = 0.0;

            string[] mem = date.Split(' ');
            date = mem[0];

            double pr = 0.0;
            double pp = 0.0;
            double ptotal;
            double rc = 0.0;
            double rk = 0.0;
            double rtotal;
            double coms = 0.0;
            Int64 numberdead = 0;

            if (!String.IsNullOrEmpty(TextBox3.Text))
                pr = Convert.ToDouble(TextBox3.Text);
            if (!String.IsNullOrEmpty(TextBox4.Text))
                pp = Convert.ToDouble(TextBox4.Text);

            ptotal = pr + pp;

            if (!String.IsNullOrEmpty(TextBox7.Text))
                rc = Convert.ToDouble(TextBox7.Text);
            if (!String.IsNullOrEmpty(TextBox1.Text))
                rk = Convert.ToDouble(TextBox1.Text);

            rtotal = rc + rk;

            if (!String.IsNullOrEmpty(TextBox2.Text))
                coms = Convert.ToDouble(TextBox2.Text);

            var datecr = CryptDec.BackToForm(date);
            var prcr = CryptDec.BackToForm(pr.ToString(CultureInfo.InvariantCulture));
            var ppcr = CryptDec.BackToForm(pp.ToString(CultureInfo.InvariantCulture));
            var ptotalcr = CryptDec.BackToForm(ptotal.ToString(CultureInfo.InvariantCulture));
            var rccr = CryptDec.BackToForm(rc.ToString(CultureInfo.InvariantCulture));
            var rkcr = CryptDec.BackToForm(rk.ToString(CultureInfo.InvariantCulture));
            var rtotalcr = CryptDec.BackToForm(rtotal.ToString(CultureInfo.InvariantCulture));
            var comcr = CryptDec.BackToForm(com);
            var comscr = CryptDec.BackToForm(coms.ToString(CultureInfo.InvariantCulture));

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

            var dt2 = new DataTable();
            var dt3 = new DataTable();

            Int64 countnumberdead = 1;

            var command2 = new SQLiteCommand("SELECT Count(*) FROM OZP", connection);

            dt2.Load(command2.ExecuteReader());
            foreach (DataRow record in dt2.Rows)
            {
                numberdead = (Int64)record["Count(*)"];
            }

            while (countnumberdead >= 1)
            {
                numberdead++;
                var command3 = new SQLiteCommand("SELECT Count(*) FROM OZP WHERE ID = '" + numberdead + "'", connection);

                dt3.Load(command3.ExecuteReader());
                foreach (DataRow record2 in dt3.Rows)
                {
                    countnumberdead = (Int64)record2["Count(*)"];
                }

            }
            string sqlquery = "INSERT INTO OZP(ID,year,month,day,date,pr,pp,ptotal,rc,rk,rtotal,com,coms) VALUES ('" +
                              numberdead + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','" + DateTime.Now.Day + "','" + datecr + "','" +
                              prcr + "','" + ppcr + "','" + ptotalcr + "','" + rccr + "','" + rkcr + "','" + rtotalcr + "','" + comcr + "','" + comscr + "')";
            var command = new SQLiteCommand(sqlquery, connection);
            command.ExecuteNonQuery();
            connection.Close();
            CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
            File.Delete(databaseName);

            _myTable.Rows.Add(new object[]
                {
                    numberdead.ToString(CultureInfo.InvariantCulture), date,pr.ToString(CultureInfo.InvariantCulture),pp.ToString(CultureInfo.InvariantCulture),ptotal.ToString(CultureInfo.InvariantCulture),rc.ToString(CultureInfo.InvariantCulture),rk.ToString(CultureInfo.InvariantCulture),rtotal.ToString(CultureInfo.InvariantCulture),com, coms.ToString(CultureInfo.InvariantCulture)
                });

            _myTable.Columns[0].ColumnName = "ID";
            _myTable.Columns[1].ColumnName = "Дата";
            _myTable.Columns[2].ColumnName = "Приход рит услуги";
            _myTable.Columns[3].ColumnName = "Приход памятники";
            _myTable.Columns[4].ColumnName = "Приход всего";
            _myTable.Columns[5].ColumnName = "Расход кладбище";
            _myTable.Columns[6].ColumnName = "Расход КАМЕЯ";
            _myTable.Columns[7].ColumnName = "Всего расход";
            _myTable.Columns[8].ColumnName = "Примечание";
            _myTable.Columns[9].ColumnName = "руб";


            DataGrid1.ItemsSource = _myTable.DefaultView;
            //DataGrid1.Columns[0].Visibility = Visibility.Hidden;

            DatePicker1.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox7.Text = "";
            TextBox1.Text = "";
            TextBox8.Text = "";
            TextBox2.Text = "";

            DatePicker1.Focus();


            var tempCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            for (int i = 0; i < _myTable.Rows.Count; i++)
            {
                if (_myTable.Rows[i].ItemArray[2].ToString() != "")
                {
                    totalpr += Convert.ToDouble(_myTable.Rows[i].ItemArray[2].ToString());
                }
                if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                {
                    totalpp += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString());
                }
                if (_myTable.Rows[i].ItemArray[4].ToString() != "")
                {
                    totalptotal += Convert.ToDouble(_myTable.Rows[i].ItemArray[4].ToString());
                }
                if (_myTable.Rows[i].ItemArray[5].ToString() != "")
                {
                    totalrc += Convert.ToDouble(_myTable.Rows[i].ItemArray[5].ToString());
                }
                if (_myTable.Rows[i].ItemArray[6].ToString() != "")
                {
                    totalrk += Convert.ToDouble(_myTable.Rows[i].ItemArray[6].ToString());
                }
                if (_myTable.Rows[i].ItemArray[7].ToString() != "")
                {
                    totalrtotal += Convert.ToDouble(_myTable.Rows[i].ItemArray[7].ToString());
                }
                if (_myTable.Rows[i].ItemArray[9].ToString() != "")
                {
                    totalcoms += Convert.ToDouble(_myTable.Rows[i].ItemArray[9].ToString());
                }

            }

            Thread.CurrentThread.CurrentCulture = tempCulture;

            TextBox5.Text = totalpr.ToString(CultureInfo.InvariantCulture);
            TextBox9.Text = totalpp.ToString(CultureInfo.InvariantCulture);
            TextBox10.Text = totalptotal.ToString(CultureInfo.InvariantCulture);
            TextBox11.Text = totalrc.ToString(CultureInfo.InvariantCulture);
            TextBox12.Text = totalrk.ToString(CultureInfo.InvariantCulture);
            TextBox6.Text = totalrtotal.ToString(CultureInfo.InvariantCulture);
            TextBox13.Text = totalcoms.ToString(CultureInfo.InvariantCulture);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BaseFunctions.SaveDataBase();
        }

        private void TextBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem2 || e.Key == Key.Decimal)
            {
                e.Handled = true;
                TextBox3.Text += ",";
                TextBox3.SelectionStart = TextBox3.Text.Length;
            }
            if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void TextBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem2 || e.Key == Key.Decimal)
            {
                e.Handled = true;
                TextBox4.Text += ",";
                TextBox4.SelectionStart = TextBox4.Text.Length;
            }
            if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem2 || e.Key == Key.Decimal)
            {
                e.Handled = true;
                TextBox1.Text += ",";
                TextBox1.SelectionStart = TextBox1.Text.Length;
            }
            if (e.Key == Key.NumPad0 || e.Key == Key.NumPad1 || e.Key == Key.NumPad2 || e.Key == Key.NumPad3 ||
                e.Key == Key.NumPad4 || e.Key == Key.NumPad5 || e.Key == Key.NumPad6 || e.Key == Key.NumPad7 ||
                e.Key == Key.NumPad8 || e.Key == Key.NumPad9 || e.Key == Key.D0 || e.Key == Key.D1 ||
                e.Key == Key.D2 || e.Key == Key.D3 || e.Key == Key.D4 || e.Key == Key.D5 || e.Key == Key.D6 ||
                e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9 || e.Key == Key.Tab)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void TextBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem2 || e.Key == Key.Decimal)
            {
                e.Handled = true;
                TextBox2.Text += ",";
                TextBox2.SelectionStart = TextBox2.Text.Length;
            }
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
