using System.Collections.Generic;
using Bits_Script_Interpreter.Program.Variable;

namespace Bits_Script_Interpreter.Compiler.Program
{
    [System.Serializable]
    class ProgramBuild
    {
        public Dictionary<string, Var> variable;

        public ProgramBuild(Dictionary<string, Var> variables) 
        {
            variable = variables;
        }
    }
}
