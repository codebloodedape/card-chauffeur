using RandomCardGenerator.StateManagement;
using System;

namespace RandomCardGenerator
{
    /// <summary>
    /// This is a stack implementaiton of card objects 'cardStack'.
    /// Resposibilities of a deck is to maintain the cards order's integrity, shuffle, reset and draw a card. For the purpose of the
    /// requirement, only drawing the topmost card is allowed at a time.
    /// </summary>
    class Deck
    {
        private StateObject stateObject;

        /// <summary>
        /// This methods will sort the stack of 52 cards to from A-13, K-J based on card suits.
        /// </summary>
        private void Sort()
        {
            Logger.Logger.Log("Sorting the deck");
            try
            {
                for (int i = 1; i <= 52; i++)
                {
                    Card card = new Card();
                    int determinant = ((i - 1) / 13) + 1;   // This variable determines which suit to allocate
                    switch (determinant)
                    {
                        case 1:
                            card.suit = Suit.Club;
                            card.number = i;
                            break;
                        case 2:
                            card.suit = Suit.Diamond;
                            card.number = i - 13;
                            break;
                        case 3:
                            card.suit = Suit.Heart;
                            card.number = i - 26;
                            break;
                        case 4:
                            card.suit = Suit.Spade;
                            card.number = i - 39;
                            break;
                    }
                    stateObject.cardStack[i - 1] = card;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
            }
            
        }

        /// <summary>
        /// Creats stack of 52 cards, sort them, shuffle them and returns the object of Deck
        /// </summary>
        internal Deck(StateObject stateObject)
        {
            this.stateObject = stateObject;
            Reset();
        }

        /// <summary>
        /// Draws the topmost card from the stack. This  cards never appears on the stack until the stack is reset.
        /// </summary>
        /// <returns>Topmost card drawn</returns>
        internal Card Draw()
        {
            Logger.Logger.Log("Drawing a card");
            try
            {
                if (stateObject.cardStack.Length == 0)
                {
                    Logger.Logger.Log("No card found in the deck. Returning null");
                    return null;
                }
                Card card = stateObject.cardStack[stateObject.cardStack.Length - 1];        // Retrieve the last element of the array before removing it.
                Array.Resize(ref stateObject.cardStack, stateObject.cardStack.Length - 1);  // This removes the last element of the array.
                return card;
            }
            catch(Exception ex)
            {
                Logger.Logger.Log(ex);
                Logger.Logger.Log("Returning null.");
                return null;
            }
        }

        /// <summary>
        /// Creats stack of 52 cards, sort them and then shuffle them.
        /// </summary>
        internal void Reset()
        {
            Logger.Logger.Log("Resetting the game.");
            stateObject.cardStack = new Card[52];
            Sort();
            Shuffle();
        }

        /// <summary>
        /// Resets the game to the state object passed
        /// </summary>
        /// <param name="stateObject">State object to reset to</param>
        internal void Reset(StateObject stateObject)
        {
            this.stateObject = stateObject;
        }

        /// <summary>
        /// Shuffles the current stack of cards
        /// </summary>
        internal void Shuffle()
        {
            Logger.Logger.Log("Shuffling the deck");

            try
            {
                Random random = new Random();

                int n = stateObject.cardStack.Length;
                while (n > 1)
                {
                    int k = random.Next(n--);
                    Card temp = stateObject.cardStack[n];
                    stateObject.cardStack[n] = stateObject.cardStack[k];
                    stateObject.cardStack[k] = temp;
                }
            }
            catch (Exception ex)
            {
                Logger.Logger.Log(ex);
            }
            
        }
    }
}
