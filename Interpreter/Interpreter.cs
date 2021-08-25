using System.IO;
using Bits_Script_Interpreter.Program.Variable;
using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Interpreter.Builtins;
using Bits_Script_Interpreter.Interpreter.Loop;
using Bits_Script_Interpreter.Interpreter.Condition;
using Bits_Script_Interpreter.Interpreter.Import_Handler;
using System.Collections.Generic;
using Bits_Script_Interpreter.Program.Function;

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
            Debug.Log($"Setted need to jump line to {needToJumpLine}, lineJumpIndex is {lineJumpIndex}", true);
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
                    if (Interpreter_Core_Variable.Get().id == 0)
                    {
                        Debug.Log("Needing to jump line was set to true but line index was the same, setting off", true);

                        InterpreteLine(fileLine[line], fileLine, line, null, false, false, null);
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
                    InterpreteLine(fileLine[line], fileLine, line, null, false, false, null);
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

        public static void InterpreteLine(string line, string[] programLine, int lineIndex, List<string> scopeVariable, bool isScopped, bool isFunction, string function)
        {
            try 
            {
                if(line.StartsWith('$'))
                { 
                    Debug.Log("Comment, skipping line", true); 
                    return;
                }

                if(line.Trim() == "")
                { 
                    Debug.Log("Empty Line, skipping line", true); 
                    return;
                }

                line = line.TrimStart();

                string[] lineSection = line.Split(' ');

                if(lineSection[0] == "}") return;

                if (lineSection[0] == "print")
                {
                    Interpreter_Builtins.Print(lineSection);
                    return;
                }

                if(lineSection[0] == "loop")
                {
                    int numberLoop = 0;
                    if(int.TryParse(lineSection[1], out int n))
                    {
                        Debug.Log("Using a number for loop !", true);
                        numberLoop = int.Parse(lineSection[1]);
                    }else if(Program_Variable.Exist(lineSection[1]) && Program_Variable.IsType(lineSection[1], "double"))
                    {
                        Debug.Log("Using a variable for loop !", true);
                        numberLoop = int.Parse(Program_Variable.GetVariable(lineSection[1], true).value.ToString());
                    }else
                    {
                        Debug.Error(true, "ERROR : loop argument : non existing variable or wrong variable type.");
                    }

                    Debug.Log($"Detected loop, number iteration : {numberLoop}", true);

                    Interpreter_Loop.Loop(programLine, lineIndex, numberLoop, isFunction, function);   
                }

                if(lineSection[0] == "if")
                {
                    string condition = Interpreter_String.AssembleArray<string, char>(lineSection, 1, ' ');

                    Interpreter_Condition.Condition(programLine, lineIndex, condition, isFunction, function);
                }

                if(lineSection[0] == "import")
                {
                    Interpreter_Import_Handler.Import(lineSection);
                }

                if (lineSection[1] == "=")
                {
                    Program_Variable.malloc(lineSection, scopeVariable, isScopped);
                    return;
                }
            
                if(lineSection[0] == "calleable")
                {
                    Debug.Log("Detected Function", true);
                    Program_Function.AddFunction(lineSection[1], Interpreter_String.GetArrayUntil<string, string>(lineSection, 2, "{"), Interpreter_String.ReadBraceContent(programLine, lineIndex));
                }

                if(lineSection[0] == "call")
                {
                    string name = lineSection[1];
                    string[] argumentsValue = Interpreter_String.AssembleArray<string, char>(lineSection, 2, ' ').Split('$');

                    for(int i = 0; i < argumentsValue.Length; i++)
                    {
                        argumentsValue[i] = argumentsValue[i].Trim();
                    } 

                    Debug.Log($"Called function {name} with arguments {Interpreter_String.AssembleArray<string, string>(argumentsValue, 0, " $ ")}", true);
                
                    Program_Function.Run(name, argumentsValue);
                }

                if(lineSection[0] == "return") 
                {
                    Calleable func = Program_Function.GetFunction(function);
                    func.stopRunning = true;
                        
                    string returnValue = Interpreter_String.AssembleArray<string, char>(lineSection, 1, ' ').Trim();

                    if (Program_Variable.Exist(returnValue)) 
                    {
                        func.ReturnValue = Program_Variable.GetVariable(returnValue, true).value;
                    }else if(lineSection[1] == "call")
                    {
                        string name = lineSection[2];
                        string[] argumentsValue = Interpreter_String.AssembleArray<string, char>(lineSection, 3, ' ').Split('$');

                        for (int i = 0; i < argumentsValue.Length; i++)
                        {
                            argumentsValue[i] = argumentsValue[i].Trim();
                        }

                        Debug.Log($"Called function {name} with arguments {Interpreter_String.AssembleArray<string, string>(argumentsValue, 0, " $ ")}", true);

                        object val = Program_Function.Run(name, argumentsValue);

                        func.ReturnValue = val;
                    }
                    else 
                    {
                        func.ReturnValue = returnValue;
                    }
                }
            }catch
            {
                return;
            }
        }
    } 
}
