using System;
using System.Collections.Generic;
using System.IO;
using FinchAPI;

namespace Project_FinchControl
{

    // **************************************************
    //
    // Title: Finch Control - Menu Starter
    // Description: Starter solution with the helper methods,
    //              opening and closing screens, and the menu
    // Application Type: Console
    // Author: Riker, Colin
    // Dated Created: 2/20/2021
    // Last Modified: 1/25/2020
    //
    // **************************************************


    public struct Notes
    {
        public const double C4 = 261.63;
        public const double D4 = 293.66;
        public const double E4 = 329.63;
        public const double F4 = 349.23;
        public const double G4 = 392.00;
        public const double A4 = 440.00;
        public const double B4f = 466.16;
        public const double B4 = 493.88;
        public const double C5 = 523.25;
    }
    class Program
    {
        /// <summary>
        /// first method run when the app starts up
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            SetTheme();

            DisplayWelcomeScreen();
            DisplayMenuScreen();
            DisplayClosingScreen();
        }

        /// <summary>
        /// setup the console theme
        /// </summary>
        static void SetTheme()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.BackgroundColor = ConsoleColor.White;
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Main Menu                                 *
        /// *****************************************************************
        /// </summary>
        static void DisplayMenuScreen()
        {
            Console.CursorVisible = true;

            bool quitApplication = false;
            string menuChoice;

            Finch finchRobot = new Finch();

            do
            {
                DisplayScreenHeader("Main Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Connect Finch Robot");
                Console.WriteLine("\tb) Talent Show");
                Console.WriteLine("\tc) Data Recorder");
                Console.WriteLine("\td) Alarm System");
                Console.WriteLine("\te) User Programming");
                Console.WriteLine("\tf) Disconnect Finch Robot");
                Console.WriteLine("\tq) Quit");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        DisplayConnectFinchRobot(finchRobot);
                        break;

                    case "b":
                        TalentShowDisplayMenuScreen(finchRobot);
                        break;

                    case "c":
                        DataRecorderDisplayMenuScreen(finchRobot);
                        break;

                    case "d":
                        AlarmSystemDisplayMenuScreen(finchRobot);
                        break;

                    case "e":
                        UserProgrammingDisplayMenuScreen(finchRobot);
                        break;

                    case "f":
                        DisplayDisconnectFinchRobot(finchRobot);
                        break;

                    case "q":
                        DisplayDisconnectFinchRobot(finchRobot);
                        quitApplication = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitApplication);
        }

        #region TALENT SHOW

        /// <summary>
        /// *****************************************************************
        /// *                     Talent Show Menu                          *
        /// *****************************************************************
        /// </summary>
        static void TalentShowDisplayMenuScreen(Finch finchRobot)
        {
            Console.CursorVisible = true;

            bool quitTalentShowMenu = false;
            int menuChoice;
            bool goodinput;

            do
            {
                DisplayScreenHeader("Talent Show Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\t1) Light and Sound");
                Console.WriteLine("\t2) Dance");
                Console.WriteLine("\t3) Mixing It Up");
                Console.WriteLine("\t4) Main Menu");
                Console.Write("\t\tEnter Choice:");
                goodinput = int.TryParse(Console.ReadLine(), out menuChoice);

                if (!goodinput) menuChoice = -1;
                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case 1:
                        TalentShowDisplayLightAndSound(finchRobot);
                        break;

                    case 2:
                        TalentShowDisplayDance(finchRobot);
                        break;

                    case 3:
                        TalentShowDisplayMixingItUp(finchRobot);
                        break;

                    case 4:
                        quitTalentShowMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitTalentShowMenu);
        }

        static void TalentShowDisplayDance(Finch finchRobot)
        {
            DisplayScreenHeader("Dance Mode");
            
            finchRobot.setMotors(30, 30);
            finchRobot.wait(1000);
            finchRobot.setMotors(30, -30);
            finchRobot.wait(1000);
            finchRobot.setMotors(-30, 30);
            finchRobot.wait(1000);
            finchRobot.setMotors(-30, -30);
            finchRobot.wait(1000);
            finchRobot.setMotors(0, 0);


            DisplayMenuPrompt("Talent Show Menu");
        }

