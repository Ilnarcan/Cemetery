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
    /// Логика взаимодействия для ComeSend.xaml
    /// </summary>
    public partial class ComeSend
    {
        private readonly string _day = "";
        private readonly int _mode;
        private readonly string _month = "";
        private readonly DataTable _myTable = new DataTable();
        private readonly string _year = "";
        private readonly string _period = "";

        public ComeSend(string y, string m, string d, int md, string per)
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
            Label4.Content = "Отчет по приходу и расходу " + _period;
            if (!String.IsNullOrEmpty(_year) && !String.IsNullOrEmpty(_month) && !String.IsNullOrEmpty(_day))
            {
                string databaseName = BaseFunctions.Getinput();
                string databaseNamecrypt = BaseFunctions.Getbase();

                string sqlquer = "SELECT ID,date,namepr,numberpr,sumpr,namepm,numberpm,sumpm,names,ediz,numberps,sums,ost FROM ComeSend WHERE ";

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
                       (Int64) record["ID"], CryptDec.NewForm(record["date"].ToString()), 
                       CryptDec.NewForm(record["namepr"].ToString()), 
                       CryptDec.NewForm(record["numberpr"].ToString()),
                       CryptDec.NewForm(record["sumpr"].ToString()), 
                       CryptDec.NewForm(record["namepm"].ToString()), 
                       CryptDec.NewForm(record["numberpm"].ToString()), 
                       CryptDec.NewForm(record["sumpm"].ToString()),
                       CryptDec.NewForm(record["names"].ToString()),
                       CryptDec.NewForm(record["ediz"].ToString()),
                       CryptDec.NewForm(record["numberps"].ToString()),
                       CryptDec.NewForm(record["sums"].ToString()),
                       CryptDec.NewForm(record["ost"].ToString())
                    });
                    dt.Rows.Add(new object[]
                    {
                       (Int64) record["ID"], CryptDec.NewForm(record["date"].ToString()), 
                       CryptDec.NewForm(record["namepr"].ToString()), 
                       CryptDec.NewForm(record["numberpr"].ToString()),
                       CryptDec.NewForm(record["sumpr"].ToString()), 
                       CryptDec.NewForm(record["namepm"].ToString()), 
                       CryptDec.NewForm(record["numberpm"].ToString()), 
                       CryptDec.NewForm(record["sumpm"].ToString()),
                       CryptDec.NewForm(record["names"].ToString()),
                       CryptDec.NewForm(record["ediz"].ToString()),
                       CryptDec.NewForm(record["numberps"].ToString()),
                       CryptDec.NewForm(record["sums"].ToString()),
                       CryptDec.NewForm(record["ost"].ToString())
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
                dt.Columns[3].ColumnName = "Количество";
                dt.Columns[4].ColumnName = "Сумма";
                dt.Columns[5].ColumnName = "Приход памятники";
                dt.Columns[6].ColumnName = "Кoличество";
                dt.Columns[7].ColumnName = "Cумма";
                dt.Columns[8].ColumnName = "Расход наим";
                dt.Columns[9].ColumnName = "Ед изм";
                dt.Columns[10].ColumnName = "Количествo";
                dt.Columns[11].ColumnName = "Суммa";
                dt.Columns[12].ColumnName = "Остаток";

                DataGrid1.ItemsSource = dt.DefaultView;
                ////DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                DatePicker1.Focus();

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
                DatePicker1.Text = rowView[1].ToString();
                TextBox1.Text = rowView[2].ToString();
                TextBox2.Text = rowView[3].ToString();
                TextBox3.Text = rowView[4].ToString();
                TextBox4.Text = rowView[5].ToString();
                TextBox5.Text = rowView[6].ToString();
                TextBox6.Text = rowView[7].ToString();
                TextBox7.Text = rowView[8].ToString();
                TextBox8.Text = rowView[9].ToString();
                TextBox9.Text = rowView[10].ToString();
                TextBox10.Text = rowView[11].ToString();
                TextBox11.Text = rowView[12].ToString();
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
                    string[] mem = (DatePicker1.ToString()).Split(' ');
                    var date = mem[0];
                    int strNum = Convert.ToInt32(rowItem[0].ToString());
                    
                    var dateD = CryptDec.BackToForm(date);
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
                            "UPDATE ComeSend SET date = '" + dateD + "'," +
                            "namepr = '" + nameprD + "'," +
                            "numberpr = '" + numberprD + "'," +
                            "sumpr = '" + sumprD + "'," +
                            "namepm = '" + namepmD + "'," +
                            "numberpm = '" + numberpmD + "'," +
                            "sumpm = '" + sumpmD + "'," +
                            "names = '" + namesD + "'," +
                            "ediz = '" + edizD + "'," +
                            "numberps = '" + numberpsD + "'," +
                            "sums = '" + sumsD + "'," +
                            "ost = '" + ostD + "' WHERE ID = '" + strNum + "'", connection);
                    command.ExecuteNonQuery();

                    connection.Close();
                    CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                    File.Delete(databaseName);

                    _myTable.Rows[rowView][1] = date;
                    _myTable.Rows[rowView][2] = TextBox1.Text;
                    _myTable.Rows[rowView][3] = TextBox2.Text;
                    _myTable.Rows[rowView][4] = TextBox3.Text;
                    _myTable.Rows[rowView][5] = TextBox4.Text;
                    _myTable.Rows[rowView][6] = TextBox5.Text;
                    _myTable.Rows[rowView][7] = TextBox6.Text;
                    _myTable.Rows[rowView][8] = TextBox7.Text;
                    _myTable.Rows[rowView][9] = TextBox8.Text;
                    _myTable.Rows[rowView][10] = TextBox9.Text;
                    _myTable.Rows[rowView][11] = TextBox10.Text;
                    _myTable.Rows[rowView][12] = TextBox11.Text;
                }

                _myTable.Columns[0].ColumnName = "ID";
                _myTable.Columns[1].ColumnName = "Дата";
                _myTable.Columns[2].ColumnName = "Приход рит услуги";
                _myTable.Columns[3].ColumnName = "Количество";
                _myTable.Columns[4].ColumnName = "Сумма";
                _myTable.Columns[5].ColumnName = "Приход памятники";
                _myTable.Columns[6].ColumnName = "Кoличество";
                _myTable.Columns[7].ColumnName = "Cумма";
                _myTable.Columns[8].ColumnName = "Расход наим";
                _myTable.Columns[9].ColumnName = "Ед изм";
                _myTable.Columns[10].ColumnName = "Количествo";
                _myTable.Columns[11].ColumnName = "Суммa";
                _myTable.Columns[12].ColumnName = "Остаток";

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
                    var command = new SQLiteCommand("DELETE FROM ComeSend WHERE ID = '" + strNum + "'", connection);

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
                _myTable.Columns[3].ColumnName = "Количество";
                _myTable.Columns[4].ColumnName = "Сумма";
                _myTable.Columns[5].ColumnName = "Приход памятники";
                _myTable.Columns[6].ColumnName = "Кoличество";
                _myTable.Columns[7].ColumnName = "Cумма";
                _myTable.Columns[8].ColumnName = "Расход наим";
                _myTable.Columns[9].ColumnName = "Ед изм";
                _myTable.Columns[10].ColumnName = "Количествo";
                _myTable.Columns[11].ColumnName = "Суммa";
                _myTable.Columns[12].ColumnName = "Остаток";

                DataGrid1.ItemsSource = _myTable.DefaultView;
                //DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                var tempCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                Thread.CurrentThread.CurrentCulture = tempCulture;
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            string date1 = "";
            string date2 = "";
            double sum11 = 0.0;
            double sum12 = 0.0;
            double sum21 = 0.0;
            double sum22 = 0.0;
            double sum31 = 0.0;
            double sum32 = 0.0;
            double sum41 = 0.0;
            double sum42 = 0.0;

            int y = 0;
            int countone = 0;

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
                    table1.Columns[x].Width = new GridLength(120);
                if (x == 3)
                    table1.Columns[x].Width = new GridLength(48);
                if (x == 4)
                    table1.Columns[x].Width = new GridLength(100);
                if (x == 5)
                    table1.Columns[x].Width = new GridLength(120);
                if (x == 6)
                    table1.Columns[x].Width = new GridLength(48);
                if (x == 7)
                    table1.Columns[x].Width = new GridLength(90);
                if (x == 8)
                    table1.Columns[x].Width = new GridLength(120);
                if (x == 9)
                    table1.Columns[x].Width = new GridLength(40);
                if (x == 10)
                    table1.Columns[x].Width = new GridLength(48);
                if (x == 11)
                    table1.Columns[x].Width = new GridLength(90);
                if (x == 12)
                    table1.Columns[x].Width = new GridLength(90);
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

            string headerName = "Отчет по приходу и расходу " + _period;

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
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Дата"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Приход рит услуги"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Кол-во"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Сумма"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Приход памятники"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Кол-во"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Сумма"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Расход услуги"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Ед.изм."))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Кол-во"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Сумма"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Остаток"))));

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
                if (_myTable.Rows[i].ItemArray[1].ToString() != "")
                {
                    date1 = _myTable.Rows[i].ItemArray[1].ToString();
                    if (date2 == "")
                        date2 = date1;

                    if (date1 == date2)
                    {
                        if (_myTable.Rows[i].ItemArray[4].ToString() != "")
                        {
                            sum11 += Convert.ToDouble(_myTable.Rows[i].ItemArray[4].ToString().Replace('.', ','));
                        }
                        if (_myTable.Rows[i].ItemArray[7].ToString() != "")
                        {
                            sum21 += Convert.ToDouble(_myTable.Rows[i].ItemArray[7].ToString().Replace('.', ','));
                        }
                        if (_myTable.Rows[i].ItemArray[11].ToString() != "")
                        {
                            sum31 += Convert.ToDouble(_myTable.Rows[i].ItemArray[11].ToString().Replace('.', ','));
                        }
                        if (_myTable.Rows[i].ItemArray[12].ToString() != "")
                        {
                            sum41 += Convert.ToDouble(_myTable.Rows[i].ItemArray[12].ToString().Replace('.', ','));
                        }
                    }
                    else
                    {

                        sum12 = sum12 + sum11;
                        sum22 = sum22 + sum21;
                        sum32 = sum32 + sum31;
                        sum42 = sum42 + sum41;
                        y = FreestringY(table1, y);

                        // Add the third row.
                        table1.RowGroups[0].Rows.Add(new TableRow());
                        currentRow = table1.RowGroups[0].Rows[y];
                        y++;

                        // Global formatting for the row.
                        currentRow.FontSize = 18;
                        currentRow.FontWeight = FontWeights.Bold;

                        // Add cells with content to the third row.
                        currentRow.Cells.Add(
                            new TableCell(new Paragraph(new Run(count.ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(date2))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run("ИТОГО за день:"))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum11.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum21.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum31.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum41.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));

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

                        count++;

                        // Add the third row.
                        table1.RowGroups[0].Rows.Add(new TableRow());
                        currentRow = table1.RowGroups[0].Rows[y];
                        y++;

                        // Global formatting for the row.
                        currentRow.FontSize = 18;
                        currentRow.FontWeight = FontWeights.Bold;

                        // Add cells with content to the third row.
                        currentRow.Cells.Add(
                            new TableCell(new Paragraph(new Run(count.ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum12.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum22.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum32.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum42.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));

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

                        count++;

                        sum11 = 0.0;
                        sum21 = 0.0;
                        sum31 = 0.0;
                        sum41 = 0.0;
                        if (_myTable.Rows[i].ItemArray[4].ToString() != "")
                        {
                            sum11 += Convert.ToDouble(_myTable.Rows[i].ItemArray[4].ToString().Replace('.', ','));
                        }
                        if (_myTable.Rows[i].ItemArray[7].ToString() != "")
                        {
                            sum21 += Convert.ToDouble(_myTable.Rows[i].ItemArray[7].ToString().Replace('.', ','));
                        }
                        if (_myTable.Rows[i].ItemArray[11].ToString() != "")
                        {
                            sum31 += Convert.ToDouble(_myTable.Rows[i].ItemArray[11].ToString().Replace('.', ','));
                        }
                        if (_myTable.Rows[i].ItemArray[12].ToString() != "")
                        {
                            sum41 += Convert.ToDouble(_myTable.Rows[i].ItemArray[12].ToString().Replace('.', ','));
                        }

                        y = FreestringY(table1, y);
                        y = FreestringY(table1, y);


                    }
                    date2 = _myTable.Rows[i].ItemArray[1].ToString();
                }
                if (_myTable.Rows[i].ItemArray[1].ToString() == "")
                {
                    if (countone == 0)
                    {
                        sum12 = sum12 + sum11;
                        sum22 = sum22 + sum21;
                        sum32 = sum32 + sum31;
                        sum42 = sum42 + sum41;
                        y = FreestringY(table1, y);

                        // Add the third row.
                        table1.RowGroups[0].Rows.Add(new TableRow());
                        currentRow = table1.RowGroups[0].Rows[y];
                        y++;

                        // Global formatting for the row.
                        currentRow.FontSize = 18;
                        currentRow.FontWeight = FontWeights.Bold;

                        // Add cells with content to the third row.
                        currentRow.Cells.Add(
                            new TableCell(new Paragraph(new Run(count.ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(date2))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run("ИТОГО за день:"))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum11.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum21.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum31.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum41.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));

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

                        count++;

                        // Add the third row.
                        table1.RowGroups[0].Rows.Add(new TableRow());
                        currentRow = table1.RowGroups[0].Rows[y];
                        y++;

                        // Global formatting for the row.
                        currentRow.FontSize = 18;
                        currentRow.FontWeight = FontWeights.Bold;

                        // Add cells with content to the third row.
                        currentRow.Cells.Add(
                            new TableCell(new Paragraph(new Run(count.ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum12.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum22.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum32.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));
                        currentRow.Cells.Add(
                            new TableCell(
                                new Paragraph(
                                    new Run(
                                        String.Format("{0:0.00}",
                                            Convert.ToDouble(sum42.ToString(CultureInfo.InvariantCulture)
                                                .Replace('.', ','))).ToString(CultureInfo.InvariantCulture)))));

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

                        count++;

                        y = FreestringY(table1, y);
                        countone = 1;
                    }

                }


                // Add the third row.
                table1.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table1.RowGroups[0].Rows[y];

                var mySolidColorBrush = new SolidColorBrush { Color = Color.FromArgb(255, 240, 240, 240) };

                // Describes the brush's color using RGB values. 
                // Each value has a range of 0-255.

                currentRow.Background = i % 2 == 0 ? mySolidColorBrush : Brushes.White;

                // Global formatting for the row.
                currentRow.FontSize = 16;
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
                if (_myTable.Rows[i].ItemArray[7].ToString() != "")
                {
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[7].ToString().Replace('.', ',')))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[7].ToString()))));
                }
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[8].ToString()))));
                currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[9].ToString()))));
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
            string date = DatePicker1.ToString();
            string namepr = TextBox1.Text;
            double numberpr = 0.0;
            double sumpr = 0.0;
            string namepm = TextBox4.Text;
            double numberpm = 0.0;
            double sumpm = 0.0;
            string names = TextBox7.Text;
            string ediz = TextBox8.Text;
            double numberps = 0.0;
            double sums = 0.0;
            double ost = 0.0;
            Int64 numberdead = 0;
            int overcount = 0;


            string[] mem = date.Split(' ');
            date = mem[0];


            if (!String.IsNullOrEmpty(TextBox2.Text))
                numberpr = Convert.ToDouble(TextBox2.Text);
            if (!String.IsNullOrEmpty(TextBox3.Text))
                sumpr = Convert.ToDouble(TextBox3.Text);

            if (!String.IsNullOrEmpty(TextBox5.Text))
                numberpm = Convert.ToDouble(TextBox5.Text);
            if (!String.IsNullOrEmpty(TextBox6.Text))
                sumpm = Convert.ToDouble(TextBox6.Text);

            if (!String.IsNullOrEmpty(TextBox9.Text))
                numberps = Convert.ToDouble(TextBox9.Text);
            if (!String.IsNullOrEmpty(TextBox10.Text))
                sums = Convert.ToDouble(TextBox10.Text);

            if (!String.IsNullOrEmpty(TextBox11.Text))
                ost = Convert.ToDouble(TextBox11.Text);

            var datecr = CryptDec.BackToForm(date);
            var nameprcr = CryptDec.BackToForm(namepr);
            var numberprcr = CryptDec.BackToForm(numberpr.ToString(CultureInfo.InvariantCulture));
            var sumprcr = CryptDec.BackToForm(sumpr.ToString(CultureInfo.InvariantCulture));
            var namepmcr = CryptDec.BackToForm(namepm);
            var numberpmcr = CryptDec.BackToForm(numberpm.ToString(CultureInfo.InvariantCulture));
            var sumpmcr = CryptDec.BackToForm(sumpm.ToString(CultureInfo.InvariantCulture));
            var namescr = CryptDec.BackToForm(names);
            var edizcr = CryptDec.BackToForm(ediz);
            var numberpscr = CryptDec.BackToForm(numberps.ToString(CultureInfo.InvariantCulture));
            var sumscr = CryptDec.BackToForm(sums.ToString(CultureInfo.InvariantCulture));
            var ostcr = CryptDec.BackToForm(ost.ToString(CultureInfo.InvariantCulture));

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

            var command2 = new SQLiteCommand("SELECT Count(*) FROM ComeSend", connection);

            dt2.Load(command2.ExecuteReader());
            foreach (DataRow record in dt2.Rows)
            {
                numberdead = (Int64)record["Count(*)"];
            }

            while (countnumberdead >= 1)
            {
                numberdead++;
                var command3 = new SQLiteCommand("SELECT Count(*) FROM ComeSend WHERE ID = '" + numberdead + "'", connection);

                dt3.Load(command3.ExecuteReader());
                foreach (DataRow record2 in dt3.Rows)
                {
                    countnumberdead = (Int64) record2["Count(*)"];
                }

            }
            string sqlquery = "INSERT INTO ComeSend(ID,year,month,day,date,namepr,numberpr,sumpr,namepm,numberpm,sumpm,names,ediz,numberps,sums,ost) VALUES ('" +
                              numberdead + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','" + DateTime.Now.Day + "','" + datecr + "','" +
                              nameprcr + "','" + numberprcr + "','" + sumprcr + "','" + namepmcr + "','" + numberpmcr + "','" +sumpmcr + "','" +namescr + "','" +edizcr + "','" +numberpscr + "','" +sumscr + "','" +ostcr + "')";
            var command = new SQLiteCommand(sqlquery, connection);
            command.ExecuteNonQuery();
            connection.Close();
            CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
            File.Delete(databaseName);

            _myTable.Rows.Add(new object[]
                {
                    numberdead.ToString(CultureInfo.InvariantCulture), date,namepr,numberpr.ToString(CultureInfo.InvariantCulture),sumpr.ToString(CultureInfo.InvariantCulture),namepm,numberpm.ToString(CultureInfo.InvariantCulture),sumpm.ToString(CultureInfo.InvariantCulture),names,ediz,numberps.ToString(CultureInfo.InvariantCulture),sums.ToString(CultureInfo.InvariantCulture),ost.ToString(CultureInfo.InvariantCulture)
                });

            _myTable.Columns[0].ColumnName = "ID";
            _myTable.Columns[1].ColumnName = "Дата";
            _myTable.Columns[2].ColumnName = "Приход рит услуги";
            _myTable.Columns[3].ColumnName = "Количество";
            _myTable.Columns[4].ColumnName = "Сумма";
            _myTable.Columns[5].ColumnName = "Приход памятники";
            _myTable.Columns[6].ColumnName = "Кoличество";
            _myTable.Columns[7].ColumnName = "Cумма";
            _myTable.Columns[8].ColumnName = "Расход наим";
            _myTable.Columns[9].ColumnName = "Ед изм";
            _myTable.Columns[10].ColumnName = "Количествo";
            _myTable.Columns[11].ColumnName = "Суммa";
            _myTable.Columns[12].ColumnName = "Остаток";


            DataGrid1.ItemsSource = _myTable.DefaultView;
            //DataGrid1.Columns[0].Visibility = Visibility.Hidden;

            DatePicker1.Text = "";
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

            DatePicker1.Focus();

            var tempCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentCulture = tempCulture;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BaseFunctions.SaveDataBase();
        }
    }
}
