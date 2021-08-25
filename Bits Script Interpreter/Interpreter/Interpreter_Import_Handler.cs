using System.IO;
using Bits_Script_Interpreter.Interpreter;
using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Program.Variable;

namespace Bits_Script_Interpreter.Interpreter.Import_Handler
{
    static class Interpreter_Import_Handler
    {
        public static void Import(string[] line)
        {
            string importName = Interpreter_String.AssembleArray<string, char>(line, 1, ' ');

            string[] fileImportContent = File.ReadAllLines("Project/" + importName.Trim());

            for(int i = 0; i < fileImportContent.Length; i++)
            {
                string current = fileImportContent[i];

                if(Interpreter_Core_Variable.Get().jmp)
                {
                    if(Interpreter_Core_Variable.Get().id == 0)
                    {
                        Debug.Log("Neeed to jump line was set true but line index was 0, setting off", true);
                        Interpreter.InterpreteLine(current, fileImportContent, i, null, false, false, null);
                        Interpreter_Core_Variable.Set(false, 0);
                    }else
                    {
                        Debug.Log("Neeed to jump line was true, index is not 0, skipping line", true);
                    }
                }else
                {
                    Interpreter.InterpreteLine(current, fileImportContent, i, null, false, false, null);
                }
            }
        }
    }
}