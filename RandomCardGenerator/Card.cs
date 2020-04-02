namespace RandomCardGenerator
{
    /// <summary>
    /// Card class containing the card details
    /// </summary>
    public class Card
    {
        public int number;
        public Suit suit;
    }

    /// <summary>
    /// Enum for suit of the card
    /// </summary>
    public enum Suit
    {
        Spade,
        Club,
        Heart,
        Diamond,
    }
}
