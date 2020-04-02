using System;
using System.Collections.Generic;
using System.Text;

namespace RandomCardGenerator.StateManagement
{
    /// <summary>
    /// Enum representing the states of the game
    /// </summary>
    public enum EStates
    {
        CARDDRAWN,
        DECKISEMPTY,
        DECKISSHUFFLED,
        RESET
    }
}
