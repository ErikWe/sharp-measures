namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnitWithBiasTermCannotBeDerived
{
    [Fact]
    public Task VerifyUnitWithBiasTermCannotBeDerivedDiagnosticsMessage() => Assert().VerifyDiagnostics();

    private static GeneratorVerifier AssertExactlyUnitWithBiasTermCannotBeDerivedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnitWithBiasTermCannotBeDerivedDiagnostics);
    private static IReadOnlyCollection<string> UnitWithBiasTermCannotBeDerivedDiagnostics { get; } = new string[] { DiagnosticIDs.UnitWithBiasTermCannotBeDerived };

    private static string Text => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
        
        [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;

    private static GeneratorVerifier Assert()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(Text, target: "DerivableUnit");

        return AssertExactlyUnitWithBiasTermCannotBeDerivedDiagnostics(Text).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical);
    }

    private static GeneratorVerifier Identical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText);

    private static string IdenticalText => """
        using SharpMeasures.Generators;
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [ScalarQuantity(typeof(UnitOfTime))]
        public partial class Time { }
        
        [ScalarQuantity(typeof(UnitOfTemperature))]
        public partial class TemperatureDifference { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        
        [Unit(typeof(Time))]
        public partial class UnitOfTime { }
        
        [Unit(typeof(TemperatureDifference), BiasTerm = true)]
        public partial class UnitOfTemperature { }
        """;
}
