using System;
using System.Collections.Generic;
using System.Text;

namespace Codewars.Solutions._4_Kyu
{
    class Snail
    {
        public enum Direction { Up, Down, Left, Right }
        public static int[] Snail(int[][] array)
        {
            Direction dir = Direction.Right;
            int width = array[0].Length;
            int height = array.Length;
            var result = new List<int>();
            int x = 0, y = -1;
            for (int i = 0; i < width * height; i++)
            {
                switch (dir)
                {
                    case Direction.Up:
                        x--;
                        if (x < 0) { x++; dir = Direction.Right; y++; }
                        break;
                    case Direction.Down:
                        x++;
                        if (x >= height) { x--; dir = Direction.Left; y--; }
                        break;
                    case Direction.Left:
                        y--;
                        if (y < 0) { y++; dir = Direction.Up; x--; }
                        break;
                    case Direction.Right:
                        y++;
                        if (y >= width) { y--; dir = Direction.Down; x++; }
                        break;
                }

                int currNum = array[x][y];
                Console.Write($"\nCurrent Number: {currNum}");
                if (!result.Contains(currNum))
                {
                    result.Add(currNum);
                }


            }
            return result.ToArray();
        }
        public static Direction GetNextDirection()
        {
            switch (currDir)
            {
                case Direction.Up:
                    x--;
                    if (x < 0) { x++; dir = Direction.Right; y++; }
                    break;
                case Direction.Down:
                    x++;
                    if (x >= height) { x--; dir = Direction.Left; y--; }
                    break;
                case Direction.Left:
                    y--;
                    if (y < 0) { y++; dir = Direction.Up; x--; }
                    break;
                case Direction.Right:
                    y++;
                    if (y >= width) { y--; dir = Direction.Down; x++; }
                    break;
            }
        }
    }
}
