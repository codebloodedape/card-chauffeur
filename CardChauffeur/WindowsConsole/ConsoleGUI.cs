using RandomCardGenerator;
using System;
using System.IO;
using System.Text;

namespace CardChauffeur.WindowsConsole
{
    /// <summary>
    /// This is the console GUI class which displays and retrieves info on STD console
    /// </summary>
    class ConsoleGUI
    {
        private string logFilePath;
        private bool logInitialized = false;
        private readonly Engine engine;
        private readonly string headerString = 
            "\n       CARD CHAUFFEUR\n\n";

        private readonly string closedCardFrame = 
            "   .====================.\n" +
            "   ||||||||||||||||||||||\n" +
            "   ||||||||||||||||||||||\n" +
            "   ||||||||||||||||||||||\n" +
            "   ||||||||||||||||||||||\n" +
            "   ||||||||||||||||||||||\n" +
            "   ||||||||||||||||||||||\n" +
            "   ||||||||||||||||||||||\n" +
            "   ||||||||||||||||||||||\n" +
            "   ||||||||||||||||||||||\n" +
            "   ||||||||||||||||||||||\n" +
            "   ||||||||||||||||||||||\n" +
            "   '===================='";

        private readonly string gamehelp =
            "\n\n   Play - P     Shuffle - S\n" +
            "   Reset - R     Quit - Q\n\n";

        private readonly string confirmationHelp = 
            "\n\n   Yes - Y     No - N\n\n\n";

        private string cardString, helpString, userNotification;
        private bool confirmationPending;
        private EUserAction lastUserAction;

        /// <summary>
        /// Gets the card code string based on index
        /// </summary>
        /// <param name="number">Index of the card</param>
        /// <returns></returns>
        private string GetCardCode(int number)
        {
            Log("Getting card's code for " + number);
            switch (number)
            {
                case 1:
                    return "A";
                case 11:
                    return "K";
                case 12:
                    return "Q";
                case 13:
                    return "J";
                default:
                    return number.ToString();
            }
        }

