namespace Codewars.Solutions._6_Kyu
{
    ///Write a function that accepts an array of 10 integers(between 0 and 9), that returns a string of those numbers in the form of a phone number.
    ///
    ///Example:
    ///Kata.CreatePhoneNumber(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0}) // => returns "(123) 456-7890"
    ///The returned format must be correct in order to complete this challenge.
    ///Don't forget the space after the closing parentheses!

    public static class CreatePhoneNumber
    {
        public static string Solution(int[] numbers)
        {
            string area       = numbers.ToStringInRange(                 0 , numbers.Length - 10);
            string firstThree = numbers.ToStringInRange(numbers.Length - 10, numbers.Length - 7);
            string midThree   = numbers.ToStringInRange(numbers.Length - 7 , numbers.Length - 4);
            string lastFour   = numbers.ToStringInRange(numbers.Length - 4 , numbers.Length);
            string phoneNum = $"({firstThree}) {midThree}-{lastFour}";
            return numbers.Length <= 10 ? phoneNum : $"+{area} {phoneNum}";
        }

        // From is inclusive. To is exclusive
        public static string ToStringInRange(this int[] numbers, int from, int to)
        {
            string result = string.Empty;
            for (int i = from; i < to; i++)
            {
                result += numbers[i];
            }
            return result;
        }
    }
}
