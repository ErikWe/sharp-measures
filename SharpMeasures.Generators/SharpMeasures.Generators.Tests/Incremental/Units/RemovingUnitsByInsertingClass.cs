namespace SharpMeasures.Generators.Tests.Incremental.Units;

using SharpMeasures.Generators.DriverUtility;

using Xunit;

public class Test
{
    [Fact]
    public async void Assert()
    {
        var driver = await IncrementalDriver.Build(InitialText, ProjectPath.Path + @"\Documentation").ConfigureAwait(false);
        
        for (int i = 0; i < 50; i++)
        {
            driver.ApplyChange(SecondText);
            driver.ApplyChange(ThirdText);
            driver.ApplyChange(FourthText);
            driver.ApplyChange(FifthText);
            driver.ApplyChange(SixthText);
            driver.ApplyChange(InitialText);
        }

        for (int i = 0; i < 50; i++)
        {
            driver.ApplyChange(SecondText);
            driver.ApplyChange(ThirdText);
            driver.ApplyChange(FourthText);
            driver.ApplyChange(FifthText);
            driver.ApplyChange(SixthText);
            driver.ApplyChange(FifthText);
            driver.ApplyChange(FourthText);
            driver.ApplyChange(ThirdText);
            driver.ApplyChange(SecondText);
            driver.ApplyChange(InitialText);
        }
    }

    private static string InitialText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SecondText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        c
        public partial class UnitOfLength { }
        """;

    private static string ThirdText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        clas
        public partial class UnitOfLength { }
        """;

    private static string FourthText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        class
        public partial class UnitOfLength { }
        """;

    private static string FifthText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        class e
        public partial class UnitOfLength { }
        """;

    private static string SixthText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        class e { }
        public partial class UnitOfLength { }
        """;
}
