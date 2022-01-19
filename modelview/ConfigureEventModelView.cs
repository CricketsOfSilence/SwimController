using ObsOverlay;
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

namespace SwimController.modelview
{
    public partial class ConfigureEventModelView : Form
    {
        private Event potentialEvent;
        public ConfigureEventModelView()
        {
            InitializeComponent();
            Repaint();
        }

        public ConfigureEventModelView(Event e)
        {
            InitializeComponent();           
            this.potentialEvent = e;
            Repaint();
        }

        private void Repaint()
        {
            this.eventNameTextBox.Text = potentialEvent.EventName;
            this.heatNumberTextBox.Text = potentialEvent.HeatNumber;
            this.recordTextBox.Text = potentialEvent.Record;
            this.lane1TextBox.Text = potentialEvent.GetLane(Lane.Lane1);
            this.lane2TextBox.Text = potentialEvent.GetLane(Lane.Lane2);
            this.lane3TextBox.Text = potentialEvent.GetLane(Lane.Lane3);
            this.lane4TextBox.Text = potentialEvent.GetLane(Lane.Lane4);
            this.lane5TextBox.Text = potentialEvent.GetLane(Lane.Lane5);
            this.lane6TextBox.Text = potentialEvent.GetLane(Lane.Lane6);
        }

        private void PreviousEventButton_Click(object sender, EventArgs e)
        {
            SetEventValues();
            
            if (potentialEvent.HasPreviousEvent())
            {
                potentialEvent = potentialEvent.PreviousEvent;
            }

            Repaint();
        }

        private void NextEventButton_Click(object sender, EventArgs e)
        {
            SetEventValues();           

            if (potentialEvent.HasNextEvent())
            {
                potentialEvent = potentialEvent.NextEvent;
                Repaint();
            }
            
            if (!potentialEvent.HasNextEvent()) 
            {
                nextEventButton.Text = "Done";
            }
        }

        private void SetEventValues()
        {
            potentialEvent.EventName = eventNameTextBox.Text;
            potentialEvent.HeatNumber = heatNumberTextBox.Text;
            potentialEvent.Record = recordTextBox.Text;
            potentialEvent.PutLane(Lane.Lane1, lane1TextBox.Text);
            potentialEvent.PutLane(Lane.Lane2, lane2TextBox.Text);
            potentialEvent.PutLane(Lane.Lane3, lane3TextBox.Text);
            potentialEvent.PutLane(Lane.Lane4, lane4TextBox.Text);
            potentialEvent.PutLane(Lane.Lane5, lane5TextBox.Text);
            potentialEvent.PutLane(Lane.Lane6, lane6TextBox.Text);
        }

        private void ConfigureEventModelView_Load(object sender, EventArgs e)
        {

        }

        public Event GetFirstEvent()
        {
            var e = potentialEvent;

            while (e.HasPreviousEvent())
            {
                e = e.PreviousEvent;
            }

            return e;
        }
    }
}
