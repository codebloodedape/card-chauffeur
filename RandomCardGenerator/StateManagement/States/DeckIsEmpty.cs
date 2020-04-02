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
        StateManager stateManager;
        public DeckIsEmpty(StateManager stateManager)
        {
            this.stateManager = stateManager;
        }
        public Card Draw()
        {
            // Cannot draw from an empty deck!
            Logger.Logger.Log("Cannot draw from an empty deck");
            return null;
        }

        public void GameReset()
        {
            stateManager.deck.Reset();
            Logger.Logger.Log("Transitioning from DeckIsEmpty state to Reset state");
            stateManager.stateObject.currentState = EStates.RESET;
        }

        public void Shuffle()
        {
            // Cannot shuffle an empty deck!
            Logger.Logger.Log("Cannot shuffle an empty deck");
        }
    }
}
