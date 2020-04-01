using RandomCardGenerator;
using System;

namespace CardChauffeur.WindowsConsole
{
    /// <summary>
    /// This is the console GUI class which displays and retrieves info on STD console
    /// </summary>
    class ConsoleGUI
    {
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

        private string cardString, helpString, log;
        private bool confirmationPending;
        private EUserAction lastUserAction;

        /// <summary>
        /// Gets the card code string based on index
        /// </summary>
        /// <param name="number">Index of the card</param>
        /// <returns></returns>
        private string GetCardCode(int number)
        {
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
            Console.Clear();
            Console.WriteLine(headerString + cardString + helpString + log);
        }

        /// <summary>
        /// Returns the card frame to be printed on console, with card suit and code.
        /// </summary>
        /// <param name="number">Code of the card</param>
        /// <param name="suit">Suit of the card</param>
        /// <returns></returns>
        private string GetCard(string number, string suit)
        {
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
                log = "";
            }
            else
            {
                log = "Invalid key";
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
                        log = "Shuffling the deck...";
                        engine.Shuffle();
                        log = "Deck reshuffled successfully.";
                        break;
                    case EUserAction.Reset:
                        log = "Resetting the game...";
                        engine.Reset();
                        log = "Game reset successfully.";
                        cardString = closedCardFrame;
                        break;
                    case EUserAction.Quit:
                        log = "Exiting...";
                        Environment.Exit(0);
                        break;
                }
                //log = "";
            }
            else
            {
                log = "Invalid key";
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
                log = "Invalid key";
                confirmationPending = false;
                helpString = gamehelp;
            }
            else
            {
                confirmationPending = true;
                helpString = confirmationHelp;
                lastUserAction = EUserAction.Quit;
                log = "Are you sure you want to exit?";
            }
        }

        /// <summary>
        /// When Reset is selected by the user
        /// </summary>
        private void ResetOptionTriggered()
        {
            if (confirmationPending)
            {
                log = "Invalid key";
                confirmationPending = false;
                helpString = gamehelp;
            }
            else
            {
                confirmationPending = true;
                helpString = confirmationHelp;
                lastUserAction = EUserAction.Reset;
                log = "Are you sure you want to reset?";
            }
        }

        /// <summary>
        /// When Shuffle is selected by the user
        /// </summary>
        private void ShuffleOptionTriggered()
        {
            if (confirmationPending)
            {
                log = "Invalid key";
                confirmationPending = false;
                helpString = gamehelp;
            }
            else
            {
                confirmationPending = true;
                helpString = confirmationHelp;
                lastUserAction = EUserAction.Shuffle;
                log = "Are you sure you want to shuffle?";
            }
        }

        /// <summary>
        /// When Play is selected by the user
        /// </summary>
        private void PlayOptionTriggered()
        {
            if (confirmationPending)
            {
                log = "Invalid key";
                confirmationPending = false;
                helpString = gamehelp;
            }
            else
            {
                log = "Drawing new card...";
                Card newCard = engine.Draw();
                if (newCard == null)
                {
                    log = "Cards in the deck are over. Do you want to reset? (Y/N): ";
                    confirmationPending = true;
                    helpString = confirmationHelp;
                    lastUserAction = EUserAction.Reset;
                }
                else
                {
                    log = "A card was drew";
                    cardString = GetCard(GetCardCode(newCard.number), GetSuitCode(newCard.suit));
                    confirmationPending = false;
                    helpString = gamehelp;
                }
            }
        }

        /// <summary>
        /// Loads the game engine and returns the object. It does not the game though. 
        /// </summary>
        internal ConsoleGUI()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            engine = new Engine();

            cardString = closedCardFrame;
            helpString = gamehelp;
            log = "";
        }

        /// <summary>
        /// This method prints the strings based on the initial state and starts listening to the console input of a character
        /// </summary>
        internal void StartGame()
        {
            engine.Start();
            printFrame();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true); //TRUE to not display the key pressed
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
                        log = "Invalid key";
                        confirmationPending = false;
                        helpString = gamehelp;
                        break;
                }
                printFrame();
            }
        }
    }
}
