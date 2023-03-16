namespace SharpMeasures.Generators.Profiling.Uncached;

using SharpMeasures.Generators.DriverUtility;

using System.Threading.Tasks;

internal static class Full
{
    public static async Task SharpMeasures()
    {
        await ExistingSolutionDriver.Build(@"C:\Users\erik_\Dev\Git\sharp-measures\SharpMeasures\SharpMeasures.sln", "SharpMeasures").ConfigureAwait(false);
    }

    public static async Task ExtensionTest()
    {
        await ExistingSolutionDriver.Build(@"C:\Users\erik_\Dev\Git\sharp-measures\SharpMeasures.ExtensionTest\SharpMeasures.ExtensionTest.sln", "SharpMeasures.ExtensionTest").ConfigureAwait(false);
    }
}
