using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Tool
{
    public static class Tools
    {
        private static Dictionary<object, object> cacheDic = new Dictionary<object, object>();//只有引用类型才需要缓存，为了解决 循环引用类 的深复制问题

        public static void ClearCopyCacheDic()
        {
            if (cacheDic != null)
            {
                cacheDic.Clear();
            }
            else
            {
                cacheDic = new Dictionary<object, object>();
            }
        }

        public static T GetCopied<T>(this T src)where T:class,new()
        {
            object dst = null;
            try
            {
                if (src == null)
                {
                    return null;
                }

                Type dstType = src.GetType();
                if (dstType.IsValueType)
                {
                    dst = src;
                }
                else
                {
                    //dst = System.Activator.CreateInstance(dstType) as T;
                    if (!cacheDic.TryGetValue(src, out dst))
                    {
                        dst = System.Activator.CreateInstance(dstType, true) as T;
                        cacheDic.Add(src, dst);
                    }
                    //Gets the members (properties, methods, fields, events, and so on) of the current Type -- from MSDN
                    System.Reflection.MemberInfo[] dstMemberInfo = dstType.GetMembers(System.Reflection.BindingFlags.GetField |
                        System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public |
                        System.Reflection.BindingFlags.SetField | System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance);

                    for (int i = 0; i < dstMemberInfo.Length; i++)
                    {
                        if (dstMemberInfo[i].MemberType == System.Reflection.MemberTypes.Field)
                        {
                            System.Reflection.FieldInfo dstFileInfo = dstMemberInfo[i] as System.Reflection.FieldInfo;
                            object srcFileInfoValue = dstFileInfo.GetValue(src);
                            ICloneable dstFileClone = srcFileInfoValue as ICloneable;
                            if (dstFileClone != null)//如果可以克隆就直接调克隆，否则递归调用本方法
                            {
                                dstFileInfo.SetValue(dst, dstFileClone.Clone());
                            }
                            else
                            {
                                object tmp = null;
                                if (!cacheDic.TryGetValue(srcFileInfoValue, out tmp))
                                {
                                    tmp = srcFileInfoValue.GetCopied();
                                    if (!cacheDic.ContainsKey(srcFileInfoValue))
                                        cacheDic.Add(srcFileInfoValue, tmp);
                                }
                                dstFileInfo.SetValue(dst, tmp);
                                //dstFileInfo.SetValue(dst, srcFileInfoValue.GetCopied());
                            }
                        }

                        if (dstMemberInfo[i].MemberType == System.Reflection.MemberTypes.Property)
                        {
                            System.Reflection.PropertyInfo dstPropertyInfo = dstMemberInfo[i] as System.Reflection.PropertyInfo;
                            System.Reflection.MethodInfo methodInfo = dstPropertyInfo.GetSetMethod(true);//就算是私有set,也要去设
                            if (methodInfo != null)
                            {
                                object srcPropertyValue = dstPropertyInfo.GetValue(src, null);//索引器不考虑复制
                                ICloneable dstPropertyClone = srcPropertyValue as ICloneable;
                                if (dstPropertyClone != null)
                                {
                                    dstPropertyInfo.SetValue(dst, dstPropertyClone.Clone(), null);
                                }
                                else
                                {
                                    object tmp = null;
                                    if (!cacheDic.TryGetValue(srcPropertyValue, out tmp))
                                    {
                                        tmp = srcPropertyValue.GetCopied();
                                        if (!cacheDic.ContainsKey(srcPropertyValue))
                                            cacheDic.Add(srcPropertyValue, tmp);
                                    }
                                    dstPropertyInfo.SetValue(dst, srcPropertyValue.GetCopied(), null);
                                    //dstPropertyInfo.SetValue(dst, srcPropertyValue.GetCopied(), null);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return dst as T;
        }
    }
    
}
