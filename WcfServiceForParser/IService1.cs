using System.ServiceModel;

namespace WcfServiceForParser
{
    // ПРИМЕЧАНИЕ. Команду "Переименовать" в меню "Рефакторинг" можно использовать для одновременного изменения имени интерфейса "IService1" в коде и файле конфигурации.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetNowWeather();
        [OperationContract]
        string[] GetTodayWeather();
        [OperationContract]
        string[] GetOtherInformation();
        [OperationContract]
        string[] GetRegionArr();
        [OperationContract]
        void SetRegionToMySQL(string town);
        [OperationContract]
        void Refresh();
    }
}
