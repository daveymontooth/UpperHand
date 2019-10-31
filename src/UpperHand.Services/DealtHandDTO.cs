namespace UpperHand.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using UpperHand.Domain.Enums;
    using UpperHand.Domain.Models;

    public class DealtHandDTO
    {

        public class CardDTO
        {
            public string Suit { get; }
            public string Value { get; }

            public CardDTO(string suit, string value)
            {
                Suit = suit;
                Value = value;
            }
        }

        public class StrengthDTO
        {
            public string Name { get; }
            public int Value { get; }

            public StrengthDTO(string name, int value)
            {
                Name = name;
                Value = value;
            }
        }

        public HandStrength Strength { get; }
        public List<CardDTO> Cards { get; } = new List<CardDTO>();
        public List<StrengthDTO> HandStrengths { get; } = new List<StrengthDTO>();

        public DealtHandDTO(IEnumerable<CardModel> cards, HandStrength strength)
        {
            foreach (CardModel card in cards)
            {
                Cards.Add(new CardDTO(card.Suit.ToString(), card.Value.ToString()));
            }

            foreach (HandStrength r in Enum.GetValues(typeof(HandStrength)))
            {
                var type = r.GetType();
                var memberInfo = type.GetMember(r.ToString());
                var attr = memberInfo[0].GetCustomAttributes(false).OfType<EnumMemberAttribute>().FirstOrDefault();
                var name = attr != null ? attr.Value : r.ToString();

                HandStrengths.Add(new StrengthDTO(name, (int)r));
            }

            Strength = strength;
        }
    }
}
