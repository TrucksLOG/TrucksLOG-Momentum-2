using System.IO;
using System.Windows.Input;
using TrucksLOG.Utilities;

namespace TrucksLOG.ViewModel
{
    class NavigationVM : ViewModelBase
    {
        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");

        private object _currentView;
        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand CustomersCommand { get; set; }
        public ICommand ProductsCommand { get; set; }
        public ICommand OrdersCommand { get; set; }
        public ICommand TransactionsCommand { get; set; }
        public ICommand ShipmentsCommand { get; set; }
        public ICommand SettingsCommand { get; set; }
        public ICommand MessagesCommand { get; set; }
        public ICommand ShowLogCommand { get; set; }
        public ICommand LoginCommand { get; set; }
        public ICommand Login2Command { get; set; }
        public ICommand Login3Command { get; set; }
        public ICommand Login4Command { get; set; }
        public ICommand Login5Command { get; set; }
        public ICommand ImpressumCommand { get; set; }

        private void Home(object obj) => CurrentView = new HomeVM();
        private void Customer(object obj) => CurrentView = new CustomerVM();
        private void Product(object obj) => CurrentView = new ProductVM();
        private void Order(object obj) => CurrentView = new OrderVM();
        private void Transaction(object obj) => CurrentView = new TransactionVM();
        private void Shipment(object obj) => CurrentView = new ShipmentVM();
        private void Setting(object obj) => CurrentView = new SettingVM();
        private void Messages(object obj) => CurrentView = new MessagesVM();
        private void ShowLog(object obj) => CurrentView = new LogFileVM();
        private void ShowLOGIN(object obj) => CurrentView = new LoginVM();
        private void Show2LOGIN(object obj) => CurrentView = new Login2VM();
        private void Show3LOGIN(object obj) => CurrentView = new Login3VM();
        private void Show4LOGIN(object obj) => CurrentView = new Login4VM();
        private void Show5LOGIN(object obj) => CurrentView = new Login5VM();
        private void ShowImpressum(object obj) => CurrentView = new ImpressumVM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            CustomersCommand = new RelayCommand(Customer);
            ProductsCommand = new RelayCommand(Product);
            OrdersCommand = new RelayCommand(Order);
            TransactionsCommand = new RelayCommand(Transaction);
            ShipmentsCommand = new RelayCommand(Shipment);
            SettingsCommand = new RelayCommand(Setting);
            MessagesCommand = new RelayCommand(Messages);
            ShowLogCommand = new RelayCommand(ShowLog);
            LoginCommand = new RelayCommand(ShowLOGIN);
            Login2Command = new RelayCommand(Show2LOGIN);
            Login3Command = new RelayCommand(Show3LOGIN);
            Login4Command = new RelayCommand(Show4LOGIN);
            Login5Command = new RelayCommand(Show5LOGIN);
            ImpressumCommand = new RelayCommand(ShowImpressum);

            // Startup Page

            if (MyIni.KeyExists("STEAM_ID", "USER") && MyIni.KeyExists("ETS_PATH", "GAMES")) 
            {
                CurrentView = new HomeVM();
            } 
            else if (MyIni.KeyExists("STEAM_ID", "USER") && !MyIni.KeyExists("ETS_PATH", "GAMES"))
            {
                CurrentView = new Login2VM();
            } else 
            {
                CurrentView = new LoginVM();
            }
          
        }
    }
}
