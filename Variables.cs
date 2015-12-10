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
        // Global variables (accessible by all forms)

        public static bool confirmClose = false;       // ensures user only asked once if they are sure they want to close the app
        public static double me = 1;
        public static SerialPort ComPort = new SerialPort();        // serial port object for data communication
        public static List <float> waypoints = new List<float>();   // holds the waypoint coordinates for data transfer
        public static bool autoCalibration = false;
        public static List<float> basecampCoordinates = new List<float>();      // holds basecamp coordinates
        public static float speed = 5;                              // trip speed
        public static Form RefToForm1 { get; set;}                  // reference to Form 1
        public static Form RefToForm2 { get; set;}                  // reference to Form 2
        public static Form RefToForm3 { get; set;}                  // reference to Form 3
        public static Form RefToForm4 { get; set;}                  // reference to Form 4
        public static Form RefToForm5 { get; set;}                  // reference to Form 5
    }
}
