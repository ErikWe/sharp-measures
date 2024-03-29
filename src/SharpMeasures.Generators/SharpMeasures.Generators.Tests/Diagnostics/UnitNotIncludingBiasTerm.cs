﻿namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnitNotIncludingBiasTerm
{
    [Fact]
    public Task BiasedScalar() => AssertBiasedScalar().VerifyDiagnostics();

    private static GeneratorVerifier AssertExactlyUnitNotIncludingBiasTermDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnitNotIncludingBiasTermDiagnostics);
    private static IReadOnlyCollection<string> UnitNotIncludingBiasTermDiagnostics { get; } = new string[] { DiagnosticIDs.UnitNotIncludingBiasTerm };

    private static string BiasedScalarText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength), UseUnitBias = true)]
        public partial class Temperature { }
            
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertBiasedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(BiasedScalarText, target: "true", prefix: "UseUnitBias = ");

        return AssertExactlyUnitNotIncludingBiasTermDiagnostics(BiasedScalarText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(BiasedScalarIdentical);
    }

    private static GeneratorVerifier BiasedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(BiasedScalarIdenticalText);

    private static string BiasedScalarIdenticalText => """
        using SharpMeasures.Generators;
            
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
