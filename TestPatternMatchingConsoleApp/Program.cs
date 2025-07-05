using System;
using System.Drawing;

namespace TestPatternMatchingConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var person = new Employee { Age = 30, YearsAtCompany = 5 };
            if (person is Employee { Age: >= 18, YearsAtCompany: > 5 })
            {
                Console.WriteLine("Age: >= 18, YearsAtCompany: > 5 }");
            }
            else
                Console.WriteLine("not Age: >= 18, YearsAtCompany: > 5 }");
            object? obj = null;
            var d = 100d;
            var str = "test";
            if (obj is null) Console.WriteLine("对象为null");
            if (d is Math.PI) Console.WriteLine("d 是圆周率");
            if (str is "test") Console.WriteLine("str内容等于test");
            obj = 100;
            if (obj is var o) Console.WriteLine(" 不管 obj 是否为null，o 指向原始值（即使null）" + o.ToString()); // 不管 obj 是否为null，o 指向原始值（即使null）
            obj = null;
            //不要用 var 跳过 null 检查。因为这样会把 null 值“吞掉”，造成 NullReferenceError 隐患。
            if (obj is var _)
            {  // 这里仍可能是 null  
               //Console.WriteLine(obj.ToString()); // NullReferenceException  
            }

            //when 只能用于 switch, 无法用于 if！
            //case string a when a.Length < 10: return a.Length;

            //3. C#8.0: Switch表达式、属性、位置、元组模式

            obj = 100d;
            string whatShape = obj switch
            {
                string c => $"obj is a string:{c}",
                //int _ => "obj is a string",
                { } => "非null",
                _ => "obj null or unknown"
            };
            Console.WriteLine(whatShape);

            //=>右边直接返回值，非常适合表达式体成员：
            //public static string Describe(this Shape s) => s switch { ... };

            //重点说明：
            //• { } 代表对象非null。
            //• { Width: 10, Height: 5 } 同时要求2个属性等于指定值。
            //• { Width: var x, Height: >5 } 可以对属性用关系操作和抽取变量。
            //• 若对象有Deconstruct方法，可用位置/元组模式更优雅。
            var p = person switch
            {
                Employee { Age: 30, YearsAtCompany: 5 } => "person Age: 30, YearsAtCompany: 5",
                Manager { Level: var level, YearsAtCompany: > 3 } => $"rect: {level}",
                { } => "person 非null",
                _ => "person null"
            };
            Console.WriteLine($"p:{p}");
            //3.4 元组模式处理多个参数的模式组合：
            //• 有效解决“复杂组合条件冗长”的老大难。
            //• 更直观表达多变量匹配，不需层层嵌套   
            var c1 = Color.RebeccaPurple.ToString();
            var c2 = Color.Blue.ToString();
            var c3 = Color.DarkCyan.ToString();
            var country = (c1, c2, c3) switch
            {
                //(Color.Blue, Color.White, Color.Red) => "France",
                ("Blue", "White", "Red") => "France",
                //"a constant value of type Color is expected"
                //The issue you're experiencing is a common one when working with System.Drawing.Color in switch expressions. The error "a constant value of type Color is expected" occurs because:
                //Switch patterns require compile-time constants: C# switch expressions need the case values to be known at compile time
                //Color values are not compile-time constants: Color.Blue, Color.White, etc. are static properties that return Color structs, but they're not compile-time constants
                //(Color.Green, Color.White, Color.Red) => "Italy", 
                //(0xFF008000, 0xFFFFFFFF, 0xFFFF0000) => "Italy",   //Green, White, Red     
                _ => "Unknown"
            };
            Console.WriteLine($"country:{country}");

            var cc1 = Color.RebeccaPurple;
            var cc2 = Color.Blue;
            var cc3 = Color.DarkCyan;
            if (cc1 == Color.Blue && cc2 == Color.White && cc3 == Color.Red)
            {
                country = "France";
            }
            else if (cc1 == Color.Green && cc2 == Color.White && cc3 == Color.Red)
            {
                country = "Italy";
            }
            else
            {
                country = "Unknown";
            }
            Console.WriteLine($"country:{country}");

            //4. C#9.0: 组合、括号及关系模式
            //4.1 组合模式（and/or/not）

            var cc = 'D';
            if (cc is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or '.' or ',')
                Console.WriteLine($"c:{cc}, c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z') or '.' or ','");
            else
                Console.WriteLine($"c:{cc}");
            //not 语法: is not null
            if (obj is not null) Console.WriteLine("obj is not null"); else Console.WriteLine("obj is null");

            //4.2 关系模式（Relational Patterns）直接用 >, <, >=, <= 表达区间
            var monthlyIncome = 2000;
            var rate = monthlyIncome switch
            {
                >= 0 and < 1000 => 0,
                < 5000 => 10,
                _ => 20
            };
            Console.WriteLine($"rate:{rate}");

            //结合属性/类型模式：
            //shape switch {
            //    Circle { Radius: >1 and <=10 } => "较合理圆",
            //    Circle { Radius: >10 } => "过大圆",
            //    Rectangle => "矩形",
            //    _ => "不匹配"
            //};

            //5. C#10.0: 扩展属性模式
            //支持嵌套点表达式：
            //之前版本：x is Person { FirstName: { Length: <=5 } }
            //C#10：x is Person { FirstName.Length: <=5 }
            if (person is Employee { Name.Length: <= 5 }) Console.WriteLine("person is Employee { Name.Length: <= 5 }"); else Console.WriteLine("person is not Employee { Name.Length: <= 5 }");

            p = person switch
            {
                Employee { Age: 30, YearsAtCompany: 5 } => "person Age: 30, YearsAtCompany: 5",
                Employee { Name.Length: > 5 } => "person Age: 30, YearsAtCompany: 5",
                Manager { Level: var level, YearsAtCompany: > 3 } => $"rect: {level}",
                { } => "person 非null",
                _ => "person null"
            };

            //模式匹配支持 with 表达式
            //C# 13 引入了 with 表达式，允许在模式匹配中创建新的对象实例，结合 when 条件使用。
            var personRc = new Person("Alice", 30) { Email = "alice@example.com" };
            var result = personRc switch
            {
                Person pp when pp.Age > 18 => pp with { Age = 10 },
                _ => obj
            };

            //模式匹配支持泛型类型参数
            //C# 13 允许在模式匹配中使用泛型类型参数，例如：
            //匹配 List<string> 或 HashSet<string> 类型，并检查其是否非空。
            if (obj is List<string> { Count: > 0 } or HashSet<string> { Count: > 0 })
            {
                Console.WriteLine("Collection is non-empty.");
            }


            //6. C#11.0: 列表与切片模式
            //6.1 列表模式
            //让数组/集合结构模式判定成为可能：
            //if (arr is [1, 2, 3])
            // 匹配长度为3，内容依次为1,2,3
            //• [..]: 切片模式，等价于“0个或多个元素”
            //• _: 忽略一个元素（discard）
            //[_, >0, ..]      // 至少2个元素，第二个>0
            //[.., <=0, _]     // 至少2个元素，倒数第二个<=0且不关心最后一个
            //注意：切片只能出现一次。

            //6.2 列表递归
            //可以做嵌套匹配：
            //bool EndsWithSingleIntList(List<List<int>> lists) => lists is [.., [_]];
            // 至少一个元素，且最后一个是单元素list

            //6.3 支持的结构
            //只要类型有 Length/Count 属性和支持 [index] 则可用列表模式，如 string、数组、自定义集合（只需这两个成员）。
            //例如处理国别码：
            //switch (s) {
            //    case [char c0, char c1]: // 即 s.Length==2
            //        ...
            //}

            //8. 常见误区与最佳实践
            //8.1 不要用 pattern 替代多态
            //最容易见的误用是用模式匹配处理继承结构的多态行为：
            // xxx
            //public static double Area(this Shape shape) => shape switch {
            //    Circle c    => c.Radius * c.Radius * Math.PI,
            //    Rectangle r => r.Width * r.Height
            //};
            //事实上，应该优先用抽象基类/接口的虚方法或属性：
            //abstract class Shape { public abstract double Area {get;} }
            //class Circle : Shape {
            //    public double Radius { get; set; }
            //    public override double Area => Radius * Radius * Math.PI;
            //}
            //为什么？
            //• 可扩展性强（新子类无需修改 Area 逻辑，符合开闭原则）。
            //• 虚调用比类型判定快。
            //• 更少维护负担。
            //8.2 Constant-Only 限制
            //只有编译期常量才能用于模式匹配，如果需要支持运行时变量，需采用传统判定。
            //8.3 避免滥用 var/discard
            //• 不要用 is var x 跳过 null 检查。
            //• discard _ 只适合确实不关心值的场景，用后不要尝试访问。
            //8.4 List/Slice 模式性能
            //使用 [..var arr, x] 这种 slice+变量捕获，编译器可能分配新数组，造成性能下降。大数据集合应谨慎。


            var personR = new Person("Alice", 30) { Email = "alice@example.com" };
            var updatedPerson = personR with { Name = "Bob" };

            // 输出: alice@example.com
            //CS8858: The receiver type 'Employee' is not a valid record type and is not a struct type.
            //Console.WriteLine($"updatedPerson.Email:{updatedPerson.Email}"); 
            Console.WriteLine($"personR.Email:{personR.Email}");
            Console.WriteLine("Hello, World!");
        }
    }

    //C# 中的 record 类型 是一种用于表示不可变数据的类，它简化了对象的创建、比较和复制操作。从 C# 9 开始引入，record 类型是面向数据建模的一种强大工具，特别适合用于需要频繁复制或比较对象的场景。
    //record 关键字声明一个记录类型。
    //构造函数参数自动成为属性（只读）。
    //默认实现 Equals()、ToString() 和 GetHashCode() 方法。
    //record 的属性默认是只读的（init 访问器），除非显式设置为 set。
    //如果需要修改，可以使用 with 表达式。
    //更高效（特别是 record struct）
    //你可以像定义普通类那样，在 record 中显式声明属性
    //当你使用 with 表达式时，只会复制构造函数参数生成的属性（即 init 属性），而显式声明的属性不会被复制。
    public record Person(string Name, int Age)
    {
        public string? Email { get; set; } // 可读可写
    }
    class Employee { public string Name { get; set; } = "Name"; public int Age { get; set; } public int YearsAtCompany { get; set; } }
    class Manager : Employee { public int Level { get; set; } = 9; }
}
