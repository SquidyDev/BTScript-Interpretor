using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Program.Variable;
using Bits_Script_Interpreter.Program.Variable.Type_Definer;
using System;

namespace Bits_Script_Interpreter.Interpreter.Builtins
{
    static class Interpreter_Builtins
    {
        public static void Print(string[] BLOCK)
        {
            string assembledMemoryBlock = Interpreter_String.AssembleArray<string, char>(BLOCK, 1, ' ');

            if(Interpreter_Type.IsString(assembledMemoryBlock) || Interpreter_Type.IsChar(assembledMemoryBlock)) 
            { 
                Console.WriteLine(Interpreter_String.RemoveQuoteMarks(assembledMemoryBlock)); 
            }
            else 
            {
                if (Program_Variable.Exist(assembledMemoryBlock)) 
                {
                    Console.WriteLine(Interpreter_String.RemoveQuoteMarks(Program_Variable.GetVariable(Interpreter_String.Remove(assembledMemoryBlock, ' '), false).value.ToString()));
                }
                else { Debug.Error(true, $"ERROR : Usage of a non defined variable, in function Builtins.Print(string[] BLOCK) (variable {assembledMemoryBlock} does not exist in the context)"); }
            }
        }
    }
}
