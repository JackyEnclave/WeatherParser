using HtmlAgilityPack;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Net;
using System.Text;

namespace WeatherParser
{
    class Parser
    {
        public Parser()
        {
            OpenTheConnection();
            string region = GetData("SELECT town FROM weather WHERE id = 1");
            _link = CreateLink(region, GetData("SELECT link FROM weather WHERE id = 1"));
            SetRegions();
            GetTodayWeather();
            SetTodayWeather();
            GetNowWeather();
            SetNowWeather();
            GetOtherInformation();
            SetOtherInformation();
            CloseTheConnection(_conn);
        }

        private static string _link;
        private static MySqlConnection _conn;
        private static HtmlDocument CreateHtmlDocument(string link)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            try // исключаем некорректный ввод или отсутствие связи
            {
                WebClient wclient = new WebClient();
                wclient.Encoding = UTF8Encoding.UTF8;
                string website = wclient.DownloadString(link);
                htmlDocument.LoadHtml(website);
                SetData("UPDATE weather SET town=null WHERE id=2");
            }
            catch
            {
                return htmlDocument;
            }
            return htmlDocument;
        }

        private static string[] Parse(string node, bool innerTextFlag, string webSite)
        {
            HtmlDocument htmlDocument = CreateHtmlDocument(webSite);
            HtmlNodeCollection nodes = htmlDocument.DocumentNode.SelectNodes(node); // ищем узлы html
            if (nodes != null)
            {
                int nodeCounter = nodes.Count();
                string[] dataArray = new string[nodeCounter];
                int i = 0;
                foreach (HtmlNode n in nodes)
                {
                    if (innerTextFlag) // получение внутреннего текста
                    {
                        dataArray[i] = n.InnerText;
                        i++;
                    }
                    else // получение ссылки
                    {
                        dataArray[i] = n.GetAttributeValue("href", "");
                        i++;
                    }
                }
                return dataArray;
            }
            string[] arr = { $"Погода в населенном пункте: {GetData("SELECT town FROM weather WHERE id = 1")}\n\nНажмите сюда для возврата к списку государств" };
            return arr;
        }

        private static string[] GetCityArr()=>Parse("//a [@class='link place-list__item-name']", true, _link);

        private static void SetRegions()
        {
            SetData("TRUNCATE TABLE regions");
            string[] city = GetCityArr();
            int i = 0;
            foreach (string str in city)
            {
                SetData($"INSERT INTO regions VALUES({i}, null)");
                SetData($"UPDATE regions SET region='{str}' WHERE id={i}");
                i++;
            }
        }
        private static string CreateLink(string region, string link)
        {
            try
            {
                HtmlDocument document = CreateHtmlDocument(link);
                string[] linkArr = Parse("//a [@class='link place-list__item-name']", false, link);
                int i = 0;
                foreach (string str in linkArr)
                {
                    linkArr[i] = $"https://yandex.ru{linkArr[i]}";
                    i++;
                }
                _link = link;
                link = linkArr[GetElementNumber(GetCityArr(), region)];
                SetData($"UPDATE weather SET link ='{link}' WHERE id=1");
                return link;
            }
            catch
            {
                SetData($"UPDATE weather SET link = 'https://yandex.ru/pogoda/region/' WHERE id=1");
                return "https://yandex.ru/pogoda/region/";
            }
        }




        private static int GetElementNumber(string[] city, string selectedCity) => Array.IndexOf(city, selectedCity); //ищем номер элемента массива для отправки ссылки

        private static string GetNowWeather() //погода сейчас
        {
            string[] nowWeather = Parse("//div [@class='temp fact__temp']", true, _link); //температура сейчас
            nowWeather = CheckTrash(nowWeather);
            if (nowWeather == null)
                return null;
            else
                return nowWeather[0];
        }

        private static void SetNowWeather()
        {
            string nowWeather = GetNowWeather();
            SetData($"UPDATE weather SET nowweather = '{nowWeather}' WHERE id=1");
        }

        private static string[] TimeOfDay() => Parse("//div [@class='day-parts-next__name']", true, _link); //время суток

        private static string[] GetTodayWeather()
        {
            string[] tempArray = Parse("//span [@class='day-parts-next__temp']", true, _link); //температура сегодня
            string[] timeOfDayArray = TimeOfDay(); //время суток
            string[] todayWeather = new string[4];
            int i = 0;
            foreach (string str in tempArray)
            {
                todayWeather[i] = $"{timeOfDayArray[i]}: {tempArray[i]}";
                i++;
            }
            return CheckTrash(todayWeather);
        }

        private static void SetTodayWeather()
        {
            string[] todayWeather = GetTodayWeather();
            int i = 1;
            if (todayWeather != null)
            {
                foreach (string str in todayWeather)
                {
                    SetData($"UPDATE weather SET todayweather = '{todayWeather[i - 1]}' WHERE id={i}");
                    i++;
                }
            }
        }

        private static string[] GetOtherInformation()
        {
            string[] otherInformation = Parser.Parse("//dd [@class='term__value']", true, _link); //ветер, температура вчера, влажность
            string[] label = Parser.Parse("//dt [@class='term__label']", true, _link); // дескрипторы
            string[] sunriseSundownTime = Parser.Parse("//dd [@class='sunrise-sunset__value']", true, _link); //восход, закат
            string[] sunriseSundownLabel = Parser.Parse("//dt [@class='sunrise-sunset__label']", true, _link); // дескрипторы 
            int i = 0;
            foreach (string str in otherInformation)
            {
                otherInformation[i] = $"{label[i]}: {otherInformation[i]}";
                i++;
            }
            i = 0;
            foreach (string str in sunriseSundownTime)
            {
                sunriseSundownTime[i] = $"{sunriseSundownLabel[i]}: {sunriseSundownTime[i]}";
                i++;
            }
            return CheckTrash(otherInformation);
        }

        private static void SetOtherInformation()
        {
            string[] otherInformation = GetOtherInformation();
            int i = 1;
            if (otherInformation != null)
            {
                foreach (string str in otherInformation)
                {
                    SetData($"UPDATE weather SET otherinfo = '{otherInformation[i - 1]}' WHERE id={i}");
                    i++;
                }
            }
        }

        private static string[] CheckTrash(string[] inf) // избавляемся от возможного мусора
        {
            if (inf[0].Contains("Погода"))
                return null;
            else
                return inf;
        }

        private static MySqlConnection OpenTheConnection()
        {
            string connmysql = "server=localhost;charset=cp1251;user=root;database=grabber;password=root;";
            MySqlConnection conn = new MySqlConnection(connmysql);
            conn.Open();
            _conn = conn;
            return conn;
        }

        private static void CloseTheConnection(MySqlConnection conn) => conn.Close();

        private static void SetData(string query)
        {
            MySqlCommand command = new MySqlCommand(query, _conn);
            command.ExecuteNonQuery();
        } 

        private static string GetData(string query)
        {
            MySqlCommand comm = new MySqlCommand(query, _conn);
            string data = comm.ExecuteScalar().ToString();
            comm.ExecuteNonQuery();
            return data;
        }
    }
}
