namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeNotStatic
{
    [Fact]
    public Task VectorGroup() => AssertVectorGroup().VerifyDiagnostics();

    [Fact]
    public Task SpecializedVectorGroup() => AssertSpecializedVectorGroup().VerifyDiagnostics();

    private static GeneratorVerifier AssertExactlyTypeNotStaticDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeNotStaticDiagnostics);
    private static IReadOnlyCollection<string> TypeNotStaticDiagnostics { get; } = new string[] { DiagnosticIDs.TypeNotStatic };

    private static string VectorGroupText => """
        using SharpMeasures.Generators;

        [VectorGroup(typeof(UnitOfLength))]
        public partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(VectorGroupText, target: "Position", prefix: "public partial class ");

        return AssertExactlyTypeNotStaticDiagnostics(VectorGroupText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(VectorGroupIdentical);
    }

    private static string SpecializedVectorGroupText => """
        using SharpMeasures.Generators;

        [SpecializedVectorGroup(typeof(Position))]
        public partial class Displacement { }

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertSpecializedVectorGroup()
    {
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(SpecializedVectorGroupText, target: "Displacement", prefix: "public partial class ");

        return AssertExactlyTypeNotStaticDiagnostics(SpecializedVectorGroupText).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(SpecializedVectorGroupIdentical);
    }

    private static GeneratorVerifier VectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(VectorGroupIdenticalText);
    private static GeneratorVerifier SpecializedVectorGroupIdentical => GeneratorVerifier.Construct<SharpMeasuresGenerator>(SpecializedVectorGroupIdenticalText);

    private static string VectorGroupIdenticalText => """
        using SharpMeasures.Generators;

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string SpecializedVectorGroupIdenticalText => """
        using SharpMeasures.Generators;

        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }

        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }

        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
