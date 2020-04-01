using RandomCardGenerator;
using System;

namespace CardChauffeur.WindowsConsole
{
    class ConsoleGUI
    {
        private Engine engine;
        string headerString, cardString, helpString, log;
        bool confirmationPending;
        UserAction lastUserAction;

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

        private void printFrame()
        {
            Console.Clear();
            Console.WriteLine(headerString + cardString + helpString + log);
        }

        private string GetClosedCardFrame()
        {
            return
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
        }

        private string GetHeader()
        {
            return
            "\n" +
            "       CARD CHAUFFEUR\n" +
            "\n";
        }

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

        private string GetHelp()
        {
            return
            "\n\n" +
            "   Play - P     Shuffle - S\n" +
            "   Reset - R     Quit - Q\n" +
            "\n";
        }

        private void NoTriggered()
        {
            if (confirmationPending)
            {
                switch (lastUserAction)
                {
                    case UserAction.Shuffle:
                    case UserAction.Reset:
                    case UserAction.Quit:
                        log = "";
                        break;
                }
            }
            else
            {
                log = "Invalid key";
            }
            confirmationPending = false;
        }

        private void YesTriggered()
        {
            if (confirmationPending)
            {
                switch (lastUserAction)
                {
                    case UserAction.Shuffle:
                        log = "Shuffling the deck...";
                        engine.Shuffle();
                        log = "Deck reshuffled successfully.";
                        break;
                    case UserAction.Reset:
                        log = "Resetting the game...";
                        engine.Reset();
                        log = "Game reset successfully.";
                        cardString = GetClosedCardFrame();
                        break;
                    case UserAction.Quit:
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
        }

        private void QuitTriggered()
        {
            if (confirmationPending)
            {
                log = "Invalid key";
                confirmationPending = false;
            }
            else
            {
                confirmationPending = true;
                lastUserAction = UserAction.Quit;
                log = "Are you sure you want to exit? (Y/N): ";
            }
        }

        private void ResetTriggered()
        {
            if (confirmationPending)
            {
                log = "Invalid key";
                confirmationPending = false;
            }
            else
            {
                confirmationPending = true;
                lastUserAction = UserAction.Reset;
                log = "Are you sure you want to reset? (Y/N): ";
            }
        }

        private void ShuffleTriggered()
        {
            if (confirmationPending)
            {
                log = "Invalid key";
                confirmationPending = false;
            }
            else
            {
                confirmationPending = true;
                lastUserAction = UserAction.Shuffle;
                log = "Are you sure you want to shuffle? (Y/N): ";
            }
        }

        private void PlayTriggered()
        {
            if (confirmationPending)
            {
                log = "Invalid key";
                confirmationPending = false;
            }
            else
            {
                log = "Drawing new card...";
                Card newCard = engine.Draw();
                if (newCard == null)
                {
                    log = "Cards in the deck are over. Do you want to reset? (Y/N): ";
                    confirmationPending = true;
                    lastUserAction = UserAction.Reset;
                }
                else
                {
                    log = "A card was drew";
                    cardString = GetCard(GetCardCode(newCard.number), GetSuitCode(newCard.suit));
                    confirmationPending = false;
                }
            }
        }

        internal ConsoleGUI()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            engine = new Engine();

            headerString = GetHeader();
            cardString = GetClosedCardFrame();
            helpString = GetHelp();
            log = "";
        }

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
                        PlayTriggered();
                        break;
                    case ConsoleKey.S:
                        ShuffleTriggered();
                        break;
                    case ConsoleKey.R:
                        ResetTriggered();
                        break;
                    case ConsoleKey.Q:
                        QuitTriggered();
                        break;
                    case ConsoleKey.Y:
                        YesTriggered();
                        break;
                    case ConsoleKey.N:
                        NoTriggered();
                        break;
                    default:
                        log = "Invalid key";
                        confirmationPending = false;
                        break;
                }
                printFrame();
            }
        }
    }
}
