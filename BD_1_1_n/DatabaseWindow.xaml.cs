using Microsoft.Reporting.WinForms;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using static System.Collections.Specialized.BitVector32;
using System.Windows.Controls.Primitives;

namespace BD_1_1_n
{
    /// <summary>
    /// Логика взаимодействия для DatabaseWindow.xaml
    /// </summary>
    public partial class DatabaseWindow : Window
    {
        private DatabaseHelper dbHelper;
       
        public DatabaseWindow(DatabaseHelper dbHelper, string u) //DatabaseHelper dbHelper
        {
            InitializeComponent();
            this.dbHelper = dbHelper;    
            
            if(u == "vova" )
            {
                Map22.Visibility = Visibility.Collapsed;
            }
        }

        //звіт 721
        

        private void Close_R721(object sender, RoutedEventArgs e)
        {
            r_721.Visibility = Visibility.Collapsed;
            FieldsPanelZ721.Visibility = Visibility.Collapsed;
            MyReportViewer721.Clear();
        }

        private void Button_Click_Z_721(object sender, RoutedEventArgs e)
        {
            r_721.Visibility = Visibility.Visible;
            FieldsPanelZ721.Visibility = Visibility.Visible;

            DataTable stationData = GetStationData();
            StationComboBox721.ItemsSource = stationData.AsEnumerable().Select(r => r.Field<string>("Name_Station")).ToList();
        }

        string selectedStationCode721 = "0001";
        string selectedStationName721 = " ";

        private void StationComboBox_SelectionChanged721(object sender, SelectionChangedEventArgs e)
        {
            if (StationComboBox721.SelectedItem != null)
            {
                selectedStationName721 = StationComboBox721.SelectedItem.ToString();
                selectedStationCode721 = GetStationCodeByName(selectedStationName721);
            }
        }

        private void Button_Click_721(object sender, RoutedEventArgs e)
        {
            //MyReportViewer721.Clear();


            DataSet MyDataSet = new DataSet();
            MyDataSet.Clear();
            string sSql1 = $"SELECT C.Designation AS \"Назва_категорії\",     " +
                $" COUNT(*) AS \"Кількість_перевищень\" " +
                $"FROM Measurment M " +
                $"INNER JOIN Optimal_Value OV ON M.ID_Measured_Unit = OV.ID_Measured_Unit " +
                $"INNER JOIN Category C ON OV.ID_Category = C.ID_Category " +
                $"INNER JOIN Measured_Unit U ON M.ID_Measured_Unit = U.ID_Measured_Unit " +
                $"WHERE   M.ID_Station = '{selectedStationCode721}' " +
                $"AND     U.Title = 'PM2.5'   AND M.Date_Time BETWEEN DATE_TRUNC('day', M.Date_Time) " +
                $"AND DATE_TRUNC('day', M.Date_Time) + INTERVAL '1 day'   " +
                $"AND M.Value_ > OV.Bottom_Border   AND C.ID_Category IN ('4', '5', '6') " +
                $"GROUP BY C.Designation;";
            using (NpgsqlCommand npgsqlcmd = new NpgsqlCommand(sSql1, dbHelper.GetConnection())) //dbHelper.GetConnection()
            {
                try
                {
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(npgsqlcmd);
                    da.Fill(MyDataSet);
                }
                catch (Exception exMessage)
                {
                    MessageBox.Show(exMessage.Message);
                }
            }

            MyReportViewer721.ProcessingMode = ProcessingMode.Local;
            MyReportViewer721.LocalReport.ReportPath = @"E:\Моє\моя практика\repos_C\BD_1_1_n\BD_1_1_n\Report7.rdlc";

            MyReportViewer721.LocalReport.SetParameters(new ReportParameter("Parameter8", selectedStationName721));


            MyReportViewer721.LocalReport.DataSources.Add(new ReportDataSource("DataSet9", MyDataSet.Tables[0]));
            MyReportViewer721.SetDisplayMode(DisplayMode.PrintLayout);
            MyReportViewer721.RefreshReport();

        }


        //звіт 74
        private void Close_R74(object sender, RoutedEventArgs e)
        {
            r_74.Visibility = Visibility.Collapsed;
            FieldsPanelZ74.Visibility = Visibility.Collapsed;
            MyReportViewer74.Clear();
        }

