using System;
using System.Collections.Generic;

namespace Codewars.Solutions._4_Kyu
{
    ///Write a function that takes a positive integer and returns the next smaller positive integer containing the same digits.
    ///
    ///For example:
    ///
    ///nextSmaller(21) == 12
    ///nextSmaller(531) == 513
    ///nextSmaller(2071) == 2017
    ///Return -1 (for Haskell: return Nothing, for Rust: return None), when there is no smaller number that contains the same digits.Also return -1 when the next smaller number with the same digits would require the leading digit to be zero.
    ///
    ///nextSmaller(9) == -1
    ///nextSmaller(111) == -1
    ///nextSmaller(135) == -1
    ///nextSmaller(1027) == -1 // 0721 is out since we don't write numbers with leading zeros
    ///some tests will include very large numbers.
    ///test data only employs positive integers.
    ///The function you write for this challenge is the inverse of this kata: "Next bigger number with the same digits."

    class NextSmallerNumberWithSameDigits
    {
        public static long Solution(long n)
        {
            if (n < 10) return -1;
            var nums = new List<int>();
            foreach (var c in n.ToString())
            {
                nums.Add((int)char.GetNumericValue(c));
            }

            // reverse the list
            nums.Reverse();

            // find largest num that has smaller num to its left
            int a = -1;
            for (int i = 1; i < nums.Count; i++)
            {
                if (nums[i] > nums[i - 1])
                {
                    a = i;
                    break;
                }
            }

            if (a == -1) return -1;

            // find largest num that's smaller than num at index a
            int b = 0, max = -1;
            for (int i = 0; i < a; i++)
            {
                if (max < nums[i] && nums[i] < nums[a])
                {
                    max = nums[i];
                    b = i;
                }
            }

            // swap num at index a and index b
            nums = Swap(nums, a, b);

            // sort from left to a - 1
            nums = PartialSort(nums, 0, a - 1);

            // undo reverse at top
            nums.Reverse();

            long r = long.Parse(string.Join(string.Empty, nums.ToArray()));
            return r.ToString().Length == n.ToString().Length ? r : -1;
        }

        private static List<T> PartialSort<T>(List<T> list, int start, int end) where T : IComparable
        {
            if (start >= end || end >= list.Count) return list;

            List<T> sortedPartialList = new List<T>();
            for (int i = start; i <= end; i++)
            {
                sortedPartialList.Add(list[i]);
            }
            sortedPartialList.Sort();
            for (int i = 0; i < sortedPartialList.Count; i++)
            {
                list[i + start] = sortedPartialList[i];
            }

            return list;
        }

        private static List<T> Swap<T>(List<T> list, int a, int b)
        {
            if (a == b) return list;
            var temp = list[a];
            list[a] = list[b];
            list[b] = temp;
            return list;
        }
    }

}
