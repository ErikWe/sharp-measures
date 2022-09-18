namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class ExpressionDoesNotIncludeQuantity
{
    [Fact]
    public Task VerifyExpressionDoesNotIncludeQuantityDiagnosticsMessage() => AssertTwoElements().VerifyDiagnostics();

    private static GeneratorVerifier AssertExactlyExpressionDoesNotIncludeQuantityDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ExpressionDoesNotIncludeQuantityDiagnostics);
    private static IReadOnlyCollection<string> ExpressionDoesNotIncludeQuantityDiagnostics { get; } = new string[] { DiagnosticIDs.ExpressionDoesNotIncludeQuantity };

    private static string TwoElementsText => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfMisc))]
        public partial class Misc { }

        [SharpMeasuresScalar(typeof(UnitOfFrequency))]
        public partial class Frequency { }
        
        [DerivedQuantity("{0}", typeof(Frequency), typeof(Misc))]
        [SharpMeasuresScalar(typeof(UnitOfTime))]
        public partial class Time { }

        [SharpMeasuresUnit(typeof(Misc))]
        public partial class UnitOfMisc { }

        [SharpMeasuresUnit(typeof(Frequency))]
        public partial class UnitOfFrequency { }

        [SharpMeasuresUnit(typeof(Time))]
        public partial class UnitOfTime { }
        """;

    private static GeneratorVerifier AssertTwoElements()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(TwoElementsText, "\"{0}\"");

        return AssertExactlyExpressionDoesNotIncludeQuantityDiagnostics(TwoElementsText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators.Quantities;
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
