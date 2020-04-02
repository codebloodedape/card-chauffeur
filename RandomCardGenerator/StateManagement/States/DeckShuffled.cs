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
        StateManager stateManager;
        public DeckShuffled(StateManager stateManager)
        {
            this.stateManager = stateManager;
        }
        public Card Draw()
        {
            Card card = stateManager.deck.Draw();
            Logger.Logger.Log("Transitioning from DeckShuffled state to CardDrew state");
            stateManager.stateObject.currentState = EStates.CARDDREW;
            return card;
        }

        public void GameReset()
        {
            stateManager.deck.Reset();
            Logger.Logger.Log("Transitioning from DeckShuffled state to Reset state");
            stateManager.stateObject.currentState = EStates.RESET;
        }

        public void Shuffle()
        {
            Logger.Logger.Log("Reshuffling");
            stateManager.deck.Shuffle();
            // No need to reassign the current state here.
        }
    }
}
