using Bits_Script_Interpreter.Interpreter.Log;

namespace Bits_Script_Interpreter.Interpreter.Keyword
{
    static class kword
    {
        static string[] keywords = { 
            "Print",
            "PrintLine",
            "string",
            "char",
            "double",
            "bool",
            "int",
            "false",
            "true"
        };

        public static void KeywordList() 
        {
            foreach(string keyword in keywords) { Utilities.PrintMessage(keyword, false); }
        }

        public static bool IsKeyword(string key) 
        {
            foreach(string keyword in keywords) 
            {
                if(keyword == key) { return true; }
            }
            return false;
        }
    }
}
