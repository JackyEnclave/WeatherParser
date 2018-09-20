using WpfForParser.ServiceReference1;

namespace WpfForParser.Model
{
    class Model
    {
        private static Service1Client _client = new Service1Client();
        public static string[] GetRegion() => _client.GetRegionArr();

        public static void SetRegion(string selectedRegion)=> _client.SetRegionToMySQL(selectedRegion);

        public static string GetNowWeather() => _client.GetNowWeather();

        public static string[] GetTodayWeather() => _client.GetTodayWeather();

        public static string[] GetOtherInformation() => _client.GetOtherInformation();
    }
}
