using LittleHelpers;
using MoneyTracker;
using System.Text.Json;

// Setting path of the safefile and instatiating an Account object.
string path = @".\safefile.json";
Account account = new();


if (File.Exists(path))
// If safefile exists, populate the acount object with it.
{
    account.transactions = JsonSerializer.Deserialize<List<Transaction>>(File.ReadAllText(path));
}

if (account.transactions == null || account.transactions.Count == 0)
// Should the safefile be empty or non-existent, offer test data.
{
    Console.Write(
        "No transactions found. Would you like to load test data? (y/n)\n");
    char choice;

    do
    {
        Console.Write(" > ");
        choice = Console.ReadKey().KeyChar;

        switch (choice)
        {
            case 'y':
                account.AddTestData();
                break;
            case 'n':
                break;
            default:
                TextManipulation.ColoredText("\nOption not available.\n", ConsoleColor.Red);
                break;
        }
    } while (choice != 'n' && choice != 'y');
}

// Hand over control to the .Display() method. It can handle the
// whole program. It hands the flow back when the user decides to
// safe and exit. Thus the last statements just safe and, well, exit. :)
account.Display();

string jsonstring = JsonSerializer.Serialize<List<Transaction>>(account.transactions);
File.WriteAllText(path, jsonstring);

Console.ReadLine();
