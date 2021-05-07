using System;
using Bits_Script_Interpreter.Interpreter;
using Bits_Script_Interpreter.Interpreter.Interpreter_Variable;
using Bits_Script_Interpreter.Interpreter.Interpreter_Cache;
using Bits_Script_Interpreter.Program.Variable;

namespace BitsSciptInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Bits Script Interpreter " + Variable.version + " Running on " + Variable.os.Platform + " - Version : " + Variable.os.Version);
            Cache.Init("Project/Main.bts", "Project/");
            ProgramVariable.Init();
            Interpreter.Init("Project/__BitsCache__/Main.bts");
            Interpreter.Interprete();

            Console.ReadLine();
        }
    }
}
