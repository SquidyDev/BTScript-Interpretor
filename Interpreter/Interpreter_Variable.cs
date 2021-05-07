using System;

namespace Bits_Script_Interpreter.Interpreter.Interpreter_Variable
{
    static class Variable
    {
        public static bool debug = false;
        public static string version = "1.0";
        public static OperatingSystem os = Environment.OSVersion; 
        public static bool isError = false;
        public static bool needToJumpLine = false;
        public static int lineToJumpIndex = 0;
    }
}
