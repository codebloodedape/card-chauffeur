using System;
using System.Collections.Generic;
using System.Text;

namespace RandomCardGenerator.StateManagement.States
{
    /// <summary>
    /// State representing that the game has been reset.
    /// </summary>
    class Reset : IState
    {
        StateManager stateManager;
        public Reset(StateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        /// <summary>
        /// State transistion: RESET -> CARD DRAW
        /// </summary>
        /// <returns></returns>
        public Card Draw()
        {
            Card card = stateManager.deck.Draw();
            Logger.Logger.Log("Transitioning from Reset state to CardDrew state");
            stateManager.stateObject.currentState = EStates.CARDDRAWN;
            return card;
        }

        /// <summary>
        /// State transistion: RESET -> DECK IS SHUFFLED
        /// </summary>
        public void Shuffle()
        {
            stateManager.deck.Shuffle();
            Logger.Logger.Log("Transitioning from Reset state to DeckShuffled state");
            stateManager.stateObject.currentState = EStates.DECKISSHUFFLED;
        }

        /// <summary>
        /// State transistion: RESET -> RESET
        /// </summary>
        public void GameReset()
        {
            Logger.Logger.Log("Re-resetting");
            stateManager.deck.Reset();
            // No need to reassign the current state here.
        }
    }
}
