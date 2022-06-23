using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using SuperQuiz.Common;
using SuperQuiz.Common.Account;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;

namespace SuperQuiz.Common.Account
{
    public class Registration
    {
        public void Registry()
        {
            Console.Clear();
            User user = new();
            Menu menu = new();
            List<User> users = new();
            StyleSheet styleSheet = new(Color.LightGray);

            String? point = "point.txt",
                path = point.Replace("point.txt", "Users");
            bool loginExistCheck = false;

            DirectoryInfo dict = new(path);
            var Files = dict.GetFiles();
            foreach (var file in Files)
            {
                FileStream fso = new(file.FullName, FileMode.Open);
                users.Add(JsonSerializer.Deserialize<User>(fso)!); fso.Close();
            }

            Console.WriteLine("\n\n\t\t\t\t\t     ------------| SIGN UP |------------");
            Console.Write("\n\t\t\t\t\t\t    Login: ");
            String? tempLogin = Console.ReadLine()!;

            if (tempLogin.Length == 0) { tempLogin = Console.ReadLine(); }
            foreach (var i in users) { if (tempLogin == i.Login) { loginExistCheck = true; } }

            while (loginExistCheck != false)
            {
                Console.Clear();
                styleSheet.AddStyle("SIGN UP", Color.Red);
                String? loginError = "\n\n\t\t\t\t\t     ------------| SIGN UP |------------";
                Console.WriteLineStyled(loginError, styleSheet);
                Console.WriteLine("\n\t\t\t\t\t\t   Such user already exists!", Color.Red);
                Console.WriteLine("\n\t\t\t\t\t\t    [1] - Try Again");
                Console.WriteLine("\t\t\t\t\t\t    [2] - Go to start screen");
                Console.WriteLine("\n\t\t\t\t\t     -----------------------------------");
                Console.Write("\t\t\t\t\t      Enter: ");

                String? choice = Console.ReadLine();
                if (choice == "1") { loginExistCheck = false; Registry(); }
                else if (choice == "2") { loginExistCheck = false; menu.StartMenu(); }
            }
            user.Login = tempLogin!;

            Console.Write("Write your password: "); user.Password = Console.ReadLine()!;
            if (user.Password.Length == 0) { user.Password = Console.ReadLine()!; }

            Console.Write("Write your birthday DD.MM.YYYY: "); user.Date = Console.ReadLine()!;
            if (user.Date.Length == 0) { user.Date = Console.ReadLine()!; }

            FileStream fs = new($"{path}\\{user.Login}.json", FileMode.Create);
            JsonSerializer.Serialize(fs, user, new JsonSerializerOptions { WriteIndented = true }); fs.Close();

            menu.MainMenu(user);
        }
    }
}
