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
        StateObject context;
        StateManager stateManager;
        public Reset(StateManager stateManager, StateObject context)
        {
            this.stateManager = stateManager;
            this.context = context;
        }

        public Card Draw()
        {
            Card card = context.deck.Draw();
            Logger.Logger.Log("Transitioning from Reset state to CardDrew state");
            context.currentState = (stateManager.cardDrewState);
            return card;
        }

        public void Shuffle()
        {
            context.deck.Shuffle();
            Logger.Logger.Log("Transitioning from Reset state to DeckShuffled state");
            context.currentState = (stateManager.deckShuffledState);
        }

        public void GameReset()
        {
            Logger.Logger.Log("Re-resetting");
            context.deck.Reset();
            // No need to reassign the current state here.
        }
    }
}