        private void Button_Click_Z_74(object sender, RoutedEventArgs e)
        {
            r_74.Visibility = Visibility.Visible;
            FieldsPanelZ74.Visibility = Visibility.Visible;
        }

        private void Show_R74(object sender, RoutedEventArgs e)
        {
            DataSet MyDataSet = new DataSet();
            string sSql = "select * from Carbon_V"; // SQL-запит
            using (NpgsqlCommand npgsqlcmd = new NpgsqlCommand(sSql, dbHelper.GetConnection()))
            {
                try
                {
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(npgsqlcmd);
                    da.Fill(MyDataSet);
                }
                catch (Exception exMessage)
                {
                    MessageBox.Show(exMessage.Message);
                }
            }

            MyReportViewer74.ProcessingMode = ProcessingMode.Local;
            MyReportViewer74.LocalReport.ReportPath = @"E:\Моє\моя практика\repos_C\BD_1_1_n\BD_1_1_n\Report6.rdlc"; // Шлях до звіту
            MyReportViewer74.LocalReport.DataSources.Add(new ReportDataSource("DataSet6", MyDataSet.Tables[0]));
            MyReportViewer74.SetDisplayMode(DisplayMode.PrintLayout);
            MyReportViewer74.RefreshReport();
        }
        //кінець 74

        //звіт 73
        private void Close_R73(object sender, RoutedEventArgs e)
        {
            r_73.Visibility = Visibility.Collapsed;
            FieldsPanelZ73.Visibility = Visibility.Collapsed;
            MyReportViewer73.Clear();
        }

        private void Button_Click_Z_73(object sender, RoutedEventArgs e)
        {
            r_73.Visibility = Visibility.Visible;
            FieldsPanelZ73.Visibility = Visibility.Visible;
        }

        private void Show_R73(object sender, RoutedEventArgs e)
        {
            DataSet MyDataSet = new DataSet();
            string sSql = "select * from Sulfur_V"; // SQL-запит
            using (NpgsqlCommand npgsqlcmd = new NpgsqlCommand(sSql, dbHelper.GetConnection()))
            {
                try
                {
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(npgsqlcmd);
                    da.Fill(MyDataSet);
                }
                catch (Exception exMessage)
                {
                    MessageBox.Show(exMessage.Message);
                }
            }

            MyReportViewer73.ProcessingMode = ProcessingMode.Local;
            MyReportViewer73.LocalReport.ReportPath = @"E:\Моє\моя практика\repos_C\BD_1_1_n\BD_1_1_n\Report5.rdlc"; // Шлях до звіту
            MyReportViewer73.LocalReport.DataSources.Add(new ReportDataSource("DataSet5", MyDataSet.Tables[0]));
            MyReportViewer73.SetDisplayMode(DisplayMode.PrintLayout);
            MyReportViewer73.RefreshReport();
        }
        //кінець 73

        //report 72
        private void Close_R72(object sender, RoutedEventArgs e)
        {
            r_72.Visibility = Visibility.Collapsed;
            FieldsPanelZ72.Visibility = Visibility.Collapsed;
            MyReportViewer72.Clear();
        }

        private void Button_Click_Z_72(object sender, RoutedEventArgs e)
        {
            r_72.Visibility = Visibility.Visible;
            FieldsPanelZ72.Visibility = Visibility.Visible;
        }

        private void Show_R72(object sender, RoutedEventArgs e)
        {
            DataSet MyDataSet = new DataSet();
            string sSql = "select * from V_72"; // SQL-запит
            using (NpgsqlCommand npgsqlcmd = new NpgsqlCommand(sSql, dbHelper.GetConnection()))
            {
                try
                {
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(npgsqlcmd);
                    da.Fill(MyDataSet);
                }
                catch (Exception exMessage)
                {
                    MessageBox.Show(exMessage.Message);
                }
            }

            MyReportViewer72.ProcessingMode = ProcessingMode.Local;
            MyReportViewer72.LocalReport.ReportPath = @"E:\Моє\моя практика\repos_C\BD_1_1_n\BD_1_1_n\Report4.rdlc"; // Шлях до звіту
            MyReportViewer72.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", MyDataSet.Tables[0]));
            MyReportViewer72.SetDisplayMode(DisplayMode.PrintLayout);
            MyReportViewer72.RefreshReport();
        }
        //кінець 72

