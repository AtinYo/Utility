using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Interfaces
{
    public class TSingleton<T>where T : class
    {
        private static object mutex = new Object();
        protected static volatile T m_instance = null;//volatile允许任意线程修改
        public static T Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (mutex)
                    {
                        var ctors = typeof(T).GetConstructors(System.Reflection.BindingFlags.Instance |
                                System.Reflection.BindingFlags.NonPublic |
                                System.Reflection.BindingFlags.Public);//指定搜索公有constructor是为了报异常
                        if (ctors.Length != 1)
                        {
                            throw new InvalidOperationException(String.Format("Type {0} must have exactly one constructor.", typeof(T)));
                        }
                        if (ctors[0].GetParameters().Length != 0 || !ctors[0].IsPrivate)//构造函数必须是无参且私有的
                        {
                            throw new InvalidOperationException(String.Format("The constructor for {0} must be private and take no parameters.", typeof(T)));
                        }
                        m_instance = ctors[0].Invoke(null) as T;
                    }
                }
                return m_instance;
            }
        }
    }
}
