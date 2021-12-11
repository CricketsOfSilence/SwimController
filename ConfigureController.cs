using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwimController
{
    public partial class ConfigureController : Form
    {
        private ConfigurationModel config;      

        public ConfigureController(ConfigurationModel config)
        {
            InitializeComponent();
            this.config = config;
            this.swimmerHeatDataLabel.Text = config.HeatSheetFilePath;
            this.eventNameLabel.Text = config.EventNameFilePath;
            this.heatNumberLabel.Text = config.HeatNumberFilePath;
            this.lane1Label.Text = config.LaneFilePaths[0];
            this.lane2Label.Text = config.LaneFilePaths[1];
            this.lane3Label.Text = config.LaneFilePaths[2];
            this.lane4Label.Text = config.LaneFilePaths[3];
            this.lane5Label.Text = config.LaneFilePaths[4];
            this.lane6Label.Text = config.LaneFilePaths[5];
        }

        private string openCsvFileDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"c:\";
                openFileDialog.Filter = "csv files (*.csv)|*.csv|txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    return openFileDialog.FileName;
                }
            }

            return "";
        }

        private string openTextFileDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"c:\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    return openFileDialog.FileName;
                }
            }

            return "";
        }

        private void loadSwimmerHeatDataButton_Click(object sender, EventArgs e)
        {
            config.HeatSheetFilePath = openCsvFileDialog();            
            swimmerHeatDataLabel.Text = config.HeatSheetFilePath;
            swimmerHeatDataLabel.BorderStyle = BorderStyle.FixedSingle;
        }

        private void setEventNameButton_Click(object sender, EventArgs e)
        {
            config.EventNameFilePath = openTextFileDialog();
            eventNameLabel.Text = config.EventNameFilePath;
            eventNameLabel.BorderStyle = BorderStyle.FixedSingle;
        }
        private void setLane1Location_Click(object sender, EventArgs e)
        {
            config.LaneFilePaths[0] = openTextFileDialog();
            lane1Label.Text = config.LaneFilePaths[0];
            lane1Label.BorderStyle = BorderStyle.FixedSingle;
        }

        private void setLane2Location_Click(object sender, EventArgs e)
        {
            config.LaneFilePaths[1] = openTextFileDialog();
            lane2Label.Text = config.LaneFilePaths[1];
            lane2Label.BorderStyle = BorderStyle.FixedSingle;
        }

        private void setLane3Location_Click(object sender, EventArgs e)
        {
            config.LaneFilePaths[2] = openTextFileDialog();
            lane3Label.Text = config.LaneFilePaths[2];
            lane3Label.BorderStyle = BorderStyle.FixedSingle;
        }

        private void setLane4Location_Click(object sender, EventArgs e)
        {
            config.LaneFilePaths[3] = openTextFileDialog();
            lane4Label.Text = config.LaneFilePaths[3];
            lane4Label.BorderStyle = BorderStyle.FixedSingle;
        }

        private void setLane5Location_Click(object sender, EventArgs e)
        {
            config.LaneFilePaths[4] = openTextFileDialog();
            lane5Label.Text = config.LaneFilePaths[4];
            lane5Label.BorderStyle = BorderStyle.FixedSingle;
        }

        private void setLane6Location_Click(object sender, EventArgs e)
        {
            config.LaneFilePaths[5] = openTextFileDialog();
            lane6Label.Text = config.LaneFilePaths[5];
            lane6Label.BorderStyle = BorderStyle.FixedSingle;
        }

        public ConfigurationModel GetConfiguration()
        {           
            return this.config;
        }

        private void heatNumberButton_Click(object sender, EventArgs e)
        {
            config.HeatNumberFilePath = openTextFileDialog();
            heatNumberLabel.Text = config.HeatNumberFilePath;
            heatNumberLabel.BorderStyle = BorderStyle.FixedSingle;
        }
    }
}
