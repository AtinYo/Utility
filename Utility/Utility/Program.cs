using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Algorithm;
namespace Utility
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isLegal = false;
            List<int> a = new List<int>();
            IList<int> ii = a;
            Random random = new Random();
            for (int i = 0; i < 100; i++)
            {
                a.Add(random.Next(1000));
            }
            SortingtAlgorithm.InsertSorting(a);
            isLegal = false;
            isLegal = CheckIsLegal(a);

            a.Clear();
            for (int i = 0; i < 100; i++)
            {
                a.Add(random.Next(1000));
            }
            SortingtAlgorithm.InsertSorting(a);
            isLegal = false;
            isLegal = CheckIsLegal(a);

            a.Clear();
            for (int i = 0; i < 100; i++)
            {
                a.Add(random.Next(1000));
            }
            SortingtAlgorithm.InsertSorting(a);
            isLegal = false;
            isLegal = CheckIsLegal(a);

        }

        public static bool CheckIsLegal<T>(IList<T> a)where T: IComparable
        {
            T lastNum = default(T);

            foreach(var num in a)
            {
                if (lastNum.CompareTo(num) > 0)
                {
                    return false;
                }
                lastNum = num;
            }
            return true;
        }
    }
}
