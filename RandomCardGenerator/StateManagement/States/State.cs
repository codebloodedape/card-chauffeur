using System;
using System.Collections.Generic;
using System.Text;

namespace RandomCardGenerator.StateManagement.States
{
    interface State
    {
        void GameReset();

        Card Draw();

        void Shuffle();
    }
}
