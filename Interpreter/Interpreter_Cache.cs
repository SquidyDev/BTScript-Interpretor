using System.IO;
using Bits_Script_Interpreter.Interpreter.Main;

namespace Bits_Script_Interpreter.Interpreter.Interpreter_Cache
{
    static class Cache
    {
        public static void PreprocessFile(string pathToMainFile) 
        {
            if(pathToMainFile != null) 
            {
                string[] line = File.ReadAllLines(pathToMainFile);
                
                for(int i = 0; i < line.Length; i++) 
                {
                    string[] word = line[i].Split(' ');
                    
                    for(int j = 0; j < word.Length; j++) 
                    {
                        if (word[j] == "#import")
                        {
                            if(File.Exists("Project/__BitsCache__/" + word[j + 1])) 
                            {
                                using (StreamWriter sw = File.AppendText("Project/__BitsCache__/" + Path.GetFileName(pathToMainFile)))
                                {
                                    sw.Write("\n");
                                    sw.WriteLine(File.ReadAllText("Project/__BitsCache__/" + word[j + 1]));
                                }
                            }else if(File.Exists("Lib/" + word[j + 1])) 
                            {
                                using (StreamWriter sw = File.AppendText("Project/__BitsCache__/" + Path.GetFileName(pathToMainFile)))
                                {
                                    sw.Write("\n");
                                    sw.WriteLine(File.ReadAllText("Lib/" + word[j + 1]));
                                }
                            }
                        }
                    }
                }
            }
        }

        public static void CreateCache(string path, string directoryPath) 
        {
            string[] file = Directory.GetFiles(directoryPath);

            string[] cacheFile = Directory.GetFiles(directoryPath + "__BitsCache__/");


            for(int i = 0; i < cacheFile.Length; i++) 
            {
                File.Delete(cacheFile[i]);
            }

            for (int i = 0; i < file.Length; i++) 
            {
                File.Copy(file[i], directoryPath + "__BitsCache__/" + Path.GetFileName(file[i]));
            }

            PreprocessFile(Interpreter_Main.GetMainFileInPath(directoryPath));
        }

        public static void Init(string path, string directoryPath) 
        {
            if (!Directory.Exists(directoryPath + "__BitsCache__")) 
            {
                Directory.CreateDirectory(directoryPath + "__BitsCache__");
            }

            CreateCache(path, directoryPath);
        }
    }
}
