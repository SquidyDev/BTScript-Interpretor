using Bits_Script_Interpreter.Interpreter.Variable;
using Bits_Script_Interpreter.Interpreter;
using Bits_Script_Interpreter.Interpreter.String;
using System.Collections.Generic;

namespace Bits_Script_Interpreter.Interpreter.Loop
{
    static class Interpreter_Loop
    {
        public static void Loop(int iteration, string[] codeBlocks, int loopStartIndex)
        {
            for (int i = 0; i < iteration; i++)
            {
                for (int j = 0; j < codeBlocks.Length; j++)
                {
                    string current = codeBlocks[j];

                    Debug.Log(current);
                    Debug.Log((loopStartIndex + j).ToString());
                    Debug.Log(Interpreter_String.AssembleArray<string, char>(codeBlocks, 0, ';'));

                    Interpreter_Variable.interpreter.InterpreteLine(current, codeBlocks, loopStartIndex + j);
                    Debug.Log($"Interpreted line : {current}", true);
                }
            }

            Debug.Log("DEBUG : Ended loop.", true);
        }

        public static string[] ReadLoopContent(string[] BLOCK, int startIndex)
        {
            List<string> output = new List<string>();

            for(int i = startIndex; i < BLOCK.Length; i++) 
            {
                //Contains the current block of the loop
                string current = BLOCK[i];

                if(current.Split(' ')[0] == "loop") 
                {
                    output.Add(current);

                    string[] loopBlock = ReadLoopContent(BLOCK, startIndex + i + 1);

                    foreach(string block in loopBlock) 
                    {
                        if(block == "start_loop") 
                        {
                            output.Add("start_loop");
                        }

                        output.Add(block);
                    }

                    output.Add("end_loop");
                }else if(current == "end_loop") 
                {
                    break;
                }
                else 
                {
                    output.Add(current);
                }
            }

            output.RemoveAt(0);

            return output.ToArray();
        }
    }
}
