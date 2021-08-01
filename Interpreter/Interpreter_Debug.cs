using System;
using Bits_Script_Interpreter.Interpreter.Variable;

namespace Bits_Script_Interpreter.Interpreter
{
    /*Contains tool to debug program*/
    public static class Debug
    {
        private static void StopProgram() 
        {
            Error("Program Exited with exit code -1");
            Console.ForegroundColor = ConsoleColor.White;
            Environment.Exit(-1);
        }

        //Print a message
        private static void Print(string message, bool debugOnly) 
        {
            if (debugOnly) 
            {
                if (Interpreter_Variable.debug) 
                {
                    Console.WriteLine("DEBUG : " + message);
                }
            }
            else 
            {
                Console.WriteLine(message);
            }
        }

        public static void Log(string message, bool debugOnly) 
        {
            Console.ForegroundColor = ConsoleColor.White;
            Print(message, debugOnly);
        }

        public static void Warn(string message, bool debugOnly)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Print(message, debugOnly);
        }

        public static void Error(string message, bool debugOnly)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Print(message, debugOnly);
        }

        public static void Log(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Print(message, false);
        }

        public static void Warn(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Print(message, false);
        }

        public static void Error(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Print(message, false);
        }

        public static void Log(bool exitWithError, string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Print(message, false);
        }

        public static void Warn(bool exitWithError, string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Print(message, false);
        }

        public static void Error(bool exitWithError, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Print(message, false);
            StopProgram();
        }
    }
}
