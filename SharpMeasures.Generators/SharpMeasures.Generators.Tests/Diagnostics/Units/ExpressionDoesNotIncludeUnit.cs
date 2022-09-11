namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class ExpressionDoesNotIncludeUnit
{
    [Fact]
    public Task VerifyExpressionDoesNotIncludeUnitDiagnosticsMessage() => AssertOneElement().VerifyDiagnostics();

    [Fact]
    public void TwoElements() => AssertTwoElements();

    private static GeneratorVerifier AssertExactlyExpressionDoesNotIncludeUnitDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ExpressionDoesNotIncludeUnitDiagnostics);
    private static IReadOnlyCollection<string> ExpressionDoesNotIncludeUnitDiagnostics { get; } = new string[] { DiagnosticIDs.ExpressionDoesNotIncludeUnit };

    private static string OneElementText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfMisc))]
        public partial class Misc { }
        
        [SharpMeasuresScalar(typeof(UnitOfFrequency))]
        public partial class Frequency { }
        
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
        
        [SharpMeasuresUnit(typeof(Misc))]
        public partial class UnitOfMisc { }

        [SharpMeasuresUnit(typeof(Frequency))]
        public partial class UnitOfFrequency { }

        [DerivableUnit("2", new[] { typeof(UnitOfFrequency) })]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }
        """;

    private static GeneratorVerifier AssertOneElement()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(OneElementText, "\"2\"");

        return AssertExactlyExpressionDoesNotIncludeUnitDiagnostics(OneElementText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static string TwoElementsText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfMisc))]
        public partial class Misc { }

        [SharpMeasuresScalar(typeof(UnitOfFrequency))]
        public partial class Frequency { }

        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresUnit(typeof(Misc))]
        public partial class UnitOfMisc { }

        [SharpMeasuresUnit(typeof(Frequency))]
        public partial class UnitOfFrequency { }

        [DerivableUnit("2 * {0}", new[] { typeof(UnitOfFrequency), typeof(UnitOfMisc) })]
        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }
        """;

    private static GeneratorVerifier AssertTwoElements()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(TwoElementsText, "\"2 * {0}\"");

        return AssertExactlyExpressionDoesNotIncludeUnitDiagnostics(TwoElementsText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfMisc))]
        public partial class Misc { }
        
        [SharpMeasuresScalar(typeof(UnitOfFrequency))]
        public partial class Frequency { }
        
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }
        
        [SharpMeasuresUnit(typeof(Misc))]
        public partial class UnitOfMisc { }

        [SharpMeasuresUnit(typeof(Frequency))]
        public partial class UnitOfFrequency { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }
        """;
}
