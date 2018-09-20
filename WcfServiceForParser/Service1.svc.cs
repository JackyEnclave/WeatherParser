using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace WcfServiceForParser
{
    class Service1 : IService1
    {
        private void SetData(string query)
        {
            MySqlCommand command = new MySqlCommand(query, _conn);
            command.ExecuteNonQuery();
        }

        private string GetData(string query)
        {
            MySqlCommand command = new MySqlCommand(query, _conn);
            string data = null;
            try
            {
                data = command.ExecuteScalar().ToString();
            }
            catch
            {
                return null;
            }
            return data;
        }

        private static MySqlConnection _conn;
        private static MySqlConnection OpenTheConnection()
        {
            string connmysql = "server=localhost;charset=cp1251;user=root;database=grabber;password=root;";
            MySqlConnection conn = new MySqlConnection(connmysql);
            conn.Open();
            _conn = conn;
            return conn;
        }

        private static void CloseTheConnection(MySqlConnection conn) => conn.Close();

        public void SetRegionToMySQL(string town)
        {
            OpenTheConnection();
            SetData("UPDATE weather SET town='" + town + "' WHERE id=1");
            CloseTheConnection(_conn);
        }
        public string[] GetRegionArr()
        {
            Refresh();
            string region = "";
            List<string> regions = new List<string>();
            int i = 0;
            OpenTheConnection();
            while (region!=null)
            {
                region = GetData($"SELECT region FROM regions WHERE id = {i}");
                regions.Add(region);
                i++;
            }
            CloseTheConnection(_conn);
            return regions.ToArray();
        }

        public string GetNowWeather()
        {
            string nowWeather;
            OpenTheConnection();
            nowWeather = GetData("SELECT nowweather FROM weather WHERE id=1");
            CloseTheConnection(_conn);
            return nowWeather;
        }

        public string[] GetTodayWeather()
        {
            string todayWeather="";
            List<string> todayWeatherList = new List<string>();
            OpenTheConnection();
            for(int i = 1; i<5; i++)
            {
                todayWeather = GetData($"SELECT todayweather FROM weather WHERE id = {i}");
                todayWeatherList.Add(todayWeather);
            }
            CloseTheConnection(_conn);
            return todayWeatherList.ToArray();
        }

        public string[] GetOtherInformation()
        {
            string otherInfo = "";
            List<string> otherInfoList = new List<string>();
            OpenTheConnection();
            for (int i=1; i<7; i++)
            {
                otherInfo = GetData($"SELECT otherinfo FROM weather WHERE id = {i}");
                otherInfoList.Add(otherInfo);
            }
            CloseTheConnection(_conn);
            return otherInfoList.ToArray();
        }

        public void Refresh() => WeatherParser.Program.Main();
    }
}
