namespace Codewars.Solutions._7_Kyu
{
    ///Implement a method that accepts 3 integer values a, b, c.The method should return true 
    ///if a triangle can be built with the sides of given length and false in any other case.
    ///
    ///(In this case, all triangles must have surface greater than 0 to be accepted).

    class IsThisATriangle
    {
        public static bool Solution(int a, int b, int c)
        {
            var sideList = new int[3] { a, b, c };
            for (int i = 0; i < 3; i++)
            {
                if (sideList[i % 3] + sideList[(i + 1) % 3] <= sideList[(i + 2) % 3])
                    return false;
            }
            return true;
        }
    }
}
