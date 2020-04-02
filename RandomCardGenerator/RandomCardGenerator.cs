using RandomCardGenerator.StateManagement;
using Logger;

namespace RandomCardGenerator
{
    /// <summary>
    /// This class manages the requests from the user interface and performs various operations on deck
    /// and the state of the application.
    /// </summary>
    public class Engine
    {
        private Deck deck;
        private StateManager statemanager;

        public void Start()
        {
            Logger.Logger.Log("Starting the game engine");
            deck = new Deck();
            statemanager = new StateManager(deck);
        }

        /// <summary>
        /// Draws the topmost card in the deck.
        /// </summary>
        /// <returns>Returns Card object. Returns null if all the cards are drawn</returns>
        public Card Draw()
        {
            Logger.Logger.Log("Drawing a card");
            return statemanager.Draw();
        }

        public void Shuffle()
        {
            Logger.Logger.Log("Shuffling the deck");
            statemanager.Shuffle();
        }

        public void Reset()
        {
            Logger.Logger.Log("Resetting the game");
            statemanager.Reset();
        }
    }
}
