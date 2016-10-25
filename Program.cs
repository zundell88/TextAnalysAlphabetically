using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextAnalysAlphabetically
{
    class Program
    { //1
        static void Main(string[] args)
        {
            StreamReader boyeStreamReader = null;
            SortedSet<string> ordlista = new SortedSet<string>(File.ReadAllLines("ordlista.txt", Encoding.Default));
            try
            {
                boyeStreamReader = new StreamReader("Boye.txt", Encoding.Default);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Couldn't find the file.");
                Console.ReadLine();
                return;
            }
            var frequencyDictionary = new SortedDictionary<string, int>(StringComparer.CurrentCultureIgnoreCase);
            string lineOfWords;
            while ((lineOfWords = boyeStreamReader.ReadLine()) != null)
            {
                string[] wordArray = ExtractWords(lineOfWords);
                foreach (var word in wordArray)
                {
                    if (word != "")
                    {
                        int n = 0;
                        frequencyDictionary.TryGetValue(word, out n);
                        frequencyDictionary[word] = n + 1;
                    }
                }
            }
            boyeStreamReader.Close();
            foreach (KeyValuePair<string, int> pair in frequencyDictionary)
            {
                bool match = ordlista.Contains(pair.Key);

                if (match == false)
                    Console.WriteLine($"{pair.Key}");
            }
        }
        public static string[] ExtractWords(string inputString)
        {
            char[] charArray = inputString.ToCharArray();
            charArray = Array.FindAll<char>(charArray, (c => (char.IsLetter(c) || c == ' ')));
            string wordString = new string(charArray);
            return wordString.Split(' ');
        }
    }
}
