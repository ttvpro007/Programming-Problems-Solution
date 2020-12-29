using System;
using System.Linq;

namespace Codewars.Solutions._8_Kyu
{
    ///Complete the solution so that it reverses the string passed into it.
    ///
    ///'world'  =>  'dlrow'
    
    class ReversedString
    {
        public static string Solution(string str)
        {
            return string.Join("", str.Reverse());
        }
    }
}
