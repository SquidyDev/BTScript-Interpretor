using System;

namespace Bits_Script_Interpreter.Interpreter.Variable
{
    static class Interpreter_Variable
    {
        public static Interpreter interpreter; //Instance of the interpreter
        public static string version = "Alpha"; //Version of the interpreter
        public static bool debug = false; //If we are in debug mode
        public static bool isError = false; //Obsolete use Debug.Error(true, MESSAGE) instead
        public static string pathToFile; //The path to the main file
        public static string firstFileToInterpreter; //The name of the main file
        public static OperatingSystem OS = Environment.OSVersion; //The Version of the OS (UNIX, Windows...)
        public static string defaultProjectPath = "Project/Main.bts"; //The default project path
        public static string[] nullArgument = { "null" }; //The null argument

        //Use to instanciate the interpreter and generate base stuff
        public static void BuildInterpreter(string filePath, string file) 
        {
            pathToFile = filePath + "/";
            firstFileToInterpreter = file;

            interpreter = new Interpreter(file);
        }
    }
}

