namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class ConstantMultiplesDisabledButNameSpecified
{
    [Fact]
    public Task Scalar() => AssertScalar().VerifyDiagnostics();

    [Fact]
    public void SpecializedScalar() => AssertSpecializedScalar();

    [Fact]
    public void Vector() => AssertVector();

    [Fact]
    public void SpecializedVector() => AssertSpecializedVector();

    [Fact]
    public void VectorGroupMember() => AssertVectorGroupMember();

    private static GeneratorVerifier AssertExactlyConstantMultiplesDisabledButNameSpecifiedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ConstantMultiplesDisabledButNameSpecifiedDiagnostics);
    private static IReadOnlyCollection<string> ConstantMultiplesDisabledButNameSpecifiedDiagnostics { get; } = new string[] { DiagnosticIDs.ConstantMultiplesDisabledButNameSpecified };

    private static string ScalarText => """
        using SharpMeasures.Generators;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, GenerateMultiplesProperty = false, Multiples = "Plancks")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(ScalarText, target: "\"Plancks\"", prefix: "Multiples = ");

        return AssertExactlyConstantMultiplesDisabledButNameSpecifiedDiagnostics(ScalarText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText => """
        using SharpMeasures.Generators;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, GenerateMultiplesProperty = false, Multiples = "Plancks")]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance{ }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedScalarText, target: "\"Plancks\"", prefix: "Multiples = ");

        return AssertExactlyConstantMultiplesDisabledButNameSpecifiedDiagnostics(SpecializedScalarText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText => """
        using SharpMeasures.Generators;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, GenerateMultiplesProperty = false, Multiples = "MultiplesOfMetreOnes")]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorText, target: "\"MultiplesOfMetreOnes\"", prefix: "Multiples = ");

        return AssertExactlyConstantMultiplesDisabledButNameSpecifiedDiagnostics(VectorText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText => """
        using SharpMeasures.Generators;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, GenerateMultiplesProperty = false, Multiples = "MultiplesOfMetreOnes")]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorText, target: "\"MultiplesOfMetreOnes\"", prefix: "Multiples = ");

        return AssertExactlyConstantMultiplesDisabledButNameSpecifiedDiagnostics(SpecializedVectorText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupMemberText => """
        using SharpMeasures.Generators;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, GenerateMultiplesProperty = false, Multiples = "MultiplesOfMetreOnes")]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorGroupMemberText, target: "\"MultiplesOfMetreOnes\"", prefix: "Multiples = ");

        return AssertExactlyConstantMultiplesDisabledButNameSpecifiedDiagnostics(VectorGroupMemberText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, GenerateMultiplesProperty = false)]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, GenerateMultiplesProperty = false)]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance{ }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, GenerateMultiplesProperty = false)]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText => """
        using SharpMeasures.Generators;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, GenerateMultiplesProperty = false)]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, GenerateMultiplesProperty = false)]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [FixedUnitInstance("Metre", "Metres")]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
