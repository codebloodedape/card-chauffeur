using Logger;
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
            "   Reset - R     Quit - Q\n" +
            "   Save - V     Recover - B\n\n";

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
            Logger.Logger.Log("Getting card's code for " + number);
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
            Logger.Logger.Log("Getting suit' unicode for " + suit.ToString());
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
            Logger.Logger.Log("Refreshing screen. \nUI state - \nHeader: " + headerString + " Help: " +
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
            Logger.Logger.Log("Forming card UI for " + number + " " + suit.ToString());
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
        private async void YesOptionTriggered()
        {
            if (confirmationPending)
            {
                switch (lastUserAction)
                {
                    case EUserAction.Shuffle:
                        Logger.Logger.Log("Shuffling the deck");
                        userNotification = "Shuffling the deck...";
                        engine.Shuffle();
                        userNotification = "Deck reshuffled successfully.";
                        break;
                    case EUserAction.Reset:
                        Logger.Logger.Log("Resetting the game.");
                        userNotification = "Resetting the game...";
                        engine.Reset();
                        userNotification = "Game reset successfully.";
                        cardString = closedCardFrame;
                        break;
                    case EUserAction.Quit:
                        Logger.Logger.Log("Quiting the game.");
                        userNotification = "Exiting...";
                        Environment.Exit(0);
                        break;
                    case EUserAction.Save:
                        Logger.Logger.Log("Saving the game.");
                        userNotification = "Saving...";
                        if (await engine.Save())
                        {
                            userNotification = "Game saved successfully.";
                        }
                        else
                        {
                            userNotification = "Game couldn't be saved.";
                        }
                        break;
                    case EUserAction.Recover:
                        Logger.Logger.Log("Recovering the game.");
                        userNotification = "Recovering...";
                        if (await engine.Recover())
                        {
                            cardString = closedCardFrame;
                            userNotification = "Game recovered successfully.";
                        }
                        else
                        {
                            userNotification = "Game couldn't be recoverd.";
                        }

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
                Logger.Logger.Log("Drawing a card.");
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
                    userNotification = "A card was drawn";
                    cardString = GetCard(GetCardCode(newCard.number), GetSuitCode(newCard.suit));
                    confirmationPending = false;
                    helpString = gamehelp;
                }
            }
        }

        /// <summary>
        /// When Recover is selected by the user
        /// </summary>
        private void RecoverOptionTriggered()
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
                lastUserAction = EUserAction.Recover;
                userNotification = "Are you sure you want to Recover?";
            }
        }

        /// <summary>
        /// When Save is selected by the user
        /// </summary>
        private void SaveOptionTriggered()
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
                lastUserAction = EUserAction.Save;
                userNotification = "Are you sure you want to Save?";
            }
        }


        /// <summary>
        /// Loads the game engine and returns the object. It does not the game though. 
        /// </summary>
        internal ConsoleGUI()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            if (Logger.Logger.CreateLogFile("card-chauffeur"))
            {
                logInitialized = true;
                try
                {
                    Logger.Logger.Log("Initializing game engine.");
                    engine = new Engine();
                    Logger.Logger.Log("Game engine initialized.");
                }
                catch (Exception ex)
                {
                    Logger.Logger.Log(ex);
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

                    Logger.Logger.Log("Starting game engine.");
                    engine.Start();
                    Logger.Logger.Log("Game engine started.");

                    printFrame();
                    while (true)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true); //TRUE to not display the key pressed
                        Logger.Logger.Log(key.Key + " pressed.");
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
                            case ConsoleKey.V:
                                SaveOptionTriggered();
                                break;
                            case ConsoleKey.B:
                                RecoverOptionTriggered();
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
                    Logger.Logger.Log(ex);
                    Console.WriteLine("Something went wrong while starting the game. Please check the log.");
                    return;
                }
            }
        }
    }
}
