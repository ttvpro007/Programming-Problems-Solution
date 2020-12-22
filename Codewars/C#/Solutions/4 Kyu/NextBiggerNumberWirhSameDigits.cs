using System;
using System.Collections.Generic;
using System.Text;

namespace Codewars.Solutions._4_Kyu
{
    class NextBiggerNumberWirhSameDigits
    {
        ///Create a function that takes a positive integer and returns the next bigger number that can be formed by rearranging its digits.For example:
        ///
        ///12 ==> 21
        ///513 ==> 531
        ///2017 ==> 2071
        ///nextBigger(num: 12)   // returns 21
        ///nextBigger(num: 513)  // returns 531
        ///nextBigger(num: 2017) // returns 2071
        ///If the digits can't be rearranged to form a bigger number, return -1 (or nil in Swift):
        ///
        ///9 ==> -1
        ///111 ==> -1
        ///531 ==> -1
        ///nextBigger(num: 9)   // returns nil
        ///nextBigger(num: 111) // returns nil
        ///nextBigger(num: 531) // returns nil

        public static long Solution(long n)
        {
            if (n < 10) return -1;
            var nums = new List<int>();
            foreach (var c in n.ToString())
            {
                nums.Add((int)char.GetNumericValue(c));
            }

            // reverse the listnums.Reverse();
            nums.Reverse();

            // find index of first smallest num that has bigger num on its left - yield index a
            int a = -1;
            for (int i = 1; i < nums.Count; i++)
            {
                if (nums[i] < nums[i - 1])
                {
                    a = i;
                    break;
                }
            }

            if (a == -1) return -1;

            // find index of smallest num on the left of index a that is bigger than num at index a - yield index b
            int b = 0, min = 10;
            for (int i = 0; i < a; i++)
            {
                if (min > nums[i] && nums[i] > nums[a])
                {
                    min = nums[i];
                    b = i;
                }
            }

            // swap num at index a and index b
            nums = Swap(nums, a, b);

            // undo the reverse
            nums.Reverse();

            // sort from index length - b to last index
            nums = PartialSort(nums, nums.Count - b, nums.Count - 1);

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
