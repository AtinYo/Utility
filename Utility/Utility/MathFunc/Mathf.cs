using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.MathFunc
{
    class Mathf
    {
        /// <summary>
        /// 求绝对值
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Abs(int x)
        {
            return x > 0 ? x : -x;
        }

        /// <summary>
        /// 求绝对值
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double Abs(double x)
        {
            return x > 0 ? x : -x;
        }

        //牛顿迭代法
        private static double Newton_Raphson_Method(double num, int n, double X)
        {
            return X - (Pow(X, n) - num) / (n * Pow(X, n - 1));
        }

        /// <summary>
        /// 求n次方根
        /// </summary>
        /// <param name="num">底数</param>
        /// <param name="n">开方数</param>
        /// <returns></returns>
        public static double N_th_root(double num, int n)
        {
            double x_2 = num;
            double x_1 = 0f;
            if (num > 0)
            {
                if (n > 1)
                {
                    while (Abs(x_2 - x_1) > 1e-6)
                    {
                        x_1 = x_2;
                        x_2 = Newton_Raphson_Method(num, n, x_1);
                    }
                }
                else if (n == 1)
                {
                    x_2 = num;
                }
            }
            return x_2;
        }

        /// <summary>
        /// 求n次幂
        /// </summary>
        /// <param name="num">底数</param>
        /// <param name="n">次幂数</param>
        /// <returns></returns>
        public static double Pow(double num, int n)
        {
            double temp = 1f;
            if (num > 0)
            {
                if (n > 1)
                {
                    while (n > 0)
                    {
                        temp *= num;
                        n--;
                    }
                }
                else if(n == 1)
                {
                    temp = num;
                }
            }
            return temp;
        }

        /// <summary>
        /// 求n次幂
        /// </summary>
        /// <param name="num">底数</param>
        /// <param name="n">次幂数</param>
        /// <returns></returns>
        public static int Pow(int num, int n)
        {
            int temp = 1;
            if (num > 0)
            {
                if (n > 1)
                {
                    while (n > 0)
                    {
                        temp *= num;
                        n--;
                    }
                }
                else if (n == 1)
                {
                    temp = num;
                }
            }
            return temp;
        }

        /// <summary>
        /// 求n次幂,更高效,实现目前比较丑陋，有时间再优化,
        /// </summary>
        /// <param name="num">底数</param>
        /// <param name="n">次幂数</param>
        /// <returns></returns>
        public static double Pow_Quick(double num, int n)
        {
            if (num <= 0 || n <= 0)
            {
                return 1f;
            }
            //原理是利用 x^n = x^(n/2) * x^(n/2)  -- n为偶数
            //           x^n = x ^ x^(n/2) * x^(n/2) -- n为奇数
            //非递归写法,递归写法很简单不写了
            int cur_n = n;
            //这里先暂时用泛型Stack,其实数学库这种对性能要求很高的，应该用自己写的栈,这个stack是用于业务逻辑，所以有很多不必要的东西
            //有时间再优化成自己写的数据结构
            Stack<bool> stack = new Stack<bool>();//代表是否需要乘以x(即num),栈深大概为logN
            //先处理进栈操作
            //stack.Push(false);//(x,n)在栈底，不需要额外乘以x
            while (cur_n >= 1)
            {
                if (cur_n % 2 == 0)//偶数
                {
                    stack.Push(false);
                    cur_n /= 2;
                }
                else//奇数
                {
                    if (cur_n == 1)
                    {
                        stack.Push(false);
                        cur_n = 0;//终止循环
                    }
                    else
                    {
                        stack.Push(true);//代表当出栈到这个节点的时候，计算结果需要额外乘以x,因为是奇数
                        cur_n = (cur_n - 1) / 2;
                    }
                }
            }
            //出栈操作
            double result = 1;
            bool temp = false;
            bool isFirst = true;
            while (stack.Count > 0)
            {
                temp = stack.Pop();
                if (isFirst)
                {
                    result *= num;
                    isFirst = false;
                }
                else
                {
                    result *= result;

                }
                if (temp)
                {
                    result *= num;//需要额外乘以x
                }
            }
            return result;
        }
    }
}
