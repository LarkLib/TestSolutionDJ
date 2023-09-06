namespace TestSourceGeneratorConsoleApp
{
    //https://learn.microsoft.com/en-us/dotnet/csharp/roslyn-sdk/source-generators-overview
    //<ItemGroup>
    //  <ProjectReference Include = "..\TestSourceGeneratorLibrary\TestSourceGeneratorLibrary.csproj" OutputItemType="Analyzer" ReferenceOutputAssembly="false" />
    //</ItemGroup>
    //The key action is to append paramters of OutputItemType="Analyzer" ReferenceOutputAssembly="false"

    public partial class Program
    {
        static void Main(string[] args)
        {
            HelloFrom("Generated Code");
            //Console.WriteLine("Hello, World!");
        }
        static partial void HelloFrom(string name);
    }
}