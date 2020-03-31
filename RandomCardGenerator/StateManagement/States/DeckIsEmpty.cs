using System;
using System.Collections.Generic;
using System.Text;

namespace RandomCardGenerator.StateManagement.States
{
    class DeckIsEmpty : State
    {
        private StateManager context;
        public DeckIsEmpty(StateManager context)
        {
            this.context = context;
        }
        public Card Draw()
        {
            return null;
        }

        public void GameReset()
        {
            context.deck.Reset();
            context.SetState(context.resetState);
        }

        public void Shuffle()
        {
            // Do nothing
        }
    }
}
