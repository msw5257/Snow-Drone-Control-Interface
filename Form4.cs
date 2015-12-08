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
    public partial class Form4 : Form
    {
        // bool snowdroneMoving == false;

        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)  //  start/stop
        {
            // if (snowdroneMoving)
            // {
            //     send stop command
            //change switch varaible to "Stop"
            // }
            // else
            // {
            //     send start command
            //change switch varaible to "Start"
            // }

            // snowdroneMoving = !snowdroneMoving;     // invert state of bool

        }

        private void button2_Click(object sender, EventArgs e)  // abort mission
        {
            // send abort mission command
            //+ change switch varaible to "Complete"
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Variables.RefToForm5 == null)
            {
                Form5 f5 = new Form5();
                f5.Show();
            }
            else
            {
                Variables.RefToForm5.Show();
            }

            this.Hide();

        }

        private void Form4_Shown(object sender, EventArgs e)
        {
            //disable and remove transition button

            button3.Enabled = false;
            button3.Visible = false;
            /*switch (textBox2.Text)
            {
                case "PackageArrived":  //received package
                    listBox1.Items.Add("[SNOW DRONE UPDATE RECEIVED]");//header
                    listBox1.Items.Add("[Time Stamp:" + DateTime.Now.Hour + "]"); //display time
                    listBox1.Items.Add("            Current Location: [ " + "LONG:" + Variables.me + " LAT:" + Variables.me + "]");//location
                    listBox1.Items.Add("");

                    listBox1.Items.Add("            Current Speed:  [ " + Variables.me + " MPH]"); //speed output
                    listBox1.Items.Add("");

                    listBox1.Items.Add("            Current Fuel Level:  [ " + Variables.me + " Liters]"); //fuel output
                    listBox1.Items.Add("");

                    if (true) //check if error code exist
                    {
                        listBox1.Items.Add("            ERROR CODE: [" + Variables.me + "]");
                    }
                    else
                    {
                        listBox1.Items.Add("            ERROR CODE: [" + Variables.me + "]");
                    }
                    listBox1.Items.Add("");

                    //end header
                    listBox1.Items.Add("[END OF TRANSMISSION]");
                    listBox1.Items.Add("");

                    //+ change switch varaible to "NoPackage"

                    break;//end of PackageArrived case

                case "Start":
                    //send waiting transmission confermation
                    listBox1.Items.Add("[SNOW DRONE HAS RESTARTED]");
                    listBox1.Items.Add("");

                    //+ change switch varaible to "NoPackage"

                    break;

                case "Stop":
                    listBox1.Items.Add("[SNOW DRONE HAS STOPPED]");
                    listBox1.Items.Add("[AWATING START SIGNAL...]");
                    listBox1.Items.Add("");
                    break;

                case "Complete":
                    //send waiting transmission confermation
                    listBox1.Items.Add("[SNOW DRONE HAS COMPLETED MISSION]");
                    listBox1.Items.Add("[SETTING COURSE BACK TO BASE]");
                    listBox1.Items.Add("");

                    //disable abort button
                    button2.Enabled = false;


                    //+ change switch varaible to "NoPackage"

                    break; //end of Complete case


                case "NoPackage": //no package received
                    listBox1.Items.Add("[AWAITING SNOW DRONE UPDATE...]");
                    listBox1.Items.Add("");

                    //+ change switch varaible to "NoPackage"

                    break;

                case "Arrived@base": //arrived at base
                    listBox1.Items.Add("[SNOW DRONE HAS ARRIVED BACK AT BASE]");
                    listBox1.Items.Add("");
                    listBox1.Items.Add("[PRESS View Report BUTTON TO SEE MISSION REPORT]");
                    listBox1.Items.Add("");

                    //disable both abort and start/stop button
                    button1.Enabled = false;
                    button2.Enabled = false;
                    //show veiw report button
                    button3.Visible = true;
                    button3.Enabled = true;
                    button3.Focus();
                    break;
            }*/
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)   // if close (red x) is pressed
        {
            if (!Variables.confirmClose)
            {
                if (MessageBox.Show("This will close down the whole application, which may interrupt a trip in progress. Confirm?", "Close Application", MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void Form4_FormClosed(Object sender, FormClosedEventArgs e)
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

        private void Form4_Load(object sender, EventArgs e)
        {
            Variables.RefToForm4 = this;                // point the reference to form 4 to this form
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            switch (textBox2.Text)
            {
                case "PackageArrived":  //received package
                    listBox1.Items.Add("[SNOW DRONE UPDATE RECEIVED]");//header
                    listBox1.Items.Add("[Time Stamp:" + DateTime.Now.Hour + "]"); //display time
                    listBox1.Items.Add("            Current Location: [ " + "LONG:" + Variables.me + " LAT:" + Variables.me + "]");//location
                    listBox1.Items.Add("");

                    listBox1.Items.Add("            Current Speed:  [ " + Variables.me + " MPH]"); //speed output
                    listBox1.Items.Add("");

                    listBox1.Items.Add("            Current Fuel Level:  [ " + Variables.me + " Liters]"); //fuel output
                    listBox1.Items.Add("");

                    if (true) //check if error code exist
                    {
                        listBox1.Items.Add("            ERROR CODE: [" + Variables.me + "]");
                    }
                    else
                    {
                        listBox1.Items.Add("            ERROR CODE: [" + Variables.me + "]");
                    }
                    listBox1.Items.Add("");

                    //end header
                    listBox1.Items.Add("[END OF TRANSMISSION]");
                    listBox1.Items.Add("");

                    //+ change switch varaible to "NoPackage"

                    break;//end of PackageArrived case

                case "Start":
                    //send waiting transmission confermation
                    listBox1.Items.Add("[SNOW DRONE HAS RESTARTED]");
                    listBox1.Items.Add("");

                    //+ change switch varaible to "NoPackage"

                    break;

                case "Stop":
                    listBox1.Items.Add("[SNOW DRONE HAS STOPPED]");
                    listBox1.Items.Add("[AWATING START SIGNAL...]");
                    listBox1.Items.Add("");
                    break;

                case "Complete":
                    //send waiting transmission confermation
                    listBox1.Items.Add("[SNOW DRONE HAS COMPLETED MISSION]");
                    listBox1.Items.Add("[SETTING COURSE BACK TO BASE]");
                    listBox1.Items.Add("");

                    //disable abort button
                    button2.Enabled = false;


                    //+ change switch varaible to "NoPackage"

                    break; //end of Complete case


                case "NoPackage": //no package received
                    listBox1.Items.Add("[AWAITING SNOW DRONE UPDATE...]");
                    listBox1.Items.Add("");

                    //+ change switch varaible to "NoPackage"

                    break;

                case "Arrived@base": //arrived at base
                    listBox1.Items.Add("[SNOW DRONE HAS ARRIVED BACK AT BASE]");
                    listBox1.Items.Add("");
                    listBox1.Items.Add("[PRESS View Report BUTTON TO SEE MISSION REPORT]");
                    listBox1.Items.Add("");

                    //disable both abort and start/stop button
                    button1.Enabled = false;
                    button2.Enabled = false;
                    //show veiw report button
                    button3.Visible = true;
                    button3.Enabled = true;
                    button3.Focus();
                    break;
            }//end of switch

        }//end of button4 click

    }//end of class
}//end of namespace
