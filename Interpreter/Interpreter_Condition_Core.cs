using System;
using System.Linq;
using System.Collections.Generic;

namespace Bits_Script_Interpreter.Interpreter.Condition 
{
    static class Interpreter_Condition_Core
    {
        public static string[] ReadBraceContent(string[] lines, int startIndex)
        {
            List<string> output = new List<string>();

            int backBraceBeforeEnding = 0;

            for(int i = startIndex + 1; i < lines.Length; i++)
            {
                string current = lines[i];

                output.Add(current.TrimStart());

                if(current.Contains('}'))
                {
                    if(current.Split(' ').Contains("else"))
                    {
                        if(backBraceBeforeEnding == 0)
                        {
                            break;
                        }else
                        {
                            backBraceBeforeEnding++;
                        }
                    }

                    if(backBraceBeforeEnding != 0)
                    {
                         backBraceBeforeEnding--;
                         continue;
                    }else
                    {
                        break;
                    }
                }

                if(current.Contains('{')) backBraceBeforeEnding++;
            }

            return output.ToArray();
        }
    }
}