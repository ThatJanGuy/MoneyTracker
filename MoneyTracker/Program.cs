//Settings
using MoneyTracker;
using Spectre.Console;

Style MenuDisabledStyle = new(Color.Black, Color.Green1);
Style MenuHighlightStyle = new(Color.Black, Color.LightGreen, Decoration.Bold);

Home.Show(MenuDisabledStyle, MenuHighlightStyle);

Console.ReadLine();
