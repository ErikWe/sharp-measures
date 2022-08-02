namespace SharpMeasures.Generators.Tests.Diagnostics.Vectors;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class MissingVectorDimension
{
    [Fact]
    public Task NoNumber_ExactListAndVerify()
    {
        string source = Text("LengthVector");

        return AssertExactlyMissingVectorDimensionDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task NumberInMiddle_ExactListAndVerify()
    {
        string source = Text("Position3D");

        return AssertExactlyMissingVectorDimensionDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task NumberAndUnderscore_ExactListAndVerify()
    {
        string source = Text("Position3_");

        return AssertExactlyMissingVectorDimensionDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ResizedNoNumber_ExactListAndVerify()
    {
        string source = ResizedText("LengthVector");

        return AssertExactlyMissingVectorDimensionDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ResizedNumberInMiddle_ExactListAndVerify()
    {
        string source = ResizedText("Position2D");

        return AssertExactlyMissingVectorDimensionDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    [Fact]
    public Task ResizedNumberAndUnderscore_ExactListAndVerify()
    {
        string source = ResizedText("Position2_");

        return AssertExactlyMissingVectorDimensionDiagnosticsWithValidLocation(source).VerifyDiagnostics();
    }

    private static GeneratorVerifier AssertExactlyMissingVectorDimensionDiagnosticsWithValidLocation(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(MissingVectorDimensionDiagnostics).AssertAllDiagnosticsValidLocation();
    private static IReadOnlyCollection<string> MissingVectorDimensionDiagnostics { get; } = new string[] { DiagnosticIDs.MissingVectorDimension };

    private static string Text(string name) => $$"""
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

    private static string ResizedText(string name) => $$"""
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
}
