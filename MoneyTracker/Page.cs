using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyTracker
{
    interface IPageBody
    {
        public void Body();
    }

    public abstract class Page
    {
        public static void Show(Style disabledStyle, Style highlightStyle)
        {
            Components.Header();
            Components.MainMenu(disabledStyle, highlightStyle);
            //Body();
        }
    }

    class Home : Page, IPageBody
    {
        public void Body()
        {
            Console.WriteLine(" ");
        }
    }
}
