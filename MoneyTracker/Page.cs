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

    public abstract class Page : IPageBody
    {
        public void Show()
        {
            Components.Header();
            Components.MainMenu();
            Body();
        }
        public abstract void Body();
    }

    class Home : Page
    {
        public void Body()
        {
            Console.WriteLine(" ");
        }
    }
}
