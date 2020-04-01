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
        private StateObject context;
        StateManager stateManager;
        public DeckIsEmpty(StateManager stateManager, StateObject context)
        {
            this.stateManager = stateManager;
            this.context = context;
        }
        public Card Draw()
        {
            // Cannot draw from an empty deck!
            return null;
        }

        public void GameReset()
        {
            context.deck.Reset();
            context.currentState = (stateManager.resetState);
        }

        public void Shuffle()
        {
            // Cannot shuffle an empty deck!
        }
    }
}
