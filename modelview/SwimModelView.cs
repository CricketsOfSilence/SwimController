using ObsOverlay.controller;
using ObsOverlay.parser;
using ObsOverlay.model;
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
using SwimController.modelview;

namespace ObsOverlay
{
    public partial class SwimModelView : Form
    {

        private readonly EventController eventController;

        public SwimModelView()
        {
            InitializeComponent();
            InitializeOtherComponents();
            this.eventController = new EventController();
            InitialLoad();
        }       

        private void Repaint()
        {
            Event e = eventController.GetCurrentEvent();

            if (e != null)
            {
                nextHeatButton.Enabled = e.HasNextEvent();
                previousHeatButton.Enabled = e.HasPreviousEvent();

                if (e.HasPreviousEvent())
                {
                    previousHeatName.Text = e.PreviousEvent.EventName;
                }
                else
                {
                    previousHeatName.Text = "N/A";
                }

                if (e.HasNextEvent())
                {
                    nextHeatName.Text = e.NextEvent.EventName;
                }
                else
                {
                    nextHeatName.Text = "N/A";
                }

                lane1TextBox.Text = e.GetLane(Lane.Lane1);
                lane2TextBox.Text = e.GetLane(Lane.Lane2);
                lane3TextBox.Text = e.GetLane(Lane.Lane3);
                lane4TextBox.Text = e.GetLane(Lane.Lane4);
                lane5TextBox.Text = e.GetLane(Lane.Lane5);
                lane6TextBox.Text = e.GetLane(Lane.Lane6);

                updateEventNameTextBox.Text = e.EventName;
            }
        }

        public void PreviousHeatButton_Click(object sender, EventArgs e)
        {
            eventController.GoToPreviousEvent();
            Repaint();
        }

        public void NextHeatButton_Click(object sender, EventArgs e)
        {
            eventController.GoToNextEvent();
            Repaint();
        }
        
        private void UpdateEventNameButton_Click(object sender, EventArgs e, String eventName)
        {
            eventController.SetEventName(eventName);
            Repaint();
        }

        private void LaneUpdateButton_Click(object sender, EventArgs e, Lane lane, String value)
        {
            eventController.SetLaneName(lane, value);
            Repaint();
        }

        private void InitialLoad()
        {
            var config = FilePathConfiguration.LoadFromConfig();
            eventController.SetConfig(config);
            Repaint();
        }

        private void ConfigureFilePathMenuItem_Click(object sender, EventArgs e)
        {
            using (ConfigureModelView configureController = new ConfigureModelView(eventController.GetConfig()))
            {
                configureController.Show();
                configureController.Visible = false;

                if (configureController.ShowDialog() == DialogResult.OK)
                {
                    //FilePathConfiguration config = new Fil
                    FilePathConfiguration config = configureController.GetConfig();
                    eventController.SetConfig(config);

                    config.WriteConfigFile();            
                    configureController.Close();
                }
            }
        }

        private void LoadEventDataMenuItem_Click(object sender, EventArgs e)
        {
            string eventDataFile = GetFileDialog();
            Event potentialEvent = null;
            switch (Path.GetExtension(eventDataFile))
            {
                case ".csv":
                    potentialEvent = CsvParser.Load(eventDataFile);
                    break;
                case ".html":
                    potentialEvent = HtmlParser.Load(eventDataFile);
                    break;
                default:
                    // throw exception
                    break;
            }

            using (ConfigureEventModelView mv = new ConfigureEventModelView(potentialEvent))
            {
                eventController.SetCurrentEvent(mv.GetFirstEvent());
            }
            Repaint();
        }

        private string GetFileDialog()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = Environment.CurrentDirectory;
                openFileDialog.Filter = "All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    return openFileDialog.FileName;
                }
            }

            return "";
        }

        private void ExitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
