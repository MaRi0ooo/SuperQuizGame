using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperQuiz.Common;
using SuperQuiz.Common.Game;
using SuperQuiz.Common.Account;
using System.Text.Json;

namespace SuperQuiz.Common
{
    public class Quiz
    {
        public string Name { get; set; } = "";
        public List<Question> Quizz { get; set; } = new();
        public Dictionary<String, int> Top20 { get; set; } = new();
        private static void ShowMenuEditQuiz()
        {
            Console.WriteLine("[1] - Edit question");
            Console.WriteLine("[2] - Edit answers");
            Console.WriteLine("[3] - Edit correct answers");
            Console.WriteLine("[4] - Set all new");
            Console.WriteLine("[0] - Back to edit quiz");
        }
        public void Start(User user, bool rnd = false)
        {
            Console.Clear();
            int countOfRightAnswers = 0;
            Console.WriteLine($"\n\t\t\t\t\t\t       {Name}");

            foreach (var question in Quizz) {
                bool RIGHT = true, EXT = true;
                List<String> userAnswers = new();

                Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------");
                
                Console.WriteLine($"\t\t\t\t\tQuestion: {question.Quest}");
                Console.WriteLine("\t\t\t\t\tAnswers:");
                for (int i = 0; i < question.Answers.Count; i++) {
                    Console.WriteLine($"\t\t\t\t\t[{i}] {question.Answers[i]}");
                }
                
                Console.WriteLine("\n------------------------------------------------------------------------------------------------------------------------");
                
                Console.WriteLine("\tWrite the number of correct answers, if there are more than 1 answers, enter the number then press Enter");
                Console.WriteLine("\t\t\t\tWhen you're done writing your answer(s), write \"end\"");
                Console.Write("\n\t\tEnter: ");

                while (EXT != false)
                {
                    String? stop = Console.ReadLine()!;
                    if (stop == "end") { EXT = false; }
                    else { userAnswers.Add(stop); }
                }
                foreach (var rightAnswers in userAnswers) {
                    if (!question.RightAnswers.Contains(rightAnswers)) { RIGHT = false; }
                }
                if (RIGHT) { countOfRightAnswers++; }
            }

            Console.WriteLine($"{countOfRightAnswers}/{Quizz.Count} are right");
            user.quizHistory.Add($"{countOfRightAnswers}/{Quizz.Count} are right | Quiz: {Name}");

            String? point = "point.txt",
                path = new FileInfo(point).FullName.Replace("point.txt", $"Users\\{user.Login}.json");

            FileStream fsc = new(path, FileMode.Create);
            JsonSerializer.Serialize(fsc, user, new JsonSerializerOptions { WriteIndented = true }); fsc.Close();

            if (!rnd) { LeaderBoard(user, countOfRightAnswers); }
            Console.WriteLine("Press any key..."); Console.ReadKey();
        }
        public static void EditQuiz(User user, Quiz quiz)
        {
            Menu menu = new();
        STARTEDIT:
            Console.Clear();
            Console.WriteLine("Edit Quiz");
            Console.Write("Type \"end\" to exit test editing mode, \nor press Enter to edit a test: ");
            if (Console.ReadLine() == "end") { quiz.LoadToFile(quiz, false); menu.QuizMenu(user); }

            Console.Clear();
            Console.WriteLine("Edit Quiz");
            Console.WriteLine("[+] Means correct question\n");
            for (int i = 0; i < quiz.Quizz.Count; i++) {
                Console.WriteLine($"{i}. {quiz.Quizz[i]}");
            }

            Console.Write("\nEnter the number of the question you want to change: ");
            int quest = int.Parse(Console.ReadLine()!);

            Console.Clear();
            Console.WriteLine(quiz.Quizz[quest]);
            bool EXT = true;
            while (EXT != false)
            {
                Console.WriteLine("");
                ShowMenuEditQuiz();
                String? choice = Console.ReadLine();
                switch (choice)
                {
                    case "0": { EXT = false; goto STARTEDIT; } break;
                    case "1":
                        {
                            Console.WriteLine("Write new question: ");
                            quiz.Quizz[quest].Quest = Console.ReadLine()!;
                        }
                        break;
                    case "2":
                        {
                            Console.WriteLine("\n[1] - Add new answer\n[2] - Edit answer\n[3] - Delete answer");
                            String? answerChoice = Console.ReadLine()!;
                            if (answerChoice == "1")
                            {
                                Console.Write("Write answer: ");
                                quiz.Quizz[quest].Answers.Add(Console.ReadLine()!);
                            }
                            else if (answerChoice == "2")
                            {
                                Console.Write("Write number of answer: ");
                                int editNUM = int.Parse(Console.ReadLine()!);
                                if (editNUM < quiz.Quizz[quest].Answers.Count)
                                {
                                    Console.Write("Write new answer: ");
                                    quiz.Quizz[quest].Answers[editNUM] = Console.ReadLine()!;
                                }
                                else { Console.WriteLine("Incorrect index"); }
                            }
                            else if (answerChoice == "3")
                            {
                                Console.Write("Write number of answer: ");
                                int deletNUM = int.Parse(Console.ReadLine()!);
                                if (deletNUM < quiz.Quizz[quest].Answers.Count) { quiz.Quizz[quest].Answers.RemoveAt(deletNUM); }
                                else { Console.WriteLine("Incorrect index"); }
                            }
                        }
                        break;
                    case "3":
                        {
                            quiz.Quizz[quest].RightAnswers.Clear();
                            while (true)
                            {
                                Console.WriteLine("When you're done writing your answer(s), write \"end\"");
                                Console.Write("Write number of all right answers: ");
                                String? rightAnswers = Console.ReadLine()!;
                                if (rightAnswers == "end") { break; }
                                else { quiz.Quizz[quest].RightAnswers.Add(rightAnswers); }
                            }
                        }
                        break;
                    case "4": { quiz.Quizz[quest].Set(); } break;

                }
            }
        }
        public void LeaderBoard(User user, int persent)
        {
            if (Top20.ContainsKey(user.Login))
            {
                if (Top20[user.Login] < persent)
                {
                    Top20.Remove(user.Login);
                    Top20.Add(user.Login, persent);
                }
            }
            else { Top20.Add(user.Login, persent); }

            var sortedDict = from entry in Top20 orderby entry.Value ascending select entry;
            Top20 = sortedDict.ToDictionary(T => T.Key, T => T.Value);
            if (Top20.Count > 20) { Top20.Remove(Top20.Keys.Last()); }
            LoadToFile(this, false);
        }
        public void LoadToFile(Quiz _quiz, bool options = false)
        {
            String? point = "point.txt",
                path = new FileInfo(point).FullName.Replace("point.txt", $"Quizzes\\{Name}.json");
            if (options)
            {
                StreamWriter sw = new("point.txt", options);
                sw.WriteLine($"\n{Name}");
                sw.Close();
            }
            FileStream fsc = new(path, FileMode.Create);
            JsonSerializer.Serialize<Quiz>(fsc, _quiz, new JsonSerializerOptions { WriteIndented = true });
            fsc.Close();
        }
    }
}
