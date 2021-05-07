using System.Collections.Generic;
using Bits_Script_Interpreter.Interpreter.Log;
using Bits_Script_Interpreter.Interpreter.Interpreter_Variable;

namespace Bits_Script_Interpreter.Interpreter.String
{
    static class Interpreter_String
    {
        public static bool ContainsSingleChar(string word, char character) 
        {
            foreach (char wordCharacter in word)
            {
                if(wordCharacter == character) { return true; }
            }
            return false;
        }

        public static bool Contains(string word, int number, char character) 
        {
            bool isComplet = false;
            int numberRef = number;

            for(int i = 0; i < word.Length; i++) 
            {
                if (numberRef == 0)
                {
                    isComplet = true;
                    break;
                }


                if(word[i] == character) 
                {
                    numberRef--;
                    if(numberRef == 0) 
                    {
                        isComplet = true;
                        break;
                    }
                }
            }

            return isComplet;
        }

        public static string GetWord(string word) 
        {
            string output = "";

            for(int i = 0; i < word.Length; i++) 
            {
                if(word[i] == '"') 
                {
                    continue;
                }
                output += word[i];
            }

            return output;
        }

        public static List<string> AssembleLine(string[] lineSection, int lineID) 
        {
            List<string> output = new List<string>();

            for(int i = 0; i < lineSection.Length; i++) 
            {
                if (lineSection[i].Contains('"')) 
                {
                    Utilities.PrintMessage("In at section : " + lineSection[i], true);
                    string charWord = "";
                    for(int j = i; j < lineSection.Length; j++) 
                    {
                        if (lineSection[j].Contains('"')) 
                        {
                            if(j == i) 
                            {
                                Utilities.PrintMessage("A", true);
                                if (Contains(lineSection[i], 2, '"')) 
                                {
                                    charWord += lineSection[j];
                                    output.Add(charWord);
                                    Utilities.PrintMessage("Out at section : " + lineSection[j], true);
                                    i = j + 1;
                                    break;
                                }
                                else 
                                {
                                    charWord += lineSection[j];
                                    Utilities.PrintMessage("Section : " + lineSection[j], true);
                                }
                            }
                            else 
                            {
                                charWord += " ";
                                charWord += lineSection[j];
                                output.Add(charWord);
                                Utilities.PrintMessage("Out at section : " + lineSection[j], true);
                                i = j + 1;
                                break;
                            }                    
                        }
                        else
                        {
                            charWord += " ";
                            charWord += lineSection[j];
                            Utilities.PrintMessage("Section : " + lineSection[j], true);
                        }
                    }
                    if(!Contains(charWord, 2, '"')) 
                    {
                        Utilities.PrintError("Error at Line " + lineID.ToString() + " : missing quote mark", false);
                        Variable.isError = true;
                    }
                }
                else 
                {
                    output.Add(lineSection[i]);
                }
            }

            return output;
        }

        public static string[] ListToArray(List<string> stringList) 
        {
            string[] output = new string[stringList.Count];
            for(int i = 0; i < output.Length; i++) 
            {
                output[i] = stringList[i];
            }
            return output;
        }

        public static string[] ReadContentInBraces(string[] section, int start) 
        {
            List<string> output = new List<string>();

            for(int i = start; i < section.Length; i++) 
            {
                if(section[i] == "}") 
                {
                    break;
                }
                else 
                {
                    output.Add(section[i]);
                }
            }

            return output.ToArray();
        }

        public static int GetLineWhereBraceEnd(string[] section, int start) 
        {
            return ReadContentInBraces(section, start).Length + start;
        }
    }
}
