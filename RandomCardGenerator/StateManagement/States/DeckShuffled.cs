using System;
using System.Collections.Generic;
using System.Text;

namespace RandomCardGenerator.StateManagement.States
{
    class DeckShuffled : State
    {
        StateManager context;
        public DeckShuffled(StateManager context)
        {
            this.context = context;
        }
        public Card Draw()
        {
            Card card = context.deck.Draw();
            context.SetState(context.cardDrewState);
            return card;
        }

        public void GameReset()
        {
            context.deck.Reset();
            context.SetState(context.resetState);
        }

        public void Shuffle()
        {
            context.deck.Shuffle();
            // No need to set state
        }
    }
}
