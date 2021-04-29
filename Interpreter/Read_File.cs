using System.IO;

namespace Bits_Script_Interpreter.Read_File
{
    public static class ReadFile
    {
        public static string[] GetLine(string filePath) 
        {
            return File.ReadAllLines(filePath);
        }
    }
}
