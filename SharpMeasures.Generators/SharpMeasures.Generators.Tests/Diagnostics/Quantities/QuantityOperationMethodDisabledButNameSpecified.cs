namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class QuantityOperationMethodDisabledButNameSpecified
{
    [Fact]
    public Task VerifyQuantityOperationMethodDisabledButNameSpecifiedDiagnosticsMessage() => AssertScalar(mirrored: false).VerifyDiagnostics();

    [Fact]
    public void Scalar() => AssertScalar(mirrored: false);

    [Fact]
    public void MirroredScalar() => AssertScalar(mirrored: true);

    [Fact]
    public void SpecializedScalar() => AssertSpecializedScalar(mirrored: false);

    [Fact]
    public void MirroredSpecializedScalar() => AssertSpecializedScalar(mirrored: true);

    [Fact]
    public void Vector() => AssertVector(mirrored: false);

    [Fact]
    public void MirroredVector() => AssertVector(mirrored: true);

    [Fact]
    public void SpecializedVector() => AssertSpecializedVector(mirrored: false);

    [Fact]
    public void MirroredSpecializedVector() => AssertSpecializedVector(mirrored: true);

    [Fact]
    public void VectorGroup() => AssertVectorGroup(mirrored: false);

    [Fact]
    public void MirroredVectorGroup() => AssertVectorGroup(mirrored: true);

    [Fact]
    public void SpecializedVectorGroup() => AssertSpecializedVectorGroup(mirrored: false);

    [Fact]
    public void MirroredSpecializedVectorGroup() => AssertSpecializedVectorGroup(mirrored: true);

    [Fact]
    public void VectorGroupMember() => AssertVectorGroupMember(mirrored: false);

    [Fact]
    public void MirroredVectorGroupMember() => AssertVectorGroupMember(mirrored: true);

    private static GeneratorVerifier AssertQuantityOperationMethodDisabledButNameSpecifiedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(QuantityOperationMethodDisabledButNameSpecifiedDiagnostics);
    private static IReadOnlyCollection<string> QuantityOperationMethodDisabledButNameSpecifiedDiagnostics { get; } = new string[] { DiagnosticIDs.QuantityOperationMethodDisabledButNameSpecified };

    private static string ScalarText(bool mirrored) => $$"""
        using SharpMeasures.Generators;

        [QuantityOperation(typeof(Length), typeof(Length), OperatorType.Division, Implementation = QuantityOperationImplementation.Operator, {{(mirrored ? "Mirrored" : string.Empty)}}MethodName = "Divide")]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(bool mirrored)
    {
        var source = ScalarText(mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, "\"Divide\"");

        return AssertQuantityOperationMethodDisabledButNameSpecifiedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText(bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Length), typeof(Length), OperatorType.Division, Implementation = QuantityOperationImplementation.Operator, {{(mirrored ? "Mirrored" : string.Empty)}}MethodName = "Divide")]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(bool mirrored)
    {
        var source = SpecializedScalarText(mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, "\"Divide\"");

        return AssertQuantityOperationMethodDisabledButNameSpecifiedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText(bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Displacement3), typeof(Length), OperatorType.Division, Implementation = QuantityOperationImplementation.Operator, {{(mirrored ? "Mirrored" : string.Empty)}}MethodName = "Divide")]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(bool mirrored)
    {
        var source = VectorText(mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, "\"Divide\"");

        return AssertQuantityOperationMethodDisabledButNameSpecifiedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText(bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Displacement3), typeof(Length), OperatorType.Division, Implementation = QuantityOperationImplementation.Operator, {{(mirrored ? "Mirrored" : string.Empty)}}MethodName = "Divide")]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(bool mirrored)
    {
        var source = SpecializedVectorText(mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, "\"Divide\"");

        return AssertQuantityOperationMethodDisabledButNameSpecifiedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupText(bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Position), typeof(Length), OperatorType.Division, Implementation = QuantityOperationImplementation.Operator, {{(mirrored ? "Mirrored" : string.Empty)}}MethodName = "Divide")]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }

        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroup(bool mirrored)
    {
        var source = VectorGroupText(mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, "\"Divide\"");

        return AssertQuantityOperationMethodDisabledButNameSpecifiedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
    }

    private static string SpecializedVectorGroupText(bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Position), typeof(Length), OperatorType.Division, Implementation = QuantityOperationImplementation.Operator, {{(mirrored ? "Mirrored" : string.Empty)}}MethodName = "Divide")]
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup(bool mirrored)
    {
        var source = SpecializedVectorGroupText(mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, "\"Divide\"");

        return AssertQuantityOperationMethodDisabledButNameSpecifiedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static string VectorGroupMemberText(bool mirrored) => $$"""
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Position3), typeof(Length), OperatorType.Division, Implementation = QuantityOperationImplementation.Operator, {{(mirrored ? "Mirrored" : string.Empty)}}MethodName = "Divide")]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(bool mirrored)
    {
        var source = VectorGroupMemberText(mirrored);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, "\"Divide\"");

        return AssertQuantityOperationMethodDisabledButNameSpecifiedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText);
    private static GeneratorVerifier SpecializedVectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators;

        [QuantityOperation(typeof(Length), typeof(Length), OperatorType.Division, Implementation = QuantityOperationImplementation.Operator)]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Length), typeof(Length), OperatorType.Division, Implementation = QuantityOperationImplementation.Operator)]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Displacement3), typeof(Length), OperatorType.Division, Implementation = QuantityOperationImplementation.Operator)]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText => """
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Displacement3), typeof(Length), OperatorType.Division, Implementation = QuantityOperationImplementation.Operator)]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupIdenticalText => """
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Position), typeof(Length), OperatorType.Division, Implementation = QuantityOperationImplementation.Operator)]
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText => """
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Position), typeof(Length), OperatorType.Division, Implementation = QuantityOperationImplementation.Operator)]
        [SpecializedVectorGroup(typeof(Position))]
        public static partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [VectorGroupMember(typeof(Displacement))]
        public partial class Displacement3 { }
        
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators;
        
        [QuantityOperation(typeof(Position3), typeof(Length), OperatorType.Division, Implementation = QuantityOperationImplementation.Operator)]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
