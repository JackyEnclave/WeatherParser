﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WpfForParser.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IService1")]
    public interface IService1 {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetNowWeather", ReplyAction="http://tempuri.org/IService1/GetNowWeatherResponse")]
        string GetNowWeather();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetNowWeather", ReplyAction="http://tempuri.org/IService1/GetNowWeatherResponse")]
        System.Threading.Tasks.Task<string> GetNowWeatherAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTodayWeather", ReplyAction="http://tempuri.org/IService1/GetTodayWeatherResponse")]
        string[] GetTodayWeather();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetTodayWeather", ReplyAction="http://tempuri.org/IService1/GetTodayWeatherResponse")]
        System.Threading.Tasks.Task<string[]> GetTodayWeatherAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetOtherInformation", ReplyAction="http://tempuri.org/IService1/GetOtherInformationResponse")]
        string[] GetOtherInformation();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetOtherInformation", ReplyAction="http://tempuri.org/IService1/GetOtherInformationResponse")]
        System.Threading.Tasks.Task<string[]> GetOtherInformationAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRegionArr", ReplyAction="http://tempuri.org/IService1/GetRegionArrResponse")]
        string[] GetRegionArr();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetRegionArr", ReplyAction="http://tempuri.org/IService1/GetRegionArrResponse")]
        System.Threading.Tasks.Task<string[]> GetRegionArrAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRegionToMySQL", ReplyAction="http://tempuri.org/IService1/SetRegionToMySQLResponse")]
        void SetRegionToMySQL(string town);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/SetRegionToMySQL", ReplyAction="http://tempuri.org/IService1/SetRegionToMySQLResponse")]
        System.Threading.Tasks.Task SetRegionToMySQLAsync(string town);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetError", ReplyAction="http://tempuri.org/IService1/GetErrorResponse")]
        string GetError();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/GetError", ReplyAction="http://tempuri.org/IService1/GetErrorResponse")]
        System.Threading.Tasks.Task<string> GetErrorAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Refresh", ReplyAction="http://tempuri.org/IService1/RefreshResponse")]
        void Refresh();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService1/Refresh", ReplyAction="http://tempuri.org/IService1/RefreshResponse")]
        System.Threading.Tasks.Task RefreshAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IService1Channel : WpfForParser.ServiceReference1.IService1, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service1Client : System.ServiceModel.ClientBase<WpfForParser.ServiceReference1.IService1>, WpfForParser.ServiceReference1.IService1 {
        
        public Service1Client() {
        }
        
        public Service1Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service1Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service1Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetNowWeather() {
            return base.Channel.GetNowWeather();
        }
        
        public System.Threading.Tasks.Task<string> GetNowWeatherAsync() {
            return base.Channel.GetNowWeatherAsync();
        }
        
        public string[] GetTodayWeather() {
            return base.Channel.GetTodayWeather();
        }
        
        public System.Threading.Tasks.Task<string[]> GetTodayWeatherAsync() {
            return base.Channel.GetTodayWeatherAsync();
        }
        
        public string[] GetOtherInformation() {
            return base.Channel.GetOtherInformation();
        }
        
        public System.Threading.Tasks.Task<string[]> GetOtherInformationAsync() {
            return base.Channel.GetOtherInformationAsync();
        }
        
        public string[] GetRegionArr() {
            return base.Channel.GetRegionArr();
        }
        
        public System.Threading.Tasks.Task<string[]> GetRegionArrAsync() {
            return base.Channel.GetRegionArrAsync();
        }
        
        public void SetRegionToMySQL(string town) {
            base.Channel.SetRegionToMySQL(town);
        }
        
        public System.Threading.Tasks.Task SetRegionToMySQLAsync(string town) {
            return base.Channel.SetRegionToMySQLAsync(town);
        }
        
        public string GetError() {
            return base.Channel.GetError();
        }
        
        public System.Threading.Tasks.Task<string> GetErrorAsync() {
            return base.Channel.GetErrorAsync();
        }
        
        public void Refresh() {
            base.Channel.Refresh();
        }
        
        public System.Threading.Tasks.Task RefreshAsync() {
            return base.Channel.RefreshAsync();
        }
    }
}
