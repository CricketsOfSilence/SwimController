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
        private const string NO_HEAT = "N/A";

        private string swimmerHeatDataFilePath = "";
        private string eventNamePath = "eventname.txt";
        private string lane1Path = "lane1.txt";
        private string lane2Path = "lane2.txt";
        private string lane3Path = "lane3.txt";
        private string lane4Path = "lane4.txt";
        private string lane5Path = "lane5.txt";
        private string lane6Path = "lane6.txt";
        
        private int heatNumber;
        private readonly List<Heat> heats;
        public HeatController()
        {
            InitializeComponent();
            heats = new List<Heat>();
            heatNumber = 0;
        }

        public HeatController(String basePath, List<Heat> heats) : this()
        {
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
            if (heatNumber >= 0 && heatNumber < heats.Count)
            {
                Heat currentHeat = heats[heatNumber];
                
                if (heatNumber - 1 >= 0)
                {
                    previousHeatName.Text = heats[heatNumber - 1].EventName;
                }
                else
                {
                    previousHeatName.Text = NO_HEAT;
                }

                if (heatNumber + 1 < heats.Count)
                {
                    nextHeatName.Text = heats[heatNumber + 1].EventName;
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
            if (heatNumber >= 0) {
                heatNumber--;
                updateHeats();
                updateButtonStatus();
            }           
        }

        public void nextHeatButton_Click(object sender, EventArgs e)
        {
            if (heatNumber < heats.Count)
            {
                heatNumber++;
                updateHeats();
                updateButtonStatus();
            }                     
        }

        private void updateButtonStatus()
        {
            if (heatNumber <= 0)
            {
                previousHeatButton.Enabled = false;
                previousHeatName.Text = NO_HEAT;
            } 
            else
            {
                previousHeatButton.Enabled = true;
                previousHeatName.Text = heats[heatNumber - 1].EventName;
            }

            if (heatNumber >= heats.Count - 1)
            {
                nextHeatButton.Enabled = false;
                nextHeatName.Text = NO_HEAT;
            }
            else
            {
                nextHeatButton.Enabled = true;
                nextHeatName.Text = heats[heatNumber + 1].EventName;
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
                System.IO.File.WriteAllText(fileName, text);
            }
        }

        private void HeatController_Load(object sender, EventArgs e)
        {

        }
        private void updateEventNameButton_Click(object sender, EventArgs e)
        {
            heats[heatNumber].EventName = updateEventNameTextBox.Text;
            write(eventNamePath, heats[heatNumber].EventName);
        }

        private void lane1UpdateButton_Click(object sender, EventArgs e)
        {
            heats[heatNumber].Lane1Name = lane1TextBox.Text;
            write(lane1Path, heats[heatNumber].Lane1Name);
        }
        private void lane2UpdateButton_Click(object sender, EventArgs e)
        {
            heats[heatNumber].Lane1Name = lane2TextBox.Text;
            write(lane2Path, heats[heatNumber].Lane2Name);
        }

        private void lane3UpdateButton_Click(object sender, EventArgs e)
        {
            heats[heatNumber].Lane3Name = lane3TextBox.Text;
            write(lane3Path, heats[heatNumber].Lane3Name);
        }                

        private void lane4UpdateButton_Click(object sender, EventArgs e)
        {
            heats[heatNumber].Lane4Name = lane4TextBox.Text;
            write(lane4Path, heats[heatNumber].Lane4Name);
        }

        private void lane5UpdateButton_Click(object sender, EventArgs e)
        {
            heats[heatNumber].Lane5Name = lane5TextBox.Text;
            write(lane5Path, heats[heatNumber].Lane5Name);
        }

        private void lane6UpdateButton_Click(object sender, EventArgs e)
        {
            heats[heatNumber].Lane6Name = lane6TextBox.Text;
            write(lane6Path, heats[heatNumber].Lane6Name);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void configureButton_Click(object sender, EventArgs e)
        {
            using (ConfigureController configureController = new ConfigureController())
            {
                configureController.Show();
                configureController.Visible = false;
    
                if (configureController.ShowDialog() == DialogResult.OK)
                {

                    if (!configureController.SwimmerHeatDataFilePath.Equals("...")) {
                        swimmerHeatDataFilePath = configureController.SwimmerHeatDataFilePath;
                    }

                    if (!configureController.EventNameFilePath.Equals("..."))
                    {
                        eventNamePath = configureController.EventNameFilePath;
                    }

                    if (!configureController.Lane1FilePath.Equals("..."))
                    {
                        lane1Path = configureController.Lane1FilePath;
                    }

                    if (!configureController.Lane2FilePath.Equals("..."))
                    {
                        lane2Path = configureController.Lane2FilePath;
                    }

                    if (!configureController.Lane3FilePath.Equals("..."))
                    {
                        lane3Path = configureController.Lane3FilePath;
                    }

                    if (!configureController.Lane4FilePath.Equals("..."))
                    {
                        lane4Path = configureController.Lane4FilePath;
                    }

                    if (!configureController.Lane5FilePath.Equals("..."))
                    {
                        lane5Path = configureController.Lane5FilePath;
                    }

                    if (!configureController.Lane6FilePath.Equals("..."))
                    {
                        lane6Path = configureController.Lane6FilePath;
                    }                 

                    LoadData();
                    configureController.Close();
                }            
            }                      
        }

        private void LoadData()
        {
            heats.Clear();
            string[] lines = System.IO.File.ReadAllLines(swimmerHeatDataFilePath);

            foreach (string line in lines)
            {
                string[] split = line.Split(',');
                Heat heat = new Heat();
                heat.EventName = split[0];
                heat.Record = split[1];
                heat.Lane1Name = split[2];
                heat.Lane2Name = split[3];
                heat.Lane3Name = split[4];
                heat.Lane4Name = split[5];
                heat.Lane5Name = split[6];
                heat.Lane6Name = split[7];
                heats.Add(heat);
            }

            updateHeats();
            updateButtonStatus();
        }
    }
}
