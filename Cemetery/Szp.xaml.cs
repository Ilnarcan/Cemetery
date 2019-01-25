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
using System.Windows.Media.Media3D;

namespace Cemetery
{
    /// <summary>
    /// Логика взаимодействия для szp.xaml
    /// </summary>
    public partial class Szp
    {
        private readonly string _day = "";
        private readonly int _mode;
        private readonly string _month = "";
        private readonly DataTable _myTable = new DataTable();
        private readonly string _year = "";
        private readonly string _period = "";

        public Szp(string y, string m, string d, int md, string per)
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
            Label4.Content = "Расход по зарплате и премиальных " + _period;
            if (!String.IsNullOrEmpty(_year) && !String.IsNullOrEmpty(_month) && !String.IsNullOrEmpty(_day))
            {
                string databaseName = BaseFunctions.Getinput();
                string databaseNamecrypt = BaseFunctions.Getbase();

                double totalav = 0.0;
                double totalzp = 0.0;
                double totalavzp;
                double totalam = 0.0;
                double totaltotal;

                string sqlquer = "SELECT ID,date,cheque,av,zp,avzp,am,total,com FROM SZP WHERE ";

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
                       CryptDec.NewForm(record["cheque"].ToString()), CryptDec.NewForm(record["av"].ToString()), 
                       CryptDec.NewForm(record["zp"].ToString()), CryptDec.NewForm(record["avzp"].ToString()),
                       CryptDec.NewForm(record["am"].ToString()), CryptDec.NewForm(record["total"].ToString()),
                       CryptDec.NewForm(record["com"].ToString())
                    });
                    dt.Rows.Add(new object[]
                    {
                       (Int64) record["ID"], CryptDec.NewForm(record["date"].ToString()), 
                       CryptDec.NewForm(record["cheque"].ToString()), CryptDec.NewForm(record["av"].ToString()), 
                       CryptDec.NewForm(record["zp"].ToString()), CryptDec.NewForm(record["avzp"].ToString()),
                       CryptDec.NewForm(record["am"].ToString()), CryptDec.NewForm(record["total"].ToString()),
                       CryptDec.NewForm(record["com"].ToString())
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
                dt.Columns[2].ColumnName = "Наименование";
                dt.Columns[3].ColumnName = "Аванс";
                dt.Columns[4].ColumnName = "Зарплата";
                dt.Columns[5].ColumnName = "Итого";
                dt.Columns[6].ColumnName = "Премия мастерам";
                dt.Columns[7].ColumnName = "Всего расход";
                dt.Columns[8].ColumnName = "Примечание";

                DataGrid1.ItemsSource = dt.DefaultView;
                ////DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                DatePicker1.Focus();

                var tempCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

                for (int i = 0; i < _myTable.Rows.Count; i++)
                {
                    if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                    {
                        totalav += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[4].ToString() != "")
                    {
                        totalzp += Convert.ToDouble(_myTable.Rows[i].ItemArray[4].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[6].ToString() != "")
                    {
                        totalam += Convert.ToDouble(_myTable.Rows[i].ItemArray[6].ToString());
                    }

                }

                Thread.CurrentThread.CurrentCulture = tempCulture;

                totalavzp = totalzp + totalav;
                totaltotal = totalavzp + totalam;

                TextBox5.Text = totalav.ToString(CultureInfo.InvariantCulture);
                TextBox9.Text = totalzp.ToString(CultureInfo.InvariantCulture);
                TextBox10.Text = totalavzp.ToString(CultureInfo.InvariantCulture);
                TextBox11.Text = totalam.ToString(CultureInfo.InvariantCulture);
                TextBox12.Text = totaltotal.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var rowView = DataGrid1.SelectedValue as DataRowView;
            if (rowView != null)
            {
                DatePicker1.Text = rowView[1].ToString();
                TextBox2.Text = rowView[2].ToString();
                TextBox3.Text = rowView[3].ToString();
                TextBox4.Text = rowView[4].ToString();
                TextBox7.Text = rowView[6].ToString();
                TextBox8.Text = rowView[8].ToString();
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

                double totalav = 0.0;
                double totalzp = 0.0;
                double totalavzp;
                double totalam = 0.0;
                double totaltotal;

                var rowItem = DataGrid1.SelectedValue as DataRowView;
                string databaseName = BaseFunctions.Getinput();
                string databaseNamecrypt = BaseFunctions.Getbase();

                if (rowItem != null)
                {
                    string[] mem = (DatePicker1.ToString()).Split(' ');
                    var date = mem[0];
                    int strNum = Convert.ToInt32(rowItem[0].ToString());
                    var dateD = CryptDec.BackToForm(date);
                    var chequeD = CryptDec.BackToForm(TextBox2.Text);
                    var nameD = CryptDec.BackToForm(TextBox3.Text);
                    var unitD = CryptDec.BackToForm(TextBox4.Text);
                    var size = Convert.ToDouble(TextBox3.Text) + Convert.ToDouble(TextBox4.Text);
                    var sizeD = CryptDec.BackToForm(size.ToString(CultureInfo.InvariantCulture));
                    var numberD = CryptDec.BackToForm(TextBox7.Text);
                    double total = size + Convert.ToDouble(TextBox7.Text);
                    var totalD = CryptDec.BackToForm(total.ToString(CultureInfo.InvariantCulture));
                    var burialsD = CryptDec.BackToForm(TextBox8.Text);
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
                            "UPDATE SZP SET date = '" + dateD + "'," +
                            "cheque = '" + chequeD + "'," +
                            "av = '" + nameD + "'," +
                            "zp = '" + unitD  + "'," +
                            "avzp = '" + sizeD + "'," +
                            "am = '" + numberD + "'," +
                            "total = '" + totalD + "'," +
                            "com = '" + burialsD + "' WHERE ID = '" + strNum + "'", connection);
                    command.ExecuteNonQuery();

                    connection.Close();
                    CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                    File.Delete(databaseName);

                    _myTable.Rows[rowView][1] = date;
                    _myTable.Rows[rowView][2] = TextBox2.Text;
                    _myTable.Rows[rowView][3] = TextBox3.Text;
                    _myTable.Rows[rowView][4] = TextBox4.Text;
                    _myTable.Rows[rowView][5] = size;
                    _myTable.Rows[rowView][6] = TextBox7.Text;
                    _myTable.Rows[rowView][7] = total;
                    _myTable.Rows[rowView][8] = TextBox8.Text;
                }

                _myTable.Columns[0].ColumnName = "ID";
                _myTable.Columns[1].ColumnName = "Дата";
                _myTable.Columns[2].ColumnName = "Наименование";
                _myTable.Columns[3].ColumnName = "Аванс";
                _myTable.Columns[4].ColumnName = "Зарплата";
                _myTable.Columns[5].ColumnName = "Итого";
                _myTable.Columns[6].ColumnName = "Премия мастерам";
                _myTable.Columns[7].ColumnName = "Всего расход";
                _myTable.Columns[8].ColumnName = "Примечание";

                DataGrid1.ItemsSource = _myTable.DefaultView;
                //DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                var tempCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

                for (int i = 0; i < _myTable.Rows.Count; i++)
                {
                    if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                    {
                        totalav += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[4].ToString() != "")
                    {
                        totalzp += Convert.ToDouble(_myTable.Rows[i].ItemArray[4].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[6].ToString() != "")
                    {
                        totalam += Convert.ToDouble(_myTable.Rows[i].ItemArray[6].ToString());
                    }

                }

                Thread.CurrentThread.CurrentCulture = tempCulture;

                totalavzp = totalzp + totalav;
                totaltotal = totalavzp + totalam;

                TextBox5.Text = totalav.ToString(CultureInfo.InvariantCulture);
                TextBox9.Text = totalzp.ToString(CultureInfo.InvariantCulture);
                TextBox10.Text = totalavzp.ToString(CultureInfo.InvariantCulture);
                TextBox11.Text = totalam.ToString(CultureInfo.InvariantCulture);
                TextBox12.Text = totaltotal.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {

            double totalav = 0.0;
            double totalzp = 0.0;
            double totalavzp;
            double totalam = 0.0;
            double totaltotal;

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
                    var command = new SQLiteCommand("DELETE FROM SZP WHERE ID = '" + strNum + "'", connection);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);

                _myTable.Rows[rowView].Delete();

                _myTable.AcceptChanges();

                _myTable.Columns[0].ColumnName = "ID";
                _myTable.Columns[1].ColumnName = "Дата";
                _myTable.Columns[2].ColumnName = "Наименование";
                _myTable.Columns[3].ColumnName = "Аванс";
                _myTable.Columns[4].ColumnName = "Зарплата";
                _myTable.Columns[5].ColumnName = "Итого";
                _myTable.Columns[6].ColumnName = "Премия мастерам";
                _myTable.Columns[7].ColumnName = "Всего расход";
                _myTable.Columns[8].ColumnName = "Примечание";

                DataGrid1.ItemsSource = _myTable.DefaultView;
                //DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                var tempCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

                for (int i = 0; i < _myTable.Rows.Count; i++)
                {
                    if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                    {
                        totalav += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[4].ToString() != "")
                    {
                        totalzp += Convert.ToDouble(_myTable.Rows[i].ItemArray[4].ToString());
                    }
                    if (_myTable.Rows[i].ItemArray[6].ToString() != "")
                    {
                        totalam += Convert.ToDouble(_myTable.Rows[i].ItemArray[6].ToString());
                    }

                }

                Thread.CurrentThread.CurrentCulture = tempCulture;

                totalavzp = totalzp + totalav;
                totaltotal = totalavzp + totalam;

                TextBox5.Text = totalav.ToString(CultureInfo.InvariantCulture);
                TextBox9.Text = totalzp.ToString(CultureInfo.InvariantCulture);
                TextBox10.Text = totalavzp.ToString(CultureInfo.InvariantCulture);
                TextBox11.Text = totalam.ToString(CultureInfo.InvariantCulture);
                TextBox12.Text = totaltotal.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {

            double totalav = 0.0;
            double totalzp = 0.0;
            double totalavzp;
            double totalam = 0.0;
            double totaltotal;

            int y = 0;

            var myFlowDocument = new FlowDocument();
            var table1 = new Table();

            const int numberOfColumns = 9;
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
                    table1.Columns[x].Width = new GridLength(150);
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
            currentRow.Cells[0].ColumnSpan = 9;

            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the row.
            currentRow.FontSize = 14;
            currentRow.FontWeight = FontWeights.Bold;

            // Add cells with content to the third row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("                                              Кузьмоловское кладбище"))));
            currentRow.Cells[0].ColumnSpan = 9;

            y = FreestringY(table1, y);

            // Add the third row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the row.
            currentRow.FontSize = 14;
            currentRow.FontWeight = FontWeights.Bold;

            string headerName = "Расход по зарплате и премиальных " + _period;

            // Add cells with content to the third row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(headerName.ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells[0].ColumnSpan = 9;

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
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Наименование"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Аванс"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Зарплата"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Итого"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Премия мастерам"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Всего расход"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Примечание"))));

            currentRow.Cells[0].TextAlignment = TextAlignment.Center;
            currentRow.Cells[1].TextAlignment = TextAlignment.Center;
            currentRow.Cells[2].TextAlignment = TextAlignment.Center;
            currentRow.Cells[3].TextAlignment = TextAlignment.Center;
            currentRow.Cells[4].TextAlignment = TextAlignment.Center;
            currentRow.Cells[5].TextAlignment = TextAlignment.Center;
            currentRow.Cells[6].TextAlignment = TextAlignment.Center;
            currentRow.Cells[7].TextAlignment = TextAlignment.Center;
            currentRow.Cells[8].TextAlignment = TextAlignment.Center;

            y = FreestringY(table1, y);

            int count = 1;

            for (int i = 0; i < _myTable.Rows.Count; i++)
            {
                if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                {
                    totalav += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString());
                }
                if (_myTable.Rows[i].ItemArray[4].ToString() != "")
                {
                    totalzp += Convert.ToDouble(_myTable.Rows[i].ItemArray[4].ToString());
                }
                if (_myTable.Rows[i].ItemArray[6].ToString() != "")
                {
                    totalam += Convert.ToDouble(_myTable.Rows[i].ItemArray[6].ToString());
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
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[2].ToString()))));
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

                currentRow.Cells[0].TextAlignment = TextAlignment.Center;
                currentRow.Cells[1].TextAlignment = TextAlignment.Center;
                currentRow.Cells[2].TextAlignment = TextAlignment.Center;
                currentRow.Cells[3].TextAlignment = TextAlignment.Center;
                currentRow.Cells[4].TextAlignment = TextAlignment.Center;
                currentRow.Cells[5].TextAlignment = TextAlignment.Center;
                currentRow.Cells[6].TextAlignment = TextAlignment.Center;
                currentRow.Cells[7].TextAlignment = TextAlignment.Center;
                currentRow.Cells[8].TextAlignment = TextAlignment.Center;

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

            totalavzp = totalav + totalzp;
            totaltotal = totalam + totalavzp;

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("ИТОГО"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", totalav).ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", totalzp).ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", totalavzp).ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", totalam).ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", totaltotal).ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));

            currentRow.Cells[3].TextAlignment = TextAlignment.Center;
            currentRow.Cells[4].TextAlignment = TextAlignment.Center;
            currentRow.Cells[5].TextAlignment = TextAlignment.Center;
            currentRow.Cells[6].TextAlignment = TextAlignment.Center;
            currentRow.Cells[7].TextAlignment = TextAlignment.Center;
            currentRow.Cells[8].TextAlignment = TextAlignment.Center;

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
            currentRow.Cells[0].ColumnSpan = 9;

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
            currentRow.Cells[0].ColumnSpan = 9;

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
            string name = TextBox2.Text;
            string com = TextBox8.Text;

            double totalav = 0.0;
            double totalzp = 0.0;
            double totalavzp;
            double totalam = 0.0;
            double totaltotal;

            string[] mem = date.Split(' ');
            date = mem[0];

            double av = 0.0;
            double zp = 0.0;
            double avzp = 0.0;
            double am = 0.0;
            double total = 0.0;
            Int64 numberdead = 0;

            if (!String.IsNullOrEmpty(TextBox3.Text))
                av = Convert.ToDouble(TextBox3.Text);
            if (!String.IsNullOrEmpty(TextBox4.Text))
                zp = Convert.ToDouble(TextBox4.Text);
                
            avzp = av + zp;

            if (!String.IsNullOrEmpty(TextBox7.Text))
                am = Convert.ToDouble(TextBox7.Text);

            total = am + avzp;


            var datecr = CryptDec.BackToForm(date);
            var namecr = CryptDec.BackToForm(name);
            var avcr = CryptDec.BackToForm(av.ToString(CultureInfo.InvariantCulture));
            var zpcr = CryptDec.BackToForm(zp.ToString(CultureInfo.InvariantCulture));
            var avzpcr = CryptDec.BackToForm(avzp.ToString(CultureInfo.InvariantCulture));
            var amcr = CryptDec.BackToForm(am.ToString(CultureInfo.InvariantCulture));
            var totalcr = CryptDec.BackToForm(total.ToString(CultureInfo.InvariantCulture));
            var comcr = CryptDec.BackToForm(com);

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

            var command2 = new SQLiteCommand("SELECT Count(*) FROM SZP", connection);

            dt2.Load(command2.ExecuteReader());
            foreach (DataRow record in dt2.Rows)
            {
                numberdead = (Int64)record["Count(*)"];
            }

            while (countnumberdead >= 1)
            {
                numberdead++;
                var command3 = new SQLiteCommand("SELECT Count(*) FROM SZP WHERE ID = '" + numberdead + "'", connection);

                dt3.Load(command3.ExecuteReader());
                foreach (DataRow record2 in dt3.Rows)
                {
                    countnumberdead = (Int64)record2["Count(*)"];
                }

            }
            string sqlquery = "INSERT INTO SZP(ID,year,month,day,date,cheque,av,zp,avzp,am,total,com) VALUES ('" +
                              numberdead + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','" + DateTime.Now.Day + "','" + datecr + "','" +
                              namecr + "','" + avcr + "','" + zpcr + "','" + avzpcr + "','" + amcr + "','" + totalcr + "','" + comcr + "')";
            var command = new SQLiteCommand(sqlquery, connection);
            command.ExecuteNonQuery();
            connection.Close();
            CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
            File.Delete(databaseName);

            _myTable.Rows.Add(new object[]
                {
                    numberdead.ToString(CultureInfo.InvariantCulture), date,name,av.ToString(CultureInfo.InvariantCulture),zp.ToString(CultureInfo.InvariantCulture),avzp.ToString(CultureInfo.InvariantCulture),am.ToString(CultureInfo.InvariantCulture),total.ToString(CultureInfo.InvariantCulture),com
                });

            _myTable.Columns[0].ColumnName = "ID";
            _myTable.Columns[1].ColumnName = "Дата";
            _myTable.Columns[2].ColumnName = "Наименование";
            _myTable.Columns[3].ColumnName = "Аванс";
            _myTable.Columns[4].ColumnName = "Зарплата";
            _myTable.Columns[5].ColumnName = "Итого";
            _myTable.Columns[6].ColumnName = "Премия мастерам";
            _myTable.Columns[7].ColumnName = "Всего расход";
            _myTable.Columns[8].ColumnName = "Примечание";


            DataGrid1.ItemsSource = _myTable.DefaultView;
            //DataGrid1.Columns[0].Visibility = Visibility.Hidden;

            DatePicker1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";

            DatePicker1.Focus();

            var tempCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

            for (int i = 0; i < _myTable.Rows.Count; i++)
            {
                if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                {
                    totalav += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString());
                }
                if (_myTable.Rows[i].ItemArray[4].ToString() != "")
                {
                    totalzp += Convert.ToDouble(_myTable.Rows[i].ItemArray[4].ToString());
                }
                if (_myTable.Rows[i].ItemArray[6].ToString() != "")
                {
                    totalam += Convert.ToDouble(_myTable.Rows[i].ItemArray[6].ToString());
                }

            }

            Thread.CurrentThread.CurrentCulture = tempCulture;

            totalavzp = totalzp + totalav;
            totaltotal = totalavzp + totalam;

            TextBox5.Text = totalav.ToString(CultureInfo.InvariantCulture);
            TextBox9.Text = totalzp.ToString(CultureInfo.InvariantCulture);
            TextBox10.Text = totalavzp.ToString(CultureInfo.InvariantCulture);
            TextBox11.Text = totalam.ToString(CultureInfo.InvariantCulture);
            TextBox12.Text = totaltotal.ToString(CultureInfo.InvariantCulture);
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
    }
}
