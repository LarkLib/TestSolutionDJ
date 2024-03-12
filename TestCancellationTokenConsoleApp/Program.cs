namespace TestCancellationTokenConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //-------------------------------------------------------------
            async Task Execute(CancellationToken token)
            {
                await Task.Delay(3000, token);
                Console.WriteLine("Executed");
            }

            CancellationTokenSource cts = new CancellationTokenSource(1000);
            cts.Token.Register(() => Console.WriteLine("任务已取消!"));

            _ = Execute(cts.Token);
            //-------------------------------------------------------------
            //https://www.cnblogs.com/shanfeng1000/p/13402152.html
            /*
               CancellationToken

　　            CancellationToken有一个构造函数，可以传入一个bool类型表示当前的CancellationToken是否是取消状态。另外，因为CancellationToken是一个结构体，所以它还有一个空参数的构造函数。　　

                public CancellationToken();//因为是结构体，才有空构造函数，不过没什么作用
                public CancellationToken(bool canceled);
　　            属性如下：　　

                //静态属性，获取一个空的CancellationToken，这个CancellationToken注册的回调方法不会被触发，作用类似于使用空构造函数得到的CancellationToken
                public static CancellationToken None { get; }
                //表示当前CancellationToken是否可以被取消
                public bool CanBeCanceled { get; }
                //表示当前CancellationToken是否已经是取消状态
                public bool IsCancellationRequested { get; }
                //和CancellationToken关联的WaitHandle对象，CancellationToken注册的回调方法执行时通过这个WaitHandle实现的
                public WaitHandle WaitHandle { get; }
　　            常用方法：　　

                //往CancellationToken中注册回调
                public CancellationTokenRegistration Register(Action callback);
                public CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext);
                public CancellationTokenRegistration Register([NullableAttribute(new[] { 1, 2 })] Action<object?> callback, object? state);
                public CancellationTokenRegistration Register([NullableAttribute(new[] { 1, 2 })] Action<object?> callback, object? state, bool useSynchronizationContext);
                //当CancellationToken处于取消状态是，抛出System.OperationCanceledException异常
                public void ThrowIfCancellationRequested();
　　            常用的注册回调的方法是上面4个Register方法，其中callback是回调执行的委托，useSynchronizationContext表示是否使用同步上下文，state是往回调委托中传的参数值

　　            另外，Register方法会返回一个CancellationTokenRegistration结构体，当注册回调之后，可以调用CancellationTokenRegistration的Unregister方法来取消注册，这个Unregister方法会返回一个bool值，当成功取消时返回true，当取消失败（比如回调已执行）将返回false:　　

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                var cancellationTokenRegistration = cancellationTokenSource.Token.Register(() =>
                {
                    Console.WriteLine("Canceled");//这里将不会执行输出
                });

                //cancellationTokenSource.Cancel();
                //var result = cancellationTokenRegistration.Unregister();//result = false

                var result = cancellationTokenRegistration.Unregister();//result = true
　　             cancellationTokenSource.Cancel();
 　　            上面提到，CancellationToken可以使用构造函数直接构造，同时可以传入一个参数，表示当前的状态，需要注意的是，CancellationToken的状态最多可以改变一次，也就是从未取消变成已取消。

　　            如果构造时传入true，也就是说CancellationToken是已取消状态，这个时候注册的回调都会立即执行：　　

                CancellationToken cancellationToken = new CancellationToken(true);
                cancellationToken.Register(() =>
                {
                    Console.WriteLine("Canceled");//这里会立即执行输出Canceled
                });
　　            但如果构造时传入的是false，说明CancellationToken处于未取消状态，这时候注册的回到都会处于一个待触发状态：　　

                CancellationToken cancellationToken = new CancellationToken(false);
                cancellationToken.Register(() =>
                {
                    Console.WriteLine("Canceled");//这里不会立即执行输出
                });
　　            通过Register方法注册的服务只会执行一次！

　　            但一般的，如果传入false构造出来的CancellationToken，可以认为是不会触发的，因为它没有触发的方法！所以一般的，我们都不会直接使用构造函数创建CancellationToken，而是使用CancellationTokenSource对象来获取一个CancellationToken

 

　　            CancellationTokenSource

　　            CancellationTokenSource可以理解为CancellationToken的控制器，控制它什么时候变成取消状态的一个对象，它有一个CancellationToken类型的属性Token，只要CancellationTokenSource创建，这个Token也会被创建，同时Token会和这个CancellationTokenSource绑定：　　

                //表示Token是否已处于取消状态
                public bool IsCancellationRequested { get; }
                //CancellationToken 对象
                public CancellationToken Token { get; }
　　            可以直接创建一个CancellationTokenSource对象，同时指定一个时间段，当过了这段时间后，CancellationTokenSource就会自动取消了。

　　            CancellationTokenSource的取消有4个方法：　　

                //立刻取消
                public void Cancel();
                //立刻取消
                public void Cancel(bool throwOnFirstException);
                //延迟指定时间后取消
                public void CancelAfter(int millisecondsDelay);
                //延迟指定时间后取消
                public void CancelAfter(TimeSpan delay);
　　            Cancel和两个CancelAfter方法没什么特别的，主要就是有一个延迟的效果，需要注意的是Cancel的两个重载之间的区别。

　　            首先，上面说道，CancellationToken状态只能改变一次（从未取消变成已取消），当CancellationToken时已取消状态时，每次往其中注册的回调都会立刻执行！当处于未取消状态时，注册进去的回调都会等待执行。

　　            需要注意的是，当在未取消状态下注册多个回调时，它们在执行时是一个类似栈的结构顺序，先注册后执行。

　　            而CancellationToken的Register可以注册多个回调，那他们可能都会抛出异常，throwOnFirstException参数表示在第一次报错时的处理行为.

　　            throwOnFirstException = true 表示立即抛出当前发生的异常，后续的回调将会取消执行　　

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                try
                {
                    cancellationTokenSource.Token.Register(() =>
                    {
                        throw new Exception("1");
                    });
                    cancellationTokenSource.Token.Register(() =>
                    {
                        throw new Exception("2");//不会执行
                    });

                    cancellationTokenSource.Cancel(true);
                }
                catch (Exception ex)
                {
                    //ex is System.Exception("1")
                }
 　　            throwOnFirstException = false 表示跳过当前回调的异常，继续执行生效的回调，等所有的回调执行完成之后，再将所有的异常打包成一个System.AggregateException异常抛出来！　　

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                try
                {
                    cancellationTokenSource.Token.Register(() =>
                    {
                        throw new Exception("1");
                    });
                    cancellationTokenSource.Token.Register(() =>
                    {
                        throw new Exception("2");
                    });

                    cancellationTokenSource.Cancel(false);//相当于cancellationTokenSource.Cancel()
                }
                catch (Exception ex)
                {
                    //ex is System.AggregateException:[Exception("2"),Exception("1")]
                }
 　　            CancellationTokenSource还可以与其它CancellationToken关联起来，生成一个新的CancellationToken，当其他CancellationToken取消时，会自动触发当前的CancellationTokenSource执行取消动作！　　

                CancellationTokenSource cancellationTokenSource1 = new CancellationTokenSource();
                cancellationTokenSource1.Token.Register(() =>
                {
                    Console.WriteLine("Cancel1");
                });
                CancellationTokenSource cancellationTokenSource2 = new CancellationTokenSource();
                cancellationTokenSource2.Token.Register(() =>
                {
                    Console.WriteLine("Cancel2");
                });
                CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationTokenSource1.Token, cancellationTokenSource2.Token);
                cancellationTokenSource.Token.Register(() =>
                {
                    Console.WriteLine("Cancel");
                });

                //cancellationTokenSource1.Cancel(); //执行这个依次输出 Cancel    Cancel1
                cancellationTokenSource2.Cancel(); //执行这个依次输出 Cancel    Cancel2
 

　　            使用场景一

　　            当我们创建异步操作时，可以传入一个CancellationToken，当异步操作处于等待执行状态时，可以通过设置CancellationToken为取消状态将异步操作取消执行：　　

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                var task = new Task(() =>
                {
                    Thread.Sleep(1500);//执行了2秒中代码
                    Console.WriteLine("Execute Some Code");
                }, cancellationTokenSource.Token);

                task.Start();//启动，等待调度执行

                //发现不对，可以取消task执行
                cancellationTokenSource.Cancel();
                Thread.Sleep(1000);//等待1秒
                Console.WriteLine("Task状态：" + task.Status);//Canceled
 　　            但是经常的，我们的取消动作可能不会那么及时，如果异步已经执行了，再执行取消时无效的，这是就需要我们自己在异步委托中检测了：　　

                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                var task = new Task(() =>
                {
                    Thread.Sleep(1500);//执行了2秒中代码
                    cancellationTokenSource.Token.ThrowIfCancellationRequested();
                    Console.WriteLine("Execute Some Code");
                }, cancellationTokenSource.Token);

                task.Start();//启动，等待调度执行

                Thread.Sleep(1000);////一段时间后发现不对，可以取消task执行
                cancellationTokenSource.Cancel();
                Thread.Sleep(1000);//等待1秒
                Console.WriteLine("Task状态：" + task.Status);//Canceled
　　

　　            使用场景二

 　　            有时，我们希望在触发某个时间后，可以执行某些代码功能，但是在异步环境下，我们不能保证那些要执行的代码是否已准备好了，比如我们有一个Close方法，当调用Close后表示是关闭状态，如果我们相当程序处于关闭状态时执行一些通知，一般的，我们可能是想到采用事件模型，或者在Close方法传入事件委托，或者采用一些诸如模板设计这样的模型去实现：　　　

                class Demo
                {
                    public void Close(Action callback)
                    {
                        //关闭
                        Thread.Sleep(3000);

                        callback?.Invoke();//执行通知
                    }
                }
　　            或者　　

                class Demo
                {
                    public event Action Callback;

                    public void Close()
                    {
                        //关闭
                        Thread.Sleep(3000);

                        Callback?.Invoke();//执行通知
                    }
                }
　　            但是这就有问题了，如果是传入参数或者采用事件模型，因为前面说过了，如果在异步环境下，我们不能保证那些要执行的代码是否已准备好了，也许在执行Close方法时，程序还未注册回调。

　　            这个时候就可以使用CancellationToken来解决这个问题：　　

                class Demo
                {
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                    public CancellationToken Token { get => cancellationTokenSource.Token; }

                    public void Close()
                    {
                        //关闭
                        Thread.Sleep(3000);

                        cancellationTokenSource.Cancel();//执行通知
                    }
                }
　　            主需要往Token属性中注册回调而无需关注Close什么时候执行了

 

　　            使用场景三

　　            有时候，我们写一个异步无限循环的方法去处理一些问题，而我们希望可以在方法外来停止它这个时候，我们就可以通过返回CancellationTokenSource来实现了：　　

                    public CancellationTokenSource Listen()
                    {
                        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                        //循环调度执行
                        Task.Run(() =>
                        {
                            while (true)
                            {
                                cancellationTokenSource.Token.ThrowIfCancellationRequested();

                                //循环执行一些操作
                                Thread.Sleep(1000);
                                Console.WriteLine("Run");
                            }
                        });

                        return cancellationTokenSource;
                    }
 　　

　　            使用场景四

　　            这种场景比较常见，它常将CancellationToken作为方法的参数，在方法内部可以往CancellationToken中注册回调委托，而CancellationToken的取消动作则是在方法外部触发，例如：

                static void Main(string[] args)
                {
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    Method("hello", cancellationTokenSource.Token);
                    cancellationTokenSource.Cancel();
                }
                static void Method(string message, CancellationToken cancellationToken = default)
                {
                    Console.WriteLine($"message:{message}");
                    cancellationToken.Register(() =>
                    {
                        Console.WriteLine($"complete");
                    });
                }
　　            这样做的好处是，可以让方法内部产生的事件交给方法调用主流程去触发，而又不干扰方法调用主流程的逻辑，具有很好的可控制性。还可以传进来一个注册好回调的CancellationToken，在方法内部适当的位置去触发取消动作。目前，.netcore内部大量的方法采用了这种方式，比如IHostedService：　　

                public interface IHostedService
                {
                    Task StartAsync(CancellationToken cancellationToken);
                    Task StopAsync(CancellationToken cancellationToken);
                }
　　            StartAsync在应用启动前执行，StopAsync是在应用停止前执行
                */

            //https://www.cnblogs.com/wucy/p/15128365.html
            /*
            浅谈C#取消令牌CancellationTokenSource
            前言#
                相信大家在使用C#进行开发的时候，特别是使用异步的场景，多多少少会接触到CancellationTokenSource。看名字就知道它和取消异步任务相关的，而且一看便知大名鼎鼎的CancellationToken就是它生产出来的。不看不知道，一看吓一跳。它在取消异步任务、异步通知等方面效果还是不错的，不仅好用而且够强大。无论是微软底层类库还是开源项目涉及到Task相关的，基本上都能看到它的身影，而微软近几年也是很重视框架中的异步操作，特别是在.NET Core上基本上能看到Task的地方就能看到CancellationTokenSource的身影。这次我们抱着学习的态度，来揭开它的神秘面纱。

            简单示例#
            相信对于CancellationTokenSource基本的使用，许多同学已经非常熟悉了。不过为了能够让大家带入文章的节奏，我们还是打算先展示几个基础的操作，让大家找找感觉，回到那个熟悉的年代。

            基础操作#
            首先呈现一个最基础的操作。

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            cancellationToken.Register(() => System.Console.WriteLine("取消了？？？"));
            cancellationToken.Register(() => System.Console.WriteLine("取消了！！！"));
            cancellationToken.Register(state => System.Console.WriteLine($"取消了。。。{state}"),"啊啊啊");
            System.Console.WriteLine("做了点别的,然后取消了.");
            cancellationTokenSource.Cancel();
            这个操作是最简单的操作，我们上面提到过CancellationTokenSource就是用来生产CancellationToken的，还可以说CancellationToken是CancellationTokenSource的表现，这个待会看源码的时候我们会知道为啥这么说。这里呢我们给CancellationToken注册几个操作，然后使用CancellationTokenSource的Cancel方法取消操作，这时候控制台就会打印结果如下

            做了点别的,然后取消了.
            取消了。。。啊啊啊
            取消了！！！
            取消了？？？
            通过上面简单的示例，大家应该非常轻松的理解了它的简单使用。

            定时取消#
            有的时候呢我们可能需要超时操作，比如我不想一直等着，到了一个固定的时间我就要取消操作，这时候我们可以利用CancellationTokenSource的构造函数给定一个限定时间，过了这个时间CancellationTokenSource就会被取消了，操作如下

            //设置3000毫秒(即3秒)后取消
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(3000);
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            cancellationToken.Register(() => System.Console.WriteLine("我被取消了."));
            System.Console.WriteLine("先等五秒钟.");
            await Task.Delay(5000);
            System.Console.WriteLine("手动取消.")
            cancellationTokenSource.Cancel();
            然后在控制台打印的结果是这个样子的，活脱脱的为我们实现了内建的超时操作。

            先等五秒钟.
            我被取消了.
            手动取消.
            上面的写法是在构造CancellationTokenSource的时候设置超时等待，还有另一种写法等同于这种写法，使用的是CancelAfter方法，具体使用如下

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.Token.Register(() => System.Console.WriteLine("我被取消了."));
            //五秒之后取消
            cancellationTokenSource.CancelAfter(5000);
            System.Console.WriteLine("不会阻塞,我会执行.");
            这个操作也是定时取消操作，需要注意的是CancelAfter方法并不会阻塞执行，所以打印的结果是

            不会阻塞,我会执行.
            我被取消了.
            关联取消#
            还有的时候是这样的场景，就是我们设置一组关联的CancellationTokenSource，我们期望的是只要这一组里的任意一个CancellationTokenSource被取消了，那么这个被关联的CancellationTokenSource就会被取消。说得通俗一点就是，我们几个当中只要一个不在了，那么你也可以不在了，具体的实现方式是这样的

            //声明几个CancellationTokenSource
            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationTokenSource tokenSource2 = new CancellationTokenSource();
            CancellationTokenSource tokenSource3 = new CancellationTokenSource();

            tokenSource2.Token.Register(() => System.Console.WriteLine("tokenSource2被取消了"));

            //创建一个关联的CancellationTokenSource
            CancellationTokenSource tokenSourceNew = CancellationTokenSource.CreateLinkedTokenSource(tokenSource.Token, tokenSource2.Token, tokenSource3.Token);
            tokenSourceNew.Token.Register(() => System.Console.WriteLine("tokenSourceNew被取消了"));
            //取消tokenSource2
            tokenSource2.Cancel();
            上述示例中因为tokenSourceNew关联了tokenSource、tokenSource2、tokenSource3所以只要他们其中有一个被取消那么tokenSourceNew也会被取消，所以上述示例的打印结果是

            tokenSourceNew被取消了
            tokenSource2被取消了
            判断取消#
            上面我们使用的方式，都是通过回调的方式得知CancellationTokenSource被取消了，没办法通过标识去得知CancellationTokenSource是否可用。不过微软贴心的为我们提供了IsCancellationRequested属性去判断，需要注意的是它是CancellationToken的属性，具体使用方式如下

            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = tokenSource.Token;
            //打印被取消
            cancellationToken.Register(() => System.Console.WriteLine("被取消了."));
            //模拟传递的场景
            Task.Run(async ()=> {
                while (!cancellationToken.IsCancellationRequested)
                {
                    System.Console.WriteLine("一直在执行...");
                    await Task.Delay(1000);
                }
            });
            //5s之后取消
            tokenSource.CancelAfter(5000);
            上述代码五秒之后CancellationTokenSource被取消，因此CancellationTokenSource的Token也会被取消。反映到IsCancellationRequested上就是值为true说明被取消，为false说明没被取消，因此控制台输出的结果是

            一直在执行...
            一直在执行...
            一直在执行...
            一直在执行...
            一直在执行...
            被取消了.
            还有另一种方式，也可以主动判断任务是否被取消，不过这种方式简单粗暴，直接是抛出了异常。如果是使用异步的方式的话，需要注意的是Task内部异常的捕获方式，否则对外可能还没有感知到具体异常的原因，它的使用方式是这样的，这里为了演示方便我直接换了一种更直接的方式

            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = tokenSource.Token;
            cancellationToken.Register(() => System.Console.WriteLine("被取消了."));
            tokenSource.CancelAfter(5000);
            while (true)
            {
                //如果操作被取消则直接抛出异常
                cancellationToken.ThrowIfCancellationRequested();
                System.Console.WriteLine("一直在执行...");
                await Task.Delay(1000);
            }
            执行五秒之后则直接抛出 System.OperationCanceledException: The operation was canceled.异常，异步情况下注意异常处理的方式即可。通过上面这些简单的示例，相信大家对CancellationTokenSource有了一定的认识，大概知道了在什么时候可以使用它，主要是异步取消通知，或者限定时间操作通知等等。CancellationTokenSource是个不错的神器，使用简单功能强大。
            */
            Console.WriteLine("Hello, World!");
            Console.ReadKey();
        }
    }
}