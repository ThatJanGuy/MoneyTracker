using LittleHelpers;

namespace MoneyTracker
{
    internal class Account
    {
        public List<Transaction> transactions = new();
        private char orderDisplayBy = ' ';
        private char sortDisplay = ' ';

        public Account(List<Transaction> transactions)
        {
            this.transactions = transactions;
        }
        public Account()
        {
        }

        public void AddTransaction()
        // Collects data from the command line and creates a new transaction
        // that it the adds to Transactions. Uses the GetInput functions
        // of LittleHelpers a lot. They set the bool exit to true when
        // the user triggers the exit condition.
        {
            while (true)
            {
                bool exit = false;

                string? title = null;
                decimal? amount = null;
                int? month = null;

                Console.Clear();
                TextManipulation.MakeHeading("New transaction ('x' to exit)");
                Console.WriteLine("\nPlease enter the following information:");

                while (string.IsNullOrEmpty(title))
                {
                    Console.Write("Title".PadRight(28) + "> ");
                    title = GetInput.GetString(out exit, "x");
                    if (exit) break;
                }
                if (exit) break;

                while (amount == null)
                {
                    Console.Write("Amount (lead - for expense)".PadRight(28) + "> ");
                    amount = GetInput.GetDecimal(out exit, "x");
                    if (exit) break;
                }
                if (exit) break;

                while (month == null)
                {
                    Console.Write("Month (1-12) ".PadRight(28) + "> ");
                    month = GetInput.GetInt(out exit, "x", 1, 12);
                    if (exit) break;
                }
                if (exit) break;

                this.transactions.Add(new Transaction(
                    Guid.NewGuid(),
                    title,
                    amount.Value,
                    month.Value
                    ));

                Display();
            }
        }

        public void AddTestData()
        {
            Console.WriteLine("Loading test Data ...");
            transactions.AddRange(new List<Transaction>
                {
                        new Transaction(Guid.NewGuid(), "Hemförsäkring", Convert.ToDecimal(-150.27), 2),
                        new Transaction(Guid.NewGuid(), "Lön", Convert.ToDecimal(25226.85), 5),
                        new Transaction(Guid.NewGuid(), "Hyra", Convert.ToDecimal(-9256), 1),
                        new Transaction(Guid.NewGuid(), "ICA", Convert.ToDecimal(-5000), 4),
                        new Transaction(Guid.NewGuid(), "Lexicon Kaffe", Convert.ToDecimal(-200), 7),
                        new Transaction(Guid.NewGuid(), "Fritids", Convert.ToDecimal(-1400), 6),
                        new Transaction(Guid.NewGuid(), "Barnbiddrag", Convert.ToDecimal(1050), 8)
                }
            );
            Console.WriteLine("Test Data has been loaded. Press ANY KEY to continue.");
            Console.ReadKey();
        }

        public void DeleteTransaction(Transaction transaction)
        {
            bool deletionSuccessful = transactions.Remove(transaction);
            if (deletionSuccessful) TextManipulation.ColoredText("Transaction deleted", ConsoleColor.Green);
            else TextManipulation.ColoredText("Operation failed", ConsoleColor.Red);
            Console.WriteLine("\nPress ANY KEY to continue");
            Console.ReadKey();
            Display();
        }


