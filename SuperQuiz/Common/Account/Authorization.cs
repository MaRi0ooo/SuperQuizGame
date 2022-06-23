using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using SuperQuiz.Common.Account;
using Console = Colorful.Console;
using System.Drawing;
using Colorful;

namespace SuperQuiz.Common.Account
{
    public class Authorization
    {
        public void Logistry()
        {
            Console.Clear();
            Menu menu = new();
            User user = new();
            List<User> users = new();
            StyleSheet styleSheet = new(Color.LightGray);

            String? point = "point.txt",
                path = point.Replace("point.txt", "Users");
            bool passwordWrong = false;

            DirectoryInfo dict = new(path);
            var Files = dict.GetFiles();
            foreach (var file in Files)
            {
                FileStream fso = new(file.FullName, FileMode.Open);
                users.Add(JsonSerializer.Deserialize<User>(fso)!); 
                fso.Close();
            }

            Console.WriteLine("\n\n\t\t\t\t\t     ------------| SIGN IN |------------");
            Console.Write("\n\t\t\t\t\t\t    Login: ");
            String? tempLogin = Console.ReadLine();

            foreach (var item in users) { 
                if (tempLogin == item.Login) { user = item; } 
            }
            if (user.Password.Length == 0) 
            {
                Console.Clear();
                styleSheet.AddStyle("SIGN IN", Color.Red);
                String? loginError = "\n\n\t\t\t\t\t     ------------| SIGN IN |------------";
                Console.WriteLineStyled(loginError, styleSheet);
                Console.WriteLine("\n\t\t\t\t\t\t      Login not exist!", Color.Red);

                Console.ReadKey();
                Logistry(); 
            }

            Console.Write("\t\t\t\t\t\t    Password: ");
            String? tempPassword = Console.ReadLine();

            if (tempPassword == user.Password) { menu.MainMenu(user); }
            else { passwordWrong = true; }
            while (passwordWrong != false)
            {
                Console.Clear();
                styleSheet.AddStyle("SIGN IN", Color.Red);
                String? loginError = "\n\n\t\t\t\t\t     ------------| SIGN IN |------------";
                Console.WriteLineStyled(loginError, styleSheet);
                Console.WriteLine("\n\t\t\t\t\t\t       Wrong password!", Color.Red);
                Console.WriteLine("\n\t\t\t\t\t\t    [1] - Try Again");
                Console.WriteLine("\t\t\t\t\t\t    [2] - Go to start screen");
                Console.WriteLine("\n\t\t\t\t\t     -----------------------------------");
                Console.Write("\t\t\t\t\t      Enter: ");

                String? choice = Console.ReadLine();
                if (choice == "1") { passwordWrong = false; Logistry(); }
                else if (choice == "2") { passwordWrong = false; menu.StartMenu(); }
            }
        }
    }
}
