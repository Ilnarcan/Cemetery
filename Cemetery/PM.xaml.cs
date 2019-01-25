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
    /// Логика взаимодействия для PM.xaml
    /// </summary>
    public partial class Pm
    {
        private readonly string _day = "";
        private readonly int _mode;
        private readonly string _month = "";
        private readonly DataTable _myTable = new DataTable();
        private readonly string _year = "";
        private readonly string _period = "";

        public Pm(string y, string m, string d, int md, string per)
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
            Label4.Content = "Памятники " + _period;
            if (!String.IsNullOrEmpty(_year) && !String.IsNullOrEmpty(_month) && !String.IsNullOrEmpty(_day))
            {
                string databaseName = BaseFunctions.Getinput();
                string databaseNamecrypt = BaseFunctions.Getbase();

                string sqlquer = "SELECT ID,cen,name,ych1,ych2,ych3,ych4,r1,r2,i1,i2,i3,i4 FROM PM WHERE ";

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
                dt.Columns.Add();
                dt.Columns.Add();
                dt.Columns.Add();

                foreach (DataRow record in dtmem.Rows)
                {
                    _myTable.Rows.Add(new object[]
                    {
                       (Int64) record["ID"], CryptDec.NewForm(record["cen"].ToString()), 
                       CryptDec.NewForm(record["name"].ToString()), 
                       CryptDec.NewForm(record["ych1"].ToString()),
                       CryptDec.NewForm(record["ych2"].ToString()), 
                       CryptDec.NewForm(record["ych3"].ToString()), 
                       CryptDec.NewForm(record["ych4"].ToString()), 
                       CryptDec.NewForm(record["r1"].ToString()),
                       CryptDec.NewForm(record["r2"].ToString()),
                       CryptDec.NewForm(record["i1"].ToString()),
                       CryptDec.NewForm(record["i2"].ToString()),
                       CryptDec.NewForm(record["i3"].ToString()),
                       CryptDec.NewForm(record["i4"].ToString())
                    });
                    dt.Rows.Add(new object[]
                    {
                       (Int64) record["ID"], CryptDec.NewForm(record["cen"].ToString()), 
                       CryptDec.NewForm(record["name"].ToString()), 
                       CryptDec.NewForm(record["ych1"].ToString()),
                       CryptDec.NewForm(record["ych2"].ToString()), 
                       CryptDec.NewForm(record["ych3"].ToString()), 
                       CryptDec.NewForm(record["ych4"].ToString()), 
                       CryptDec.NewForm(record["r1"].ToString()),
                       CryptDec.NewForm(record["r2"].ToString()),
                       CryptDec.NewForm(record["i1"].ToString()),
                       CryptDec.NewForm(record["i2"].ToString()),
                       CryptDec.NewForm(record["i3"].ToString()),
                       CryptDec.NewForm(record["i4"].ToString())
                    });
                }


                //var dataAdp = new SQLiteDataAdapter(command);

                //dataAdp.Fill(dt);
                //dataAdp.Fill(_myTable);

                connection.Close();

                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);

                dt.Columns[0].ColumnName = "ID";
                dt.Columns[1].ColumnName = "Цена";
                dt.Columns[2].ColumnName = "Наименование";
                dt.Columns[3].ColumnName = "На начало месяца";
                dt.Columns[4].ColumnName = "В том числе по квит захор";
                dt.Columns[5].ColumnName = "Приход";
                dt.Columns[6].ColumnName = "Возврат";
                dt.Columns[7].ColumnName = "Расход по квит захор";
                dt.Columns[8].ColumnName = "Расход по квит гранитн изд";
                dt.Columns[9].ColumnName = "ИТОГО продано";
                dt.Columns[10].ColumnName = "Остаток на тек день";
                dt.Columns[11].ColumnName = "По квит захор";
                dt.Columns[12].ColumnName = "По квит памят";

                DataGrid1.ItemsSource = dt.DefaultView;
                ////DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                TextBox1.Focus();

                var tempCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                Thread.CurrentThread.CurrentCulture = tempCulture;
            }
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var rowView = DataGrid1.SelectedValue as DataRowView;
            if (rowView != null)
            {
                TextBox1.Text = rowView[1].ToString();
                TextBox2.Text = rowView[2].ToString();
                TextBox3.Text = rowView[3].ToString();
                TextBox4.Text = rowView[4].ToString();
                TextBox5.Text = rowView[5].ToString();
                TextBox6.Text = rowView[6].ToString();
                TextBox7.Text = rowView[7].ToString();
                TextBox8.Text = rowView[8].ToString();
                TextBox9.Text = rowView[9].ToString();
                TextBox10.Text = rowView[10].ToString();
                TextBox11.Text = rowView[11].ToString();
                TextBox12.Text = rowView[12].ToString();
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
                var rowItem = DataGrid1.SelectedValue as DataRowView;
                string databaseName = BaseFunctions.Getinput();
                string databaseNamecrypt = BaseFunctions.Getbase();

                if (rowItem != null)
                {
                    int strNum = Convert.ToInt32(rowItem[0].ToString());
                    
                    var nameprD = CryptDec.BackToForm(TextBox1.Text);
                    var numberprD = CryptDec.BackToForm(TextBox2.Text);
                    var sumprD = CryptDec.BackToForm(TextBox3.Text);
                    var namepmD = CryptDec.BackToForm(TextBox4.Text);
                    var numberpmD = CryptDec.BackToForm(TextBox5.Text);
                    var sumpmD = CryptDec.BackToForm(TextBox6.Text);
                    var namesD = CryptDec.BackToForm(TextBox7.Text);
                    var edizD = CryptDec.BackToForm(TextBox8.Text);
                    var numberpsD = CryptDec.BackToForm(TextBox9.Text);
                    var sumsD = CryptDec.BackToForm(TextBox10.Text);
                    var ostD = CryptDec.BackToForm(TextBox11.Text);
                    var i4D = CryptDec.BackToForm(TextBox11.Text);

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
                            "UPDATE PM SET cen = '" + nameprD + "'," +
                            "name = '" + numberprD + "'," +
                            "ych1 = '" + sumprD + "'," +
                            "ych2 = '" + namepmD + "'," +
                            "ych3 = '" + numberpmD + "'," +
                            "ych4 = '" + sumpmD + "'," +
                            "r1 = '" + namesD + "'," +
                            "r2 = '" + edizD + "'," +
                            "i1 = '" + numberpsD + "'," +
                            "i2 = '" + sumsD + "'," +
                            "i3 = '" + ostD + "'," +
                            "i4 = '" + i4D + "' WHERE ID = '" + strNum + "'", connection);
                    command.ExecuteNonQuery();

                    connection.Close();
                    CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                    File.Delete(databaseName);

                    _myTable.Rows[rowView][1] = TextBox1.Text;
                    _myTable.Rows[rowView][2] = TextBox2.Text;
                    _myTable.Rows[rowView][3] = TextBox3.Text;
                    _myTable.Rows[rowView][4] = TextBox4.Text;
                    _myTable.Rows[rowView][5] = TextBox5.Text;
                    _myTable.Rows[rowView][6] = TextBox6.Text;
                    _myTable.Rows[rowView][7] = TextBox7.Text;
                    _myTable.Rows[rowView][8] = TextBox8.Text;
                    _myTable.Rows[rowView][9] = TextBox9.Text;
                    _myTable.Rows[rowView][10] = TextBox10.Text;
                    _myTable.Rows[rowView][11] = TextBox11.Text;
                    _myTable.Rows[rowView][12] = TextBox12.Text;
                }

                _myTable.Columns[0].ColumnName = "ID";
                _myTable.Columns[1].ColumnName = "Цена";
                _myTable.Columns[2].ColumnName = "Наименование";
                _myTable.Columns[3].ColumnName = "На начало месяца";
                _myTable.Columns[4].ColumnName = "В том числе по квит захор";
                _myTable.Columns[5].ColumnName = "Приход";
                _myTable.Columns[6].ColumnName = "Возврат";
                _myTable.Columns[7].ColumnName = "Расход по квит захор";
                _myTable.Columns[8].ColumnName = "Расход по квит гранитн изд";
                _myTable.Columns[9].ColumnName = "ИТОГО продано";
                _myTable.Columns[10].ColumnName = "Остаток на тек день";
                _myTable.Columns[11].ColumnName = "По квит захор";
                _myTable.Columns[12].ColumnName = "По квит памят";

                DataGrid1.ItemsSource = _myTable.DefaultView;
                //DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                var tempCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                Thread.CurrentThread.CurrentCulture = tempCulture;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
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
                    var command = new SQLiteCommand("DELETE FROM PM WHERE ID = '" + strNum + "'", connection);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);

                _myTable.Rows[rowView].Delete();

                _myTable.AcceptChanges();

                _myTable.Columns[0].ColumnName = "ID";
                _myTable.Columns[1].ColumnName = "Цена";
                _myTable.Columns[2].ColumnName = "Наименование";
                _myTable.Columns[3].ColumnName = "На начало месяца";
                _myTable.Columns[4].ColumnName = "В том числе по квит захор";
                _myTable.Columns[5].ColumnName = "Приход";
                _myTable.Columns[6].ColumnName = "Возврат";
                _myTable.Columns[7].ColumnName = "Расход по квит захор";
                _myTable.Columns[8].ColumnName = "Расход по квит гранитн изд";
                _myTable.Columns[9].ColumnName = "ИТОГО продано";
                _myTable.Columns[10].ColumnName = "Остаток на тек день";
                _myTable.Columns[11].ColumnName = "По квит захор";
                _myTable.Columns[12].ColumnName = "По квит памят";

                DataGrid1.ItemsSource = _myTable.DefaultView;
                //DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                var tempCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                Thread.CurrentThread.CurrentCulture = tempCulture;
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            int y = 0;

            var myFlowDocument = new FlowDocument();
            var table1 = new Table();

            const int numberOfColumns = 13;
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
                    table1.Columns[x].Width = new GridLength(60);
                if (x == 4)
                    table1.Columns[x].Width = new GridLength(60);
                if (x == 5)
                    table1.Columns[x].Width = new GridLength(60);
                if (x == 6)
                    table1.Columns[x].Width = new GridLength(60);
                if (x == 7)
                    table1.Columns[x].Width = new GridLength(120);
                if (x == 8)
                    table1.Columns[x].Width = new GridLength(120);
                if (x == 9)
                    table1.Columns[x].Width = new GridLength(60);
                if (x == 10)
                    table1.Columns[x].Width = new GridLength(60);
                if (x == 11)
                    table1.Columns[x].Width = new GridLength(60);
                if (x == 12)
                    table1.Columns[x].Width = new GridLength(60);
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
            currentRow.Cells[0].ColumnSpan = 13;

            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the row.
            currentRow.FontSize = 14;
            currentRow.FontWeight = FontWeights.Bold;

            // Add cells with content to the third row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("                                              Кузьмоловское кладбище"))));
            currentRow.Cells[0].ColumnSpan = 13;

            y = FreestringY(table1, y);

            // Add the third row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the row.
            currentRow.FontSize = 14;
            currentRow.FontWeight = FontWeights.Bold;

            string headerName = "Памятники " + _period;

            // Add cells with content to the third row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(headerName.ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells[0].ColumnSpan = 13;

            y = FreestringY(table1, y);

            // Add the second (header) row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the header row.
            currentRow.FontSize = 12;
            currentRow.FontWeight = FontWeights.Bold;

            // Add cells with content to the second row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("№"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Цена"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Наименование"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("На начало месяца"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("В том числе по квит захор"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Приход"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Возврат"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Расход по квит захор"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Расход по квит гранитн изд"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("ИТОГО продано"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Остаток на тек день"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("По квит захор"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("По квит памят"))));

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
            currentRow.Cells[10].TextAlignment = TextAlignment.Center;
            currentRow.Cells[11].TextAlignment = TextAlignment.Center;
            currentRow.Cells[12].TextAlignment = TextAlignment.Center;

            y = FreestringY(table1, y);

            int count = 1;

            for (int i = 0; i < _myTable.Rows.Count; i++)
            {
                //if (_myTable.Rows[i].ItemArray[7].ToString() != "")
                //{
                //    totalsum += Convert.ToDouble(_myTable.Rows[i].ItemArray[7].ToString().Replace('.', ','));
                //}

                // Add the third row.
                table1.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table1.RowGroups[0].Rows[y];

                var mySolidColorBrush = new SolidColorBrush { Color = Color.FromArgb(255, 240, 240, 240) };

                // Describes the brush's color using RGB values. 
                // Each value has a range of 0-255.

                currentRow.Background = i % 2 == 0 ? mySolidColorBrush : Brushes.White;

                // Global formatting for the row.
                currentRow.FontSize = 10;
                currentRow.FontWeight = FontWeights.Normal;

                // Add cells with content to the third row.
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(count.ToString(CultureInfo.InvariantCulture)))));
                if (_myTable.Rows[i].ItemArray[1].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[1].ToString().Replace('.', ',')))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[1].ToString()))));
                }
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[2].ToString()))));
                if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString().Replace('.', ',')))
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
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[4].ToString().Replace('.', ',')))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[4].ToString()))));
                }
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[5].ToString()))));
                if (_myTable.Rows[i].ItemArray[6].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[6].ToString().Replace('.', ',')))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[6].ToString()))));
                }
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[7].ToString()))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[8].ToString()))));
                if (_myTable.Rows[i].ItemArray[9].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[9].ToString().Replace('.', ',')))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[9].ToString()))));
                }
                if (_myTable.Rows[i].ItemArray[10].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[10].ToString().Replace('.', ',')))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[10].ToString()))));
                }
                if (_myTable.Rows[i].ItemArray[11].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[11].ToString().Replace('.', ',')))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[11].ToString()))));
                }
                if (_myTable.Rows[i].ItemArray[12].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[12].ToString().Replace('.', ',')))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[12].ToString()))));
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
                currentRow.Cells[10].TextAlignment = TextAlignment.Center;
                currentRow.Cells[11].TextAlignment = TextAlignment.Center;
                currentRow.Cells[12].TextAlignment = TextAlignment.Center;

                y++;
                count++;
            }

            //y = FreestringY(table1, y);


            //// Bold the first cell.
            //table1.RowGroups[0].Rows.Add(new TableRow());
            //currentRow = table1.RowGroups[0].Rows[y];
            //y++;

            //// Global formatting for the footer row.
            //currentRow.FontSize = 14;
            //currentRow.FontWeight = FontWeights.Bold;

            //currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            //currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            //currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            //currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            //currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            //currentRow.Cells.Add(new TableCell(new Paragraph(new Run("ИТОГО"))));
            //currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            //currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", totalsum).ToString(CultureInfo.InvariantCulture)))));

            //currentRow.Cells[5].TextAlignment = TextAlignment.Center;
            //currentRow.Cells[7].TextAlignment = TextAlignment.Center;

            y = FreestringY(table1, y);
            y = FreestringY(table1, y);

            // Bold the first cell.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];

            // Global formatting for the footer row.
            currentRow.FontSize = 13;
            currentRow.FontWeight = FontWeights.Bold;

            // Add cells with content to the third row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("                       Директор                                                                     М.Р. Багаутдинов"))));
            currentRow.Cells[0].ColumnSpan = 13;

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
            currentRow.Cells[0].ColumnSpan = 13;

            return y;
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
        private void TextBox5_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem2 || e.Key == Key.Decimal)
            {
                e.Handled = true;
                TextBox5.Text += ",";
                TextBox5.SelectionStart = TextBox5.Text.Length;
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
        private void TextBox6_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem2 || e.Key == Key.Decimal)
            {
                e.Handled = true;
                TextBox6.Text += ",";
                TextBox6.SelectionStart = TextBox6.Text.Length;
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
        private void TextBox9_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem2 || e.Key == Key.Decimal)
            {
                e.Handled = true;
                TextBox9.Text += ",";
                TextBox9.SelectionStart = TextBox9.Text.Length;
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
        private void TextBox10_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem2 || e.Key == Key.Decimal)
            {
                e.Handled = true;
                TextBox10.Text += ",";
                TextBox10.SelectionStart = TextBox10.Text.Length;
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
        private void TextBox11_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem2 || e.Key == Key.Decimal)
            {
                e.Handled = true;
                TextBox11.Text += ",";
                TextBox11.SelectionStart = TextBox11.Text.Length;
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
            string name = TextBox2.Text;
            string r1 = TextBox7.Text;
            string r2 = TextBox8.Text;

            double cen = 0.0;

            double ych1 = 0.0;
            double ych2 = 0.0;
            double ych3 = 0.0;
            double ych4 = 0.0;

            double i1 = 0.0;
            double i2 = 0.0;
            double i3 = 0.0;
            double i4 = 0.0;

            Int64 numberdead = 0;

            if (!String.IsNullOrEmpty(TextBox1.Text))
                cen = Convert.ToDouble(TextBox1.Text);

            if (!String.IsNullOrEmpty(TextBox3.Text))
                ych1 = Convert.ToDouble(TextBox3.Text);
            if (!String.IsNullOrEmpty(TextBox4.Text))
                ych2 = Convert.ToDouble(TextBox3.Text);
            if (!String.IsNullOrEmpty(TextBox5.Text))
                ych3 = Convert.ToDouble(TextBox5.Text);
            if (!String.IsNullOrEmpty(TextBox6.Text))
                ych4 = Convert.ToDouble(TextBox6.Text);

            if (!String.IsNullOrEmpty(TextBox9.Text))
                i1 = Convert.ToDouble(TextBox9.Text);
            if (!String.IsNullOrEmpty(TextBox10.Text))
                i2 = Convert.ToDouble(TextBox10.Text);
            if (!String.IsNullOrEmpty(TextBox11.Text))
                i3 = Convert.ToDouble(TextBox11.Text);
            if (!String.IsNullOrEmpty(TextBox12.Text))
                i4 = Convert.ToDouble(TextBox12.Text);

            var cencr = CryptDec.BackToForm(cen.ToString(CultureInfo.InvariantCulture));
            var namecr = CryptDec.BackToForm(name);

            var ych1cr = CryptDec.BackToForm(ych1.ToString(CultureInfo.InvariantCulture));
            var ych2cr = CryptDec.BackToForm(ych2.ToString(CultureInfo.InvariantCulture));
            var ych3cr = CryptDec.BackToForm(ych3.ToString(CultureInfo.InvariantCulture));
            var ych4cr = CryptDec.BackToForm(ych4.ToString(CultureInfo.InvariantCulture));

            var r1cr = CryptDec.BackToForm(r1);
            var r2cr = CryptDec.BackToForm(r2);

            var i1cr = CryptDec.BackToForm(i1.ToString(CultureInfo.InvariantCulture));
            var i2cr = CryptDec.BackToForm(i2.ToString(CultureInfo.InvariantCulture));
            var i3cr = CryptDec.BackToForm(i3.ToString(CultureInfo.InvariantCulture));
            var i4cr = CryptDec.BackToForm(i4.ToString(CultureInfo.InvariantCulture));

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

            var command2 = new SQLiteCommand("SELECT Count(*) FROM PM", connection);

            dt2.Load(command2.ExecuteReader());
            foreach (DataRow record in dt2.Rows)
            {
                numberdead = (Int64)record["Count(*)"];
            }

            while (countnumberdead >= 1)
            {
                numberdead++;
                var command3 = new SQLiteCommand("SELECT Count(*) FROM PM WHERE ID = '" + numberdead + "'", connection);

                dt3.Load(command3.ExecuteReader());
                foreach (DataRow record2 in dt3.Rows)
                {
                    countnumberdead = (Int64)record2["Count(*)"];
                }

            }
            string sqlquery = "INSERT INTO PM(ID,year,month,day,cen,name,ych1,ych2,ych3,ych4,r1,r2,i1,i2,i3,i4) VALUES ('" +
                              numberdead + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','" + DateTime.Now.Day + "','" + cencr + "','" +
                              namecr + "','" + ych1cr + "','" + ych2cr + "','" + ych3cr + "','" + ych4cr + "','" + r1cr + "','" + r2cr + "','" + i1cr + "','" + i2cr + "','" + i3cr + "','" + i4cr + "')";
            var command = new SQLiteCommand(sqlquery, connection);
            command.ExecuteNonQuery();
            connection.Close();
            CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
            File.Delete(databaseName);

            _myTable.Rows.Add(new object[]
                {
                    numberdead.ToString(CultureInfo.InvariantCulture), cen.ToString(CultureInfo.InvariantCulture),name,ych1.ToString(CultureInfo.InvariantCulture),ych2.ToString(CultureInfo.InvariantCulture),ych3.ToString(CultureInfo.InvariantCulture),ych4.ToString(CultureInfo.InvariantCulture),r1,r2,i1.ToString(CultureInfo.InvariantCulture),i2.ToString(CultureInfo.InvariantCulture),i3.ToString(CultureInfo.InvariantCulture),i4.ToString(CultureInfo.InvariantCulture)
                });

            _myTable.Columns[0].ColumnName = "ID";
            _myTable.Columns[1].ColumnName = "Цена";
            _myTable.Columns[2].ColumnName = "Наименование";
            _myTable.Columns[3].ColumnName = "На начало месяца";
            _myTable.Columns[4].ColumnName = "В том числе по квит захор";
            _myTable.Columns[5].ColumnName = "Приход";
            _myTable.Columns[6].ColumnName = "Возврат";
            _myTable.Columns[7].ColumnName = "Расход по квит захор";
            _myTable.Columns[8].ColumnName = "Расход по квит гранитн изд";
            _myTable.Columns[9].ColumnName = "ИТОГО продано";
            _myTable.Columns[10].ColumnName = "Остаток на тек день";
            _myTable.Columns[11].ColumnName = "По квит захор";
            _myTable.Columns[12].ColumnName = "По квит памят";

            DataGrid1.ItemsSource = _myTable.DefaultView;
            //DataGrid1.Columns[0].Visibility = Visibility.Hidden;

            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox4.Text = "";
            TextBox5.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";
            TextBox8.Text = "";
            TextBox9.Text = "";
            TextBox10.Text = "";
            TextBox11.Text = "";
            TextBox12.Text = "";

            TextBox1.Focus();

            var tempCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentCulture = tempCulture;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BaseFunctions.SaveDataBase();
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

        private void TextBox12_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Oem2 || e.Key == Key.Decimal)
            {
                e.Handled = true;
                TextBox12.Text += ",";
                TextBox12.SelectionStart = TextBox12.Text.Length;
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
