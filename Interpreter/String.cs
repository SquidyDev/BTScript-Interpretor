using System;
using System.Collections.Generic;
using Bits_Script_Interpreter.Program.Variable;

namespace Bits_Script_Interpreter.Interpreter.String
{
    static class Interpreter_String
    {
        public static T AssembleArray<T, U>(T[] array, int begin, U separator) 
        {
            T output = default(T);

            for(int i = begin; i < array.Length; i++) 
            {
                dynamic add1 = array[i], add2 = separator;

                output += add1;
                output += add2;
            }

            return output;
        }

        public static T AssembleArray<T>(T[] array, int begin)
        {
            T output = default(T);

            for(int i = begin; i < array.Length; i++)
            {
                dynamic add1 = array[i];
                output += add1;
            }

            return output;
        }

        public static T AssembleList<T, U>(List<T> list, int begin, U separator)
        {
            T output = default(T);

            for(int i = begin; i < list.Count; i++)
            {
                dynamic add1 = list[i], add2 = separator;

                output += add1;
                output += add2;
            }

            return output;
        }
        
        public static List<T> ArrayToList<T>(T[] array)
        {
            List<T> output = new List<T>();

            foreach(T t in array)
            {
                output.Add(t);
            }

            return output;
        }

        public static bool isNumber(char c) 
        {
            try 
            {
                Convert.ToInt32(c);

                return true;
            }
            catch 
            {
                return false;
            }
        }

        public static string RemoveFirstZero(string withZero) 
        {
            string output = "";

            for (int i = 0; i < withZero.Length; i++) 
            {
                if(withZero[0] == 0) 
                {
                    for(int j = 1; j < withZero.Length; j++) 
                    {
                        output += withZero[j];
                    }
                }
            }

            return output;
        }

        public static string RemoveNonInt(string withInt) 
        {
            string output = "";

            foreach(char c in withInt) 
            {
                if (isNumber(c)) { output += c; }
            }

            return output;
        }

        public static string Remove(string baseStr, char removeChar) 
        {
            string output = "";

            foreach(char character in baseStr) 
            {
                if(character != removeChar) { output += character; }
            }

            return output;
        }

        public static bool HowManyTime(string value, char detected, int time, bool acceptGreater) 
        {
            int numberOfTime = 0;

            foreach(char character in value) 
            {
                if(character == detected) { numberOfTime++; Debug.Log("DEBUG : Added one to numberOfTime", true); }
            }

            if (numberOfTime == time) return true;
            else if (numberOfTime < time) return false;

            if (numberOfTime > time && acceptGreater) return true;
            else return false;
        }

        public static string RemoveQuoteMarks(string withQuoteMarks) 
        {
            string output = "";

            foreach(char character in withQuoteMarks) 
            {
                if(character == '"') { continue; }
                else { output += character; }
            }

            return output;
        }

        public static bool ArrayContains<T>(T[] array, T element)
        {
            dynamic e = element;
            foreach(T t in array) 
            {
                dynamic value  = t; 
                if(value == e) return true;
            }
            return false;
        }

        public static void AddArrayToList<T>(List<T> list, T[] array)
        {
            foreach(T element in array)
            {
                list.Add(element);
            }
        }

        public static T[] RemoveAllInArray<T>(T[] array, T element)
        {
            List<T> output = new List<T>();
            dynamic e = element;
            dynamic current = default(T);

            foreach(T t in array)
            {
                current = t;

                if(current == e)
                {
                    continue;
                }

                output.Add(t);
            }

            return output.ToArray();
        }

        public static string[] ReadBraceContent(string[] lines, int startIndex)
        {
            List<string> output = new List<string>();

            int backBraceBeforeEnding = 0;

            for(int i = startIndex + 1; i < lines.Length; i++)
            {
                string current = lines[i];

                output.Add(current.TrimStart());

                if(current.Contains('{')) backBraceBeforeEnding++;

                if(current.Contains('}'))
                {
                    if(backBraceBeforeEnding != 0)
                    {
                         backBraceBeforeEnding--;
                         continue;
                    }else
                    {
                        break;
                    }
                }
            }

            return output.ToArray();
        }

        public static string[] TrimAllArray(string[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                array[i].Trim();
            }

            return array;
        }

        public static string[] RemoveAllCharInArray(string[] array, char character)
        {
            List<string> output = new List<string>();

            for(int i = 0; i < array.Length; i++)
            {
                string current = array[i];
                string next = "";
                for(int j = 0; j < current.Length; j++)
                {
                    if(current[j] == character) continue;
                    next += current[j];
                }
                output.Add(next);
            }

            return output.ToArray();
        }

        public static Dictionary<T, U> CombineDict<T, U>(Dictionary<T, U> a, Dictionary<T, U> b)
        {
            Dictionary<T, U> output = new Dictionary<T, U>();

            foreach (KeyValuePair<T, U> v in a)
            {
                output.Add(v.Key, v.Value);
            }

            foreach(KeyValuePair<T, U> v in b)
            {
                output.Add(v.Key, v.Value);
            }

            return output;
        }

        public static T[] GetArrayUntil<T, U>(T[] baseArray, int startIndex, U stopAt)
        {
            List<T> output = new List<T>();
            dynamic stopAtDynamic = stopAt;


            for(int i = startIndex; i < baseArray.Length; i++)
            {
                if((dynamic)baseArray[i] == stopAtDynamic)
                {
                    break;
                }else
                {
                    output.Add(baseArray[i]);
                }
            }

            return output.ToArray();
        }
    }
}
