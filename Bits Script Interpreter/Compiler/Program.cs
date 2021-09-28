﻿using System.Collections.Generic;
using Bits_Script_Interpreter.Program.Variable;

namespace Bits_Script_Interpreter.Compiler.Program
{
    /*Contain Functions & Variables when the program is compiled*/
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
