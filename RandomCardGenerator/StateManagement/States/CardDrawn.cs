using System;
using System.Collections.Generic;
using System.Text;

namespace RandomCardGenerator.StateManagement.States
{
    /// <summary>
    /// State representing a card has been drew.
    /// </summary>
    class CardDrawn : IState
    {
        StateManager stateManager;
        public CardDrawn(StateManager stateManager)
        {
            this.stateManager = stateManager;
        }

        /// <summary>
        /// State transistion: CARD IS DRAWN -> CARD IS DRAWN
        /// </summary>
        /// <returns></returns>
        public Card Draw()
        {
            Card card = stateManager.deck.Draw();

            if (card == null)
            {
                // Deck is empty.
                Logger.Logger.Log("Deck is empty. Transitioning from CardDrew state to DeckIsEmpty state");
                stateManager.stateObject.currentState = EStates.DECKISEMPTY;
            }
            // else, no need to reassign the current state here.

            return card;
        }

        /// <summary>
        /// State transistion: CARD IS DRAWN -> RESET
        /// </summary>
        public void GameReset()
        {
            stateManager.deck.Reset();
            Logger.Logger.Log("Transitioning from CardDrew state to Reset state");
            stateManager.stateObject.currentState = EStates.RESET;
        }

        /// <summary>
        /// State transistion: CARD IS DRAWN -> DECK SHUFFLED
        /// </summary>
        public void Shuffle()
        {
            stateManager.deck.Shuffle();
            Logger.Logger.Log("Transitioning from CardDrew state to DeckShuffled state");
            stateManager.stateObject.currentState = EStates.DECKISSHUFFLED;
        }
    }
}
