namespace SharpMeasures.Generators.Tests.Incremental.Units;

using SharpMeasures.Generators.DriverUtility;

using Xunit;

public class DefiningUnit
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

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial record 
        """;

    private static string SecondText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [SharpMeasuresUnit(typeof(Time))]
        public partial record struc
        """;

    private static string ThirdText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [SharpMeasuresUnit(typeof(Time))]
        public partial record struct
        """;

    private static string FourthText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [SharpMeasuresUnit(typeof(Time))]
        public partial record struct 
        """;

    private static string FifthText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [SharpMeasuresUnit(typeof(Time))]
        public partial record struct U
        """;

    private static string FinalText { get; } = """
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Scalars;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [SharpMeasuresUnit(typeof(Time))]
        public partial record struct UnitOfTime { }
        """;
}
