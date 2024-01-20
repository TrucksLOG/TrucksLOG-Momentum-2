using TrucksLOG.Utilities;
using SCSSdkClient;
using SCSSdkClient.Object;
using System;
using System.Windows;
using System.Windows.Controls;

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

        }

        private void Telemetry_Data(SCSTelemetry data, bool updated)
        {
            if (!updated)
                return;
            try
            {
                if (InvokeRequired)
                {
                    Invoke(new TelemetryData(Telemetry_Data), data, updated);
                    return;
                }

                TruckDaten.SPEED = (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph;
                TruckDaten.RPM = (int)data.TruckValues.CurrentValues.DashboardValues.RPM;
                TruckDaten.FUEL_GERADE_INT = (int)data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount;
                TruckDaten.STARTORT = data.JobValues.CitySource.Trim();
                TruckDaten.ZIELORT = data.JobValues.CityDestination.Trim();
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

        private void TelemetryFerry(object sender, EventArgs e) =>
            MessageBox.Show("Ferry");

        private void TelemetryFined(object sender, EventArgs e) =>
            MessageBox.Show("Fined");

        private void TelemetryJobCancelled(object sender, EventArgs e) =>
            MessageBox.Show("Job Cancelled");

        private void TelemetryJobDelivered(object sender, EventArgs e) =>
            MessageBox.Show("Job Delivered");

        private void TelemetryOnJobStarted(object sender, EventArgs e) =>
                                                            MessageBox.Show("Just started job OR loaded game with active.");

        private void TelemetryRefuel(object sender, EventArgs e) {}

        private void TelemetryRefuelEnd(object sender, EventArgs e) { }

        private void TelemetryRefuelPayed(object sender, EventArgs e) { }

        private void TelemetryTollgate(object sender, EventArgs e) =>
                                    MessageBox.Show("Tollgate");

        private void TelemetryTrain(object sender, EventArgs e) =>
            MessageBox.Show("Train");

    }
    

}
