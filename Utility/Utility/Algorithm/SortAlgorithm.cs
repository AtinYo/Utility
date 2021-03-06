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
    public static class SortAlgorithm
    {
        #region InsertSort [最坏:O(n^2),平均:O(n^2)]
        /// <summary>
        /// 插入排序算法.排序结果默认是升序,如果要得到降序结果,修改CompareTo返回值即可.[基于元素的交换,不建议用在存储结构是连续分布的且数量交大的集合.如数组]
        /// </summary>
        /// <typeparam name="T">集合数据的类型</typeparam>
        /// <param name="src">需要排序的集合</param>
        public static void InsertSort<T>(IList<T> src)where T : IComparable
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

        #region MergeSort [最坏:O(nlgn),平均:O(nlgn)]
        /// <summary>
        /// 合并排序算法.排序结果默认是升序.如果要得到降序结果,修改CompareTo返回值即可.[由于使用了函数递归,不建议用在n规模较大的集合,避免StackOverflow]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        public static void MergeSort<T>(IList<T> src) where T : IComparable
        {
            if(src != null && src.Count > 0)
            {
                int left = 0;
                int right = src.Count - 1;
                MergeSort(src, left, right);
            }
        }
        
        public static void MergeSort<T>(IList<T> src, int left, int right) where T : IComparable
        {
            //left和right都是可以到达的index
            if(src != null && left >= 0 && right < src.Count && left < right)
            {
                int index = (left + right) / 2;
                MergeSort(src, left, index);
                MergeSort(src, index + 1, right);
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

        #region BubbleSort [最坏:O(n^2),平均:O(n^2)]
        /// <summary>
        /// 冒泡排序.排序结果默认是升序,如果要得到降序结果,修改CompareTo返回值即可.[基于交换的简单但低效的排序算法]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        public static void BubbleSort<T>(IList<T> src) where T : IComparable
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

        #region QuickSort [最坏:O(n^2),平均:O(nlgn)期望]
        /// <summary>
        /// 快速排序.排序结果默认是升序,如果要得到降序结果,修改CompareTo返回值即可.[基于交换的排序]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        public static void QuickSort<T>(IList<T> src) where T : IComparable
        {
            if(src != null && src.Count > 0)
            {
                QuickSort(src, 0, src.Count - 1);
            }
        }

        public static void QuickSort<T>(IList<T> src, int left, int right) where T : IComparable
        {
            if(src != null && src.Count > 0 && left >= 0 && right < src.Count && right > left)
            {
                #region 随机选取哨兵
                int guardIndex = RandomNumProducer.Instance.Range(left + 1, right);
                Swap(src, guardIndex, left);
                #endregion
                T guard = src[left];//取该元素作为哨兵,使得排序后,小于它的在它左边,大于它的在它右边.然后再分别对左右两部分快排
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
                QuickSort(src, left, j - 1);
                QuickSort(src, j + 1, right);
            }
        }
        #endregion

        #region HeapSort [最坏:O(nlgn),平均:--]
        /// <summary>
        /// 堆排序.先构建堆,然后"输出"堆顶元素,再通过维护"输出"堆顶后的堆来实现排序.[最大堆法.参考算法导论]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        public static void MaxHeapSort<T>(IList<T> src) where T : IComparable
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

        /// <summary>
        /// 堆排序.先构建堆,然后"输出"堆顶元素,再通过维护"输出"堆顶后的堆来实现排序.[最小堆法.]
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        public static void MinHeapSort<T>(IList<T> src) where T : IComparable
        {
            if(src != null && src.Count > 0)
            {
                //先构建堆
                BuildMinHeap(src);

                //"输出"堆顶元素.就是将堆顶元素放到list末端,且让堆大小减少1
                int heap_size = src.Count;
                while (heap_size > 0)
                {
                    Swap(src, 0, heap_size - 1);
                    heap_size--;
                    MinHeapify_Downward(src, 0, heap_size);
                }
            }
        }

        /// <summary>
        /// 堆最小化.较小元素上浮.堆底插入元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <param name="index"></param>
        /// <param name="heap_size"></param>
        private static void MinHeapify_Upward<T>(IList<T> src, int index, int heap_size) where T : IComparable
        {
            if (index >= 0 && index < heap_size)
            {
                int parentIndex = (index - 1) / 2;
                if (parentIndex >= 0)
                {
                    if (src[index].CompareTo(src[parentIndex]) < 0)//最小元素"上浮"
                    {
                        Swap(src, index, parentIndex);
                        MinHeapify_Upward(src, parentIndex, heap_size);
                    }
                }
            }
        }

        /// <summary>
        /// 堆最小化.较大元素下沉.堆顶插入元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        /// <param name="index"></param>
        /// <param name="heap_size"></param>
        private static void MinHeapify_Downward<T>(IList<T> src, int index, int heap_size) where T : IComparable
        {
            if (index >= 0 && index < heap_size)
            {
                int SmallestIndex = index;
                int RightChildIndex = 2 * (index + 1);
                int LeftChildIndex = RightChildIndex - 1;
                if (LeftChildIndex < heap_size && src[LeftChildIndex].CompareTo(src[index]) < 0)
                {
                    SmallestIndex = LeftChildIndex;
                }
                if (RightChildIndex < heap_size && src[RightChildIndex].CompareTo(src[SmallestIndex]) < 0)
                {
                    SmallestIndex = RightChildIndex;
                }
                if (SmallestIndex != index)
                {
                    Swap(src, SmallestIndex, index);
                    MinHeapify_Downward(src, SmallestIndex, heap_size);
                }
            }
        }

        /// <summary>
        /// 构建最小堆.通过不断插入元素构建
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        private static void BuildMinHeap<T>(IList<T> src) where T : IComparable
        {
            //算法思想是,一开始是一个0元素的堆,不断插入元素(在堆尾),插入后进行最小堆化.
            int heap_size = 0;
            while (heap_size < src.Count)
            {
                heap_size++;//相当于插入一个元素到堆中
                MinHeapify_Upward(src, heap_size - 1, heap_size);
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

        #region CountSort [O(k+n)]
        /// <summary>
        /// 计数排序.时间复杂度为O(k+n),其中k为要排序序列中最大元素的值.当k=O(n)时,可以考虑采用计数排序--算法导论
        /// </summary>
        /// <typeparam name="T">需要实现相等接口</typeparam>
        /// <param name="src"></param>
        public static void CountSort<T>(IList<T> src) where T : IEquatable<T>
        {
            if (src != null && src.Count > 0)
            {
                //泛型计数排序,到时候再实现,需要用到字典作为容器,这样即便k很大,也可以利用字典作为存储数据来处理,第二个,只有字典才能让排序序列中元素作为索引(作为key)
            }
        }
        #endregion

        #region RadixSort
        /// <summary>
        /// 基数排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="src"></param>
        public static void RadixSort<T>(IList<T> src)
        {
            //有空再实现.
            //思想是这样的: 将要排序的值分成若干位进行排序,[比如数字分成每一位排序,个位十位百位等等,从低位向高位排序;又比如时间,按年月日排序]
            //先对值用 稳定排序算法(一定要稳定) 在某一位上进行排序，然后逐位地排序
        }
        #endregion

        #region BucketSort
        public static void BucketSort<T>(IList<T> src)
        {
            //要求被排序的序列是在一个确定的区间[a,b]
            //思想是,用一个临时链表把某个区间分成若干个自区间(桶);比如说[0,100]分成[0,9]、[10,19]、[20,29]等等,每个[a,a+9]的区间都是一个桶
            //然后把待排序序列逐个放进桶
            //桶自己内部再用排序算法进行排序,
            //最后把每个桶元素按顺序输出,即完成排序了
        }
        #endregion

        #region tools
        /// <summary>
        /// 检查list 是否 是升序(或降序)序列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="isAscend"></param>
        /// <returns></returns>
        public static bool CheckSortedListIsLegal<T>(IList<T> list, bool isAscend = true) where T : IComparable
        {
            T lastNum = list[0];
            bool compareResult = false;
            for(int i = 1; i < list.Count; i++)
            {
                compareResult = isAscend ? lastNum.CompareTo(list[i]) > 0 : lastNum.CompareTo(list[i]) < 0;
                if (compareResult)
                {
                    return false;
                }
                lastNum = list[i];
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
