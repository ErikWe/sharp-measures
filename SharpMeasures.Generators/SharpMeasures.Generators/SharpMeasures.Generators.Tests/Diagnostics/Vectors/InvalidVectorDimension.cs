namespace SharpMeasures.Generators.Tests.Diagnostics.Vectors;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class InvalidVectorDimension
{
    private static string ImplicitText(string name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class {{name}} { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ExplicitText(string dimension) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [SharpMeasuresVector(typeof(UnitOfLength), Dimension = {{dimension}})]
        public partial class LengthVector { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    [Fact]
    public Task One_Implicit_ExactListAndVerify()
    {
        string source = ImplicitText("Position1");

        return AssertExactlyInvalidVectorDimensionDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task One_Explicit_ExactListAndVerify()
    {
        string source = ExplicitText("1");

        return AssertExactlyInvalidVectorDimensionDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Zero_Implicit_ExactListAndVerify()
    {
        string source = ImplicitText("Position0");

        return AssertExactlyInvalidVectorDimensionDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Zero_Explicit_ExactListAndVerify()
    {
        string source = ExplicitText("0");

        return AssertExactlyInvalidVectorDimensionDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Negative_ExactListAndVerify()
    {
        string source = ExplicitText("-1");

        return AssertExactlyInvalidVectorDimensionDiagnostics(source).VerifyDiagnostics();
    }

    private static string ResizedImplicitText(string name) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ResizedSharpMeasuresVector(typeof(Position3))]
        public partial class {{name}} { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string ResizedExplicitText(string dimension) => $$"""
        using SharpMeasures.Generators.Scalars;
        using SharpMeasures.Generators.Units;
        using SharpMeasures.Generators.Vectors;

        [ResizedSharpMeasuresVector(typeof(Position3), Dimension = {{dimension}})]
        public partial class LengthVector { }

        [SharpMeasuresVector(typeof(UnitOfLength))]
        public partial class Position3 { }

        [SharpMeasuresScalar(typeof(UnitOfLength))]
        public partial class Length { }

        [SharpMeasuresUnit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    [Fact]
    public Task One_ResizedImplicit_ExactListAndVerify()
    {
        string source = ResizedImplicitText("Position1");

        return AssertExactlyInvalidVectorDimensionDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task One_ResizedExplicit_ExactListAndVerify()
    {
        string source = ResizedExplicitText("1");

        return AssertExactlyInvalidVectorDimensionDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Zero_ResizedImplicit_ExactListAndVerify()
    {
        string source = ResizedImplicitText("Position0");

        return AssertExactlyInvalidVectorDimensionDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Zero_ResizedExplicit_ExactListAndVerify()
    {
        string source = ResizedExplicitText("0");

        return AssertExactlyInvalidVectorDimensionDiagnostics(source).VerifyDiagnostics();
    }

    [Fact]
    public Task Negative_ResizedExactListAndVerify()
    {
        string source = ResizedExplicitText("-1");

        return AssertExactlyInvalidVectorDimensionDiagnostics(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyInvalidVectorDimensionDiagnostics(string source)
        => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(InvalidVectorDimensionDiagnostics);

    private static IReadOnlyCollection<string> InvalidVectorDimensionDiagnostics { get; } = new string[] { DiagnosticIDs.InvalidVectorDimension };
}
