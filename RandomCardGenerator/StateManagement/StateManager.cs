using RandomCardGenerator.StateManagement.States;

namespace RandomCardGenerator.StateManagement
{
    public class StateManager
    {
        internal Reset resetState;
        internal CardDrew cardDrewState;
        internal DeckShuffled deckShuffledState;
        internal DeckIsEmpty deckIsEmptyState;

        internal Deck deck;

        private State currentState;
        
        internal StateManager(Deck deck)
        {
            resetState = new Reset(this);
            cardDrewState = new CardDrew(this);
            deckShuffledState = new DeckShuffled(this);
            deckIsEmptyState = new DeckIsEmpty(this);

            this.deck = deck;
            currentState = resetState;
        }

        internal Card Draw()
        {
            return currentState.Draw();
        }

        internal void Reset()
        {
            currentState.GameReset();
        }

        internal void Shuffle()
        {
            currentState.Shuffle();
        }

        internal void SetState(State state)
        {
            currentState = state;
        }

        //private static State currentState, previousState;

        //private static void UpdateState(State state)
        //{
        //    previousState.Copy(currentState);
        //    currentState.Copy(state);
        //}

        //internal static State GetCurrentState()
        //{
        //    return currentState;
        //}
        
        //internal static State GetPreviousState()
        //{
        //    return previousState;
        //}

        //internal static void Draw(Card card)
        //{
        //    State state = new State();
        //    state.card.Copy(card);
        //    state.reset = false;
        //    UpdateState(state);
        //}

        //internal static void Reset()
        //{
        //    previousState = new State();
        //    currentState = new State();

        //    currentState.reset = true;
        //}
    }
}
