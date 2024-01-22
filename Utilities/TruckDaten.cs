using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TrucksLOG.Utilities
{
    public class TruckDaten : INotifyPropertyChanged
        {
            public string T_1 = "fster5gdf11gdg33w563";
            public uint telemetry_version;
            public uint scssdk_version;
            public bool game_paused;
            public bool onjob;
            // TRUCK
            private string lkw_hersteller;
            private string lkw_hersteller_id;
            private string lkw_modell;
            private string lkw_modell_id;
            private bool cargo_loaded;
            private int speed = 999;
            private bool trailer_attached;
            private bool trailer_angehangen;
            private double tankvolumen;
            private double fuel_gerade;
            private int fuel_gerade_int;
            private double fuel_average;
            private int fuel_max;
            private string fuel_gerade_Text;
            private string fuel_gerade_tankanzeige;
            private float kilometerstand;
            private int kilometerstand_dashboard;
            private int max_speed_tour;
            private int reststrecke_mit_fuel;
            private string reststrecke_mit_fuel_text;
            private double Zu_schnell;
            private int zu_schnell_sekunden;
            private int max_speed_heute;
            private double pos_x;
            private double pos_y;
            private double pos_z;
            private double heading;
            private int rpm;
            private int rpm_max;
            private string lkw_license_plate;
            private string lkw_license_country;
            private string lkw_license_country_id;
            private string trailer_license_plate;
            private string trailer_license_country;
            private string trailer_license_country_id;
            private string elektrik_an;
            private string licht_an;
            private string licht_an_2;
            private string licht_an_3;
            private string aux_licht_an;
            private string warnlicht;
            private int tempomat;
            private double blinker_links;
            private double blinker_rechts;
            private bool handbremse;
            private bool oil_pressure;
            private bool air_pressure;

            // SETTINGS
            private bool game_aktiv;
            private bool job_aktiv;
            private string client_version;
            private string pfad_ets;
            private string pfad_ets_profile;
            private string pfad_ats;
            private string pfad_ats_profile;
            private string pfad_tmp;
            private bool werbeanzeige;
            private string infotext_1;
            private string infotext_2;
            private string infotext_3;
            private string km_meilen;
            private string km_meilen_2;
            private string to_lb;
            private string liter_gallonen;
            private int speed_limit;
            private bool speed_limit_image;
            private string real_eco_modus;
            private Uri waiting_screen_image;
            private double afk_inter;


            // JOB
            private string frachtmarkt;
            private float gewicht;
            private ulong einkommen;
            private string tour_id;
            private string startort;
            private string startort_id;
            private string startfirma;
            private string startfirma_id;
            private string zielort;
            private string zielort_id;
            private string zielfirma;
            private string zielfirma_id;
            private string trailer_id;
            private string ladung;
            private bool spezial_event;
            private string ladung_id;
            private uint strecke;
            private uint rest_km;
            private float rest_km_float;
            private string spiel;
            private int job_gef_strecke;
            private float frachtschaden;
            private float frachtschaden_dashboard;
            private float lkwschaden_dashboard;
            private TimeSpan rest_zeit;
            private bool jobfinished;
            private float km_preis;
            private string euro_dollar;
            private int kurze_idnummer;
            private int earned_xp;
            private long einnahmen;
            private string status_tour;
            private uint abgabe_km;

            // TANKEN
            private float liter_getankt;

            // MAUT
            private int mautstation_gebühr;

            // STRAFEN
            private int strafe_gebühr;
            private string strafe_vergehen;
            private int strafe_job_cancel;

            // TRAIN - FERRY
            private int transport_gebühr_fähre;
            private int transport_gebühr_zug;
            private string transport_startort_fähre;
            private string transport_zielort_fähre;
            private string transport_startort_zug;
            private string transport_zielort_zug;

            // USER
            private string user_nickname;
            private string user_spedition;
            private string anti_afk_text_1;
            private string anti_afk_text_2;
            private string anti_afk_text_3;
            private string tank_leer;
            private int rang;
            private string username;
            private string pass;
            private string startoptionen;
            private string site_font_color;
            private string truck_rpm_text;
            private string cpu_id;
            private string cpu;
            private string windows_version;
            private string mb_serial;
            private string fahrerliste_header;
            private int cpu_text;
            private double music_volume;
            private double sound_volume_fx;
            private string theme;
            private int anz_touren;
            private string punkte;
            private string sponsor;
            private int speed_limit_player;

        // IMPLEMENTS

        public int FUEL_MAX
        {
            get => fuel_max;
            set
            {
                if (fuel_max != value)
                {
                    fuel_max = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public double FUEL_AVERAGE
        {
            get => fuel_average;
            set
            {
                if (fuel_average != value)
                {
                    fuel_average = value;
                    NotifyPropertyChanged();
                }
            }
        }
        public string USER_NICKNAME
            {
                get => user_nickname;
                set
                {
                    if (user_nickname != value)
                    {
                        user_nickname = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int SPEED_LIMIT_PLAYER
            {
                get => speed_limit_player;
                set
                {
                    if (speed_limit_player != value)
                    {
                        speed_limit_player = value;
                        NotifyPropertyChanged();
                    }
                }
            }
            public string SPONSOR
            {
                get => sponsor;
                set
                {
                    if (sponsor != value)
                    {
                        sponsor = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public bool ONJOB
            {
                get => onjob;
                set
                {
                    if (onjob != value)
                    {
                        onjob = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int KILOMETERSTAND_DASHBOARD
            {
                get => kilometerstand_dashboard;
                set
                {
                    if (kilometerstand_dashboard != value)
                    {
                        kilometerstand_dashboard = value;
                        NotifyPropertyChanged();
                    }
                }
            }
            public uint ABGABE_KM
            {
                get => abgabe_km;
                set
                {
                    if (abgabe_km != value)
                    {
                        abgabe_km = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string STATUS_TOUR
            {
                get => status_tour;
                set
                {
                    if (status_tour != value)
                    {
                        status_tour = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public long EINNAHMEN
            {
                get => einnahmen;
                set
                {
                    if (einnahmen != value)
                    {
                        einnahmen = value;
                        NotifyPropertyChanged();
                    }
                }
            }
            public string PUNKTE
            {
                get => punkte;
                set
                {
                    if (punkte != value)
                    {
                        punkte = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int ANZ_TOUREN
            {
                get => anz_touren;
                set
                {
                    if (anz_touren != value)
                    {
                        anz_touren = value;
                        NotifyPropertyChanged();
                    }
                }
            }
            public bool GAME_PAUSED
            {
                get => game_paused;
                set
                {
                    if (game_paused != value)
                    {
                        game_paused = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public int TEMPOMAT
            {
                get => tempomat;
                set
                {
                    if (tempomat != value)
                    {
                        tempomat = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public double AFK_INTER
            {
                get => afk_inter;
                set
                {
                    if (afk_inter != value)
                    {
                        afk_inter = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public string THEME
            {
                get => theme;
                set
                {
                    if (theme != value)
                    {
                        theme = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public bool SPEZIAL_EVENT
            {
                get => spezial_event;
                set
                {
                    if (spezial_event != value)
                    {
                        spezial_event = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public uint TELEMETRY_VERSION
            {
                get => telemetry_version;
                set
                {
                    if (telemetry_version != value)
                    {
                        telemetry_version = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public Uri WAITING_SCREEN_IMAGE
            {
                get => waiting_screen_image;
                set
                {
                    if (waiting_screen_image != value)
                    {
                        waiting_screen_image = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public uint SCSSDK_VERSION
            {
                get => scssdk_version;
                set
                {
                    if (scssdk_version != value)
                    {
                        scssdk_version = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public double MUSIC_VOLUME
            {
                get => music_volume;
                set
                {
                    if (music_volume != value)
                    {
                        music_volume = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public double SOUND_VOLUME_FX
            {
                get => sound_volume_fx;
                set
                {
                    if (sound_volume_fx != value)
                    {
                        sound_volume_fx = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int EARNED_XP
            {
                get => earned_xp;
                set
                {
                    if (earned_xp != value)
                    {
                        earned_xp = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int CPU_TEXT
            {
                get => cpu_text;
                set
                {
                    if (cpu_text != value)
                    {
                        cpu_text = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public string PFAD_ATS_PROFILE
            {
                get => pfad_ats_profile;
                set
                {
                    if (pfad_ats_profile != value)
                    {
                        pfad_ats_profile = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string PFAD_ETS_PROFILE
            {
                get => pfad_ets_profile;
                set
                {
                    if (pfad_ets_profile != value)
                    {
                        pfad_ets_profile = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string LKW_LICENSE_COUNTRY
            {
                get => lkw_license_country;
                set
                {
                    if (lkw_license_country != value)
                    {
                        lkw_license_country = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string LKW_LICENSE_COUNTRY_ID
            {
                get => lkw_license_country_id;
                set
                {
                    if (lkw_license_country_id != value)
                    {
                        lkw_license_country_id = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string TRAILER_LICENSE_COUNTRY_ID
            {
                get => trailer_license_country_id;
                set
                {
                    if (trailer_license_country_id != value)
                    {
                        trailer_license_country_id = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string TRAILER_LICENSE_COUNTRY
            {
                get => trailer_license_country;
                set
                {
                    if (trailer_license_country != value)
                    {
                        trailer_license_country = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string TRAILER_LICENSE_PLATE
            {
                get => trailer_license_plate;
                set
                {
                    if (trailer_license_plate != value)
                    {
                        trailer_license_plate = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public string LKW_LICENSE_PLATE
            {
                get => lkw_license_plate;
                set
                {
                    if (lkw_license_plate != value)
                    {
                        lkw_license_plate = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public string LKW_HERSTELLER_ID
            {
                get => lkw_hersteller_id;
                set
                {
                    if (lkw_hersteller_id != value)
                    {
                        lkw_hersteller_id = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string LADUNG
            {
                get => ladung;
                set
                {
                    if (ladung != value)
                    {
                        ladung = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string FAHRERLISTE_HEADER
            {
                get => fahrerliste_header;
                set
                {
                    if (fahrerliste_header != value)
                    {
                        fahrerliste_header = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string TRAILER_ID
            {
                get => trailer_id;
                set
                {
                    if (trailer_id != value)
                    {
                        trailer_id = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string MB_SERIAL
            {
                get => mb_serial;
                set
                {
                    if (mb_serial != value)
                    {
                        mb_serial = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public string WINDOWS_VERSION
            {
                get => windows_version;
                set
                {
                    if (windows_version != value)
                    {
                        windows_version = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string CPU
            {
                get => cpu;
                set
                {
                    if (cpu != value)
                    {
                        cpu = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string CPU_ID
            {
                get => cpu_id;
                set
                {
                    if (cpu_id != value)
                    {
                        cpu_id = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string ZIELFIRMA_ID
            {
                get => zielfirma_id;
                set
                {
                    if (zielfirma_id != value)
                    {
                        zielfirma_id = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public string ZIELORT
            {
                get => zielort;
                set
                {
                    if (zielort != value)
                    {
                        zielort = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public string STARTFIRMA_ID
            {
                get => startfirma_id;
                set
                {
                    if (startfirma_id != value)
                    {
                        startfirma_id = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string STARTORT
            {
                get => startort;
                set
                {
                    if (startort != value)
                    {
                        startort = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string TRUCK_RPM_TEXT
            {
                get => truck_rpm_text;
                set
                {
                    if (truck_rpm_text != value)
                    {
                        truck_rpm_text = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int RPM
            {
                get => rpm;
                set
                {
                    if (rpm != value)
                    {
                        rpm = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int RPM_MAX
            {
                get => rpm_max;
                set
                {
                    if (rpm_max != value)
                    {
                        rpm_max = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string SITE_FONT_COLOR
            {
                get => site_font_color;
                set
                {
                    if (site_font_color != value)
                    {
                        site_font_color = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string STARTFIRMA
            {
                get => startfirma;
                set
                {
                    if (startfirma != value)
                    {
                        startfirma = value;
                        NotifyPropertyChanged();
                    }
                }
            }
            public string ZIELFIRMA
            {
                get => zielfirma;
                set
                {
                    if (zielfirma != value)
                    {
                        zielfirma = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string REAL_ECO_MODUS
            {
                get => real_eco_modus;
                set
                {
                    if (real_eco_modus != value)
                    {
                        real_eco_modus = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public bool SPEED_LIMIT_IMAGE
            {
                get => speed_limit_image;
                set
                {
                    if (speed_limit_image != value)
                    {
                        speed_limit_image = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int SPEED_LIMIT
            {
                get => speed_limit;
                set
                {
                    if (speed_limit != value)
                    {
                        speed_limit = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public int KURZE_IDNUMMER
            {
                get => kurze_idnummer;
                set
                {
                    if (kurze_idnummer != value)
                    {
                        kurze_idnummer = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public double POS_Z
            {
                get => pos_z;
                set
                {
                    if (pos_z != value)
                    {
                        pos_z = value;
                        NotifyPropertyChanged();
                    }
                }
            }



            public double POS_Y
            {
                get => pos_y;
                set
                {
                    if (pos_y != value)
                    {
                        pos_y = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public double POS_X
            {
                get => pos_x;
                set
                {
                    if (pos_x != value)
                    {
                        pos_x = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public double HEADING
            {
                get => heading;
                set
                {
                    if (heading != value)
                    {
                        heading = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int MAX_SPEED_HEUTE
            {
                get => max_speed_heute;
                set
                {
                    if (max_speed_heute != value)
                    {
                        max_speed_heute = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public string EURO_DOLLAR
            {
                get => euro_dollar;
                set
                {
                    if (euro_dollar != value)
                    {
                        euro_dollar = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public float KM_PREIS
            {
                get => km_preis;
                set
                {
                    if (km_preis != value)
                    {
                        km_preis = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int ZU_SCHNELL_SEKUNDEN
            {
                get => zu_schnell_sekunden;
                set
                {
                    if (zu_schnell_sekunden != value)
                    {
                        zu_schnell_sekunden = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public bool AIR_PRESSURE
            {
                get => air_pressure;
                set
                {
                    if (air_pressure != value)
                    {
                        air_pressure = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public bool OIL_PRESSURE
            {
                get => oil_pressure;
                set
                {
                    if (oil_pressure != value)
                    {
                        oil_pressure = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public bool HANDBREMSE
            {
                get => handbremse;
                set
                {
                    if (handbremse != value)
                    {
                        handbremse = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public bool JOB_FINISHED
            {
                get => jobfinished;
                set
                {
                    if (jobfinished != value)
                    {
                        jobfinished = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string FUEL_GERADE_TANKANZEIGE
            {
                get => fuel_gerade_tankanzeige;
                set
                {
                    if (fuel_gerade_tankanzeige != value)
                    {
                        fuel_gerade_tankanzeige = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public int FUEL_GERADE_INT
            {
                get => fuel_gerade_int;
                set
                {
                    if (fuel_gerade_int != value)
                    {
                        fuel_gerade_int = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public double ZU_SCHNELL
            {
                get => Zu_schnell;
                set
                {
                    if (Zu_schnell != value)
                    {
                        Zu_schnell = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public double BLINKER_LINKS
            {
                get => blinker_links;
                set
                {
                    if (blinker_links != value)
                    {
                        blinker_links = value;
                        NotifyPropertyChanged();
                    }
                }
            }
            public double BLINKER_RECHTS
            {
                get => blinker_rechts;
                set
                {
                    if (blinker_rechts != value)
                    {
                        blinker_rechts = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public TimeSpan REST_ZEIT
            {
                get => rest_zeit;
                set
                {
                    if (rest_zeit != value)
                    {
                        rest_zeit = value;
                        NotifyPropertyChanged();
                    }
                }
            }
            public string WARNLICHT
            {
                get => warnlicht;
                set
                {
                    if (warnlicht != value)
                    {
                        warnlicht = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public string AUX_LICHT_AN
            {
                get => aux_licht_an;
                set
                {
                    if (aux_licht_an != value)
                    {
                        aux_licht_an = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string LICHT_AN_2
            {
                get => licht_an_2;
                set
                {
                    if (licht_an_2 != value)
                    {
                        licht_an_2 = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string LICHT_AN_3
            {
                get => licht_an_3;
                set
                {
                    if (licht_an_3 != value)
                    {
                        licht_an_3 = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public bool TRAILER_ANGEHANGEN
            {
                get => trailer_angehangen;
                set
                {
                    if (trailer_angehangen != value)
                    {
                        trailer_angehangen = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public string LICHT_AN
            {
                get => licht_an;
                set
                {
                    if (licht_an != value)
                    {
                        licht_an = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string ELEKTRIK_AN
            {
                get => elektrik_an;
                set
                {
                    if (elektrik_an != value)
                    {
                        elektrik_an = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string STARTOPTIONEN
            {
                get => startoptionen;
                set
                {
                    if (startoptionen != value)
                    {
                        startoptionen = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string USERNAME
            {
                get => username;
                set
                {
                    if (username != value)
                    {
                        username = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string PASS
            {
                get => pass;
                set
                {
                    if (pass != value)
                    {
                        pass = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int RANG
            {
                get => rang;
                set
                {
                    if (rang != value)
                    {
                        rang = value;
                        NotifyPropertyChanged();
                    }
                }
            }



            public string TANK_LEER
            {
                get => tank_leer;
                set
                {
                    if (tank_leer != value)
                    {
                        tank_leer = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string ANTI_AFK_TEXT_1
            {
                get => anti_afk_text_1;
                set
                {
                    if (anti_afk_text_1 != value)
                    {
                        anti_afk_text_1 = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string ANTI_AFK_TEXT_2
            {
                get => anti_afk_text_2;
                set
                {
                    if (anti_afk_text_2 != value)
                    {
                        anti_afk_text_2 = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string ANTI_AFK_TEXT_3
            {
                get => anti_afk_text_3;
                set
                {
                    if (anti_afk_text_3 != value)
                    {
                        anti_afk_text_3 = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string USER_SPEDITION
            {
                get => user_spedition;
                set
                {
                    if (user_spedition != value)
                    {
                        user_spedition = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string LITER_GALLONEN
            {
                get => liter_gallonen;
                set
                {
                    if (liter_gallonen != value)
                    {
                        liter_gallonen = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public string FUEL_GERADE_TEXT
            {
                get => fuel_gerade_Text;
                set
                {
                    if (fuel_gerade_Text != value)
                    {
                        fuel_gerade_Text = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string RESTSTRECKE_MIT_FUEL_TEXT
            {
                get => reststrecke_mit_fuel_text;
                set
                {
                    if (reststrecke_mit_fuel_text != value)
                    {
                        reststrecke_mit_fuel_text = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int RESTSTRECKE_MIT_FUEL
            {
                get => reststrecke_mit_fuel;
                set
                {
                    if (reststrecke_mit_fuel != value)
                    {
                        reststrecke_mit_fuel = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public string TO_LB
            {
                get => to_lb;
                set
                {
                    if (to_lb != value)
                    {
                        to_lb = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string KM_MEILEN_2
            {
                get => km_meilen_2;
                set
                {
                    if (km_meilen_2 != value)
                    {
                        km_meilen_2 = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string KM_MEILEN
            {
                get => km_meilen;
                set
                {
                    if (km_meilen != value)
                    {
                        km_meilen = value;
                        NotifyPropertyChanged();
                    }
                }
            }



            public int MAX_SPEED_TOUR
            {
                get => max_speed_tour;
                set
                {
                    if (max_speed_tour != value)
                    {
                        max_speed_tour = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public int TRANSPORT_GEBÜHR_ZUG
            {
                get => transport_gebühr_zug;
                set
                {
                    if (transport_gebühr_zug != value)
                    {
                        transport_gebühr_zug = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int TRANSPORT_GEBÜHR_FÄHRE
            {
                get => transport_gebühr_fähre;
                set
                {
                    if (transport_gebühr_fähre != value)
                    {
                        transport_gebühr_fähre = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string TRANSPORT_STARTORT_FÄHRE
            {
                get => transport_startort_fähre;
                set
                {
                    if (transport_startort_fähre != value)
                    {
                        transport_startort_fähre = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string TRANSPORT_ZIELORT_FÄHRE
            {
                get => transport_zielort_fähre;
                set
                {
                    if (transport_zielort_fähre != value)
                    {
                        transport_zielort_fähre = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string TRANSPORT_STARTORT_ZUG
            {
                get => transport_startort_zug;
                set
                {
                    if (transport_startort_zug != value)
                    {
                        transport_startort_zug = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string TRANSPORT_ZIELORT_ZUG
            {
                get => transport_zielort_zug;
                set
                {
                    if (transport_zielort_zug != value)
                    {
                        transport_zielort_zug = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public float LITER_GETANKT
            {
                get => liter_getankt;
                set
                {
                    if (liter_getankt != value)
                    {
                        liter_getankt = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int STRAFE_JOB_CANCEL
            {
                get => strafe_job_cancel;
                set
                {
                    if (strafe_job_cancel != value)
                    {
                        strafe_job_cancel = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string STRAFE_VERGEHEN
            {
                get => strafe_vergehen;
                set
                {
                    if (strafe_vergehen != value)
                    {
                        strafe_vergehen = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public int STRAFE_GEBÜHR
            {
                get => strafe_gebühr;
                set
                {
                    if (strafe_gebühr != value)
                    {
                        strafe_gebühr = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public int MAUTSTATION_GEBÜHR
            {
                get => mautstation_gebühr;
                set
                {
                    if (mautstation_gebühr != value)
                    {
                        mautstation_gebühr = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public float KILOMETERSTAND
            {
                get => kilometerstand;
                set
                {
                    if (kilometerstand != value)
                    {
                        kilometerstand = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public float LKWSCHADEN_DASHBOARD
            {
                get => lkwschaden_dashboard;
                set
                {
                    if (lkwschaden_dashboard != value)
                    {
                        lkwschaden_dashboard = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public float FRACHTSCHADEN_DASHBOARD
            {
                get => frachtschaden_dashboard;
                set
                {
                    if (frachtschaden_dashboard != value)
                    {
                        frachtschaden_dashboard = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public float FRACHTSCHADEN
            {
                get => frachtschaden;
                set
                {
                    if (frachtschaden != value)
                    {
                        frachtschaden = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int JOB_GEF_STRECKE
            {
                get => job_gef_strecke;
                set
                {
                    if (job_gef_strecke != value)
                    {
                        job_gef_strecke = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public double TANKVOLUMEN
            {
                get => tankvolumen;
                set
                {
                    if (tankvolumen != value)
                    {
                        tankvolumen = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public double FUEL_GERADE
            {
                get => fuel_gerade;
                set
                {
                    if (fuel_gerade != value)
                    {
                        fuel_gerade = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public bool TRAILER_ATTACHED
            {
                get => trailer_attached;
                set
                {
                    if (trailer_attached != value)
                    {
                        trailer_attached = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string INFOTEXT_1
            {
                get => infotext_1;
                set
                {
                    if (infotext_1 != value)
                    {
                        infotext_1 = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string INFOTEXT_2
            {
                get => infotext_2;
                set
                {
                    if (infotext_2 != value)
                    {
                        infotext_2 = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string INFOTEXT_3
            {
                get => infotext_3;
                set
                {
                    if (infotext_3 != value)
                    {
                        infotext_3 = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public bool WERBEANZEIGE
            {
                get => werbeanzeige;
                set
                {
                    if (werbeanzeige != value)
                    {
                        werbeanzeige = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public int SPEED
            {
                get => speed;
                set
                {
                    if (speed != value)
                    {
                        speed = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string PFAD_TMP
            {
                get => pfad_tmp;
                set
                {
                    if (pfad_tmp != value)
                    {
                        pfad_tmp = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string PFAD_ATS
            {
                get => pfad_ats;
                set
                {
                    if (pfad_ats != value)
                    {
                        pfad_ats = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string PFAD_ETS
            {
                get => pfad_ets;
                set
                {
                    if (pfad_ets != value)
                    {
                        pfad_ets = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public bool JOB_INAKTIV
            {
                get => job_aktiv;
                set
                {
                    if (job_aktiv != value)
                    {
                        job_aktiv = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public bool JOB_AKTIV
            {
                get => job_aktiv;
                set
                {
                    if (job_aktiv != value)
                    {
                        job_aktiv = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public bool CARGO_LOADED
            {
                get => cargo_loaded;
                set
                {
                    if (cargo_loaded != value)
                    {
                        cargo_loaded = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public ulong EINKOMMEN
            {
                get => einkommen;
                set
                {
                    if (einkommen != value)
                    {
                        einkommen = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public float GEWICHT
            {
                get => gewicht;
                set
                {
                    if (gewicht != value)
                    {
                        gewicht = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string SPIEL
            {
                get => spiel;
                set
                {
                    if (spiel != value)
                    {
                        spiel = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string FRACHTMARKT
            {
                get => frachtmarkt;
                set
                {
                    if (frachtmarkt != value)
                    {
                        frachtmarkt = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public bool GAME_AKTIV
            {
                get => game_aktiv;
                set
                {
                    if (game_aktiv != value)
                    {
                        game_aktiv = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string TOUR_ID
            {
                get => tour_id;
                set
                {
                    if (tour_id != value)
                    {
                        tour_id = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public uint REST_KM
            {
                get => rest_km;
                set
                {
                    if (rest_km != value)
                    {
                        rest_km = value;
                        NotifyPropertyChanged();
                    }
                }
            }



            public float REST_KM_FLOAT
            {
                get => rest_km_float;
                set
                {
                    if (rest_km_float != value)
                    {
                        rest_km_float = value;
                        NotifyPropertyChanged();
                    }
                }
            }


            public uint STRECKE
            {
                get => strecke;
                set
                {
                    if (strecke != value)
                    {
                        strecke = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string ZIELORT_ID
            {
                get => zielort_id;
                set
                {
                    if (zielort_id != value)
                    {
                        zielort_id = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            public string STARTORT_ID
            {
                get => startort_id;
                set
                {
                    if (startort_id != value)
                    {
                        startort_id = value;
                        NotifyPropertyChanged();
                    }
                }

            }

            public string LADUNG_ID
            {
                get => ladung_id;
                set
                {
                    if (ladung_id != value)
                    {
                        ladung_id = value;
                        NotifyPropertyChanged();
                    }
                }

            }

            public string CLIENT_VERSION
            {
                get => client_version;
                set
                {
                    if (client_version != value)
                    {
                        client_version = value;
                        NotifyPropertyChanged();
                    }
                }

            }

            public string LKW_HERSTELLER
            {
                get => lkw_hersteller;
                set
                {
                    if (lkw_hersteller != value)
                    {
                        lkw_hersteller = value;
                        NotifyPropertyChanged();
                    }
                }

            }

            public string LKW_MODELL
            {
                get => lkw_modell;
                set
                {
                    if (lkw_modell != value)
                    {
                        lkw_modell = value;
                        NotifyPropertyChanged();
                    }
                }

            }

            public string LKW_MODELL_ID
            {
                get => lkw_modell_id;
                set
                {
                    if (lkw_modell_id != value)
                    {
                        lkw_modell_id = value;
                        NotifyPropertyChanged();
                    }
                }

            }


            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            private void SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
            {
                if (!Equals(field, newValue))
                {
                    field = newValue;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
            }
        
    }
}
