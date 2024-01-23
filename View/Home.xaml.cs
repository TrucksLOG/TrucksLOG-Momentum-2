using TrucksLOG.Utilities;
using SCSSdkClient;
using SCSSdkClient.Object;
using System;
using System.Windows;
using System.Windows.Controls;
using TrucksLOG.Classes;
using System.Media;
using NLog;
using System.Diagnostics.Eventing.Reader;

namespace TrucksLOG.View
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        public static SCSSdkTelemetry Telemetry;

        public bool InvokeRequired { get; private set; }
        private readonly TruckDaten TruckDaten = new();
        public static readonly IniFile MyIni = new IniFile(@"Settings.ini");
        public static readonly IniFile TourIni = new IniFile(@"TOUR_DATA.ini");

        public Home()
        {
            InitializeComponent();

            Telemetry = new SCSSdkTelemetry();

            Telemetry.Data += Telemetry_Data;
            Telemetry.JobStarted += TelemetryOnJobStarted;
            Telemetry.JobCancelled += TelemetryJobCancelled;
            Telemetry.JobDelivered += TelemetryJobDelivered;
            Telemetry.Fined += TelemetryFined;
            Telemetry.Tollgate += TelemetryTollgate;
            Telemetry.Ferry += TelemetryFerry;
            Telemetry.Train += TelemetryTrain;
            Telemetry.RefuelPayed += TelemetryRefuelPayed;
            Telemetry.RefuelEnd += TelemetryRefuelEnd;
            this.DataContext = TruckDaten;

            if (MyIni.KeyExists("STEAM_ID", "USER") && long.Parse(MyIni.Read("STEAM_ID", "USER")) > 10)
            {
                NO_STEAM_STACK.Visibility= Visibility.Collapsed;
            } else
            {
                NO_STEAM_STACK.Visibility = Visibility.Visible;
            }
        }



        private void Telemetry_Data(SCSTelemetry data, bool updated)
        {
            if (!updated)
                return;
            try
            {
                TruckDaten.GAME_PAUSED = data.Paused;
                TruckDaten.SPIEL = data.Game.ToString();
                TruckDaten.SPEED = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph;
                TruckDaten.RPM = (int)data.TruckValues.CurrentValues.DashboardValues.RPM;
                TruckDaten.FUEL_GERADE_INT = (int)data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount;
                TruckDaten.STARTORT = data.JobValues.CitySource.Trim();
                TruckDaten.STARTFIRMA = data.JobValues.CompanySource.Trim();
                TruckDaten.ZIELORT = data.JobValues.CityDestination.Trim();
                TruckDaten.ZIELFIRMA = data.JobValues.CompanyDestination.Trim();
                TruckDaten.LADUNG = data.JobValues.CargoValues.Name.Trim();
                TruckDaten.GEWICHT = (int)data.JobValues.CargoValues.Mass;
                TruckDaten.EINKOMMEN = (ulong)data.JobValues.Income;
                TruckDaten.REST_KM = (uint)(TruckDaten.SPIEL == "Ets2" ? data.NavigationValues.NavigationDistance / 1000 : (data.NavigationValues.NavigationDistance / 1.609 / 1000));
                TruckDaten.RPM = (int)data.TruckValues.CurrentValues.DashboardValues.RPM;
                TruckDaten.FUEL_GERADE_INT = (int)(TruckDaten.SPIEL == "Ets2" ? data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount : data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount / 3.785);
                TruckDaten.FUEL_AVERAGE = Math.Round(data.TruckValues.CurrentValues.DashboardValues.FuelValue.AverageConsumption, 2);
                TruckDaten.FUEL_MAX = (int)data.TruckValues.ConstantsValues.CapacityValues.Fuel;
                TruckDaten.RPM_MAX = (int)data.TruckValues.ConstantsValues.MotorValues.EngineRpmMax;



                // FRACHTSCHADEN
                TruckDaten.FRACHTSCHADEN = JobData.Berechne_Frachtschaden(data.JobValues.CargoValues.CargoDamage);

            }
            catch (Exception ex)
            {
                // ignored atm i found no proper way to shut the telemetry down and down call this anymore when this or another thing is already disposed
                Console.WriteLine("Telemetry was closed: " + ex);
            }
        }

        private void TelemetryFerry(object sender, EventArgs e) {

            if (TruckDaten.GAME_PAUSED)
                return;
        }

        private void TelemetryFined(object sender, EventArgs e) {

            if (TruckDaten.GAME_PAUSED)
                return;
        }

        private void TelemetryJobCancelled(object sender, EventArgs e) {
        }

        private void TelemetryJobDelivered(object sender, EventArgs e) {


        }

        private void TelemetryOnJobStarted(object sender, EventArgs e) {
      
            
        }

        private void TelemetryRefuel(object sender, EventArgs e) {

            if (TruckDaten.GAME_PAUSED)
                return;
        }

        private void TelemetryRefuelEnd(object sender, EventArgs e) {

            if (TruckDaten.GAME_PAUSED)
                return;
        }

        private void TelemetryRefuelPayed(object sender, EventArgs e) {

            if (TruckDaten.GAME_PAUSED)
                return;
        }

        private void TelemetryTollgate(object sender, EventArgs e) {

            if (TruckDaten.GAME_PAUSED)
                return;
        }

        private void TelemetryTrain(object sender, EventArgs e) {

            if (TruckDaten.GAME_PAUSED)
                return;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new Login();
        }
    }
    

}
