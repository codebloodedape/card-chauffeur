using System;

namespace CardChauffeur.RandomCardGenerator
{
    class Deck
    {
        private Card[] cardStack;

        private void Sort()
        {
            for (int i = 1; i <= 52; i++)
            {
                Card card = new Card();
                int determinant = ((i - 1) / 13) + 1;
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
                cardStack[i - 1] = card;
            }
        }
        internal Deck()
        {
            Reset();
        }

        internal Card Draw()
        {
            Card card = cardStack[cardStack.Length - 1];
            Array.Resize(ref cardStack, cardStack.Length - 1);
            return card;
        }

        internal void Reset()
        {
            cardStack = new Card[52];
            Sort();
            Shuffle();
        }

        internal void Shuffle()
        {
            Random random = new Random();
            
            int n = cardStack.Length;
            while (n > 1)
            {
                int k = random.Next(n--);
                Card temp = cardStack[n];
                cardStack[n] = cardStack[k];
                cardStack[k] = temp;
            }

        }
        
    }
}
