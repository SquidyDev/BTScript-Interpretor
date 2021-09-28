using Bits_Script_Interpreter.Compiler.Program;
using Bits_Script_Interpreter.Compiler.Builder;
using Bits_Script_Interpreter.Program.Variable;
using System.IO;
using System.Collections.Generic;

namespace Bits_Script_Interpreter.Compiler
{
    static class Compile
    {
        /*Use to build the project*/
        private static void BuildApp(Dictionary<string, Var> variabeList, string path) 
        {
            Build.BuildProgram(variabeList, path);
        }

        /*<summary>
         *use to compile all the variable, function, class into a .program or .btsLib file that you can use later
         </summary>*/
        public static void CompileProgram(Dictionary<string, Var> variabeList) 
        {
            if (!Directory.Exists("Project/Bin")) 
            {
                Directory.CreateDirectory("Project/Bin");
            }

            BuildApp(variabeList, "Project/Bin/Build.btsLib");
        }
    }
}
