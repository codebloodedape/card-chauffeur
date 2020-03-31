using CardChauffeur.RandomCardGenerator;
using CardChauffeur.RandomCardGenerator.StateManagement;

namespace CardChauffeur.WindowsConsole.StateManagement
{
    internal static class GameState
    {
        private static State currentState, previousState;

        internal static State GetCurrentState()
        {
            return currentState;
        }
        internal static State GetPreviousState()
        {
            return previousState;
        }

        internal static void UpdateState(Card card)
        {
            previousState.Copy(currentState);

            currentState.card = card;
            currentState.reset = false;
        }

        internal static void Reset()
        {
            if(previousState == null)
            {
                previousState = new State();
            }
            if (currentState != null)
            {
                previousState.Copy(currentState);
            }
            else
            {
                currentState = new State();
            }
            
            currentState.card = null;
            currentState.reset = true;
        }
    }   
}
