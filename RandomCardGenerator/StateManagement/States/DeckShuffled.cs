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
        StateObject context;
        StateManager stateManager;
        public DeckShuffled(StateManager stateManager, StateObject context)
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

        public void GameReset()
        {
            context.deck.Reset();
            context.currentState = (stateManager.resetState);
        }

        public void Shuffle()
        {
            context.deck.Shuffle();
            // No need to reassign the current state here.
        }
    }
}
