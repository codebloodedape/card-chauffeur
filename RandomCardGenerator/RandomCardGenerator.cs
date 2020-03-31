using RandomCardGenerator.StateManagement;

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
        public Engine()
        { }

        public void Start()
        {
            deck = new Deck();
            statemanager = new StateManager(deck);
        }

        /// <summary>
        /// Draws the topmost card in the deck.
        /// </summary>
        /// <returns>Returns Card object. Returns null if all the cards are drawn</returns>
        public Card Draw()
        {
            return statemanager.Draw();
        }

        public void Shuffle()
        {
            statemanager.Shuffle();
        }

        public void Reset()
        {
            statemanager.Reset();
        }
    }
}
