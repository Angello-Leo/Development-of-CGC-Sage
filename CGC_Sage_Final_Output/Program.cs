using System;
using static System.Formats.Asn1.AsnWriter;
using System.Runtime.ConstrainedExecution;
using System.Linq.Expressions;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;


namespace CGC_sage_debug
{
    internal class Program
    {
        public static void Main()
        {
            TitleScreen(); // Title Screen Method
            Console.Clear();
            string[] users = { "[1] - User 1", "[2] - User 2", "[3] - User 3", "[4] - User 4", "[5] - User 5 " }; // Array for displaying users
            bool keepRunning = true;
            while (keepRunning)
            {
                HomeScreen(); // Homescreen Method
                try
                {
                    Console.ForegroundColor = ConsoleColor.Yellow; // Change Text to yellow
                    Console.Write("Please Enter number Here: ", Console.ForegroundColor);
                    Console.ResetColor(); // Revert Color back to normal
                    int input0 = Convert.ToInt32(Console.ReadLine()); // User input

                    switch (input0)
                    {
                        case 1:
                            keepRunning = false;
                            Console.Clear();
                            Login(users); // Method for Login
                            break;

                        case 2:
                            keepRunning = false;
                            Console.Clear();
                            CreateUser(users); // Method for Creating User
                            break;

                        case 3:
                            keepRunning = false;
                            Console.Clear();
                            DeleteUserinMain(users); // Method For Deleting User in the Homescreen
                            break;

                        case 4:
                            keepRunning = false;
                            ExitScreen(); // Exit Screen Method
                            Console.Clear();
                            break;

                        default:
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please select a valid number.", Console.ForegroundColor);
                            Console.ResetColor();
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Clear();
                    // Handle the case where the user input is not a valid number
                    Console.WriteLine("Invalid input! Please enter a valid number.", Console.ForegroundColor);
                }
            }
        }

        // Login method
        public static void Login(string[] users)
        {
            bool Login = false;
            while (Login == false)
            {

                DisplayandValidateUsers(users); //Method for Displaying and Validating users
                Console.ForegroundColor = ConsoleColor.Red; // Change text color to red
                Console.WriteLine("[0] - Exit", Console.ForegroundColor);
                Console.ResetColor(); // Revert text color to normal
                Console.Write("Please select an option: ");
                int input1 = Convert.ToInt32(Console.ReadLine()); // User Input
                if (input1 == 0)
                {
                    Login = true;
                    Main(); // Main Methodd
                    break;
                }
                try
                {
                    if (input1 <= 5)
                    {
                        Login = true;
                    }
                    else
                    {
                        Console.WriteLine("Please input a valid number");
                        Login = false;
                    }
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input error.. Please input a number!");
                    Console.ResetColor();
                }

                string username, password, storedUsername = string.Empty, storedPassword = string.Empty;
                string filePath = $"C:\\Users\\gello\\Desktop\\Final Project Database\\User Login\\User {input1}\\User {input1}.txt"; // File Directory in Main Computer

                while (Login == true)
                {
                    try
                    {
                        Console.Write("Enter a username: ");
                        username = Console.ReadLine();
                        Console.Write("Enter a password: ");
                        password = Console.ReadLine();

                        // Logic to validate if there is a user or none
                        if (!File.Exists(filePath) || new FileInfo(filePath).Length == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("User not found. Please create a user.");
                            CreateUser(users);
                            break;
                        }

                        using (StreamReader sr = new StreamReader(File.Open(filePath, FileMode.Open)))
                        {
                            storedUsername = sr.ReadLine();
                            storedPassword = sr.ReadLine();
                        }

                        if (username == storedUsername && password == storedPassword)
                        {
                            Console.Clear();
                            // Get the console dimensions
                            int consoleWidth = Console.WindowWidth;
                            int consoleHeight = Console.WindowHeight;

                            // The messages to display
                            Console.ForegroundColor = ConsoleColor.Green;
                            string welcomeMessage = $"Welcome back {username}!";
                            Console.ResetColor();
                            string pressStartMessage = "Press Any key to start program";

                            // Calculate the center position for the welcome message
                            int welcomeX = (consoleWidth - welcomeMessage.Length) / 2;
                            int welcomeY = consoleHeight / 2 - 1;  // Above the center for better alignment

                            // Calculate the position for the "Press Any key" message
                            int pressStartX = (consoleWidth - pressStartMessage.Length) / 2;
                            int pressStartY = consoleHeight / 2 + 1;  // Below the center

                            // Move the cursor to the center and display the welcome message
                            Console.SetCursorPosition(welcomeX, welcomeY);
                            Console.ForegroundColor = ConsoleColor.Green;  // Set the color to green
                            Console.WriteLine(welcomeMessage);
                            Console.ResetColor();

                            // Move the cursor to the bottom and display the "Press any key" message
                            Console.SetCursorPosition(pressStartX, pressStartY);
                            Console.WriteLine(pressStartMessage);

                            // Wait for a key press before closing
                            Console.ReadKey();
                            StartProgram();
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Username or Password is incorrect. Please try again.");
                            Console.ResetColor();
                        }
                    }
                    catch (Exception ex)
                    {
                        // Catch any other unexpected exceptions
                        Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                    }

                }
            }

        }
        // Method for displaying and validating user slots
        public static void DisplayandValidateUsers(string[] users)
        {
            string[] filePaths = new string[] // Array for Location of users
            {
        "C:\\Users\\gello\\Desktop\\Final Project Database\\User Login\\User 1\\User 1.txt",
        "C:\\Users\\gello\\Desktop\\Final Project Database\\User Login\\User 2\\User 2.txt",
        "C:\\Users\\gello\\Desktop\\Final Project Database\\User Login\\User 3\\User 3.txt",
        "C:\\Users\\gello\\Desktop\\Final Project Database\\User Login\\User 4\\User 4.txt",
        "C:\\Users\\gello\\Desktop\\Final Project Database\\User Login\\User 5\\User 5.txt"
            };
            Console.ForegroundColor = ConsoleColor.Cyan;
            PrintCentered("Please select a user slot:");
            PrintLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Yellow = user slot is taken", Console.ForegroundColor);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Green = user slot is availale", Console.ForegroundColor);
            Console.ResetColor();
            Console.WriteLine();
            bool anyUserExists = false;

            // Check if each file exists and display the user associated with that file
            for (int i = 0; i < filePaths.Length; i++)
            {
                if (File.Exists(filePaths[i]))
                {
                    // User exists, show the user in yellow
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"User {i + 1}: {users[i]}");
                    anyUserExists = true;
                }
                else
                {
                    // If the file doesn't exist, mark the user as available
                    // We assume that if a user file is missing, they are considered open
                    string userDisplay = $"{users[i]} ";

                    // Display deleted users in red
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"User {i + 1}: {userDisplay}");
                }
            }

