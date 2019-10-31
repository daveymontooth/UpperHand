namespace UpperHand.Domain.Enums
{
    using System.Runtime.Serialization;
    public enum HandStrength
    {
        [EnumMember(Value = "High Card")]
        HighCard = 1,
        [EnumMember(Value = "Pair")]
        Pair = 2,
        [EnumMember(Value = "Two Pair")]
        TwoPair = 3,
        [EnumMember(Value = "Three of a Kind")]
        ThreeOfKind = 4,
        [EnumMember(Value = "Straight")]
        Straight = 5,
        [EnumMember(Value = "Flush")]
        Flush = 6,
        [EnumMember(Value = "Full House")]
        FullHouse = 7,
        [EnumMember(Value = "Four of a Kind")]
        FourOfKind = 8,
        [EnumMember(Value = "Straight Flush")]
        StraightFlush = 9,
        [EnumMember(Value = "Royal Flush")]
        RoyalFlush = 10
    }
}
