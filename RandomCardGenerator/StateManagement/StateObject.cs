using RandomCardGenerator.StateManagement.States;
using System;
using System.Collections.Generic;
using System.Text;

namespace RandomCardGenerator.StateManagement
{
    /// <summary>
    /// This holds the state values at a given state. 
    /// For the scope of the appication, it holdes the current cards stack(deck) and the current state's object
    /// </summary>
    class StateObject
    {
        internal Deck deck;
        internal IState currentState;
    }
}
