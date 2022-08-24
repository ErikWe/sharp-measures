namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidConstantMultiplesName
{
    [Fact]
    public Task VerifyInvalidConstantMultiplesNameDiagnosticsMessage_Null() => AssertScalar(NullName).VerifyDiagnostics();

    [Fact]
    public Task VerifyInvalidConstantMultiplesNameDiagnosticsMessage_Empty() => AssertScalar(EmptyName).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void Scalar(SourceSubtext constantMultiplesName) => AssertScalar(constantMultiplesName);

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void SpecializedScalar(SourceSubtext constantMultiplesName) => AssertSpecializedScalar(constantMultiplesName);

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void Vector(SourceSubtext constantMultiplesName) => AssertVector(constantMultiplesName);

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void SpecializedVector(SourceSubtext constantMultiplesName) => AssertSpecializedVector(constantMultiplesName);

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void VectorGroupMember(SourceSubtext constantMultiplesName) => AssertVectorGroupMember(constantMultiplesName);

    public static IEnumerable<object[]> InvalidConstantMultiplesNames() => new object[][]
    {
        new object[] { NullName },
        new object[] { EmptyName }
    };

    private static SourceSubtext NullName { get; } = SourceSubtext.Covered("null");
    private static SourceSubtext EmptyName { get; } = SourceSubtext.Covered("\"\"");

    private static GeneratorVerifier AssertExactlyInvalidConstantMultiplesNameDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidConstantMultiplesNameDiagnostics);
    private static IReadOnlyCollection<string> InvalidConstantMultiplesNameDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidConstantMultiplesName };

    private static string ScalarText(SourceSubtext constantMultiplesName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = {{constantMultiplesName}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(SourceSubtext constantMultiplesName)
    {
        var source = ScalarText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedScalarText(SourceSubtext constantMultiplesName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = {{constantMultiplesName}})]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(SourceSubtext constantMultiplesName)
    {
        var source = SpecializedScalarText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorText(SourceSubtext constantMultiplesName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = {{constantMultiplesName}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(SourceSubtext constantMultiplesName)
    {
        var source = VectorText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string SpecializedVectorText(SourceSubtext constantMultiplesName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = {{constantMultiplesName}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(SourceSubtext constantMultiplesName)
    {
        var source = SpecializedVectorText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }

    private static string VectorGroupMemberText(SourceSubtext constantMultiplesName) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = {{constantMultiplesName}})]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(SourceSubtext constantMultiplesName)
    {
        var source = VectorGroupMemberText(constantMultiplesName);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, constantMultiplesName.Context.With(outerPrefix: "Multiples = "));

        return AssertExactlyInvalidConstantMultiplesNameDiagnostics(source).AssertDiagnosticsLocation(expectedLocation, source);
    }
}
