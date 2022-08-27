namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class ContradictoryAttributes
{
    [Fact]
    public Task IncludeAndExcludeUnit_Scalar() => AssertIncludeAndExcludeUnit_Scalar().VerifyDiagnostics();

    [Fact]
    public void IncludeAndExcludeUnit_SpecializedScalar() => AssertIncludeAndExcludeUnit_SpecializedScalar();

    [Fact]
    public void IncludeAndExcludeUnit_Vector() => AssertIncludeAndExcludeUnit_Vector();

    [Fact]
    public void IncludeAndExcludeUnit_SpecializedVector() => AssertIncludeAndExcludeUnit_SpecializedVector();

    [Fact]
    public void IncludeAndExcludeUnit_VectorGroup() => AssertIncludeAndExcludeUnit_VectorGroup();

    [Fact]
    public void IncludeAndExcludeUnit_SpecializedVectorGroup() => AssertIncludeAndExcludeUnit_SpecializedVectorGroup();

    [Fact]
    public void IncludeAndExcludeBase_Scalar() => AssertIncludeAndExcludeBase_Scalar();

    [Fact]
    public void IncludeAndExcludeBase_SpecializedScalar() => AssertIncludeAndExcludeBase_SpecializedScalar();

    private static GeneratorVerifier AssertExactlyContradictoryAttributesDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(ContradictoryAttributesDiagnostics);
    private static IReadOnlyCollection<string> ContradictoryAttributesDiagnostics { get; } = new string[] { DiagnosticIDs.ContradictoryAttributes };

    private static string IncludeAndExcludeUnitText_Scalar => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_Scalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_Scalar, target: "Length", prefix: "public partial class ");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_Scalar).AssertDiagnosticsLocation(expectedLocation, IncludeAndExcludeUnitText_Scalar);
    }

    private static string IncludeAndExcludeUnitText_SpecializedScalar => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_SpecializedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_SpecializedScalar, target: "Distance", prefix: "public partial class ");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_SpecializedScalar).AssertDiagnosticsLocation(expectedLocation, IncludeAndExcludeUnitText_SpecializedScalar);
    }

    private static string IncludeAndExcludeUnitText_Vector => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_Vector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_Vector, target: "Position3", prefix: "public partial class ");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_Vector).AssertDiagnosticsLocation(expectedLocation, IncludeAndExcludeUnitText_Vector);
    }

    private static string IncludeAndExcludeUnitText_SpecializedVector => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_SpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_SpecializedVector, target: "Displacement3", prefix: "public partial class ");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_SpecializedVector).AssertDiagnosticsLocation(expectedLocation, IncludeAndExcludeUnitText_SpecializedVector);
    }

    private static string IncludeAndExcludeUnitText_VectorGroup => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_VectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_VectorGroup, target: "Position", prefix: "public static partial class ");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_VectorGroup).AssertDiagnosticsLocation(expectedLocation, IncludeAndExcludeUnitText_VectorGroup);
    }

    private static string IncludeAndExcludeUnitText_SpecializedVectorGroup => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_SpecializedVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_SpecializedVectorGroup, target: "Displacement", prefix: "public static partial class ");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_SpecializedVectorGroup).AssertDiagnosticsLocation(expectedLocation, IncludeAndExcludeUnitText_SpecializedVectorGroup);
    }

    private static string IncludeAndExcludeBaseText_Scalar => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [IncludeBases("Metre")]
        [ExcludeBases("Kilometre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeBase_Scalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeBaseText_Scalar, target: "Length", prefix: "public partial class ");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeBaseText_Scalar).AssertDiagnosticsLocation(expectedLocation, IncludeAndExcludeBaseText_Scalar);
    }

    private static string IncludeAndExcludeBaseText_SpecializedScalar => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Units.Utility;

        [IncludeBases("Metre")]
        [ExcludeBases("Kilometre")]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnit("Metre", "Metres")]
        [PrefixedUnit("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeBase_SpecializedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeBaseText_SpecializedScalar, target: "Distance", prefix: "public partial class ");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeBaseText_SpecializedScalar).AssertDiagnosticsLocation(expectedLocation, IncludeAndExcludeBaseText_SpecializedScalar);
    }
}
