namespace SharpMeasures.Generators.Tests.CrossAssembly.Scalars;

using SharpMeasures.Generators.DriverUtility;
using SharpMeasures.Generators.Tests.Verify;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnitInOtherAssembly
{
    [Fact]
    public async void RunTest()
    {
        var driverAndCompilation = await CrossAssemblyDriverConstruction.ConstructAndRun<SharpMeasuresGenerator>(LocalSource, ForeignSource, ProjectPath.Path + @"\Documentation").ConfigureAwait(false);

        Assert.NotNull(driverAndCompilation);

        GeneratorVerifier.Construct(LocalSource, driverAndCompilation.Value.Driver, driverAndCompilation.Value.Compilation, new GeneratorVerifierSettings(false, false));
    }

    private static string LocalSource => """
        using SharpMeasures.Generators.Scalars;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Distance { }
        """;

    private static string ForeignSource => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
