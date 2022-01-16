namespace SharpMeasures.Benchmark;

using BenchmarkDotNet.Running;

internal static class Program
{
    static void Main()
    {
        BenchmarkRunner.Run(typeof(Program).Assembly);
    }
}
