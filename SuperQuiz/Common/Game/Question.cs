using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperQuiz.Common.Game
{
    public class Question
    {
        public string Quest { get; set; } = "";
        public List<String> Answers { get; set; } = new();
        public List<String> RightAnswers { get; set; } = new();

        public void Set()
        {
            Console.Write("\n\t\t\t\t\t\tQuestion: ");
            Quest = Console.ReadLine()!;
            if (Quest.Length == 0) { Quest = Console.ReadLine()!; }

            Console.Clear();
            Console.WriteLine("\n\n\t\t\t\t\t  ------------| SET QUESTION |------------");
            Console.WriteLine("\n\t\t\t\t\t   When you're done, write \"end\" for quit");
            Console.WriteLine("\t\t\t\t\t\t Write answer for question");
            bool EXT = true;
            while (EXT != false)
            {
                Console.Write("\n\t\t\t\t\t    Enter: ");
                String? stop = Console.ReadLine()!;
                if (stop == "end") { EXT = false; }
                else { Answers.Add(stop); }
                for (int i = 0; i < Answers.Count; i++) {
                    Console.WriteLine($"\t\t\t\t\t       [{i}] {Answers[i]}");
                }
            }

            Console.Clear();
            Console.WriteLine("\n\n\t\t\t\t\t  ------------| SET ANSWER |------------");
            Console.WriteLine("\n\t\t\t\t\t  When you're done, write \"end\" for quit");
            Console.WriteLine("\t\t\t\t\tChoose number of answer for question");
            EXT = true;
            while (EXT != false)
            {
                for (int i = 0; i < Answers.Count; i++) {
                    Console.Write($"\n\t\t\t\t\t       [{i}] {Answers[i]}");
                }
                Console.Write("\n\t\t\t\t\tEnter: ");
                String? stop = Console.ReadLine()!;
                if (stop == "end") { EXT = false; }
                else { RightAnswers.Add(stop); }

                for (int i = 0; i < RightAnswers.Count; i++) {
                    Console.Write(Answers[int.Parse(RightAnswers[i])]);
                }
            }
        }
        public override string ToString()
        {
            String? str = $"Question: {Quest}\n";
            for (int i = 0; i < Answers.Count; i++)
            {
                str += $"[{i}] {Answers[i]}";
                for (int j = 0; j < RightAnswers.Count; j++)
                {
                    if (i == int.Parse(RightAnswers[j])) { str += " +"; }
                }
                str += '\n';
            }
            return str;
        }
    }
}
