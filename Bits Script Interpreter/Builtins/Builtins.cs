using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Program.Variable;
using System;
using System.Collections.Generic;
using Bits_Script_Interpreter.Evaluator.String;

namespace Bits_Script_Interpreter.Interpreter.Builtins
{
    static class Interpreter_Builtins
    {
        public static void Print(string[] BLOCK)
        {
            string assembledMemoryBlock = Interpreter_String.AssembleArray<string, char>(BLOCK, 1, ' ');

            Console.WriteLine(Interpreter_String_Evaluator.Evaluate(assembledMemoryBlock));
        }
    }
}
