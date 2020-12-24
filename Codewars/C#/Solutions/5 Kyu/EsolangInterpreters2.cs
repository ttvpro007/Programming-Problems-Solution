using System;
using System.Text;
using System.Collections.Generic;

namespace Codewars.Solutions._5_Kyu
{
    ///Esolang Interpreters #2 - Custom Smallfuck Interpreter
    ///About this Kata Series
    ///"Esolang Interpreters" is a Kata Series that originally began as three separate, independent esolang interpreter Kata authored by @donaldsebleung which all shared a similar format and were all somewhat inter-related.Under the influence of a fellow Codewarrior, these three high-level inter-related Kata gradually evolved into what is known today as the "Esolang Interpreters" series.
    ///
    ///This series is a high-level Kata Series designed to challenge the minds of bright and daring programmers by implementing interpreters for various esoteric programming languages/Esolangs, mainly Brainfuck derivatives but not limited to them, given a certain specification for a certain Esolang.Perhaps the only exception to this rule is the very first Kata in this Series which is intended as an introduction/taster to the world of esoteric programming languages and writing interpreters for them.
    ///
    ///The Language
    ///Smallfuck is an esoteric programming language/Esolang invented in 2002 which is a sized-down variant of the famous Brainfuck Esolang.Key differences include:
    ///
    ///Smallfuck operates only on bits as opposed to bytes
    ///It has a limited data storage which varies from implementation to implementation depending on the size of the tape
    ///It does not define input or output - the "input" is encoded in the initial state of the data storage (tape) and the "output" should be decoded in the final state of the data storage (tape)
    ///Here are a list of commands in Smallfuck:
    ///
    ///> - Move pointer to the right (by 1 cell)
    ///< - Move pointer to the left (by 1 cell)
    ///* - Flip the bit at the current cell
    ///[ - Jump past matching] if value at current cell is 0
    ///] - Jump back to matching [ (if value at current cell is nonzero)
    ///As opposed to Brainfuck where a program terminates only when all of the commands in the program have been considered(left to right), Smallfuck terminates when any of the two conditions mentioned below become true:
    ///
    ///All commands have been considered from left to right
    ///The pointer goes out-of-bounds(i.e. if it moves to the left of the first cell or to the right of the last cell of the tape)
    ///Smallfuck is considered to be Turing-complete if and only if it had a tape of infinite length; however, since the length of the tape is always defined as finite(as the interpreter cannot return a tape of infinite length), its computational class is of bounded-storage machines with bounded input.
    ///
    ///More information on this Esolang can be found here.
    ///
    ///The Task
    ///Implement a custom Smallfuck interpreter interpreter() (interpreter in Haskell and F#, Interpreter in C#, custom_small_fuck:interpreter/2 in Erlang) which accepts the following arguments:
    ///
    ///code - Required.The Smallfuck program to be executed, passed in as a string. May contain non-command characters. Your interpreter should simply ignore any non-command characters.
    ///tape - Required.The initial state of the data storage (tape), passed in as a string. For example, if the string "00101100" is passed in then it should translate to something of this form within your interpreter: [0, 0, 1, 0, 1, 1, 0, 0]. You may assume that all input strings for tape will be non-empty and will only contain "0"s and "1"s.
    ///Your interpreter should return the final state of the data storage (tape) as a string in the same format that it was passed in. For example, if the tape in your interpreter ends up being[1, 1, 1, 1, 1] then return the string "11111".
    ///
    ///
    ///NOTE: The pointer of the interpreter always starts from the first (leftmost) cell of the tape, same as in Brainfuck.
    ///
    ///Good luck :D
    ///
    ///Kata in this Series
    ///Esolang Interpreters #1 - Introduction to Esolangs and My First Interpreter (MiniStringFuck)
    ///Esolang Interpreters #2 - Custom Smallfuck Interpreter
    ///Esolang Interpreters #3 - Custom Paintfuck Interpreter
    ///Esolang Interpreters #4 - Boolfuck Interpreter

