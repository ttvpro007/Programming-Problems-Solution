using System;
using System.Collections.Generic;
using System.Text;

namespace Codewars.Solutions._4_Kyu
{
    ///We need to sum big numbers and we require your help.
    ///
    ///Write a function that returns the sum of two numbers.The input numbers are strings and the function must return a string.
    ///
    ///Example
    ///add("123", "321"); -> "444"
    ///add("11", "99");   -> "110"
    ///Notes
    ///The input numbers are big.
    ///The input is a string of only digits
    ///The numbers are positives
    
    class AddingBigNumbers
    {
        public static string Solution(string a, string b)
        {
            a = Reverse(a);
            b = Reverse(b);
            string c = string.Empty;
            int mem = 0;

            int len = a.Length > b.Length ? a.Length : b.Length;

            for (int i = 0; i < len; i++)
            {
                int r = (a.Length > i ? GetValue(a[i]) : 0) + (b.Length > i ? GetValue(b[i]) : 0) + mem;
                string rs = r.ToString();

                if (r >= 10)
                {
                    mem = GetValue(rs[0]);
                    c += "" + rs[1];
                }
                else
                {
                    mem = 0;
                    c += "" + rs[0];
                }

                if (i == len - 1 && mem != 0)
                {
                    c += "" + mem;
                }
            }

            return Reverse(c);
        }

        private static string Reverse(string s)
        {
            var a = s.ToCharArray();
            Array.Reverse(a);
            return new string(a);
        }

        private static int GetValue(char c)
        {
            return (int)char.GetNumericValue(c);
        }
    }
}
