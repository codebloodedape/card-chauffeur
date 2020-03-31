using CardChauffeur.RandomCardGenerator.StateManagement;
using CardChauffeur.WindowsConsole.StateManagement;

namespace CardChauffeur.RandomCardGenerator
{
    class Engine
    {
        private Deck deck;
        internal Engine()
        {
            deck = new Deck();
            GameState.Reset();
        }

        internal State Play()
        {
            Card card = deck.Draw();
            GameState.UpdateState(card);
            return GameState.GetCurrentState();
        }

        internal State Shuffle()
        {
            deck.Shuffle();
            return GameState.GetCurrentState();
        }

        internal State Reset()
        {
            deck.Reset();
            GameState.Reset();
            return GameState.GetCurrentState();
        }

        internal State GetCurrentState()
        {
            return GameState.GetCurrentState();
        }
    }
}
