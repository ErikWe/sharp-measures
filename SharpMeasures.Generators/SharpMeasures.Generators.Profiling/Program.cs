namespace SharpMeasures.Generators.Profiling;

using System.Threading.Tasks;

internal static class Program
{
    public static async Task Main()
    {
        await Uncached.Full.Run().ConfigureAwait(false);
    }
}
