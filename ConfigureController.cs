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
        public string SwimmerHeatDataFilePath = "";
        public string EventNameFilePath = "";
        public string Lane1FilePath = "";
        public string Lane2FilePath = "";
        public string Lane3FilePath = "";
        public string Lane4FilePath = "";
        public string Lane5FilePath = "";
        public string Lane6FilePath = "";


        public ConfigureController()
        {
            InitializeComponent();
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
            SwimmerHeatDataFilePath = openCsvFileDialog();
            swimmerHeatDataLabel.Text = SwimmerHeatDataFilePath;
        }

        private void setEventNameButton_Click(object sender, EventArgs e)
        {
            EventNameFilePath = openTextFileDialog();
            eventNameLabel.Text = EventNameFilePath;
        }
        private void setLane1Location_Click(object sender, EventArgs e)
        {
            Lane1FilePath = openTextFileDialog();
            lane1Label.Text = Lane1FilePath;
        }

        private void setLane2Location_Click(object sender, EventArgs e)
        {
            Lane2FilePath = openTextFileDialog();
            lane2Label.Text = Lane2FilePath;
        }

        private void setLane3Location_Click(object sender, EventArgs e)
        {
            Lane3FilePath = openTextFileDialog();
            lane3Label.Text = Lane3FilePath;
        }

        private void setLane4Location_Click(object sender, EventArgs e)
        {
            Lane4FilePath = openTextFileDialog();
            lane4Label.Text = Lane4FilePath;
        }

        private void setLane5Location_Click(object sender, EventArgs e)
        {
            Lane5FilePath = openTextFileDialog();
            lane5Label.Text = Lane5FilePath;
        }

        private void setLane6Location_Click(object sender, EventArgs e)
        {
            Lane6FilePath = openTextFileDialog();
            lane6Label.Text = Lane6FilePath;
        }

    }
}
