namespace Codewars.Solutions._8_Kyu
{
    class ReturnNegative
    {
        ///In this simple assignment you are given a number and have to make it negative.But maybe the number is already negative?
        ///
        ///Example:
        ///
        ///Kata.MakeNegative(1); // return -1
        ///Kata.MakeNegative(-5); // return -5
        ///Kata.MakeNegative(0); // return 0
        
        public static int Solution(int number)
        {
            return number > 0 ? -number : number;
        }
    }
}
