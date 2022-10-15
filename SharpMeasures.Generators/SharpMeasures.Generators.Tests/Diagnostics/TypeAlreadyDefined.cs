namespace SharpMeasures.Generators.Tests.Diagnostics;

using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Tests.Utility;
using SharpMeasures.Generators.Tests.Verify;

using System.Collections.Generic;
using System.Threading.Tasks;

using VerifyXunit;

using Xunit;

[UsesVerify]
public class TypeAlreadyDefined
{
    [Fact]
    public Task Unit_DefinedAsScalar() => Assert(ScalarDefinition, UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task Unit_DefinedAsSpecializedScalar() => Assert(SpecializedScalarDefinition, UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task Unit_DefinedAsVector() => Assert(VectorDefinition, UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task Unit_DefinedAsSpecializedVector() => Assert(SpecializedVectorDefinition, UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task Unit_DefinedAsVectorGroupMember() => Assert(VectorGroupMemberDefinition, UnitDefinition).VerifyDiagnostics();

    [Fact]
    public Task Scalar_DefinedAsUnit() => Assert(UnitDefinition, ScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task Scalar_DefinedAsSpecializedScalar() => Assert(SpecializedScalarDefinition, ScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task Scalar_DefinedAsVector() => Assert(VectorDefinition, ScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task Scalar_DefinedAsSpecializedVector() => Assert(SpecializedVectorDefinition, ScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task Scalar_DefinedAsVectorGroupMember() => Assert(VectorGroupMemberDefinition, ScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedScalar_DefinedAsUnit() => Assert(UnitDefinition, SpecializedScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedScalar_DefinedAsScalar() => Assert(ScalarDefinition, SpecializedScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedScalar_DefinedAsVector() => Assert(VectorDefinition, SpecializedScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedScalar_DefinedAsSpecializedVector() => Assert(SpecializedVectorDefinition, SpecializedScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedScalar_DefinedAsVectorGroupMember() => Assert(VectorGroupMemberDefinition, SpecializedScalarDefinition).VerifyDiagnostics();

    [Fact]
    public Task Vector_DefinedAsUnit() => Assert(UnitDefinition, VectorDefinition).VerifyDiagnostics();

    [Fact]
    public Task Vector_DefinedAsScalar() => Assert(ScalarDefinition, VectorDefinition).VerifyDiagnostics();

    [Fact]
    public Task Vector_DefinedAsSpecializedScalar() => Assert(SpecializedScalarDefinition, VectorDefinition).VerifyDiagnostics();

    [Fact]
    public Task Vector_DefinedAsSpecializedVector() => Assert(SpecializedVectorDefinition, VectorDefinition).VerifyDiagnostics();

    [Fact]
    public Task Vector_DefinedAsVectorGroupMember() => Assert(VectorGroupMemberDefinition, VectorDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVector_DefinedAsUnit() => Assert(UnitDefinition, SpecializedVectorDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVector_DefinedAsScalar() => Assert(ScalarDefinition, SpecializedVectorDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVector_DefinedAsSpecializedScalar() => Assert(SpecializedScalarDefinition, SpecializedVectorDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVector_DefinedAsVector() => Assert(VectorDefinition, SpecializedVectorDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVector_DefinedAsVectorGroupMember() => Assert(VectorGroupMemberDefinition, SpecializedVectorDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsUnit() => Assert(UnitDefinition, VectorGroupMemberDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsScalar() => Assert(ScalarDefinition, VectorGroupMemberDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsSpecializedScalar() => Assert(SpecializedScalarDefinition, VectorGroupMemberDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsVector() => Assert(VectorDefinition, VectorGroupMemberDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroupMember_DefinedAsSpecializedVector() => Assert(SpecializedVectorDefinition, VectorGroupMemberDefinition).VerifyDiagnostics();

    [Fact]
    public Task VectorGroup_DefinedAsSpecializedVectorGroup() => AssertStatic(SpecializedVectorGroupDefinition, VectorGroupDefinition).VerifyDiagnostics();

    [Fact]
    public Task SpecializedVectorGroup_DefinedAsVectorGroup() => AssertStatic(VectorGroupDefinition, SpecializedVectorGroupDefinition).VerifyDiagnostics();

    private static SourceLocationContext UnitDefinition { get; } = new SourceLocationContext(target: "Unit", postfix: "(typeof(Length))");
    private static SourceLocationContext ScalarDefinition { get; } = new SourceLocationContext(target: "ScalarQuantity", postfix: "(typeof(UnitOfLength))");
    private static SourceLocationContext SpecializedScalarDefinition { get; } = new SourceLocationContext(target: "SpecializedScalarQuantity", postfix: "(typeof(Length))");
    private static SourceLocationContext VectorDefinition { get; } = new SourceLocationContext(target: "VectorQuantity", postfix: "(typeof(UnitOfLength))");
    private static SourceLocationContext SpecializedVectorDefinition { get; } = new SourceLocationContext(target: "SpecializedVectorQuantity", postfix: "(typeof(Displacement2))");
    private static SourceLocationContext VectorGroupDefinition { get; } = new SourceLocationContext(target: "VectorGroup", postfix: "(typeof(UnitOfLength))");
    private static SourceLocationContext SpecializedVectorGroupDefinition { get; } = new SourceLocationContext(target: "SpecializedVectorGroup", postfix: "(typeof(Position))");
    private static SourceLocationContext VectorGroupMemberDefinition { get; } = new SourceLocationContext(target: "VectorGroupMember", postfix: "(typeof(Position))");

    private static GeneratorVerifier AssertExactlyTypeAlreadyDefinedDiagnostics(string source) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(source).AssertExactlyListedDiagnosticsIDsReported(TypeAlreadyDefinedDiagnostics);
    private static IReadOnlyCollection<string> TypeAlreadyDefinedDiagnostics { get; } = new string[] { DiagnosticIDs.TypeAlreadyDefined };

    private static string Text(SourceLocationContext otherDefinition, SourceLocationContext errorDefinition) => $$"""
        using SharpMeasures.Generators;
        
        [{{otherDefinition}}]
        [{{errorDefinition}}] // <-
        public partial class A2 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement2 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier Assert(SourceLocationContext otherDefinition, SourceLocationContext errorDefinition)
    {
        var source = Text(otherDefinition, errorDefinition);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, errorDefinition.With(outerPostfix: "] // <-"));

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(Identical(otherDefinition));
    }

    private static string StaticText(SourceLocationContext otherDefinition, SourceLocationContext errorDefinition) => $$"""
        using SharpMeasures.Generators;
        
        [{{otherDefinition}}]
        [{{errorDefinition}}] // <-
        public static partial class A { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement2 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static GeneratorVerifier AssertStatic(SourceLocationContext otherDefinition, SourceLocationContext errorDefinition)
    {
        var source = StaticText(otherDefinition, errorDefinition);
        var expectedLocation = ExpectedDiagnosticsLocation.TextSpan(source, errorDefinition.With(outerPostfix: "] // <-"));

        return AssertExactlyTypeAlreadyDefinedDiagnostics(source).AssertDiagnosticsLocation(expectedLocation).AssertIdenticalSources(IdenticalStatic(otherDefinition));
    }

    private static GeneratorVerifier Identical(SourceLocationContext otherDefinition) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalText(otherDefinition));
    private static GeneratorVerifier IdenticalStatic(SourceLocationContext otherDefinition) => GeneratorVerifier.Construct<SharpMeasuresGenerator>(IdenticalStaticText(otherDefinition));

    private static string IdenticalText(SourceLocationContext otherDefinition) => $$"""
        using SharpMeasures.Generators;
        
        [{{otherDefinition}}]
        public partial class A2 { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement2 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;

    private static string IdenticalStaticText(SourceLocationContext otherDefinition) => $$"""
        using SharpMeasures.Generators;
        
        [{{otherDefinition}}]
        public static partial class A { }
        
        [VectorQuantity(typeof(UnitOfLength))]
        public partial class Displacement2 { }
        
        [VectorGroup(typeof(UnitOfLength))]
        public static partial class Position { }
        
        [ScalarQuantity(typeof(UnitOfLength))]
        public partial class Length { }
        
        [Unit(typeof(Length))]
        public partial class UnitOfLength { }
        """;
}
