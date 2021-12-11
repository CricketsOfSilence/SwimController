using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwimController
{
    public partial class HeatController : Form
    {
        private const string CONFIG_DIR = "SwimController";
        private const string CONFIG_FILE = "config.ini";
        private const string NO_HEAT = "N/A";

        private string heatSheetFilePath = "";
        private string eventNamePath = "";
        private string lane1Path = "";
        private string lane2Path = "";
        private string lane3Path = "";
        private string lane4Path = "";
        private string lane5Path = "";
        private string lane6Path = "";
        private string recordFilePath = "";
        private string heatNumberFilePath = "";

        ConfigurationModel config;

        private int spacesToAppend = 4;
        
        private int eventIndex;
        private readonly List<Heat> heats;
        public HeatController()
        {
            InitializeComponent();
            heats = new List<Heat>();
            eventIndex = 0;
            config = new ConfigurationModel();
        }

        public HeatController(String basePath, List<Heat> heats) : this()
        {
            config = new ConfigurationModel()
            {
                EventNameFilePath = Path.Combine(basePath, eventNamePath),
                LaneFilePaths = new string[] {
                    Path.Combine(basePath, lane1Path),
                    Path.Combine(basePath, lane2Path),
                    Path.Combine(basePath, lane3Path),
                    Path.Combine(basePath, lane4Path),
                    Path.Combine(basePath, lane5Path),
                    Path.Combine(basePath, lane6Path)
                }
            };

            this.eventNamePath = Path.Combine(basePath, eventNamePath);
            this.lane1Path = Path.Combine(basePath, lane1Path);
            this.lane2Path = Path.Combine(basePath, lane2Path);
            this.lane3Path = Path.Combine(basePath, lane3Path);
            this.lane4Path = Path.Combine(basePath, lane4Path);
            this.lane5Path = Path.Combine(basePath, lane5Path);
            this.lane6Path = Path.Combine(basePath, lane6Path);

            this.heats = heats;
            updateHeats();
            updateButtonStatus();         
        }

        //public void updatedEventName_Click(object sender, EventArgs e)
        //{
        //    heats[heatNumber].EventName = updateEventNameTextBox.Text;
        //    write(EVENT_NAME, updateEventNameTextBox.Text);
        //}     

        private void updateHeats()
        {
            if (eventIndex >= 0 && eventIndex < heats.Count)
            {
                Heat currentHeat = heats[eventIndex];
                
                if (eventIndex - 1 >= 0)
                {
                    previousHeatName.Text = heats[eventIndex - 1].EventName;
                }
                else
                {
                    previousHeatName.Text = NO_HEAT;
                }

                if (eventIndex + 1 < heats.Count)
                {
                    nextHeatName.Text = heats[eventIndex + 1].EventName;
                }
                else
                {
                    nextHeatName.Text = NO_HEAT;
                }
              
                loadHeat(currentHeat);
            }
        }

        public void previousHeatButton_Click(object sender, EventArgs e)
        {
            if (eventIndex >= 0) {
                eventIndex--;
                updateHeats();
                updateButtonStatus();
            }           
        }

        public void nextHeatButton_Click(object sender, EventArgs e)
        {
            if (eventIndex < heats.Count)
            {
                eventIndex++;
                updateHeats();
                updateButtonStatus();
            }                     
        }

        private void updateButtonStatus()
        {
            if (eventIndex <= 0)
            {
                previousHeatButton.Enabled = false;
                previousHeatName.Text = NO_HEAT;
            } 
            else
            {
                previousHeatButton.Enabled = true;
                previousHeatName.Text = heats[eventIndex - 1].EventName;
            }

            if (eventIndex >= heats.Count - 1)
            {
                nextHeatButton.Enabled = false;
                nextHeatName.Text = NO_HEAT;
            }
            else
            {
                nextHeatButton.Enabled = true;
                nextHeatName.Text = heats[eventIndex + 1].EventName;
            }
        }

        private void loadHeat(Heat heat)
        {           
            write(eventNamePath, heat.EventName);
            updateEventNameTextBox.Text = heat.EventName;
            currentEventLabel.Text = heat.EventName;
            write(lane1Path, heat.Lane1Name);
            lane1TextBox.Text = heat.Lane1Name;
            write(lane2Path, heat.Lane2Name);
            lane2TextBox.Text = heat.Lane2Name;
            write(lane3Path, heat.Lane3Name);
            lane3TextBox.Text = heat.Lane3Name;
            write(lane4Path, heat.Lane4Name);
            lane4TextBox.Text = heat.Lane4Name;
            write(lane5Path, heat.Lane5Name);
            lane5TextBox.Text = heat.Lane5Name;
            write(lane6Path, heat.Lane6Name);
            lane6TextBox.Text = heat.Lane6Name;
        }

        private void write(string fileName, string text)
        {
            if (File.Exists(fileName))
            {
                System.IO.File.WriteAllText(fileName, text.PadRight(spacesToAppend));
            }
        }

        private void HeatController_Load(object sender, EventArgs e)
        {
            try
            {
                string appDataFolderPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                if (File.Exists(Path.Combine(appDataFolderPath, CONFIG_DIR, CONFIG_FILE)))
                {
                    string[] rows = File.ReadAllLines(Path.Combine(appDataFolderPath, CONFIG_DIR, CONFIG_FILE));
                    foreach (String s in rows)
                    {
                        string[] split = s.Split('=');
                        if (split.Length > 1)
                        {
                            switch (split[0])
                            {
                                case "heatSheet":
                                    heatSheetFilePath = split[1];
                                    break;
                                case "eventName":
                                    eventNamePath = split[1];
                                    break;
                                case "record":
                                    recordFilePath = split[1];
                                    break;
                                case "heat":
                                    heatNumberFilePath = split[1];
                                    break;
                                case "lane1":
                                    lane1Path = split[1];
                                    break;
                                case "lane2":
                                    lane2Path = split[1];
                                    break;
                                case "lane3":
                                    lane3Path = split[1];
                                    break;
                                case "lane4":
                                    lane4Path = split[1];
                                    break;
                                case "lane5":
                                    lane5Path = split[1];
                                    break;
                                case "lane6":
                                    lane6Path = split[1];
                                    break;
                            }
                        }
                    }
                    config = new ConfigurationModel()
                    {
                        HeatSheetFilePath = heatSheetFilePath,
                        EventNameFilePath = eventNamePath,
                        RecordFilePath = recordFilePath,
                        HeatNumberFilePath = heatNumberFilePath,
                        LaneFilePaths = new string[]
                        {
                            lane1Path,
                            lane2Path,
                            lane3Path,
                            lane4Path,
                            lane5Path,
                            lane6Path
                        }
                    };
                    LoadData();
                }
            } catch (Exception exception) { 
                // ignore and log error
            }
        }
        private void updateEventNameButton_Click(object sender, EventArgs e)
        {
            heats[eventIndex].EventName = updateEventNameTextBox.Text;
            write(eventNamePath, heats[eventIndex].EventName);
        }

        private void lane1UpdateButton_Click(object sender, EventArgs e)
        {
            heats[eventIndex].Lane1Name = lane1TextBox.Text;
            write(lane1Path, heats[eventIndex].Lane1Name);
        }
        private void lane2UpdateButton_Click(object sender, EventArgs e)
        {
            heats[eventIndex].Lane2Name = lane2TextBox.Text;
            write(lane2Path, heats[eventIndex].Lane2Name);
        }

        private void lane3UpdateButton_Click(object sender, EventArgs e)
        {
            heats[eventIndex].Lane3Name = lane3TextBox.Text;
            write(lane3Path, heats[eventIndex].Lane3Name);
        }                

        private void lane4UpdateButton_Click(object sender, EventArgs e)
        {
            heats[eventIndex].Lane4Name = lane4TextBox.Text;
            write(lane4Path, heats[eventIndex].Lane4Name);
        }

        private void lane5UpdateButton_Click(object sender, EventArgs e)
        {
            heats[eventIndex].Lane5Name = lane5TextBox.Text;
            write(lane5Path, heats[eventIndex].Lane5Name);
        }

        private void lane6UpdateButton_Click(object sender, EventArgs e)
        {
            heats[eventIndex].Lane6Name = lane6TextBox.Text;
            write(lane6Path, heats[eventIndex].Lane6Name);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void configureButton_Click(object sender, EventArgs e)
        {
            using (ConfigureController configureController = new ConfigureController(config))
            {
                configureController.Show();
                configureController.Visible = false;
    
                if (configureController.ShowDialog() == DialogResult.OK)
                {

                    ConfigurationModel config = configureController.GetConfiguration();

                    if (!String.IsNullOrEmpty(config.HeatSheetFilePath))
                    {
                        heatSheetFilePath = config.HeatSheetFilePath;
                    }

                    if (!String.IsNullOrEmpty(config.EventNameFilePath))
                    {
                        eventNamePath = config.EventNameFilePath;
                    }

                    if (!String.IsNullOrEmpty(config.HeatNumberFilePath))
                    {
                        heatNumberFilePath = config.HeatNumberFilePath;
                    }

                    if (!String.IsNullOrEmpty(config.RecordFilePath)) {
                        recordFilePath = config.RecordFilePath;
                    }

                    if (!String.IsNullOrEmpty(config.LaneFilePaths[0]))
                    {
                        lane1Path = config.LaneFilePaths[0];
                    }

                    if (!String.IsNullOrEmpty(config.LaneFilePaths[1]))
                    {
                        lane2Path = config.LaneFilePaths[1];
                    }

                    if (!String.IsNullOrEmpty(config.LaneFilePaths[2]))
                    {
                        lane3Path = config.LaneFilePaths[2];
                    }

                    if (!String.IsNullOrEmpty(config.LaneFilePaths[3]))
                    {
                        lane4Path = config.LaneFilePaths[3];
                    }

                    if (!String.IsNullOrEmpty(config.LaneFilePaths[4]))
                    {
                        lane5Path = config.LaneFilePaths[4];
                    }

                    if (!String.IsNullOrEmpty(config.LaneFilePaths[5]))
                    {
                        lane6Path = config.LaneFilePaths[5];
                    }
                    writeToConfigFile();
                    LoadData();
                    configureController.Close();
                }            
            }                      
        }
        private void writeToConfigFile()
        {
            try
            {
                string appDataFolderPath = System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                StringBuilder builder = new StringBuilder();

                if (!String.IsNullOrEmpty(heatSheetFilePath))
                    builder.AppendFormat("heatSheet={0}\n", heatSheetFilePath);
                
                if (!String.IsNullOrEmpty(eventNamePath))
                    builder.AppendFormat("eventName={0}\n", eventNamePath);
                
                if (!String.IsNullOrEmpty(recordFilePath))
                    builder.AppendFormat("record={0}\n", recordFilePath);
                
                if (!String.IsNullOrEmpty(heatNumberFilePath))
                    builder.AppendFormat("heat={0}\n", heatNumberFilePath);
                
                if (!String.IsNullOrEmpty(lane1Path))
                    builder.AppendFormat("lane1={0}\n", lane1Path);
                
                if (!String.IsNullOrEmpty(lane2Path))
                    builder.AppendFormat("lane2={0}\n", lane2Path);
                
                if (!String.IsNullOrEmpty(lane3Path))
                    builder.AppendFormat("lane3={0}\n", lane3Path);
                
                if (!String.IsNullOrEmpty(lane4Path))
                    builder.AppendFormat("lane4={0}\n", lane4Path);
                
                if (!String.IsNullOrEmpty(lane5Path))
                    builder.AppendFormat("lane5={0}\n", lane5Path);
                
                if (!String.IsNullOrEmpty(lane6Path))
                    builder.AppendFormat("lane6={0}\n", lane6Path);

                if (!Directory.Exists(Path.Combine(appDataFolderPath, CONFIG_DIR))) {
                    Directory.CreateDirectory(Path.Combine(appDataFolderPath, CONFIG_DIR));
                }

                File.WriteAllText(Path.Combine(appDataFolderPath, CONFIG_DIR, CONFIG_FILE),
                    builder.ToString());
            } 
            catch (Exception exception)
            {
                System.Console.WriteLine("Exception: " + exception.Message);
                // ignore
            }
        }

        private void LoadData()
        {
            heats.Clear();
            string[] lines = File.ReadAllLines(heatSheetFilePath);

            foreach (string line in lines)
            {
                string[] split = line.Split(',');
                Heat heat = new Heat
                {
                    EventName = split[0],
                    Record = split[1],
                    Lane1Name = split[2],
                    Lane2Name = split[3],
                    Lane3Name = split[4],
                    Lane4Name = split[5],
                    Lane5Name = split[6],
                    Lane6Name = split[7]
                };
                heats.Add(heat);
            }

            updateHeats();
            updateButtonStatus();
        }
    }
}
