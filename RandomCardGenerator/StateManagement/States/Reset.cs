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
            context.currentState = (stateManager.cardDrewState);
            return card;
        }

        public void Shuffle()
        {
            context.deck.Shuffle();
            context.currentState = (stateManager.deckShuffledState);
        }

        public void GameReset()
        {
            context.deck.Reset();
            // No need to reassign the current state here.
        }
    }
}