        /// <summary>
        /// Gets the unicode of the suit symbol
        /// </summary>
        /// <param name="suit">Suit of the card</param>
        /// <returns></returns>
        private string GetSuitCode(Suit suit)
        {
            Log("Getting suit' unicode for " + suit.ToString());
            switch (suit)
            {
                case Suit.Club:
                    return "\u2663";
                case Suit.Diamond:
                    return "\u2666";
                case Suit.Heart:
                    return "\u2665";
                case Suit.Spade:
                    return "\u2660";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Clears the console and prints the header, card frame, help and action log
        /// </summary>
        private void printFrame()
        {
            Log("Refreshing screen. \nUI state - \nHeader: " + headerString + " Help: " + 
                helpString + " User notification: " + userNotification);
            Console.Clear();
            Console.WriteLine(headerString + cardString + helpString + userNotification);
        }

        /// <summary>
        /// Returns the card frame to be printed on console, with card suit and code.
        /// </summary>
        /// <param name="number">Code of the card</param>
        /// <param name="suit">Suit of the card</param>
        /// <returns></returns>
        private string GetCard(string number, string suit)
        {
            Log("Forming card UI for " + number + " " + suit.ToString());
            return
            "   .====================.\n" +
            "   ||" + number + "                 ||\n" +
            "   ||                  ||\n" +
            "   ||                  ||\n" +
            "   ||                  ||\n" +
            "   ||                  ||\n" +
            "   ||        " + suit + "         ||\n" +
            "   ||                  ||\n" +
            "   ||                  ||\n" +
            "   ||                  ||\n" +
            "   ||                  ||\n" +
            "   ||                 " + number + "||\n" +
            "   '===================='";
        }

        /// <summary>
        /// When No is selected by the user
        /// </summary>
        private void NoOptionTriggered()
        {
            if (confirmationPending)
            {
                userNotification = "";
            }
            else
            {
                userNotification = "Invalid key";
            }
            confirmationPending = false;
            helpString = gamehelp;
        }

        /// <summary>
        /// When Yes is selected by the user
        /// </summary>
        private void YesOptionTriggered()
        {
            if (confirmationPending)
            {
                switch (lastUserAction)
                {
                    case EUserAction.Shuffle:
                        Log("Shuffling the deck");
                        userNotification = "Shuffling the deck...";
                        engine.Shuffle();
                        userNotification = "Deck reshuffled successfully.";
                        break;
                    case EUserAction.Reset:
                        Log("Resetting the game.");
                        userNotification = "Resetting the game...";
                        engine.Reset();
                        userNotification = "Game reset successfully.";
                        cardString = closedCardFrame;
                        break;
                    case EUserAction.Quit:
                        Log("Quiting the game.");
                        userNotification = "Exiting...";
                        Environment.Exit(0);
                        break;
                }
                //log = "";
            }
            else
            {
                userNotification = "Invalid key";
            }
            confirmationPending = false;
            helpString = gamehelp;
        }

        /// <summary>
        /// When Quit is selected by the user
        /// </summary>
        private void QuitOptionTriggered()
        {
            if (confirmationPending)
            {
                userNotification = "Invalid key";
                confirmationPending = false;
                helpString = gamehelp;
            }
            else
            {
                confirmationPending = true;
                helpString = confirmationHelp;
                lastUserAction = EUserAction.Quit;
                userNotification = "Are you sure you want to exit?";
            }
        }

        /// <summary>
        /// When Reset is selected by the user
        /// </summary>
        private void ResetOptionTriggered()
        {
            if (confirmationPending)
            {
                userNotification = "Invalid key";
                confirmationPending = false;
                helpString = gamehelp;
            }
            else
            {
                confirmationPending = true;
                helpString = confirmationHelp;
                lastUserAction = EUserAction.Reset;
                userNotification = "Are you sure you want to reset?";
            }
        }

        /// <summary>
        /// When Shuffle is selected by the user
        /// </summary>
        private void ShuffleOptionTriggered()
        {
            if (confirmationPending)
            {
                userNotification = "Invalid key";
                confirmationPending = false;
                helpString = gamehelp;
            }
            else
            {
                confirmationPending = true;
                helpString = confirmationHelp;
                lastUserAction = EUserAction.Shuffle;
                userNotification = "Are you sure you want to shuffle?";
            }
        }

        /// <summary>
        /// When Play is selected by the user
        /// </summary>
        private void PlayOptionTriggered()
        {
            if (confirmationPending)
            {
                userNotification = "Invalid key";
                confirmationPending = false;
                helpString = gamehelp;
            }
            else
            {
                Log("Drawing a card.");
                userNotification = "Drawing new card...";
                Card newCard = engine.Draw();
                if (newCard == null)
                {
                    userNotification = "Cards in the deck are over. Do you want to reset? (Y/N): ";
                    confirmationPending = true;
                    helpString = confirmationHelp;
                    lastUserAction = EUserAction.Reset;
                }
                else
                {
                    userNotification = "A card was drew";
                    cardString = GetCard(GetCardCode(newCard.number), GetSuitCode(newCard.suit));
                    confirmationPending = false;
                    helpString = gamehelp;
                }
            }
        }

        private bool CreateLogFile()
        {
            string currentDirectoryPath;
            try
            {
                currentDirectoryPath = Directory.GetCurrentDirectory();
            }
            catch(UnauthorizedAccessException)
            {
                Console.WriteLine("Couldn't start the application as you do not have permission to read in the current directory");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled exception occured.");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }

            string logDirectoryPath = currentDirectoryPath + "\\log";
            if (!Directory.Exists(logDirectoryPath))
            {
                try
                {
                    Directory.CreateDirectory(logDirectoryPath);
                }
                catch (UnauthorizedAccessException)
                {
                    Console.WriteLine("Couldn't start the application as you do not have permission to create files in the directory "
                        + logDirectoryPath);
                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unhandled exception occured.");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine(ex.StackTrace);
                    return false;
                }
            }

            logFilePath = logDirectoryPath + "\\card-chauffeur.log";

            try
            {
                using (FileStream fs = File.Create(logFilePath))
                {
                    byte[] info = new UTF8Encoding(true).GetBytes("Log Initialised");
                    fs.Write(info, 0, info.Length);
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Couldn't start the application as you do not have permission to create files in the directory "
                    + logDirectoryPath);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhandled exception occured.");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                return false;
            }
            //Log("Log Initialised");
            return true;
        }

        private void Log(string text)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(logFilePath, true))
                {
                    file.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ":" + text);
                }
            }
            catch (Exception ex)
            {
                Console.Clear();
                Console.WriteLine("Unable to log. Exiting the game...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        private void Log(Exception ex)
        {
            try
            {
                using (StreamWriter file = new StreamWriter(logFilePath, true))
                {
                    file.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") + ":" + ex.Message + "\n" + ex.StackTrace);
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Unable to log. Exiting the game...");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Loads the game engine and returns the object. It does not the game though. 
        /// </summary>
        internal ConsoleGUI()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            if (CreateLogFile())
            {
                logInitialized = true;
                try
                {
                    Log("Initializing game engine.");
                    engine = new Engine();
                    Log("Game engine initialized.");
                }
                catch (Exception ex)
                {
                    Log(ex);
                }

                cardString = closedCardFrame;
                helpString = gamehelp;
                userNotification = "";
            }
            else
            {
                logInitialized = false;
            }
        }

        /// <summary>
        /// This method prints the strings based on the initial state and starts listening to the console input of a character
        /// </summary>
        internal void StartGame()
        {
            if (logInitialized)
            {
                try
                {

                    Log("Starting game engine.");
                    engine.Start();
                    Log("Game engine started.");

                    printFrame();
                    while (true)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true); //TRUE to not display the key pressed
                        Log(key.Key + " pressed.");
                        switch (key.Key)
                        {
                            case ConsoleKey.P:
                                PlayOptionTriggered();
                                break;
                            case ConsoleKey.S:
                                ShuffleOptionTriggered();
                                break;
                            case ConsoleKey.R:
                                ResetOptionTriggered();
                                break;
                            case ConsoleKey.Q:
                                QuitOptionTriggered();
                                break;
                            case ConsoleKey.Y:
                                YesOptionTriggered();
                                break;
                            case ConsoleKey.N:
                                NoOptionTriggered();
                                break;
                            default:
                                userNotification = "Invalid key";
                                confirmationPending = false;
                                helpString = gamehelp;
                                break;
                        }
                        printFrame();
                    }

                }
                catch (Exception ex)
                {
                    Log(ex);
                    Console.WriteLine("Something went wrong while starting the game. Please check the log.");
                    return;
                }
            }
        }
    }
}
