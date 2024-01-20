using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace TrucksLOG.View
{
    /// <summary>
    /// Interaktionslogik für LogFile.xaml
    /// </summary>
    public partial class LogFile : UserControl
    {
        public LogFile()
        {
            InitializeComponent();
            var d = DateTime.Now;

            string FileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\TrucksLOG\Log_" + d.Day.ToString("00") + "_" + d.Month.ToString("00") + "_" + d.Year.ToString() + ".txt";
            string FileName_Copy = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\TrucksLOG\Log_" + d.Day.ToString("00") + "_" + d.Month.ToString("00") + "_" + d.Year.ToString() + "_COPY.txt";

            File.Copy(FileName, FileName_Copy, true);
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Action)(() =>
            {

                var contents = File.ReadAllLines(FileName_Copy);
                string Content = "";

                foreach (var line in contents)
                {
                    Content += line;
                    Content += Environment.NewLine;
                }
                Content_Viewer.Text = Content;
                File.Delete(FileName_Copy);
            }));

           
        }

        public static void CLEAR_LOG()
        {
            var d = DateTime.Now;
            string FileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\TrucksLOG\Log_" + d.Day.ToString("00") + "_" + d.Month.ToString("00") + "_" + d.Year.ToString() + ".txt";
            File.WriteAllText(FileName, string.Empty);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var d = DateTime.Now;
            string FileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\TrucksLOG\Log_" + d.Day.ToString("00") + "_" + d.Month.ToString("00") + "_" + d.Year.ToString() + ".txt";
     
         
            string p = FileName;
            string args = string.Format("/e, /select, \"{0}\"", p);

            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = "explorer";
            info.Arguments = args;
            Process process = Process.Start(info);
        }
    }
}
