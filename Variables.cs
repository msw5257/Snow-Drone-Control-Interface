using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Forms;

namespace SDCI
{
    class Variables
    {
        public static bool confirmClose = false;
        public static double me = 1;
        public static double[] tripData;
        public static SerialPort ComPort = new SerialPort();
        public static Form RefToForm1 { get; set;}
        public static Form RefToForm2 { get; set;}
        public static Form RefToForm3 { get; set;}
        public static Form RefToForm4 { get; set;}
        public static Form RefToForm5 { get; set;}
    }
}