        // report 71
        private void Close_R71(object sender, RoutedEventArgs e)
        {
            r_71.Visibility = Visibility.Collapsed;
            FieldsPanelZ71.Visibility = Visibility.Collapsed;
            MyReportViewer71.Clear();
        }

        private void Button_Click_Z_71(object sender, RoutedEventArgs e)
        {
            r_71.Visibility = Visibility.Visible;
            FieldsPanelZ71.Visibility = Visibility.Visible;
        }

        private void Show_R71(object sender, RoutedEventArgs e)
        {
            MyReportViewer71.Clear();           
            DateTime fromDate;
            DateTime toDate;         
            fromDate = FromDat.SelectedDate ?? DateTime.MinValue;
            toDate = ToDat.SelectedDate ?? DateTime.MaxValue;
            DataSet MyDataSet = new DataSet();           
            string sSql1 = $" SELECT\r\n    " +
                $" Stations.City,\r\n   " +
                $" Measured_Unit.Title AS Unit_Title,\r\n    " +
                $" MAX(Measurment.Value_) AS Max_Value\r\n" +
                $" FROM\r\n    Measurment\r\n" +
                $" JOIN Stations ON Measurment.ID_Station = Stations.ID_Station\r\n" +
                $" JOIN Measured_Unit ON Measurment.ID_Measured_Unit = Measured_Unit.ID_Measured_Unit\r\n" +
                $" WHERE\r\n    Measured_Unit.Title IN ('PM2.5', 'PM10')    " +
                $" AND Measurment.Date_Time >= '{fromDate}' " +
                $" AND Measurment.Date_Time <= '{toDate}' " +
                $" GROUP BY\r\n    Stations.City, Measured_Unit.Title;";
                       
            using (NpgsqlCommand npgsqlcmd = new NpgsqlCommand(sSql1, dbHelper.GetConnection())) //dbHelper.GetConnection()
            {
                try
                {
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(npgsqlcmd);
                    da.Fill(MyDataSet);
                }
                catch (Exception exMessage)
                {
                    MessageBox.Show(exMessage.Message);
                }
            }

            MyReportViewer71.ProcessingMode = ProcessingMode.Local;
            MyReportViewer71.LocalReport.ReportPath = @"E:\Моє\моя практика\repos_C\BD_1_1_n\BD_1_1_n\Report3.rdlc"; // Шлях до звіту
            MyReportViewer71.LocalReport.SetParameters(new ReportParameter("Parameter711", fromDate.ToString()));
            MyReportViewer71.LocalReport.SetParameters(new ReportParameter("Parameter712", toDate.ToString()));
            MyReportViewer71.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", MyDataSet.Tables[0]));
            MyReportViewer71.SetDisplayMode(DisplayMode.PrintLayout);
            MyReportViewer71.RefreshReport();
        }
        //кінець 71

        //звіт 2 початок
        private void Close_R2(object sender, RoutedEventArgs e)
        {
            r_2.Visibility = Visibility.Collapsed;
            FieldsPanelZ2.Visibility = Visibility.Collapsed;
            MyReportViewer2.Clear();
        }

        private void Button_Click_Z_2(object sender, RoutedEventArgs e)
        {
            r_2.Visibility = Visibility.Visible;
            FieldsPanelZ2.Visibility = Visibility.Visible;

            DataTable stationData = GetStationData();
            StationComboBox.ItemsSource = stationData.AsEnumerable().Select(r => r.Field<string>("Name_Station")).ToList();
        }

        string selectedStationCode = "0001";
        string selectedStationName = "";

