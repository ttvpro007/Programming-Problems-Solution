using System.Linq;

namespace Codewars.Solutions._6_Kyu
{
    ///Given a string of words, you need to find the highest scoring word.
    ///
    ///Each letter of a word scores points according to its position in the alphabet: a = 1, b = 2, c = 3 etc.
    ///
    ///You need to return the highest scoring word as a string.
    ///
    ///If two words score the same, return the word that appears earliest in the original string.
    ///
    ///All letters will be lowercase and all inputs will be valid.

    class HighestScoringWord
    {
        public static string Solution(string str) => str.Split(' ').OrderByDescending(w => w.Sum(c => GetScore(c))).First();
        public static int GetScore(char chr) => "abcdefghijklmnopqrstuvwxyz".IndexOf(chr) + 1;

        // other solution
        // public static string High(string str) => str.Split(' ').OrderByDescending(w => w.Sum(c => c - 'a' + 1)).First();
    }
}
