namespace UpperHand.Domain.Models
{

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UpperHand.Domain.Enums;

    public class HandModel
    {
        /*
         * Hand is a rich model since we need to know state and
         * need to be able to interact with state
         */

        /*
         * Private members:
         * Keep a list of cards that are in our hand
         * Keep a private instance of Random, so that we can deal random hands */
        private readonly List<CardModel> _cards;
        private static Random _random;

        /*
         * Publicly accessible state:
         * Make cards enumerable since we only want to serve them as an iterable 
         * collection
         */
        public IEnumerable<CardModel> Cards { get { return _cards; } }

        /*
         * Set cards and random to instances
         */
        public HandModel() {
            _cards = new List<CardModel>();
            _random = new Random();
        }

        /* State validation:
         * What state would we typically ask the Hand model to validate?
         * We want to be able to test that the Hand model knows what hand
         * it has
         */
        public bool HasCard(CardModel card)
        {
            return _cards.Any(c => c.Suit == card.Suit && c.Value == card.Value);
        }

        public bool HasDuplicates()
        {
            return _cards.GroupBy(c => new { c.Suit, c.Value }).Count(g => g.Count() == 2) >= 1;
        }
        /*  
         * HAND STRENGTH:
         * Return early by strongest to weakest
         */
        public bool HasRoyalFlush()
        {
            /* 
             * ROYAL FLUSH: 
             * Flush where all face values are greater than 9
             * No need to sort, since the suits are all the same
             * implies there are no duplicate face values.
             */
            return HasFlush() && _cards.All(c => c.Value > CardValue.Nine);
        }
        public bool HasStraightFlush()
        {
            /* 
             * STRAIGHT FLUSH: 
             * Flush where face values are also sequential but not all 
             * face values are greater than 9  
             */
            return HasFlush() && HasStraight();
        }
        public bool HasFourOfKind()
        {
            /* 
             * FOUR OF A KIND: 
             * Four cards in the hand share the same face value. 
             * To determine this:
             * Group Cards by face value
             * Check to see if any groups have a count of 4
             */
            return _cards.GroupBy(c => c.Value).Any(g => g.Count() == 4);
        }
        public bool HasFullHouse()
        {
            /* 
             * FULL HOUSE: A hand with three of a kind and a pair 
             */
            return HasThreeOfKind() && HasPair();
        }
        public bool HasFlush()
        {
            /* 
             * FLUSH: 
             * 5 non-sequential cards that share the same suit 
             * To determine this:
             * Check that all cards match the suit of the first card
             * but the hand is not a Royal or Straight flush
             */
            return _cards.All(c => _cards.First().Suit == c.Suit);
        }
        public bool HasStraight()
        {
            /* 
             * STRAIGHT: 
             * 5 sequential cards that do not share the same suit 
             * One of the trickier ones to determine
             * To determin this:
             * Group cards by face value. 
             * Make sure that the count of the groups = the hand count (5)
             * This means there are no cards that share the same value
             * Subtract the lowest face value from the highest and make sure the
             * difference equals 4
             */
            return _cards
                  .GroupBy(c => c.Value)
                  .Count() == _cards.Count()
                  && _cards.Max(c => c.Value) - _cards.Min(c => c.Value) == 4;
        }
        public bool HasThreeOfKind()
        {
            /* 
             * THREE OF A KIND: 
             * Three cards with the same face value 
             * To determine this:
             * Group cards by their face value
             * Check to see if any group count equals 3
             */
            return _cards.GroupBy(c => c.Value).Any(g => g.Count() == 3);
        }
        public bool HasTwoPair()
        {
            /* 
             * TWO PAIR: 
             * Two sets of 2 cards with the same face value 
             * To determine this:
             * Group cards by face value
             * Check if the count of groups with 2 equals 2
             */
            return _cards.GroupBy(c => c.Value).Count(g => g.Count() == 2) == 2;
        }
        public bool HasPair()
        {
            /* 
             * PAIR: 
             * One set of 2 cards with the same face value 
             * To determine this:
             * Group cards by face value
             * Check if the count of groups with 2 equals 1
             */
            return _cards.GroupBy(c => c.Value).Count(g => g.Count() == 2) == 1;
        }

        /* 
         * What should we be able to ask the Hand for?
         */
        public CardModel GetHighCard() {
            return _cards.OrderBy(c => c.Value).Last();
        }

        public HandStrength GetHandStrength()
        {
            return
                HasRoyalFlush() ? HandStrength.RoyalFlush
                : HasStraightFlush() ? HandStrength.StraightFlush
                : HasFourOfKind() ? HandStrength.FourOfKind
                : HasFullHouse() ? HandStrength.FullHouse
                : HasFlush() ? HandStrength.Flush
                : HasStraight() ? HandStrength.Straight
                : HasThreeOfKind() ? HandStrength.ThreeOfKind
                : HasTwoPair() ? HandStrength.TwoPair
                : HasPair() ? HandStrength.Pair
                : HandStrength.HighCard;
        }

        public void AddCardToHand(CardModel card) { 
            /* Check to see if we already have the card being added */
            
            if(HasCard(card))
            {
                throw new InvalidOperationException("Cannot add duplicate card.");
            }

            _cards.Add(card);

        }
        /* 
         * This is our main action. 
         * This is what happens when people issue a RequestDealtHand command.
         * Ideally, this would raise a HandDealtEvent which we would track for
         * analytics or machine learning possibilities.
         */
        public HandModel DealHand() {
            Array suits = Enum.GetValues(typeof(CardSuit));
            Array values = Enum.GetValues(typeof(CardValue));

            while (_cards.Count() < 5)
            {
                var suit = (CardSuit)suits.GetValue(_random.Next(suits.Length));
                var value = (CardValue)values.GetValue(_random.Next(values.Length));

                var card = new CardModel(suit, value);

                if (!HasCard(card))
                {
                    AddCardToHand(card);
                }
                else
                {
                    DealHand();
                }
            }

            /* 
             * Model should return itself since it is a rich model
             * and contains all state and behavior. If we were to 
             * raise domain events, we would raise one before returning.
             */
            return this;
        }

    }
}
