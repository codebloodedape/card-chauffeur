using RandomCardGenerator;
using System;

namespace CardChauffeur.WindowsConsole
{
    class ConsoleGUI
    {
        private Engine engine;
        string headerString, cardString, helpString, confirmationString;
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
            Console.WriteLine(headerString + cardString + helpString + confirmationString);
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

        internal ConsoleGUI()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            engine = new Engine();

            headerString = GetHeader();
            cardString = GetClosedCardFrame();
            helpString = GetHelp();
            confirmationString = "";
        }

        internal void StartGame()
        {
            engine.Start();
            printFrame();
            while (true)
            {
                ConsoleKeyInfo key = Console.ReadKey(true); //TRUE to not display the key pressed
                //State state;
                switch (key.Key)
                {
                    case ConsoleKey.P:

                        if (confirmationPending)
                        {
                            confirmationString = "Invalid key";
                            confirmationPending = false;
                        }
                        else
                        {
                            Card newCard = engine.Draw();
                            if (newCard == null)
                            {
                                confirmationString = "Cards in the deck are over. Do you want to reset? (Y/N): ";
                                confirmationPending = true;
                                lastUserAction = UserAction.Reset;
                            }
                            else
                            {
                                confirmationString = "";
                                cardString = GetCard(GetCardCode(newCard.number), GetSuitCode(newCard.suit));
                                confirmationPending = false;
                            }
                        }
                        break;
                    
                    case ConsoleKey.S:

                        if (confirmationPending)
                        {
                            confirmationString = "Invalid key";
                            confirmationPending = false;
                        }
                        else
                        {
                            confirmationPending = true;
                            lastUserAction = UserAction.Shuffle;
                            confirmationString = "Are you sure you want to shuffle? (Y/N): ";
                        }
                        break;
                    
                    case ConsoleKey.R:

                        if (confirmationPending)
                        {
                            confirmationString = "Invalid key";
                            confirmationPending = false;
                        }
                        else
                        {
                            confirmationPending = true;
                            lastUserAction = UserAction.Reset;
                            confirmationString = "Are you sure you want to reset? (Y/N): ";
                        }

                        break;
                    
                    case ConsoleKey.Q:

                        if (confirmationPending)
                        {
                            confirmationString = "Invalid key";
                            confirmationPending = false;
                        }
                        else
                        {
                            confirmationPending = true;
                            lastUserAction = UserAction.Quit;
                            confirmationString = "Are you sure you want to exit? (Y/N): ";
                        }
                        
                        break;

                    case ConsoleKey.Y:

                        if (confirmationPending)
                        {
                            switch (lastUserAction)
                            {
                                case UserAction.Shuffle:
                                    engine.Shuffle();
                                    //card = GetCard(GetCardCode(state.card.number), GetSuitCode(state.card.suit));
                                    break;
                                case UserAction.Reset:
                                    engine.Reset();
                                    cardString = GetClosedCardFrame();
                                    break;
                                case UserAction.Quit:
                                    Environment.Exit(0);
                                    break;
                            }
                            confirmationString = "";
                        }
                        else
                        {
                            confirmationString = "Invalid key";
                        }
                        confirmationPending = false;
                        break;

                    case ConsoleKey.N:

                        if (confirmationPending)
                        {
                            switch (lastUserAction)
                            {
                                case UserAction.Shuffle:
                                case UserAction.Reset:
                                case UserAction.Quit:
                                    confirmationString = "";
                                    break;
                            }
                        }
                        else
                        {
                            confirmationString = "Invalid key";
                        }
                        confirmationPending = false;
                        break;

                    default:
                        confirmationString = "Invalid key";
                        confirmationPending = false;
                        break;
                   
                }
                printFrame();
            }
        }
    }
}
