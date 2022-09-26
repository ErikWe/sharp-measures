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
    public void IncludeAndExcludeUnit_VectorGroupMember() => AssertIncludeAndExcludeUnit_VectorGroupMember();

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

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_Scalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_Scalar, target: "SharpMeasuresScalar");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_Scalar).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeUnitIdentical_Scalar);
    }

    private static string IncludeAndExcludeUnitText_SpecializedScalar => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_SpecializedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_SpecializedScalar, target: "SpecializedSharpMeasuresScalar");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_SpecializedScalar).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeUnitIdentical_SpecializedScalar);
    }

    private static string IncludeAndExcludeUnitText_Vector => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_Vector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_Vector, target: "SharpMeasuresVector");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_Vector).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeUnitIdentical_Vector);
    }

    private static string IncludeAndExcludeUnitText_SpecializedVector => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_SpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_SpecializedVector, target: "SpecializedSharpMeasuresVector");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_SpecializedVector).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeUnitIdentical_SpecializedVector);
    }

    private static string IncludeAndExcludeUnitText_VectorGroup => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_VectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_VectorGroup, target: "SharpMeasuresVectorGroup");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_VectorGroup).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeUnitIdentical_VectorGroup);
    }

    private static string IncludeAndExcludeUnitText_SpecializedVectorGroup => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_SpecializedVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_SpecializedVectorGroup, target: "SpecializedSharpMeasuresVectorGroup");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_SpecializedVectorGroup).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeUnitIdentical_SpecializedVectorGroup);
    }

    private static string IncludeAndExcludeUnitText_VectorGroupMember => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_VectorGroupMember()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_VectorGroupMember, target: "SharpMeasuresVectorGroupMember");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_VectorGroupMember).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeUnitIdentical_VectorGroupMember);
    }

    private static string IncludeAndExcludeBaseText_Scalar => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [IncludeUnitBases("Metre")]
        [ExcludeUnitBases("Kilometre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeBase_Scalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeBaseText_Scalar, target: "SharpMeasuresScalar");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeBaseText_Scalar).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeBaseIdentical_Scalar);
    }

    private static string IncludeAndExcludeBaseText_SpecializedScalar => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [IncludeUnitBases("Metre")]
        [ExcludeUnitBases("Kilometre")]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeBase_SpecializedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeBaseText_SpecializedScalar, target: "SpecializedSharpMeasuresScalar");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeBaseText_SpecializedScalar).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeBaseIdentical_SpecializedScalar);
    }

    private static GeneratorVerifier IncludeAndExcludeUnitIdentical_Scalar => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IncludeAndExcludeUnitIdenticalText_Scalar);
    private static GeneratorVerifier IncludeAndExcludeUnitIdentical_SpecializedScalar => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IncludeAndExcludeUnitIdenticalText_SpecializedScalar);
    private static GeneratorVerifier IncludeAndExcludeUnitIdentical_Vector => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IncludeAndExcludeUnitIdenticalText_Vector);
    private static GeneratorVerifier IncludeAndExcludeUnitIdentical_SpecializedVector => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IncludeAndExcludeUnitIdenticalText_SpecializedVector);
    private static GeneratorVerifier IncludeAndExcludeUnitIdentical_VectorGroup => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IncludeAndExcludeUnitIdenticalText_VectorGroup);
    private static GeneratorVerifier IncludeAndExcludeUnitIdentical_SpecializedVectorGroup => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IncludeAndExcludeUnitIdenticalText_SpecializedVectorGroup);
    private static GeneratorVerifier IncludeAndExcludeUnitIdentical_VectorGroupMember => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IncludeAndExcludeUnitIdenticalText_VectorGroupMember);
    private static GeneratorVerifier IncludeAndExcludeBaseIdentical_Scalar => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IncludeAndExcludeBaseIdenticalText_Scalar);
    private static GeneratorVerifier IncludeAndExcludeBaseIdentical_SpecializedScalar => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IncludeAndExcludeBaseIdenticalText_SpecializedScalar);

    private static string IncludeAndExcludeUnitIdenticalText_Scalar => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [IncludeUnits("Metre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeUnitIdenticalText_SpecializedScalar => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [IncludeUnits("Metre")]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeUnitIdenticalText_Vector => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeUnitIdenticalText_SpecializedVector => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Metre")]
        [SpecializedSharpMeasuresVector(typeof(Position3))]
        public partial class Displacement3 { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeUnitIdenticalText_VectorGroup => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Metre")]
        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeUnitIdenticalText_SpecializedVectorGroup => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Metre")]
        [SpecializedSharpMeasuresVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeUnitIdenticalText_VectorGroupMember => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [IncludeUnits("Metre")]
        [SharpMeasuresVectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [SharpMeasuresVectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeBaseIdenticalText_Scalar => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [IncludeUnitBases("Metre")]
        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeBaseIdenticalText_SpecializedScalar => """
        using SharpMeasures.Generators.Quantities;
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;

        [IncludeUnitBases("Metre")]
        [SpecializedSharpMeasuresScalar(typeof(Length))]
        public partial class Distance { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
