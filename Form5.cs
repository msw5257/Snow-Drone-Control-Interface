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
    public partial class Form5 : Form
    {

        public Form5()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)  //  start new mission
        {
            this.Close();

            // 1. show form 1
            // 2. hide this form
        }

        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)   // if close (red x) is pressed
        {
            if (!Variables.confirmClose)
            {
                if (MessageBox.Show("This will close down the whole application, which may interrupt a trip in progress. Confirm?", "Close Application", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void Form5_FormClosed(Object sender, FormClosedEventArgs e)
        {
            if (!Variables.confirmClose)
            {
                MessageBox.Show("The application has been closed successfully.", "Application Closed!", MessageBoxButtons.OK);
                Variables.confirmClose = true;
                System.Windows.Forms.Application.Exit();
            }
        }

        private void Form5_Shown(object sender, EventArgs e)
        {

            for (int i = 0; i <= 50; i++)//output report
            {
                listBox1.Items.Add("[Time Stamp:" + Variables.me + "]"); //display time
                listBox1.Items.Add("            Route History"+Variables.me +": [ " + "LONG:" + Variables.me + " LAT:" + Variables.me + "]");//location
                listBox1.Items.Add("");

            }
           
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {


        }
    }
}