        static void TalentShowDisplayMixingItUp(Finch finchRobot)
        {
            DisplayScreenHeader("Mixing it up!");
            finchRobot.setMotors(63, -63); //Get the robot spinning in a circle
            int tempo = CalculateTempo(100); // Millie seconds per note;

            //Happy Birthday rather awkardly out of tune...
            finchRobot.setLED(255, 0, 0);
            playNote(finchRobot, tempo, Notes.C4);
            playNote(finchRobot, tempo*2, Notes.C4);

            finchRobot.setLED(0, 255, 0);
            playNote(finchRobot, tempo, Notes.D4);
            playNote(finchRobot, tempo, Notes.C4);
            playNote(finchRobot, tempo, Notes.F4);

            finchRobot.setLED(0, 0, 255);
            playNote(finchRobot, tempo, Notes.E4);
            playNote(finchRobot, tempo, Notes.C4);
            playNote(finchRobot, tempo, Notes.C4);

            finchRobot.setLED(255, 0, 0);
            playNote(finchRobot, tempo, Notes.D4);
            playNote(finchRobot, tempo, Notes.C4);
            playNote(finchRobot, tempo, Notes.G4);

            finchRobot.setLED(0, 255, 0);
            playNote(finchRobot, tempo, Notes.F4);
            playNote(finchRobot, tempo, Notes.C4);
            playNote(finchRobot, tempo, Notes.C4);

            finchRobot.setLED(0, 0, 255);
            playNote(finchRobot, tempo, Notes.C5);
            playNote(finchRobot, tempo, Notes.A4);
            playNote(finchRobot, tempo, Notes.F4);

            finchRobot.setLED(255, 0, 0);
            playNote(finchRobot, tempo, Notes.E4);
            playNote(finchRobot, tempo, Notes.D4);
            playNote(finchRobot, tempo, Notes.B4f);
            playNote(finchRobot, tempo, Notes.B4f);

            finchRobot.setLED(0, 255, 0);
            playNote(finchRobot, tempo, Notes.A4);
            playNote(finchRobot, tempo, Notes.F4);
            playNote(finchRobot, tempo, Notes.G4);

            finchRobot.setLED(0, 0, 255);
            playNote(finchRobot, tempo*3, Notes.F4);

            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();
            finchRobot.setMotors(0, 0);
            DisplayMenuPrompt("Talent Show Menu");
        }

        static void playNote(Finch finchRobot, int tempo, double note)
        {
            finchRobot.noteOn((int)note);
            finchRobot.wait(tempo);
        }

        static int CalculateTempo(int bmp)
        {
            return 60 / bmp * 1000;
        }

        /// <summary>
        /// *****************************************************************
        /// *               Talent Show > Light and Sound                   *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void TalentShowDisplayLightAndSound(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Light and Sound");

            Console.WriteLine("\tThe Finch robot will now show off its glowing talent!");
            DisplayContinuePrompt();

