using System;
using System.Collections.Generic;
using Bits_Script_Interpreter.Program.Variable;

namespace Bits_Script_Interpreter.Interpreter.String
{
    /*List of usefull function
     * (The name string has been given at the beginning but this class is not all about string)
     */
    static class Interpreter_String
    {
        //Assemble an array with a separator
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

        //Assemble an array without a separator
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

        //Assemble a list with a separator
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
        
        //Convert an Array to a list
        //WARNING this method is decapted use the method arrayName.ToList(); instead
        public static List<T> ArrayToList<T>(T[] array)
        {
            List<T> output = new List<T>();

            foreach(T t in array)
            {
                output.Add(t);
            }

            return output;
        }

        //Check if a char is a number
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

        //Remove the first null terminaison character of a string
        //Usable for exemple if the string has this problem "Hello this is a string \0 with problems.\0" The method will remove the first \0
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

        //Keep only the int in a string (ex : "12a34" when converted become : "1234")
        public static string RemoveNonInt(string withInt) 
        {
            string output = "";

            foreach(char c in withInt) 
            {
                if (isNumber(c)) { output += c; }
            }

            return output;
        }

        //use to remove a specific char from a string
        public static string Remove(string baseStr, char removeChar) 
        {
            string output = "";

            foreach(char character in baseStr) 
            {
                if(character != removeChar) { output += character; }
            }

            return output;
        }

        //Count how many time there is a character in a string
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

        //Remove " from a string
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

        //Check if an array contain an element of the same type
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

        //Use to Merge a List and a array
        public static void AddArrayToList<T>(List<T> list, T[] array)
        {
            foreach(T element in array)
            {
                list.Add(element);
            }
        }

        //Remove a specific element in an array
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

        //Use to read the content in brace ({})
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

        //Use to trim an array
        public static string[] TrimAllArray(string[] array)
        {
            for(int i = 0; i < array.Length; i++)
            {
                array[i].Trim();
            }

            return array;
        }

        //Use to remove character from a string array
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

        //Use to Combine to dictionnary
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

        //Use to get all the element in an array until a special element is encounter
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
