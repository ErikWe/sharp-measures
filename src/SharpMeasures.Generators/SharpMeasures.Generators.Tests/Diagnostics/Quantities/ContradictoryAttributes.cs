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
        using SharpMeasures.Generators;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_Scalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_Scalar, target: "ScalarQuantity");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_Scalar).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeUnitIdentical_Scalar);
    }

    private static string IncludeAndExcludeUnitText_SpecializedScalar => """
        using SharpMeasures.Generators;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_SpecializedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_SpecializedScalar, target: "SpecializedScalarQuantity");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_SpecializedScalar).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeUnitIdentical_SpecializedScalar);
    }

    private static string IncludeAndExcludeUnitText_Vector => """
        using SharpMeasures.Generators;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_Vector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_Vector, target: "VectorQuantity");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_Vector).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeUnitIdentical_Vector);
    }

    private static string IncludeAndExcludeUnitText_SpecializedVector => """
        using SharpMeasures.Generators;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_SpecializedVector()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_SpecializedVector, target: "SpecializedVectorQuantity");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_SpecializedVector).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeUnitIdentical_SpecializedVector);
    }

    private static string IncludeAndExcludeUnitText_VectorGroup => """
        using SharpMeasures.Generators;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_VectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_VectorGroup, target: "VectorGroup");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_VectorGroup).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeUnitIdentical_VectorGroup);
    }

    private static string IncludeAndExcludeUnitText_SpecializedVectorGroup => """
        using SharpMeasures.Generators;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_SpecializedVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_SpecializedVectorGroup, target: "SpecializedVectorGroup");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_SpecializedVectorGroup).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeUnitIdentical_SpecializedVectorGroup);
    }

    private static string IncludeAndExcludeUnitText_VectorGroupMember => """
        using SharpMeasures.Generators;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeUnit_VectorGroupMember()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeUnitText_VectorGroupMember, target: "VectorGroupMember");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeUnitText_VectorGroupMember).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeUnitIdentical_VectorGroupMember);
    }

    private static string IncludeAndExcludeBaseText_Scalar => """
        using SharpMeasures.Generators;

        [IncludeUnitBases("Metre")]
        [ExcludeUnitBases("Kilometre")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeBase_Scalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeBaseText_Scalar, target: "ScalarQuantity");

        return AssertExactlyContradictoryAttributesDiagnostics(IncludeAndExcludeBaseText_Scalar).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IncludeAndExcludeBaseIdentical_Scalar);
    }

    private static string IncludeAndExcludeBaseText_SpecializedScalar => """
        using SharpMeasures.Generators;

        [IncludeUnitBases("Metre")]
        [ExcludeUnitBases("Kilometre")]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertIncludeAndExcludeBase_SpecializedScalar()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(IncludeAndExcludeBaseText_SpecializedScalar, target: "SpecializedScalarQuantity");

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
        using SharpMeasures.Generators;

        [IncludeUnits("Metre")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeUnitIdenticalText_SpecializedScalar => """
        using SharpMeasures.Generators;

        [IncludeUnits("Metre")]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeUnitIdenticalText_Vector => """
        using SharpMeasures.Generators;

        [IncludeUnits("Metre")]
        [ExcludeUnits("Kilometre")]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeUnitIdenticalText_SpecializedVector => """
        using SharpMeasures.Generators;

        [IncludeUnits("Metre")]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeUnitIdenticalText_VectorGroup => """
        using SharpMeasures.Generators;

        [IncludeUnits("Metre")]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeUnitIdenticalText_SpecializedVectorGroup => """
        using SharpMeasures.Generators;

        [IncludeUnits("Metre")]
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeUnitIdenticalText_VectorGroupMember => """
        using SharpMeasures.Generators;

        [IncludeUnits("Metre")]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeBaseIdenticalText_Scalar => """
        using SharpMeasures.Generators;

        [IncludeUnitBases("Metre")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IncludeAndExcludeBaseIdenticalText_SpecializedScalar => """
        using SharpMeasures.Generators;

        [IncludeUnitBases("Metre")]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [FixedUnitInstance("Metre", "Metres")]
        [PrefixedUnitInstance("Kilometre", "Kilometres", "Metre", MetricPrefixName.Kilo)]
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
