﻿namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnitNotDerivable
{
    [Fact]
    public Task WithoutID() => AssertWithoutID().VerifyDiagnostics();

    [Fact]
    public void WithID() => AssertWithID();

    private static GeneratorVerifier AssertExactlyUnitNotDerivableDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source, GeneratorVerifierSettings.TestCodeAssertions).AssertExactlyListedDiagnosticsIDsReported(UnitNotDerivableDiagnostics);
    private static IReadOnlyCollection<string> UnitNotDerivableDiagnostics { get; } = new string[] { DiagnosticIDs.UnitNotDerivable };

    private static string WithoutIDText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [DerivedUnitInstance("Metre", "Metres", new[] { "MetrePerSecond", "Second" })]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertWithoutID()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(WithoutIDText, target: "DerivedUnitInstance");

        return AssertExactlyUnitNotDerivableDiagnostics(WithoutIDText).AssertDiagnosticsLocation(expectedLocation);
    }

    private static string WithIDText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [DerivedUnitInstance("Metre", "Metres", "1", new[] { "MetrePerSecond", "Second" })]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertWithID()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(WithIDText, target: "DerivedUnitInstance");

        return AssertExactlyUnitNotDerivableDiagnostics(WithIDText).AssertDiagnosticsLocation(expectedLocation);
    }
}
