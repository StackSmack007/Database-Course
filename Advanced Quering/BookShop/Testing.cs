using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop
{
    public abstract class A
    {
        protected A()
        {
            Console.WriteLine("A");
        }
        protected A(string zemi)
        {
            Console.WriteLine("Zemi");
        }
    }

    public class B : A
    {
        public B(string daimi):base(daimi)
        {
            Console.WriteLine(daimi.ToLower());
            Console.WriteLine("B");
        }
    }
}
