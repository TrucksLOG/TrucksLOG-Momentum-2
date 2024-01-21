using TrucksLOG.Utilities;
using SCSSdkClient;
using SCSSdkClient.Object;
using System;
using System.Windows;
using System.Windows.Controls;
using TrucksLOG.Classes;

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
                DASHBOARD_STACK.Visibility = Visibility.Visible;
                NO_STEAM_STACK.Visibility= Visibility.Collapsed;
            } else
            {
                DASHBOARD_STACK.Visibility = Visibility.Collapsed;
                NO_STEAM_STACK.Visibility = Visibility.Visible;
            }
        }



        private void Telemetry_Data(SCSTelemetry data, bool updated)
        {
            if (!updated)
                return;
            try
            {
                TruckDaten.SPEED = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph;
                TruckDaten.RPM = (int)data.TruckValues.CurrentValues.DashboardValues.RPM;
                TruckDaten.FUEL_GERADE_INT = (int)data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount;
                TruckDaten.STARTORT = data.JobValues.CitySource.Trim();
                TruckDaten.STARTFIRMA = data.JobValues.CompanySource.Trim();
                TruckDaten.ZIELORT = data.JobValues.CityDestination.Trim();
                TruckDaten.ZIELFIRMA = data.JobValues.CompanyDestination.Trim();
                TruckDaten.LADUNG = data.JobValues.CargoValues.Name.Trim();
                TruckDaten.EINKOMMEN = (ulong)data.JobValues.Income;
                TruckDaten.REST_KM = (uint)(TruckDaten.SPIEL == "Ets2" ? data.NavigationValues.NavigationDistance / 1000 : (data.NavigationValues.NavigationDistance / 1.609 / 1000));

            }
            catch (Exception ex)
            {
                // ignored atm i found no proper way to shut the telemetry down and down call this anymore when this or another thing is already disposed
                Console.WriteLine("Telemetry was closed: " + ex);
            }
        }

        private void Invoke(TelemetryData telemetryData, SCSTelemetry data, bool updated)
        {
            throw new NotImplementedException();
        }

        private void TelemetryFerry(object sender, EventArgs e) { }

        private void TelemetryFined(object sender, EventArgs e) { }

        private void TelemetryJobCancelled(object sender, EventArgs e) { }

        private void TelemetryJobDelivered(object sender, EventArgs e) { }

        private void TelemetryOnJobStarted(object sender, EventArgs e) {

            JobData job = new()
            {
                SPEZIAL_EVENT = TruckDaten.SPEZIAL_EVENT.ToString(),
                INTERNE_ID = DB.RandomString(),
                STARTORT = TruckDaten.STARTORT,
                STARTFIRMA = TruckDaten.STARTFIRMA,
                ZIELORT = TruckDaten.ZIELORT,
                ZIELFIRMA = TruckDaten.ZIELFIRMA,
                LADUNG = TruckDaten.LADUNG,
                GEWICHT = TruckDaten.GEWICHT,
                EINKOMMEN = TruckDaten.EINKOMMEN
            };
            int returntype = DB.INSERT_TOUR(job);

            if (returntype > 0 )
            {
                TourIni.Write("TOUR_ID", job.INTERNE_ID, job.INTERNE_ID);
                TourIni.Write("STARTORT", job.STARTORT, job.INTERNE_ID);
                TourIni.Write("ZIELORT", job.ZIELORT, job.INTERNE_ID);
                TourIni.Write("STARFIRMA", job.STARTFIRMA, job.INTERNE_ID);
                TourIni.Write("ZIELFIRMA", job.ZIELFIRMA, job.INTERNE_ID);
                TourIni.Write("TOUR_STATUS", "AUF FAHRT", job.INTERNE_ID);
            }
            else
            {
                TourIni.Write("TOUR_ID", job.INTERNE_ID, job.INTERNE_ID);
                TourIni.Write("TOUR_STATUS", "FEHLER beim Eintrag. Return-ID: " + returntype, job.INTERNE_ID);
            }

        }

        private void TelemetryRefuel(object sender, EventArgs e) { }

        private void TelemetryRefuelEnd(object sender, EventArgs e) { }

        private void TelemetryRefuelPayed(object sender, EventArgs e) { }

        private void TelemetryTollgate(object sender, EventArgs e) { }

        private void TelemetryTrain(object sender, EventArgs e) { }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new Login();
        }
    }
    

}
