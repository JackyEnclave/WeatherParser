using System.ComponentModel;
using DevExpress.Mvvm;
using System.Collections.Generic;

namespace WpfForParser.VM
{
    class ViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public ViewModel()
        {
            _regionArr = Model.Model.GetRegion();
            _regionArrForFilter = _regionArr;
            RegionArr = _regionArr; 
            _check = "Hidden"; // скрываем лишние элементы окна
            _checkError = "Hidden";
        }

        private string _selectedObject;
        public string SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                _selectedObject = value;
                RaisePropertyChanged(() => SelectedObject);
                if (_selectedObject != null)
                {
                    Model.Model.SetRegion(_selectedObject);
                    try
                    {
                        _regionArr = Model.Model.GetRegion(); // меняем список городов или регионов в зависимости от выбора пользователя
                        RegionArr = _regionArr;
                        _checkError = "Hidden";
                        CheckError = _checkError;
                    }
                    catch
                    {
                        _checkError = "Visible";
                        CheckError = _checkError;
                    }
                    _regionArrForFilter = _regionArr;
                    _nowWeather = Model.Model.GetNowWeather(); // погода сейчас
                    NowWeather = _nowWeather;
                    _todayWeather = Model.Model.GetTodayWeather(); // погода сегодня
                    TodayWeather = _todayWeather;
                    _otherInfo = Model.Model.GetOtherInformation();  // информация о ветре, давлении и пр.
                    OtherInfo = _otherInfo;

                    if (_nowWeather == null || _nowWeather == "") // скрываем лишние элементы окна
                    {
                        _check = "Hidden";
                        CheckNowWeather = "Hidden";
                    }
                    else
                    {
                        _check = "Visible";
                        CheckNowWeather = "Visible";
                    }

                }
            }
        }

        private static string[] _regionArrForFilter;
        // механизм поиска
        private string _filterObject;
        public string FilterObject
        {
            get { return _filterObject; }
            set
            {
                _filterObject = value;

                RaisePropertyChanged(() => FilterObject);
                var list = new List<string>();
                foreach (string str in _regionArrForFilter)
                {
                    if (str != null && str.Contains(FilterObject))
                    {
                        list.Add(str);
                    }
                }
                _regionArr = list.ToArray();
                RegionArr = _regionArr;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        // получаем список городов или регионов
        private string[] _regionArr;
        public string[] RegionArr
        {
            get { return _regionArr; }
            set
            {
                OnPropertyChanged(nameof(RegionArr));
            }
        }

        // погода сейчас
        private string _nowWeather;
        public string NowWeather
        {
            get { return _nowWeather; }
            set
            {
                OnPropertyChanged(nameof(NowWeather));
            }
        }

        // погода сегодня
        private string[] _todayWeather;
        public string[] TodayWeather
        {
            get { return _todayWeather; }
            set
            {
                OnPropertyChanged(nameof(TodayWeather));
            }
        }

        // ветер, давление и прочее
        private string[] _otherInfo;
        public string[] OtherInfo
        {
            get { return _otherInfo; }
            set
            {
                OnPropertyChanged(nameof(OtherInfo));
            }
        }

        // видимость элементов окна
        private string _check;
        public string CheckNowWeather
        {
            get { return _check; }
            set
            {
                OnPropertyChanged(nameof(CheckNowWeather));
            }
        }

        // обработка отсутствия связи
        private string _checkError;
        public string CheckError
        {
            get { return _checkError; }
            set
            {
                OnPropertyChanged(nameof(CheckError));
            }
        }
    }
}