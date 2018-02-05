using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Tool;
namespace Utility.Algorithm
{
    /// <summary>
    /// 实现了IList<T>接口且T实现了IComparable的集合类 的排序算法
    /// </summary>
    public static class SortingAlgorithm
    {
        #region InsertSorting
        /// <summary>
        /// 插入排序算法.排序结果默认是升序,如果要得到降序结果,修改CompareTo返回值即可.[基于元素的交换,不建议用在存储结构是连续分布的且数量交大的集合.如数组]
        /// </summary>
        /// <typeparam name="T">集合数据的类型</typeparam>
        /// <param name="src">需要排序的集合</param>
        public static void InsertSorting<T>(IList<T> src)where T : IComparable
        {
            if (src != null && src.Count > 1)
            {
                //将传入的集合视为一维数组,左边部分是已排序的,默认起始有一个元素,就是集合的第一个元素.
                //右边部分是需要进行排序的.每次从这部分逐个取元素插入到左边部分

                int sortedCount = 1;//已排序部分的元素数量,同时也代表了右边未排序部分的起始index

                while (sortedCount < src.Count)
                {
                    int sortedStartIndex = 0;//已排序部分的开始index
                    int sortedEndIndex = sortedCount - 1;//已排序部分的结束index

                    int InsertIndex = 0;//插入的index

                    #region 对左边的已排序部分进行折半查询,查找curStartIndex所指元素 在左边部分应该插入的 InsertIndex
                    while (sortedStartIndex <= sortedEndIndex)
                    {
                        int index = (sortedStartIndex + sortedEndIndex) / 2;
                        int compareResult = src[sortedCount].CompareTo(src[index]);
                        if (compareResult == 0)
                        {
                            InsertIndex = index + 1;
                            break;
                        }
                        if (compareResult < 0)
                        {
                            //[sortedStartIndex,sortedEndIndex]取[sortedStartIndex,index-1]区间
                            sortedEndIndex = index - 1;
                        }
                        else
                        {
                            //[sortedStartIndex,sortedEndIndex]取[index+1,sortedEndIndex]区间
                            sortedStartIndex = index + 1;
                        }
                        InsertIndex = sortedStartIndex;
                    }
                    #endregion

                    #region 基于交换的插入操作.因为没有新开辟集合,因此要在原集合进行插入就要基于交换.
                    int j = sortedCount;
                    T temp = src[sortedCount];
                    while (j > InsertIndex)
                    {
                        //Swap(src[j - 1], src[j]);
                        src[j] = src[j - 1];
                        j--;
                    }
                    src[InsertIndex] = temp;
                    #endregion
                    sortedCount++;
                }

            }
        }
        #endregion

        #region MergeSorting
        /// <summary>
        /// 合并排序算法.排序结果默认是升序.如果要得到降序结果,修改CompareTo返回值即可.[由于使用了函数递归,不建议用在n规模较大的集合,避免StackOverflow]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        public static void MergeSorting<T>(IList<T> src) where T : IComparable
        {
            if(src != null && src.Count > 0)
            {
                int left = 0;
                int right = src.Count - 1;
                MergeSorting(src, left, right);
            }
        }


        public static void MergeSorting<T>(IList<T> src, int left, int right) where T : IComparable
        {
            //left和right都是可以到达的index
            if(src != null && left >= 0 && right < src.Count && left < right)
            {
                int index = (left + right) / 2;
                MergeSorting(src, left, index);
                MergeSorting(src, index + 1, right);
                Merge(src, left, right, index);
            }
        }

        

        private static void Merge<T>(IList<T> src, int left, int right, int mid) where T : IComparable
        {
            T[] temp = new T[right - left + 1];
            int index = 0;
            int index_1 = left;
            int index_2 = mid + 1;
            int result = 0;
            while (index < temp.Length)
            {
                if (index_1 <= mid && index_2 <= right)
                {
                    result = src[index_1].CompareTo(src[index_2]);
                    if (result <= 0)
                    {
                        temp[index++] = src[index_1++];
                    }
                    else
                    {
                        temp[index++] = src[index_2++];
                    }
                }
                else
                {
                    //说明已经有一个已经为空了
                    while (index_1 <= mid)
                    {
                        temp[index++] = src[index_1++];
                    }
                    while (index_2 <= right)
                    {
                        temp[index++] = src[index_2++];
                    }
                }
            }
            for(int i=0; i < temp.Length; i++)
            {
                src[left + i] = temp[i];
            }
        }
        #endregion
    }
}
