namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

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

    private static GeneratorVerifier AssertExactlyUnitNotDerivableDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnitNotDerivableDiagnostics);
    private static IReadOnlyCollection<string> UnitNotDerivableDiagnostics { get; } = new string[] { DiagnosticIDs.UnitNotDerivable };

    private static string WithoutIDText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [DerivedUnit("Metre", "Metres", new[] { "MetrePerSecond", "Second" })]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertWithoutID()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(WithoutIDText, target: "DerivedUnit");

        return AssertExactlyUnitNotDerivableDiagnostics(WithoutIDText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static string WithIDText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [DerivedUnit("Metre", "Metres", "1", new[] { "MetrePerSecond", "Second" })]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertWithID()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(WithIDText, target: "DerivedUnit");

        return AssertExactlyUnitNotDerivableDiagnostics(WithIDText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
        
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
