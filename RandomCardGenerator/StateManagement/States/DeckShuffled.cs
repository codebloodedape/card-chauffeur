using System;
using System.Collections.Generic;
using System.Text;

namespace RandomCardGenerator.StateManagement.States
{
    /// <summary>
    /// State representing that the deck has been shuffled
    /// </summary>
    class DeckShuffled : IState
    {
        StateManager stateManager;

        /// <summary>
        /// State transistion: DECK SHUFFLED -> DECK SHUFFLED
        /// </summary>
        /// <param name="stateManager"></param>
        public DeckShuffled(StateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        /// <summary>
        /// State transistion: DECK SHUFFLED -> CARD DRAWN
        /// </summary>
        /// <returns></returns>
        public Card Draw()
        {
            Card card = stateManager.deck.Draw();
            Logger.Logger.Log("Transitioning from DeckShuffled state to CardDrew state");
            stateManager.stateObject.currentState = EStates.CARDDRAWN;
            return card;
        }

        /// <summary>
        /// State transistion: DECK SHUFFLED -> RESET
        /// </summary>
        public void GameReset()
        {
            stateManager.deck.Reset();
            Logger.Logger.Log("Transitioning from DeckShuffled state to Reset state");
            stateManager.stateObject.currentState = EStates.RESET;
        }

        /// <summary>
        /// State transistion: DECK SHUFFLED -> DECK SHUFFLED
        /// </summary>
        public void Shuffle()
        {
            Logger.Logger.Log("Reshuffling");
            stateManager.deck.Shuffle();
            // No need to reassign the current state here.
        }
    }
}
