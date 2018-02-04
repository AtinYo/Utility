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
            int[] a = new int[100];
            Random random = new Random();
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = random.Next(1000);
            }
            SortingtAlgorithm.InsertSorting(ref a);
            isLegal = false;
            isLegal = CheckIsLegal(a);

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = random.Next(1000);
            }
            SortingtAlgorithm.InsertSorting(ref a);
            isLegal = false;
            isLegal = CheckIsLegal(a);

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = random.Next(1000);
            }
            SortingtAlgorithm.InsertSorting(ref a);
            isLegal = false;
            isLegal = CheckIsLegal(a);

            for (int i = 0; i < a.Length; i++)
            {
                a[i] = random.Next(1000);
            }
            SortingtAlgorithm.InsertSorting(ref a);
            isLegal = false;
            isLegal = CheckIsLegal(a);
        }

        public static bool CheckIsLegal(int[] a)
        {
            int lastNum = -1;
            foreach(var num in a)
            {
                if (lastNum > num)
                {
                    return false;
                }
                lastNum = num;
            }
            return true;
        }
    }
}
