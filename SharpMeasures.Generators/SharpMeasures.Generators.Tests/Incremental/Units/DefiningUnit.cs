namespace SharpMeasures.Generators.Tests.Incremental.Units;

using SharpMeasures.Generators.DriverUtility;
using SharpMeasures.Generators.Tests.Verify;

using Xunit;

public class DefiningUnit
{
    [Fact]
    public async void RunTest()
    {
        var driver = await IncrementalDriver.Build(InitialText, ProjectPath.Path + @"\Documentation").ConfigureAwait(false);

        driver.ApplyChange(SecondText);
        driver.ApplyChange(ThirdText);
        driver.ApplyChange(FourthText);
        driver.ApplyChange(FifthText);

        var compilation = await driver.ApplyChangeAndRetrieveCompilation(FinalText).ConfigureAwait(false);

        Assert.NotNull(compilation);

        GeneratorVerifier.Construct(FinalText, driver.Driver, compilation!);
    }

    private static string InitialText { get; } = """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [Unit(typeof(Time))]
        public partial record 
        """;

    private static string SecondText { get; } = """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [Unit(typeof(Time))]
        public partial record struc
        """;

    private static string ThirdText { get; } = """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [Unit(typeof(Time))]
        public partial record struct
        """;

    private static string FourthText { get; } = """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [Unit(typeof(Time))]
        public partial record struct 
        """;

    private static string FifthText { get; } = """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [Unit(typeof(Time))]
        public partial record struct U
        """;

    private static string FinalText { get; } = """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [Unit(typeof(Time))]
        public partial record struct UnitOfTime { }
        """;
}
