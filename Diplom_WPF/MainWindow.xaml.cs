using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Configuration;

namespace Diplom_WPF
{
    public partial class MainWindow : Window
    {
        string connectionString;
        int shift = 0;
        List<string> products = new List<string>();
        List<string> factories = new List<string>();
        List<string> dates = new List<string>();
        bool loaded = false;

        public MainWindow(){
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;}
        
        private void UploadDB(){
            SqlConnection connection = null;
            try{
                connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT DISTINCT production FROM ProductionValues", connection);
                SqlDataReader dataReader = command.ExecuteReader();
                products.Clear();
                while (dataReader.Read()){
                    products.Add(dataReader.GetString(0));}
                dataReader.Close();
                command = new SqlCommand("SELECT DISTINCT factory FROM ProductionValues", connection);
                dataReader = command.ExecuteReader();
                factories.Clear();
                while (dataReader.Read()){
                    factories.Add(dataReader.GetString(0));}
                dataReader.Close();
                command = new SqlCommand("SELECT DISTINCT date FROM ProductionValues", connection);
                dataReader = command.ExecuteReader();
                dates.Clear();
                while (dataReader.Read()){
                    dates.Add(dataReader.GetDateTime(0).ToString("yyyy.MM")); }
                dataReader.Close();
                for (int i = 0; i < shift; i++) {
                    for (int j = 0; j < dates.Count - 1; j++) {
                        string buf = dates[j];
                        dates[j] = dates[j + 1];
                        dates[j + 1] = buf; } }

                int[,,] values = new int[dates.Count, products.Count, factories.Count];
                for (int i=0;i<dates.Count;i++) {
                    for (int j = 0; j < products.Count; j++) {
                        for (int k = 0; k < factories.Count; k++) {
                            command = new SqlCommand("SELECT value FROM ProductionValues WHERE date='" + dates[i] + ".01 ' AND production='" + products[j] + "' AND factory='" + factories[k] + "';", connection);
                            dataReader = command.ExecuteReader();
                            while (dataReader.Read())
                            { values[i, j, k] = dataReader.GetInt32(0); }
                            dataReader.Close(); } } }
                
                l11.Text = values[dates.Count - 1, 0, 0].ToString();
                l12.Text = values[dates.Count - 1, 0, 1].ToString();
                l13.Text = values[dates.Count - 1, 0, 2].ToString();
                l14.Text = values[dates.Count - 1, 0, 3].ToString();
                l21.Text = values[dates.Count - 1, 1, 0].ToString();
                l22.Text = values[dates.Count - 1, 1, 1].ToString();
                l23.Text = values[dates.Count - 1, 1, 2].ToString();
                l24.Text = values[dates.Count - 1, 1, 3].ToString();
                l31.Text = values[dates.Count - 1, 2, 0].ToString();
                l32.Text = values[dates.Count - 1, 2, 1].ToString();
                l33.Text = values[dates.Count - 1, 2, 2].ToString();
                l34.Text = values[dates.Count - 1, 2, 3].ToString();
                l41.Text = values[dates.Count - 1, 3, 0].ToString();
                l42.Text = values[dates.Count - 1, 3, 1].ToString();
                l43.Text = values[dates.Count - 1, 3, 2].ToString();
                l44.Text = values[dates.Count - 1, 3, 3].ToString();
                l51.Text = values[dates.Count - 1, 4, 0].ToString();
                l52.Text = values[dates.Count - 1, 4, 1].ToString();
                l53.Text = values[dates.Count - 1, 4, 2].ToString();
                l54.Text = values[dates.Count - 1, 4, 3].ToString();

                u11.Text = (values[0, 0, 0] + values[0, 1, 0] + values[0, 2, 0] + values[0, 3, 0] + values[0, 4, 0]).ToString();
                u12.Text = (values[0, 0, 1] + values[0, 1, 1] + values[0, 2, 1] + values[0, 3, 1] + values[0, 4, 1]).ToString();
                u13.Text = (values[0, 0, 2] + values[0, 1, 2] + values[0, 2, 2] + values[0, 3, 2] + values[0, 4, 2]).ToString();
                u14.Text = (values[0, 0, 3] + values[0, 1, 3] + values[0, 2, 3] + values[0, 3, 3] + values[0, 4, 3]).ToString();
                u21.Text = (values[1, 0, 0] + values[1, 1, 0] + values[1, 2, 0] + values[1, 3, 0] + values[1, 4, 0]).ToString();
                u22.Text = (values[1, 0, 1] + values[1, 1, 1] + values[1, 2, 1] + values[1, 3, 1] + values[1, 4, 1]).ToString();
                u23.Text = (values[1, 0, 2] + values[1, 1, 2] + values[1, 2, 2] + values[1, 3, 2] + values[1, 4, 2]).ToString();
                u24.Text = (values[1, 0, 3] + values[1, 1, 3] + values[1, 2, 3] + values[1, 3, 3] + values[1, 4, 3]).ToString();
                u31.Text = (values[2, 0, 0] + values[2, 1, 0] + values[2, 2, 0] + values[2, 3, 0] + values[2, 4, 0]).ToString();
                u32.Text = (values[2, 0, 1] + values[2, 1, 1] + values[2, 2, 1] + values[2, 3, 1] + values[2, 4, 1]).ToString();
                u33.Text = (values[2, 0, 2] + values[2, 1, 2] + values[2, 2, 2] + values[2, 3, 2] + values[2, 4, 2]).ToString();
                u34.Text = (values[2, 0, 3] + values[2, 1, 3] + values[2, 2, 3] + values[2, 3, 3] + values[2, 4, 3]).ToString();

                r11.Text = (values[2, 0, 0] + values[2, 0, 1] + values[2, 0, 2] + values[2, 0, 3]).ToString();
                r12.Text = (values[1, 0, 0] + values[1, 0, 1] + values[1, 0, 2] + values[1, 0, 3]).ToString();
                r13.Text = (values[0, 0, 0] + values[0, 0, 1] + values[0, 0, 2] + values[0, 0, 3]).ToString();
                r21.Text = (values[2, 1, 0] + values[2, 1, 1] + values[2, 1, 2] + values[2, 1, 3]).ToString();
                r22.Text = (values[1, 1, 0] + values[1, 1, 1] + values[1, 1, 2] + values[1, 1, 3]).ToString();
                r23.Text = (values[0, 1, 0] + values[0, 1, 1] + values[0, 1, 2] + values[0, 1, 3]).ToString();
                r31.Text = (values[2, 2, 0] + values[2, 2, 1] + values[2, 2, 2] + values[2, 2, 3]).ToString();
                r32.Text = (values[1, 2, 0] + values[1, 2, 1] + values[1, 2, 2] + values[1, 2, 3]).ToString();
                r33.Text = (values[0, 2, 0] + values[0, 2, 1] + values[0, 2, 2] + values[0, 2, 3]).ToString();
                r41.Text = (values[2, 3, 0] + values[2, 3, 1] + values[2, 3, 2] + values[2, 3, 3]).ToString();
                r42.Text = (values[1, 3, 0] + values[1, 3, 1] + values[1, 3, 2] + values[1, 3, 3]).ToString();
                r43.Text = (values[0, 3, 0] + values[0, 3, 1] + values[0, 3, 2] + values[0, 3, 3]).ToString();
                r51.Text = (values[2, 4, 0] + values[2, 4, 1] + values[2, 4, 2] + values[2, 4, 3]).ToString();
                r52.Text = (values[1, 4, 0] + values[1, 4, 1] + values[1, 4, 2] + values[1, 4, 3]).ToString();
                r53.Text = (values[0, 4, 0] + values[0, 4, 1] + values[0, 4, 2] + values[0, 4, 3]).ToString();

                dh1.Text = dates[0];
                dh2.Text = dates[1];
                dh3.Text = dates[2];
                fh1.Text = factories[0];
                fh2.Text = factories[1];
                fh3.Text = factories[2];
                fh4.Text = factories[3];
                ph1.Text = products[0];
                ph2.Text = products[1];
                ph3.Text = products[2];
                ph4.Text = products[3];
                ph5.Text = products[4];
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            UploadDB();
            loaded = true;
        }

        private void UpdateDB(int row, int column, string value)
        {
            if (value != ""&&value != null)
            {
                connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                SqlCommand command = new SqlCommand("UPDATE ProductionValues SET value=" + value + " WHERE date='" + dates[2] + ".01 ' AND production='" + products[row - 1] + "' AND factory='" + factories[column - 1] + "';", connection);
                command.ExecuteNonQuery();
                connection.Close();
                UploadDB();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            shift++;
            UploadDB();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (shift != 0) shift--;
            else shift += 2;
            UploadDB();
        }

        private void l11_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(1, 1, l11.Text);}

        private void l12_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(1, 2, l12.Text);}

        private void l13_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(1, 3, l13.Text);}

        private void l14_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(1, 4, l14.Text);}

        private void l21_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(2, 1, l21.Text);}

        private void l22_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(2, 2, l22.Text);}

        private void l23_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(2, 3, l23.Text);}

        private void l24_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(2, 4, l24.Text);}

        private void l31_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(3, 1, l31.Text);}

        private void l32_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(3, 2, l32.Text);}

        private void l33_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(3, 3, l33.Text);}

        private void l34_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(3, 4, l34.Text);}

        private void l41_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(4, 1, l41.Text);}

        private void l42_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(4, 2, l42.Text);}

        private void l43_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(4, 3, l43.Text);}

        private void l44_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(4, 4, l44.Text);}

        private void l51_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(5, 1, l51.Text);}

        private void l52_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(5, 2, l52.Text);}

        private void l53_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(5, 3, l53.Text);}

        private void l54_TextChanged(object sender, TextChangedEventArgs e){
            if (loaded) UpdateDB(5, 4, l54.Text);}
    }
}
