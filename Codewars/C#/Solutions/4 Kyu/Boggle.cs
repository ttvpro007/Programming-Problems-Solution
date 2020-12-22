using System;
using System.Collections.Generic;
using System.Linq;

namespace Codewars.Solutions._4_Kyu
{
    class Boggle
    {
        public Boggle(char[][] board, string word)
        {
            this.board = board;
            row = board.GetLength(0);
            col = board.GetLength(1);
        }

        public void Init()
        {
        }

        public bool Check()
        {
            return false;
        }

        private char[][] board = default;
        int row, col;
    }

    public class Cell
    {
        public List<Cell> neighbors;
        public char value;

        public Cell(char value)
        {
            this.value = value;
        }

        public void AddToNeighborList(Cell neighbor)
        {
            if (neighbors.Contains(neighbor)) return;
            neighbors.Add(neighbor);
        }
    }
}
