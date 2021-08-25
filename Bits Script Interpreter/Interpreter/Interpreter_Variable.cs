using System;

namespace Bits_Script_Interpreter.Interpreter.Variable
{
    static class Interpreter_Variable
    {
        public static Interpreter interpreter;
        public static string version = "Alpha";
        public static bool debug = false;
        public static bool isError = false; //Obsolete use Debug.Error(true, MESSAGE) instead
        public static string pathToFile;
        public static string firstFileToInterpreter;
        public static OperatingSystem OS = Environment.OSVersion;
        public static string defaultProjectPath = "Project/Main.bts"; 
        
        public static void BuildInterpreter(string filePath, string file) 
        {
            pathToFile = filePath + "/";
            firstFileToInterpreter = file;

            interpreter = new Interpreter(file);
        }
    }
}

