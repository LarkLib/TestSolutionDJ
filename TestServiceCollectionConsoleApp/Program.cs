using Bingle.Core.Interface;
using Bingle.Core.Service;
using Microsoft.Extensions.DependencyInjection;

namespace TestServiceCollectionConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            //Install-Package Microsoft.Extensions.DependencyInjection
            IServiceCollection container = new ServiceCollection();
            // IServiceCollection
            container.AddTransient<ITestServiceA, TestServiceA>();  // 瞬时生命周期  每一次获取的对象都是新的对象
            container.AddSingleton<ITestServiceB, TestServiceB>(); // 单例生命周期  在容器中永远只有当前这一个
            container.AddScoped<ITestServiceC, TestServiceC>();    //当前请求作用域内  只有当前这个实例

            container.AddSingleton<ITestServiceD>(new TestServiceD());  // 也是单例生命周期

            ServiceProvider provider = container.BuildServiceProvider() ?? throw new NullReferenceException();

            ITestServiceA? testA = provider.GetService<ITestServiceA>();
            ITestServiceA? testA1 = provider.GetService<ITestServiceA>();
            Console.WriteLine(object.ReferenceEquals(testA, testA1));

            ITestServiceB? testB = provider.GetService<ITestServiceB>();
            ITestServiceB? testB1 = provider.GetService<ITestServiceB>();
            Console.WriteLine(object.ReferenceEquals(testB, testB1));

            ITestServiceC? testC = provider.GetService<ITestServiceC>();
            ITestServiceC? testC1 = provider.GetService<ITestServiceC>();
            Console.WriteLine(object.ReferenceEquals(testC, testC1));

            IServiceScope scope = provider.CreateScope();
            ITestServiceC? testc3 = provider.GetService<ITestServiceC>();
            var testc4 = scope.ServiceProvider.GetService<ITestServiceC>();
            Console.WriteLine(object.ReferenceEquals(testc3, testc4));

            ITestServiceD? testD = provider.GetService<ITestServiceD>();
            ITestServiceD? testD1 = provider.GetService<ITestServiceD>();
            Console.WriteLine(object.ReferenceEquals(testD, testD1));

        }
    }
}

#region 
namespace Bingle.Core.Interface
{
    public interface ITestServiceA
    {
        void Show();
    }
}
namespace Bingle.Core.Service
{
    public class TestServiceA : ITestServiceA
    {
        public void Show()
        {
            Console.WriteLine("A123456");
        }
    }
}

namespace Bingle.Core.Interface
{
    public interface ITestServiceB
    {
        void Show();
    }
}

namespace Bingle.Core.Service
{
    public class TestServiceB : ITestServiceB
    {

        public TestServiceB(ITestServiceA iTestService)
        {

        }


        public void Show()
        {
            Console.WriteLine($"This is TestServiceB B123456");
        }
    }
}


namespace Bingle.Core.Interface
{
    public interface ITestServiceC
    {
        void Show();
    }
}

namespace Bingle.Core.Service
{
    public class TestServiceC : ITestServiceC
    {
        public TestServiceC(ITestServiceB iTestServiceB)
        {
        }
        public void Show()
        {
            Console.WriteLine("C123456");
        }
    }
}

namespace Bingle.Core.Interface
{
    public interface ITestServiceD
    {
        void Show();
    }
}

namespace Bingle.Core.Service
{
    public class TestServiceD : ITestServiceD
    {
        public void Show()
        {
            Console.WriteLine("D123456");
        }
    }
}
#endregion
