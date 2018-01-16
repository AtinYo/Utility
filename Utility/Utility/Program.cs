using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(MathFunc.Mathf.Pow_Quick(2, 2));
            Console.WriteLine(MathFunc.Mathf.Pow_Quick(2, 8));
            Console.WriteLine(MathFunc.Mathf.Pow_Quick(4, 2));
            Console.WriteLine(MathFunc.Mathf.Pow_Quick(2, 10));
            Console.WriteLine(MathFunc.Mathf.Pow_Quick(2, 5));
        }
    }
}
