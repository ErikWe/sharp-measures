namespace SharpMeasures.Generators.Tests.CrossAssembly.SpecializedScalars;

using SharpMeasures.Generators.DriverUtility;
using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class BaseInOtherAssembly
{
    [Fact]
    public async void RunTest()
    {
        var driverAndCompilation = await CrossAssemblyDriverConstruction.ConstructAndRun<SharpMeasuresGenerator>(LocalSource, ForeignSource).ConfigureAwait(false);

        Assert.NotNull(driverAndCompilation);

        GeneratorVerifier.Construct(LocalSource, driverAndCompilation.Value.Driver, driverAndCompilation.Value.Compilation, GeneratorVerifierSettings.TestCodeAssertions).AssertAllListedSourceNamesGenerated("Distance.Common.g.cs");
    }

    private static string LocalSource => """
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }
        """;

    private static string ForeignSource => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
