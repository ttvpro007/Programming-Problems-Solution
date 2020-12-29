using System;
using System.Collections.Generic;
using System.Linq;

namespace Codewars.Solutions._4_Kyu
{
    ///A famous casino is suddenly faced with a sharp decline of their revenues.They decide to offer Texas hold'em also online. 
    ///Can you help them by writing an algorithm that can rank poker hands?
    ///
    ///Task
    ///Create a poker hand that has a method to compare itself to another poker hand:
    ///
    ///Result PokerHand.CompareWith(PokerHand hand);
    ///    A poker hand has a constructor that accepts a string containing 5 cards:
    ///
    ///PokerHand hand = new PokerHand("KS 2H 5C JD TD");
    ///    The characteristics of the string of cards are:
    ///
    ///Each card consists of two characters, where
    ///The first character is the value of the card: 2, 3, 4, 5, 6, 7, 8, 9, T(en), J(ack), Q(ueen), K(ing), A(ce)
    ///The second character represents the suit: S(pades), H(earts), D(iamonds), C(lubs)
    ///A space is used as card separator between cards
    ///The result of your poker hand compare can be one of these 3 options:
    ///
    ///public enum Result
    ///{
    ///    Win,
    ///    Loss,
    ///    Tie
    ///}
    ///Notes
    ///Apply the Texas Hold'em rules for ranking the cards.
    ///Low aces are NOT valid in this kata.
    ///There is no ranking for the suits.
    ///If you finished this kata, you might want to continue with Sortable Poker Hands

    public enum Result
    {
        Win,
        Loss,
        Tie
    }

    public class PokerHand
    {
        public enum Rank
        {
            RoyalFlush      = 10,
            StraightFlush   = 9,
            FourOfAKind     = 8,
            FullHouse       = 7,
            Flush           = 6,
            Straight        = 5,
            ThreeOfAKind    = 4,
            TwoPair         = 3,
            Pair            = 2,
            HighCard        = 1
        }

        public Rank Rating { get; private set; }
        public List<Card> Hand { get; private set; }

        public PokerHand(string hand)
        {
            Hand = GetCards(hand);
            rankCountLookup = GetRankCountLookup(Hand);
            Rating = Evaluate(Hand);
        }

        public Result CompareWith(PokerHand other)
        {
            Result result = CompareRating(other);
            switch (result)
            {
                case Result.Win:
                case Result.Loss:
                    break;
                case Result.Tie:
                    result = TieBreak(other);
                    break;
            }
            return result;
        }

        private Dictionary<int, int> rankCountLookup;

        private Result CompareRating(PokerHand other)
        {
            if (Rating > other.Rating) return Result.Win;
            else if (Rating < other.Rating) return Result.Loss;
            else return Result.Tie;
        }

        private Result TieBreak(PokerHand other)
        {
            for (int i = 0; i < Hand.Count; i++)
            {
                if (Hand[i].Rank > other.Hand[i].Rank) return Result.Win;
                else if (Hand[i].Rank < other.Hand[i].Rank) return Result.Loss;
            }
            return Result.Tie;
        }

        private List<Card> GetCards(string hand)
        {
            var cards = new List<Card>();
            var splittedHand = hand.Split(' ');
            for (int i = 0; i < splittedHand.Length; i++)
            {
                cards.Add(new Card(splittedHand[i][0], splittedHand[i][1]));
            }
            return OrderByRankAndCount(cards);
        }

        private static List<Card> OrderByRankAndCount(List<Card> cards)
        {
            // order by descending of card rank then group by rank and order by decending of repeated count
            return cards.OrderByDescending(c => c.Rank)
                        .GroupBy(c => c.Rank)
                        .OrderByDescending(c => c.Count())
                        .SelectMany(c => c)
                        .ToList();
        }