    public class SmallFuck
    {
        public static string Interpreter(string code, string tape)
        {
            SmallFuckProcessor processor = new SmallFuckProcessor(new BitSmallFuckModule(tape));
            return processor.Process(code);
        }
    }

    public abstract class SmallFuckModule
    {
        public Dictionary<char, Func<SmallFuckModule, int, int>> Commands { get; protected set; }
        public string output { get => tape.ToString(); }
        public bool skipping { get; protected set; }
        public bool isOutOfBound { get => pointer < 0 || pointer >= tape.Length; }

        public SmallFuckModule(string tape)
        {
            skipping = false;
            cell = 0;
            this.tape = new StringBuilder(tape);
            pointer = 0;
        }

        protected int cell;
        protected StringBuilder tape;
        protected int pointer;
    }

    public class BitSmallFuckModule : SmallFuckModule
    {
        public BitSmallFuckModule(string tape) : base(tape)
        {
            Commands = new Dictionary<char, Func<SmallFuckModule, int, int>>()
        {
            { '>', (module, commandIndex) => (module as BitSmallFuckModule).MoveRight(commandIndex) },
            { '<', (module, commandIndex) => (module as BitSmallFuckModule).MoveLeft(commandIndex) },
            { '*', (module, commandIndex) => (module as BitSmallFuckModule).Flip(commandIndex) },
            { '[', (module, commandIndex) => (module as BitSmallFuckModule).SkipIfZero(commandIndex) },
            { ']', (module, commandIndex) => (module as BitSmallFuckModule).BackIfNonZero(commandIndex) }
        };

            openBracketStack = new Stack<int>();
            previousBracketLookUp = new Dictionary<int, int>();
            skippingAtIndex = -1;
        }

        public int MoveRight(int commandIndex)
        {
            if (!skipping) pointer++;
            return commandIndex;
        }

        public int MoveLeft(int commandIndex)
        {
            if (!skipping) pointer--;
            return commandIndex;
        }

        public int Flip(int commandIndex)
        {
            if (!skipping) tape[pointer] = tape[pointer] == '0' ? '1' : '0';
            return commandIndex;
        }

        public int SkipIfZero(int commandIndex)
        {
            // push the position of open bracket
            openBracketStack.Push(commandIndex);

            if (tape[pointer] == '0' && !skipping)
            {
                skipping = true;
                skippingAtIndex = commandIndex;
            }
            return commandIndex;
        }

        public int BackIfNonZero(int commandIndex)
        {
            if (openBracketStack.Count > 0 && !previousBracketLookUp.ContainsKey(commandIndex))
            {
                // add last open bracket index to lookup with the most recent close bracket index
                previousBracketLookUp.Add(commandIndex, openBracketStack.Pop());
            }

            if (previousBracketLookUp.TryGetValue(commandIndex, out int previousBracketIndex))
            {
                if (tape[pointer] != '0' && !skipping)
                {
                    commandIndex = previousBracketIndex;
                }

                // end skipping if the close bracket is paired with the open bracket that start the skipping
                if (skippingAtIndex == previousBracketIndex)
                {
                    skipping = false;
                    skippingAtIndex = -1;
                }
            }
            return commandIndex;
        }


        private Stack<int> openBracketStack;
        private Dictionary<int, int> previousBracketLookUp;
        private int skippingAtIndex;
    }

    public class SmallFuckProcessor
    {
        public SmallFuckProcessor(SmallFuckModule smallFuckModule)
        {
            this.smallFuckModule = smallFuckModule;
        }

        public string Process(string input)
        {
            if (smallFuckModule.Commands == null) return string.Empty;

            for (int i = 0; i < input.Length; i++)
            {
                if (smallFuckModule.Commands.TryGetValue(input[i], out var func))
                {
                    i = func(smallFuckModule, i);
                }

                if (smallFuckModule.isOutOfBound) break;
            }

            return smallFuckModule.output;
        }

        private SmallFuckModule smallFuckModule;
    }
}