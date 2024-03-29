﻿using NLog;
using SCSSdkClient;
using SCSSdkClient.Object;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TrucksLOG.Classes;
using TrucksLOG.Utilities;

namespace TrucksLOG.View
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : UserControl
    {
        private static SCSSdkTelemetry Telemetry;

        public bool InvokeRequired { get; private set; }
        private readonly TruckDaten TruckDaten = new();
        public static readonly IniFile MyIni = new(@"Settings.ini");
        private string STATUS { get; set; }
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

            HOME_ATS_IMG.Visibility = MyIni.KeyExists("ATS_PATH", "GAME") ? Visibility.Visible : Visibility.Collapsed;
        }



        private void Telemetry_Data(SCSTelemetry data, bool updated)
        {
            if (!updated)
                return;
            try
            {
                if (InvokeRequired)
                {
                    // ReSharper disable once ConditionIsAlwaysTrueOrFalse
                    Invoke(new TelemetryData(Telemetry_Data), data, updated);
                    return;
                }
                #region SYSTEM
                TruckDaten.GAME_AKTIV = !data.SdkActive;
                TruckDaten.GAME_PAUSED = data.Paused;
                TruckDaten.SPIEL = data.Game.ToString();
                TruckDaten.TRUCK_RPM_TEXT = data.Game.ToString() == "Ets2" ? TruckDaten.RPM + " U/PM" : TruckDaten.RPM + " RPM";
                TruckDaten.KM_MEILEN = TruckDaten.SPIEL == "Ets2" ? " KM/H" : " mp/h";
                TruckDaten.KM_MEILEN_2 = TruckDaten.SPIEL == "Ets2" ? " KM" : " Mi";
                TruckDaten.EURO_DOLLAR = TruckDaten.SPIEL == "Ets2" ? " €" : " $";
                TruckDaten.TO_LB = TruckDaten.SPIEL == "Ets2" ? " T " : " lb ";
                TruckDaten.LITER_GALLONEN = TruckDaten.SPIEL == "Ets2" ? " L" : " Gal.";

  
                #endregion

                #region TRUCK DATEN
                TruckDaten.SPEED = TruckDaten.SPIEL == "Ets2" ? (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Kph : (int)data.TruckValues.CurrentValues.DashboardValues.Speed.Mph;
                TruckDaten.RPM = (int)data.TruckValues.CurrentValues.DashboardValues.RPM;
                TruckDaten.RPM_MAX = (int)data.TruckValues.ConstantsValues.MotorValues.EngineRpmMax;
                TruckDaten.LKW_MODELL = data.TruckValues.ConstantsValues.Brand;
                TruckDaten.LKW_HERSTELLER = data.TruckValues.ConstantsValues.Brand;
                TruckDaten.LKW_HERSTELLER_ID = data.TruckValues.ConstantsValues.BrandId;
                TruckDaten.LKW_MODELL = data.TruckValues.ConstantsValues.Name;
                TruckDaten.LKW_MODELL_ID = data.TruckValues.ConstantsValues.Id;
                TruckDaten.LKW_LICENSE_PLATE = data.TruckValues.ConstantsValues.LicensePlate;
                TruckDaten.LKW_LICENSE_COUNTRY = data.TruckValues.ConstantsValues.LicensePlateCountry;
                TruckDaten.LKW_LICENSE_COUNTRY_ID = data.TruckValues.ConstantsValues.LicensePlateCountryId;
                TruckDaten.TANKVOLUMEN = TruckDaten.SPIEL == "Ets2" ? data.TruckValues.ConstantsValues.CapacityValues.Fuel : data.TruckValues.ConstantsValues.CapacityValues.Fuel / 3.785;
                TruckDaten.FUEL_GERADE = TruckDaten.SPIEL == "Ets2" ? data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount : data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount / 3.785;
                TruckDaten.FUEL_GERADE_TANKANZEIGE = TruckDaten.SPIEL == "Ets2" ? (int)data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount + " " + TruckDaten.LITER_GALLONEN : (int)(data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount / 3.785) + " " + TruckDaten.LITER_GALLONEN;
                TruckDaten.HANDBREMSE = data.TruckValues.CurrentValues.MotorValues.BrakeValues.ParkingBrake;
                TruckDaten.KILOMETERSTAND = data.TruckValues.CurrentValues.DashboardValues.Odometer;
                TruckDaten.KILOMETERSTAND_DASHBOARD = (int)data.TruckValues.CurrentValues.DashboardValues.Odometer;
                TruckDaten.RESTSTRECKE_MIT_FUEL = TruckDaten.SPIEL == "Ets2" ? (int)Math.Round(data.TruckValues.CurrentValues.DashboardValues.FuelValue.Range, 0) : (int)Math.Round(data.TruckValues.CurrentValues.DashboardValues.FuelValue.Range / 1.609, 0);
                TruckDaten.TEMPOMAT = data.Game.ToString() == "Ets2" ? (int)data.TruckValues.CurrentValues.DashboardValues.CruiseControlSpeed.Kph : (int)data.TruckValues.CurrentValues.DashboardValues.CruiseControlSpeed.Mph;
                var fuel_gerade_2 = (int)Math.Round(data.TruckValues.CurrentValues.DashboardValues.FuelValue.Amount, 0);
                TruckDaten.FUEL_GERADE_TEXT = TruckDaten.SPIEL == "Ets2"
                    ? fuel_gerade_2 + " " + TruckDaten.LITER_GALLONEN + " ( " + TruckDaten.RESTSTRECKE_MIT_FUEL + " " + TruckDaten.KM_MEILEN_2 + " )"
                    : Math.Round(fuel_gerade_2 / 3.785, 0) + " " + TruckDaten.LITER_GALLONEN + " ( " + TruckDaten.RESTSTRECKE_MIT_FUEL + " " + TruckDaten.KM_MEILEN_2 + " )";
                TruckDaten.TANK_LEER = TruckDaten.SPIEL == "Ets2" ? TruckDaten.FUEL_GERADE < 250 ? "Red" : "Orange" : TruckDaten.FUEL_GERADE < 100 ? "Red" : "Orange";
                TruckDaten.ELEKTRIK_AN = data.TruckValues.CurrentValues.ElectricEnabled ? "#FFF" : "#3C3F47";
                TruckDaten.OIL_PRESSURE = data.TruckValues.CurrentValues.DashboardValues.WarningValues.OilPressure;
                TruckDaten.AIR_PRESSURE = data.TruckValues.CurrentValues.DashboardValues.WarningValues.AirPressure;
                TruckDaten.POS_X = data.TruckValues.CurrentValues.PositionValue.Position.X;
                TruckDaten.POS_Y = data.TruckValues.CurrentValues.PositionValue.Position.Y;
                TruckDaten.POS_Z = data.TruckValues.CurrentValues.PositionValue.Position.Z;
                TruckDaten.HEADING = data.TruckValues.CurrentValues.PositionValue.Orientation.Heading;
                TruckDaten.LICHT_AN = data.TruckValues.CurrentValues.LightsValues.Parking ? "#FFF" : "#3C3F47";
                TruckDaten.LICHT_AN_2 = data.TruckValues.CurrentValues.LightsValues.BeamLow ? "#FFF" : "#3C3F47";
                TruckDaten.LICHT_AN_3 = data.TruckValues.CurrentValues.LightsValues.BeamHigh ? "#FFF" : "#3C3F47";
                TruckDaten.WARNLICHT = data.TruckValues.CurrentValues.LightsValues.Beacon ? "#FFF" : "#3C3F47";
                TruckDaten.BLINKER_LINKS = data.TruckValues.CurrentValues.LightsValues.BlinkerLeftOn ? 1 : 0;
                TruckDaten.BLINKER_RECHTS = data.TruckValues.CurrentValues.LightsValues.BlinkerRightOn ? 1 : 0;
                #endregion

                #region TRAILER DATEN
                TruckDaten.TRAILER_ATTACHED = data.TrailerValues[0].Attached;
                TruckDaten.TRAILER_ANGEHANGEN = data.TrailerValues[0].Attached;
                TruckDaten.TRAILER_ID = data.TrailerValues[0].Id ?? "-";
                TruckDaten.TRAILER_LICENSE_PLATE = data.TrailerValues[0].LicensePlate ?? "-";
                TruckDaten.TRAILER_LICENSE_COUNTRY = data.TrailerValues[0].LicensePlateCountry ?? "-";
                TruckDaten.TRAILER_LICENSE_COUNTRY_ID = data.TrailerValues[0].LicensePlateCountryId ?? "-";
                #endregion

                #region AUFTRAG
                TruckDaten.CARGO_LOADED = data.JobValues.CargoLoaded;
                TruckDaten.JOB_AKTIV = data.JobValues.CargoLoaded;
                TruckDaten.STRECKE = (uint)(TruckDaten.SPIEL == "Ets2" ? data.JobValues.PlannedDistanceKm : data.JobValues.PlannedDistanceKm / 1.609);
                TruckDaten.STARTORT = data.JobValues.CitySource;
                TruckDaten.STARTORT_ID = data.JobValues.CitySourceId;
                TruckDaten.STARTFIRMA = data.JobValues.CompanySource;
                TruckDaten.STARTFIRMA_ID = data.JobValues.CompanySourceId;
                TruckDaten.ZIELORT = data.JobValues.CityDestination;
                TruckDaten.ZIELORT_ID = data.JobValues.CityDestinationId;
                TruckDaten.ZIELFIRMA = data.JobValues.CompanyDestination;
                TruckDaten.ZIELFIRMA_ID = data.JobValues.CompanyDestinationId;
                TruckDaten.LADUNG = data.JobValues.CargoValues.Name;
                TruckDaten.LADUNG_ID = data.JobValues.CargoValues.Id;
                TruckDaten.SPEZIAL_EVENT = data.JobValues.SpecialJob;
                TruckDaten.ABGABE_KM = (uint)data.GamePlay.JobDelivered.DistanceKm;
                TruckDaten.REST_KM = (uint)(TruckDaten.SPIEL == "Ets2" ? data.NavigationValues.NavigationDistance / 1000 : (data.NavigationValues.NavigationDistance / 1.609 / 1000));
                TruckDaten.REST_KM_FLOAT = (float)(TruckDaten.SPIEL == "Ets2" ? data.NavigationValues.NavigationDistance / 1000 : (data.NavigationValues.NavigationDistance / 1.609 / 1000));
                TruckDaten.FRACHTMARKT = data.JobValues.Market.ToString();
                TruckDaten.EINKOMMEN = data.JobValues.Income;
                TruckDaten.JOB_GEF_STRECKE = (uint)(TruckDaten.TRAILER_ANGEHANGEN ? TruckDaten.STRECKE - TruckDaten.REST_KM : 0);
                TruckDaten.KM_PREIS = (float)Math.Round((TruckDaten.EINKOMMEN - 600) / (float)TruckDaten.STRECKE, 2);
                TruckDaten.GEWICHT = TruckDaten.SPIEL == "Ets2" ? (int)(data.JobValues.CargoValues.Mass / 1000) : (int)(data.JobValues.CargoValues.Mass * 2.20461);
                TruckDaten.EARNED_XP = data.GamePlay.JobDelivered.EarnedXp;
                TruckDaten.EINNAHMEN = data.GamePlay.JobDelivered.Revenue;
                TruckDaten.AUTOLOADING = data.GamePlay.JobDelivered.AutoLoaded;
                TruckDaten.AUTOPARKING = data.GamePlay.JobDelivered.AutoParked;
                
                #endregion

                #region SCHÄDEN TRUCK / TRAILER
                TruckDaten.FRACHTSCHADEN = (float)Math.Round(data.JobValues.CargoValues.CargoDamage * 100, 2);
                TruckDaten.FRACHTSCHADEN_DASHBOARD = (float)Math.Round(data.JobValues.CargoValues.CargoDamage * 100, 2);
                var schaden1 = data.TruckValues.CurrentValues.DamageValues.Cabin * 100;
                var schaden2 = data.TruckValues.CurrentValues.DamageValues.WheelsAvg * 100;
                var schaden3 = data.TruckValues.CurrentValues.DamageValues.Chassis * 100;
                var schaden4 = data.TruckValues.CurrentValues.DamageValues.Engine * 100;
                var schaden5 = data.TruckValues.CurrentValues.DamageValues.Transmission * 100;
                TruckDaten.LKWSCHADEN_DASHBOARD = (float)Math.Round((new[] { schaden1, schaden2, schaden3, schaden4, schaden5 }).Max(), 2);
                #endregion

                #region STRAFEN
                TruckDaten.STRAFE_GEBUEHR = (int)data.GamePlay.FinedEvent.Amount;
                TruckDaten.STRAFE_VERGEHEN = data.GamePlay.FinedEvent.Offence.ToString();
                TruckDaten.STRAFE_JOB_CANCEL = (int)data.GamePlay.JobCancelled.Penalty;
                #endregion

                #region TANKEN
                TruckDaten.LITER_GETANKT = data.GamePlay.RefuelEvent.Amount;
                #endregion

                #region TRANSPORT FÄHRE
                TruckDaten.TRANSPORT_GEBUEHR_FÄHRE = (int)data.GamePlay.FerryEvent.PayAmount;
                TruckDaten.TRANSPORT_STARTORT_FÄHRE = data.GamePlay.FerryEvent.SourceName;
                TruckDaten.TRANSPORT_ZIELORT_FÄHRE = data.GamePlay.FerryEvent.TargetName;
                TruckDaten.TRANSPORT_GEBUEHR_ZUG = (int)data.GamePlay.TrainEvent.PayAmount;
                TruckDaten.TRANSPORT_STARTORT_ZUG = data.GamePlay.TrainEvent.SourceName;
                TruckDaten.TRANSPORT_ZIELORT_ZUG = data.GamePlay.TrainEvent.TargetName;
                TruckDaten.INFOTEXT_1 = TruckDaten.GEWICHT + TruckDaten.TO_LB + TruckDaten.LADUNG_ID;
                TruckDaten.INFOTEXT_2 = TruckDaten.STARTORT_ID;
                #endregion

                #region MAX SPEED BERECHNUNG
                if (TruckDaten.SPEED > TruckDaten.MAX_SPEED_HEUTE)
                    TruckDaten.MAX_SPEED_HEUTE = TruckDaten.SPEED;
                if (data.TrailerValues[0].Attached && TruckDaten.SPEED > TruckDaten.MAX_SPEED_TOUR)
                    TruckDaten.MAX_SPEED_TOUR = TruckDaten.SPEED;
                TruckDaten.MAUTSTATION_GEBUEHR = (int)data.GamePlay.TollgateEvent.PayAmount;
                #endregion

                #region STRAFEN
                TruckDaten.STRAFE_GEBUEHR = (int)data.GamePlay.FinedEvent.Amount;
                TruckDaten.STRAFE_VERGEHEN = data.GamePlay.FinedEvent.Offence.ToString();
                TruckDaten.STRAFE_JOB_CANCEL = (int)data.GamePlay.JobCancelled.Penalty;
                #endregion

                #region TANKEN
                TruckDaten.LITER_GETANKT = data.GamePlay.RefuelEvent.Amount;
                #endregion

                #region TRANSPORT FÄHRE
                TruckDaten.TRANSPORT_GEBUEHR_FÄHRE = (int)data.GamePlay.FerryEvent.PayAmount;
                TruckDaten.TRANSPORT_STARTORT_FÄHRE = data.GamePlay.FerryEvent.SourceName;
                TruckDaten.TRANSPORT_ZIELORT_FÄHRE = data.GamePlay.FerryEvent.TargetName;
                TruckDaten.TRANSPORT_GEBUEHR_ZUG = (int)data.GamePlay.TrainEvent.PayAmount;
                TruckDaten.TRANSPORT_STARTORT_ZUG = data.GamePlay.TrainEvent.SourceName;
                TruckDaten.TRANSPORT_ZIELORT_ZUG = data.GamePlay.TrainEvent.TargetName;
                TruckDaten.INFOTEXT_1 = TruckDaten.GEWICHT + TruckDaten.TO_LB + TruckDaten.LADUNG_ID;
                TruckDaten.INFOTEXT_2 = TruckDaten.STARTORT_ID;
                #endregion

            }
            catch (Exception ex)
            {
                // ignored atm i found no proper way to shut the telemetry down and down call this anymore when this or another thing is already disposed
                Console.WriteLine("Telemetry was closed: " + ex);
            }
        }


        private static object Invoke(Delegate method, object value, bool updated) => Invoke(method, null, false);


        private void TelemetryOnJobStarted(object sender, EventArgs e)
        {

            if(STATUS == "AUF FAHRT") { return; }

            JobData job = new()
            {
                STARTORT = TruckDaten.STARTORT,
                STARTORT_ID = TruckDaten.STARTORT_ID,
                STARTFIRMA = TruckDaten.STARTFIRMA,
                STARTFIRMA_ID = TruckDaten.STARTFIRMA_ID,
                ZIELORT = TruckDaten.ZIELORT,
                ZIELORT_ID = TruckDaten.ZIELORT_ID,
                ZIELFIRMA = TruckDaten.ZIELFIRMA,
                ZIELFIRMA_ID= TruckDaten.ZIELFIRMA_ID,
                LADUNG = TruckDaten.LADUNG,
                LADUNG_ID = TruckDaten.LADUNG_ID,
                GEWICHT = TruckDaten.GEWICHT,
                EINKOMMEN = TruckDaten.EINKOMMEN,
                GESAMT_STRECKE = TruckDaten.STRECKE,
                FRACHTMARKT = TruckDaten.FRACHTMARKT,
                LKW_HERSTELLER = TruckDaten.LKW_HERSTELLER,
                LKW_HERSTELLER_ID = TruckDaten.LKW_HERSTELLER_ID,
                LKW_MODELL = TruckDaten.LKW_MODELL,
                LKW_MODELL_ID = TruckDaten.LKW_MODELL_ID,
                LKW_LICENSE_PLATE = TruckDaten.LKW_LICENSE_PLATE,
                LKW_LICENSE_COUNTRY = TruckDaten.LKW_LICENSE_COUNTRY,
                LKW_LICENSE_COUNTRY_ID = TruckDaten.LKW_LICENSE_COUNTRY_ID,
                SPIEL = TruckDaten.SPIEL
            };

            if (MyIni.CHECK_TOUR(job.ZIELORT_ID, job.ZIELFIRMA_ID, job.STARTORT_ID, job.STARTFIRMA_ID, job.LADUNG_ID))
            {
                // WENN TOUR EXISTIERT!!!
                TruckDaten.TOUR_ID = uint.Parse(MyIni.Read("TOUR_ID", "AKTUELLE_TOUR"));
            }
            else
            {
                // WENN TOUR NICHT EXISTIERT!!!
                DB.TOUR_INSERT(job);
                TruckDaten.TOUR_ID = uint.Parse(MyIni.Read("TOUR_ID", "AKTUELLE_TOUR"));
            }
            STATUS = "AUF FAHRT";

            Wait(2000);

        }
        private void TelemetryJobDelivered(object sender, EventArgs e) 
        {
            JobData jobend = new()
            {
                FRACHTSCHADEN = TruckDaten.FRACHTSCHADEN,
                EINNAHMEN = TruckDaten.EINNAHMEN,
                JOB_GEF_STRECKE = TruckDaten.JOB_GEF_STRECKE,
                REST_STRECKE = TruckDaten.REST_KM,
                ODO_ENDE = TruckDaten.KILOMETERSTAND
            };

            DB.TOUR_END(jobend);
            STATUS = "ABGESCHLOSSEN";
            TruckDaten.TOUR_ID = 0;
            Wait(2000);

        }

        private void TelemetryJobCancelled(object sender, EventArgs e)
        {

            
         
        }

        private void TelemetryFerry(object sender, EventArgs e)
        {

            if (TruckDaten.GAME_PAUSED)
                return;
        }

        private void TelemetryFined(object sender, EventArgs e)
        {

            if (TruckDaten.GAME_PAUSED)
                return;
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



        private void Image_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                ProcessStartInfo startInfo = new(MyIni.Read("ETS_PATH", "GAMES"))
                {
                    Arguments = MyIni.Read("ETS_ARGUMENTS", "GAMES")
                };
                Process.Start(startInfo);
            }
            catch (Exception Er_ETS2)
            {
                MainWindow.Logger.Error("Fehler in STARTE_ETS_SINGLE: " + Er_ETS2.Message + Er_ETS2.Source);
            }
        }

        private void Image_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            try
            {
                if(MyIni.KeyExists("ATS_PATH", "GAMES"))
                {
                    ProcessStartInfo startInfo = new(MyIni.Read("ATS_PATH", "GAMES"))
                    {
                        Arguments = MyIni.Read("ATS_ARGUMENTS", "GAMES")
                    };
                    Process.Start(startInfo);
                } else
                {
                    MessageBox.Show("Kein ATS-Pfad angegeben!", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception Er_ATS2)
            {
                MainWindow.Logger.Error("Fehler in STARTE_ATS_SINGLE: " + Er_ATS2.Message + Er_ATS2.Source);
            }
        }

        private void Kein_Steam_ID_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new Login();
        }

        private void FINISH_Click(object sender, RoutedEventArgs e)
        {

            this.TelemetryJobDelivered(null, null);

        }

        public static async void Wait(int milliseconds)
        {
            await Task.Delay(milliseconds);
        }

    }


}
