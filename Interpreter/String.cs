using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
