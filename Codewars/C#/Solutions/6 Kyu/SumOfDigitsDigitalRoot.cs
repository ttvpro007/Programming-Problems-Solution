using System.Linq;

namespace Codewars.Solutions._6_Kyu
{
    ///Digital root is the recursive sum of all the digits in a number.
    ///
    ///Given n, take the sum of the digits of n. If that value has more than one digit, continue reducing
    ///in this way until a single-digit number is produced.The input will be a non-negative integer.
    
    class SumOfDigitsDigitalRoot
    {
        public int Solution(long n)
        {
            // better solution: return (int) (1 + (n - 1) % 9); https://en.wikipedia.org/wiki/Digital_root#Congruence_formula
            // brute force solution: return n != 0 && n % 9 == 0 ? 9 : (int)(n % 9);
            return n < 10 ? (int)n : DigitalRoot((long)n.ToString().Sum(c => char.GetNumericValue(c)));
        }
    }
}
