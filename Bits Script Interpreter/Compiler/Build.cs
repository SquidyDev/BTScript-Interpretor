using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Bits_Script_Interpreter.Compiler.Program;
using Bits_Script_Interpreter.Program.Variable;
using System.Collections.Generic;

namespace Bits_Script_Interpreter.Compiler.Builder
{
    static class Build
    {
        public static void BuildProgram(Dictionary<string, Var> variableList, string path) 
        {
            FileStream stream = new FileStream(path, FileMode.OpenOrCreate);

            BinaryFormatter formatter = new BinaryFormatter();

            ProgramBuild build = new ProgramBuild(variableList);

            formatter.Serialize(stream, build);

            stream.Close();
        }
    }
}
