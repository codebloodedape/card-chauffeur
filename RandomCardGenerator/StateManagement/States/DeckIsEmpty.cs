using System;
using System.Collections.Generic;
using System.Text;

namespace RandomCardGenerator.StateManagement.States
{
    /// <summary>
    /// State representing that the deck is empty
    /// </summary>
    class DeckIsEmpty : IState
    {
        StateManager stateManager;

        /// <summary>
        /// State transistion: DECK IS EMPTY -> DECK IS EMPTY
        /// </summary>
        /// <param name="stateManager"></param>
        public DeckIsEmpty(StateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        /// <summary>
        /// State transistion: DECk IS EMPTY -> CARD DRAWN
        /// </summary>
        /// <returns></returns>
        public Card Draw()
        {
            // Cannot draw from an empty deck!
            Logger.Logger.Log("Cannot draw from an empty deck");
            return null;
        }

        /// <summary>
        /// State transistion: DECK IS EMPTY -> RESET
        /// </summary>
        public void GameReset()
        {
            stateManager.deck.Reset();
            Logger.Logger.Log("Transitioning from DeckIsEmpty state to Reset state");
            stateManager.stateObject.currentState = EStates.RESET;
        }

        /// <summary>
        /// State transistion: DECK IS EMPTY -> DECK SHUFFLED
        /// </summary>
        public void Shuffle()
        {
            // Cannot shuffle an empty deck!
            Logger.Logger.Log("Cannot shuffle an empty deck");
        }
    }
}