        public void Display()
        {

            List<Transaction>? orderedList = new();

            // Catches nulled asset lists to avoid later references to null.
            if (this.transactions == null)
            {
                TextManipulation.ColoredText("No transactions in account!\n" +
                    "Populate list before calling an output.\n",
                    ConsoleColor.Yellow
                );
                return;
            }

            Console.Clear();

            if (this.orderDisplayBy == ' ' || this.sortDisplay == ' ')
            {
                while (true)
                {
                    this.orderDisplayBy = ' ';
                    this.sortDisplay = ' ';

                    Console.Write(
                        "Display transactions\n" +
                        "Sort by:\n" +
                        "(1) Month\n" +
                        "(2) Amount\n" +
                        "(3) Title\n" +
                        "Enter number > ");

                    orderDisplayBy = Console.ReadKey().KeyChar;

                    if (this.orderDisplayBy != '1' && this.orderDisplayBy != '2' && this.orderDisplayBy != '3')
                    {
                        TextManipulation.ColoredText("Please chose a given option.", ConsoleColor.Red);
                        continue;
                    }

                    Console.Write("\n\n(1) Ascending or\n" +
                        "(2) Descending?");

                    sortDisplay = Console.ReadKey().KeyChar;

                    if (this.sortDisplay != '1' && this.sortDisplay != '2')
                    {
                        TextManipulation.ColoredText("Please chose a given option.", ConsoleColor.Red);
                        continue;
                    }
                    break;
                }
            }

            Console.Clear();

            orderedList = transactions;
            if (this.orderDisplayBy == '1' && this.sortDisplay == '1') orderedList = orderedList.OrderBy(t => t.Month).ToList();
            else if (this.orderDisplayBy == '2' && this.sortDisplay == '1') orderedList = orderedList.OrderBy(t => t.Amount).ToList();
            else if (this.orderDisplayBy == '3' && this.sortDisplay == '1') orderedList = orderedList.OrderBy(t => t.Title).ToList();
            else if (this.orderDisplayBy == '1' && this.sortDisplay == '2') orderedList = orderedList.OrderByDescending(t => t.Month).ToList();
            else if (this.orderDisplayBy == '2' && this.sortDisplay == '2') orderedList = orderedList.OrderByDescending(t => t.Amount).ToList();
            else if (this.orderDisplayBy == '3' && this.sortDisplay == '2') orderedList = orderedList.OrderByDescending(t => t.Title).ToList();

            // Create a nice heading for the output table.
            TextManipulation.MakeHeading(
                "ID".PadRight(4) + "| " +
                "Title".PadRight(30) + "| " +
                "Month".PadRight(7) + "| " +
                "Amount".PadRight(15)
            );

            for (int i = 0; i < orderedList.Count; i++)
            {
                Console.WriteLine(
                    i.ToString().PadRight(4) + "| " +
                    orderedList[i].Title.PadRight(30) + "| " +
                    orderedList[i].Month.ToString().PadRight(7) + "| " +
                /*FIXME! There needs to be a culture thingy that is saved together with the amount. Or something.
                 This here works for this assignment, though.*/
                    orderedList[i].Amount.ToString("C").PadRight(10));
            }

            Console.Write($"\nTo EDIT a transaction press '1'.\n" +
                $"To DELETE a transaction press '2'.\n" +
                $"To ADD a transaction press '3'.\n" +
                $"To CHANGE the SORTING PARAMETERS press '4'\n" +
                $"To safe an exit press 'x'.\n" +
                $"Enter number > ");

            int menuChoice = Console.ReadKey().KeyChar;

            switch (menuChoice)
            {
                case '1':
                    bool cancel1 = false;
                    int? idToEdit = null;
                    Console.Write("\nEnter the ID of the transaction you wish to edit ('c' to cancel): ");
                    while (idToEdit == null)
                    {
                        idToEdit = GetInput.GetInt(out cancel1, "c", 0, orderedList.Count);
                        if (cancel1) break;
                    }
                    if (cancel1) Display();

                    Transaction toEdit = (Transaction)transactions.Where(t => t.Id == orderedList[(int)idToEdit].Id);
                    toEdit.Edit();

                    break;

                case '2':
                    bool cancel2 = false;
                    int? idToDelete = null;
                    Console.Write("\nEnter the ID of the transaction you wish to delete ('c' to cancel): ");
                    while (idToDelete == null)
                    {
                        idToDelete = GetInput.GetInt(out cancel2, "c", 0, orderedList.Count);
                        if (cancel2) break;
                    }
                    if (cancel2) Display();

                    DeleteTransaction(orderedList[(int)idToDelete]);
                    break;
                case '3':
                    AddTransaction();
                    break;
                case '4':
                    this.orderDisplayBy = ' ';
                    this.sortDisplay = ' ';
                    Display();
                    break;
                case 'x':
                    Console.WriteLine("Safe");
                    break;
                default:
                    TextManipulation.ColoredText("Please enter a valid option.", ConsoleColor.Red);
                    break;
            }
        }

        public void Edit(Transaction transaction)
        {
            transactions.
                Remove(transaction);
            if (deletionSuccessful) TextManipulation.ColoredText("Transaction deleted", ConsoleColor.Green);
            else TextManipulation.ColoredText("Operation failed", ConsoleColor.Red);
            Console.WriteLine("\nPress ANY KEY to continue");
            Console.ReadKey();
            Display();
        }
    }
}
