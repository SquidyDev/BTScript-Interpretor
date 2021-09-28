using System;
using Bits_Script_Interpreter.Interpreter.Variable;
using Bits_Script_Interpreter.Program.Variable;
using Bits_Script_Interpreter.Interpreter;
using Bits_Script_Interpreter.Compiler;

namespace Bits_Script_Interpreter
{
    /// <summary>
    /// BTScript :
    /// an intuitive and easy programming langage
    /// the following is the source code, all the code is commented 
    /// This Code is under Apache 2.0 Liscence (https://www.apache.org/licenses/LICENSE-2.0)
    /// Have fun and create amazing thing with the base code :D
    /// </summary>
    class Class_Main
    {
        /*Default Program Function*/
        static void Main(string[] args)
        {
            Debug.Log($"Bits Script Interpreter - Version {Interpreter_Variable.version} - Running on {Interpreter_Variable.OS.VersionString}");
            string pathToFile;
            string wantToCompile;

            if (Interpreter_Variable.defaultProjectPath == String.Empty)
            {
                Debug.Log("Enter the path to the main .bts file");
                pathToFile = Console.ReadLine();
            }
            else 
            {
                pathToFile = Interpreter_Variable.defaultProjectPath;
            }

            Debug.Log("Do You want to run or build ? (y/n)");
            wantToCompile = Console.ReadLine();

            Debug.Log("Program Runtime :");


            Interpreter_Variable.pathToFile = "Main.bts";
            Debug.Log($"Path to file is {Interpreter_Variable.pathToFile}", true);

            Interpreter_Variable.BuildInterpreter("", pathToFile);

            if(wantToCompile == "y") 
            {
                Compile.CompileProgram(Program_Variable.programVariables);
                Debug.Log("Sucessfully compiled into ProjPath/Bin/Build.btsLib");
            }
        }
    }    
}
