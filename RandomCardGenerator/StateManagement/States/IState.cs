using System;
using System.Collections.Generic;
using System.Text;

namespace RandomCardGenerator.StateManagement.States
{
    /// <summary>
    /// Interface for the states
    /// </summary>
    interface IState
    {
        void GameReset();

        Card Draw();

        void Shuffle();
    }
}
