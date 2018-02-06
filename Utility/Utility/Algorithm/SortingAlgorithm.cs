﻿using System;
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
        #region InsertSorting [最坏:O(n^2),平均:O(n^2)]
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

        #region MergeSorting [最坏:O(nlgn),平均:O(nlgn)]
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

        #region BubbleSorting [最坏:O(n^2),平均:O(n^2)]
        /// <summary>
        /// 冒泡排序.排序结果默认是升序,如果要得到降序结果,修改CompareTo返回值即可.[基于交换的简单但低效的排序算法]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        public static void BubbleSorting<T>(IList<T> src) where T : IComparable
        {
            if(src != null && src.Count > 0)
            {
                int result = 0;
                for (int i = 0; i < src.Count; i++)
                {
                    for(int j = src.Count - 1; j > 0; j--)
                    {
                        result = src[j].CompareTo(src[j - 1]);
                        if (result <= 0)
                        {
                            Swap(src, j - 1, j);
                        }
                    }
                }
            }
        }
        #endregion

        #region QuickSorting [最坏:O(n^2),平均:O(nlgn)期望]
        /// <summary>
        /// 快速排序.排序结果默认是升序,如果要得到降序结果,修改CompareTo返回值即可.[基于交换的排序]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        public static void QuickSorting<T>(IList<T> src) where T : IComparable
        {
            if(src != null && src.Count > 0)
            {
                QuickSorting(src, 0, src.Count - 1);
            }
        }

        public static void QuickSorting<T>(IList<T> src, int left, int right) where T : IComparable
        {
            if(src != null && src.Count > 0 && left >= 0 && right < src.Count && right > left)
            {
                T guard = src[left];//取list的第一个元素作为哨兵,使得排序后,小于它的在它左边,大于它的在它右边.然后再分别对左右两部分快排
                int i = left + 1;
                int j = right;
                while (true)
                {
                    while (i <= right && src[i].CompareTo(guard) <= 0)
                    {
                        i++;
                    }
                    while (src[j].CompareTo(guard) > 0)//由于是先执行i的处理,到这里的时候,只需要拿第一个比哨兵小的即可,不需要判断j>=0
                    {
                        j--;
                    }
                    if (i >= j)
                        break;

                    Swap(src, i, j);
                }
                Swap(src, left, j);//别忘了处理哨兵
                QuickSorting(src, left, j - 1);
                QuickSorting(src, j + 1, right);
            }
        }
        #endregion

        #region HeapSorting [最坏:O(nlgn),平均:--]
        /// <summary>
        /// 堆排序.先构建堆,然后"输出"堆顶元素,再通过维护"输出"堆顶后的堆来实现排序.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        public static void HeapSorting<T>(IList<T> src) where T : IComparable
        {
            if(src != null && src.Count > 0)
            {
                //先构建堆
                BuildMaxHeap(src);

                //"输出"堆顶元素.就是将堆顶元素放到list末端,且让堆大小减少1
                int heap_size = src.Count;
                while (heap_size > 0)
                {
                    Swap(src, 0, heap_size - 1);
                    heap_size--;
                    MaxHeapify(src, 0, heap_size);
                }
            }
        }

        /// <summary>
        /// 构建最大堆.自底向上构建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        private static void BuildMaxHeap<T>(IList<T> src) where T : IComparable
        {
            int startIndex = (src.Count - 1) / 2;//因为 index 从 (src.Count - 1) / 2 + 1 开始的节点都是叶子节点,可以视为元素为1的堆,不需要MaxHeapify化.
            while(startIndex >= 0)
            {
                MaxHeapify(src, startIndex, src.Count);
                startIndex--;
            }
        }

        /// <summary>
        /// 堆最大化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <param name="index"></param>
        private static void MaxHeapify<T>(IList<T> src, int index, int heap_size) where T : IComparable
        {
            if(index >= 0 && index < heap_size)
            {
                int LargestIndex = index;
                int RightChildIndex = 2 * (index + 1);
                int LeftChildIndex = RightChildIndex - 1;
                if (LeftChildIndex < heap_size && src[index].CompareTo(src[LeftChildIndex]) < 0)
                {
                    LargestIndex = LeftChildIndex;
                }
                if (RightChildIndex < heap_size && src[LargestIndex].CompareTo(src[RightChildIndex]) < 0)
                {
                    LargestIndex = RightChildIndex;
                }
                if (LargestIndex != index)
                {
                    Swap(src, LargestIndex, index);
                    MaxHeapify(src, LargestIndex, heap_size);
                }
            }
        }
        //下面三个是求堆中 父节点index,左子节点index,右子节点index
        //private static int ParentIndex(int i)
        //{
        //    return (i - 1) / 2;
        //}
        //private static int LeftChildIndex(int i)
        //{
        //    return 2 * (i + 1) - 1;
        //}
        //private static int RightChildIndex(int i)
        //{
        //    return 2 * (i + 1);
        //}
        #endregion

        #region tools
        public static bool CheckSortedListIsLegal<T>(IList<T> list) where T : IComparable
        {
            T lastNum = default(T);

            foreach (var num in list)
            {
                if (lastNum.CompareTo(num) > 0)
                {
                    return false;
                }
                lastNum = num;
            }
            return true;
        }

        private static void Swap<T>(IList<T> list, int index_x, int index_y) where T : IComparable
        {
            T temp = list[index_x];
            list[index_x] = list[index_y];
            list[index_y] = temp;
        }
        #endregion
    }
}
