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
            Logger.Logger.Log("Transitioning from DeckShuffled state to CardDrew state");
            context.currentState = (stateManager.cardDrewState);
            return card;
        }

        public void GameReset()
        {
            context.deck.Reset();
            Logger.Logger.Log("Transitioning from DeckShuffled state to Reset state");
            context.currentState = (stateManager.resetState);
        }

        public void Shuffle()
        {
            Logger.Logger.Log("Reshuffling");
            context.deck.Shuffle();
            // No need to reassign the current state here.
        }
    }
}
