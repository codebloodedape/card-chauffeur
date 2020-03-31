using System;
using System.Collections.Generic;
using System.Text;

namespace RandomCardGenerator.StateManagement.States
{
    class CardDrew : State
    {
        StateManager context;
        public CardDrew(StateManager context)
        {
            this.context = context;
        }
        public Card Draw()
        {
            Card card = context.deck.Draw();

            if (card == null)
            {
                context.SetState(context.deckIsEmptyState);
            }
            // else, no need to change the state.

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
            context.SetState(context.deckShuffledState);
        }
    }
}
