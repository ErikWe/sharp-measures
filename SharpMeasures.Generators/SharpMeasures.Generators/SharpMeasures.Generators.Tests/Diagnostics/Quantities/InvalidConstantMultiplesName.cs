namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidConstantMultiplesName
{
    [Fact]
    public Task VerifyInvalidConstantMultiplesNameDiagnosticsMessage_Null()
    {
        string source = ScalarText("null");

        return AssertExactlyInvalidConstantMultiplesNameDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyInvalidConstantMultiplesNameDiagnosticsMessage_Emptyl()
    {
        string source = ScalarText("\"\"");

        return AssertExactlyInvalidConstantMultiplesNameDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void Scalar_ExactList(string constantMultiplesName)
    {
        string source = ScalarText(constantMultiplesName);

        AssertExactlyInvalidConstantMultiplesNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void SpecializedScalar_ExactList(string constantMultiplesName)
    {
        string source = SpecializedScalarText(constantMultiplesName);

        AssertExactlyInvalidConstantMultiplesNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void Vector_ExactList(string constantMultiplesName)
    {
        string source = VectorText(constantMultiplesName);

        AssertExactlyInvalidConstantMultiplesNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void SpecializedVector_ExactList(string constantMultiplesName)
    {
        string source = SpecializedVectorText(constantMultiplesName);

        AssertExactlyInvalidConstantMultiplesNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(InvalidConstantMultiplesNames))]
    public void VectorGroupMember_ExactList(string constantMultiplesName)
    {
        string source = VectorGroupMemberText(constantMultiplesName);

        AssertExactlyInvalidConstantMultiplesNameDiagnosticsWithValidLocation(source);
    }

    private static IEnumerable<object[]> InvalidConstantMultiplesNames() => new object[][]
    {
        new[] { "null" },
        new[] { "\"\"" }
    };

    private static GeneratorVerifier AssertExactlyInvalidConstantMultiplesNameDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidConstantMultiplesNameDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> InvalidConstantMultiplesNameDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidConstantMultiplesName };

    private static string ScalarText(string multiples) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = {{multiples}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarText(string multiples) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35, Multiples = {{multiples}})]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorText(string multiples) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = {{multiples}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorText(string multiples) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = {{multiples}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberText(string multiples) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [VectorConstant("MetreOnes", "Metre", 1, 1, 1, Multiples = {{multiples}})]
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
}
