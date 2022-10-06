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
                    .RightAligned()
                    .Color(Color.RosyBrown));
        }

        public static void MainMenu()
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
        }
    }
}
