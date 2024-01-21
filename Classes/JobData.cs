using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrucksLOG.Classes
{
    public class JobData
    {
        public string SPEZIAL_EVENT { get; set; }
        public string INTERNE_ID { get; set; }
        public int ID_KURZ { get; set; }
        public string ID_LANG { get; set; }
        public uint GESAMT_STRECKE { get; set; }
        public uint GESAMT_STRECKE_INT { get; set; }
        public uint REST_STRECKE { get; set; }
        public float REST_STRECKE_FLOAT { get; set; }
        public string CLIENT_KEY { get; set; }
        public string CLIENT_KEY2 { get; set; }
        public string CLIENT_VERSION { get; set; }
        public string STARTORT { get; set; }
        public string STARTORT_ID { get; set; }
        public string STARTFIRMA { get; set; }
        public string STARTFIRMA_ID { get; set; }
        public string ZIELORT { get; set; }
        public string ZIELORT_ID { get; set; }
        public string ZIELFIRMA { get; set; }
        public string ZIELFIRMA_ID { get; set; }
        public string LADUNG { get; set; }
        public string LADUNG_ID { get; set; }
        public string TRAILER_ID { get; set; }
        public float GEWICHT { get; set; }
        public string FRACHTMARKT { get; set; }
        public string LKW_HERSTELLER { get; set; }
        public string LKW_HERSTELLER_ID { get; set; }
        public string LKW_MODELL { get; set; }
        public string LKW_MODELL_ID { get; set; }
        public string SPIEL { get; set; }
        public ulong EINKOMMEN { get; set; }
        public int EINKOMMEN_INT { get; set; }
        public double TANKVOLUMEN { get; set; }
        public double FUEL_GERADE { get; set; }
        public string LKW_LICENSE_PLATE { get; set; }
        public string LKW_LICENSE_COUNTRY { get; set; }
        public string LKW_LICENSE_COUNTRY_ID { get; set; }
        public bool TRAILER_ATTACH { get; set; }
        public string TRAILER_LICENSE_PLATE { get; set; }
        public string TRAILER_LICENSE_COUNTRY { get; set; }
        public string TRAILER_LICENSE_COUNTRY_ID { get; set; }
        public float ODO_START { get; set; }
        public float ODO_ENDE { get; set; }
        public uint JOB_GEF_STRECKE { get; set; }
        public int JOB_GEF_STRECKE_FLOAT { get; set; }
        public float FRACHTSCHADEN { get; set; }
        public int CANCEL_STRAFEE { get; set; }
        public bool AUTOPARKING { get; set; }
        public bool AUTOLOADING { get; set; }
        public int EARNED_XP { get; set; }
        public double ABG_POSX { get; set; }
        public double ABG_POSY { get; set; }
        public double ABG_POSZ { get; set; }
        public uint DELIVERYTIME { get; set; }
        public int ZU_SCHNELL_SEK { get; set; }
        public int MAX_SPEED { get; set; }
        public int VS_TOUR { get; set; }
        public string FAHRER_FIRMENNAME { get; set; }
        public float FRACHTSCHADEN_DASHBOARD { get; set; }
        public long EINNAHMEN { get; set; }
        public string WEBHOOK_SENDEN { get; set; }
        public string TOUR_STATUS { get; set; }
        public float KILOMETERPREIS { get; set; }
        public int CHEAT_ENGINE { get; set; }
        public int VIRTUAL_SPEDITEUR { get; set; }
        public int TRAILER_CHANGER { get; set; }
        public int FERRY_COST { get; set; }

    }
}
