namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class DuplicateConstantMultiplesName
{
    [Fact]
    public Task VerifyDuplicateConstantMultiplesNameDiagnosticsMessage()
    {
        string source = ScalarText(", Multiples = \"MultiplesOfPlancks\"", ", Multiples = \"MultiplesOfPlancks\"");

        return AssertExactlyDuplicateConstantMultiplesNameDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(DuplicateConstantMultiplesNames))]
    public void Scalar_ExactList(string firstArgument, string secondArgument)
    {
        string source = ScalarText(firstArgument, secondArgument);

        AssertExactlyDuplicateConstantMultiplesNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(DuplicateConstantMultiplesNames))]
    public void SpecializedScalar_ExactList(string firstArgument, string secondArgument)
    {
        string source = SpecializedScalarText(firstArgument, secondArgument);

        AssertExactlyDuplicateConstantMultiplesNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(DuplicateConstantMultiplesNames))]
    public void Vector_ExactList(string firstArgument, string secondArgument)
    {
        string source = VectorText(firstArgument, secondArgument);

        AssertExactlyDuplicateConstantMultiplesNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(DuplicateConstantMultiplesNames))]
    public void SpecializedVector_ExactList(string firstArgument, string secondArgument)
    {
        string source = SpecializedVectorText(firstArgument, secondArgument);

        AssertExactlyDuplicateConstantMultiplesNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(DuplicateConstantMultiplesNames))]
    public void VectorGroupMember_ExactList(string firstArgument, string secondArgument)
    {
        string source = VectorGroupMemberText(firstArgument, secondArgument);

        AssertExactlyDuplicateConstantMultiplesNameDiagnosticsWithValidLocation(source);
    }

    private static IEnumerable<object[]> DuplicateConstantMultiplesNames() => new object[][]
    {
        new[] { ", Multiples = \"MultiplesOfPlancks\"", ", Multiples = \"MultiplesOfPlancks\"" },
        new[] { string.Empty, ", Multiples = \"MultiplesOfPlancks\"" }
    };

    private static GeneratorVerifier AssertExactlyDuplicateConstantMultiplesNameDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(DuplicateConstantMultiplesNameDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> DuplicateConstantMultiplesNameDiagnostics { get; } = new string[] { DiagnosticIDs.DuplicateConstantMultiplesName };

    private static string ScalarText(string firstArgument, string secondArgument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35{{firstArgument}})]
        [ScalarConstant("Planck2", "Metre", 1.616255E-35{{secondArgument}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarText(string firstArgument, string secondArgument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [ScalarConstant("Planck", "Metre", 1.616255E-35{{firstArgument}})]
        [ScalarConstant("Planck2", "Metre", 1.616255E-35{{secondArgument}})]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorText(string firstArgument, string secondArgument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ScalarConstant("Planck", "Metre", 1.616255E-35{{firstArgument}})]
        [ScalarConstant("Planck2", "Metre", 1.616255E-35{{secondArgument}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorText(string firstArgument, string secondArgument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ScalarConstant("Planck", "Metre", 1.616255E-35{{firstArgument}})]
        [ScalarConstant("Planck2", "Metre", 1.616255E-35{{secondArgument}})]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberText(string firstArgument, string secondArgument) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ScalarConstant("Planck", "Metre", 1.616255E-35{{firstArgument}})]
        [ScalarConstant("Planck2", "Metre", 1.616255E-35{{secondArgument}})]
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
