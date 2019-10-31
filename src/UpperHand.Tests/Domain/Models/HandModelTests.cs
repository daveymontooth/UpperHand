namespace UpperHand.Tests.Domain.Models
{
    using System;
    using System.Linq;
    using UpperHand.Domain.Enums;
    using UpperHand.Domain.Models;
    using Xunit;

    public class HandModelTests
    {
        private readonly HandModel _handModel;

        public HandModelTests() {
            /* These could be injected in but that adds to the complexity of this example */
            _handModel = new HandModel();
        }

        [Fact]
        public void CanAddCardToHandModel() {
            var cardModel = new CardModel(CardSuit.Club, CardValue.Ace);

            _handModel.AddCardToHand(cardModel);

            Assert.Equal(_handModel.Cards.First(), cardModel);
        }

        [Fact]
        public void CanAddMultipleCardsToHandModel()
        {
            var expectedCount = 5;

            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Ace));
            _handModel.AddCardToHand(new CardModel(CardSuit.Diamond, CardValue.Eight));
            _handModel.AddCardToHand(new CardModel(CardSuit.Heart, CardValue.Five));
            _handModel.AddCardToHand(new CardModel(CardSuit.Spade, CardValue.Four));
            _handModel.AddCardToHand(new CardModel(CardSuit.Spade, CardValue.Six));

            Assert.Equal(expectedCount, _handModel.Cards.ToList().Count);
        }

        [Fact]
        public void CanDetectRoyalFlush()
        {
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Ten));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Jack));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Queen));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.King));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Ace));

            Assert.True(_handModel.HasRoyalFlush());
        }

        [Fact]
        public void CanDetectStraightFlush()
        {
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Five));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Six));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Seven));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Eight));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Nine));

            Assert.True(_handModel.HasStraightFlush());
        }

        [Fact]
        public void CanDetectFourOfKind()
        {
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Five));
            _handModel.AddCardToHand(new CardModel(CardSuit.Heart, CardValue.Five));
            _handModel.AddCardToHand(new CardModel(CardSuit.Spade, CardValue.Five));
            _handModel.AddCardToHand(new CardModel(CardSuit.Diamond, CardValue.Five));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Nine));

            Assert.True(_handModel.HasFourOfKind());
        }

        [Fact]
        public void CanDetectFullHouse()
        {
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Five));
            _handModel.AddCardToHand(new CardModel(CardSuit.Heart, CardValue.Five));
            _handModel.AddCardToHand(new CardModel(CardSuit.Spade, CardValue.Five));
            _handModel.AddCardToHand(new CardModel(CardSuit.Diamond, CardValue.Four));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Four));

            Assert.True(_handModel.HasFullHouse());
        }

        [Fact]
        public void CanDetectFlush()
        {

            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Two));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Ace));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Nine));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Seven));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Jack));

            Assert.True(_handModel.HasFlush());

        }

        [Fact]
        public void CanDetectNonFlush()
        {

            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Two));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Ace));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Nine));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Seven));
            _handModel.AddCardToHand(new CardModel(CardSuit.Heart, CardValue.Jack));

            Assert.False(_handModel.HasFlush());

        }

        [Fact]
        public void CanDetectStraight()
        {
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Two));
            _handModel.AddCardToHand(new CardModel(CardSuit.Diamond, CardValue.Four));
            _handModel.AddCardToHand(new CardModel(CardSuit.Diamond, CardValue.Five));
            _handModel.AddCardToHand(new CardModel(CardSuit.Spade, CardValue.Three));
            _handModel.AddCardToHand(new CardModel(CardSuit.Heart, CardValue.Six));

            Assert.True(_handModel.HasStraight());
        }

        [Fact]
        public void CanDetectNonStraight()
        {
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Two));
            _handModel.AddCardToHand(new CardModel(CardSuit.Diamond, CardValue.Four));
            _handModel.AddCardToHand(new CardModel(CardSuit.Diamond, CardValue.Five));
            _handModel.AddCardToHand(new CardModel(CardSuit.Spade, CardValue.Three));
            _handModel.AddCardToHand(new CardModel(CardSuit.Heart, CardValue.Seven));

            Assert.False(_handModel.HasStraight());
        }

        [Fact]
        public void CanDetectNonStraightByDuplicateValue()
        {
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Two));
            _handModel.AddCardToHand(new CardModel(CardSuit.Diamond, CardValue.Two));
            _handModel.AddCardToHand(new CardModel(CardSuit.Diamond, CardValue.Five));
            _handModel.AddCardToHand(new CardModel(CardSuit.Spade, CardValue.Three));
            _handModel.AddCardToHand(new CardModel(CardSuit.Heart, CardValue.Six));

            Assert.False(_handModel.HasStraight());
        }

        [Fact]
        public void CanDetectThreeOfKind()
        {

            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Two));
            _handModel.AddCardToHand(new CardModel(CardSuit.Heart, CardValue.Two));
            _handModel.AddCardToHand(new CardModel(CardSuit.Spade, CardValue.Two));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Seven));
            _handModel.AddCardToHand(new CardModel(CardSuit.Heart, CardValue.Jack));

            Assert.False(_handModel.HasFlush());

        }

        [Fact]
        public void CanDetectTwoPair()
        {

            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Two));
            _handModel.AddCardToHand(new CardModel(CardSuit.Heart, CardValue.Two));
            _handModel.AddCardToHand(new CardModel(CardSuit.Spade, CardValue.Three));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Three));
            _handModel.AddCardToHand(new CardModel(CardSuit.Heart, CardValue.Jack));

            Assert.False(_handModel.HasFlush());

        }

        [Fact]
        public void CanDetectPair()
        {

            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Two));
            _handModel.AddCardToHand(new CardModel(CardSuit.Heart, CardValue.Two));
            _handModel.AddCardToHand(new CardModel(CardSuit.Spade, CardValue.Three));
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Four));
            _handModel.AddCardToHand(new CardModel(CardSuit.Heart, CardValue.Jack));

            Assert.False(_handModel.HasFlush());

        }

        [Fact]
        public void CanDetectHighCard()
        {
            _handModel.AddCardToHand(new CardModel(CardSuit.Club, CardValue.Ace));
            _handModel.AddCardToHand(new CardModel(CardSuit.Diamond, CardValue.Five));
            _handModel.AddCardToHand(new CardModel(CardSuit.Diamond, CardValue.Queen));
            _handModel.AddCardToHand(new CardModel(CardSuit.Spade, CardValue.Four));
            _handModel.AddCardToHand(new CardModel(CardSuit.Diamond, CardValue.Nine));

            Assert.Equal(CardValue.Ace, _handModel.GetHighCard().Value);
        }

        [Fact]
        public void CanDealAHand()
        {
            _handModel.DealHand();

            Assert.True(_handModel.Cards.Count() > 0);
        }

        [Fact]
        public void ValidateDuplicateCardException() {

            var ex = Assert.Throws<InvalidOperationException>(
                () => {
                    var cardModel = new CardModel(CardSuit.Club, CardValue.Ace);
                    _handModel.AddCardToHand(cardModel);
                    _handModel.AddCardToHand(cardModel);
                }
            );

            /* Should move strings like this to a constants file */
            Assert.Equal("Cannot add duplicate card.", ex.Message);

        }

        [Fact]
        public void ValidateDealtHandHasFiveCards()
        {
            _handModel.DealHand();

            Assert.True(_handModel.Cards.Count() == 5);
        }

        [Fact]
        public void CanGetHandStrengthFromDealtHand()
        {
            _handModel.DealHand();
            var strength = _handModel.GetHandStrength();

            Assert.NotNull(strength);
        }
    }
}
