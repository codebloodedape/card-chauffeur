using RandomCardGenerator.StateManagement.States;
using System;

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

        internal StateObject stateObject;
        internal Deck deck;

        private IState GetCurrentState(EStates state)
        {
            switch(state)
            {
                case EStates.CARDDREW:
                    return cardDrewState;
                case EStates.DECKISEMPTY:
                    return deckIsEmptyState;
                case EStates.DECKISSHUFFLED:
                    return deckShuffledState;
                case EStates.RESET:
                    return resetState;
                default:
                    return null;
            }
        }

        internal StateManager(StateObject stateObject)
        {
            Logger.Logger.Log("Initialising State Manager");
            
            //stateObject = new StateObject();

            // Initialising all the state objects
            Logger.Logger.Log("Initialising all the state objects");
            resetState = new Reset(this);
            cardDrewState = new CardDrew(this);
            deckShuffledState = new DeckShuffled(this);
            deckIsEmptyState = new DeckIsEmpty(this);

            // Updating the StateObject with inital values, i.e., newly reset deck and initial current state are "RESET"
            Logger.Logger.Log("Updating the StateObject with inital values");
            this.stateObject = stateObject;
            deck = new Deck(this.stateObject);
            //this.deck = deck;
            //stateObject.deck = deck.cardStack;
            //stateObject.currentState = EStates.RESET;
        }

        /// <summary>
        /// Request for state trasistion on drawing a card
        /// </summary>
        /// <returns>Returns the cards topmost card in the deck. Returns NULL if the deck is empty</returns>
        internal Card Draw()
        {
            try
            {
                return GetCurrentState(stateObject.currentState).Draw();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
                return null;
            }
        }

        /// <summary>
        /// Request for state trasistion on resetting of game
        /// </summary>
        internal void Reset()
        {
            try
            {
                GetCurrentState(stateObject.currentState).GameReset();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
            }
        }

        /// <summary>
        /// Request for state trasistion on shuffling the deck
        /// </summary>
        internal void Shuffle()
        {
            try
            {
                GetCurrentState(stateObject.currentState).Shuffle();
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
            }
        }

        internal bool Save()
        {
            return Logger.Recovery.Save(stateObject);
        }

        internal bool Recover()
        {
            stateObject = Logger.Recovery.Recover<StateObject>();
            deck.Recover(stateObject);
            return stateObject == null ? false : true;
        }
    }
}
