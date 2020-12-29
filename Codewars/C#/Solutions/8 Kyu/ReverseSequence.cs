namespace Codewars.Solutions._8_Kyu
{
    ///Build a function that returns an array of integers from n to 1 where n>0.
    ///
    ///Example : n=5 >> [5,4,3,2,1]

    class ReverseSequence
    {
        public static int[] Solution(int n)
        {
            int[] result = new int[n];
            for (int i = 0; n > 0; n--, i++)
            {
                result[i] = n;
            }
            return result;
        }
    }
}
