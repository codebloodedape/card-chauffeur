using RandomCardGenerator.StateManagement;

namespace RandomCardGenerator
{
    /// <summary>
    /// This class manages the requests from the user interface and performs various operations on deck
    /// and the state of the application.
    /// </summary>
    public class Engine
    {
        private StateObject stateObject;
        private StateManager statemanager;

        /// <summary>
        /// Initialises state machine and sets the initial state object
        /// </summary>
        public void Start()
        {
            Logger.Logger.Log("Starting the game engine");
            stateObject = new StateObject();
            statemanager = new StateManager(stateObject);
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

        /// <summary>
        /// Shuffles the deck
        /// </summary>
        public void Shuffle()
        {
            Logger.Logger.Log("Shuffling the deck");
            statemanager.Shuffle();
        }

        /// <summary>
        /// Resets the game
        /// </summary>
        public void Reset()
        {
            Logger.Logger.Log("Resetting the game");
            statemanager.Reset();
        }

        /// <summary>
        /// Saves the state of the game on to a local file
        /// </summary>
        /// <returns></returns>
        public bool Save()
        {
            return statemanager.Save();
        }

        /// <summary>
        /// Recovers the state of the previously saved game
        /// </summary>
        /// <returns></returns>
        public bool Recover()
        {
            return statemanager.Recover();
        }
    }
}