        private void StationComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StationComboBox.SelectedItem != null)
            {
                selectedStationName = StationComboBox.SelectedItem.ToString();
                selectedStationCode = GetStationCodeByName(selectedStationName);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            MyReportViewer2.Clear();
            string conn = "Host=localhost;Port=5432;Username=roma;Password=1234;Database=MyDB1;";           
            DateTime fromDate;
            DateTime toDate;

            NpgsqlConnection connection = new NpgsqlConnection(conn);
            connection.Open();
            fromDate = FromD.SelectedDate ?? DateTime.MinValue;
            toDate = ToD.SelectedDate ?? DateTime.MaxValue;
            DataSet MyDataSet = new DataSet();
            MyDataSet.Clear();
            string sSql1 = $"SELECT\r\n    MU.Title AS Measured_Unit_Title,\r\n    MAX(M.Value_) AS Max_Value,\r\n    MIN(M.Value_) AS Min_Value,\r\n    CAST(AVG(M.Value_) AS NUMERIC(10, 3)) AS Avg_Value,\r\n    MU.Unit AS Measured_Unit_Unit\r\nFROM\r\n    Measurment M\r\nINNER JOIN\r\n    Measured_Unit MU ON M.ID_Measured_Unit = MU.ID_Measured_Unit\r\nWHERE\r\n    M.ID_Station = '{selectedStationCode}'\r\n    AND M.Date_Time >=  '{fromDate}'\r\n    AND M.Date_Time <= '{toDate}'\r\nGROUP BY\r\n    M.ID_Station,\r\n    M.ID_Measured_Unit,\r\n    MU.Title,\r\n    MU.Unit;";
            string sSql = $"SELECT    MU.Title AS Measured_Unit_Title,    MAX(M.Value_) AS Max_Value,    MIN(M.Value_) AS Min_Value,    CAST(AVG(M.Value_) AS NUMERIC(10, 3)) AS Avg_Value,    MU.Unit AS Measured_Unit_Unit FROM    Measurment M INNER JOIN     Measured_Unit MU ON M.ID_Measured_Unit = MU.ID_Measured_Unit WHERE     M.ID_Station = '{selectedStationCode}'     AND M.Date_Time >=  '{fromDate}'    AND M.Date_Time <= '{toDate}' GROUP BY     M.ID_Station,     M.ID_Measured_Unit,     MU.Title,    MU.Unit;"; // SQL-запит
            using (NpgsqlCommand npgsqlcmd = new NpgsqlCommand(sSql1, connection)) //dbHelper.GetConnection()
            {
                try
                {
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(npgsqlcmd);
                    da.Fill(MyDataSet);
                }
                catch (Exception exMessage)
                {
                    MessageBox.Show(exMessage.Message);
                }
            }
            
            MyReportViewer2.ProcessingMode = ProcessingMode.Local;
            MyReportViewer2.LocalReport.ReportPath = @"E:\Моє\моя практика\repos_C\BD_1_1_n\BD_1_1_n\Report2.rdlc"; 

            MyReportViewer2.LocalReport.SetParameters(new ReportParameter("Parameter1", selectedStationName));
            MyReportViewer2.LocalReport.SetParameters(new ReportParameter("Parameter2", fromDate.ToString()));
            MyReportViewer2.LocalReport.SetParameters(new ReportParameter("Parameter3", toDate.ToString()));

            MyReportViewer2.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", MyDataSet.Tables[0]));
            MyReportViewer2.SetDisplayMode(DisplayMode.PrintLayout);           
            MyReportViewer2.RefreshReport();
        }
        //звіт 2 кінець

        //звіт 1 початок
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {            
            DataSet MyDataSet = new DataSet();
            string sSql = "select * from Connected_Stations_Info"; // SQL-запит
            using (NpgsqlCommand npgsqlcmd = new NpgsqlCommand(sSql, dbHelper.GetConnection()))
            {
                try
                {
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(npgsqlcmd);
                    da.Fill(MyDataSet);
                }
                catch (Exception exMessage)
                {
                    MessageBox.Show(exMessage.Message);
                }
            }           
            MyReportViewer.ProcessingMode = ProcessingMode.Local;
            MyReportViewer.LocalReport.ReportPath = @"E:\Моє\моя практика\repos_C\BD_1_1_n\BD_1_1_n\Report1.rdlc"; // Шлях до звіту
            MyReportViewer.LocalReport.DataSources.Add(new ReportDataSource("DataSet1", MyDataSet.Tables[0]));
            MyReportViewer.SetDisplayMode(DisplayMode.PrintLayout);
            MyReportViewer.RefreshReport();
        }

        private void Close_R1(object sender, RoutedEventArgs e)
        {
            r_1.Visibility = Visibility.Collapsed;
            ClB1.Visibility = Visibility.Collapsed;
            ShowB1.Visibility = Visibility.Collapsed;
        }

