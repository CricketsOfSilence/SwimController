using ObsOverlay.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObsOverlay
{
    public partial class ConfigureModelView : Form
    {
        private readonly FilePathConfiguration config;      

        public ConfigureModelView(FilePathConfiguration config)
        {
            InitializeComponent();
            this.config = config;            
            this.eventNameLabel.Text = config.EventName;
            this.heatNumberLabel.Text = config.HeatNumber;
            this.lane1Label.Text = config.GetLane(Lane.Lane1);
            this.lane2Label.Text = config.GetLane(Lane.Lane2);
            this.lane3Label.Text = config.GetLane(Lane.Lane3);
            this.lane4Label.Text = config.GetLane(Lane.Lane4);
            this.lane5Label.Text = config.GetLane(Lane.Lane5);
            this.lane6Label.Text = config.GetLane(Lane.Lane6);
        }

        private string OpenTextFileDialog()
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

        private void SetEventNameButton_Click(object sender, EventArgs e)
        {
            config.EventName = OpenTextFileDialog();
            eventNameLabel.Text = config.EventName;
            eventNameLabel.BorderStyle = BorderStyle.FixedSingle;
        }
        private void SetLane1Location_Click(object sender, EventArgs e)
        {
            config.Lanes[Lane.Lane1] = OpenTextFileDialog();
            lane1Label.Text = config.Lanes[Lane.Lane1];
            lane1Label.BorderStyle = BorderStyle.FixedSingle;
        }

        private void SetLane2Location_Click(object sender, EventArgs e)
        {
            config.Lanes[Lane.Lane2] = OpenTextFileDialog();
            lane2Label.Text = config.Lanes[Lane.Lane2];
            lane2Label.BorderStyle = BorderStyle.FixedSingle;
        }

        private void SetLane3Location_Click(object sender, EventArgs e)
        {
            config.Lanes[Lane.Lane3] = OpenTextFileDialog();
            lane3Label.Text = config.Lanes[Lane.Lane3];
            lane3Label.BorderStyle = BorderStyle.FixedSingle;
        }

        private void SetLane4Location_Click(object sender, EventArgs e)
        {
            config.Lanes[Lane.Lane4] = OpenTextFileDialog();
            lane4Label.Text = config.Lanes[Lane.Lane4];
            lane4Label.BorderStyle = BorderStyle.FixedSingle;
        }

        private void SetLane5Location_Click(object sender, EventArgs e)
        {
            config.Lanes[Lane.Lane5] = OpenTextFileDialog();
            lane5Label.Text = config.Lanes[Lane.Lane5];
            lane5Label.BorderStyle = BorderStyle.FixedSingle;
        }

        private void SetLane6Location_Click(object sender, EventArgs e)
        {
            config.Lanes[Lane.Lane6] = OpenTextFileDialog();
            lane6Label.Text = config.Lanes[Lane.Lane6];
            lane6Label.BorderStyle = BorderStyle.FixedSingle;
        }

        private void HeatNumberButton_Click(object sender, EventArgs e)
        {
            config.HeatNumber = OpenTextFileDialog();
            heatNumberLabel.Text = config.HeatNumber;
            heatNumberLabel.BorderStyle = BorderStyle.FixedSingle;
        }

        public FilePathConfiguration GetConfig()
        {
            return config;
        }
    }
}
