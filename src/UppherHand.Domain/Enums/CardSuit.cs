using System.Runtime.Serialization;

namespace UpperHand.Domain.Enums
{
    public enum CardSuit
    {
        /* 
         * Using EnumMember to provide ubiquitous language
         * to client and system language to system, 
         * e.g. "Ace of Hearts" vs "Ace of Heart"
         */
        [EnumMember(Value = "Clubs")]
        Club,
        [EnumMember(Value = "Diamonds")]
        Diamond,
        [EnumMember(Value = "Hearts")]
        Heart,
        [EnumMember(Value = "Spades")]
        Spade
    }
}
