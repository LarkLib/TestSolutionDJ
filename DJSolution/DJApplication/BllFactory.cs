using System;
using System.Collections;
using System.Reflection;

namespace DJ.LMS.WinForms
{
    /// <summary>
    /// 反射类业务逻辑层操作类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BllFactory<T> where T : class
    {
        private static Hashtable objCache = new Hashtable();
        private static object synRoot = new object();

        public static T Instance
        {
            get
            {
                string CacheKey = typeof(T).FullName;
                string CacheValue = typeof(T).Assembly.GetName().Name;
                T bll = (T)objCache[CacheKey];
                if (bll == null)
                {
                    lock (synRoot)
                    {
                        //bll = (T)Assembly.Load(CacheValue).CreateInstance(CacheKey); 
                        Assembly assemblyObj = Assembly.Load(CacheValue);
                        if (assemblyObj == null)
                        {
                            throw new ArgumentNullException("sFilePath", string.Format("无法加载{0} 的程序集", CacheValue));
                        }
                        bll = (T)assemblyObj.CreateInstance(CacheKey);
                        objCache.Add(CacheKey, bll);
                    }
                }
                return bll;
            }
        }
    }
}
