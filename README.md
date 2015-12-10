# Snow-Drone-Control-Interface
C# Form Application for Antarctic Research


## Subsystem Overview

The Snow Drone Control Interface (SDCI) is a C# Windows Forms application that allows the user to communicate with the Snow Drone’s onboard control. The SDCI creates a mission plan based on the user’s inputted waypoints, speed, and basecamp calibration, and displays the current drone location, fuel level(if implemented), and speed while the Snow Drone is out in the field. 

Designed as a Windows Forms application in Microsoft Visual Studio, SDCI walks through its mission lifecycle with the user from Mission Planning, to Mission Overview/Data Transfer, to Mission Update, and finally to Route History.

The SDCI assumes the waypoint data and commands will be transferred using the Communication team’s radio, which will be connected to the user’s computer serial port via USB. One feature of this application is the ability to automatically detect any COM ports in use by the computer, and provide a dropdown list for the user to select which one the radio is connected to.

The SDCI also assumes constant connection with the Snow Drone, so while data is streamed back to the SDCI during missions, the SDCI will acknowledge reception of the data. If connection is lost (either the SDCI does not receive periodic updates or the Snow Drone does not receive an ACK before a decided upon timeout period), the Snow Drone onboard controls should direct the vehicle to immediately return to the basecamp. This means that if the program is closed while a mission is in progress, the Snow Drone will assume loss of connection and head back to base. Checks have been put in place to ensure the user means to close the application when he or she clicks the close button so that this will not happen.


## Major Application Features:

* Input Waypoint Check: Does not allow waypoints to be added to the mission unless they contain latitude and longitude coordinates that fall within the area of Antarctica (-65 <= latitude <= -90 and 0 <= longitude <= 180 degrees).

* Speed Input Check: Checks that the speed input is greater than 0 mph and less than or equal to 20 mph.

* Auto or Manual Basecamp Calibration: Auto basecamp calibration adds a dummy byte to the data to be sent to the onboard controls (to be agreed upon with the onboard Controls team). This dummy byte tells the onboard controls to use the current GPS coordinates as the basecamp coordinates. Otherwise, the user may select manual calibration and input the latitude and longitude coordinates of the basecamp. These coordinates are verified in the same way as the waypoint coordinates.

* Application Closure Double Check: To make sure the user does not accidentally close the app and lose data or lose connection to the Snow Drone and cancel the mission, a message box will display the first time the app tries to close for any reason, and makes the user confirm that they would like to close the app before doing so. If the user selects no to the confirmation question, the app will resume where it left off.

* Mission Overview/Edit: Displays the details inputted by the user on Form 3 in a condensed format for review. If there is a mistake, the user may press Edit and return to Form 2 to change the mission details.

* Serial Port Selection: Upon loading Form 2 (Mission Planner), a combo box drop down list displays all the computer’s COM ports in use. From these, the user selects the one pertaining to the radio USB input.


## Future Features:

* Mission Updates: Form 4 will display periodic updates from the Onboard Controls of the mission’s progress (latitude and longitude, speed, etc.).

* Route History: Updates from the Snow Drone as the mission progresses (current location, speed, timestamps, etc.) should be stored in a text file (named after the date and time) as they are received, and then should be displayed on the last form of the app (Form 5) for the user to review.

* Serial Port Data Transmission: Commands to and data from the onboard controls will be sent via this serial port, which is connected to a radio via USB.

* Integration with Google Maps: Utilize the Google Maps API to display a clickable map for waypoint input. Waypoints can either be specified by inputting the latitude and longitude coordinates in the input boxes provided, or by clicking points on the map and pressing Add Waypoint. Since the basecamp will not have consistent Internet access, the map data will have to be downloaded to the computer prior to use. https://github.com/yanzhezhaodavid/SDCI_map/blob/master/loginPage.html
The code at the above URL is one attempt at tackling the Integration with Maps feature. The code can help the scientist present the movement of the vehicle on the map in the interface. Type in the latitude and longitude, and the vehicle will go to the destination in order. showing the Snow Drone’s trip visually. The vehicle is given in pixel size. The map is also a picture, so the Snow Drone is actually moving a specified number of pixels on the map. For students next semester, they will need to design a good map on the interface and then give the vehicle a proper size, then it will move more accurately. The code still have some problems currently. A nice feature for this interface would be the display of the fuel level, the GPR connection, and the intensity of the GPS signal. To do this, it would be best to create a database to receive and store the signals sent from other groups, then compact and send the updated information out.
 
* Data Loss Prevention: Store mission data in case of an app crash so that it can be recovered when the app is reopened.

* Serial Port Baud Rate Input: Provide a textbox input for the user to input the baud rate for serial transmission in case the protocol changes in the future, or the current protocol design does not use the default 9600 baud. Be sure to check the that the input is an integer value and that it falls between some reasonable baud rate range. Do not allow Upload to be pressed until this input is valid. Conversely, a combo box drop down list could let the user select between common baud rates.

* More Accurate Input Waypoint Check: Use the current position of the Snow Drone/basecamp and latitude and longitude distance calculations to determine if the waypoints are close enough to the current position to be valid. This link may be useful: http://www.movable-type.co.uk/scripts/latlong.html.

* Speed Input in mph or km/hr: Allow the user to select from a dropdown list either mph or km/hr and the application will convert to mph either way to send to the onboard controls.

* Save/Load Missions: Allow the data for a particular mission (waypoints, speed, calibration, etc.) to be saved in a text file with the mission name as the file name in a particular format so that it can be loaded into the application input fields where it can be edited or used as is again.
