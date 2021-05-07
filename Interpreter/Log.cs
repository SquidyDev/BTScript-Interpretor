using System;
using Bits_Script_Interpreter.Interpreter.Interpreter_Variable;

namespace Bits_Script_Interpreter.Interpreter.Log
{
    public static class Utilities
    {
        public static void PrintMessage(string message, bool debugOnly) 
        {
            if (debugOnly == true)
            {
                if (Variable.debug == true)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(message);
                }
            }
            else if (debugOnly == false)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(message);
            }
        }

        public static void PrintWarning(string message, bool debugOnly)
        {
            if (debugOnly == true)
            {
                if (Variable.debug == true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(message);
                }
            }
            else if (debugOnly == false)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(message);
            }
        }

        public static void PrintError(string message, bool debugOnly)
        {
            if(debugOnly == true) 
            {
                if(Variable.debug == true) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(message);
                }
            }
            else if(debugOnly == false) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(message);
            }
        }

        public static void PrintMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(message);
        }

        public static void PrintWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
        }

        public static void PrintError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
        }


        public static void PrintError(bool isStopped, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Variable.isError = isStopped;
        }
    }
}
