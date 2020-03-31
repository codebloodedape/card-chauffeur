using RandomCardGenerator;
using RandomCardGenerator.StateManagement;
using System;
using System.Collections.Generic;

namespace TestApplication
{
    class Program
    {
        //private Engine engine;

        static void Main(string[] args)
        {
            Console.WriteLine("Test");
            CardDrawTest();
        }

        internal static void CardDrawTest()
        {
            Engine engine = new Engine();
            engine.Start();
            List<string> cards = new List<string>();
            for (int i = 0; i < 52; i++)
            {
                State state = engine.Draw();
                string item = state.card.number + "_" + state.card.suit.ToString();
                Console.WriteLine("Adding " + item);
                if (!cards.Contains(item))
                    cards.Add(item);
                else
                    break;
            }
            Console.WriteLine("total cards: " + cards.Count);
            string result = cards.Count == 52 ? ("Test successfull!") : ("Test Failed");
            Console.WriteLine(result);
        }
    }
}
