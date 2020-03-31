namespace CardChauffeur.RandomCardGenerator.StateManagement
{
    class State
    {
        internal Card card;
        internal bool reset;

        internal void Copy(State state)
        {
            card = state.card;
            reset = state.reset;
        }
    }
}
