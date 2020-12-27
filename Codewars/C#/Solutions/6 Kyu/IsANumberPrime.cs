using System.Linq;
using System.Collections.Generic;

namespace Codewars.Solutions._6_Kyu
{
    ///Define a function that takes one integer argument and returns logical value true 
    ///or false depending on if the integer is a prime.
    ///
    ///Per Wikipedia, a prime number (or a prime) is a natural number greater than 1 
    ///that has no positive divisors other than 1 and itself.
    ///
    ///Requirements
    ///You can assume you will be given an integer input.
    ///You can not assume that the integer will be only positive.You may be given 
    ///negative numbers as well (or 0).
    ///NOTE on performance: There are no fancy optimizations required, but still the most
    ///trivial solutions might time out. Numbers go up to 2^31 (or similar, depends on 
    ///language version). Looping all the way up to n, or n/2, will be too slow.

    class IsANumberPrime
    { 
        public static bool IsPrime(int n)
        {
            return n > 1 && FindFactors(n).Count() == 2;
        }

        public static IEnumerable<int> FindFactors(int number)
        {
            for (int factor = 1; factor * factor <= number; factor++)
            {
                if (number % factor == 0)
                {
                    yield return factor;
                    int otherFactor = number / factor;
                    if (factor != otherFactor)
                    {
                        yield return otherFactor;
                    }
                }
            }
        }
    }
}
