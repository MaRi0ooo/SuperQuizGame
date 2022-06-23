using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperQuiz.Common;

namespace SuperQuiz
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!new FileInfo("point.txt").Exists) { new FileInfo("point.txt").Create(); }
            if (!new DirectoryInfo("Users").Exists) { new DirectoryInfo("Users").Create(); }
            if (!new DirectoryInfo("Quizzes").Exists) { new DirectoryInfo("Quizzes").Create(); }

            Console.OutputEncoding = System.Text.Encoding.Default;
            Console.InputEncoding = System.Text.Encoding.Default;

            Menu menu = new(); menu.StartMenu();
        }
    }
}