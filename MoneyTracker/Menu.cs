using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Spectre.Console;

namespace MoneyTracker
{

    public class Menu
    {   
        public Style? MenuStyle { get; set; }
        public string? Title { get; set; }




        // Ask for the user's favorite fruit
        var menuChoice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Main Menu")
                .AddChoices(new[] {
                    "Show transactions",
                    "Add new transaction",
                    "Edit transactions",
                    "Save and quit"
                }
                )
            );


    }
}
