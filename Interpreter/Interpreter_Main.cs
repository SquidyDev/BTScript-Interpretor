using System.IO;

namespace Bits_Script_Interpreter.Interpreter.Main
{
    static class Interpreter_Main
    {
        public static string GetMainFileInPath(string path) 
        {
            foreach(var file in Directory.GetFiles(path)) 
            {
                string[] fileLine = File.ReadAllLines(file);
                for (int i = 0; i < fileLine.Length; i++) 
                {
                    if(fileLine[i] == "[script main]") 
                    {
                        return file;
                    }
                }

            }

            return null;
        }
    }
}
