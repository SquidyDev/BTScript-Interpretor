using Bits_Script_Interpreter.Interpreter;
using System;
using System.IO;
using Bits_Script_Interpreter.Interpreter.Variable;
using Bits_Script_Interpreter.Program.Variable;
using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Interpreter.Builtins;
using Bits_Script_Interpreter.Interpreter.Loop;

namespace Bits_Script_Interpreter.Interpreter
{
    struct Interpreter_Core_Response
    {
        public bool jmp;
        public int id;

        public void Contruct(int index, bool jump)
        {
            id = index;
            jmp = jump;
        }
    }

    static class Interpreter_Core_Variable
    {
        private static bool needToJumpLine = false;
        private static int lineJumpIndex = 0;

        public static void Set(bool jump, int index)
        {
            needToJumpLine = jump;
            lineJumpIndex = index;
        }

        public static Interpreter_Core_Response Get()
        {
            Interpreter_Core_Response output = new Interpreter_Core_Response();
            output.Contruct(lineJumpIndex, needToJumpLine);
            return output;
        }
    }

    class Interpreter
    {
        string filePath;
        string file;

        void Interprete()
        {
            string[] fileLine = File.ReadAllLines(file);

            for (int line = 0; line < fileLine.Length; line++)
            {
                if (Interpreter_Core_Variable.Get().jmp)
                {
                    if (Interpreter_Core_Variable.Get().id == line)
                    {
                        Debug.Log("Needing to jump line was set to true but line index was the same, setting off", true);

                        InterpreteLine(fileLine[line], fileLine, line);
                        Interpreter_Core_Variable.Set(false, 0);
                    }
                    else
                    {
                        Debug.Log("Needing to jump line is true, skipping some line", true);
                        Interpreter_Core_Variable.Set(true, Interpreter_Core_Variable.Get().id - 1);
                    }
                }
                else
                {
                    InterpreteLine(fileLine[line], fileLine, line);
                }
            }
        }

        void CheckForFile(string file)
        {
            string[] rawFilePath = file.Split('.');
            string extension = rawFilePath[rawFilePath.Length - 1];

            if (extension == "bts")
            {
                Debug.Log("DEBUG : File extension is .bts", true);
            }
            else
            {
                Debug.Error(true, "ERROR : file extension must be .bts");
            }

            Interprete();
        }

        public Interpreter(string file)
        {
            this.file = file;

            CheckForFile(file);
        }

        public void InterpreteLine(string line, string[] programLine, int lineIndex)
        {
            if(line.StartsWith('$')) Debug.Log("Comment, skipping line", true); return;
            if(line.Trim() == "") Debug.Log("Empty Line, skipping line", true); return;

            string[] lineSection = line.Split(' ');

            if (lineSection[0] == "print")
            {
                Interpreter_Builtins.Print(lineSection);
                return;
            }

            if (lineSection[1] == "=")
            {
                Program_Variable.malloc(lineSection);
                return;
            }
        }
    } 
}
