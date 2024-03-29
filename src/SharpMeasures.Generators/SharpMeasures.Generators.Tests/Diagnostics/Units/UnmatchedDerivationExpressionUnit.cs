﻿namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnmatchedDerivationExpressionUnit
{
    [Fact]
    public Task VerifyUnmatchedDerivationExpressionUnitDiagnosticsMessage() => Assert().VerifyDiagnostics();

    private static GeneratorVerifier AssertExactlyUnmatchedDerivationExpressionUnitDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnmatchedDerivationExpressionUnitDiagnostics);
    private static IReadOnlyCollection<string> UnmatchedDerivationExpressionUnitDiagnostics { get; } = new string[] { DiagnosticIDs.UnmatchedDerivationExpressionUnit };

    private static string Text => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfFrequency))]
        public partial class Frequency { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }

        [Unit(typeof(Frequency))]
        public partial class UnitOfFrequency { }

        [DerivableUnit("{0} * {1}", new[] { typeof(UnitOfFrequency) })]
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
        """;

    private static GeneratorVerifier Assert()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(Text, "\"{0} * {1}\"");

        return AssertExactlyUnmatchedDerivationExpressionUnitDiagnostics(Text).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfFrequency))]
        public partial class Frequency { }

        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }

        [Unit(typeof(Frequency))]
        public partial class UnitOfFrequency { }

        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
        """;
}
