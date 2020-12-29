﻿using System.Linq;

namespace Codewars.Solutions._6_Kyu
{
    ///Your goal in this kata is to implement a difference function, which subtracts one list from another and returns the result.
    ///
    ///It should remove all values from list a, which are present in list b.
    ///
    ///Kata.ArrayDiff(new int[] {1, 2}, new int[] { 1 }) => new int[] { 2 }
    ///If a value is present in b, all of its occurrences must be removed from the other:
    ///
    ///Kata.ArrayDiff(new int[] { 1, 2, 2, 2, 3 }, new int[] { 2 }) => new int[] { 1, 3 }

    class ArrayDiff
    {
        public static int[] Solution(int[] a, int[] b)
        {
            return a.Where(n => !b.Contains(n)).ToArray();
        }
    }
}
