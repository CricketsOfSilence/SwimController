using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObsOverlay.model
{
    public class FilePathConfiguration
    {
        private const string CONFIG_DIR = "SwimController";
        private const string CONFIG_FILE = "config.ini";
        public string EventName { get; set; }

        public Dictionary<Lane, string> Lanes { get;  set; }
        
        public string Records { get; set; }
        public string HeatNumber { get; set; }

        public FilePathConfiguration()
        {
            Lanes = new Dictionary<Lane, string>();
        }

        public string GetLane(Lane lane)
        {
            if (Lanes.ContainsKey(lane))
            {
                return Lanes[lane];
            }
            else
            {
                return "";
            }
        }

        public void PutLane(Lane lane, string value)
        {
            if (Lanes == null)
            {
                Lanes = new Dictionary<Lane, string>();
            }            

            Lanes[lane] = value;
        }

        public static FilePathConfiguration LoadFromConfig()
        {
            var filePathConfig = new FilePathConfiguration();
            try
            {
                string appDataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), CONFIG_DIR, CONFIG_FILE);
                if (File.Exists(appDataFolderPath))
                {
                    foreach (String line in File.ReadLines(appDataFolderPath))
                    {
                        string[] split = line.Split('=');
                        if (split.Length > 1)
                        {
                            switch (split[0])
                            {
                                case "eventName":
                                    filePathConfig.EventName = split[1];
                                    break;
                                case "record":
                                    filePathConfig.Records = split[1];
                                    break;
                                case "heat":
                                    filePathConfig.HeatNumber = split[1];
                                    break;
                                case "lane1":
                                    filePathConfig.PutLane(Lane.Lane1, split[1]);
                                    break;
                                case "lane2":
                                    filePathConfig.PutLane(Lane.Lane2, split[1]);
                                    break;
                                case "lane3":
                                    filePathConfig.PutLane(Lane.Lane3, split[1]);
                                    break;
                                case "lane4":
                                    filePathConfig.PutLane(Lane.Lane4, split[1]);
                                    break;
                                case "lane5":
                                    filePathConfig.PutLane(Lane.Lane5, split[1]);
                                    break;
                                case "lane6":
                                    filePathConfig.PutLane(Lane.Lane6, split[1]);
                                    break;
                            }
                        }
                    }
                }
            } catch (Exception e)
            {

            }
            return filePathConfig;
        }

        public void WriteConfigFile()
        {
            try
            {                
                StringBuilder builder = new StringBuilder();

                if (!String.IsNullOrEmpty(EventName))
                    builder.AppendFormat("eventName={0}\n", EventName);

                if (!String.IsNullOrEmpty(Records))
                    builder.AppendFormat("record={0}\n", Records);

                if (!String.IsNullOrEmpty(HeatNumber))
                    builder.AppendFormat("heat={0}\n", HeatNumber);

                if (!String.IsNullOrEmpty(Lanes[Lane.Lane1]))
                    builder.AppendFormat("lane1={0}\n", Lanes[Lane.Lane1]);

                if (!String.IsNullOrEmpty(Lanes[Lane.Lane2]))
                    builder.AppendFormat("lane2={0}\n", Lanes[Lane.Lane2]);

                if (!String.IsNullOrEmpty(Lanes[Lane.Lane3]))
                    builder.AppendFormat("lane3={0}\n", Lanes[Lane.Lane3]);

                if (!String.IsNullOrEmpty(Lanes[Lane.Lane4]))
                    builder.AppendFormat("lane4={0}\n", Lanes[Lane.Lane4]);

                if (!String.IsNullOrEmpty(Lanes[Lane.Lane5]))
                    builder.AppendFormat("lane5={0}\n", Lanes[Lane.Lane5]);

                if (!String.IsNullOrEmpty(Lanes[Lane.Lane6]))
                    builder.AppendFormat("lane6={0}\n", Lanes[Lane.Lane6]);

                string appDataFolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), CONFIG_DIR);

                if (!Directory.Exists(appDataFolderPath))
                {
                    Directory.CreateDirectory(appDataFolderPath);
                }                

                File.WriteAllText(Path.Combine(appDataFolderPath, CONFIG_FILE), builder.ToString());
            }
            catch (Exception exception)
            {
                System.Console.WriteLine("Exception: " + exception.Message);
                // ignore
            }
        }
    }
}
