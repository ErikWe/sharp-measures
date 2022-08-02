namespace SharpMeasures.Generators.Tests.Diagnostics.Units;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class UnrecognizedUnitName
{
    [Fact]
    public Task VerifyUnrecognizedUnitNameDiagnosticsMessage_Null()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [UnitAlias("Meter", "Meters", null)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyUnrecognizedUnitNameDiagnosticsMessage_Empty()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [UnitAlias("Meter", "Meters", "")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task VerifyUnrecognizedUnitNameDiagnosticsMessage_Missing()
    {
        string source = """
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [UnitAlias("Meter", "Meters", "Metre")]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        return AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void UnitAlias_ExactList(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [UnitAlias("Meter", "Meters", {{name}})]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void BiasedUnit_ExactList(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfTemperature))]
            public partial class TemperatureDifference { }

            [BiasedUnit("Celsius", "Celsius", {{name}}, -273.15)]
            [SharpMeasuresUnit(typeof(TemperatureDifference), BiasTerm = true)]
            public partial class UnitOfTemperature { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void DerivedUnit_ExactList(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresScalar(typeof(UnitOfTime))]
            public partial class Time { }

            [SharpMeasuresScalar(typeof(UnitOfSpeed))]
            public partial class Speed { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }

            [FixedUnit("Second", "Seconds")]
            [SharpMeasuresUnit(typeof(Time))]
            public partial class UnitOfTime { }

            [DerivableUnit("{0} / {1}", typeof(UnitOfLength), typeof(UnitOfTime))]
            [DerivedUnit("MetrePerSecond", "MetresPerSecond", new[] { {{name}}, "Second" })]
            [SharpMeasuresUnit(typeof(Speed))]
            public partial class UnitOfSpeed { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void PrefixedUnit_ExactList(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Units.Utility;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [PrefixedUnit("Kilometre", "Kilometres", {{name}}, MetricPrefixName.Kilo)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void ScaledUnit_ExactList(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [ScaledUnit("Kilometre", "Kilometres", {{name}}, 1000)]
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void ScalarDefaultUnit_ExactList(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength), DefaultUnitName = {{name}}, DefaultUnitSymbol = "m")]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedScalarDefaultUnit_ExactList(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SpecializedSharpMeasuresScalar(typeof(Length), DefaultUnitName = {{name}}, DefaultUnitSymbol = "m")]
            public partial class Distance { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void ScalarConstant_ExactList(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            [ScalarConstant("Planck", {{name}}, 1.616255E-35)]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedScalarConstant_ExactList(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;

            [ScalarConstant("Planck", {{name}}, 1.616255E-35)]
            [SpecializedSharpMeasuresScalar(typeof(Length))]
            public partial class Distance { }
            
            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }

            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void VectorDefaultUnit_ExactListAndVerify(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVector(typeof(UnitOfLength), DefaultUnitName = {{name}}, DefaultUnitSymbol = "m")]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedVectorDefaultUnit_ExactListAndVerify(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SpecializedSharpMeasuresVector(typeof(Position3), DefaultUnitName = {{name}}, DefaultUnitSymbol = "m")]
            public partial class Displacement3 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void VectorGroupDefaultUnit_ExactListAndVerify(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SharpMeasuresVectorGroup(typeof(UnitOfLength), DefaultUnitName = {{name}}, DefaultUnitSymbol = "m")]
            public static partial class Position { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedVectorGroupDefaultUnit_ExactListAndVerify(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [SpecializedSharpMeasuresVectorGroup(typeof(Position), DefaultUnitName = {{name}}, DefaultUnitSymbol = "m")]
            public static partial class Displacement { }

            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public static partial class Position { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void VectorConstant_ExactList(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("MetreOnes", {{name}}, 1, 1, 1)]
            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedVectorConstant_ExactList(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("MetreOnes", {{name}}, 1, 1, 1)]
            [SpecializedSharpMeasuresVector(typeof(Position3))]
            public partial class Displacement3 { }

            [SharpMeasuresVector(typeof(UnitOfLength))]
            public partial class Position3 { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void VectorGroupMemberConstant_ExactList(string name)
    {
        string source = $$"""
            using SharpMeasures.Generators.Scalars;
            using SharpMeasures.Generators.Units;
            using SharpMeasures.Generators.Vectors;

            [VectorConstant("MetreOnes", {{name}}, 1, 1, 1)]
            [SharpMeasuresVectorGroupMember(typeof(Position))]
            public partial class Position3 { }

            [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
            public static partial class Position { }

            [SharpMeasuresScalar(typeof(UnitOfLength))]
            public partial class Length { }
            
            [SharpMeasuresUnit(typeof(Length))]
            public partial class UnitOfLength { }
            """;

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void ScalarIncludeBase_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = ScalarUnitListText("IncludeBases", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void ScalarExcludeBase_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = ScalarUnitListText("ExcludeBases", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void ScalarIncludeUnit_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = ScalarUnitListText("IncludeUnits", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void ScalarExcludeUnit_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = ScalarUnitListText("ExcludeUnits", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedScalarIncludeBase_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = SpecializedScalarUnitListText("IncludeBases", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedScalarExcludeBase_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = SpecializedScalarUnitListText("ExcludeBases", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedScalarIncludeUnit_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = SpecializedScalarUnitListText("IncludeUnits", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedScalarExcludeUnit_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = SpecializedScalarUnitListText("ExcludeUnits", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void VectorIncludeUnit_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = VectorUnitListText("IncludeUnits", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void VectorExcludeUnit_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = VectorUnitListText("ExcludeUnits", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedVectorIncludeUnit_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = SpecializedVectorUnitListText("IncludeUnits", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedVectorExcludeUnit_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = SpecializedVectorUnitListText("ExcludeUnits", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void VectorGroupIncludeUnit_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = VectorGroupUnitListText("IncludeUnits", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void VectorGroupExcludeUnit_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = VectorGroupUnitListText("ExcludeUnits", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedVectorGroupIncludeUnit_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = SpecializedVectorGroupUnitListText("IncludeUnits", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    [Theory]
    [MemberData(nameof(UnrecognizedUnitNames))]
    public void SpecializedVectorGroupExcludeUnit_ExactList(string name)
    {
        if (name is "null")
        {
            name = "(string)null";
        }

        string source = SpecializedVectorGroupUnitListText("ExcludeUnits", name);

        AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(source);
    }

    private static IEnumerable<object[]> UnrecognizedUnitNames() => new object[][]
    {
        new[] { "null" },
        new[] { "\"\"" },
        new[] { "\"Metre\"" }
    };

    private static GeneratorVerifier AssertExactlyUnrecognizedUnitNameDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(UnrecognizedUnitNameDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> UnrecognizedUnitNameDiagnostics { get; } = new string[] { DiagnosticIDs.UnrecognizedUnitName };

    private static string ScalarUnitListText(string attribute, string unitInstanceName) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}({{unitInstanceName}})]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarUnitListText(string attribute, string unitInstanceName) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [{{attribute}}({{unitInstanceName}})]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorUnitListText(string attribute, string unitInstanceName) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}({{unitInstanceName}})]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorUnitListText(string attribute, string unitInstanceName) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}({{unitInstanceName}})]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupUnitListText(string attribute, string unitInstanceName) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}({{unitInstanceName}})]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupUnitListText(string attribute, string unitInstanceName) => $$"""
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [{{attribute}}({{unitInstanceName}})]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
