using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SDCI
{
    public partial class Form3 : Form
    {
        public Form RefToForm2 { get; set; }

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)  //  start button
        {
            // 1. send data to serial port (upload mission info)
            // 2. send start command to serial port
            if (Variables.RefToForm4 == null)
            {
                Form4 f4 = new Form4();
                f4.Show();
            }
            else
            {
                Variables.RefToForm4.Show();
            }

            this.Hide();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Variables.RefToForm3 = this;                // point the reference to form 3 to this form
        }

        private void button2_Click(object sender, EventArgs e)  //  edit button
        {
            Variables.RefToForm2.Show();         // show Form 2 again (using a reference to it) to edit trip
            this.Hide();                    // hide this form
        }


        private void Form3_Shown(object sender, EventArgs e)
        {
            listBox1.Items.Add("[SYSTEM INFO:]");
            if (true)   //check what the user chooses
            {
                listBox1.Items.Add("            Calibration Selected: [MANUAL]");   //show Auto Calib Result or Manual
                listBox1.Items.Add("");
                listBox1.Items.Add("            Calibrated Input: [ " + " LONG:" + Variables.me + " LAT:" + Variables.me + "]");
            }
            else
            {
                listBox1.Items.Add("            Calibration Selected: [AUTOMATIC]");   //show Auto Calib Result or Manual
            }
            listBox1.Items.Add("");

            listBox1.Items.Add("            Speed Input: [ " + Variables.me + " MPH]"); //speed output
            listBox1.Items.Add("");

            listBox1.Items.Add("[WAYPOINT INFO:]");
            listBox1.Items.Add("            Total Waypoints: [ " + Variables.me + " Points]");
            for (int i = 0; i <= 50; i++)//output waypoints
            {
                listBox1.Items.Add("            Point" + i + " :[ " + "LONG:" + Variables.me + " LAT:" + Variables.me + "]");
                listBox1.Items.Add("");
            }
            //final note
            listBox1.Items.Add("[END OF VERIFICATION.]");
            listBox1.Items.Add("[PRESS EDIT TO CHANGE YOUR CURRENT MISSION INFO]");
            listBox1.Items.Add("[PRESS START TO START MISSION]");

        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)   // if close (red x) is pressed
        {
            if (!Variables.confirmClose)
            {
                if (MessageBox.Show("This will close down the whole application, which may interrupt a trip in progress. Confirm?", "Close Application", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void Form3_FormClosed(Object sender, FormClosedEventArgs e)
        {
            if (!Variables.confirmClose)
            {
                MessageBox.Show("The application has been closed successfully.", "Application Closed!", MessageBoxButtons.OK);

                Variables.confirmClose = true;

                System.Windows.Forms.Application.Exit();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
    }

       
}
