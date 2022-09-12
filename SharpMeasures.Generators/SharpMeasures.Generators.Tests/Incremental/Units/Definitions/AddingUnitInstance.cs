namespace SharpMeasures.Generators.Tests.Incremental.Units.Definitions;

using SharpMeasures.Generators.DriverUtility;
using SharpMeasures.Generators.Tests.Verify;

using Xunit;

public class AddingUnitInstance
{
    [Fact]
    public async void RunTest()
    {
        var driver = await IncrementalDriver.Build(InitialText, ProjectPath.Path + @"\Documentation").ConfigureAwait(false);

        driver.ApplyChange(SecondText);
        driver.ApplyChange(ThirdText);

        var compilation = await driver.ApplyChangeAndRetrieveCompilation(FinalText).ConfigureAwait(false);

        Assert.NotNull(compilation);

        GeneratorVerifier.Construct(FinalText, driver.Driver, compilation!);
    }

    private static string InitialText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SecondText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [PrefixedU
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ThirdText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", 
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string FinalText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [FixedUnitInstance("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
