namespace SharpMeasures.Generators.Tests.Incremental.Units.Derivable;

using SharpMeasures.Generators.DriverUtility;
using SharpMeasures.Generators.Tests.Verify;

using Xunit;

public class DefiningDerivableUnit
{
    [Fact]
    public async void RunTest()
    {
        var driver = await IncrementalDriver.Build(InitialText, ProjectPath.Path + @"\Documentation").ConfigureAwait(false);

        driver.ApplyChange(SecondText);
        driver.ApplyChange(ThirdText);
        driver.ApplyChange(FourthText);

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

        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static string SecondText { get; } = """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
        
        [DerivableUnit("{0} / {1}", typeof(UnitOfLength)]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static string ThirdText { get; } = """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
        
        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTim), Permutations = true]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static string FourthText { get; } = """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
        
        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTim)]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;

    private static string FinalText { get; } = """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [ScalarQuantity(typeof(UnitOfSpeed))]
        public partial class Speed { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
        
        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [Unit(typeof(Speed))]
        public partial class UnitOfSpeed { }
        """;
}
