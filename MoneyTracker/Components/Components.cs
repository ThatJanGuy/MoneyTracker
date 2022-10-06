using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker
{
    public class Components { 
        public static void Header()
        {
            AnsiConsole.Write(
                new FigletText("MyBudget")
                    .Centered()
                    .Color(Color.RosyBrown));
        }

        public static void MainMenu(Style disabledStyle, Style highlightStyle)
        {
            var menuChoice = AnsiConsole.Prompt(
               new SelectionPrompt<string>()
                   .Title("Main Menu")
                   .AddChoices(new[] {
                        "Show transactions",
                        "Add new transaction",
                        "Edit transactions",
                        "Save and quit"
                   }));

            Console.WriteLine(menuChoice);
        }
    }
}
