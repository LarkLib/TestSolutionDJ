using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现枚举的各种相关操作。
    /// 枚举操作，涉及字符串和枚举对象互转、获取枚举成员、获取名称和值集合、获取枚举值、枚举描述等操作。
    /// </summary>
    public class EnumHelper
    {
        /// <summary>    
        /// 通过字符串获取枚举成员实例    
        /// </summary>    
        /// <typeparam name="T">枚举名,比如Enum1</typeparam>    
        /// <param name="member">枚举成员的常量名或常量值,    
        /// 范例:Enum1枚举有两个成员A=0,B=1,则传入"A"或"0"获取 Enum1.A 枚举类型</param>  
        public static T GetInstance<T>(string member)
        {
            return ConvertHelper.ConvertTo<T>(Enum.Parse(typeof(T), member, true));
        }
        /// <summary>    
        /// 获取枚举成员名称和成员值的键值对集合    
        /// </summary>    
        /// <typeparam name="T">枚举名,比如Enum1</typeparam> 
        public static Dictionary<string, object> GetMemberKeyValue<T>()
        {
            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            string[] memberNames = EnumHelper.GetMemberNames<T>();
            string[] array = memberNames;
            for (int i = 0; i < array.Length; i++)
            {
                string text = array[i];
                dictionary.Add(text, EnumHelper.GetMemberValue<T>(text));
            }
            return dictionary;
        }
        /// <summary>    
        /// 获取枚举所有成员名称    
        /// </summary>    
        /// <typeparam name="T">枚举名,比如Enum1</typeparam>
        public static string[] GetMemberNames<T>()
        {
            return Enum.GetNames(typeof(T));
        }
        /// <summary>    
        /// 获取枚举成员的名称    
        /// </summary>    
        /// <typeparam name="T">枚举名,比如Enum1</typeparam>    
        /// <param name="member">枚举成员实例或成员值,    
        /// 范例:Enum1枚举有两个成员A=0,B=1,则传入Enum1.A或0,获取成员名称"A"</param>
        public static string GetMemberName<T>(object member)
        {
            Type underlyingType = EnumHelper.GetUnderlyingType(typeof(T));
            object value = ConvertHelper.ConvertTo(member, underlyingType);
            return Enum.GetName(typeof(T), value);
        }
        /// <summary>    
        /// 获取枚举所有成员值    
        /// </summary>    
        /// <typeparam name="T">枚举名,比如Enum1</typeparam> 
        public static Array GetMemberValues<T>()
        {
            return Enum.GetValues(typeof(T));
        }
        /// <summary>    
        /// 获取枚举成员的值    
        /// </summary>    
        /// <typeparam name="T">枚举名,比如Enum1</typeparam>    
        /// <param name="memberName">枚举成员的常量名,    
        /// 范例:Enum1枚举有两个成员A=0,B=1,则传入"A"获取0</param>
        public static object GetMemberValue<T>(string memberName)
        {
            Type underlyingType = EnumHelper.GetUnderlyingType(typeof(T));
            T instance = EnumHelper.GetInstance<T>(memberName);
            return ConvertHelper.ConvertTo(instance, underlyingType);
        }
        /// <summary>    
        /// 获取枚举的基础类型    
        /// </summary>    
        /// <param name="enumType">枚举类型</param>
        public static Type GetUnderlyingType(Type enumType)
        {
            return Enum.GetUnderlyingType(enumType);
        }
        /// <summary>    
        /// 检测枚举是否包含指定成员    
        /// </summary>    
        /// <typeparam name="T">枚举名,比如Enum1</typeparam>    
        /// <param name="member">枚举成员名或成员值</param> 
        public static bool IsDefined<T>(string member)
        {
            return Enum.IsDefined(typeof(T), member);
        }
        /// <summary>    
        /// 返回指定枚举类型的指定值的描述    
        /// </summary>    
        /// <param name="t">枚举类型</param>    
        /// <param name="v">枚举值</param>    
        /// <returns></returns> 
        public static string GetDescription(Type t, object v)
        {
            string result;
            try
            {
                FieldInfo field = t.GetField(EnumHelper.smethod_0(t, v));
                DescriptionAttribute[] array = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
                result = ((array.Length > 0) ? array[0].Description : EnumHelper.smethod_0(t, v));
            }
            catch
            {
                result = "UNKNOWN";
            }
            return result;
        }
        private static string smethod_0(Type type_0, object object_0)
        {
            string result;
            try
            {
                result = Enum.GetName(type_0, object_0);
            }
            catch
            {
                result = "UNKNOWN";
            }
            return result;
        }
        /// <summary>    
        /// 获取枚举类型的对应序号及描述名称    
        /// </summary>    
        /// <param name="t">枚举类型</param>    
        /// <returns></returns> 
        public static SortedList GetStatus(Type t)
        {
            SortedList sortedList = new SortedList();
            Array values = Enum.GetValues(t);
            for (int i = 0; i < values.Length; i++)
            {
                string value = values.GetValue(i).ToString();
                int num = (int)Enum.Parse(t, value);
                string description = EnumHelper.GetDescription(t, num);
                sortedList.Add(num, description);
            }
            return sortedList;
        }
    }
}
