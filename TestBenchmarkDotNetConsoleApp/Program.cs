using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Engines;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Mathematics;
using BenchmarkDotNet.Running;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Perfolizer.Mathematics.OutlierDetection;
using System;

namespace TestBenchmarkDotNetConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //snippet illustrates how you can trigger a benchmark on all types in the specified assembly:
            //var summary = BenchmarkRunner.Run(typeof(Program).Assembly);

            //You can use the following code snippet to run benchmarking on a specific type:
            //var summary = BenchmarkRunner.Run<BenchmarkLINQPerformance>();

            //Or you can use:
            //var summary = BenchmarkRunner.Run(typeof(BenchmarkLINQPerformance));

            //var summary = BenchmarkRunner.Run<IntroSetupCleanupIteration>();
            var summary = BenchmarkRunner.Run<IntroOutliers>();
            Console.WriteLine("Hello, World!");
        }
    }



    //Sample https://benchmarkdotnet.org/articles/samples/IntroArguments.html
    //Jobs https://benchmarkdotnet.org/articles/configs/jobs.html
    /*
Basically, a job describes how to run your benchmark. Practically, it's a set of characteristics which can be specified. You can specify one or several jobs for your benchmarks.

Characteristics
There are several categories of characteristics which you can specify. Let's consider each category in detail.

Id
It's a single string characteristic. It allows to name your job. This name will be used in logs and a part of a folder name with generated files for this job. Id doesn't affect benchmark results, but it can be useful for diagnostics. If you don't specify Id, random value will be chosen based on other characteristics

Environment
Environment specifies an environment of the job. You can specify the following characteristics:

Platform: x86 or x64
Runtime:
Clr: Full .NET Framework (available only on Windows)
Core: CoreCLR (x-plat)
Mono: Mono (x-plat)
Jit:
LegacyJit (available only for Runtime.Clr)
RyuJit (available only for Runtime.Clr and Runtime.Core)
Llvm (available only for Runtime.Mono)
Affinity: Affinity of a benchmark process
GcMode: settings of Garbage Collector
Server: true (Server mode) or false (Workstation mode)
Concurrent: true (Concurrent mode) or false (NonConcurrent mode)
CpuGroups: Specifies whether garbage collection supports multiple CPU groups
Force: Specifies whether the BenchmarkDotNet's benchmark runner forces full garbage collection after each benchmark invocation
AllowVeryLargeObjects: On 64-bit platforms, enables arrays that are greater than 2 gigabytes (GB) in total size
LargeAddressAware: Specifies that benchmark can handle addresses larger than 2 gigabytes. See also: Sample: IntroLargeAddressAware and LARGEADDRESSAWARE
false: Benchmark uses the defaults (64-bit: enabled; 32-bit: disabled).
true: Explicitly specify that benchmark can handle addresses larger than 2 gigabytes.
EnvironmentVariables: customized environment variables for target benchmark. See also: Sample: IntroEnvVars
BenchmarkDotNet will use host process environment characteristics for non specified values.

Run
In this category, you can specify how to benchmark each method.

RunStrategy:
Throughput: default strategy which allows to get good precision level
ColdStart: should be used only for measuring cold start of the application or testing purpose
Monitoring: A mode without overhead evaluating, with several target iterations
LaunchCount: how many times we should launch process with target benchmark
WarmupCount: how many warmup iterations should be performed
IterationCount: how many target iterations should be performed (if specified, BenchmarkDotNet.Jobs.RunMode.MinIterationCount and BenchmarkDotNet.Jobs.RunMode.MaxIterationCount will be ignored)
IterationTime: desired time of a single iteration
UnrollFactor: how many times the benchmark method will be invoked per one iteration of a generated loop
InvocationCount: count of invocation in a single iteration (if specified, IterationTime will be ignored), must be a multiple of UnrollFactor
MinIterationCount: Minimum count of target iterations that should be performed, the default value is 15
MaxIterationCount: Maximum count of target iterations that should be performed, the default value is 100
MinWarmupIterationCount: Minimum count of warmup iterations that should be performed, the default value is 6
MaxWarmupIterationCount: Maximum count of warmup iterations that should be performed, the default value is 50
Usually, you shouldn't specify such characteristics like LaunchCount, WarmupCount, IterationCount, or IterationTime because BenchmarkDotNet has a smart algorithm to choose these values automatically based on received measurements. You can specify it for testing purposes or when you are damn sure that you know the right characteristics for your benchmark (when you set IterationCount = 20 you should understand why 20 is a good value for your case).

Accuracy
If you want to change the accuracy level, you should use the following characteristics instead of manually adjusting values of WarmupCount, IterationCount, and so on.

MaxRelativeError, MaxAbsoluteError: Maximum acceptable error for a benchmark (by default, BenchmarkDotNet continue iterations until the actual error is less than the specified error). In these two characteristics, the error means half of 99.9% confidence interval. MaxAbsoluteError is an absolute TimeInterval; doesn't have a default value. MaxRelativeError defines max acceptable ((<half of CI 99.9%>) / Mean).
MinIterationTime: Minimum time of a single iteration. Unlike Run.IterationTime, this characteristic specifies only the lower limit. In case of need, BenchmarkDotNet can increase this value.
MinInvokeCount: Minimum about of target method invocation. Default value if 4 but you can decrease this value for cases when single invocations takes a lot of time.
EvaluateOverhead: if your benchmark method takes nanoseconds, BenchmarkDotNet overhead can significantly affect measurements. If this characteristic is enabled, the overhead will be evaluated and subtracted from the result measurements. Default value is true.
WithOutlierMode: sometimes you could have outliers in your measurements. Usually these are unexpected outliers which arose because of other processes activities. By default (OutlierMode.RemoveUpper), all upper outliers (which is larger than Q3) will be removed from the result measurements. However, some of benchmarks have expected outliers. In these situation, you expect that some of invocation can produce outliers measurements (e.g. in case of network activities, cache operations, and so on). If you want to see result statistics with these outliers, you should use OutlierMode.DontRemove. If you can also choose OutlierMode.RemoveLower (outliers which are smaller than Q1 will be removed) or OutlierMode.RemoveAll (all outliers will be removed). See also: @BenchmarkDotNet.Mathematics.OutlierMode
AnalyzeLaunchVariance: this characteristic makes sense only if Run.LaunchCount is default. If this mode is enabled and, BenchmarkDotNet will try to perform several launches and detect if there is a variance between launches. If this mode is disable, only one launch will be performed.
Infrastructure
Usually, you shouldn't specify any characteristics from this section, it can be used for advanced cases only.

Toolchain: a toolchain which generates source code for target benchmark methods, builds it, and executes it. BenchmarkDotNet has own toolchains for .NET, .NET Core, Mono and CoreRT projects. If you want, you can define own toolchain.
Clock: a clock which will be used for measurements. BenchmarkDotNet automatically choose the best available clock source, but you can specify own clock source.
EngineFactory: a provider for measurement engine which performs all the measurement magic. If you don't trust BenchmarkDotNet, you can define own engine and implement all the measurement stages manually.
Usage
There are several ways to specify a job.

Object style
You can create own jobs directly from the source code via a custom config:

[Config(typeof(Config))]
public class MyBenchmarks
{
    private class Config : ManualConfig
    {
        public Config()
        {
            Add(
                new Job("MySuperJob", RunMode.Dry, EnvMode.RyuJitX64)
                {
                    Env = { Runtime = Runtime.Core },
                    Run = { LaunchCount = 5, IterationTime = TimeInterval.Millisecond * 200 },
                    Accuracy = { MaxStdErrRelative = 0.01 }
                });

            // The same, using the .With() factory methods:
            Add(
                Job.Dry
                .WithPlatform(Platform.X64)
                .WithJit(Jit.RyuJit)
                .WithRuntime(Runtime.Core)
                .WithLaunchCount(5)
                .WithIterationTime(TimeInterval.Millisecond * 200)
                .WithMaxRelativeError(0.01)
                .WithId("MySuperJob"));
        }
    }
    // Benchmarks
}
Basically, it's a good idea to start with predefined values (e.g. EnvMode.RyuJitX64 and RunMode.Dry passed as constructor args) and modify rest of the properties using property setters or with help of object initializer syntax.

Note that the job cannot be modified after it's added into config. Trying to set a value on property of the frozen job will throw an InvalidOperationException. Use the Job.Frozen property to determine if the code properties can be altered.

If you do want to create a new job based on frozen one (all predefined job values are frozen) you can use the .With() extension method

            var newJob = Job.Dry.With(Platform.X64);
or pass the frozen value as a constructor argument

            var newJob = new Job(Job.Dry) { Env = { Platform = Platform.X64 } };
or use the .Apply() method on unfrozen job

            var newJob = new Job() { Env = { Platform = Platform.X64 } }.Apply(Job.Dry);
in any case the Id property will not be transfered and you must pass it explicitly (using the .ctor id argument or the .WithId() extension method).

Attribute style
You can also add new jobs via attributes. Examples:

[DryJob]
[ClrJob, CoreJob, MonoJob]
[LegacyJitX86Job, LegacyJitX64Job, RyuJitX64Job]
[SimpleJob(RunStrategy.ColdStart, launchCount: 1, warmupCount: 5, iterationCount: 5, id: "FastAndDirtyJob")]
public class MyBenchmarkClass
Note that each of the attributes identifies a separate job, the sample above will result in 8 different jobs, not a single merged job.

Attribute style for merging jobs
Sometimes you want to apply some changes to other jobs, without adding a new job to a config (which results in one extra benchmark run).

To do that you can use following predefined job mutator attributes:

[EvaluateOverhead]
[GcConcurrent]
[GcForce]
[GcServer]
[InnerIterationCount]
[InvocationCount]
[IterationCount]
[IterationTime]
[MaxAbsoluteError]
[MaxIterationCount]
[MaxRelativeError]
[MinInvokeCount]
[MinIterationCount]
[MinIterationTime]
[Outliers]
[ProcessCount]
[RunOncePerIteration]
[WarmupCount]
[MinWarmupCount]
[MaxWarmupCount]
So following example:

[ClrJob, CoreJob]
[GcServer(true)]
public class MyBenchmarkClass
Is going to be merged to a config with two jobs:

CoreJob with GcServer=true
ClrJob with GcServer=true
Custom attributes
You can also create your own custom attributes with your favourite set of jobs. Example:

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Assembly)]
public class MySuperJobAttribute : Attribute, IConfigSource
{
    protected MySuperJobAttribute()
    {
        var job = new Job("MySuperJob", RunMode.Core);
        job.Env.Platform = Platform.X64;
        Config = ManualConfig.CreateEmpty().With(job);
    }

    public IConfig Config { get; }
}

[MySuperJob]
public class MyBenchmarks
Sample: IntroGcMode
Source code
using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Order;

namespace BenchmarkDotNet.Samples
{
    [Config(typeof(Config))]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [MemoryDiagnoser]
    public class IntroGcMode
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                AddJob(Job.MediumRun.WithGcServer(true).WithGcForce(true).WithId("ServerForce"));
                AddJob(Job.MediumRun.WithGcServer(true).WithGcForce(false).WithId("Server"));
                AddJob(Job.MediumRun.WithGcServer(false).WithGcForce(true).WithId("Workstation"));
                AddJob(Job.MediumRun.WithGcServer(false).WithGcForce(false).WithId("WorkstationForce"));
            }
        }

        [Benchmark(Description = "new byte[10kB]")]
        public byte[] Allocate()
        {
            return new byte[10000];
        }

        [Benchmark(Description = "stackalloc byte[10kB]")]
        public unsafe void AllocateWithStackalloc()
        {
            var array = stackalloc byte[10000];
            Consume(array);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static unsafe void Consume(byte* input)
        {
        }
    }
}
     */

    //JobBaseline
    //If you want to compare several runtime configuration, you can mark one of your jobs with baseline = true.

    //Configs https://benchmarkdotnet.org/articles/configs/configs.html
    //Config is a set of so called jobs, columns, exporters, loggers, diagnosers, analysers, validators that help you to build your benchmark.

    //StatisticsColumns
    //偏度(skewness)和峰度(kurtosis）https://zhuanlan.zhihu.com/p/53184516
    //[MediumRunJob, SkewnessColumn, KurtosisColumn]
    //RankColumn
    //[MValueColumn]
    //Disassembler https://benchmarkdotnet.org/articles/features/disassembler.html
    //EtwProfiler https://benchmarkdotnet.org/articles/features/etwprofiler.html
    //EventPipeProfiler https://benchmarkdotnet.org/articles/features/event-pipe-profiler.html
    //在类上可以加入[MemoryDiagnoser]特性，以查看方法使用内存情况。
    
    [Config(typeof(Config))]
    [RankColumn(NumeralSystem.Arabic)]
    [RankColumn(NumeralSystem.Roman)]
    [RankColumn(NumeralSystem.Stars)]
    [MemoryDiagnoser]
    [SimpleJob(RunStrategy.Monitoring, launchCount: 1, warmupCount: 2, iterationCount: 3, baseline: true)]
    public class IntroSetupCleanupIteration
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                AddColumn(
                    StatisticColumn.P0,
                    StatisticColumn.P25,
                    StatisticColumn.P50,
                    StatisticColumn.P67,
                    StatisticColumn.P80,
                    StatisticColumn.P85,
                    StatisticColumn.P90,
                    StatisticColumn.P95,
                    StatisticColumn.P100);
            }
        }

        #region function
        private int setupCounter;
        private int cleanupCounter;

        [IterationSetup]
        public void IterationSetup()
            => Console.WriteLine($"// IterationSetup ({++setupCounter})");

        [IterationCleanup]
        public void IterationCleanup()
            => Console.WriteLine($"// IterationCleanup ({++cleanupCounter})");


        [GlobalSetup]
        public void GlobalSetup()
            => Console.WriteLine("// " + "GlobalSetup");

        [GlobalCleanup]
        public void GlobalCleanup()
            => Console.WriteLine("// " + "GlobalCleanup");

        #region SetupCleanupTarget
        //SetupCleanupTarget
        //Sometimes it's useful to run setup or cleanups for specific benchmarks. All four setup and cleanup attributes have a Target property that allow the setup/cleanup method to be run for one or more specific benchmark methods.
        [GlobalSetup(Target = nameof(BenchmarkA))]
        public void GlobalSetupA()
            => Console.WriteLine("// " + "GlobalSetup A");

        [Benchmark]
        public void BenchmarkA()
            => Console.WriteLine("// " + "Benchmark A");

        [GlobalSetup(Targets = new[] { nameof(BenchmarkB), nameof(BenchmarkC) })]
        public void GlobalSetupB()
            => Console.WriteLine("// " + "GlobalSetup B");

        [Benchmark]
        public void BenchmarkB()
            => Console.WriteLine("// " + "Benchmark B");

        [Benchmark]
        public void BenchmarkC()
            => Console.WriteLine("// " + "Benchmark C");

        #endregion

        [Benchmark]
        public void BenchmarkD()
            => Console.WriteLine("// " + "Benchmark D");

        //BenchmarkBaseline
        //You can mark a method as a baseline with the help of[Benchmark(Baseline = true)].
        //As a result, you will have additional Ratio column in the summary table:

        //RatioSD
        //The ratio of two benchmarks is not a single number, it's a distribution. In most simple cases, the range of the ratio distribution is narrow, and BenchmarkDotNet displays a single column Ratio with the mean value. However, it also adds the RatioSD column (the standard deviation of the ratio distribution) in complex situations. In the below example, the baseline benchmark is spoiled by a single outlier

        //CategoryBaseline
        //The only way to have several baselines in the same class is to separate them by categories and mark the class with [GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)].
        [Benchmark(Baseline = true)]
        public void Benchmark()
            => Console.WriteLine("// " + "Benchmark");

        [Benchmark]
        [BenchmarkCategory("Slow")]
        public void Benchmark2()
            => Console.WriteLine("// " + "Benchmark2");
        #endregion

        #region features
        //Parameterization
        //You can mark one or several fields or properties in your class by the[Params] attribute.
        //In this attribute, you can specify set of values. Every value must be a compile-time constant.
        //As a result, you will get results for each combination of params values.

        //ParamsPriority
        //In order to sort columns of parameters in the results table you can use the Property Priority inside the params attribute.The priority range is [Int32.MinValue;Int32.MaxValue], lower priorities will appear earlier in the column order.The default priority is set to 0.

        [Params(100, 200, Priority = -100)]
        public int A { get; set; }

        [Params(10, 20)]
        public int B { get; set; }

        [Benchmark]
        public void BenchmarkParams() => Console.Write($"{A:3} + {B:3} ={(A + B):3}");
        /////////////////////////////////////////////////////////////////////////////////////
        //ParamsSource
        //In case you want to use a lot of values,
        //you should use[ParamsSource] You can mark one or several fields or properties in your class by the[Params] attribute.
        //In this attribute, you have to specify the name of public method/property which is going to provide
        //the values(something that implements IEnumerable).
        //The source must be within benchmarked type!
        // property with public setter
        [ParamsSource(nameof(ValuesForA))]
        public int C { get; set; }

        // public field
        [ParamsSource(nameof(ValuesForB))]
        public int D;

        // public property
        public IEnumerable<int> ValuesForA => new[] { 100, 200 };

        // public static method
        public static IEnumerable<int> ValuesForB() => new[] { 10, 20 };

        // public static method
        [Benchmark]
        public void BenchmarkParamsSource() => Console.Write($"{A:3} + {B:3} ={(A + B):3}");
        /////////////////////////////////////////////////////////////////////////////////////
        //ParamsAllValues
        //If you want to use all possible values of an enum or another type with a small number of values, you can use the[ParamsAllValues] attribute, instead of listing all the values by hand.The types supported by the attribute are:
        //bool
        //any enum that is not marked with[Flags]
        //Nullable<T>, where T is an enum or boolean
        public enum CustomEnum
        {
            One = 1,
            Two,
            Three
        }

        [ParamsAllValues]
        public CustomEnum E { get; set; }

        [ParamsAllValues]
        public bool? F { get; set; }

        [Benchmark]
        public void BenchmarkParamsAllValues() => Console.Write($"BenchmarkParamsAllValues: {(int)E * 100:5} + {(F == true ? 20 : F == false ? 10 : 0):5}");
        /////////////////////////////////////////////////////////////////////////////////////
        //Arguments
        //As an alternative to using [Params], you can specify arguments for your benchmarks. There are several ways to do it (described below).
        //The[Arguments] allows you to provide a set of values.Every value must be a compile-time constant(it's C# language limitation for attributes in general). You can also combine [Arguments] with [Params]. As a result, you will get results for each combination of params values.
        [Params(true, false)] // Arguments can be combined with Params
        public bool AddExtra5Milliseconds;

        [Benchmark]
        [Arguments(100, 10)]
        [Arguments(100, 20)]
        [Arguments(200, 10)]
        [Arguments(200, 20)]
        public void BenchmarkArguments(int a, int b)
        {
            if (AddExtra5Milliseconds)
                Thread.Sleep(a + b + 5);
            else
                Thread.Sleep(a + b);
        }
        /////////////////////////////////////////////////////////////////////////////////////
        //ArgumentsSource
        //In case you want to use a lot of values, you should use[ArgumentsSource].
        //You can mark one or several fields or properties in your class by the[ArgumentsSource] attribute.
        //In this attribute, you have to specify the name of public method/property
        //which is going to provide the values(something that implements IEnumerable).
        //The source must be within benchmarked type!
        [Benchmark]
        [ArgumentsSource(nameof(Numbers))]
        public double ManyArguments(double x, double y) => Math.Pow(x, y);

        public IEnumerable<object[]> Numbers() // for multiple arguments it's an IEnumerable of array of objects (object[])
        {
            yield return new object[] { 1.0, 1.0 };
            yield return new object[] { 2.0, 2.0 };
            yield return new object[] { 4.0, 4.0 };
            yield return new object[] { 10.0, 10.0 };
        }

        [Benchmark]
        [ArgumentsSource(nameof(TimeSpans))]
        public void SingleArgument(TimeSpan time) => Thread.Sleep(time);

        public IEnumerable<object> TimeSpans() // for single argument it's an IEnumerable of objects (object)
        {
            yield return TimeSpan.FromMilliseconds(10);
            yield return TimeSpan.FromMilliseconds(100);
        }
        /////////////////////////////////////////////////////////////////////////////////////
        //Multimodal

        /////////////////////////////////////////////////////////////////////////////////////
        #endregion
    }
    public class BenchmarkLINQPerformance
    {
        private readonly List<string>
        data = new List<string>();

        [GlobalSetup]
        public void GlobalSetup()
        {
            for (int i = 65; i < 90; i++)
            {
                char c = (char)i;
                data.Add(c.ToString());
            }
        }

        [Benchmark]
        public string Single() => data.SingleOrDefault(x => x.Equals("M"));

        [Benchmark]
        public string First() => data.FirstOrDefault(x => x.Equals("M"));
    }

    [Config(typeof(Config))]
    public class IntroOutliers
    {
        private class Config : ManualConfig
        {
            public Config()
            {
                var jobBase = Job.Default.WithWarmupCount(0).WithIterationCount(10).WithInvocationCount(1).WithUnrollFactor(1);
                AddJob(jobBase.WithOutlierMode(OutlierMode.DontRemove).WithId("DontRemoveOutliers"));
                AddJob(jobBase.WithOutlierMode(OutlierMode.RemoveUpper).WithId("RemoveUpperOutliers"));
            }
        }

        private int counter;

        [Benchmark]
        public void Foo()
        {
            counter++;
            int noise = counter % 10 == 0 ? 500 : 0;
            Thread.Sleep(100 + noise);
        }
    }
}