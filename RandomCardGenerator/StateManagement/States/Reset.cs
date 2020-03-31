using System;
using System.Collections.Generic;
using System.Text;

namespace RandomCardGenerator.StateManagement.States
{
    class Reset : State
    {
        StateManager context;
        public Reset(StateManager context)
        {
            this.context = context;
        }

        public Card Draw()
        {
            Card card = context.deck.Draw();
            context.SetState(context.cardDrewState);
            return card;
        }

        public void Shuffle()
        {
            context.deck.Shuffle();
            context.SetState(context.deckShuffledState);
        }

        public void GameReset()
        {
            context.deck.Reset();
            // No need to change the state
        }
    }
}
