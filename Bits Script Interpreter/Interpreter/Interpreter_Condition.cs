using Bits_Script_Interpreter.Evaluator.Bool;
using System.Collections.Generic;
using Bits_Script_Interpreter.Program.Variable;
using Bits_Script_Interpreter.Evaluator.String.Bool;
using Bits_Script_Interpreter.Evaluator.String.Core;
using Bits_Script_Interpreter.Interpreter.String;
using Bits_Script_Interpreter.Program.Function;

namespace Bits_Script_Interpreter.Interpreter.Condition
{
    static class Interpreter_Condition
    {
        public static bool? GetConditionResult(string condition)
        {
            if(!Interpreter_String_Evaluator_Core.ContainString(condition))
            {
                Debug.Log("Evaluating for bool", true);
                return Interpreter_Bool_Evaluator.Evaluate(condition);
            }else
            {
                Debug.Log("Evaluating for string", true);
                return Interpreter_String_Bool_Evaluator.Evaluate(condition);
            }
        }

        public static void Condition(string[] lines, int startIndex, string condition, bool isInFunction, string function)
        {
            List<string> scopeVariable = new List<string>();
            string[] statementContent = Interpreter_Condition_Core.ReadBraceContent(lines, startIndex);
            bool? isConditionTrue = GetConditionResult(condition);

            int numberOFLineToJump = 0;

            if(isConditionTrue == true)
            {
                Debug.Log("Expression is true, running code !", true);

                for(int i = 0; i < statementContent.Length; i++)
                {
                    if(Interpreter_Core_Variable.Get().jmp)
                    {
                        if(Interpreter_Core_Variable.Get().id == 0)
                        {
                            Debug.Log("jmp was set to true but id was 0", true);
                            Interpreter.InterpreteLine(statementContent[i], statementContent, i, scopeVariable, true, isInFunction, function);
                            Interpreter_Core_Variable.Set(false, 0);
                        }else
                        {
                            Interpreter_Core_Variable.Set(true, Interpreter_Core_Variable.Get().id - 1);
                            continue;
                        }
                    }else
                    {
                        Interpreter.InterpreteLine(statementContent[i], statementContent, i, scopeVariable, true, isInFunction, function);
                    }
                }
            }

            numberOFLineToJump += statementContent.Length;

            if(statementContent[statementContent.Length - 1].Contains("else"))
            {
                Debug.Log("There is an else !", true);
                string[] elseStatementContent = Interpreter_Condition_Core.ReadBraceContent(lines, startIndex + statementContent.Length);

                if(isConditionTrue == false)
                {
                    for(int i = 0; i < elseStatementContent.Length; i++)
                    {
                        Interpreter.InterpreteLine(elseStatementContent[i], elseStatementContent, i, scopeVariable, true, isInFunction, function);
                    }
                }
                numberOFLineToJump += elseStatementContent.Length;
            }else 
            {
                Debug.Log("There is no else, skipping line(s) !", true);
            }

            Program_Variable.DeleteScopeVariable(scopeVariable);
            scopeVariable.Clear();

            Interpreter_Core_Variable.Set(true, numberOFLineToJump);
        }
    }
}