using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperQuiz.Common.Account
{
    public class User
    {
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";
        public string Date { get; set; } = "";

        public List<String> quizHistory { get; set; } = new();
        public void ShowQuizHistory()
        {
            if (quizHistory.Count == 0) { Console.WriteLine("You doesn't complete any quiz yet!"); }
            else 
            { 
                foreach (var quiz in quizHistory) { 
                    Console.WriteLine($"\t\t\t\t\t\t {quiz}"); 
                } 
            }
        }
        public override string ToString() { return $"Login: {Login}\nPassword: {Password}\nDate: {Date}"; }
    }
}
