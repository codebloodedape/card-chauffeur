namespace RandomCardGenerator
{
    public class Card
    {
        public int number;
        public Suit suit;

        public void Copy(Card card)
        {
            number = card.number;
            suit = card.suit;
        }
    }

    public enum Suit
    {
        Spade,
        Club,
        Heart,
        Diamond,
    }
}
