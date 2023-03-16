namespace SharpMeasures.Generators.Profiling.Uncached;

using SharpMeasures.Generators.DriverUtility;

internal static class SimpleScalar
{
    public static void Run()
    {
        DriverConstruction.ConstructAndRun<SharpMeasuresGenerator>(Text);
    }

    private static string Text => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
