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
            SortingAlgorithm.HeapSorting(a);
            isLegal = false;
            isLegal = SortingAlgorithm.CheckSortedListIsLegal(a);

            a.Clear();
            for (int i = 0; i < 100; i++)
            {
                a.Add(random.Next(1000));
            }
            SortingAlgorithm.HeapSorting(a);
            isLegal = false;
            isLegal = SortingAlgorithm.CheckSortedListIsLegal(a);

            a.Clear();
            for (int i = 0; i < 100; i++)
            {
                a.Add(random.Next(1000));
            }
            SortingAlgorithm.HeapSorting(a);
            isLegal = false;
            isLegal = SortingAlgorithm.CheckSortedListIsLegal(a);

        }

        
    }
}
