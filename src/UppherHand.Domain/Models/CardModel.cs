namespace UpperHand.Domain.Models
{
    using UpperHand.Domain.Enums;

    public class CardModel
    {
        /*
         * Card is intentionally anemic since a card doesn't do 
         * anything other than display it's value.
         * Could arguably move this into the Hand model as a value object
         */
        private CardSuit _suit { get; }
        private CardValue _value { get; }

        public CardSuit Suit { get { return _suit; } }
        public CardValue Value { get { return _value; } }

        public CardModel(CardSuit suit, CardValue value)
        {
            _suit = suit;
            _value = value;
        }
    }
}
