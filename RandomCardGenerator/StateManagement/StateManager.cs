using RandomCardGenerator.StateManagement.States;

namespace RandomCardGenerator.StateManagement
{
    /// <summary>
    /// Resposibility of State Manager are as follows:
    /// 1. initialise all state objects,
    /// 2. create state representation object with initial values of the deck and current state (which is 'Reset'),
    /// 3. receive requests from the game(Random Card Generator), and
    /// 4. invoke current states methods based on the request
    /// </summary>
    public class StateManager
    {
        internal IState resetState, cardDrewState, deckShuffledState, deckIsEmptyState;

        private StateObject stateObject;

        internal StateManager(Deck deck)
        {
            stateObject = new StateObject();

            // Initialising all the state objects
            resetState = new Reset(this, stateObject);
            cardDrewState = new CardDrew(this, stateObject);
            deckShuffledState = new DeckShuffled(this, stateObject);
            deckIsEmptyState = new DeckIsEmpty(this, stateObject);

            // Updating the StateObject with inital values, i.e., newly reset deck and initial current state are "RESET"
            stateObject.deck = deck;
            stateObject.currentState = resetState;
        }

        /// <summary>
        /// Request for state trasistion on drawing a card
        /// </summary>
        /// <returns>Returns the cards topmost card in the deck. Returns NULL if the deck is empty</returns>
        internal Card Draw()
        {
            return stateObject.currentState.Draw();
        }

        /// <summary>
        /// Request for state trasistion on resetting of game
        /// </summary>
        internal void Reset()
        {
            stateObject.currentState.GameReset();
        }

        /// <summary>
        /// Request for state trasistion on shuffling the deck
        /// </summary>
        internal void Shuffle()
        {
            stateObject.currentState.Shuffle();
        }
    }
}
