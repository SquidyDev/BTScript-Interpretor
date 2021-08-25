using Bits_Script_Interpreter.Program.Function;
using Bits_Script_Interpreter.Interpreter.String;
using System.Collections.Generic;
using Bits_Script_Interpreter.Program.Variable;

/*
<summary>
    Use for loop
    read all the code in the loop
    and then run the code
</summary>
*/

namespace Bits_Script_Interpreter.Interpreter.Loop
{
    static class Interpreter_Loop
    {
        public static void Loop(string[] lines, int currentLine, int time, bool isFunction, string function)
        {
            List<string> scopeVariable = new List<string>();
            string[] loopContent = Interpreter_String.ReadBraceContent(lines, currentLine);

            Debug.Log("Loop Content : ", true);
            Debug.Log(Interpreter_String.AssembleArray<string, char>(loopContent, 0, '\n'), true);

            for(int i = 0; i < time; i++)
            {
                for(int j = 0; j < loopContent.Length; j++)
                {
                    Interpreter.InterpreteLine(loopContent[j], loopContent, j, scopeVariable, true, isFunction, function);
                }
            }

            Program_Variable.DeleteScopeVariable(scopeVariable);
            scopeVariable.Clear();

            Interpreter_Core_Variable.Set(true, loopContent.Length);
        }
    }
}
