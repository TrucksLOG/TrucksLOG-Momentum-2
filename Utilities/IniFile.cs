﻿using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace TrucksLOG.Utilities
{
    public class IniFile
    {
        readonly string Path;
        readonly string EXE = Assembly.GetExecutingAssembly().GetName().Name;

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        private static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode)]
        static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

        public IniFile(string IniPath = null)
        {
            Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
        }

        public string Read(string Key, string Section = null)
        {
            var RetVal = new StringBuilder(255);
            _ = GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
            return RetVal.ToString();
        }

        public void Write(string Key, string Value, string Section = null)
        {
            WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
        }

        public void DeleteKey(string Key, string Section = null)
        {
            Write(Key, null, Section ?? EXE);
        }

        public void DeleteSection(string Section = null)
        {
            Write(null, null, Section ?? EXE);
        }

        public bool KeyExists(string Key, string Section = null)
        {
            return Read(Key, Section).Length > 0;
        }

        internal bool CHECK_TOUR(string zIELORT_ID, string zIELFIRMA_ID, string sTARTORT_ID, string sTARTFIRMA_ID, string lADUNG_ID)
        {
            return Read("STARTORT", "AKTUELLE_TOUR") == sTARTORT_ID && Read("STARTFIRMA", "AKTUELLE_TOUR") == sTARTFIRMA_ID && Read("ZIELORT", "AKTUELLE_TOUR") == zIELORT_ID && Read("ZIELFIRMA", "AKTUELLE_TOUR") == zIELFIRMA_ID && Read("LADUNG", "AKTUELLE_TOUR") == lADUNG_ID;
        }
    }
}
