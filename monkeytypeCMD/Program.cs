using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace monkeytypeCMD
{
    internal class Program
    {
        private static Words dictionaryWords = new Words();
        private static bool isStarted = false;
        private static int msCounter = 0;
        private static Stopwatch timer = new Stopwatch();
        static void Main(string[] args)
        {
            int amoutOfWords = 10;
            TimeSpan typingTime = new TimeSpan();
            if (isStarted == false)
            {
                Console.WriteLine("press enter to start!");
                while (true)
                {
                    if (Console.ReadKey().KeyChar == (char)ConsoleKey.Enter)
                    {
                        Console.Clear();
                        timer.Start();
                        
                        isStarted = true;
                        break;
                    }
                    continue;
                }
            }
            string wordslist = GenerateWords(amoutOfWords);
            string enteredWord = "";
            Console.WriteLine(wordslist);
            int counter = 0;
            while (true)
            {
                if (enteredWord.Length >= wordslist.Length - 1)
                {
                    break;
                }
                char curChar = Console.ReadKey().KeyChar;
                if (curChar == (char)ConsoleKey.Backspace && counter > 0)
                {
                    Console.SetCursorPosition(counter - 1, 1);
                    Console.Write(" ");
                    enteredWord = DeleteLastChar(enteredWord);
                    counter--;
                    Console.SetCursorPosition(counter, 1);
                    if (ContainsError(counter, wordslist, enteredWord))
                        continue;
                    Console.ForegroundColor = ConsoleColor.White;
                    continue;
                }
                enteredWord += curChar;
                counter++;
                if (ContainsError(counter, wordslist, enteredWord))
                {
                    Console.SetCursorPosition((counter - 1), 1);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(curChar);
                    Console.SetCursorPosition((counter), 1);
                    continue;
                }
                Console.ForegroundColor = ConsoleColor.White;
            }
            timer.Stop();
            typingTime = timer.Elapsed;
            Console.ForegroundColor = ConsoleColor.White;
            
            Console.WriteLine("");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("gratz! Typing finished in:");
            Console.WriteLine(typingTime.TotalSeconds.ToString("#.##") + "seconds!");
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("your WPM is " + (amoutOfWords / (typingTime.TotalSeconds / 60)).ToString("#.##"));
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("your CPM is " + ((amoutOfWords / (typingTime.TotalSeconds / 60)) * 5).ToString("#.##"));
            Console.ReadLine();
        }

        private static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            msCounter = e.SignalTime.Millisecond;
        }

        private static string GenerateWords(int amountOfWords)
        {
            int maxRnd = dictionaryWords.WordsArray.Length - 1;
            string words = "";
            Random rand = new Random();
            
            for (int i = 0; i < amountOfWords; i++)
            {
                words += dictionaryWords.WordsArray[(int)rand.Next(0, maxRnd)] + " ";
            }
            return words;
        }
        private static bool ContainsError(int counter, string wordsString, string currentWord) 
        {
            if (counter <= 0)
                return false;
            if (wordsString[counter-1] == currentWord[counter-1])
                return false;
            return true;
        }
        private static string DeleteLastChar(string fullWord)
        {
            string wordToReturn = string.Empty;

            for (int i = 0; i < fullWord.Length - 1; i++)
                wordToReturn += fullWord[i];
            return wordToReturn;
        }
    }
}
