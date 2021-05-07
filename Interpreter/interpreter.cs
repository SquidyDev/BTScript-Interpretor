using Bits_Script_Interpreter.Interpreter.Log;
using Bits_Script_Interpreter.Read_File;
using Bits_Script_Interpreter.Interpreter.Interpreter_Variable;
using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Program.Variable;
using Bits_Script_Interpreter.Interpreter.Keyword;
using Bits_Script_Interpreter.Program.Function;
using System;
using System.IO;

namespace Bits_Script_Interpreter.Interpreter
{
    static class Interpreter
    {
        static string currentFile;

        public static void InterpreteLine(string line, int lineIndex, Function func, string[] lineList, int currentLineIndex) 
        {
            bool isFuncVariable = func == null ? false : true;

            string[] programSection = line.Split(' ');
            string[] section = Interpreter_String.ListToArray(Interpreter_String.AssembleLine(programSection, lineIndex));

            if(Variable.debug)
                foreach(string s in section) 
                {
                    Utilities.PrintMessage(s, true);
                }

            if(Variable.debug)
                Console.WriteLine("--------------------------------------------");

            for(int i = 0; i < section.Length; i++) 
            {
                if (!Variable.isError)
                {
                    if(section[i] == "\n" || section[i] == "" || section[i] == " ")  
                    {
                        break;
                    }

                    if (section[i] == "#import") //Check for preprocessor 
                    {
                        break;
                    }

                    if (section[i] == "PrintLine")
                    {
                        if (ProgramVariable.Exists(section[i + 1], isFuncVariable, func))
                        {
                            if (ProgramVariable.GetVariable(section[i + 1], isFuncVariable, func).IsType("string"))
                            {
                                Console.WriteLine(Interpreter_String.GetWord(ProgramVariable.GetVariable(section[i + 1], isFuncVariable, func).value.ToString()));
                            }
                            else
                            {
                                Utilities.PrintError("Error at line : " + i.ToString() + " : Print only Accept string, not " + ProgramVariable.variable[section[i + 1]].type + ".", false);
                            }
                        }
                        else
                        {
                            Console.WriteLine(Interpreter_String.GetWord(section[i + 1]));
                        }
                        break;
                    } else if (section[i] == "Print")
                    {
                        if (ProgramVariable.Exists(section[i + 1], isFuncVariable, func))
                        {
                            if (ProgramVariable.GetVariable(section[i + 1], isFuncVariable, func).IsType("string"))
                            {
                                Console.WriteLine(Interpreter_String.GetWord(ProgramVariable.GetVariable(section[i + 1], isFuncVariable, func).value.ToString()));
                            }
                            else
                            {
                                Utilities.PrintError("Error at line : " + i.ToString() + " : Print only Accept string, not " + ProgramVariable.variable[section[i + 1]].type + ".", false);
                            }
                        }
                        else
                        {
                            Console.Write(Interpreter_String.GetWord(section[i + 1]));
                        }
                        break;
                    }else if(section[i] == "[script"){ break; }

                    try
                    {
                        if(section[i + 2] == "=") 
                        {
                            if (ProgramVariable.IsType(section[i])) 
                            {
                                if(!ProgramVariable.Exists(section[i + 1], isFuncVariable, func)) 
                                {
                                    if (ProgramVariable.VariableTypeMatchVariableValue(section[i], (object)section[i + 3]))
                                    {
                                        if (!kword.IsKeyword(section[i + 1]))
                                        {
                                            ProgramVariable.AddVariable(section[i + 1], section[i + 3], isFuncVariable, func, section[i]);
                                            break;
                                        }
                                        else 
                                        {
                                            Utilities.PrintError("Error at line " + i + " Cannot create a variable that as the name of a keyWord", false);
                                            Variable.isError = true;
                                        }
                                    }
                                    else 
                                    {
                                        Utilities.PrintError("Error at line " + i.ToString() + ": Variable : " + section[i + 1] + " is type : " + section[i] + " but it's value is not a : " + section[i] + ".", false);
                                        Variable.isError = true;
                                    }
                                    break;
                                }
                                else 
                                {
                                    Utilities.PrintError("Cannot create a variable already defined (variable : " + section[i + 1] + " is already defined).", false);
                                    Variable.isError = true;
                                    break;
                                }
                            }
                            else 
                            {
                                Utilities.PrintError("Undiefined type " + section[i] + " at line " + i.ToString() + " (" + line + ").", false);
                                Variable.isError = true;
                                break;
                            }
                        }
                        else if(section[i + 1] == "=") 
                        {
                            if (ProgramVariable.Exists(section[i], isFuncVariable, func)) 
                            {
                                ProgramVariable.SetVariable(section[i], section[i + 2], isFuncVariable, func, ProgramVariable.GetVariable(section[i], isFuncVariable, func).type);
                            }
                            else 
                            {
                                Utilities.PrintError("Error at line " + i + " : variable " + section[i] + ", is not defined, always define a variable before using it.", false);
                                Variable.isError = true;
                                break;
                            }
                        }
                    }
                    catch 
                    {
                        
                    }

                    if (section[i] == "function")
                    {
                        if (!kword.IsKeyword(section[i + 1]))
                        {
                            if (!Func.Exists(section[i + 1]))
                            {
                                Func.AddFunction(new Function(section[i + 1], Interpreter_String.ReadContentInBraces(lineList, currentLineIndex + 1)));
                                Variable.needToJumpLine = true;
                                Variable.lineToJumpIndex = Interpreter_String.GetLineWhereBraceEnd(lineList, currentLineIndex + 1);
                                Utilities.PrintMessage($"[DEBUG] : Created function : {Func.GetFunction(section[i + 1]).ToString()}, skipping until line {Variable.lineToJumpIndex}", true);
                                break;
                            }
                            else
                            {
                                Utilities.PrintError(true, $"Error at line {lineIndex}, Cannot create a function thats already exist (function : {section[i + 1]} Already Exist).");
                            }
                        }
                        else
                        {
                            Utilities.PrintError(true, $"Error at line {lineIndex}, Cannot create a function thats as the same name then a keyword.");
                        }
                    }

                    if (kword.IsKeyword(section[i]) == false && kword.isNotSymbol(section[i]) == false)
                    {
                        Utilities.PrintMessage($"section[i] is not a kword (section : {section[i]})", true);
                        if (Func.Exists(section[i]))
                        {
                            if (Func.HasFunctionArs(section, i + 1))
                            {
                                Utilities.PrintMessage($"[DEBUG] : Started Function named {section[i]}", true);
                                Func.GetFunction(section[i]).Run();
                                break;
                            }
                            else 
                            {
                                Utilities.PrintError(true, $"Error at line {lineIndex}, missing () or missing argument when calling the function {section[i]}");
                                break;
                            }
                        }
                        else
                        {
                            Utilities.PrintError(true, $"Error at line {lineIndex}, Cannot call a function that doesn't exist (function : {section[i]} doesn't exist) (line : {line}).");
                        }
                    }
                }
            }
        }

