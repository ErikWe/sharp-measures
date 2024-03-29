﻿namespace SharpMeasures.Generators.Tests.Incremental.Units.Derivable;

using SharpMeasures.Generators.DriverUtility;
using SharpMeasures.Generators.Tests.Verify;

using Xunit;

public class MalformedExpression
{
    [Fact]
    public async void RunTest()
    {
        var driver = await IncrementalDriver.Build(InitialText).ConfigureAwait(false);

        var compilation = await driver.ApplyChangeAndRetrieveCompilation(FinalText).ConfigureAwait(false);

        Assert.NotNull(compilation);

        GeneratorVerifier.Construct(FinalText, driver.Driver, compilation!);
    }

    private static string InitialText { get; } = """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }

        [ScalarQuantity(typeof(UnitOfFrequency))]
        public partial class Frequency { }
        
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }

        [DerivableUnit("1 / {0} * {}", typeof(UnitOfTime))]
        [Unit(typeof(Frequency))]
        public partial class UnitOfFrequency { }
        """;

    private static string FinalText { get; } = """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [ScalarQuantity(typeof(UnitOfFrequency))]
        public partial class Frequency { }
        
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
        
        [DerivableUnit("1 / {0}", typeof(UnitOfTime))]
        [Unit(typeof(Frequency))]
        public partial class UnitOfFrequency { }
        """;
}
