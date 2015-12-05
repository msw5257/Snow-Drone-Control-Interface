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
    public partial class Form2 : Form
    {
        int index = 0;      // number of waypoints
        List<string> items = new List<string>();

        public Form2()
        {
            InitializeComponent();

            listBox1.DataSource = items;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
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
                Variables.confirmClose = true; 
                System.Windows.Forms.Application.Exit();
            }
        }

        private void button4_Click(object sender, EventArgs e)  // upload button
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
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
                    label4.Text = "Longitude";

                    if (double.TryParse(textBox1.Text, out n))
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

        private void button1_Click(object sender, EventArgs e)
        {
            index++;
            items.Add(index.ToString() + "\t" + textBox1.Text + "\t" + textBox2.Text + "\n");

            listBox1.DataSource = null;
            listBox1.DataSource = items;

            textBox1.Text = "Latitude";
            textBox2.Text = "Longitude";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
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
                    label3.Text = "Latitude";

                    if (double.TryParse(textBox2.Text, out n))
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

        private void button2_Click(object sender, EventArgs e)
        {
            // The Remove button was clicked
            try
            {
                // Remove the item in the List.
                items.RemoveAt(index - 1);
                index--;
            }
            catch
            {
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
                    items.RemoveAt(index - 1);
                    index--;
                }
                catch
                {
                }
            }

            listBox1.DataSource = null;
            listBox1.DataSource = items;
        }
    }
}