        public static void Interprete() 
        {
            //Check if File Exist
            if (!File.Exists(currentFile)) 
            {
                Utilities.PrintError("Error I0003 : File Must exist to run.", false);
                Variable.isError = true;
                return;
            }

            //Check if file as at least 1 line
            string[] line = ReadFile.GetLine(currentFile);
            if(line.Length == 0) 
            {
                Utilities.PrintError("Error I0002 : File must have at least 1 line to compile .", false);
                Variable.isError = true;
            }

            for(int lineIndex = 0; lineIndex < line.Length; lineIndex++) 
            {
                if (!Variable.needToJumpLine)
                {
                    Utilities.PrintMessage("[DEBUG] : needToJumpLine was set to False.", true);
                    InterpreteLine(line[lineIndex], lineIndex + 1, null, line, lineIndex);
                }
                else 
                {
                    if(Variable.lineToJumpIndex == lineIndex) 
                    {
                        Utilities.PrintMessage($"[DEBUG] : End Of Line Skipping (line : {lineIndex}).", true);
                        Variable.needToJumpLine = false;
                    }
                    else 
                    {
                        Utilities.PrintMessage($"[DEBUG] : Skipping Line (line : {lineIndex}) ...", true);
                    }
                }
            }

            if (Func.FunctionList.ContainsKey("Main"))
            {
                Func.FunctionList["Main"].Run();
            }
            else 
            {
                Utilities.PrintError("Error : File don't contain Main function...");
            }
        }

        public static void Init(string filePath) 
        {
            //Check if file extension is .bts
            string[] extension = filePath.Split(".");
            if (extension[extension.Length-1] != "bts") 
            {
                Utilities.PrintError("Error I0001 : File must have the extension .bts", false);
                Variable.isError = true;
                return;
            }


            currentFile = filePath;
        }
    }
}
