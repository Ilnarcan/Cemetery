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
    ///     Логика взаимодействия для EditAwardMasters.xaml
    /// </summary>
    public partial class EditAwardMasters
    {
        private readonly string _day = "";
        private readonly int _mode;
        private readonly string _month = "";
        private readonly DataTable _myTable = new DataTable();
        private readonly string _year = "";
        private readonly string _period = "";

        public EditAwardMasters(string y, string m, string d, int md, string per)
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

            Label4.Content = "Премия мастерам " + _period;

            if (!String.IsNullOrEmpty(_year) && !String.IsNullOrEmpty(_month) && !String.IsNullOrEmpty(_day))
            {
                string databaseName = BaseFunctions.Getinput();
                string databaseNamecrypt = BaseFunctions.Getbase();

                double sum30 = 0;
                double sum100 = 0;

                string sqlquer = "SELECT ID,team,master,total100,total30,totalPay FROM AM WHERE ";

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
                       (Int64) record["ID"], CryptDec.NewForm(record["team"].ToString()), 
                       CryptDec.NewForm(record["master"].ToString()), 
                       CryptDec.NewForm(record["total100"].ToString()), 
                       CryptDec.NewForm(record["total30"].ToString()), 
                       CryptDec.NewForm(record["totalPay"].ToString())
                    });
                    dt.Rows.Add(new object[]
                    {
                       (Int64) record["ID"], CryptDec.NewForm(record["team"].ToString()), 
                       CryptDec.NewForm(record["master"].ToString()), 
                       CryptDec.NewForm(record["total100"].ToString()), 
                       CryptDec.NewForm(record["total30"].ToString()), 
                       CryptDec.NewForm(record["totalPay"].ToString())
                    });
                }

                //var dataAdp = new SQLiteDataAdapter(command);

                //dataAdp.Fill(dt);
                //dataAdp.Fill(_myTable);

                //dt = _myTable;

                connection.Close();

                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);
                dt.Columns[0].ColumnName = "ID";
                dt.Columns[1].ColumnName = "Бригада №";
                dt.Columns[2].ColumnName = "ФИО";
                dt.Columns[3].ColumnName = "Сумма руб 100%";
                dt.Columns[4].ColumnName = "Сумма руб 30%";
                dt.Columns[5].ColumnName = "Сумма выплаты руб";
                DataGrid1.ItemsSource = dt.DefaultView;
                ////DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                TextBox1.Focus();
                TextBox1.SelectionStart = TextBox1.Text.Length;

                var tempCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
                for (int i = 0; i < _myTable.Rows.Count; i++)
                    if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                    {
                        sum100 += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString());
                        sum30 += Convert.ToDouble(_myTable.Rows[i].ItemArray[5].ToString());
                    }

                Thread.CurrentThread.CurrentCulture = tempCulture;
                TextBox4.Text = sum30.ToString(CultureInfo.InvariantCulture);
                TextBox5.Text = sum100.ToString(CultureInfo.InvariantCulture);
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
            }
            
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            double sum30 = 0;
            double sum100 = 0;

            double total30 = 0.0;

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
                    var teamd = CryptDec.BackToForm(TextBox1.Text);
                    var masterd = CryptDec.BackToForm(TextBox2.Text);
                    var total100D = CryptDec.BackToForm(TextBox3.Text);

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
                    if (TextBox3.Text != "")
                    {
                        total30 = double.Parse(TextBox3.Text) * 0.3;
                    }
                    var total30D = CryptDec.BackToForm(total30.ToString(CultureInfo.InvariantCulture));
                    var connection = new SQLiteConnection(string.Format("Data Source={0};", databaseName));

                    connection.Open();
                    var command =
                        new SQLiteCommand(
                            "UPDATE AM SET team = '" + teamd + "'," +
                            "master = '" + masterd + "'," +
                            "total100 = '" + total100D + "'," +
                            "total30 = '" + total30D + "'," +
                            "totalPay = '" + total30D + "' WHERE ID = '" + strNum + "'", connection);
                    command.ExecuteNonQuery();

                    connection.Close();
                    CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                    File.Delete(databaseName);

                    _myTable.Rows[rowView][1] = TextBox1.Text;
                    _myTable.Rows[rowView][2] = TextBox2.Text;
                    _myTable.Rows[rowView][3] = TextBox3.Text;
                    if (TextBox3.Text != "")
                    {
                        _myTable.Rows[rowView][4] = double.Parse(TextBox3.Text) * 0.3;
                        _myTable.Rows[rowView][5] = double.Parse(TextBox3.Text) * 0.3;
                    }
                }

                _myTable.Columns[0].ColumnName = "ID";
                _myTable.Columns[1].ColumnName = "Бригада №";
                _myTable.Columns[2].ColumnName = "ФИО";
                _myTable.Columns[3].ColumnName = "Сумма руб 100%";
                _myTable.Columns[4].ColumnName = "Сумма руб 30%";
                _myTable.Columns[5].ColumnName = "Сумма выплаты руб";

                DataGrid1.ItemsSource = _myTable.DefaultView;
                //DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                var tempCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

                for (int i = 0; i < _myTable.Rows.Count; i++)
                    if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                    {
                        sum100 += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString());
                        sum30 += Convert.ToDouble(_myTable.Rows[i].ItemArray[5].ToString());
                    }

                Thread.CurrentThread.CurrentCulture = tempCulture;

                TextBox4.Text = sum30.ToString(CultureInfo.InvariantCulture);
                TextBox5.Text = sum100.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            double sum30 = 0;
            double sum100 = 0;

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
                    var command = new SQLiteCommand("DELETE FROM AM WHERE ID = '" + strNum + "'", connection);

                    command.ExecuteNonQuery();

                    connection.Close();
                }
                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);

                _myTable.Rows[rowView].Delete();

                _myTable.AcceptChanges();

                _myTable.Columns[0].ColumnName = "ID";
                _myTable.Columns[1].ColumnName = "Бригада №";
                _myTable.Columns[2].ColumnName = "ФИО";
                _myTable.Columns[3].ColumnName = "Сумма руб 100%";
                _myTable.Columns[4].ColumnName = "Сумма руб 30%";
                _myTable.Columns[5].ColumnName = "Сумма выплаты руб";

                DataGrid1.ItemsSource = _myTable.DefaultView;
                //DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                var tempCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

                for (int i = 0; i < _myTable.Rows.Count; i++)
                    if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                    {
                        sum100 += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString());
                        sum30 += Convert.ToDouble(_myTable.Rows[i].ItemArray[5].ToString());
                    }

                Thread.CurrentThread.CurrentCulture = tempCulture;

                TextBox4.Text = sum30.ToString(CultureInfo.InvariantCulture);
                TextBox5.Text = sum100.ToString(CultureInfo.InvariantCulture);

            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            double sum30 = 0;
            double sum100 = 0;

            int y = 0;

            var myFlowDocument = new FlowDocument();
            var table1 = new Table();

            const int numberOfColumns = 6;
            for (int x = 0; x < numberOfColumns; x++)
            {
                table1.Columns.Add(new TableColumn());

                // Set alternating background colors for the middle colums. 

                table1.Columns[x].Background = Brushes.White;

                if (x == 0)
                    table1.Columns[x].Width = new GridLength(30);
                if (x == 1)
                    table1.Columns[x].Width = new GridLength(120);
                if (x == 2)
                    table1.Columns[x].Width = new GridLength(200);
                if (x == 3 || x == 4 || x == 5)
                    table1.Columns[x].Width = new GridLength(140);
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
            currentRow.Cells[0].ColumnSpan = 6;

            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the row.
            currentRow.FontSize = 14;
            currentRow.FontWeight = FontWeights.Bold;

            // Add cells with content to the third row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("                                              Кузьмоловское кладбище"))));
            currentRow.Cells[0].ColumnSpan = 6;

            y = FreeStringY(table1, y);

            // Add the third row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the row.
            currentRow.FontSize = 14;
            currentRow.FontWeight = FontWeights.Bold;

            string headerName = "   Премия мастерам " + _period;

            // Add cells with content to the third row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(headerName.ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells[0].ColumnSpan = 6;

            y = FreeStringY(table1, y);

            // Add the second (header) row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the header row.
            currentRow.FontSize = 18;
            currentRow.FontWeight = FontWeights.Bold;

            // Add cells with content to the second row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("№"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Бригада"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("ФИО"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Сумма 100%"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Сумма 30%"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("Выплата"))));

            currentRow.Cells[0].TextAlignment = TextAlignment.Center;
            currentRow.Cells[1].TextAlignment = TextAlignment.Center;
            currentRow.Cells[2].TextAlignment = TextAlignment.Center;
            currentRow.Cells[3].TextAlignment = TextAlignment.Center;
            currentRow.Cells[4].TextAlignment = TextAlignment.Center;
            currentRow.Cells[5].TextAlignment = TextAlignment.Center;

            y = FreeStringY(table1, y);

            int count = 1;

            for (int i = 0; i < _myTable.Rows.Count; i++)
            {
                if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                {
                    sum30 += Convert.ToDouble(_myTable.Rows[i].ItemArray[5].ToString().Replace('.',','));
                    sum100 += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString().Replace('.', ','));
                }

                // Add the third row.
                table1.RowGroups[0].Rows.Add(new TableRow());
                currentRow = table1.RowGroups[0].Rows[y];

                var mySolidColorBrush = new SolidColorBrush {Color = Color.FromArgb(255, 240, 240, 240)};

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
                                    String.Format("{0:0.00}", Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString().Replace('.', ',')))
                                        .ToString(CultureInfo.InvariantCulture)))));
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble((_myTable.Rows[i].ItemArray[4]).ToString().Replace('.',',')))
                                        .ToString(CultureInfo.InvariantCulture)))));
                    currentRow.Cells.Add(
                        new TableCell(
                            new Paragraph(
                                new Run(
                                    String.Format("{0:0.00}", Convert.ToDouble((_myTable.Rows[i].ItemArray[5]).ToString().Replace('.', ',')))
                                        .ToString(CultureInfo.InvariantCulture)))));
                }
                else
                {
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[3].ToString()))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[4].ToString()))));
                    currentRow.Cells.Add(new TableCell(new Paragraph(new Run(_myTable.Rows[i].ItemArray[5].ToString()))));
                }
                currentRow.Cells[0].TextAlignment = TextAlignment.Center;
                currentRow.Cells[1].TextAlignment = TextAlignment.Center;
                currentRow.Cells[2].TextAlignment = TextAlignment.Center;
                currentRow.Cells[3].TextAlignment = TextAlignment.Center;
                currentRow.Cells[4].TextAlignment = TextAlignment.Center;
                currentRow.Cells[5].TextAlignment = TextAlignment.Center;

                y++;
                count++;
            }

            y = FreeStringY(table1, y);

            // Bold the first cell.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the footer row.
            currentRow.FontSize = 14;
            currentRow.FontWeight = FontWeights.Bold;

            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("ИТОГО"))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", sum100).ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", sum30).ToString(CultureInfo.InvariantCulture)))));
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(String.Format("{0:0.00}", sum30).ToString(CultureInfo.InvariantCulture)))));
            // and set the row to span all 6 columns.

            currentRow.Cells[1].TextAlignment = TextAlignment.Center;
            currentRow.Cells[3].TextAlignment = TextAlignment.Center;
            currentRow.Cells[4].TextAlignment = TextAlignment.Center;
            currentRow.Cells[5].TextAlignment = TextAlignment.Center;

            y = FreeStringY(table1, y);
            y = FreeStringY(table1, y);

            // Bold the first cell.
            table1.RowGroups[0].Rows.Add(new TableRow());
            currentRow = table1.RowGroups[0].Rows[y];

            // Global formatting for the footer row.
            currentRow.FontSize = 14;
            currentRow.FontWeight = FontWeights.Bold;

            // Add cells with content to the third row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run("                       Директор                                                                     М.Р. Багаутдинов"))));
            currentRow.Cells[0].ColumnSpan = 6;

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

                myFlowDocument.ColumnGap = 0;

                myFlowDocument.ColumnWidth = (myFlowDocument.PageWidth -
                                              myFlowDocument.ColumnGap -
                                              myFlowDocument.PagePadding.Left -
                                              myFlowDocument.PagePadding.Right);
                const int margin = 5;
                var pageSize = new Size(dialog.PrintableAreaWidth - margin*2, dialog.PrintableAreaHeight - margin*2);
                var paginator = myFlowDocument as IDocumentPaginatorSource;
                paginator.DocumentPaginator.PageSize = pageSize;
                dialog.PrintDocument(paginator.DocumentPaginator, "Flow print");
            }
        }

        private static int FreeStringY(Table table1, int y)
        {
            // Add the third row.
            table1.RowGroups[0].Rows.Add(new TableRow());
            TableRow currentRow = table1.RowGroups[0].Rows[y];
            y++;

            // Global formatting for the row.
            currentRow.FontSize = 14;
            currentRow.FontWeight = FontWeights.Normal;

            // Add cells with content to the third row.
            currentRow.Cells.Add(new TableCell(new Paragraph(new Run(""))));
            currentRow.Cells[0].ColumnSpan = 6;

            return y;
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox1.Text != "" && TextBox2.Text != "" && TextBox3.Text != "")
            {
                string team = TextBox1.Text;
                string master = TextBox2.Text;
                double total100 = 0.0;
                double total30 = 0.0;
                double sum30 = 0;
                double sum100 = 0;

                var teamcr = CryptDec.BackToForm(team);

                var mastercr = CryptDec.BackToForm(master);
                if (!String.IsNullOrEmpty(TextBox3.Text))
                {
                    total100 = Convert.ToDouble(TextBox3.Text);
                    total30 = Convert.ToDouble(total100*0.3);
                }

                var total100Cr = CryptDec.BackToForm(total100.ToString(CultureInfo.InvariantCulture));
                var total30Cr = CryptDec.BackToForm(total30.ToString(CultureInfo.InvariantCulture));

                string databaseName = BaseFunctions.Getinput();
                string databaseNamecrypt = BaseFunctions.Getbase();
                string sqlquery;
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

                var dt2 = new DataTable();
                var dt3 = new DataTable();

                Int64 countnumberdead = 1;

                var command2 = new SQLiteCommand("SELECT Count(*) FROM AM", connection);

                dt2.Load(command2.ExecuteReader());
                foreach (DataRow record in dt2.Rows)
                {
                    numberdead = (Int64)record["Count(*)"];
                }

                while (countnumberdead >= 1)
                {
                    numberdead++;
                    var command3 = new SQLiteCommand("SELECT Count(*) FROM AM WHERE ID = '" + numberdead + "'", connection);

                    dt3.Load(command3.ExecuteReader());
                    foreach (DataRow record2 in dt3.Rows)
                    {
                        countnumberdead = (Int64)record2["Count(*)"];
                    }

                }

                if (!String.IsNullOrEmpty(TextBox3.Text))
                {
                    sqlquery =
                        "INSERT INTO AM(ID,year,month,day,team,master,total100,total30,totalPay) VALUES ('" +
                        numberdead + "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','" + DateTime.Now.Day + "','" + teamcr +
                        "','" +
                        mastercr + "','" + total100Cr + "','" + total30Cr + "','" + total30Cr + "')";
                }
                else
                {
                    sqlquery = "INSERT INTO AM(ID,year,month,day,team,master) VALUES ('" + numberdead +
                               "','" + DateTime.Now.Year + "','" + DateTime.Now.Month + "','" + DateTime.Now.Day + "','" + teamcr + "','" + mastercr +
                               "')";
                }
                var command = new SQLiteCommand(sqlquery, connection);
                command.ExecuteNonQuery();


                connection.Close();
                CryptDec.CryptFile(databaseName, databaseNamecrypt, new DESCryptoServiceProvider());
                File.Delete(databaseName);

                if (!String.IsNullOrEmpty(TextBox3.Text))
                {
                    // ReSharper disable CoVariantArrayConversion
                    _myTable.Rows.Add(new[]
                    {
                        numberdead.ToString(CultureInfo.InvariantCulture), team, master,
                        total100.ToString(CultureInfo.InvariantCulture), total30.ToString(CultureInfo.InvariantCulture),
                        total30.ToString(CultureInfo.InvariantCulture)
                    });
                    // ReSharper restore CoVariantArrayConversion
                }
                else
                {
                    // ReSharper disable CoVariantArrayConversion
                    _myTable.Rows.Add(new[] { numberdead.ToString(CultureInfo.InvariantCulture), team, master, "", "", "" });
                    // ReSharper restore CoVariantArrayConversion
                }

                _myTable.Columns[0].ColumnName = "ID";
                _myTable.Columns[1].ColumnName = "Бригада №";
                _myTable.Columns[2].ColumnName = "ФИО";
                _myTable.Columns[3].ColumnName = "Сумма руб 100%";
                _myTable.Columns[4].ColumnName = "Сумма руб 30%";
                _myTable.Columns[5].ColumnName = "Сумма выплаты руб";

                DataGrid1.ItemsSource = _myTable.DefaultView;
                //DataGrid1.Columns[0].Visibility = Visibility.Hidden;

                TextBox1.Text = "";
                TextBox2.Text = "";
                TextBox3.Text = "";

                TextBox1.Focus();
                TextBox1.SelectionStart = TextBox1.Text.Length;

                var tempCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");

                for (int i = 0; i < _myTable.Rows.Count; i++)
                    if (_myTable.Rows[i].ItemArray[3].ToString() != "")
                    {
                        sum100 += Convert.ToDouble(_myTable.Rows[i].ItemArray[3].ToString());
                        sum30 += Convert.ToDouble(_myTable.Rows[i].ItemArray[5].ToString());
                    }

                Thread.CurrentThread.CurrentCulture = tempCulture;

                TextBox4.Text = sum30.ToString(CultureInfo.InvariantCulture);
                TextBox5.Text = sum100.ToString(CultureInfo.InvariantCulture);
            }
            else
            {
                Message("Заполните все поля!");
            }
        }

        private void TextBox3_KeyDown_1(object sender, KeyEventArgs e)
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
                e.Key == Key.D7 || e.Key == Key.D8 || e.Key == Key.D9)
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            BaseFunctions.SaveDataBase();
        }
    }
}