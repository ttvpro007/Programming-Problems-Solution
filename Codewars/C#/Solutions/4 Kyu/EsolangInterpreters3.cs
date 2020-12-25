using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Codewars.Solutions._4_Kyu
{
    ///Esolang Interpreters #3 - Custom Paintfuck Interpreter
    ///About this Kata Series
    ///"Esolang Interpreters" is a Kata Series that originally began as three separate, independent esolang interpreter Kata authored by @donaldsebleung which all shared a similar format and were all somewhat inter-related.Under the influence of a fellow Codewarrior, these three high-level inter-related Kata gradually evolved into what is known today as the "Esolang Interpreters" series.
    ///
    ///This series is a high-level Kata Series designed to challenge the minds of bright and daring programmers by implementing interpreters for various esoteric programming languages/Esolangs, mainly Brainfuck derivatives but not limited to them, given a certain specification for a certain Esolang.Perhaps the only exception to this rule is the very first Kata in this Series which is intended as an introduction/taster to the world of esoteric programming languages and writing interpreters for them.
    ///
    ///The Language
    ///Paintfuck is a borderline-esoteric programming language/Esolang which is a derivative of Smallfuck (itself a derivative of the famous Brainfuck) that uses a two-dimensional data grid instead of a one-dimensional tape.
    ///
    ///Valid commands in Paintfuck include:
    ///
    ///n - Move data pointer north (up)
    ///e - Move data pointer east (right)
    ///s - Move data pointer south (down)
    ///w - Move data pointer west (left)
    ///* - Flip the bit at the current cell (same as in Smallfuck)
    ///[ - Jump past matching] if bit under current pointer is 0 (same as in Smallfuck)
    ///] - Jump back to the matching [ (if bit under current pointer is nonzero) (same as in Smallfuck)
    ///The specification states that any non-command character(i.e.any character other than those mentioned above) should simply be ignored.The output of the interpreter is the two-dimensional data grid itself, best as animation as the interpreter is running, but at least a representation of the data grid itself after a certain number of iterations(explained later in task).
    ///
    ///In current implementations, the 2D datagrid is finite in size with toroidal(wrapping) behaviour.This is one of the few major differences of Paintfuck from Smallfuck as Smallfuck terminates(normally) whenever the pointer exceeds the bounds of the tape.
    ///
    ///Similar to Smallfuck, Paintfuck is Turing-complete if and only if the 2D data grid/canvas were unlimited in size.However, since the size of the data grid is defined to be finite, it acts like a finite state machine.
    ///
    ///More info on this Esolang can be found here.
    ///
    ///The Task
    ///Your task is to implement a custom Paintfuck interpreter interpreter()/Interpret which accepts the following arguments in the specified order:
    ///
    ///code - Required.The Paintfuck code to be executed, passed in as a string. May contain comments(non - command characters), in which case your interpreter should simply ignore them.If empty, simply return the initial state of the data grid.
    ///iterations - Required.A non-negative integer specifying the number of iterations to be performed before the final state of the data grid is returned.See notes for definition of 1 iteration.If equal to zero, simply return the initial state of the data grid.
    ///width - Required.The width of the data grid in terms of the number of data cells in each row, passed in as a positive integer.
    ///height - Required.The height of the data grid in cells(i.e.number of rows) passed in as a positive integer.
    ///A few things to note:
    ///
    ///Your interpreter should treat all command characters as case-sensitive so N, E, S and W are not valid command characters
    ///Your interpreter should initialize all cells within the data grid to a value of 0 regardless of the width and height of the grid
    ///In this implementation, your pointer must always start at the top-left hand corner of the data grid(i.e.first row, first column). This is important as some implementations have the data pointer starting at the middle of the grid.
    ///One iteration is defined as one step in the program, i.e.the number of command characters evaluated.For example, given a program nessewnnnewwwsswse and an iteration count of 5, your interpreter should evaluate nesse before returning the final state of the data grid.Non-command characters should not count towards the number of iterations.
    ///Regarding iterations, the act of skipping to the matching]
    ///    when a [ is encountered(or vice versa) is considered to be one iteration regardless of the number of command characters in between.The next iteration then commences at the command right after the matching] (or [).
    ///Your interpreter should terminate normally and return the final state of the 2D data grid whenever any of the mentioned conditions become true: (1) All commands have been considered left to right, or(2) Your interpreter has already performed the number of iterations specified in the second argument.
    ///The return value of your interpreter should be a representation of the final state of the 2D data grid where each row is separated from the next by a CRLF(\r\n). For example, if the final state of your datagrid is
    ///[
    ///  [1, 0, 0],
    ///  [0, 1, 0],
    ///  [0, 0, 1]
    ///]
    ///... then your return string should be "100\r\n010\r\n001".
    ///
    ///Good luck :D
    ///
    ///Kata in this Series
    ///Esolang Interpreters #1 - Introduction to Esolangs and My First Interpreter (MiniStringFuck)
    ///Esolang Interpreters #2 - Custom Smallfuck Interpreter
    ///Esolang Interpreters #3 - Custom Paintfuck Interpreter
    ///Esolang Interpreters #4 - Boolfuck Interpreter

    public class PaintFuck
    {
        public static string Interpret(string code, int iterations, int width, int height)
        {
            PaintFuckProcessor processor = new PaintFuckProcessor(new BitPaintFuckModule(width, height));
            return processor.Process(code, iterations);
        }
    }

    public abstract class PaintFuckModule
    {
        public Dictionary<char, Func<PaintFuckModule, int, int>> Commands { get; protected set; }
        public string cleanPattern { get; protected set; }
        public string output { get => TableToString(); }
        public bool skipping { get; protected set; }
        public bool isOutOfBound { get => dataPointer < 0 || dataPointer >= width * height; }

        public PaintFuckModule(int width, int height)
        {
            table = new List<int>(new int[width * height]);
            cleanPattern = string.Empty;
            skipping = false;
            this.width = width;
            this.height = height;
            dataPointer = 0;
        }

        protected virtual string TableToString()
        {
            string result = string.Empty;
            for (int i = 0; i < width * height; i++)
            {
                result += table[i];
                result += i < width * height - 1 && i % width == width - 1 ? "\r\n" : string.Empty;
            }
            return result;
        }

        protected List<int> table;
        protected int width;
        protected int height;
        protected int dataPointer;
    }

    public class BitPaintFuckModule : PaintFuckModule
    {
        public BitPaintFuckModule(int width, int height) : base(width, height)
        {
            Commands = new Dictionary<char, Func<PaintFuckModule, int, int>>()
        {
            { 'n', (module, commandIndex) => (module as BitPaintFuckModule).MoveUp(commandIndex) },
            { 'e', (module, commandIndex) => (module as BitPaintFuckModule).MoveRight(commandIndex) },
            { 's', (module, commandIndex) => (module as BitPaintFuckModule).MoveDown(commandIndex) },
            { 'w', (module, commandIndex) => (module as BitPaintFuckModule).MoveLeft(commandIndex) },
            { '*', (module, commandIndex) => (module as BitPaintFuckModule).Flip(commandIndex) },
            { '[', (module, commandIndex) => (module as BitPaintFuckModule).SkipIfZero(commandIndex) },
            { ']', (module, commandIndex) => (module as BitPaintFuckModule).BackIfNonZero(commandIndex) }
        };

            cleanPattern = @"[^nesw*\[\]]";
            openBracketStack = new Stack<int>();
            previousBracketLookUp = new Dictionary<int, int>();
            skippingAtIndex = -1;
        }

        public int MoveUp(int codePointer)
        {
            if (!skipping)
                dataPointer = dataPointer - width < 0 ? (dataPointer + (height - 1) * width) : (dataPointer - width);

            return codePointer;
        }

        public int MoveRight(int codePointer)
        {
            if (!skipping)
                dataPointer = (dataPointer % width == width - 1) ? (dataPointer + 1 - width) : (dataPointer + 1);

            return codePointer;
        }

        public int MoveDown(int codePointer)
        {
            if (!skipping)
                dataPointer = dataPointer + width >= width * height ? (dataPointer - (height - 1) * width) : (dataPointer + width);

            return codePointer;
        }

        public int MoveLeft(int codePointer)
        {
            if (!skipping)
                dataPointer = (dataPointer % width == 0) ? (dataPointer - 1 + width) : (dataPointer - 1);

            return codePointer;
        }

        public int Flip(int codePointer)
        {
            if (!skipping)
                table[dataPointer] ^= 1;

            return codePointer;
        }

        public int SkipIfZero(int codePointer)
        {
            openBracketStack.Push(codePointer);

            if (table[dataPointer] == 0 && !skipping)
            {
                skipping = true;
                skippingAtIndex = codePointer;
            }
            return codePointer;
        }

        public int BackIfNonZero(int codePointer)
        {
            if (openBracketStack.Count > 0 && !previousBracketLookUp.ContainsKey(codePointer))
                previousBracketLookUp.Add(codePointer, openBracketStack.Pop());

            if (previousBracketLookUp.TryGetValue(codePointer, out int previousBracketIndex))
            {
                if (table[dataPointer] != 0 && !skipping)
                    codePointer = previousBracketIndex;

                if (skippingAtIndex == previousBracketIndex && skipping)
                {
                    skipping = false;
                    skippingAtIndex = -1;
                }
            }
            return codePointer;
        }

        private Stack<int> openBracketStack;
        private Dictionary<int, int> previousBracketLookUp;
        private int skippingAtIndex;
    }

    public class PaintFuckProcessor
    {
        public PaintFuckProcessor(PaintFuckModule paintFuckModule)
        {
            this.paintFuckModule = paintFuckModule;
        }

        public string Process(string input, int iterations)
        {
            if (paintFuckModule.Commands == null)
                return string.Empty;

            input = Clean(input);
            int step = 0;
            int codePointer = 0;
            while (step < iterations && codePointer < input.Length && !paintFuckModule.isOutOfBound)
            {
                if (paintFuckModule.Commands.TryGetValue(input[codePointer], out var func))
                    codePointer = func(paintFuckModule, codePointer);

                codePointer++;

                if (!paintFuckModule.skipping)
                    step++;
            }
            return paintFuckModule.output;
        }

        private string Clean(string input)
        {
            return Regex.Replace(input, paintFuckModule.cleanPattern, "");
        }

        private PaintFuckModule paintFuckModule;
    }
}
