using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SuperQuiz.Common.Account;
using SuperQuiz.Common.Game;

namespace SuperQuiz.Common
{
    public class Menu
    {
        private static void ShowStartMenu()
        {
            Console.Clear();
            Console.WriteLine("\n\n\t\t\t\t\t     ------------| MENU |------------");
            Console.WriteLine("\n\t\t\t\t\t          [1] - Authorization");
            Console.WriteLine("\t\t\t\t\t          [2] - Registration");
            Console.WriteLine("\t\t\t\t\t          [0] - Exit");
            Console.WriteLine("\n\t\t\t\t\t     --------------------------------");
            Console.Write("\t\t\t\t\t    Enter: ");
        }
        private static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine("\n\n\t\t\t\t\t    ------------| MAIN MENU |------------");
            Console.WriteLine("\n\t\t\t\t\t\t     [1] - Profile");
            Console.WriteLine("\t\t\t\t\t\t     [2] - Quizzes");
            Console.WriteLine("\t\t\t\t\t\t     [0] - Exit");
            Console.WriteLine("\n\t\t\t\t\t    -------------------------------------");
            Console.Write("\t\t\t\t\t       Enter: ");
        }
        private static void ShowProfileMenu()
        {
            Console.Clear();
            Console.WriteLine("\n\n\t\t\t\t\t    ------------| PROFILE MENU |------------");
            Console.WriteLine("\n\t\t\t\t\t\t     [1] - Show info");
            Console.WriteLine("\t\t\t\t\t\t     [2] - Show quiz history");
            Console.WriteLine("\t\t\t\t\t\t     [3] - Change password");
            Console.WriteLine("\t\t\t\t\t\t     [4] - Change date of birthday");
            Console.WriteLine("\t\t\t\t\t\t     [0] - Back to main menu");
            Console.WriteLine("\n\t\t\t\t\t    ----------------------------------------");
            Console.Write("\t\t\t\t\t       Enter: ");
        }
        private static void ShowQuizMenu()
        {
            Console.Clear();
            Console.WriteLine("\n\n\t\t\t\t\t    -----------| QUIZ MENU |-----------");
            Console.WriteLine("\n\t\t\t\t\t\t     [1] - List of quizzes");
            Console.WriteLine("\t\t\t\t\t\t     [2] - Create new quiz");
            Console.WriteLine("\t\t\t\t\t\t     [3] - Random quiz");
            Console.WriteLine("\t\t\t\t\t\t     [0] - Back to main menu");
            Console.WriteLine("\n\t\t\t\t\t    -----------------------------------");
            Console.Write("\t\t\t\t\t       Enter: ");
        }
        public void StartMenu()
        {
            Authorization auth = new();
            Registration regn = new();
            bool EXT = true;
            while (EXT != false)
            {
                ShowStartMenu();
                String? choice = Console.ReadLine();
                if (choice == "0") { EXT = false; Environment.Exit(0); }
                else if (choice == "1") { EXT = false; auth.Logistry(); }
                else if (choice == "2") { EXT = false; regn.Registry(); }
            }
        }
        public void MainMenu(User user)
        {
            bool EXT = true;
            while (EXT != false)
            {
                ShowMainMenu();
                String? choice = Console.ReadLine();
                if (choice == "0") { EXT = false; Environment.Exit(0); }
                else if (choice == "1") { ProfileMenu(user); }
                else if (choice == "2") { QuizMenu(user); }
            }
        }
        public void ProfileMenu(User user)
        {
            bool EXT = true;
            while (EXT != false)
            {
                ShowProfileMenu();
                String? choice = Console.ReadLine();
                switch (choice)
                {
                    case "0": { MainMenu(user); } break;
                    case "1":
                        {
                            Console.Clear();
                            Console.WriteLine("\n\n\t\t\t\t\t    ------------| PROFILE INFO |------------");
                            Console.WriteLine($"\n\t\t\t\t\t\t         Login: {user.Login}");
                            Console.WriteLine($"\t\t\t\t\t\t         Password: {user.Password}");
                            Console.WriteLine($"\t\t\t\t\t\t         Date: {user.Date}");
                            Console.WriteLine("\n\t\t\t\t\t    Press any key..."); Console.ReadKey();
                            ProfileMenu(user);
                        }
                        break;
                    case "2":
                        {
                            Console.Clear();
                            Console.WriteLine("\n\n\t\t\t\t\t    ------------| QUIZ HISTORY |------------\n");
                            user.ShowQuizHistory();
                            Console.WriteLine("\n\t\t\t\t\t    Press any key..."); Console.ReadKey();
                            ProfileMenu(user);
                        }
                        break;
                    case "3":
                        {
                            Console.Clear();
                            Console.WriteLine("\n\n\t\t\t\t\t    ------------| PASSWORD CHANGER |------------\n");
                            String? point = "point.txt",
                                path = new FileInfo(point).FullName.Replace("point.txt", "Users\\" + user.Login + ".json");

                            Console.Write("\n\t\t\t\t\t\t        New password: ");
                            String? newPassword = Console.ReadLine()!;

                            using FileStream fso = new(path, FileMode.Open);
                            User userInfo = JsonSerializer.Deserialize<User>(fso)!;
                            userInfo.Password = newPassword;
                            user.Password = newPassword;
                            fso.Close();

                            FileStream fsc = new(path, FileMode.Create);
                            JsonSerializer.Serialize(fsc, user, new JsonSerializerOptions { WriteIndented = true });
                            fsc.Close();

                            Console.WriteLine("\n\t\t\t\t\t\t        Password changed!");
                            Console.WriteLine("\n\t\t\t\t\t    Press any key..."); Console.ReadKey();
                            ProfileMenu(user);
                        }
                        break;
                    case "4":
                        {
                            Console.Clear();
                            Console.WriteLine("\n\n\t\t\t\t\t    ------------| DATE CHANGER |------------\n");
                            String? point = "point.txt",
                                path = new FileInfo(point).FullName.Replace("point.txt", "Users\\" + user.Login + ".json");

                            Console.Write("\n\t\t\t\t\t\t New date |DD.MM.YYYY|: ");
                            String? newDate = Console.ReadLine()!;

                            using FileStream fso = new(path, FileMode.Open);
                            User userInfo = JsonSerializer.Deserialize<User>(fso)!;
                            userInfo.Date = newDate;
                            user.Date = newDate;
                            fso.Close();

                            FileStream fsc = new(path, FileMode.Create);
                            JsonSerializer.Serialize(fsc, user, new JsonSerializerOptions { WriteIndented = true });
                            fsc.Close();

                            Console.WriteLine("\n\t\t\t\t\t\t          Date changed!");
                            Console.WriteLine("\n\t\t\t\t\t    Press any key..."); Console.ReadKey();
                            ProfileMenu(user);
                        }
                        break;
                }
            }
        }
        public void QuizMenu(User user)
        {
            bool EXT = true;
            while (EXT != false)
            {
                ShowQuizMenu();
                String? choice = Console.ReadLine();
                switch (choice)
                {
                    case "0": { MainMenu(user); } break;
                    case "1":
                        {
                            Console.Clear();
                            StreamReader sr = new("point.txt");
                            String[] quizNamesTemp = (sr.ReadToEnd()).Split(new char[] { '\n', '\r' }); 
                            sr.Close();

                            List<String> quizNames = new();
                            foreach (var str in quizNamesTemp) {
                                if (str != "") { quizNames.Add(str); }
                            }
                            Console.WriteLine("\n");
                            for (int i = 0; i < quizNames.Count; i++) {
                                Console.Write($"\t\t\t\t\t\t[{i}] {quizNames[i]}\n");
                            }

                            Console.Write("\t\t\t\t\t\tWrite number of quiz: "); 
                            int NUM = int.Parse(Console.ReadLine()!);

                            String point = "point.txt",
                                path = new FileInfo(point).FullName.Replace("point.txt", $"Quizzes\\{quizNames[NUM]}.json");

                            FileStream fso = new(path, FileMode.Open);
                            Quiz quiz = JsonSerializer.Deserialize<Quiz>(fso)!;
                            fso.Close();

                            Console.Clear();
                            Console.WriteLine($"\n\n\t\t\t\t\t    ------------| QUIZ |------------");
                            Console.WriteLine($"\n\t\t\t\t\t\t       {quizNames[NUM]}");
                            Console.WriteLine("\n\t\t\t\t\t\t  [1] - Start quiz");
                            Console.WriteLine("\t\t\t\t\t\t  [2] - Show top20");
                            Console.WriteLine("\t\t\t\t\t\t  [3] - Edit quiz");
                            Console.WriteLine($"\n\t\t\t\t\t    --------------------------------");
                            Console.Write("\t\t\t\t\t    Enter: ");
                            String? chs = Console.ReadLine()!;

                            if (chs == "1") { quiz.Start(user); }
                            else if (chs == "2")
                            {
                                Console.Clear();
                                Console.WriteLine($"\n\n\t\t\t\t\t    ------------| TOP 20 |------------\n");
                                Console.WriteLine($"\n\t\t\t\t\t\t         {quizNames[NUM]}");
                                foreach (var str in quiz.Top20) {
                                    Console.WriteLine($"\t\t\t\t\t\t      {str.Key} | {str.Value} / {quiz.Quizz.Count}");
                                }
                                Console.WriteLine("\n\t\t\t\t\t    Press any key..."); Console.ReadKey();
                            }
                            else if (chs == "3") { Quiz.EditQuiz(user, quiz); }

                        }
                        break;
                    case "2":
                        {
                            StreamReader sr = new("point.txt");
                            String[] quizNames = (sr.ReadToEnd()).Split(new char[] { '\n' });
                            sr.Close();

                            bool nameCheck = true;
                            while (nameCheck != false)
                            {
                                Console.Clear();
                                Console.WriteLine("\n\n\t\t\t\t\t    ------------| CREATE QUIZ |------------");

                                Quiz _quiz = new();
                                Console.Write("\n\t\t\t\t\t\t    Name of quiz: ");
                                String? name = Console.ReadLine()!;

                                bool FLAG = true;
                                foreach (var n in quizNames) {
                                    if (name == n) { FLAG = false; }
                                }
                                if (!FLAG) { Console.WriteLine("Quiz name already user, type another"); }
                                else { nameCheck = false; }
                                _quiz.Name = name;

                                while (true)
                                {
                                    Console.Clear();
                                    Console.WriteLine("\n\n\t\t\t\t\t  ------------| SET QUESTION |------------");

                                    Question _quest = new(); _quest.Set();
                                    _quiz.Quizz.Add(_quest);

                                    Console.WriteLine("When you're done, write \"end\"");
                                    Console.Write("Enter: ");
                                    String? chs = Console.ReadLine();

                                    if (chs == "end") { break; }
                                    else { Console.WriteLine("Invalid input"); }
                                }
                                _quiz.LoadToFile(_quiz, true);
                            }
                        }
                        break;
                    case "3":
                        {
                            Random rnd = new();
                            Quiz quizR = new();
                            quizR.Name = "Random Quiz";

                            StreamReader sr = new("point.txt");
                            String[] quizNamesTemp = (sr.ReadToEnd()).Split(new char[] { '\n', '\n' });
                            sr.Close();

                            List<String> quizNames = new();
                            foreach(var str in quizNamesTemp) { 
                                if(str != "") { quizNames.Add(str); } 
                            }

                            String? point = "point.txt";
                            while(quizR.Quizz.Count != 10)
                            {
                                int randomQuiz = rnd.Next(0, quizNames.Count());
                                String? path = new FileInfo(point).FullName.Replace("point.txt", $"Quizzes\\{quizNames[randomQuiz]}.json");

                                FileStream fso = new(path, FileMode.Open);
                                Quiz tempQuiz = JsonSerializer.Deserialize<Quiz>(fso)!;
                                fso.Close();

                                int randomQuestion = rnd.Next(0, tempQuiz.Quizz.Count());
                                quizR.Quizz.Add(tempQuiz.Quizz[randomQuestion]);
                            }
                            quizR.Start(user, true);
                        }
                        break;
                }
            }
        }
    }
}