        private void Button_Click_Z(object sender, RoutedEventArgs e)
        {
            r_1.Visibility = Visibility.Visible;
            ClB1.Visibility = Visibility.Visible;
            ShowB1.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FieldsPanel.Visibility = Visibility.Visible;
            dataGrid.Visibility = Visibility.Visible;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            FieldsPanel.Visibility = Visibility.Collapsed;
            dataGrid.Visibility = Visibility.Collapsed;
        }

        //таблиці
        private void Server_B_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = GetDataFromTable("mqtt_server");
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Stations_B_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = GetDataFromTable("Stations");
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Meas_Unit_B_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = GetDataFromTable("Measured_Unit");
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Fav_St_B_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = GetDataFromTable5();
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Category_B_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = GetDataFromTable("Category");
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Opt_Val_B_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = GetDataFromTable6();
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        private void Mes_Unit_B_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = GetDataFromTable7();
            dataGrid.ItemsSource = dataTable.DefaultView;
        }

        //private void Rezult_B_Click(object sender, RoutedEventArgs e)
        //{
        //    LoadingText.Visibility = Visibility.Visible;
        //    loadingProgressBar.Visibility = Visibility.Visible;

        //    DataTable dataTable = GetDataFromTable8();

        //    LoadingText.Visibility = Visibility.Collapsed;
        //    loadingProgressBar.Visibility = Visibility.Collapsed;

        //    dataGrid.ItemsSource = dataTable.DefaultView;
        //}
        private async void Rezult_B_Click(object sender, RoutedEventArgs e)
        {
            dataGrid.ItemsSource = null;
            LoadingText.Visibility = Visibility.Visible;
            loadingProgressBar.Visibility = Visibility.Visible;

            DataTable dataTable = await GetDataFromTable8Async();

            LoadingText.Visibility = Visibility.Collapsed;
            loadingProgressBar.Visibility = Visibility.Collapsed;

            dataGrid.ItemsSource = dataTable.DefaultView;
        }



        private DataTable GetDataFromTable(string tableName)
        {           
            string query = $"SELECT * FROM {tableName}";
            using (NpgsqlCommand command = new NpgsqlCommand(query, dbHelper.GetConnection()))
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        private DataTable GetDataFromTable5()
        {        
            string query = $"SELECT    " +
                $"fs.User_Name,    " +
                $"st.City,   " +
                $"st.Name_Station,   " +
                $"st.Status AS Station_Status " +
                $"FROM    Favorite_Station fs " +
                $"JOIN   Stations st ON fs.ID_Station = st.ID_Station; ";

            using (NpgsqlCommand command = new NpgsqlCommand(query, dbHelper.GetConnection()))
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        private DataTable GetDataFromTable6()
        {            
            string query = $"SELECT " +
                $" OV.Bottom_Border AS \"Нижня межа\"," +
                $" OV.Upper_Border AS \"Верхня межа\" ," +
                $" C.Designation AS \"Категорія\" ," +
                $" MU.Title AS \"Параметр\" ," +
                $" MU.Unit AS \"Одиниця вимірювання\" " +
                $" FROM " +
                $" Optimal_Value OV" +
                $" JOIN " +
                $" Category C ON OV.ID_Category = C.ID_Category" +
                $" JOIN  Measured_Unit MU ON OV.ID_Measured_Unit = MU.ID_Measured_Unit; ";

            using (NpgsqlCommand command = new NpgsqlCommand(query, dbHelper.GetConnection()))
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    return dataTable;
                }
            }
        }

        private DataTable GetDataFromTable7()
        {           
            string query = $" SELECT  " +
                $"MMU.Message_ AS \"Повідомлення\",   " +
                $"MMU.Queue_Number AS \"Номер черги\" ,   " +
                $"S.City AS \"Місто станції\",    " +
                $"S.Name_Station AS \"Назва станції\",   " +
                $"MU.Title AS \"Параметр\",   " +
                $"MU.Unit AS \"Одиниця вимірювання\" " +
                $"FROM    " +
                $"MQTT_Message_Unit MMU " +
                $"JOIN    Stations S ON MMU.ID_Station = S.ID_Station " +
                $"JOIN   Measured_Unit MU ON MMU.ID_Measured_Unit = MU.ID_Measured_Unit;";
            using (NpgsqlCommand command = new NpgsqlCommand(query, dbHelper.GetConnection()))
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        //private DataTable GetDataFromTable8()
        //{            
        //    string query =
        //        "SELECT " +
        //        "M.Date_Time AS \"Дата та час\" , " +
        //        "M.Value_ AS \"Значення\", " +
        //        "S.City  AS \"Місто станції\", " +
        //        "S.Name_Station  AS \"Назва станції\", " +
        //        "MU.Title  AS \"Параметр\", " +
        //        "MU.Unit AS \"Одиниця вимірювання\" " +
        //        "FROM " +
        //        "Measurment M " +
        //        "JOIN " +
        //        "Stations S ON M.ID_Station = S.ID_Station " +
        //        "JOIN " +
        //        "Measured_Unit MU ON M.ID_Measured_Unit = MU.ID_Measured_Unit " +
        //        "LIMIT 350;";

        //    using (NpgsqlCommand command = new NpgsqlCommand(query, dbHelper.GetConnection()))
        //    {
        //        using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
        //        {
        //            DataTable dataTable = new DataTable();
        //            adapter.Fill(dataTable);
        //            return dataTable;
        //        }
        //    }
        //}
        private async Task<DataTable> GetDataFromTable8Async()
        {
            string query =
                "SELECT " +
                "M.Date_Time AS \"Дата та час\" , " +
                "M.Value_ AS \"Значення\", " +
                "S.City  AS \"Місто станції\", " +
                "S.Name_Station  AS \"Назва станції\", " +
                "MU.Title  AS \"Параметр\", " +
                "MU.Unit AS \"Одиниця вимірювання\" " +
                "FROM " +
                "Measurment M " +
                "JOIN " +
                "Stations S ON M.ID_Station = S.ID_Station " +
                "JOIN " +
                "Measured_Unit MU ON M.ID_Measured_Unit = MU.ID_Measured_Unit;"; 

            using (NpgsqlCommand command = new NpgsqlCommand(query, dbHelper.GetConnection()))
            {
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    await Task.Run(() => adapter.Fill(dataTable));
                    return dataTable;

                }
            }
        }


        public DataTable GetStationData()
        {
            DataTable dataTable = new DataTable();
                string query = "SELECT Name_Station FROM Stations";
                using (NpgsqlCommand command = new NpgsqlCommand(query, dbHelper.GetConnection()))
                {
                    using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }           
            return dataTable;
        }

        private string GetStationCodeByName(string stationName)
        {
                string query = "SELECT ID_Station FROM Stations WHERE Name_Station = @StationName";
                using (NpgsqlCommand command = new NpgsqlCommand(query, dbHelper.GetConnection()))
                {
                    command.Parameters.AddWithValue("@StationName", stationName);
                    return (string)command.ExecuteScalar();
                }            
        }

        //чернетка
        private void ShowMap(object sender, RoutedEventArgs e)
        {
            gmapControl.Visibility = Visibility.Visible;
            CloseMap.Visibility = Visibility.Visible;
            // Ініціалізація GMapControl
            gmapControl.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            //gmapControl.MapProvider = GMapProviders.GoogleMap;
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            gmapControl.Position = new PointLatLng(49.789469410339, 30.959431325703594);
            // Включение интерактивных возможностей
            gmapControl.InvertedMouseWheelZooming = true;
            gmapControl.DragButton = MouseButton.Left;            
            gmapControl.Zoom = 5;
            
            //маркери
           // makeMarker(new PointLatLng(54.6961334816182, 25.2985095977783), "roma");

            // Виконання SQL-запиту для отримання даних з таблиці Stations
            string sql = "SELECT Name_Station, Coordinates FROM Stations";
            using (NpgsqlCommand cmd = new NpgsqlCommand(sql, dbHelper.GetConnection()))
            {
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Отримання значень "Name_Station" і "Coordinates" з результату запиту
                        string tag = reader["Name_Station"].ToString();
                        string coordinates = reader["Coordinates"].ToString();

                        coordinates = coordinates.Replace("(", "").Replace(")", "").Replace(",", " ").Replace(".",",");
                        string[] coordParts = coordinates.Split(' ');
                        //string x = coordParts[0];
                        //string y = coordParts[1];
                        //MessageBox.Show(x);
                        //string a = "35,2254115";
                        double lat = double.Parse(coordParts[0]);
                        double lon = double.Parse(coordParts[1]);

                        // Виклик методу makeMarker з отриманими значеннями
                        makeMarker(lon, lat,  tag);
                    }
                }
            }


        }

        private void CloseGMap(object sender, RoutedEventArgs e)
        {
            gmapControl.Visibility = Visibility.Collapsed;
            CloseMap.Visibility = Visibility.Collapsed;
        }

        GMapMarker currentMarker;
        void makeMarker(double lat, double lon, string tag) ///void makeMarker(PointLatLng p, string tag)  void makeMarker(double lat, double lon, string tag)
        {
            PointLatLng p = new PointLatLng(lat, lon);
            currentMarker = new GMapMarker(p);
            {
                BitmapImage markerImage = new BitmapImage(new Uri("E:\\Моє\\моя практика\\repos_C\\BD_1_1_n\\BD_1_1_n\\Img\\image-removebg-preview.png"));

                Image image = new Image
                {
                    Source = markerImage,
                    Width = 24, // Встановіть розмір, який вам потрібен для вашої іконки
                    Height = 24, // Встановіть розмір, який вам потрібен для вашої іконки
                };

                currentMarker.Shape = image;
                currentMarker.Offset = new System.Windows.Point(-15, -15);
                currentMarker.ZIndex = int.MaxValue;

                // Створення власного вспливаючого балончика
                var tooltipPopup = new Popup
                {
                    Placement = PlacementMode.Mouse,
                    Child = new TextBlock
                    {
                        Text = tag,
                        Background = Brushes.Yellow, // Встановіть бажаний колір фону
                        Foreground = Brushes.Black, // Встановіть бажаний колір тексту
                        Padding = new Thickness(5) // Встановіть відступи, які вам потрібні
                    }
                };

                currentMarker.Shape.MouseEnter += (sender, e) => tooltipPopup.IsOpen = true;
                currentMarker.Shape.MouseLeave += (sender, e) => tooltipPopup.IsOpen = false;

                gmapControl.Markers.Add(currentMarker);
            }
        }

        
    }
}


