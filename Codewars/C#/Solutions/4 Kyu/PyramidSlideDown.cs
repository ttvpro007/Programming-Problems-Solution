using System;

namespace Codewars.Solutions._4_Kyu
{
    /// Lyrics...
    /// Pyramids are amazing! Both in architectural and mathematical sense.If you have a computer, you
    /// can mess with pyramids even if you are not in Egypt at the time. For example, let's consider the 
    /// following problem. Imagine that you have a pyramid built of
    /// numbers, like this one here:
    ///
    ///           /3/
    ///          \7\ 4 
    ///         2 \4\ 6 
    ///        8 5 \9\ 3
    ///
    /// Here comes the task...
    /// Let's say that the 'slide down' is the maximum sum of consecutive numbers from the top to the bottom 
    /// of the pyramid.
    /// As you can see, the longest 'slide down' is 3 + 7 + 4 + 9 = 23
    ///
    ///
    /// Your task is to write a function longestSlideDown (in ruby/crystal/julia: longest_slide_down) that
    /// takes a pyramid representation as argument and returns its' largest 'slide down'. For example:
    ///
    /// LongestSlideDown(new[] { new[] {3}, new[] { 7, 4 }, new[] { 2, 4, 6 }, new[] { 8, 5, 9, 3 } }); => 23
    ///
    /// By the way...
    /// My tests include some extraordinarily high pyramids so as you can guess, brute-force method 
    /// is a bad idea unless you have a few centuries to waste. You must come up with something more clever 
    /// than that.
    ///
    /// (c) This task is a lyrical version of the Problem 18 and/or Problem 67 on ProjectEuler.

    class PyramidSlideDown
    {
        public static int Solution(int[][] pyramid)
        {
            // find from bottom up
            for (int i = pyramid.GetLength(0) - 2; i >= 0; i--)
            {
                for (int j = 0; j < pyramid[i].GetLength(0); j++)
                {
                    int left = pyramid[i][j] + pyramid[i + 1][j];
                    int right = pyramid[i][j] + pyramid[i + 1][j + 1];
                    pyramid[i][j] = Math.Max(left, +right);
                }
            }

            return pyramid[0][0];
        }
    }
}
