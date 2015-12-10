using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace SDCI
{
    public partial class Form2 : Form
    {
        List<string> items = new List<string>(); // holds the items in the waypoint log
        int index = 0;                          // number of waypoints
        bool validSpeed = true;                 // app starts with a valid default speed
        bool validWaypointLog = false;          // are there any waypoints entered? if not, no upload allowed
        bool calibrationSelected = false;       // did user select calibration? if not, no upload allwoed

        public Form2()
        {
            InitializeComponent();
            listBox1.DataSource = items;
        }

        private void Form2_Load(object sender, EventArgs e) // on form 2 startup
        {
            Variables.RefToForm2 = this;        // point the reference to form 2 at this form
            
            button1.Enabled = false;            // disable waypoint add until valid waypoint entered
            button4.Enabled = false;            // disable Upload until waypoints entered

            // This section of code prints all the computers COM ports in use into a drop down comboBox
            // The user must then select which one the radio is using
            string[] ArrayComPortsNames = null;
            int index = -1;
            string ComPortName = null;

            ArrayComPortsNames = SerialPort.GetPortNames();
            do
            {
                index += 1;
                comboBox1.Items.Add(ArrayComPortsNames[index]);
            }

            while (!((ArrayComPortsNames[index] == ComPortName)
                          || (index == ArrayComPortsNames.GetUpperBound(0))));
            Array.Sort(ArrayComPortsNames);

            //want to get first out
            if (index == ArrayComPortsNames.GetUpperBound(0))
            {
                ComPortName = ArrayComPortsNames[0];
            }
            comboBox1.Text = ArrayComPortsNames[0];
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)   // if close (red x) is pressed
        {
            if (!Variables.confirmClose)
            {
                if (MessageBox.Show("This will close down the whole application, which may interrupt a trip in progress. Confirm?", "Close Application", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void Form2_FormClosed(Object sender, FormClosedEventArgs e)
        {
            if (!Variables.confirmClose)
            {
                MessageBox.Show("The application has been closed successfully.", "Application Closed!", MessageBoxButtons.OK);
                Variables.confirmClose = true;              // this ensures we don't check before closing each individual form
                System.Windows.Forms.Application.Exit();
            }
        }

        private void button4_Click(object sender, EventArgs e)  // upload button
        {
            // add speed, calibration bytes, and waypoints to array (Variables.tripData) to be uploaded

            Variables.ComPort.PortName = comboBox1.Text;        // set the COM port name for upload
            Variables.speed = float.Parse(textBox3.Text);       // convert speed text to float and set the global speed variable
            Variables.basecampCoordinates.Clear();              // clear in case editting old data
            Variables.basecampCoordinates.Add(float.Parse(textBox4.Text));      // add latitude coordinate
            Variables.basecampCoordinates.Add(float.Parse(textBox5.Text));      // add longitude coordinate


            // if Form 3 isn't already opened, open it. Otherwise, show the open Form 3
            if (Variables.RefToForm3 == null)
            {
                Form3 f3 = new Form3();
                f3.Show();
            }
            else
            {
                Variables.RefToForm3.Show();
            }

            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)   // latitude waypoint input
        {
            double n;

            if (!double.TryParse(textBox1.Text, out n)) // check if input isn't a number
            {
                label3.Text = "must be a number";
                button1.Enabled = false;
            }
            else
            {
                if ((n <= -65) && (n >= -90))
                {
                    double z;

                    label3.Text = "Latitude";

                    if (double.TryParse(textBox2.Text, out z) && (z >= 0) && (z <= 180))
                    {
                        button1.Enabled = true;
                    }
                }
                else
                {
                    label3.Text = "must be between\n-65 and -90";
                    button1.Enabled = false;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)   // longitude waypoint input
        {
            double n;

            if (!double.TryParse(textBox2.Text, out n)) // check if input isn't a number
            {
                label4.Text = "must be a number";
                button1.Enabled = false;
            }
            else
            {
                if ((n <= 180) && (n >= 0))
                {
                    double z;

                    label4.Text = "Longitude";

                    if (double.TryParse(textBox1.Text, out z) && (z >= -90) && (z <= -65))
                    {
                        button1.Enabled = true;
                    }
                }
                else
                {
                    label4.Text = "must be between\n0 and 180";
                    button1.Enabled = false;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)  // add waypoint button
        {
            index++;
            items.Add(index.ToString().PadRight(10) + textBox1.Text.PadRight(6) + "\t" + textBox2.Text + "\n");
            
            Variables.waypoints.Add(float.Parse(textBox1.Text));     // add latitude to waypoint list
            Variables.waypoints.Add(float.Parse(textBox2.Text));     // add longitude to waypoint list

            listBox1.DataSource = null;
            listBox1.DataSource = items;

            textBox1.Text = "Latitude";
            textBox2.Text = "Longitude";

            validWaypointLog = true;            // at least one valid waypoint now

            if (validSpeed && validWaypointLog && calibrationSelected)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)  // The Remove button was clicked
        {
            try
            {
                items.RemoveAt(index - 1);      // remove the item from the waypoint log
                Variables.waypoints.RemoveAt(2*index - 1);    // remove the longitude waypoint from the waypoint list
                Variables.waypoints.RemoveAt(2*index - 2);    // remove the latitude waypoint from the waypoint list
                index--;
            }
            catch
            {
            }

            if (index < 1)                  // if last waypoint deleted, no waypoints to upload
            {
                validWaypointLog = false;
                button4.Enabled = false;    // disable upload button
            }

            if (validSpeed && validWaypointLog && calibrationSelected)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }

            listBox1.DataSource = null;
            listBox1.DataSource = items;
        }

        private void button3_Click(object sender, EventArgs e)  // delete all waypoints
        {
            // The Delete all Waypoints button was clicked
            int temp_index = index;

            for (int i = 0; i < temp_index; i++)
            {
                try
                {
                    // Remove the item in the List.
                    items.RemoveAt(index - 1);  // remove the item from the waypoint log
                    index--;
                    Variables.waypoints.Clear();    // clear the list of waypoint for data transfer
                }
                catch
                {
                }
            }

            validWaypointLog = false;               // no longer any waypoints to upload
            button4.Enabled = false;                // disable upload button

            listBox1.DataSource = null;
            listBox1.DataSource = items;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)   // speed input
        {
            double n;

            if (!double.TryParse(textBox3.Text, out n)) // check if input isn't a number
            {
                label6.Text = "must be a number";
                button4.Enabled = false;
                validSpeed = false;
            }
            else
            {
                if ((n <= 20) && (n > 0))
                {
                    label6.Text = "";
                    validSpeed = true;
                }
                else
                {
                    label6.Text = "must be between\n0 and 20";
                    button4.Enabled = false;
                    validSpeed = false;
                }
            }

            if (validSpeed && validWaypointLog && calibrationSelected)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)   // auto calibration - send dummy bytes to onboard controls
            {
                textBox4.Text = "-100";       // dummy bytes must be something other than possible coordinates
                textBox5.Text = "-100";
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                calibrationSelected = true;
                Variables.autoCalibration = true;           // set global variable to true
            }           // else manual calibration - allow user to enter basecamp coordinates
            else
            {
                textBox4.Text = "Latitude";
                textBox5.Text = "Longitude";
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                Variables.autoCalibration = false;          // set global variable to false

                double n;

                if (!(double.TryParse(textBox4.Text, out n) && (n <= -65) && (n >= -90))) // check if input isn't a number
                {
                    calibrationSelected = false;
                }
                else
                {
                    double z;

                    if (double.TryParse(textBox5.Text, out z) && (z >= 0) && (z <= 180) && radioButton2.Checked)
                    {
                        calibrationSelected = true;
                    }
                    else
                    {
                        calibrationSelected = false;
                    }
                }
            }
            
            if (validSpeed && validWaypointLog && calibrationSelected)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }

        }

        private void textBox4_TextChanged(object sender, EventArgs e)   // manual basecamp input (latitude)
        {
            double n;

            if (!double.TryParse(textBox4.Text, out n)) // check if input isn't a number
            {
                label9.Text = "must be a number";
                calibrationSelected = false;
            }
            else
            {
                if ((n <= -65) && (n >= -90))
                {
                    double z;

                    label9.Text = "Latitude";

                    if (double.TryParse(textBox5.Text, out z) && (z >= 0) && (z <= 180) && radioButton2.Checked)
                    {
                        calibrationSelected = true;
                    }
                }
                else
                {
                    label9.Text = "must be between\n-65 and -90";
                    calibrationSelected = false;
                }
            }

            if (validSpeed && validWaypointLog && calibrationSelected)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            double n;

            if (!double.TryParse(textBox5.Text, out n)) // check if input isn't a number
            {
                label10.Text = "must be a number";
                calibrationSelected = false;
            }
            else
            {
                if ((n <= 180) && (n >= 0))
                {
                    double z;

                    label10.Text = "Longitude";

                    if (double.TryParse(textBox4.Text, out z) && (z >= -90) && (z <= -65) && radioButton2.Checked)
                    {
                        calibrationSelected = true;
                    }
                }
                else
                {
                    label10.Text = "must be between\n0 and 180";
                    calibrationSelected = false;
                }
            }

            if (validSpeed && validWaypointLog && calibrationSelected)
            {
                button4.Enabled = true;
            }
            else
            {
                button4.Enabled = false;
            }
        }
    }
}
