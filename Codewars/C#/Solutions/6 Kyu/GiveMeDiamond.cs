namespace Codewars.Solutions._6_Kyu
{
    class GiveMeDiamond
    {
        ///Jamie is a programmer, and James' girlfriend. She likes diamonds, and wants a diamond string from James. Since James doesn't know how to make this happen, he needs your help.
        ///
        ///Task
        ///You need to return a string that looks like a diamond shape when printed on the screen, using asterisk (*) characters.Trailing spaces should be removed, and every line must be terminated with a newline character (\n).
        ///
        ///Return null/nil/None/... if the input is an even number or negative, as it is not possible to print a diamond of even or negative size.
        ///
        ///Examples
        ///A size 3 diamond:
        ///
        /// *
        ///***
        /// *
        ///...which would appear as a string of " *\n***\n *\n"
        ///
        ///A size 5 diamond:
        ///
        ///  *
        /// ***
        ///*****
        /// ***
        ///  *
        ///...that is:
        ///
        ///"  *\n ***\n*****\n ***\n  *\n"
        
        public static string Solution(int n)
        {
            if (n <= 0 || n % 2 == 0) return null;
            string result = string.Empty;
            int starAmount = 1;
            int indentAmount = n / 2;
            for (int i = 0; i < n; i++)
            {
                result += GetIndent(indentAmount) + GetStar(starAmount) + "\n";
                starAmount += (i < n / 2) ? 2 : -2;
                indentAmount += (i < n / 2) ? -1 : 1;
            }
            return result;
        }

        public static string GetStar(int amount)
        {
            string star = string.Empty;
            for (int i = 0; i < amount; i++)
            {
                star += "*";
            }
            return star;
        }

        public static string GetIndent(int amount)
        {
            string indent = string.Empty;
            for (int i = 0; i < amount; i++)
            {
                indent += " ";
            }
            return indent;
        }
    }
}
