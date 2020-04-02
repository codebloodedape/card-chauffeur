using System;
using System.Collections.Generic;
using System.Text;

namespace RandomCardGenerator.StateManagement.States
{
    /// <summary>
    /// State representing a card has been drew.
    /// </summary>
    class CardDrew : IState
    {
        StateObject context;
        StateManager stateManager;
        public CardDrew(StateManager stateManager, StateObject context)
        {
            this.stateManager = stateManager;
            this.context = context;
        }
        public Card Draw()
        {
            Card card = context.deck.Draw();

            if (card == null)
            {
                // Deck is empty.
                Logger.Logger.Log("Deck is empty. Transitioning from CardDrew state to DeckIsEmpty state");
                context.currentState = (stateManager.deckIsEmptyState);
            }
            // else, no need to reassign the current state here.

            return card;
        }

        public void GameReset()
        {
            context.deck.Reset();
            Logger.Logger.Log("Transitioning from CardDrew state to Reset state");
            context.currentState = (stateManager.resetState);
        }

        public void Shuffle()
        {
            context.deck.Shuffle();
            Logger.Logger.Log("Transitioning from CardDrew state to DeckShuffled state");
            context.currentState = (stateManager.deckShuffledState);
        }
    }
}