            for (int lightSoundLevel = 0; lightSoundLevel < 255; lightSoundLevel++)
            {
                finchRobot.setLED(lightSoundLevel, lightSoundLevel, lightSoundLevel);
                finchRobot.noteOn(lightSoundLevel * 100);
            }

            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            DisplayMenuPrompt("Talent Show Menu");
        }

        #endregion

        #region DATA RECORDER
        static void DataRecorderDisplayMenuScreen(Finch finchrobot)
        {
            bool quitDataRecorderMenu = false;
            string menuChoice;

            bool lightsensor = false;
            int numberOfDataPoints = 0;
            double dataPointFrequency = 0.0;
            double[] data = { };
  

            do
            {
                DisplayScreenHeader("Data Recorder Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) Change sensor (temp/light)");
                Console.WriteLine("\tb) Change # of data points");
                Console.WriteLine("\tc) Change frequency of data points");
                Console.WriteLine("\td) Get data");
                Console.WriteLine("\te) Show data");
                Console.WriteLine("\tf) return to main menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();

              
                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        lightsensor = DataRecorderSensorSelection();
                        break;
                    case "b":
                        numberOfDataPoints = DataRecorderDisplayGetNumberOfDataPoints();
                        break;

                    case "c":
                        dataPointFrequency = DataRecorderDisplayGetDataPointFrequence();
                        break;

                    case "d":
                        data = DataRecorderDisplayGetData(numberOfDataPoints, dataPointFrequency, lightsensor, finchrobot);
                        break;

                    case "e":
                        DataRecorderDisplayData(data);
                        break;

                    case "f":
                        quitDataRecorderMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitDataRecorderMenu);
        }

        static bool DataRecorderSensorSelection()
        {
            string menuChoice;
            bool goodinput = false;
            bool lightsensor = false;
            do
            {
                DisplayScreenHeader("Data Recorder");
                Console.WriteLine("What kind of data do you want to collect?");
                Console.WriteLine("\ta) Temperature");
                Console.WriteLine("\tb) Light");

                menuChoice = Console.ReadLine().ToLower();

                switch(menuChoice)
                {
                    case "a": 
                        lightsensor = false;
                        goodinput = true;
                        break;
                    case "b":
                        lightsensor = true;
                        goodinput = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!goodinput);

            return lightsensor;
        }

        static int DataRecorderDisplayGetNumberOfDataPoints()
        {
            DisplayScreenHeader("Data Recorder");

            Console.WriteLine("Please enter the number of samples you want");
            bool goodinput = false;
            int amount;
            
            do
            {
                goodinput = int.TryParse(Console.ReadLine(), out amount);
                if (!goodinput) Console.WriteLine("That's not a number try again!");
            } 
            while (!goodinput);
            
            DisplayContinuePrompt();
            return amount;
        }

        static double DataRecorderDisplayGetDataPointFrequence()
        {
            DisplayScreenHeader("Data Recorder");

            Console.WriteLine("Please enter the rate of sampling in seconds");
            bool goodinput = false;
            int frequency;

            do
            {
                goodinput = int.TryParse(Console.ReadLine(), out frequency);
                if (!goodinput) Console.WriteLine("That's not a number try again!");
            }
            while (!goodinput);

            DisplayContinuePrompt();
            return frequency;
        }

        static double[] DataRecorderDisplayGetData(int numberOfDataPoints, double dataPointFrequency,bool lightsensor ,Finch finchrobot)
        {
            DisplayScreenHeader("Data Recorder");
            double[] data = new double[numberOfDataPoints];

            string sensortype = "temperature";
            if (lightsensor) sensortype = "light level";
            Console.WriteLine("Data Recorder will be checking the {0}, at a rate of {2}, for a total number of {1} samples", sensortype, numberOfDataPoints, dataPointFrequency);
            Console.WriteLine("The application is ready to begin,");
            DisplayContinuePrompt();

            for(int i = 0; i < numberOfDataPoints; i++)
            {
                if (lightsensor) {
                    data[i] = (finchrobot.getLeftLightSensor() + finchrobot.getRightLightSensor()) / 2; //Avg of light sensors
                }
                else {
                    data[i] = ConvertCToF(finchrobot.getTemperature()); //Get temp convert to F
                }
                finchrobot.wait((int)(dataPointFrequency * 1000.0)); //Take decimal seconds turn them into ms and drop any fractional ms
            }

            Console.WriteLine("Data collection is complete");
            DisplayContinuePrompt();
            return data;
        }

        static void DataRecorderDisplayDataTable(double[] data)
        {
            int i = 0;
            Console.WriteLine("{0,-12} | {1, -10}", "Datapoint #", "Mesurement");
            foreach(double datapoint in data){
                i++;
                Console.WriteLine("{0, -12} | {1, -10}", i, datapoint);
            }
        }

        static void DataRecorderDisplayData(double[] data)
        {
            DisplayScreenHeader("Data Recorder");
            DataRecorderDisplayDataTable(data);
            DisplayContinuePrompt();
        }

        static double ConvertCToF(double celsius)
        {
            return (celsius * 1.8) + 32; //Credit http://www.srhartley.com/celsius-to-fahrenheit/formula/
        }
        #endregion

        #region ALARM SYSTEM
        static void AlarmSystemDisplayMenuScreen(Finch finchrobot)
        {

            string menuChoice;
            bool quitAlarmSystemMenu = false;

            string sensorsToMonitor = "temp";
            bool isMax = false; //Max or min
            int minMaxThresholdValue = 0;
            int timeToMonitor = 0;

            do
            {
                DisplayScreenHeader("Alarm System Menu");

                //
                // get user menu choice
                //
                Console.WriteLine("\ta) set sensor type");
                Console.WriteLine("\tb) set range type");
                Console.WriteLine("\tc) set maximum/minimum value");
                Console.WriteLine("\td) set time to monitor");
                Console.WriteLine("\te) start alarm");
                Console.WriteLine("\tf) test selected sensor live");
                Console.WriteLine("\tg) return to main menu");
                Console.Write("\t\tEnter Choice:");
                menuChoice = Console.ReadLine().ToLower();


                //
                // process user menu choice
                //
                switch (menuChoice)
                {
                    case "a":
                        sensorsToMonitor = AlarmDisplaySetSensor();
                        break;
                    case "b":
                        isMax = AlarmDisplayRangeType();
                        break;

                    case "c":
                        minMaxThresholdValue = AlarmDisplaySetMinMaxThreshholdValue(isMax);
                        break;

                    case "d":
                        timeToMonitor = AlarmSystemMonitorTime();
                        break;

                    case "e":
                        AlarmDisplaySetAlarm(finchrobot, sensorsToMonitor, isMax, minMaxThresholdValue, timeToMonitor);
                        break;

                    case "f":
                        AlarmTestSensor(finchrobot, sensorsToMonitor);
                        break;
                    
                    case "g":
                        quitAlarmSystemMenu = true;
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }

            } while (!quitAlarmSystemMenu);
        }

        static string AlarmDisplaySetSensor()
        {
            string menuChoice;
            string output = "";
            bool goodinput = false;
            do
            {
                DisplayScreenHeader("Data Recorder");
                Console.WriteLine("What kind of data do you want to collect?");
                Console.WriteLine("\ta) Temperature sensor");
                Console.WriteLine("\tb) Light sensor, left side");
                Console.WriteLine("\tb) Light sensor, right side");
                Console.WriteLine("\tb) Light sensor, avg of both sides");

                menuChoice = Console.ReadLine().ToLower();

                switch (menuChoice)
                {
                    case "a":
                        output = "temp";
                        goodinput = true;
                        break;
                    case "b":
                        output = "left";
                        goodinput = true;
                        break;
                    case "c":
                        output = "right";
                        goodinput = true;
                        break;
                    case "d":
                        output = "both";
                        goodinput = true;
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("\tPlease enter a letter for the menu choice.");
                        DisplayContinuePrompt();
                        break;
                }
            } while (!goodinput);

            return output;
        }

        static bool AlarmDisplayRangeType()
        {
            bool output = false;
            bool goodinput = false;
            string menuChoice;
            do
            {
                DisplayScreenHeader("Alarm System");
                Console.WriteLine("For alarm system monitor do you want to monitor if you exceed a maxium or fall below a minium");

                Console.WriteLine("\ta) min ");
                Console.WriteLine("\tb) max ");

                menuChoice = Console.ReadLine().ToLower();

                if (menuChoice == "a")
                {
                    output = false;
                    goodinput = true;
                }
                else if (menuChoice == "b")
                {
                    output = true;
                    goodinput = true;
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("\tPlease enter a letter for the menu choice.");
                    DisplayContinuePrompt();
                }

            } while (!goodinput);
            return output;
        }

        static int AlarmDisplaySetMinMaxThreshholdValue(bool isMax)
        {
            string minmax = isMax ? "maximum" : "minimum";
            bool goodinput = false;
            int output = 0;
            do
            {
                DisplayScreenHeader("Alarm System");
                Console.WriteLine("Please enter a number for what your {0} ", minmax);

                goodinput = int.TryParse(Console.ReadLine(), out output);
                if (!goodinput) Console.WriteLine("That's not a number try again");
            } while (!goodinput);

            Console.WriteLine("Your {0} is set to {1}", minmax, output);
            DisplayContinuePrompt();
            return output;
        }

        static int AlarmSystemMonitorTime()
        {
            bool goodinput = false;
            int output = 0;
            do
            {
                DisplayScreenHeader("Alarm System");
                Console.WriteLine("Please enter a number for what your timelimit to be ");

                goodinput = int.TryParse(Console.ReadLine(), out output);
                if (!goodinput) Console.WriteLine("That's not a number try again");
            } while (!goodinput);

            Console.WriteLine("Your timelimit is set to {0}", output);
            DisplayContinuePrompt();
            return output;
        }

        static void AlarmDisplaySetAlarm(Finch finchrobot, string sensorsToMonitor, bool isMax, int treshhold, int timeToMonitor)
        {
            string minmax = isMax ? "maximum" : "minimum";
            DisplayScreenHeader("Alarm System");
            Console.WriteLine("Alarm System parameters:\nsensors to monitor {0}\n your {1} is set to {2}\n The system will be alarmed for {3} seconds",sensorsToMonitor, minmax, treshhold, timeToMonitor);
            Console.WriteLine("The system is ready to start");
            DisplayContinuePrompt();

            for (int i = 0; i < timeToMonitor; i++)
            {
                switch (sensorsToMonitor)
                {
                    case "temp":
                        if (isMax && (finchrobot.getTemperature() > treshhold)) //Max & temp is greater than treshold
                        {
                            AlarmTrigger();
                            return;
                        } 
                        else if(finchrobot.getTemperature() < treshhold) //Min & temp is less than treshold
                        {
                            AlarmTrigger();
                            return;
                        }

                        fixdisplay(finchrobot.getTemperature().ToString());
                        break;

                    case "left":
                        if (isMax && (finchrobot.getLeftLightSensor() > treshhold)) //Max & left light is greater than treshold
                        {
                            AlarmTrigger();
                            return;
                        }
                        else if (finchrobot.getLeftLightSensor() < treshhold) //Min & left light is less than treshold
                        {
                            AlarmTrigger();
                            return;
                        }
                        fixdisplay(finchrobot.getLeftLightSensor().ToString());
                        break;

                    case "right":
                        if (isMax && (finchrobot.getRightLightSensor() > treshhold)) //Max & left right is greater than treshold
                        {
                            AlarmTrigger();
                            return;
                        }
                        else if (finchrobot.getRightLightSensor() < treshhold) //Min & left right is less than treshold
                        {
                            AlarmTrigger();
                            return;
                        }
                        fixdisplay(finchrobot.getRightLightSensor().ToString());
                        break;

                    case "both":
                        if (isMax && (AvgTempSensors(finchrobot) > treshhold)) //Max & both light is greater than treshold
                        {
                            AlarmTrigger();
                            return;
                        }
                        else if (AvgTempSensors(finchrobot) < treshhold) //Min & both light is less than treshold
                        {
                            AlarmTrigger();
                            return;
                        }
                        fixdisplay(AvgTempSensors(finchrobot).ToString());
                        break;

                    default:
                        Console.WriteLine("Something has gone wrong!");
                        DisplayContinuePrompt();
                        return;
                }
                
                finchrobot.wait(1000);
            }

        }

        static void AlarmTestSensor(Finch finch, string sensorsToMonitor)
        {
            bool quit = false;
            Console.WriteLine("Live Monitoring for {0}, press any key to stop", sensorsToMonitor);
            do
            {
                switch (sensorsToMonitor)
                {
                    case "temp":
                        Console.WriteLine(finch.getTemperature());
                        break;

                    case "left":
                        Console.WriteLine(finch.getLeftLightSensor());
                        break;

                    case "right":
                        Console.WriteLine(finch.getRightLightSensor());
                        break;

                    case "both":
                        Console.WriteLine(AvgTempSensors(finch));
                        break;

                    default:
                        Console.WriteLine("Something has gone wrong!");
                        DisplayContinuePrompt();
                        return;

                }
                quit = Console.KeyAvailable;
                finch.wait(1000);
            } while (!quit);
        }

        static void AlarmTrigger()
        {
            DisplayScreenHeader("Alarm System");
            Console.WriteLine("The Alarm system has been triggered!");
            DisplayContinuePrompt();
        }

        static int AvgTempSensors(Finch finchrobot)
        {
            return (finchrobot.getLeftLightSensor() + finchrobot.getRightLightSensor()) / 2;
        }

        static void fixdisplay(string data)
        {
            int currentleftPosition = Console.CursorLeft;
            int currenttopPosition = Console.CursorTop;
            Console.SetCursorPosition(Console.WindowWidth - 4, 0);
            Console.Write(data);
            Console.SetCursorPosition(currentleftPosition, currenttopPosition); // Reset where the cursor was.
        }
        #endregion

        #region USER PROGRAMMING
        static void UserProgrammingDisplayMenuScreen(Finch finchrobot)
        {
            DisplayScreenHeader("User Programming");
            Console.WriteLine("This is under development check back soon!");
            DisplayContinuePrompt();
        }
        #endregion

        #region FINCH ROBOT MANAGEMENT

        /// <summary>
        /// *****************************************************************
        /// *               Disconnect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        static void DisplayDisconnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            DisplayScreenHeader("Disconnect Finch Robot");

            Console.WriteLine("\tAbout to disconnect from the Finch robot.");
            DisplayContinuePrompt();

            finchRobot.disConnect();

            Console.WriteLine("\tThe Finch robot is now disconnect.");

            DisplayMenuPrompt("Main Menu");
        }

        /// <summary>
        /// *****************************************************************
        /// *                  Connect the Finch Robot                      *
        /// *****************************************************************
        /// </summary>
        /// <param name="finchRobot">finch robot object</param>
        /// <returns>notify if the robot is connected</returns>
        static bool DisplayConnectFinchRobot(Finch finchRobot)
        {
            Console.CursorVisible = false;

            bool robotConnected;

            DisplayScreenHeader("Connect Finch Robot");

            Console.WriteLine("\tAbout to connect to Finch robot. Please be sure the USB cable is connected to the robot and computer now.");
            DisplayContinuePrompt();

            robotConnected = finchRobot.connect();
            if (robotConnected == false) Console.WriteLine("ERROR CONNECTING ROBOT!");
            // TODO test connection and provide user feedback - text, lights, sounds

            DisplayMenuPrompt("Main Menu");

            //
            // reset finch robot
            //
            finchRobot.setLED(0, 0, 0);
            finchRobot.noteOff();

            return robotConnected;
        }

        #endregion

        #region USER INTERFACE

        /// <summary>
        /// *****************************************************************
        /// *                     Welcome Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayWelcomeScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tFinch Control");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// *****************************************************************
        /// *                     Closing Screen                            *
        /// *****************************************************************
        /// </summary>
        static void DisplayClosingScreen()
        {
            Console.CursorVisible = false;

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\tThank you for using Finch Control!");
            Console.WriteLine();

            DisplayContinuePrompt();
        }

        /// <summary>
        /// display continue prompt
        /// </summary>
        static void DisplayContinuePrompt()
        {
            Console.WriteLine();
            Console.WriteLine("\tPress any key to continue.");
            Console.ReadKey();
        }

        /// <summary>
        /// display menu prompt
        /// </summary>
        static void DisplayMenuPrompt(string menuName)
        {
            Console.WriteLine();
            Console.WriteLine($"\tPress any key to return to the {menuName} Menu.");
            Console.ReadKey();
        }

        /// <summary>
        /// display screen header
        /// </summary>
        static void DisplayScreenHeader(string headerText)
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("\t\t" + headerText);
            Console.WriteLine();
        }

        #endregion
    }
}