            // Reset the color after displaying all users
            Console.ResetColor();
        }
        // Delete section 
        public static void DeleteUserinMain(string[] users)
        {
            bool DeleteUserinMain = true;
            while (DeleteUserinMain == true)
            {
                // Display the current users
                DisplayandValidateUsers(users);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[0] - Exit", Console.ForegroundColor);
                Console.ResetColor();

                // Ask the user to select which user to delete
                Console.Write("Select the number of the user that you want to delete: ");
                int DeleteUserNum = int.Parse(Console.ReadLine());
                if (DeleteUserNum == 0)
                {
                    DeleteUserinMain = false;
                    Main();
                    break;
                }

                Console.Write("Do you want to delete this user? (Y/N): ");
                string choice = Console.ReadLine().ToUpper();

                if (choice == "Y")
                {
                    string userFolder = $"C:\\Users\\gello\\Desktop\\Final Project Database\\User Login\\User {DeleteUserNum}";

                    // Check if the user folder exists
                    if (Directory.Exists(userFolder))
                    {
                        string[] files = Directory.GetFiles(userFolder);

                        // Delete all files in the user folder
                        foreach (var file in files)
                        {
                            DeleteFile(file); // Call your DeleteFile method here
                        }

                        // After deleting files, delete the folder itself
                        Directory.Delete(userFolder);


                        Console.Clear();
                        // Set the console text color to red
                        Console.ForegroundColor = ConsoleColor.Red;

                        // Get the console dimensions
                        int consoleWidth = Console.WindowWidth;
                        int consoleHeight = Console.WindowHeight;

                        // The message to display
                        string message = $"User {DeleteUserNum} has been deleted.";
                        string message2 = "Press any key to continue";

                        // Calculate the X position to center the message
                        int messageX = (consoleWidth - message.Length) / 2;
                        int messageY = consoleHeight / 2 - 1;  // Vertically center the message
                        int message2X = (consoleWidth - message.Length) / 2;
                        int message2Y = consoleHeight / 2 + 1;

                        // Inform the user that the user data has been deleted
                        Console.SetCursorPosition(messageX, messageY);
                        Console.WriteLine(message);
                        Console.ResetColor();
                        Console.SetCursorPosition(message2X, message2Y);
                        Console.WriteLine(message2);

                        // Pause to see result
                        Console.ReadKey();
                        Console.Clear();


                        Console.WriteLine("Do you want to create a new user? (Y/N)");
                        Console.Write("Input answer here: ");
                        char inputnewuser = char.Parse(Console.ReadLine().ToUpper());

                        switch (inputnewuser)
                        {
                            case 'Y':
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                // Get the console dimensions
                                int ConsoleWidth = Console.WindowWidth;
                                int ConsoleHeight = Console.WindowHeight;

                                // The messages to display
                                string CreatedMessage = "Heading to User Creation Page....";
                                string presskeymessage = "Press Any key to go to continue";

                                // Calculate the center position for the welcome message
                                int welcomeX = (ConsoleWidth - CreatedMessage.Length) / 2;
                                int welcomeY = ConsoleHeight / 2 - 1;  // Above the center for better alignment

                                // Calculate the position for the "Press Start" message
                                int pressStartX = (ConsoleWidth - presskeymessage.Length) / 2;
                                int pressStartY = ConsoleHeight / 2 + 1;  // Below the center

                                // Move the cursor to the center and display the welcome message
                                Console.SetCursorPosition(welcomeX, welcomeY);
                                Console.WriteLine(CreatedMessage, Console.ForegroundColor);

                                // Move the cursor to the bottom and display the "Press" message
                                Console.ResetColor();
                                Console.SetCursorPosition(pressStartX, pressStartY);
                                Console.WriteLine(presskeymessage);

                                // Wait for a key press before closing
                                Console.ReadKey();
                                Console.Clear();
                                CreateUser(users);
                                break;
                            case 'N':
                                Console.Clear();
                                Console.ForegroundColor = ConsoleColor.Green;
                                // Get the console dimensions
                                int Consolewidth = Console.WindowWidth;
                                int Consoleheight = Console.WindowHeight;

                                // The messages to display
                                string TitleScreenMessge = "Heading to Title Screen...";
                                string presskeymes = "Press Any key to continue";

                                // Calculate the center position for the welcome message
                                int titleX = (Consolewidth - TitleScreenMessge.Length) / 2;
                                int titleY = Consoleheight / 2 - 1;  // Above the center for better alignment

                                // Calculate the position for the "Press any key" message
                                int keyX = (Consolewidth - presskeymes.Length) / 2;
                                int keyY = Consoleheight / 2 + 1;  // Below the center

                                // Move the cursor to the center and display the welcome message
                                Console.SetCursorPosition(titleX, titleY);
                                Console.WriteLine(TitleScreenMessge, Console.ForegroundColor);

                                // Move the cursor to the bottom and display the "Press" message
                                Console.ResetColor();
                                Console.SetCursorPosition(keyX, keyY);
                                Console.WriteLine(presskeymes);

                                // Wait for a key press before closing
                                Console.ReadKey();
                                Console.Clear();
                                Main();
                                break;
                        }


                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine($"User {DeleteUserNum} folder not found.");
                        Login(users);
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("User deletion canceled.");
                    Login(users);
                }

                // Re-display the users after deletion to reflect the changes
                DisplayandValidateUsers(users);
            }
        }

        // User creation logic
        public static void CreateUser(string[] users)
        {
            bool CreateUser = true;
            while (CreateUser == true)
            {
                DisplayandValidateUsers(users);  // Show the list of users.
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[0] - Exit", Console.ForegroundColor);
                Console.ResetColor();
                Console.Write("Input user number here: ");
                int input2 = Convert.ToInt32(Console.ReadLine());
                if (input2 == 0)
                {
                    Main();
                    CreateUser = false;
                    break;
                }

                string username, password;
                string userFolderPath = $"C:\\Users\\gello\\Desktop\\Final Project Database\\User Login\\User {input2}";
                string filePath = $"{userFolderPath}\\User {input2}.txt";

                if (File.Exists(filePath))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("User already exists.", Console.ForegroundColor);
                    Console.ResetColor();
                    DeleteUser(input2, users);  // Delete the existing user
                }
                else
                {
                    Console.Write("Enter Username: ");
                    username = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    password = Console.ReadLine();

                    // Ensure the folder exists before creating the file
                    Directory.CreateDirectory(userFolderPath);

                    using (StreamWriter sw = new StreamWriter(File.Create(filePath)))
                    {
                        sw.WriteLine(username);
                        sw.WriteLine(password);
                    }

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Green;
                    // Get the console dimensions
                    int consoleWidth = Console.WindowWidth;
                    int consoleHeight = Console.WindowHeight;

                    // The messages to display
                    string welcomeMessage = "User has been created!";
                    string pressMessage = "Press Any key to go to login page...";

                    // Calculate the center position for the welcome message
                    int welcomeX = (consoleWidth - welcomeMessage.Length) / 2;
                    int welcomeY = consoleHeight / 2 - 1;  // Above the center for better alignment

                    // Calculate the position for the "Press Start" message
                    int pressStartX = (consoleWidth - pressMessage.Length) / 2;
                    int pressStartY = consoleHeight / 2 + 1;  // Below the center

                    // Move the cursor to the center and display the welcome message
                    Console.SetCursorPosition(welcomeX, welcomeY);
                    Console.WriteLine(welcomeMessage, Console.ForegroundColor);

                    // Move the cursor to the bottom and display the "Press" message
                    Console.ResetColor();
                    Console.SetCursorPosition(pressStartX, pressStartY);
                    Console.WriteLine(pressMessage);

                    // Wait for a key press before closing
                    Console.ReadKey();
                    Console.Clear();
                    // call Login or return to your main menu
                    Login(users);
                }
            }
        }

        // Deleting a user upon login
        public static void DeleteUser(int userNumber, string[] users)
        {
            Console.Write("Do you want to delete this user? (Y/N): ");
            string choice = Console.ReadLine().ToUpper();

            if (choice == "Y")
            {
                string userFolder = $"C:\\Users\\gello\\Desktop\\Final Project Database\\User Login\\User {userNumber}";
                string[] files = Directory.GetFiles(userFolder);

                foreach (var file in files)
                {
                    DeleteFile(file);
                }
                Console.Clear();

                // Set the console text color to red
                Console.ForegroundColor = ConsoleColor.Red;

                // Get the console dimensions
                int consoleWidth = Console.WindowWidth;
                int consoleHeight = Console.WindowHeight;

                // The message to display
                string message = $"User {userNumber} has been deleted.";
                string message2 = "Press any key to continue";

                // Calculate the X position to center the message
                int messageX = (consoleWidth - message.Length) / 2;
                int messageY = consoleHeight / 2;  // Vertically center the message
                int message2X = (consoleWidth - message.Length) / 2;
                int message2Y = consoleHeight / 2 + 1;

                // Move the cursor to the calculated position and display the message
                Console.SetCursorPosition(messageX, messageY);
                Console.WriteLine(message);
                Console.ResetColor();
                Console.SetCursorPosition(message2X, message2Y);
                Console.WriteLine(message2);

                // Pause to see result
                Console.ReadKey();
                Console.Clear();

                Console.Write("Do you want to create a new user? Y/N");
                char newuserchoice = char.Parse(Console.ReadLine().ToUpper());
                switch (newuserchoice)
                {
                    case 'Y':
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Heading to user creation page...", Console.ForegroundColor);
                        Console.ResetColor();
                        CreateUser(users);
                        break;
                    case 'N':
                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Heading to Title Sceen...", Console.ForegroundColor);
                        Console.ResetColor();
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        Main();
                        break;

                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("User deletion canceled.");
            }
        }




        // Delete file method
        public static void DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
                Console.WriteLine("User file has been deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting file: {ex.Message}");
            }
        }

        // Start the main program
        public static void StartProgram()
        {
            Console.Clear();
            bool Progamstart = true;
            int windowWidth = Console.WindowWidth;

            while (Progamstart == true)
            {
                // Print top horizontal line
                PrintLine();
                Console.ForegroundColor = ConsoleColor.Green;
                PrintCentered("CGC Sage at Your Service! ");

                // Print a divider line (horizontal) between options and choices
                Console.ResetColor();
                PrintLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                PrintCentered("Select the number on the action that you want to do");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("[1] Career Planning");
                Console.WriteLine("[2] Play a Game");
                Console.WriteLine("[3] Unit Converter");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[0] - Exit");
                Console.ResetColor();

                // Print bottom horizontal line
                PrintLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Input number here: ");
                Console.ResetColor();
                try
                {
                    int selection = int.Parse(Console.ReadLine());
                    if (selection > 3)
                    {
                        Console.WriteLine("Please input a number from the choices (1, 2, 3)");
                    }

                    switch (selection)
                    {
                        case 0:
                            ExitScreen(); // Exit Screen Method
                            Progamstart = false;
                            break;
                        case 1:
                            CareerPlanning(); // Career Planning Method
                            break;
                        case 2:
                            MemoryGame(); // Memory Game Method
                            break;
                        case 3:
                            UnitConverter(); // Unit Converter Method
                            break;

                    }
                }
                catch (FormatException)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Input error.. Please input a number!");
                    Console.ResetColor();

                }
            }
        }

        // Method for career planning
        public static void CareerPlanning()
        {
            Console.Clear();
            bool careerplanning = true;
            while (careerplanning == true)
            {
                CareerPlanInstructions();
                bool startcareer = true;
                int careerscore = 0;
                bool question1 = true;
                while (question1 == true)
                {
                    Console.Clear();
                    while (startcareer == true)
                    {
                        question1 = true;
                        while (question1 == true)
                        {
                            //question 1
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            PrintCentered("Question 1");
                            PrintLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("What are your hobbies?");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[1] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Reading different kinds of books");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[2] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Playing sports");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[3] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Talking with friends");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[4] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Drawing / sketching pictures");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine(" ");
                            Console.Write("Input the number of your answer: ");
                            char inputq1 = char.Parse(Console.ReadLine());
                            Console.ResetColor();

                            if (inputq1 == '1')
                            {
                                Console.Clear();
                                careerscore += 4;
                                question1 = false;
                            }
                            else if (inputq1 == '2')
                            {
                                Console.Clear();
                                careerscore += 3;
                                question1 = false;
                            }
                            else if (inputq1 == '3')
                            {
                                Console.Clear();
                                careerscore += 2;
                                question1 = false;
                            }
                            else if (inputq1 == '4')
                            {
                                Console.Clear();
                                careerscore += 1;
                                question1 = false;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Clear();
                                Console.WriteLine("Please input a valid number");
                                Console.ResetColor();
                                question1 = true;
                            }
                        }
                        //question 2
                        bool question2 = true;
                        while (question2 == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            PrintCentered("Question 2");
                            PrintLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("What is your personality?");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[1] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Curious");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[2] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Confident");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[3] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Friendly");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[4] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Creative");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Input the number of your answer: ");
                            char inputq2 = char.Parse(Console.ReadLine());
                            Console.ResetColor();

                            if (inputq2 == '1')
                            {
                                Console.Clear();
                                careerscore += 4;
                                question2 = false;
                            }
                            else if (inputq2 == '2')
                            {
                                Console.Clear();
                                careerscore += 3;
                                question2 = false;
                            }
                            else if (inputq2 == '3')
                            {
                                Console.Clear();
                                careerscore += 2;
                                question2 = false;
                            }
                            else if (inputq2 == '4')
                            {
                                Console.Clear();
                                careerscore += 1;
                                question2 = false;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Clear();
                                Console.WriteLine("Please input a valid number");
                                Console.ResetColor();
                                question2 = true;
                            }

                        }
                        //question 3
                        bool question3 = true;
                        while (question3 == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            PrintCentered("Question 3");
                            PrintLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("What do you usually do during your free time?");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[1] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Sleep");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[2] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Listening to Music");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[3] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Shopping");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[4] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Playing Games");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Input the number of your answer: ");
                            char inputq3 = char.Parse(Console.ReadLine());
                            Console.ResetColor();

                            if (inputq3 == '1')
                            {
                                Console.Clear();
                                careerscore += 4;
                                question3 = false;
                            }
                            else if (inputq3 == '2')
                            {
                                Console.Clear();
                                careerscore += 3;
                                question3 = false;
                            }
                            else if (inputq3 == '3')
                            {
                                Console.Clear();
                                careerscore += 2;
                                question3 = false;
                            }
                            else if (inputq3 == '4')
                            {
                                Console.Clear();
                                careerscore += 1;
                                question3 = false;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Clear();
                                Console.WriteLine("Please input a valid number");
                                Console.ResetColor();
                                question3 = true;
                            }

                        }
                        //question 4
                        bool question4 = true;
                        while (question4 == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            PrintCentered("Question 4");
                            PrintLine();

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("Which of the following do you find the most interesting?");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[1] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Creating new inventions");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[2] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Solving logical puzzles");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[3] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Running your own business");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[4] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Designing");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Input the number of your answer: ");
                            char inputq4 = char.Parse(Console.ReadLine());
                            Console.ResetColor();

                            if (inputq4 == '1')
                            {
                                Console.Clear();
                                careerscore += 4;
                                question4 = false;
                            }
                            else if (inputq4 == '2')
                            {
                                Console.Clear();
                                careerscore += 3;
                                question4 = false;
                            }
                            else if (inputq4 == '3')
                            {
                                Console.Clear();
                                careerscore += 2;
                                question4 = false;
                            }
                            else if (inputq4 == '4')
                            {
                                Console.Clear();
                                careerscore += 1;
                                question4 = false;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Clear();
                                Console.WriteLine("Please input a valid number");
                                Console.ResetColor();
                                question4 = true;
                            }
                        }

                        //question 5
                        bool question5 = true;
                        while (question5 == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            PrintCentered("Question 5");
                            PrintLine();

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("You saw a person asking for help, what will you do?");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[1] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Ask why they need help");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[2] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Analyze the situation before making a decision");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[3] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Rush to help");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[4] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Gather other people to help");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Input the number of your answer: ");
                            char inputq5 = char.Parse(Console.ReadLine());
                            Console.ResetColor();

                            if (inputq5 == '1')
                            {
                                Console.Clear();
                                careerscore += 4;
                                question5 = false;
                            }
                            else if (inputq5 == '2')
                            {
                                Console.Clear();
                                careerscore += 3;
                                question5 = false;
                            }
                            else if (inputq5 == '3')
                            {
                                Console.Clear();
                                careerscore += 1;
                                question5 = false;
                            }
                            else if (inputq5 == '4')
                            {
                                Console.Clear();
                                careerscore += 2;
                                question5 = false;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Clear();
                                Console.WriteLine("Please input a valid number");
                                Console.ResetColor();
                                question5 = true;
                            }
                        }

                        //question 6
                        bool question6 = true;
                        while (question6 == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            PrintCentered("Question 6");
                            PrintLine();
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("What is your favorite thing to do in school?");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[1] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Writing esssays'");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[2] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Breaks/Lunch");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[3] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Lab Experiments");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[4] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Recitation");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Input the number of your answer: ");
                            char inputq6 = char.Parse(Console.ReadLine());
                            Console.ResetColor();

                            if (inputq6 == '1')
                            {
                                Console.Clear();
                                careerscore += 1;
                                question6 = false;
                            }
                            else if (inputq6 == '2')
                            {
                                Console.Clear();
                                careerscore += 3;
                                question6 = false;
                            }
                            else if (inputq6 == '3')
                            {
                                Console.Clear();
                                careerscore += 4;
                                question6 = false;
                            }
                            else if (inputq6 == '4')
                            {
                                Console.Clear();
                                careerscore += 2;
                                question6 = false;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Clear();
                                Console.WriteLine("Please input a valid number");
                                Console.ResetColor();
                                question6 = true;
                            }
                        }

                        //question 7
                        bool question7 = true;
                        while (question7 == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            PrintCentered("Question 7");
                            PrintLine();

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("How do you perform in school?");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[1] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("I study alone and usually have little to no recitation in class");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[2] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("I always like to volunteer as a leader during group activities");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[3] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("I have consistent scores in activities, quizzes, and exams");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[4] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("I have a lot of connections inside and outside our classroom");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Input the number of your answer: ");
                            char inputq7 = char.Parse(Console.ReadLine());
                            Console.ResetColor();

                            if (inputq7 == '1')
                            {
                                Console.Clear();
                                careerscore += 4;
                                question7 = false;
                            }
                            else if (inputq7 == '2')
                            {
                                Console.Clear();
                                careerscore += 2;
                                question7 = false;
                            }
                            else if (inputq7 == '3')
                            {
                                Console.Clear();
                                careerscore += 3;
                                question7 = false;
                            }
                            else if (inputq7 == '4')
                            {
                                Console.Clear();
                                careerscore += 1;
                                question7 = false;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Clear();
                                Console.WriteLine("Please input a valid number");
                                Console.ResetColor();
                                question7 = true;
                            }

                        }
                        //question 8
                        bool question8 = true;
                        while (question8 == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            PrintCentered("Question 8");
                            PrintLine();

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("What is the first thing that you do when you wake up in the morning?");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[1] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Fix bed");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[2] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Open and check phone");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[3] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("look at the clock");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[4] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Try to sleep more");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Input the number of your answer: ");
                            char inputq8 = char.Parse(Console.ReadLine());
                            Console.ResetColor();

                            if (inputq8 == '1')
                            {
                                Console.Clear();
                                careerscore += 3;
                                question8 = false;
                            }
                            else if (inputq8 == '2')
                            {
                                Console.Clear();
                                careerscore += 2;
                                question8 = false;
                            }
                            else if (inputq8 == '3')
                            {
                                Console.Clear();
                                careerscore += 1;
                                question8 = false;
                            }
                            else if (inputq8 == '4')
                            {
                                Console.Clear();
                                careerscore += 4;
                                question8 = false;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Clear();
                                Console.WriteLine("Please input a valid number");
                                Console.ResetColor();
                                question8 = true;
                            }
                        }

                        bool question9 = true;
                        while (question9 == true)
                        {
                            //question 9
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            PrintCentered("Question 9");
                            PrintLine();

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("After school what do you usually do?");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[1] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("Nothing, I just go home");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[2] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("I tend to go to different locations");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[3] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("I go home and organize my tasks");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[4] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("I like to browse the internet as I go home");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Input the number of your answer: ");
                            char inputq9 = char.Parse(Console.ReadLine());
                            Console.ResetColor();

                            if (inputq9 == '1')
                            {
                                Console.Clear();
                                careerscore += 4;
                                question9 = false;
                            }
                            else if (inputq9 == '2')
                            {
                                Console.Clear();
                                careerscore += 1;
                                question9 = false;
                            }
                            else if (inputq9 == '3')
                            {
                                Console.Clear();
                                careerscore += 2;
                                question9 = false;
                            }
                            else if (inputq9 == '4')
                            {
                                Console.Clear();
                                careerscore += 3;
                                question9 = false;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Clear();
                                Console.WriteLine("Please input a valid number");
                                Console.ResetColor();
                                question9 = true;
                            }

                        }
                        bool question10 = true;
                        while (question10 == true)
                        {
                            //question 10
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            PrintCentered("Question 10");
                            PrintLine();

                            Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("You have an upcomming exam for the next month. When will you study for exams?");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[1] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("1 week before the exam");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[2] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("1 day before the exam");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[3] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("1-2 months before the exam");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("[4] ");
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.WriteLine("2 weeks before the exam");
                            Console.WriteLine(" ");

                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("Input the number of your answer: ");
                            char inputq10 = char.Parse(Console.ReadLine());
                            Console.ResetColor();

                            if (inputq10 == '1')
                            {
                                Console.Clear();
                                careerscore += 3;
                                question10 = false;
                            }
                            else if (inputq10 == '2')
                            {
                                Console.Clear();
                                careerscore += 4;
                                question10 = false;
                            }
                            else if (inputq10 == '3')
                            {
                                Console.Clear();
                                careerscore += 1;
                                question10 = false;
                            }
                            else if (inputq10 == '4')
                            {
                                careerscore += 2;
                                question10 = false;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Clear();
                                Console.WriteLine("Please input a valid number");
                                Console.ResetColor();
                                question10 = true;
                            }

                            bool displayscore = true;
                            while (displayscore == true)
                            {
                                Console.Clear();

                                if (careerscore <= 10 && careerscore < 11)
                                {
                                    careerplanning = false;
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    DisplayFreeLance();
                                    break;
                                }
                                else if (careerscore > 10 && careerscore <= 20)
                                {
                                    careerplanning = false;
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    DisplayEntrepreneur();
                                    break;
                                }
                                else if (careerscore > 20 && careerscore <= 30)
                                {
                                    careerplanning = false;
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    DisplaySkill();
                                    break;
                                }
                                else if (careerscore > 30)
                                {
                                    careerplanning = false;
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    DisplayKnowledge();
                                    break;
                                }
                            }
                        }
                    }
                }


            }


        }
        static void CareerPlanInstructions()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string message = "Welcome to the Career Planning Adventure! In this game, you'll be guided through a series of " +
                            "questions designed to help you discover more about your interests, skills, and preferences in the career world. " +
                            "By choosing the answer that suits you best, you will uncover a career path that's a perfect fit for your unique qualities! ";
            // Set the console window size (Optional: adjust based on your console window size)
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Split the message into lines (Optional: for wrapping text to avoid exceeding window width)
            string[] lines = WrapText(message, windowWidth - 4);  // Account for padding

            // Calculate the number of empty lines to vertically center the text
            int totalTextHeight = lines.Length;
            int paddingTop = (windowHeight - totalTextHeight) / 2;

            // Clear the console screen
            Console.Clear();

            // Print the top padding (empty lines)
            for (int i = 0; i < paddingTop; i++)
            {
                Console.WriteLine();
            }

            // Loop through each line and justify the text
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                // Justify all lines except the last one
                if (i < lines.Length - 1)
                {
                    line = JustifyText(line, windowWidth - 4); // Account for padding
                }

                // Calculate horizontal padding and print the line
                int paddingLeft = (windowWidth - line.Length) / 2;
                Console.WriteLine(new string(' ', paddingLeft) + line);
            }
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            MaximizeConsole();// maximize the console window
            PrintCentered("How to Play (Instructions) "); // Print in Center
            Console.ResetColor();
            Console.WriteLine(" ");
            string instruction1 = "Read Each Question Carefully: For each question, you'll be presented with a set of possible answers. " +
                                  "Take your time to think about which option best matches your personality, skills, or interests.";
            string instruction2 = "Select Your Answer: Choose the answer that resonates with you the most. Remember, there are no right " +
                                 "or wrong answers. The game is all about discovering what suits YOU.";
            string instruction3 = "Get Career Suggestions: After answering all the questions, you'll receive career suggestions that align with your responses. " +
                                 "These suggestions can guide you in your real-life career planning journey.";
            string instruction4 = "Explore More Options: Once you’ve received your suggestions, you can explore additional resources or re-take the quiz to refine your choices! ";

            Console.WriteLine("1. " + instruction1); // Print Instruction 1
            Console.WriteLine(" ");
            Console.WriteLine("2 " + instruction2); // Print Instruction 2
            Console.WriteLine(" ");
            Console.WriteLine("3. " + instruction3); // Print Instruction 3
            Console.WriteLine(" ");
            Console.WriteLine("4. " + instruction4); // Print Instruction 4
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press any key to start the career planning game ");
            Console.ResetColor();
            Console.ReadKey();
            ResizeConsole(500, 200);
        }
        //This method displays the message for free lance based career
        static void DisplayFreeLance()
        {
            // The message you want to display
            string message = "You are most likely fitted to a freelance-based career if you thrive on flexibility, " +
                             "enjoy managing your own time, and are motivated by the freedom to choose projects that align with your skills and passions. " +
                             "This career path offers independence and the opportunity to work with a variety of clients, providing both challenges and rewards " +
                             "as you build your own business and reputation.   (Press any Key to Display Jobs)";

            // Set the console window size (Optional: adjust based on your console window size)
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Split the message into lines (Optional: for wrapping text to avoid exceeding window width)
            string[] lines = WrapText(message, windowWidth - 4);  // Account for padding

            // Calculate the number of empty lines to vertically center the text
            int totalTextHeight = lines.Length;
            int paddingTop = (windowHeight - totalTextHeight) / 2;

            // Clear the console screen
            Console.Clear();

            // Print the top padding (empty lines)
            for (int i = 0; i < paddingTop; i++)
            {
                Console.WriteLine();
            }

            // Loop through each line and justify the text
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                // Justify all lines except the last one
                if (i < lines.Length - 1)
                {
                    line = JustifyText(line, windowWidth - 4); // Account for padding
                }

                // Calculate horizontal padding and print the line
                int paddingLeft = (windowWidth - line.Length) / 2;
                Console.WriteLine(new string(' ', paddingLeft) + line);
            }

            // Pause the console so the user can read the message
            Console.ReadKey();
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\tJob Exmples: Graphic Designer, Web Developer, Copywriter, Social Media Manager, Virtual Assistant");
            Console.ResetColor();
            Console.WriteLine("\t(Press any key to Continue)");
            Console.ReadKey();
            Console.Clear();
            CareerPlanTryAgain();

        }

        static void DisplayKnowledge()
        {
            // The message you want to display
            string message = "You are most likely fitted to a knowledge-based career if you enjoy problem-solving, critical thinking, " +
                             "and applying your expertise to complex challenges. This career path values continuous learning, research, " +
                             "and leveraging your skills to contribute to innovation and strategic decision-making in various industries. " +
                             "  (Press any Key to Display Jobs)";

            // Set the console window size (Optional: adjust based on your console window size)
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Split the message into lines (Optional: for wrapping text to avoid exceeding window width)
            string[] lines = WrapText(message, windowWidth - 4);  // Account for padding

            // Calculate the number of empty lines to vertically center the text
            int totalTextHeight = lines.Length;
            int paddingTop = (windowHeight - totalTextHeight) / 2;

            // Clear the console screen
            Console.Clear();

            // Print the top padding (empty lines)
            for (int i = 0; i < paddingTop; i++)
            {
                Console.WriteLine();
            }

            // Loop through each line and justify the text
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                // Justify all lines except the last one
                if (i < lines.Length - 1)
                {
                    line = JustifyText(line, windowWidth - 4); // Account for padding
                }

                // Calculate horizontal padding and print the line
                int paddingLeft = (windowWidth - line.Length) / 2;
                Console.WriteLine(new string(' ', paddingLeft) + line);
            }

            // Pause the console so the user can read the message
            Console.ReadKey();
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\tJob Exmples: Data Analyst, Research Scientist, Engineer, Lawyer, Teacher");
            Console.ResetColor();
            Console.WriteLine("\t(Press any key to Continue)");
            Console.ReadKey();
            Console.Clear();
            CareerPlanTryAgain();

        }

        static void DisplaySkill()
        {
            // The message you want to display
            string message = "You are most likely fitted to a skill-based career if you excel in hands-on tasks,  " +
                             "enjoy working with tools or technology, and take pride in mastering specific techniques. " +
                             "This career path values practical expertise and the ability to produce tangible results " +
                             "through specialized skills in fields like trades, crafts, and technical services.  (Press any Key to Display Jobs)";

            // Set the console window size (Optional: adjust based on your console window size)
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Split the message into lines (Optional: for wrapping text to avoid exceeding window width)
            string[] lines = WrapText(message, windowWidth - 4);  // Account for padding

            // Calculate the number of empty lines to vertically center the text
            int totalTextHeight = lines.Length;
            int paddingTop = (windowHeight - totalTextHeight) / 2;

            // Clear the console screen
            Console.Clear();

            // Print the top padding (empty lines)
            for (int i = 0; i < paddingTop; i++)
            {
                Console.WriteLine();
            }

            // Loop through each line and justify the text
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                // Justify all lines except the last one
                if (i < lines.Length - 1)
                {
                    line = JustifyText(line, windowWidth - 4); // Account for padding
                }

                // Calculate horizontal padding and print the line
                int paddingLeft = (windowWidth - line.Length) / 2;
                Console.WriteLine(new string(' ', paddingLeft) + line);
            }

            // Pause the console so the user can read the message
            Console.ReadKey();
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\tJob Exmples: Electrician, Automotive Technician, Chef, Mechanic, Hair Stylist");
            Console.ResetColor();
            Console.WriteLine("\t(Press any key to Continue)");
            Console.ReadKey();
            Console.Clear();
            CareerPlanTryAgain();

        }

        static void DisplayEntrepreneur()
        {
            // The message you want to display
            string message = "You are most likely fitted to an entrepreneur-based career if you are passionate about innovation, " +
                             "enjoy taking risks, and have a strong drive to build and grow your own business. This career path " +
                             "requires creativity, leadership, and the ability to adapt to challenges while seeking new opportunities " +
                             "and creating value in the marketplace.  (Press any Key to Display Jobs)";

            // Set the console window size (Optional: adjust based on your console window size)
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            // Split the message into lines (Optional: for wrapping text to avoid exceeding window width)
            string[] lines = WrapText(message, windowWidth - 4);  // Account for padding

            // Calculate the number of empty lines to vertically center the text
            int totalTextHeight = lines.Length;
            int paddingTop = (windowHeight - totalTextHeight) / 2;

            // Clear the console screen
            Console.Clear();

            // Print the top padding (empty lines)
            for (int i = 0; i < paddingTop; i++)
            {
                Console.WriteLine();
            }

            // Loop through each line and justify the text
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                // Justify all lines except the last one
                if (i < lines.Length - 1)
                {
                    line = JustifyText(line, windowWidth - 4); // Account for padding
                }

                // Calculate horizontal padding and print the line
                int paddingLeft = (windowWidth - line.Length) / 2;
                Console.WriteLine(new string(' ', paddingLeft) + line);
            }

            // Pause the console so the user can read the message
            Console.ReadKey();
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\tJob Exmples: Tech Founder, Small Business Owner, Product Manager, Franchise Owner, E-commerce Entrepreneur");
            Console.ResetColor();
            Console.WriteLine("\t(Press any key to Continue)");
            Console.ReadKey();
            Console.Clear();
            CareerPlanTryAgain();

        }

        static void CareerPlanTryAgain()
        {
            // Get the console dimensions
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;

            // The messages to display
            string TryAgainMessage = "Would you like to try again?";
            string AnswerMessage = "Press Enter for Yes, Esc for No";

            // Calculate the center position for the welcome message
            int welcomeX = (consoleWidth - TryAgainMessage.Length) / 2;
            int welcomeY = consoleHeight / 2 - 1;  // Above the center for better alignment

            // Calculate the position for the "Press Start" message
            int pressStartX = (consoleWidth - AnswerMessage.Length) / 2;
            int pressStartY = consoleHeight / 2 + 1;  // Below the center

            // Move the cursor to the center and display the welcome message
            Console.SetCursorPosition(welcomeX, welcomeY);
            Console.WriteLine(TryAgainMessage);

            // Move the cursor to the bottom and display the "Press Start" message
            Console.SetCursorPosition(pressStartX, pressStartY);
            Console.WriteLine(AnswerMessage);

            bool validKey = false;
            while (!validKey)
            {
                // Wait for the user to press a key
                ConsoleKeyInfo keyInfo = Console.ReadKey(true); // true to not show the key in the console

                // Check if the key pressed is Enter or Escape
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    validKey = true;
                    CareerPlanning(); // Restart the Career Planning process
                    Console.Clear();
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    StartProgram(); // Start the program from the beginning
                    Console.Clear(); // Clear the console
                    break;
                }
                else
                {
                    // Invalid key, prompt again
                    Console.Clear();
                    PrintCentered("Invalid key. Press Enter for Yes, Esc for No.");
                    validKey = false;
                }

            }

            // Wait for a key press before closing
            Console.ReadKey();
        }

        static void TitleScreen()
        {
            Console.Clear();
            // Get the console dimensions
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;

            // The messages to display
            string welcomeMessage = "Welcome to CGC Sage";
            string pressStartMessage = "Press Any key to start program";

            // Calculate the center position for the welcome message
            int welcomeX = (consoleWidth - welcomeMessage.Length) / 2;
            int welcomeY = consoleHeight / 2 - 1;  // Above the center for better alignment

            // Calculate the position for the "Press Start" message
            int pressStartX = (consoleWidth - pressStartMessage.Length) / 2;
            int pressStartY = consoleHeight / 2 + 1;  // Below the center

            // Move the cursor to the center and display the welcome message
            Console.SetCursorPosition(welcomeX, welcomeY);
            Console.WriteLine(welcomeMessage);

            // Move the cursor to the bottom and display the "Press Start" message
            Console.SetCursorPosition(pressStartX, pressStartY);
            Console.WriteLine(pressStartMessage);

            // Wait for a key press before closing
            Console.ReadKey();
        }
        static void HomeScreen()
        {
            // Set the foreground color to gray for a nice neutral tone
            Console.ForegroundColor = ConsoleColor.Green;

            // Print the title in centered format
            PrintCentered("CGCSage at your service!");

            // Print a divider line
            PrintLine();
            PrintLine2();
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Cyan;

            // Print the options
            PrintCentered("Select an option below:");
            Console.WriteLine();
            Console.WriteLine("[1] Login to Existing user");
            Console.WriteLine("[2] Create a new user");
            Console.WriteLine("[3] Delete a user");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("[4] Exit");

            // Reset the color settings
            Console.ResetColor();
        }

        // Method to print centered text
        static void PrintCentered(string message)
        {
            int windowWidth = Console.WindowWidth;
            int messageLength = message.Length;
            int padding = (windowWidth - messageLength) / 2;
            Console.WriteLine(new string(' ', padding) + message);
        }

        // Method to print a divider line
        static void PrintLine()
        {
            int windowWidth = Console.WindowWidth;
            Console.WriteLine(new string('-', windowWidth));
        }
        static void PrintLine2()
        {
            int windowLength = Console.WindowLeft;
            Console.WriteLine(new string('|', windowLength));
        }

        static void ExitScreen()
        {
            Console.Clear();
            // Get the console dimensions
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;

            // The messages to display
            string welcomeMessage = "Thank you for using CGC Sage";
            string pressStartMessage = "Press Any key to exit the program";

            // Calculate the center position for the welcome message
            int welcomeX = (consoleWidth - welcomeMessage.Length) / 2;
            int welcomeY = consoleHeight / 2 - 1;  // Above the center for better alignment

            // Calculate the position for the "Press Start" message
            int pressStartX = (consoleWidth - pressStartMessage.Length) / 2;
            int pressStartY = consoleHeight / 2 + 1;  // Below the center

            // Move the cursor to the center and display the welcome message
            Console.SetCursorPosition(welcomeX, welcomeY);
            Console.WriteLine(welcomeMessage);

            // Move the cursor to the bottom and display the "Press Start" message
            Console.SetCursorPosition(pressStartX, pressStartY);
            Console.WriteLine(pressStartMessage);

            // Wait for a key press before closing
            Console.ReadKey();
            Console.Clear();
            Environment.Exit(0); // Terminate the program

        }

        public static void UnitConverter()
        {
            // loops for the whole program
            bool programConverter = true;
            do
            {
                Console.Clear(); // clears the console
                                 // Starter Selection
                Console.ForegroundColor = ConsoleColor.Green;
                PrintCentered("Welcome to Unit Converter");
                PrintLine();
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[1]. Measurement Unit");
                Console.WriteLine("[2]. Weight");
                Console.WriteLine("[3]. Volume");
                Console.WriteLine("[4]. Temperature");
                Console.WriteLine("[5]. Exit");
                Console.Write("What would you like to convert? (Input number of your selection): ");
                string unitSelection = Convert.ToString(Console.ReadLine());

                switch (unitSelection)
                {
                    case "1": // Measurement Unit
                        bool programMeasurement = true;
                        while (programMeasurement == true)
                        {
                            string mesIn = ""; // measurement abbreviation to be convert
                            string mesFin = ""; // measurement abbreviation to be conveted into
                            float mesValue = 0; // measurement value
                            Console.Clear();

                            PrintCentered("Converting Measurement Units");
                            PrintLine();
                            // Selections
                            string[] Measurement = { "mm", "cm", "dm", "m", "dam", "hm", "km" }; // arrays from smallest to biggest value
                            Console.WriteLine("----------------------------");
                            Console.WriteLine("|km, hm, dam, m, dm, cm, mm|");
                            Console.WriteLine("----------------------------");
                            bool measurementChecker = true;
                            while (measurementChecker == true) // loops for proper input
                            {
                                bool checker = true;
                                while (checker == true) // loops for proper input abbreviation
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Convert: ");
                                    mesIn = Console.ReadLine().ToLower();
                                    checker = Checker(Measurement, mesIn); // checks the inputed by the user
                                }
                                checker = true; // allows looping for the inputed of user of the measure abbreaviation 
                                while (checker == true) // loops for proper input abbreviation
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("To: ");
                                    mesFin = Console.ReadLine().ToLower();
                                    checker = Checker(Measurement, mesFin); // checks the inputed by the user
                                }
                                checker = true; // allows looping for the inputed of user of the measure abbreaviation 
                                while (checker == true) // loops for proper input value
                                {
                                    // Checks the value
                                    try
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Converting: ");
                                        mesValue = float.Parse(Console.ReadLine());
                                        checker = false;
                                    }
                                    catch (FormatException) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Input! Please select the proper input!"); Console.ReadKey(true); Console.Write("\n"); }
                                }
                                int finMes = Locating(Measurement, mesIn, mesFin); // locates the distance from mesIn and mesFin
                                Conversion(finMes, mesValue, mesFin); // Converts the number values to its corresponding equivalent

                                // Decision to repeat or quit the program
                                bool mesdec = true;
                                while (mesdec == true)
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("\nWould you like to continue (y/n)? ");
                                    string mesRes = Convert.ToString(Console.ReadLine().ToLower());
                                    switch (mesRes)
                                    {
                                        case "y": mesdec = false; measurementChecker = false; break; // repeats the process
                                        case "n": mesdec = false; measurementChecker = false; programMeasurement = false; break; // quits the measurement program
                                        default: Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Input! Please select the proper input!"); Console.ReadKey(true); break; // repeats the message
                                    }
                                }
                            }
                        }
                        break;

                    case "2": // Weight Program
                        bool programWeight = true;
                        while (programWeight == true)
                        {
                            string weiIn = ""; // weight abbreviation to be convert
                            string weiFin = ""; // weight abbreviation to be converted into
                            float weiValue = 0; // value of measurement
                            Console.Clear();

                            // Selection
                            PrintCentered("Converting Weight");
                            PrintLine();
                            string[] Weight = { "mg", "cg", "dg", "g", "dag", "hg", "kg" }; // array from smallest to biggest difference value
                            Console.WriteLine("----------------------------");
                            Console.WriteLine("|kg, hg, dag, g, dg, cg, mg|");
                            Console.WriteLine("----------------------------");
                            bool measurementChecker = true;
                            while (measurementChecker == true) // loop for proper input
                            {
                                bool checker = true;
                                while (checker == true) // loop for proper input abbreviation
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Convert: ");
                                    weiIn = Console.ReadLine().ToLower();
                                    checker = Checker(Weight, weiIn); // checks the inputed by the user
                                }
                                checker = true; // allows for looping
                                while (checker == true) // loop for proper input abbreviation
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("To: ");
                                    weiFin = Console.ReadLine().ToLower();
                                    checker = Checker(Weight, weiFin); // checks the inputed by the user
                                }
                                checker = true; // allows for looping to check value
                                while (checker == true) // looping for checking value
                                {
                                    // Checks the value inputed
                                    try
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Converting: ");
                                        weiValue = float.Parse(Console.ReadLine());
                                        checker = false;
                                    }
                                    catch (FormatException) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Input! Please select the proper input!"); Console.ReadKey(true); Console.Write("\n"); }
                                }
                                int finWei = Locating(Weight, weiIn, weiFin); // locate the difference between weiIn and weiFin
                                Conversion(finWei, weiValue, weiFin); // Converts the value to its corresponding equivalent value

                                // Decision
                                bool weidec = true;
                                while (weidec == true)
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("\nWould you like to continue (y/n)? ");
                                    string weiRes = Convert.ToString(Console.ReadLine().ToLower());
                                    switch (weiRes)
                                    {
                                        case "y": weidec = false; measurementChecker = false; break; // repeats the weight program
                                        case "n": weidec = false; measurementChecker = false; programWeight = false; break; // exits the weight program
                                        default: Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Input! Please select the proper input!"); Console.ReadKey(true); break; // repeats the message.
                                    }
                                }
                            }
                        }
                        break;
                    case "3": // Volume program
                        bool programVolume = true;
                        while (programVolume == true)
                        {
                            string volIn = ""; // volume abbreviation to be convert
                            string volFin = ""; // volume abbreviation to be converted into
                            float volValue = 0; // volume value
                            Console.Clear();

                            // Selection
                            PrintCentered("Converting Volume");
                            PrintLine();
                            string[] Volume = { "ml", "cl", "dl", "l", "dal", "hl", "kl" }; // arrays from smallest to biggest value
                            Console.WriteLine("----------------------------");
                            Console.WriteLine("|kl, hl, dal, l, dl, cl, ml|");
                            Console.WriteLine("----------------------------");
                            bool measurementChecker = true;
                            while (measurementChecker == true) // loop for proper input
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                bool checker = true;
                                while (checker == true) // loop for proper input abbreviation
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("Convert: ");
                                    volIn = Console.ReadLine().ToLower();
                                    checker = Checker(Volume, volIn); // checks the inputed by the user
                                }
                                checker = true; // allows for looping input abbreviation
                                while (checker == true) // loop for proper input abbreviation
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("To: ");
                                    volFin = Console.ReadLine().ToLower();
                                    checker = Checker(Volume, volFin); // checks the inputed by the user
                                }
                                checker = true; // allows for looping for input numbers
                                while (checker == true) // loop for proper input number
                                {
                                    // Checks input number
                                    try
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.Write("Converting: ");
                                        volValue = float.Parse(Console.ReadLine());
                                        checker = false;
                                    }
                                    catch (FormatException) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Input! Please select the proper input!"); Console.ReadKey(true); Console.Write("\n"); }
                                }
                                int finVol = Locating(Volume, volIn, volFin); // locate the difference between volIN and volFin
                                Conversion(finVol, volValue, volFin); // Converts the value to its corresponding equivalent value

                                // Decision
                                bool voldec = true;
                                while (voldec == true)
                                {
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.Write("\nWould you like to continue (y/n)? ");
                                    string volRes = Convert.ToString(Console.ReadLine().ToLower());
                                    switch (volRes)
                                    {
                                        case "y": voldec = false; measurementChecker = false; break; // repeat the volume program
                                        case "n": voldec = false; measurementChecker = false; programVolume = false; break; // exits the volume program
                                        default: Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Input! Please select the proper input!"); Console.ReadKey(true); break; // repeats the message
                                    }
                                }
                            }
                        }
                        break;
                    case "4": // Temperature Program

                        bool programTemperature = true;
                        while (programTemperature == true) // loops the temperature selection
                        {
                            Console.Clear();
                            Console.ForegroundColor = ConsoleColor.White;
                            PrintCentered("Converting Temperature");
                            PrintLine();
                            Console.WriteLine("[1]. Celcius to Farenheight");
                            Console.WriteLine("[2]. Farenheight to Celcius");
                            Console.WriteLine("[3]. Exit");
                            Console.Write("Input number here: ");
                            string tempSelection = Convert.ToString(Console.ReadLine());
                            bool tempSel = true;
                            while (tempSel == true) // loop for repeating the process of the corresponding program
                            {
                                switch (tempSelection)
                                {
                                    case "1": // Celcius to Farenheight
                                        try
                                        {
                                            Console.Clear();
                                            PrintCentered("Converting Celcius to Farenheight");
                                            PrintLine();
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write("Celcius: ");
                                            float celcius = float.Parse(Console.ReadLine());
                                            CelToFar(celcius); // Calculates

                                            // Decision
                                            bool tempCel = true;
                                            while (tempCel == true)
                                            {
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.Write("\nWould you like to continue (y/n)? ");
                                                char tempRes = Convert.ToChar(Console.ReadLine().ToLower());
                                                if (tempRes == 'y')
                                                {
                                                    tempCel = false; break; // repeats the process
                                                }
                                                else if (tempRes == 'n')
                                                {
                                                    tempCel = false; tempSel = false; break; // exit
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Input! Please select the proper input!"); Console.ReadKey(true); // repeat the message
                                                }
                                            }
                                        }
                                        catch (FormatException) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Input! Please select the proper input!"); Console.ReadKey(true); break; }
                                        break;
                                    case "2": // Farenheight to Celcius
                                        try
                                        {
                                            Console.Clear();
                                            PrintCentered("Converting Farenheight to Celcius");
                                            PrintLine();
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.Write("Farenheight: ");
                                            float farenheight = float.Parse(Console.ReadLine());
                                            FarToCel(farenheight); // Calculates

                                            // Decision
                                            bool tempFar = true;
                                            while (tempFar == true)
                                            {
                                                Console.ForegroundColor = ConsoleColor.White;
                                                Console.WriteLine("\nWould you like to continue (y/n)? ");
                                                char tempRes = Convert.ToChar(Console.ReadLine().ToLower());
                                                if (tempRes == 'y')
                                                {
                                                    tempFar = false; break; // repeats the process
                                                }
                                                else if (tempRes == 'n')
                                                {
                                                    tempFar = false; tempSel = false; break; // exit
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Input! Please select the proper input!"); Console.ReadKey(true); // repeat the message
                                                }
                                            }
                                        }
                                        catch (FormatException) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Input! Please select the proper input!"); Console.ReadKey(true); break; } // repeats the message
                                        break;
                                    case "3": programTemperature = false; tempSel = false; break; // exits the temperature selection converter
                                    default: Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("\nInvalid Input! Please select the proper input!"); Console.ReadKey(true); tempSel = false; break; // repeats the message
                                }
                            }
                        }
                        break;
                    case "5": // exits the unit converter
                        Console.WriteLine("\nExiting......"); Console.WriteLine("Thank you for using our Bot!!"); programConverter = false; Console.Clear(); break;
                    default: Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Input! Please select the proper input!"); Console.ReadKey(true); break; // repeat the message
                }
            } while (programConverter == true);
        }

        //Essentials for the game
        const int width = 100; // width of the Console
        const int height = 30; // height of the Console
        static double score = 0; // score of the player
        static double comboScore = 1; // comboScore used to multiply to the score
        static bool gameOver = false; // controls for the running the game.
        static int attempts = 1; // refers to the limit of tries of the user after a wrong input
        static bool control = true; // controls for the loop of control
        static bool challenge = false; // for enabling challenge mode.

        public static void MemoryGame()
        {
            Console.Clear();
            //Start of the Game.
            DisplayMemoryGame(); //Displays starting of the game.

            //loop for running the game.
            while (gameOver != true)
            {
                PlayGame(); // runs the game
            }

        }

        // This method is used to wrap text into multiple lines to fit within the console to make it more readable
        static string[] WrapText(string text, int maxWidth)
        {
            var words = text.Split(' ');
            var lines = new System.Collections.Generic.List<string>();
            string currentLine = "";

            foreach (var word in words)
            {
                if ((currentLine + word).Length > maxWidth)
                {
                    lines.Add(currentLine);
                    currentLine = word;
                }
                else
                {
                    if (currentLine.Length > 0)
                        currentLine += " ";
                    currentLine += word;
                }
            }

            if (currentLine.Length > 0)
                lines.Add(currentLine);


            return lines.ToArray();
        }

        static string JustifyText(string line, int maxWidth)
        {
            var words = line.Split(' ');
            if (words.Length == 1) return line; // No need to justify single-word lines

            // Calculate total spaces needed to fill the line
            int spacesToDistribute = maxWidth - line.Length + (words.Length - 1);

            // Distribute spaces between words
            int spaceBetweenWords = spacesToDistribute / (words.Length - 1);
            int extraSpaces = spacesToDistribute % (words.Length - 1);

            string justifiedLine = words[0];

            for (int i = 1; i < words.Length; i++)
            {
                // Add the normal space between words
                justifiedLine += new string(' ', spaceBetweenWords);

                // Distribute extra spaces one by one
                if (extraSpaces > 0)
                {
                    justifiedLine += " ";
                    extraSpaces--;
                }

                // Add the next word
                justifiedLine += words[i];
            }

            return justifiedLine;
        }

        // For Unit Converter
        // Checks abbreviation
        static bool Checker(string[] Value, string reqValue)
        {
            int a = 0; // starter indication in the array
            bool check = true;
            string val = Value[0]; // initial abbreviation to be matched with reqValue
            try
            {
                for (int i = 0; i < Value.Length; i++) // checks the abbreviation to be matched with reqValue
                {
                    val = Value[a];
                    if (val == reqValue)
                    {
                        check = false; break; // if matched returns check = false to determine that it matched
                    }
                    a++;
                }
            }
            catch (IndexOutOfRangeException) { Console.ForegroundColor = ConsoleColor.Red; Console.WriteLine("Invalid Input! Please select the proper input!\n"); Console.ReadKey(true); return check; } // returns check = true if it is not matched to any abbreviation
            return check;
        }
        // Locates the difference between the two abbreviation
        static int Locating(string[] Value, string inValue, string reqValue)
        {
            int a = 0; // starter indication in the array
            int c = 0; // Checks the distance between the two abbreviation
            string val = Value[0]; // initial abbreviation to be matched with reqValue
            for (int v = 1; v < Value.Length; v++) // checks the abbreviation to be matched with reqValue
            {
                val = Value[a];
                if (val == inValue)
                {
                    break;
                }
                a++;
                c++;
            }
            a = 0; // restart the indication of the array
            for (int v = 1; v < Value.Length; v++) // checks the abbreviation to be matched with reqValue
            {
                val = Value[a];
                if (val == reqValue)
                {
                    break;
                }
                a++;
                c--;
            }
            return c; // returns the diffence of checking the two abbreviation
        }
        // Converts the value
        static void Conversion(int x, double unit, string set)
        {
            // x is the difference of the two abbreviation in the array
            double Base = 1; // stater value equivalent
            if (x > 0) // if x is positive
            {
                for (int v = 0; v < x; v++)
                {
                    Base *= 10; // multiply the value until it reaches v is less than x
                }
                unit *= Base;
                Console.WriteLine(Math.Round(unit, 2) + set); // multiplies and display the final value
            }
            else if (x < 0) // if x is negative
            {
                for (int v = 0; v > x; v--)
                {
                    Base /= 10; // divides the value until its reaches v is greater than x
                }
                unit *= Base;

                // divides and display the final value
                if (x >= -2)
                {
                    Console.WriteLine(Math.Round(unit, 2) + set);
                }
                else if (x == -3)
                {
                    Console.WriteLine(Math.Round(unit, 3) + set);
                }
                else if (x == -4)
                {
                    Console.WriteLine(Math.Round(unit, 4) + set);
                }
                else if (x == -5)
                {
                    Console.WriteLine(Math.Round(unit, 5) + set);
                }
                else if (x == -6)
                {
                    Console.WriteLine(Math.Round(unit, 6) + set);
                }
            }
        }
        // Calculates Celcius to Farenheight
        static void CelToFar(float cel)
        {
            double celtoFar = (cel * 1.8) + 32;
            Console.WriteLine("Farengheight: " + Math.Round(celtoFar, 2) + "°F");
        }

        // Calculates Farenheight to Celcius
        static void FarToCel(float far)
        {
            double fartoCel = (far - 32) / 1.8;
            Console.WriteLine("Celcius: " + Math.Round(fartoCel, 2) + "°C");
        }

        // Methods for memory game
        // Starting Display
        static void DisplayMemoryGame()
        {
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + -3);
            Console.Write("Memory Game");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + -2);
            Console.Write("Developed by CGC Sage");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + -1);
            Console.Write("Press any key to play....");
            Console.ReadKey(true);
        }
        // Get Random Numbers
        static string GetRandomNumbers()
        {
            Random rand = new Random();
            string num = "";
            switch (rand.Next(10))
            {
                case 0: num = "0"; break;
                case 1: num = "1"; break;
                case 2: num = "2"; break;
                case 3: num = "3"; break;
                case 4: num = "4"; break;
                case 5: num = "5"; break;
                case 6: num = "6"; break;
                case 7: num = "7"; break;
                case 8: num = "8"; break;
                case 9: num = "9"; break;
            }
            return num;
        }
        // Get Random Letter
        static string GetRandomLetters()
        {
            Random rand = new Random();
            string alpahabet = "";
            switch (rand.Next(26))
            {
                case 0: alpahabet = "A"; break;
                case 1: alpahabet = "B"; break;
                case 2: alpahabet = "C"; break;
                case 3: alpahabet = "D"; break;
                case 4: alpahabet = "E"; break;
                case 5: alpahabet = "F"; break;
                case 6: alpahabet = "G"; break;
                case 7: alpahabet = "H"; break;
                case 8: alpahabet = "I"; break;
                case 9: alpahabet = "J"; break;
                case 10: alpahabet = "K"; break;
                case 11: alpahabet = "L"; break;
                case 12: alpahabet = "M"; break;
                case 13: alpahabet = "N"; break;
                case 14: alpahabet = "O"; break;
                case 15: alpahabet = "P"; break;
                case 16: alpahabet = "Q"; break;
                case 17: alpahabet = "R"; break;
                case 18: alpahabet = "S"; break;
                case 19: alpahabet = "T"; break;
                case 20: alpahabet = "U"; break;
                case 21: alpahabet = "V"; break;
                case 22: alpahabet = "W"; break;
                case 23: alpahabet = "X"; break;
                case 24: alpahabet = "Y"; break;
                case 25: alpahabet = "Z"; break;
            }
            return alpahabet;
        }
        // Runs the game
        static void PlayGame()
        {
            Console.Clear(); // clears the console
            Console.SetWindowSize(width, height); // sets width and height of the console
            Console.SetBufferSize(width, height); // sets width and height of the buffer size of the console
            int speed = 0; // speed
            int maxRows = 0; // max rows
            bool numbers = false; // indication of including numbers

            // loop the message
            bool selection = true;
            while (selection != false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Difficulty:");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("[1] Easy");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("[2] Medium");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[3] Hard");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("[4] Challenge");
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("[5] Quit");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Input number here: ");
                int difficulty = Convert.ToInt32(Console.ReadLine());
                Console.ResetColor();
                switch (difficulty)
                {
                    case 1: maxRows = 5; speed = 2000; selection = false; break; // sets the mode to easy
                    case 2: maxRows = 8; speed = 1500; selection = false; break; // sets the mode to medium
                    case 3: maxRows = 10; speed = 1000; numbers = true; selection = false; break; // set the mode to hard
                    case 4: maxRows = 5; speed = 1500; numbers = true; challenge = true; selection = false; break; // sets the mode to challenge
                    case 5: Console.Clear(); attempts = 0; control = false; gameOver = true; selection = false; break; // quits the game
                    default: Console.Write("Invalid Input"); break; // repeats the message
                }
            }
            while (attempts != 0) // loops until its not game over
            {
                Console.Clear();
                int rows = 1;
                int numRows = maxRows; // used to indicate the remaining boxes
                control = true;
                while (control == true) // loop until the control is not available
                {
                    while (numRows != 0) // loop until all the boxes were placed to respective place
                    {
                        string letter = ""; // random letters to be guessed
                        string number = ""; // random numbers to be guessded
                        Instructions(); // shows the basic keys to used and shows the score and the attempts
                        Thread.Sleep(speed); // speed of the game
                        if (numbers == true) //if number is available
                        {
                            Random rand = new Random();
                            switch (rand.Next(2))
                            {
                                case 0: // if number
                                    // shows and arrange the boxes with its guess numbers
                                    number = GetRandomNumbers();
                                    Box(number, speed);
                                    Save.save[rows] = number; // saves the number
                                    ArrangeBoxes(rows, number, maxRows);
                                    numRows--; rows++;

                                    // deletes the guess letter inside the boxes
                                    if (numRows == 0)
                                    {
                                        rows = 1;
                                        for (int i = 0; i < maxRows; i++)
                                        {
                                            DeleteRandom(rows, maxRows); // delete the guess letter
                                            rows++;
                                        }
                                        Blank(maxRows); // input blanks
                                    }
                                    break;
                                case 1: // if letter
                                    // shows and arrange the boxes with its guess letters
                                    letter = GetRandomLetters();
                                    Box(letter, speed);
                                    Save.save[rows] = letter; // saves the letter
                                    ArrangeBoxes(rows, letter, maxRows);
                                    numRows--; rows++;

                                    // deletes the guess letter inside the boxes
                                    if (numRows == 0)
                                    {
                                        rows = 1;
                                        for (int i = 0; i < maxRows; i++)
                                        {
                                            DeleteRandom(rows, maxRows); // delete the guess letter
                                            rows++;
                                        }
                                        Blank(maxRows); // input blanks
                                    }
                                    break;
                            }
                        }
                        else // if number is not available
                        {
                            // shows and arrange the boxes with its guess letters
                            letter = GetRandomLetters();
                            Box(letter, speed);
                            Save.save[rows] = letter; // saves the letter
                            ArrangeBoxes(rows, letter, maxRows);
                            numRows--; rows++;

                            // deletes the guess letter inside the boxes
                            if (numRows == 0)
                            {
                                rows = 1;
                                for (int i = 0; i < maxRows; i++)
                                {
                                    DeleteRandom(rows, maxRows); // delete the guess letter
                                    rows++;
                                }
                                Blank(maxRows); // input blanks
                            }
                        }
                    }
                    char exit = Control(maxRows); // allows key interactions
                    if (exit == 'e') // if exit
                    {
                        Console.Clear(); attempts = 0; control = false; gameOver = true; break;
                    }
                    else if (exit == 'a') // if another loop for the boxes
                    {
                        control = false; break;
                    }
                    if (exit == 'c') // if another loop in a Challenge mode
                    {
                        control = false;
                        if (maxRows != 10)
                        { maxRows += 1; }
                        break;
                    }
                }
            }
        }
        // Shows scores, attempts, and the keys used in the Game.
        static void Instructions()
        {
            // Displays score on the upper right side of the Console.
            Console.SetCursorPosition(2, height / 2 - 5);
            Console.Write("Score " + score);

            // Displays attempts on the upper right side of the Console.
            Console.SetCursorPosition(2, height / 2 - 6);
            Console.Write("Attempts " + attempts);

            // Displays "Guessing Game" at the top side of the Console.
            Console.SetCursorPosition(width / 2 - 6, height / 2 + -9);
            Console.Write("Guessing Game! (Wait for all the boxes to appear)");

            // Displays the function of Enter on the bottom left of the Console
            Console.SetCursorPosition(width / 2 + -40, height / 2 + 9);
            Console.Write("Enter - To input letters/numbers");

            // Displays the function of Esc on the bottom right of the Console
            Console.SetCursorPosition(width / 2 + 15, height / 2 + 9);
            Console.Write("Esc - To quit the game.");
        }
        // Pops up on the right of the screen for the letter/numbers to guess
        static void Box(string guess, int speed)
        {
            // shows the letter/number inside a box
            Console.SetCursorPosition(width / 2 + 40, height / 2 + -8);
            Console.Write(" ===\n");
            Console.SetCursorPosition(width / 2 + 40, height / 2 + -7);
            Console.Write($"| {guess} |\n"); // contains the letter/number.
            Console.SetCursorPosition(width / 2 + 40, height / 2 + -6);
            Console.Write(" ===\n");

            Thread.Sleep(speed); // delays the execution

            // Immediately deletes the contain of the box
            Console.SetCursorPosition(width / 2 + 40, height / 2 + -8);
            Console.Write("           ");
            Console.SetCursorPosition(width / 2 + 40, height / 2 + -7);
            Console.Write($"           ");
            Console.SetCursorPosition(width / 2 + 40, height / 2 + -6);
            Console.Write("            ");
        }

        // Arrange the boxes in order with its guessing value
        static void ArrangeBoxes(int rows, string guess, int maxRows)
        {
            int newRows = -18; // used to add for adjusting the position in the x axis.
            int downBox = -3;  // used to add for adjusting the position in the y axis.

            if (maxRows > 5 && rows <= 5) // if the max rows is greater than 5 and do not have 5 boxes arrange
            {
                downBox = -6;
                for (int i = 1; i < rows; i++) // adjust the value of newRows for consistent distance between Boxes
                {
                    newRows += 8;
                }
                // Positions the box in order at the top first.
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write(" ===\n"); downBox++;
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write($"| {guess} |\n"); downBox++; // contains guessing value
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write(" ===");
            }
            else if (maxRows > 5 && rows > 5) // if the max rows is greater than 5 and have 5 boxes arrange
            {
                rows -= 5;
                for (int i = 1; i < rows; i++) // adjust the value of newRows for consistent distance between Boxes
                {
                    newRows += 8;
                }
                // Positions the box in order at the bottom of the first 5 box.
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write(" ===\n"); downBox++;
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write($"| {guess} |\n"); downBox++; // contains guessing value
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write(" ===");
            }
            else // if the max rows is less than or equal to 5
            {
                for (int i = 1; i < rows; i++) // adjust the value of newRows for consistent distance between Boxes
                {
                    newRows += 8;
                }
                // Positions the box at the center in order.
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write(" ===\n"); downBox++;
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write($"| {guess} |\n"); downBox++; // contains guessing value
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write(" ===");
            }
        }
        //Deletes the guessing elements inside the box
        static void DeleteRandom(int rows, int maxRows)
        {
            int newRows = -18; // used to add for adjusting the position in the x axis.
            int downBox = -3;  // used to add for adjusting the position in the y axis.

            if (maxRows > 5 && rows <= 5) // if the max rows is greater than 5 and do not have 5 boxes arrange
            {
                downBox = -6;
                for (int i = 1; i < rows; i++) // adjust the value of newRows for consistent distance between Boxes
                {
                    newRows += 8;
                }
                // Positions the box in order at the top first.
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write(" ===\n"); downBox++;
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write($"|   |\n"); downBox++; // deletes the guessing value
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write(" ===");
            }
            else if (maxRows > 5 && rows > 5) // if the max rows is greater than 5 and have 5 boxes arrange
            {
                rows -= 5;
                for (int i = 1; i < rows; i++) // adjust the value of newRows for consistent distance between Boxes
                {
                    newRows += 8;
                }
                // Positions the box in order at the bottom of the first 5 box.
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write(" ===\n"); downBox++;
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write($"|   |\n"); downBox++; // deletes the guessing value
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write(" ===");
            }
            else // if the max rows is less than or equal to 5
            {
                for (int i = 1; i < rows; i++) // adjust the value of newRows for consistent distance between Boxes
                {
                    newRows += 8;
                }
                // Positions the box at the center in order.
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write(" ===\n"); downBox++;
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write($"|   |\n"); downBox++; // deletes the guessing value
                Console.SetCursorPosition(width / 2 + newRows, height / 2 + downBox);
                Console.Write(" ===");
            }
        }
        // Creates Blank for the player to input
        static void Blank(int maxRows)
        {
            int x = 0; // helps in adjusting in x axis
            for (int i = 0; i < maxRows; i++) // loops for the exact number of blanks
            {

                if (maxRows > 5) // if max rows is greater than 5
                {
                    x = 0;
                    int y = 6; // helps in adjusting the y axis
                    if (x <= 10 && i < 5) // if does not have first 5 boxes
                    {
                        x = 0;
                        y = 5;
                        for (int v = 1; v <= i; v++) // adjust the value of x for consistent distance between blanks
                        {
                            x += 2;
                        }
                    }
                    for (int v = 1; v <= i - 5; v++) // adjust the value of x for consistent distance between blanks
                    {
                        x += 2;
                    }
                    // Positions and Place the Blank
                    Console.SetCursorPosition(width / 2 - 6 + x + 2, height / 2 + y);
                    Console.Write("_");
                    x += 2;
                }
                else if (maxRows <= 5) // if max rows is less than and equal to 5
                {
                    // Positions and Place the Blank
                    Console.SetCursorPosition(width / 2 - maxRows + x + 1, height / 2 + 5);
                    Console.Write("_");
                    x += 2;
                }
            }
        }
        // Checks the Key pressed by the player
        static char Control(int maxRows)
        {
            char game = ' '; // returns for condition
            if (Console.KeyAvailable) // Checks pressed by the player
            {
                var key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.LeftArrow) // if left arrows
                {
                    if (Position.X == -3) // limits the position of the cursor in x axis.
                    { }
                    else
                    {
                        Position.X += -2; // adjust the cursor to the left
                    }
                    // Positions the Cursor
                    Console.SetCursorPosition(width / 2 + Position.X - 1, height / 2 + 5 + Position.Y);
                }
                else if (key == ConsoleKey.RightArrow) // if right arrows
                {
                    if (Position.X == 5) // limits the position of the cursor in x axis.
                    { }
                    else
                    {
                        if (Position.X == -3 && Position.Y == 1 && maxRows > 5 || Position.X == -1 && Position.Y == 1 && maxRows > 6 || Position.X == 1 && Position.Y == 1 && maxRows > 7 || Position.X == 3 && Position.Y == 1 && maxRows > 8 || Position.X == 5 && Position.Y == 1 && maxRows > 9)
                        {
                            if (Position.X == -3 && Position.Y == 1 && maxRows == 6 || Position.X == -1 && Position.Y == 1 && maxRows == 7 || Position.X == 1 && Position.Y == 1 && maxRows == 8 || Position.X == 3 && Position.Y == 1 && maxRows == 9 || Position.X == 5 && Position.Y == 1 && maxRows > 10)
                            {

                            }
                            else
                            {
                                Position.X += 2; // adjust the cursor to the right 
                            }
                        }
                        else if (Position.X == -3 && Position.Y == 0 || Position.X == -1 && Position.Y == 0 || Position.X == 1 && Position.Y == 0 || Position.X == 3 && Position.Y == 0 || Position.X == 5 && Position.Y == 0)
                        {
                            Position.X += 2; // adjust the cursor to the right
                        }
                        Console.SetCursorPosition(width / 2 + Position.X - 1, height / 2 + 5 + Position.Y); // Position the Cursor
                    }
                }

                if (maxRows > 5) // if max rows is greater than 5
                {
                    if (key == ConsoleKey.UpArrow) // if upper arrow
                    {
                        if (Position.Y == 0) // limits the position of the cursor in y axis.
                        { }
                        else
                        {
                            Position.Y += -1; // adjust the cursor up
                        }
                        Console.SetCursorPosition(width / 2 + Position.X - 1, height / 2 + 5 + Position.Y); // Position the Cursor
                    }
                    else if (key == ConsoleKey.DownArrow) // if down arrow
                    {
                        if (Position.Y == 1) // limits the position of the cursor in y axis.
                        { }
                        else
                        {
                            if (Position.X == -3 && maxRows > 5 || Position.X == -1 && maxRows > 6 || Position.X == 1 && maxRows > 7 || Position.X == 3 && maxRows > 8 || Position.X == 5 && maxRows > 9)
                            {
                                Position.Y += 1; // adjust the cursor down
                            }
                            Console.SetCursorPosition(width / 2 + Position.X - 1, height / 2 + 5 + Position.Y); // Position the Cursor
                        }
                    }
                }
                if (key == ConsoleKey.Enter) // if enter
                {
                    string input = Console.ReadLine().ToUpper(); // enter input by the user
                    LocateSave(input); // locate save position
                    if (Boxes.filledBoxes == maxRows) // if all the blanks were filled
                    {
                        //loops the message
                        bool des = true;
                        while (des == true)
                        {
                            Console.SetCursorPosition(width / 2 - 8, height / 2 + 7);
                            Console.Write("Check the input? (Y/N) ");
                            string res = Console.ReadLine().ToUpper();
                            switch (res)
                            {
                                case "Y":
                                    CheckInput(maxRows); des = false;
                                    if (challenge == true) // if challenge is true
                                    {
                                        game = 'c';
                                    }
                                    else // if challenge is false
                                    {
                                        game = 'a';
                                    }
                                    break;
                                case "N": des = false; Console.SetCursorPosition(width / 2 + -8, height / 2 + 7); Console.Write("                         "); Console.SetCursorPosition(width / 2 + Position.X - 1, height / 2 + 5 + Position.Y); break; // deletes the message and go back to original position.
                                default: // repeats the message again
                                    Console.SetCursorPosition(width / 2 - 3, height / 2 + 8);
                                    Console.Write("Invalid Input");
                                    Thread.Sleep(100); Console.SetCursorPosition(width / 2 - 3, height / 2 + 8); Console.Write("                      "); break;
                            }
                        }
                    }
                    else // if all the blanks are not filled
                    {
                        // Displays saved
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(width / 2 - 2, height / 2 + 7);
                        Console.Write("Saved");

                        Console.ForegroundColor = ConsoleColor.White;
                        Console.SetCursorPosition(width / 2 + Position.X - 1, height / 2 + 5 + Position.Y); // brings back to original position
                    }
                }
                else if (key == ConsoleKey.Escape)
                {
                    // loops the message
                    bool loop = true;
                    while (loop == true)
                    {
                        Console.SetCursorPosition(width / 2 - 10, height / 2 + 7);
                        Console.Write("Quit the Game? (Y/N) ");
                        string res = Console.ReadLine().ToUpper();

                        switch (res)
                        {
                            case "Y":
                                loop = false;
                                game = 'e';
                                GameOverMessage();
                                PlayGame();
                                break; // quits the game
                            case "N": // deletes the message and go back to original position
                                Console.SetCursorPosition(width / 2 - 10, height / 2 + 7);
                                Console.Write("                         "); loop = false; Console.SetCursorPosition(width / 2 + Position.X - 1, height / 2 + 5 + Position.Y); break;
                            default: // repeats the message
                                Console.SetCursorPosition(width / 2 - 7, height / 2 + 8);
                                Console.Write("Invalid Input");
                                Thread.Sleep(100); Console.SetCursorPosition(width / 2 - 7, height / 2 + 8); Console.Write("                      "); break;
                        }
                    }
                }
            }
            return game;
        }

        // Checks the input by the player
        static void CheckInput(int maxRows)
        {
            for (int i = 1; i <= maxRows; i++)
            {
                int x = -18; // helps with positioning in x axis.
                int y = -2; // helps with position in y axis.

                //adjust the positions
                for (int v = 1; v < i; v++)
                {
                    if (i > 5) // if rows 6 and above
                    {
                        if (v + 5 == i) // if rows 6, do not adjust the x position for x axis
                        {
                            break;
                        }
                        else if (v + 5 != i) // if rows 7 above adjust the x position
                        {
                            x += 8;
                        }
                    }
                    else // adjust the x position
                    {
                        x += 8;
                    }
                }
                if (maxRows > 5 && Boxes.checkBoxes + Boxes.wrongBoxes < 5) // if checked boxes is less than 5
                {
                    y = -5;
                }
                if (Save.input[i] == Save.save[i]) // if correct input
                {
                    score += 25; // add scores
                    comboScore += 0.20; // add comboScores
                    Console.ForegroundColor = ConsoleColor.Green; // green for correct input
                    Console.SetCursorPosition(width / 2 + x + 2, height / 2 + y);
                    Console.Write(Save.input[i]);
                    Boxes.checkBoxes++; // mark as correct input
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else // if not correct input
                {
                    comboScore = 1; // resets the combo score
                    Console.ForegroundColor = ConsoleColor.Red; // red for wrong input
                    Console.SetCursorPosition(width / 2 + x + 2, height / 2 + y);
                    Console.Write(Save.save[i]);
                    Boxes.wrongBoxes++; // mark as wrong input
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            if (Boxes.checkBoxes == maxRows) // if all the rows are mark as check
            {
                score = Math.Round(score * comboScore, 2);

            }
            else if (Boxes.checkBoxes + Boxes.wrongBoxes == maxRows) // it not all the rows are marked as check
            {
                attempts -= 1; // minus attempts
                while (attempts == 0) // if all the attempts has been used
                {
                    GameOverMessage(); // end the game
                    PlayGame();
                    break;
                }
            }
            Console.ReadKey(true);

            // Resets the value
            for (int i = 0; i < maxRows; i++)
            {
                Save.save[i + 1] = null;
                Save.input[i + 1] = null;
            }
            Boxes.checkBoxes = 0;
            Boxes.filledBoxes = 0;
            Boxes.wrongBoxes = 0;
        }

        // Locate saves inputted in the blanks
        static void LocateSave(string input)
        {
            int num = 0; // indicates row location save
            if (Position.X == -3 && Position.Y == 0) // row 1
            {
                num = 1;
                if (Save.input[num] == null)
                {
                    Boxes.filledBoxes++; // mark as filled blanks
                }
                else { }
                Save.input[num] = input; // saved inputed by the player
            }
            else if (Position.X == -1 && Position.Y == 0) // row 2
            {
                num = 2;
                if (Save.input[num] == null)
                {
                    Boxes.filledBoxes++; // mark as filled blanks
                }
                else { }
                Save.input[num] = input; // saved inputed by the player
            }
            else if (Position.X == 1 && Position.Y == 0) // row 3
            {
                num = 3;
                if (Save.input[num] == null)
                {
                    Boxes.filledBoxes++; // mark as filled blanks
                }
                else { }
                Save.input[num] = input; // saved inputed by the player
            }
            else if (Position.X == 3 && Position.Y == 0) // row 4
            {
                num = 4;
                if (Save.input[num] == null)
                {
                    Boxes.filledBoxes++; // mark as filled blanks
                }
                else { }
                Save.input[num] = input; // saved inputed by the player
            }
            else if (Position.X == 5 && Position.Y == 0) // row 5
            {
                num = 5;
                if (Save.input[num] == null)
                {
                    Boxes.filledBoxes++; // mark as filled blanks
                }
                else { }
                Save.input[num] = input; // saved inputed by the player
            }
            if (Position.X == -3 && Position.Y == 1) // row 6
            {
                num = 6;
                if (Save.input[num] == null)
                {
                    Boxes.filledBoxes++; // mark as filled blanks
                }
                else { }
                Save.input[num] = input; // saved inputed by the player
            }
            else if (Position.X == -1 && Position.Y == 1) // row 7
            {
                num = 7;
                if (Save.input[num] == null)
                {
                    Boxes.filledBoxes++; // mark as filled blanks
                }
                else { }
                Save.input[num] = input; // saved inputed by the player
            }
            else if (Position.X == 1 && Position.Y == 1) // row 8
            {
                num = 8;
                if (Save.input[num] == null)
                {
                    Boxes.filledBoxes++; // mark as filled blanks
                }
                else { }
                Save.input[num] = input; // saved inputed by the player
            }
            else if (Position.X == 3 && Position.Y == 1) // row 9
            {
                num = 9;
                if (Save.input[num] == null)
                {
                    Boxes.filledBoxes++; // mark as filled blanks
                }
                else { }
                Save.input[num] = input; // saved inputed by the player
            }
            else if (Position.X == 5 && Position.Y == 1) // row 10
            {
                num = 10;
                if (Save.input[num] == null)
                {
                    Boxes.filledBoxes++; // mark as filled blanks
                }
                else { }
                Save.input[num] = input; // saved inputed by the player
            }
        }
        static void GameOverMessage()
        {
            Console.Clear();
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + -3);
            Console.Write("Game Over!!");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + -2);
            Console.Write("Thank you for Playing!!");
            Console.SetCursorPosition(Console.WindowWidth / 2 - 8, Console.WindowHeight / 2 + -1);
            Console.Write("Score: " + score);
            Console.ReadKey(true);
            Console.Clear();
        }

        // Import necessary Windows API functions
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        // Define constants for the ShowWindow API
        const int SW_MAXIMIZE = 3;
        const int SW_RESTORE = 9; // Restore window to its original size from maximized state

        // Method to maximize the console window
        static void MaximizeConsole()
        {
            IntPtr consoleHandle = GetConsoleWindow();
            ShowWindow(consoleHandle, SW_MAXIMIZE);
        }

        // Method to resize the console window to a specific width and height
        static void ResizeConsole(int width, int height)
        {
            // First, restore the window (to avoid issues with resizing from a maximized state)
            IntPtr consoleHandle = GetConsoleWindow();
            ShowWindow(consoleHandle, SW_RESTORE);

            // Now, resize the console window to the desired size
            try
            {
                Console.SetWindowSize(width, height);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Error: The specified size is too small or too large.");
            }
        }
    }

}
// Saved
struct Save()
{
    public static string[] save = new string[11]; // save guess letters/numbers
    public static string[] input = new string[11]; // save inputed by the 
}

// Position
struct Position
{
    public static int X = -1; // Position of the cursor in the x axis
    public static int Y; // Positon of the cursor in the y axis
}

//Boxes
struct Boxes
{
    public static int filledBoxes;
    public static int checkBoxes;
    public static int wrongBoxes;
}