//робота з картою НА МАЙБУТНЄ
//void mapControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
//{
//    Point clickPoint = e.GetPosition(gmapControl);
//    PointLatLng point = gmapControl.FromLocalToLatLng((int)clickPoint.X, (int)clickPoint.Y);
//    //GMapMarker marker = new GMapMarker(point);
//    //mapControl.Markers.Add(marker);
//    // set current marker
//    currentMarker = new GMapMarker(point);
//    {
//        //currentMarker.Shape = new CustomMarkerRed(this, currentMarker, "custom position marker");
//        currentMarker.Offset = new System.Windows.Point(-15, -15);
//        currentMarker.ZIndex = int.MaxValue;
//        gmapControl.Markers.Add(currentMarker);
//    }
//}
//gmapControl.MouseLeftButtonDown += new MouseButtonEventHandler(mapControl_MouseLeftButtonDown);

//gmapControl.UpdateLayout();


//gmapControl.ReloadMap();

//gmapControl.Manager. = true;

//GMapMarker marker = new GMapMarker(new PointLatLng(54.6961334816182, 25.2985095977783));
//BitmapImage markerImage = new BitmapImage(new Uri("E:\\Моє\\моя практика\\repos_C\\BD_1_1_n\\BD_1_1_n\\Img\\image-removebg-preview.png"));
//marker.Tag = "ejgopnribj";
//Image image = new Image
//{
//    Source = markerImage,
//    Width = 24, // Встановіть розмір, який вам потрібен для вашої іконки
//    Height = 24, // Встановіть розмір, який вам потрібен для вашої іконки
//};
//marker.Shape = image;
//gmapControl.Markers.Add(marker);


//var currentMarker = new GMap.NET.WindowsPresentation.GMapMarker(point);
//{

//    currentMarker.Offset = new Point(-16, -32);
//    currentMarker.ZIndex = int.MaxValue;

//    gmapControl.Markers.Add(currentMarker);
//}