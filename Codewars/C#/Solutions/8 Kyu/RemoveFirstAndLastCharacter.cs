namespace Codewars.Solutions._8_Kyu
{
    class RemoveFirstAndLastCharacter
    {
        ///It's pretty straightforward. Your goal is to create a function that removes 
        ///the first and last characters of a string. You're given one parameter, the 
        ///original string. You don't have to worry with strings with less than two 
        ///characters.
        
        public static string Remove_char(string s)
        {
            return s.Length >= 2 ? s.Remove(s.Length - 1, 1).Remove(0, 1) : string.Empty;
        }
    }
}