        private Rank Evaluate(List<Card> cards)
        {
            bool isStraight = IsStraight(cards);
            bool isFlush = IsFlush(cards);
            bool isRoyal = cards.Sum(card => card.Rank) == 60; // (T)10 + (J)11 + (Q)12 + (K)13 + (A)14
            if (isRoyal && isStraight && isFlush)   return Rank.RoyalFlush;
            else if (isStraight && isFlush)         return Rank.StraightFlush;
            else if (IsFourOfAKind())               return Rank.FourOfAKind;
            else if (IsFullHouse())                 return Rank.FullHouse;
            else if (isFlush)                       return Rank.Flush;
            else if (isStraight)                    return Rank.Straight;
            else if (IsThreeOfAKind())              return Rank.ThreeOfAKind;
            else if (IsTwoPair())                   return Rank.TwoPair;
            else if (IsPair())                      return Rank.Pair;
            else                                    return Rank.HighCard;
        }

        private bool IsStraight(List<Card> cards)
        {
            bool isStraight = true;
            for (int prev = 0, curr = 1; curr < cards.Count; prev++, curr++)
            {
                if (cards[prev].Rank != cards[curr].Rank + 1)
                {
                    isStraight = false;
                    break;
                }
            }
            return isStraight;
        }

        private bool IsFlush(List<Card> cards)
        {
            bool isFlush = true;
            for (int prev = 0, curr = 1; curr < cards.Count; prev++, curr++)
            {
                if (cards[prev].Suit != cards[curr].Suit)
                {
                    isFlush = false;
                    break;
                }
            }
            return isFlush;
        }

        private bool IsFourOfAKind()
        {
            var rankKeys = rankCountLookup.Keys;
            return rankKeys.Count == 2 && Math.Abs(rankCountLookup[rankKeys.ElementAt(0)] - rankCountLookup[rankKeys.ElementAt(1)]) == 3;
        }

        private bool IsFullHouse()
        {
            var rankKeys = rankCountLookup.Keys;
            return rankKeys.Count == 2 && Math.Abs(rankCountLookup[rankKeys.ElementAt(0)] - rankCountLookup[rankKeys.ElementAt(1)]) == 1;
        }

        private bool IsThreeOfAKind()
        {
            var rankKeys = rankCountLookup.Keys;
            if (rankKeys.Count != 3) return false;
            bool isThreeOfAKind = false;
            foreach (var key in rankKeys)
            {
                if (rankCountLookup[key] == 3)
                {
                    isThreeOfAKind = true;
                    break;
                }
            }
            return isThreeOfAKind;
        }

        private bool IsTwoPair()
        {
            var rankKeys = rankCountLookup.Keys;
            if (rankKeys.Count != 3) return false;
            bool isTwoPair = false;
            foreach (var key in rankKeys)
            {
                if (rankCountLookup[key] == 2)
                {
                    isTwoPair = true;
                    break;
                }
            }
            return isTwoPair;
        }

        private bool IsPair()
        {
            if (rankCountLookup.Keys.Count != 4) return false;
            return true;
        }

        private Dictionary<int, int> GetRankCountLookup(List<Card> cards)
        {
            var rankCountLookup = new Dictionary<int, int>();
            for (int i = 0; i < cards.Count; i++)
            {
                if (!rankCountLookup.ContainsKey(cards[i].Rank)) rankCountLookup.Add(cards[i].Rank, 1);
                else rankCountLookup[cards[i].Rank] += 1;
            }
            return rankCountLookup;
        }
    }

    public class Card
    {
        public int Rank;
        public char Suit;
        public Card(char rank, char suit)
        {
            Rank = RankLookUp[rank];
            Suit = suit;
        }

        public static readonly Dictionary<char, int> RankLookUp = new Dictionary<char, int>()
        {
            {'2', 2}, {'3', 3}, {'4', 4},
            {'5', 5}, {'6', 6}, {'7', 7},
            {'8', 8}, {'9', 9}, {'T', 10},
            {'J', 11}, {'Q', 12}, {'K', 13}, {'A', 14}
        };
    }
}
