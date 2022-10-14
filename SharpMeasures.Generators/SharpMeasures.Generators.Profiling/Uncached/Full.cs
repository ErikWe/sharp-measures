namespace SharpMeasures.Generators.Profiling.Uncached;

using SharpMeasures.Generators.DriverUtility;

using System.Threading.Tasks;

internal static class Full
{
    public static async Task Run()
    {
        await ExistingSolutionDriver.Build(@"C:\Users\erik_\Dev\Git\sharp-measures\SharpMeasures\SharpMeasures.sln").ConfigureAwait(false);
    }
}
