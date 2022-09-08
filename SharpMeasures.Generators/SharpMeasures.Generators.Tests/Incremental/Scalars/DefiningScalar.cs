namespace SharpMeasures.Generators.Tests.Incremental.Scalars;

using SharpMeasures.Generators.DriverUtility;

using Xunit;

public class DefiningScalar
{
    [Fact]
    public async void Assert()
    {
        var driver = await IncrementalDriver.Build(InitialText, ProjectPath.Path + @"\Documentation").ConfigureAwait(false);

        driver.ApplyChange(SecondText);
        driver.ApplyChange(ThirdText);
        driver.ApplyChange(FourthText);
        driver.ApplyChange(FifthText);
        driver.ApplyChange(FinalText);
    }

    private static string InitialText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial record
        """;

    private static string SecondText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial record struc
        """;

    private static string ThirdText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial record struct
        """;

    private static string FourthText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial record struct 
        """;

    private static string FifthText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial record struct L
        """;

    private static string FinalText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial record struct Length { }
        """;
}
