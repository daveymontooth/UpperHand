namespace UpperHand.Tests.Domain.Models
{
    using UpperHand.Domain.Enums;
    using UpperHand.Domain.Models;
    using Xunit;
    public class CardModelTests
    {
        [Fact]
        public void CanCreateCard()
        {
            /* Assert that card is not null */
            var card = new CardModel(CardSuit.Heart, CardValue.Five);
            Assert.NotNull(card);
        }

        [Fact]
        public void CanCreateAceOfSpades()
        {
            /* Assert that we can create a specific card */
            var card = new CardModel(CardSuit.Spade, CardValue.Ace);
            Assert.Equal(CardSuit.Spade, card.Suit);
            Assert.Equal(CardValue.Ace, card.Value);
        }
    }
}
