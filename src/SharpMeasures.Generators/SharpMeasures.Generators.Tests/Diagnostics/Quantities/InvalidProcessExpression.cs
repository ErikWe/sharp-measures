namespace SharpMeasures.Generators.Tests.Diagnostics.Quantities;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidProcessExpression
{
    [Fact]
    public Task VerifyInvalidProcessExpressionDiagnosticsMessage_Null() => AssertScalar(NullExpression).VerifyDiagnostics();

    [Theory]
    [MemberData(nameof(InvalidProcessExpressions))]
    public void Scalar(SourceSubtext expression) => AssertScalar(expression);

    [Theory]
    [MemberData(nameof(InvalidProcessExpressions))]
    public void SpecializedScalar(SourceSubtext expression) => AssertSpecializedScalar(expression);

    [Theory]
    [MemberData(nameof(InvalidProcessExpressions))]
    public void Vector(SourceSubtext expression) => AssertVector(expression);

    [Theory]
    [MemberData(nameof(InvalidProcessExpressions))]
    public void SpecializedVector(SourceSubtext expression) => AssertSpecializedVector(expression);

    [Theory]
    [MemberData(nameof(InvalidProcessExpressions))]
    public void VectorGroupMember(SourceSubtext expression) => AssertVectorGroupMember(expression);

    public static IEnumerable<object[]> InvalidProcessExpressions() => new object[][]
    {
        new object[] { NullExpression },
        new object[] { EmptyExpression }
    };

    private static SourceSubtext NullExpression { get; } = SourceSubtext.Covered("null");
    private static SourceSubtext EmptyExpression { get; } = SourceSubtext.Covered("\"\"");

    private static GeneratorVerifier AssertExactlyInvalidProcessExpressionDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidProcessExpressionDiagnostics);
    private static IReadOnlyCollection<string> InvalidProcessExpressionDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidProcessExpression };

    private static string ScalarText(SourceSubtext expression) => $$"""
        using SharpMeasures.Generators;

        [QuantityProcess("Name", {{expression}})]
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
            
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertScalar(SourceSubtext expression)
    {
        var source = ScalarText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, expression.Context.With(outerPrefix: "QuantityProcess(\"Name\", "));

        return AssertExactlyInvalidProcessExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(ScalarIdentical);
    }

    private static string SpecializedScalarText(SourceSubtext expression) => $$"""
        using SharpMeasures.Generators;

        [QuantityProcess("Name", {{expression}})]
        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedScalar(SourceSubtext expression)
    {
        var source = SpecializedScalarText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, expression.Context.With(outerPrefix: "QuantityProcess(\"Name\", "));

        return AssertExactlyInvalidProcessExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedScalarIdentical);
    }

    private static string VectorText(SourceSubtext expression) => $$"""
        using SharpMeasures.Generators;

        [QuantityProcess("Name", {{expression}})]
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVector(SourceSubtext expression)
    {
        var source = VectorText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, expression.Context.With(outerPrefix: "QuantityProcess(\"Name\", "));

        return AssertExactlyInvalidProcessExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorIdentical);
    }

    private static string SpecializedVectorText(SourceSubtext expression) => $$"""
        using SharpMeasures.Generators;

        [QuantityProcess("Name", {{expression}})]
        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVector(SourceSubtext expression)
    {
        var source = SpecializedVectorText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, expression.Context.With(outerPrefix: "QuantityProcess(\"Name\", "));

        return AssertExactlyInvalidProcessExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorIdentical);
    }

    private static string VectorGroupMemberText(SourceSubtext expression) => $$"""
        using SharpMeasures.Generators;

        [QuantityProcess("Name", {{expression}})]
        [VectorGroupMember(typeof(Position))]
        public partial class Position3 { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroupMember(SourceSubtext expression)
    {
        var source = VectorGroupMemberText(expression);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, expression.Context.With(outerPrefix: "QuantityProcess(\"Name\", "));

        return AssertExactlyInvalidProcessExpressionDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupMemberIdentical);
    }

    private static GeneratorVerifier ScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(ScalarIdenticalText);
    private static GeneratorVerifier SpecializedScalarIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedScalarIdenticalText);
    private static GeneratorVerifier VectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorIdenticalText);
    private static GeneratorVerifier SpecializedVectorIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorIdenticalText);
    private static GeneratorVerifier VectorGroupMemberIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupMemberIdenticalText);

    private static string ScalarIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedScalarIdenticalText => """
        using SharpMeasures.Generators;

        [SpecializedScalarQuantity(typeof(Length))]
        public partial class Distance { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorIdenticalText => """
        using SharpMeasures.Generators;

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorIdenticalText => """
        using SharpMeasures.Generators;

        [SpecializedVectorQuantity(typeof(Position3))]
        public partial class Displacement3 { }

        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Position3 { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string VectorGroupMemberIdenticalText => """
        using SharpMeasures.Generators;

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
